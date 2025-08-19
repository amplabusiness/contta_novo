#!/usr/bin/env powershell
# PowerShell script to generate Blueprint Deploy JSON payload

param(
    [Parameter(Mandatory=$true)]
    [string]$MongoUri,

    [Parameter(Mandatory=$false)]
    [string]$KeycloakAdminPassword = "",

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

function Convert-ToJsonStringLiteral {
    param([string]$s)
    if ($null -eq $s) { return '' }
    # Converte para literal JSON seguro, usando conversão para JSON de um objeto simples
    $json = ($s | ConvertTo-Json -Compress)
    # Remove aspas externas para poder injetar no template já com aspas
    if ($json.StartsWith('"') -and $json.EndsWith('"')) {
        $json = $json.Substring(1, $json.Length - 2)
    }
    return $json
}

$KeycloakAdminPasswordEsc = Convert-ToJsonStringLiteral -s $KeycloakAdminPassword
$KeycloakPublicUrlEsc     = Convert-ToJsonStringLiteral -s $KeycloakPublicUrl
$MongoUriEsc              = Convert-ToJsonStringLiteral -s $MongoUri
$OidcIssuerEsc            = Convert-ToJsonStringLiteral -s $OidcIssuer
$CorsOriginsEsc           = Convert-ToJsonStringLiteral -s $CorsOrigins
$ProductionUrlEsc         = Convert-ToJsonStringLiteral -s $ProductionUrl
$RabbitUrlEsc             = Convert-ToJsonStringLiteral -s $RabbitUrl

# Substitui os placeholders com os valores fornecidos (substituição literal)
$payloadJson = $payloadJson.Replace('__KEYCLOAK_ADMIN_PASSWORD__', $KeycloakAdminPasswordEsc)
$payloadJson = $payloadJson.Replace('__KEYCLOAK_PUBLIC_URL__', $KeycloakPublicUrlEsc)
$payloadJson = $payloadJson.Replace('__MONGODB_URI__', $MongoUriEsc)
$payloadJson = $payloadJson.Replace('__OIDC_ISSUER__', $OidcIssuerEsc)
$payloadJson = $payloadJson.Replace('__CORS_ORIGINS__', $CorsOriginsEsc)
$payloadJson = $payloadJson.Replace('__PRODUCTION_URL__', $ProductionUrlEsc)
$payloadJson = $payloadJson.Replace('__RABBITMQ_URL__', $RabbitUrlEsc)

# Imprime o JSON final para ser capturado pelo curl
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8
Write-Output $payloadJson
