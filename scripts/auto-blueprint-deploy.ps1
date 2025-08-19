#!/usr/bin/env powershell
# PowerShell script to generate Blueprint Deploy JSON payload

param(
    [Parameter(Mandatory=$true)]
    [string]$KeycloakAdminPassword,

    [Parameter(Mandatory=$true)]
    [string]$MongoUri,

    [Parameter(Mandatory=$false)]
    [string]$KeycloakPublicUrl = "https://contta-keycloak-staging.onrender.com",

    [Parameter(Mandatory=$false)]
    [string]$CorsOrigins = "*.vercel.app,https://localhost:3000",

    [Parameter(Mandatory=$false)]
    [string]$ProductionUrl = "",

    [Parameter(Mandatory=$false)]
    [string]$RabbitUrl = ""
)

$OidcIssuer = "$KeycloakPublicUrl/realms/contta"

$templatePath = Join-Path $PSScriptRoot "blueprint-payload.template.json"
$payloadJson = Get-Content -Path $templatePath -Raw

# Substitui os placeholders com os valores fornecidos
$payloadJson = $payloadJson -replace '__KEYCLOAK_ADMIN_PASSWORD__', $KeycloakAdminPassword
$payloadJson = $payloadJson -replace '__KEYCLOAK_PUBLIC_URL__', $KeycloakPublicUrl
$payloadJson = $payloadJson -replace '__MONGODB_URI__', $MongoUri
$payloadJson = $payloadJson -replace '__OIDC_ISSUER__', $OidcIssuer
$payloadJson = $payloadJson -replace '__CORS_ORIGINS__', $CorsOrigins
$payloadJson = $payloadJson -replace '__PRODUCTION_URL__', $ProductionUrl
$payloadJson = $payloadJson -replace '__RABBITMQ_URL__', $RabbitUrl

# Imprime o JSON final para ser capturado pelo curl
Write-Output $payloadJson
