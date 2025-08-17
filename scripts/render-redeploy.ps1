param(
  [Parameter(Mandatory=$true)][string] $ServiceId,
  [Parameter(Mandatory=$false)][int] $TimeoutSec = 600
)

if (-not $env:RENDER_API_TOKEN -or $env:RENDER_API_TOKEN.Trim() -eq '') {
  Write-Error 'Defina a variável de ambiente RENDER_API_TOKEN com seu token da Render.'
  exit 1
}

$headers = @{ Authorization = "Bearer $($env:RENDER_API_TOKEN)"; 'Content-Type' = 'application/json' }
$deployUrl = "https://api.render.com/v1/services/$ServiceId/deploys"
$deployBody = '{"clearCache":true}'

Write-Host "Disparando redeploy para serviço $ServiceId..."
$resp = Invoke-RestMethod -Method Post -Uri $deployUrl -Headers $headers -Body $deployBody -ErrorAction Stop
$deployId = $resp.id
if (-not $deployId) { Write-Error 'Nenhum deploy id retornado.'; exit 1 }
Write-Host "Deploy id: $deployId"

$statusUrl = "https://api.render.com/v1/deploys/$deployId"
$deadline = (Get-Date).AddSeconds($TimeoutSec)

do {
  Start-Sleep -Seconds 10
  try {
    $s = Invoke-RestMethod -Method Get -Uri $statusUrl -Headers $headers -ErrorAction Stop
    $state = $s.status
  } catch { $state = '' }
  Write-Host ("status=" + $state)
  if ($state -eq 'live') { Write-Host 'Deploy live.'; exit 0 }
  if ($state -in @('failed','canceled')) { Write-Error ("Deploy falhou: " + $state); exit 1 }
} while ((Get-Date) -lt $deadline)

Write-Error 'Timeout aguardando deploy ficar live.'
exit 1
