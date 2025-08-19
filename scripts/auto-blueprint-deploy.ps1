#!/usr/bin/env powershell
# PowerShell script to execute Blueprint Deploy on Windows

param(
    [Parameter(Mandatory=$false)]
    [string]$RenderApiToken
)

if (-not $RenderApiToken) {
    $RenderApiToken = $env:RENDER_API_TOKEN
}
if (-not $RenderApiToken) {
    Write-Error "RENDER_API_TOKEN não definido. Defina como variável de ambiente ou passe como parâmetro."
    exit 1
}

Write-Host "🚀 EXECUTANDO BLUEPRINT DEPLOY AUTOMATICAMENTE" -ForegroundColor Green
Write-Host "=" * 50
Write-Host "Repo: amplabusiness/contta_novo"
Write-Host "Blueprint: render.yaml"
Write-Host ""

# ===== Coleta/Defaults de variáveis necessárias =====

# URL pública do Keycloak (default para serviço atual)
$KeycloakPublicUrl = if ($env:KEYCLOAK_PUBLIC_URL -and $env:KEYCLOAK_PUBLIC_URL.Trim() -ne '') { $env:KEYCLOAK_PUBLIC_URL.TrimEnd('/') } else { "https://contta-keycloak-staging.onrender.com" }

# OIDC Issuer (default baseado no Keycloak público)
$OIDCIssuer = if ($env:OIDC_ISSUER -and $env:OIDC_ISSUER.Trim() -ne '') { $env:OIDC_ISSUER.TrimEnd('/') } else { "$KeycloakPublicUrl/realms/contta" }

# CORS_ORIGINS default
$CorsOrigins = if ($env:CORS_ORIGINS -and $env:CORS_ORIGINS.Trim() -ne '') { $env:CORS_ORIGINS } else { "*.vercel.app,https://localhost:3000" }

# PRODUCTION_URL opcional
$ProductionUrl = $env:PRODUCTION_URL

# Rabbit opcional
$RabbitUrl = $env:RABBITMQ_URL

# KEYCLOAK_ADMIN_PASSWORD
$KeycloakAdminPassword = $env:KEYCLOAK_ADMIN_PASSWORD
if (-not $KeycloakAdminPassword -or $KeycloakAdminPassword.Trim() -eq '') {
    $sec = Read-Host -Prompt "Defina KEYCLOAK_ADMIN_PASSWORD (não exibido)" -AsSecureString
    $KeycloakAdminPassword = [Runtime.InteropServices.Marshal]::PtrToStringAuto([Runtime.InteropServices.Marshal]::SecureStringToBSTR($sec))
}

# MONGODB_URI
$MongoUri = $env:MONGODB_URI
if (-not $MongoUri -or $MongoUri.Trim() -eq '') {
    Write-Host "Exemplo Atlas SRV: mongodb+srv://USER:PASS@cluster0.xxxx.mongodb.net/contta?retryWrites=true&w=majority" -ForegroundColor DarkGray
    $MongoUri = Read-Host -Prompt "Cole sua MONGODB_URI"
}

if (-not $MongoUri -or $MongoUri.Trim() -eq '') {
    Write-Error "MONGODB_URI não fornecida. Abortando."
    exit 1
}

# Blueprint payload
$blueprintPayload = @{
    type = "blueprint"
    repo = "https://github.com/amplabusiness/contta_novo"
    branch = "main"
    blueprintPath = "render.yaml"
    serviceDetails = @(
        @{
            name = "contta-keycloak-staging"
            env = "docker"
            plan = "starter"
            envVars = @{
                KEYCLOAK_ADMIN = "admin"
                KEYCLOAK_ADMIN_PASSWORD = "$KeycloakAdminPassword"
                KC_HOSTNAME = "$KeycloakPublicUrl"
                KC_HOSTNAME_URL = "$KeycloakPublicUrl"
                KC_HOSTNAME_ADMIN_URL = "$KeycloakPublicUrl"
                KEYCLOAK_PUBLIC_URL = "$KeycloakPublicUrl"
            }
        },
        @{
            name = "contta-searchapi-staging"
            env = "docker" 
            plan = "starter"
            envVars = @{
                NODE_ENV = "production"
                PORT = "5001"
                MONGODB_URI = "$MongoUri"
                OIDC_ISSUER = "$OIDCIssuer"
                OIDC_AUDIENCE = "contta-portal"
                CORS_ORIGINS = "$CorsOrigins"
            }
        },
        @{
            name = "contta-excelparser-staging"
            env = "docker"
            plan = "starter" 
            envVars = @{
                NODE_ENV = "production"
                PORT = "5002"
                OIDC_ISSUER = "$OIDCIssuer"
                OIDC_AUDIENCE = "contta-portal"
                PRODUCTION_URL = "$ProductionUrl"
            }
        },
        @{
            name = "contta-consumerxml-staging"
            env = "docker"
            plan = "starter"
            envVars = @{
                RABBITMQ_URL = "$RabbitUrl"
                RABBITMQ_QUEUE = "Modelo55"
                RABBITMQ_PREFETCH = "20"
                "RabbitMQ__Durable" = "true"
                "RabbitMQ__Exclusive" = "false"
                "RabbitMQ__AutoDelete" = "false"
                "RabbitMQ__DeadLetterExchange" = "dlx.nfe"
                "RabbitMQ__DeadLetterRoutingKey" = "Modelo55.dlq"
            }
        }
    )
}

