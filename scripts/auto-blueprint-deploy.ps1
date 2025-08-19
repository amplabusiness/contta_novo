#!/usr/bin/env powershell
# PowerShell script to execute Blueprint Deploy on Windows

param(
    [Parameter(Mandatory=$false)]
    [string]$RenderApiToken
)

# =============================================================================
# Helper Functions
# =============================================================================

function Watch-RenderService {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory=$true)]
        [string]$ServiceId,

        [Parameter(Mandatory=$true)]
        [string]$ServiceName,

        [Parameter(Mandatory=$true)]
        [string]$ApiToken
    )
    
    $maxAttempts = 90 # 15 minutos (90 * 10s)
    $attempt = 0
    
    while ($attempt -lt $maxAttempts) {
        try {
            $headers = @{ "Authorization" = "Bearer $ApiToken"; "Accept" = "application/json" }
            $uri = "https://api.render.com/v1/services/$ServiceId/deploys?limit=1"
            $deploys = Invoke-RestMethod -Uri $uri -Headers $headers
            
            if ($deploys.Count -gt 0) {
                $status = $deploys[0].deploy.status
                Write-Host "[$ServiceName] Status: $status" -ForegroundColor Gray
                
                if ($status -eq "live") {
                    Write-Host "✅ [$ServiceName] Deploy concluído!" -ForegroundColor Green
                    return $true
                } elseif ($status -in @("build_failed", "failed", "canceled")) {
                    Write-Host "❌ [$ServiceName] Deploy falhou com status: $status!" -ForegroundColor Red
                    return $false
                }
            } else {
                Write-Host "[$ServiceName] Aguardando início do deploy..." -ForegroundColor Gray
            }
        } catch {
            Write-Host "⚠️ [$ServiceName] Erro ao verificar status: $($_.Exception.Message)" -ForegroundColor Yellow
        }
        
        Start-Sleep -Seconds 10
        $attempt++
    }
    
    Write-Host "⏰ [$ServiceName] Timeout aguardando deploy." -ForegroundColor Yellow
    return $false
}


# =============================================================================
# Main Script
# =============================================================================

if (-not $RenderApiToken) {
    $RenderApiToken = $env:RENDER_API_TOKEN
}
if (-not $RenderApiToken) {
    Write-Error "RENDER_API_TOKEN não definido. Defina como variável de ambiente ou passe como parâmetro."
    exit 1
}

Write-Host "🚀 EXECUTANDO BLUEPRINT DEPLOY AUTOMATICAMENTE" -ForegroundColor Green
Write-Host "=" * 50

# --- Coleta/Defaults de variáveis necessárias ---

$KeycloakPublicUrl = if ($env:KEYCLOAK_PUBLIC_URL -and $env:KEYCLOAK_PUBLIC_URL.Trim() -ne '') { $env:KEYCLOAK_PUBLIC_URL.TrimEnd('/') } else { "https://contta-keycloak-staging.onrender.com" }
$OIDCIssuer = if ($env:OIDC_ISSUER -and $env:OIDC_ISSUER.Trim() -ne '') { $env:OIDC_ISSUER.TrimEnd('/') } else { "$KeycloakPublicUrl/realms/contta" }
$CorsOrigins = if ($env:CORS_ORIGINS -and $env:CORS_ORIGINS.Trim() -ne '') { $env:CORS_ORIGINS } else { "*.vercel.app,https://localhost:3000" }
$ProductionUrl = $env:PRODUCTION_URL
$RabbitUrl = $env:RABBITMQ_URL

$KeycloakAdminPassword = $env:KEYCLOAK_ADMIN_PASSWORD
if (-not $KeycloakAdminPassword -or $KeycloakAdminPassword.Trim() -eq '') {
    $sec = Read-Host -Prompt "Defina KEYCLOAK_ADMIN_PASSWORD (não exibido)" -AsSecureString
    $KeycloakAdminPassword = [Runtime.InteropServices.Marshal]::PtrToStringAuto([Runtime.InteropServices.Marshal]::SecureStringToBSTR($sec))
}

$MongoUri = $env:MONGODB_URI
if (-not $MongoUri -or $MongoUri.Trim() -eq '') {
    Write-Host "Exemplo Atlas SRV: mongodb+srv://USER:PASS@cluster0.xxxx.mongodb.net/contta?retryWrites=true&w=majority" -ForegroundColor DarkGray
    $MongoUri = Read-Host -Prompt "Cole sua MONGODB_URI"
}

