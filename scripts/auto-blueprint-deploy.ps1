#!/usr/bin/env powershell
# Generate Blueprint Deploy JSON payload (Mongo optional; Rabbit via template)

param(
    [Parameter(Mandatory=$false)]
    [string]$MongoUri = "",

    [Parameter(Mandatory=$false)]
    [string]$CorsOrigins = "*.vercel.app,https://localhost:3000",

    [Parameter(Mandatory=$false)]
    [string]$ProductionUrl = "",

    [Parameter(Mandatory=$false)]
    [string]$RabbitUrl = ""
)

$templatePath = Join-Path $PSScriptRoot "blueprint-payload.template.json"
if (-not (Test-Path -Path $templatePath)) {
    Write-Error "Template not found: $templatePath"
    exit 1
}

$payloadJson = Get-Content -Path $templatePath -Raw -Encoding UTF8

function Convert-ToJsonStringLiteral {
    param([string]$s)
    if ($null -eq $s) { return '' }
    $json = ($s | ConvertTo-Json -Compress)
    if ($json.StartsWith('"') -and $json.EndsWith('"')) {
        $json = $json.Substring(1, $json.Length - 2)
    }
    return $json
}

$CorsOriginsEsc   = Convert-ToJsonStringLiteral -s $CorsOrigins
$ProductionUrlEsc = Convert-ToJsonStringLiteral -s $ProductionUrl
$RabbitUrlEsc     = Convert-ToJsonStringLiteral -s $RabbitUrl

$payloadJson = $payloadJson.Replace('__CORS_ORIGINS__', $CorsOriginsEsc)
$payloadJson = $payloadJson.Replace('__PRODUCTION_URL__', $ProductionUrlEsc)
$payloadJson = $payloadJson.Replace('__RABBITMQ_URL__', $RabbitUrlEsc)

[Console]::OutputEncoding = [System.Text.Encoding]::UTF8
Write-Output $payloadJson