Write-Host "📋 Executando Blueprint Deploy..." -ForegroundColor Yellow

try {
    $body = $blueprintPayload | ConvertTo-Json -Depth 10
    $response = Invoke-RestMethod -Uri "https://api.render.com/v1/blueprints" `
        -Method Post `
        -Headers @{
            "Authorization" = "Bearer $RenderApiToken"
            "Content-Type" = "application/json"
            "Accept" = "application/json"
        } `
        -Body $body

    Write-Host "✅ Blueprint Deploy iniciado!" -ForegroundColor Green
    Write-Host ($response | ConvertTo-Json -Depth 5)

    # Extract Service IDs
    Write-Host ""
    Write-Host "📋 Service IDs extraídos:" -ForegroundColor Cyan
    
    $keycloakId = ($response.services | Where-Object { $_.name -eq "contta-keycloak-staging" }).id
    $searchId = ($response.services | Where-Object { $_.name -eq "contta-searchapi-staging" }).id
    $excelId = ($response.services | Where-Object { $_.name -eq "contta-excelparser-staging" }).id
    $consumerId = ($response.services | Where-Object { $_.name -eq "contta-consumerxml-staging" }).id

    Write-Host "RENDER_SERVICE_ID_KEYCLOAK=$keycloakId" -ForegroundColor White
    Write-Host "RENDER_SERVICE_ID_SEARCHAPI=$searchId" -ForegroundColor White
    Write-Host "RENDER_SERVICE_ID_EXCELPARSER=$excelId" -ForegroundColor White
    Write-Host "RENDER_SERVICE_ID_CONSUMERXML=$consumerId" -ForegroundColor White

    Write-Host ""
    Write-Host "🔄 Monitorando deploys..." -ForegroundColor Yellow
    
    # Monitor function
    function Watch-RenderService {
        param($serviceId, $serviceName)
        
        $maxAttempts = 60
        $attempt = 0
        
        while ($attempt -lt $maxAttempts) {
            try {
                $deployResponse = Invoke-RestMethod -Uri "https://api.render.com/v1/services/$serviceId/deploys?limit=1" `
                    -Headers @{ "Authorization" = "Bearer $RenderApiToken" }
                
                $status = $deployResponse[0].status
                Write-Host "[$serviceName] Status: $status" -ForegroundColor Gray
                
                if ($status -eq "live") {
                    Write-Host "✅ [$serviceName] Deploy concluído!" -ForegroundColor Green
                    return $true
                } elseif ($status -eq "failed") {
                    Write-Host "❌ [$serviceName] Deploy falhou!" -ForegroundColor Red
                    return $false
                }
                
                Start-Sleep 10
                $attempt++
            } catch {
                Write-Host "⚠️ [$serviceName] Erro ao verificar status: $($_.Exception.Message)" -ForegroundColor Yellow
                Start-Sleep 10
                $attempt++
            }
        }
        
        Write-Host "⏰ [$serviceName] Timeout aguardando deploy" -ForegroundColor Yellow
        return $false
    }

    # Monitor services sequencialmente para simplificar escopo/saída
    Watch-RenderService -serviceId $keycloakId -serviceName "Keycloak" | Out-Null
    Watch-RenderService -serviceId $searchId -serviceName "Search API" | Out-Null
    Watch-RenderService -serviceId $excelId -serviceName "Excel Parser" | Out-Null
    Watch-RenderService -serviceId $consumerId -serviceName "Consumer" | Out-Null

    Write-Host ""
    Write-Host "🎯 BLUEPRINT DEPLOY CONCLUÍDO!" -ForegroundColor Green
    Write-Host "URLs dos serviços:" -ForegroundColor Cyan
    Write-Host "- Keycloak: https://contta-keycloak-staging.onrender.com"
    Write-Host "- Search API: https://contta-searchapi-staging.onrender.com"
    Write-Host "- Excel Parser: https://contta-excelparser-staging.onrender.com"

} catch {
    Write-Host "❌ Erro ao executar Blueprint Deploy: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}
