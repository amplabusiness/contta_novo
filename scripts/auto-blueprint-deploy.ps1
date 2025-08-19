#!/usr/bin/env powershell
# Windows PowerShell 5.1 friendly: Lê template, aplica overrides e aciona Render Blueprint API

[CmdletBinding()]
param(
    [Parameter(Mandatory=$false)] [string]$RenderApiToken,
    [Parameter(Mandatory=$false)] [string]$MongoUri,
    [Parameter(Mandatory=$false)] [string]$RabbitUrl,
    [Parameter(Mandatory=$false)] [string]$CorsOrigins = "*.vercel.app,https://localhost:3000",
    [Parameter(Mandatory=$false)] [string]$ProductionUrl,
    [switch]$ShowPayload
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8
[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12

# Carregar .env na raiz (se existir) e exportar variáveis para o processo
function Load-DotEnv {
    param([string]$Path)
    if (-not (Test-Path -LiteralPath $Path)) { return }
    try {
        $lines = Get-Content -LiteralPath $Path -Encoding UTF8
        foreach ($line in $lines) {
            if ([string]::IsNullOrWhiteSpace($line)) { continue }
            $trim = $line.Trim()
            if ($trim.StartsWith('#')) { continue }
            $idx = $trim.IndexOf('=')
            if ($idx -lt 1) { continue }
            $key = $trim.Substring(0, $idx).Trim()
            $val = $trim.Substring($idx + 1).Trim()
            if ($val.StartsWith('"') -and $val.EndsWith('"')) { $val = $val.Substring(1, $val.Length - 2) }
            if ($val.StartsWith("'") -and $val.EndsWith("'")) { $val = $val.Substring(1, $val.Length - 2) }
            if (-not [string]::IsNullOrWhiteSpace($key)) { $env:$key = $val }
        }
        Write-Host "Carregado .env: $Path"
    } catch {
        Write-Warning "Falha ao ler .env ($Path): $($_.Exception.Message)"
    }
}

# Detectar raiz do repo (pasta acima de scripts)
$repoRoot = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
$dotenvPath = Join-Path $repoRoot '.env'
Load-DotEnv -Path $dotenvPath

# 1) Token
if (-not $RenderApiToken -or $RenderApiToken.Trim() -eq '') {
    $RenderApiToken = $env:RENDER_API_TOKEN
}
if (-not $RenderApiToken -or $RenderApiToken.Trim() -eq '') {
    $RenderApiToken = Read-Host 'Cole seu RENDER_API_TOKEN (rnd_...)'
}
if (-not $RenderApiToken -or $RenderApiToken.Trim() -eq '') {
    Write-Error 'RENDER_API_TOKEN é obrigatório. Defina em env ou informe quando solicitado.'
}

# 2) Ler template
$templatePath = Join-Path $PSScriptRoot 'blueprint-payload.template.json'
if (-not (Test-Path -LiteralPath $templatePath)) {
    Write-Error "Template não encontrado: $templatePath"
}
$templateRaw = Get-Content -LiteralPath $templatePath -Raw -Encoding UTF8
$payload = $templateRaw | ConvertFrom-Json

# 3) Aplicar overrides (se fornecidos)
function Get-ServiceByName([object]$payload, [string]$serviceName) {
    return $payload.serviceDetails | Where-Object { $_.name -eq $serviceName } | Select-Object -First 1
}

$svcSearch = Get-ServiceByName $payload 'contta-searchapi-staging'
$svcExcel  = Get-ServiceByName $payload 'contta-excelparser-staging'
$svcConsumer = Get-ServiceByName $payload 'contta-consumerxml-staging'

if (-not $svcSearch) { Write-Warning 'Serviço contta-searchapi-staging não encontrado no template.' }
if (-not $svcExcel)  { Write-Warning 'Serviço contta-excelparser-staging não encontrado no template.' }
if (-not $svcConsumer){ Write-Warning 'Serviço contta-consumerxml-staging não encontrado no template.' }

# Garantir mapas envVars
foreach ($svc in @($svcSearch, $svcExcel, $svcConsumer)) {
    if ($null -ne $svc -and -not $svc.PSObject.Properties.Match('envVars')) {
        Add-Member -InputObject $svc -MemberType NoteProperty -Name envVars -Value (@{})
    }
}

if ($CorsOrigins -and $svcSearch) { $svcSearch.envVars.CORS_ORIGINS = $CorsOrigins }
if ($ProductionUrl -and $svcExcel) { $svcExcel.envVars.PRODUCTION_URL = $ProductionUrl }

# Preferir variáveis de ambiente se não passadas por parâmetro
if (-not $MongoUri -or $MongoUri.Trim() -eq '') { $MongoUri = $env:MONGODB_URI }
if (-not $RabbitUrl -or $RabbitUrl.Trim() -eq '') { $RabbitUrl = $env:RABBITMQ_URL }

if ($MongoUri) {
    if ($svcSearch) { $svcSearch.envVars.MONGODB_URI = $MongoUri }
    if ($svcExcel)  { $svcExcel.envVars.MONGODB_URI  = $MongoUri }
}
if ($RabbitUrl -and $svcConsumer) {
    $svcConsumer.envVars.RABBITMQ_URL = $RabbitUrl
}

# 4) Serializar payload
$body = $payload | ConvertTo-Json -Depth 50
if ($ShowPayload) {
    Write-Host '--- Payload que será enviado ---'
    Write-Output $body
    Write-Host '---------------------------------'
}

# 5) Chamar API da Render
$uri = 'https://api.render.com/v1/blueprints'
$headers = @{ Authorization = "Bearer $RenderApiToken"; 'Content-Type'='application/json'; Accept='application/json' }

try {
    $response = Invoke-WebRequest -UseBasicParsing -Method Post -Uri $uri -Headers $headers -Body $body -TimeoutSec 120
    $status = $response.StatusCode
    Write-Host ("Status HTTP: {0}" -f $status)
    Write-Host 'Resposta:'
    if ($response.Content) { Write-Output $response.Content } else { Write-Output ($response | ConvertTo-Json -Depth 10) }
    if ($status -lt 200 -or $status -ge 300) { exit 1 }
}
catch {
    Write-Error 'Falha ao chamar a API da Render.'
    if ($_.Exception.Response -and $_.Exception.Response.StatusCode) {
        try { Write-Host ("Status HTTP: {0}" -f [int]$_.Exception.Response.StatusCode) } catch {}
    }
    if ($_.ErrorDetails.Message) { Write-Host $_.ErrorDetails.Message }
    elseif ($_.Exception.Response) {
        try {
            $reader = New-Object System.IO.StreamReader($_.Exception.Response.GetResponseStream())
            $errBody = $reader.ReadToEnd()
            if ($errBody) { Write-Output $errBody }
        } catch {}
    }
    exit 1
}
