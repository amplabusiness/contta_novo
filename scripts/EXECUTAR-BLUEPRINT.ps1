param(
  [Parameter(Mandatory=$false)]
  [string]$RenderApiToken = ""
)

$ErrorActionPreference = 'Stop'
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8

if (-not $RenderApiToken) {
  $RenderApiToken = Read-Host 'Cole seu RENDER_API_TOKEN (rnd_...)'
}
if (-not $RenderApiToken) {
  Write-Error 'Token não informado.'; exit 1
}

$templatePath = Join-Path $PSScriptRoot 'blueprint-payload.template.json'
if(-not (Test-Path $templatePath)){
  Write-Error "Template não encontrado: $templatePath"; exit 1
}
$payload = Get-Content -Raw -Encoding UTF8 $templatePath

$headers = @{ 
  Authorization = "Bearer $RenderApiToken"; 
  'Content-Type' = 'application/json';
  Accept = 'application/json'
}

try {
  $response = Invoke-WebRequest -UseBasicParsing -Method Post -Uri 'https://api.render.com/v1/blueprints' -Headers $headers -Body $payload
  Write-Host ("Status HTTP: {0}" -f $response.StatusCode)
  Write-Host 'Resposta:'
  Write-Output $response.Content
} catch {
  Write-Host 'Status HTTP: 000'
  Write-Host 'Resposta:'
  Write-Output $_.Exception.Message
  if ($_.Exception.Response -and $_.Exception.Response.Content) {
    Write-Output $_.Exception.Response.Content
  }
  exit 1
}
