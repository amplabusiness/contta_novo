param(
  [Parameter(Mandatory=$false)] [string[]] $Urls = @()
)

if (-not $Urls -or $Urls.Count -eq 0) {
  $Urls = @(
    $env:RENDER_URL_KEYCLOAK,
    "$env:RENDER_URL_SEARCHAPI/health",
    "$env:RENDER_URL_EXCELPARSER/health"
  ) | Where-Object { $_ -and $_.Trim() -ne '' }
}

if (-not $Urls -or $Urls.Count -eq 0) {
  Write-Host "Nenhuma URL fornecida. Forneça via parâmetro -Urls ou variáveis de ambiente."
  exit 0
}

foreach ($u in $Urls) {
  try {
    $resp = Invoke-WebRequest -Uri $u -UseBasicParsing -Method Head -TimeoutSec 15
    $code = [int]$resp.StatusCode
  } catch { $code = $_.Exception.Response.StatusCode.value__ }
  if (-not $code) { $code = 0 }
  Write-Host ($code.ToString().PadLeft(3) + ' ' + $u)
}
