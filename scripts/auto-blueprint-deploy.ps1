#!/usr/bin/env powershell
# PowerShell script to generate Blueprint Deploy JSON payload (sem Keycloak e sem try/catch)

param(
    [Parameter(Mandatory=$true)]
    [string]$MongoUri,

    [Parameter(Mandatory=$false)]
    [string]$CorsOrigins = "*.vercel.app,https://localhost:3000",

    [Parameter(Mandatory=$false)]
    [string]$ProductionUrl = "",

    [Parameter(Mandatory=$false)]
    [string]$RabbitUrl = ""
)

$templatePath = Join-Path $PSScriptRoot "blueprint-payload.template.json"
if (-not (Test-Path -Path $templatePath)) {
    Write-Error "Template não encontrado: $templatePath"
    exit 1
}

$payloadJson = Get-Content -Path $templatePath -Raw -Encoding UTF8

function Normalize-MongoUri {
    param([string]$uri)
    if ([string]::IsNullOrWhiteSpace($uri)) { return $uri }
    $hasPath = $false

    if ($uri -match '^[^?]*\/([^\/?]+)\?') { $hasPath = $true }
    elseif ($uri -match '^[^?]*\/([^\/?]+)$') { $hasPath = ($Matches[1] -ne '') }
    elseif ($uri -match '^[^?]*\/$') { $hasPath = $false }
    else {
        if ($uri -notmatch '\/[^\/]+$' -and $uri -notmatch '\/[^\/?]+\?') { $hasPath = $false }
    }

    if (-not $hasPath) {
        if ($uri -match '\?$') {
            return $uri -replace '\?$', '/contta?retryWrites=true&w=majority'
        } elseif ($uri -match '\/\?$') {
            return $uri -replace '/\?$', '/contta?retryWrites=true&w=majority'
        } elseif ($uri -match '\/$') {
            return "$uri" + 'contta?retryWrites=true&w=majority'
        } elseif ($uri -match '\?') {
            return $uri -replace '\?', '/contta?'
        } else {
            return "$uri" + '/contta?retryWrites=true&w=majority'
        }
    }
    return $uri
}

$MongoUriNorm = Normalize-MongoUri -uri $MongoUri

function Convert-ToJsonStringLiteral {
    param([string]$s)
    if ($null -eq $s) { return '' }
    $json = ($s | ConvertTo-Json -Compress)
    if ($json.StartsWith('"') -and $json.EndsWith('"')) {
        $json = $json.Substring(1, $json.Length - 2)
    }
    return $json
}

$MongoUriEsc      = Convert-ToJsonStringLiteral -s $MongoUriNorm
$CorsOriginsEsc   = Convert-ToJsonStringLiteral -s $CorsOrigins
$ProductionUrlEsc = Convert-ToJsonStringLiteral -s $ProductionUrl
$RabbitUrlEsc     = Convert-ToJsonStringLiteral -s $RabbitUrl

$payloadJson = $payloadJson.Replace('__MONGODB_URI__', $MongoUriEsc)
$payloadJson = $payloadJson.Replace('__CORS_ORIGINS__', $CorsOriginsEsc)
$payloadJson = $payloadJson.Replace('__PRODUCTION_URL__', $ProductionUrlEsc)
$payloadJson = $payloadJson.Replace('__RABBITMQ_URL__', $RabbitUrlEsc)

[Console]::OutputEncoding = [System.Text.Encoding]::UTF8
Write-Output $payloadJson