if (-not ($MongoUri -and $MongoUri.Trim() -ne '')) {
    Write-Error "MONGODB_URI não fornecida. Abortando."
    exit 1
}

# --- Construção do Payload ---

$blueprintPayload = @{
    repo = "https://github.com/amplabusiness/contta_novo"
    branch = "main"
    blueprintPath = "render.yaml"
    serviceDetails = @(
        @{
            name = "contta-keycloak-staging"
            envVars = @{
                KEYCLOAK_ADMIN_PASSWORD = $KeycloakAdminPassword
                KC_HOSTNAME = $KeycloakPublicUrl
                KC_HOSTNAME_URL = $KeycloakPublicUrl
                KC_HOSTNAME_ADMIN_URL = $KeycloakPublicUrl
            }
        },
        @{
            name = "contta-searchapi-staging"
            envVars = @{
                MONGODB_URI = $MongoUri
                OIDC_ISSUER = $OIDCIssuer
                CORS_ORIGINS = $CorsOrigins
            }
        },
        @{
            name = "contta-excelparser-staging"
            envVars = @{
                OIDC_ISSUER = $OIDCIssuer
                PRODUCTION_URL = $ProductionUrl
            }
        },
        @{
            name = "contta-consumerxml-staging"
            envVars = @{
                RABBITMQ_URL = $RabbitUrl
            }
        }
    )
}

$headers = @{
    "Authorization" = "Bearer $RenderApiToken"
    "Content-Type"  = "application/json"
    "Accept"        = "application/json"
}

# --- Execução e Monitoramento ---

Write-Host "📋 Executando Blueprint Deploy..." -ForegroundColor Yellow
$body = $blueprintPayload | ConvertTo-Json -Depth 10

# Envolvendo a chamada da API em um bloco try/catch para melhor tratamento de erros
try {
    $response = Invoke-RestMethod -Uri "https://api.render.com/v1/blueprints" -Method Post -Headers $headers -Body $body

    Write-Host "✅ Blueprint Deploy iniciado com sucesso!" -ForegroundColor Green
    $blueprintId = $response.id
    Write-Host "Blueprint ID: $blueprintId"

    $keycloak = $response.services | Where-Object { $_.name -eq "contta-keycloak-staging" }
    $searchApi = $response.services | Where-Object { $_.name -eq "contta-searchapi-staging" }
    $excelParser = $response.services | Where-Object { $_.name -eq "contta-excelparser-staging" }
    $consumer = $response.services | Where-Object { $_.name -eq "contta-consumerxml-staging" }

    Write-Host ""
    Write-Host "🔄 Monitorando deploys... (pode levar vários minutos)" -ForegroundColor Yellow

    Watch-RenderService -ServiceId $keycloak.id -ServiceName "Keycloak" -ApiToken $RenderApiToken
    Watch-RenderService -ServiceId $searchApi.id -ServiceName "Search API" -ApiToken $RenderApiToken
    Watch-RenderService -ServiceId $excelParser.id -ServiceName "Excel Parser" -ApiToken $RenderApiToken
    Watch-RenderService -ServiceId $consumer.id -ServiceName "Consumer" -ApiToken $RenderApiToken

    Write-Host ""
    Write-Host "🎯 BLUEPRINT DEPLOY CONCLUÍDO!" -ForegroundColor Green
    Write-Host "URLs dos serviços:" -ForegroundColor Cyan
    Write-Host "- Keycloak: $($keycloak.url)"
    Write-Host "- Search API: $($searchApi.url)"
    Write-Host "- Excel Parser: $($excelParser.url)"

} catch {
    Write-Host "❌ Erro ao executar Blueprint Deploy." -ForegroundColor Red
    if ($_.Exception.Response) {
        $errorResponse = $_.Exception.Response.GetResponseStream()
        $reader = New-Object System.IO.StreamReader($errorResponse)
        $reader.BaseStream.Position = 0
        $errorBody = $reader.ReadToEnd()
        Write-Host "Detalhes do erro da API: $errorBody" -ForegroundColor Red
    } else {
        Write-Host "Detalhes do erro: $($_.Exception.Message)" -ForegroundColor Red
    }
    exit 1
}
