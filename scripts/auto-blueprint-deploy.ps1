#!/usr/bin/env powershell
# PowerShell script to execute Blueprint Deploy on Windows

param(
    [Parameter(Mandatory=$true)]
    [string]$RenderApiToken
)

Write-Host "🚀 EXECUTANDO BLUEPRINT DEPLOY AUTOMATICAMENTE" -ForegroundColor Green
Write-Host "=" * 50
Write-Host "Repo: amplabusiness/contta_novo"
Write-Host "Blueprint: render.yaml"
Write-Host ""

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
                KEYCLOAK_ADMIN_PASSWORD = "ConttaKeycloak2025!@#"
            }
        },
        @{
            name = "contta-searchapi-staging"
            env = "docker" 
            plan = "starter"
            envVars = @{
                NODE_ENV = "production"
                PORT = "5001"
                MONGODB_URI = "mongodb://placeholder"
                OIDC_ISSUER = "https://contta-keycloak-staging.onrender.com/realms/contta"
                OIDC_AUDIENCE = "contta-portal"
                CORS_ORIGINS = "http://localhost:3000,https://contta-portal.vercel.app"
            }
        },
        @{
            name = "contta-excelparser-staging"
            env = "docker"
            plan = "starter" 
            envVars = @{
                NODE_ENV = "production"
                PORT = "5002"
                OIDC_ISSUER = "https://contta-keycloak-staging.onrender.com/realms/contta"
                OIDC_AUDIENCE = "contta-portal"
                PRODUCTION_URL = "https://contta-portal.vercel.app"
            }
        },
        @{
            name = "contta-consumerxml-staging"
            env = "docker"
            plan = "starter"
            envVars = @{
                RABBITMQ_URL = "amqp://placeholder"
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
} | ConvertTo-Json -Depth 10

Write-Host "📋 Executando Blueprint Deploy..." -ForegroundColor Yellow

try {
    $response = Invoke-RestMethod -Uri "https://api.render.com/v1/blueprints" `
        -Method Post `
        -Headers @{
            "Authorization" = "Bearer $RenderApiToken"
            "Content-Type" = "application/json"
        } `
        -Body $blueprintPayload

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
    function Monitor-Service {
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

    # Monitor all services
    $jobs = @()
    $jobs += Start-Job -ScriptBlock { Monitor-Service $args[0] $args[1] } -ArgumentList $keycloakId, "Keycloak"
    $jobs += Start-Job -ScriptBlock { Monitor-Service $args[0] $args[1] } -ArgumentList $searchId, "Search API"
    $jobs += Start-Job -ScriptBlock { Monitor-Service $args[0] $args[1] } -ArgumentList $excelId, "Excel Parser"
    $jobs += Start-Job -ScriptBlock { Monitor-Service $args[0] $args[1] } -ArgumentList $consumerId, "Consumer"

    # Wait for all jobs
    $jobs | Wait-Job | Receive-Job
    $jobs | Remove-Job

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
