#requires -Version 5.1
<#
.SYNOPSIS
	Configura os GitHub Actions Secrets necessários (repo ou ambiente "production").

.PREREQUISITES
	- GitHub CLI (gh): https://cli.github.com/
	- Permissão para definir segredos no repositório

.USAGE
	powershell -ExecutionPolicy Bypass -File scripts/setup-github-secrets.ps1
#>

function Ensure-GhCli {
	if (-not (Get-Command gh -ErrorAction SilentlyContinue)) {
		Write-Error "GitHub CLI (gh) não encontrado. Instale em https://cli.github.com/ e tente novamente."
		exit 1
	}
	try {
		gh auth status | Out-Null
	} catch {
		Write-Host "Autenticando no GitHub CLI..." -ForegroundColor Yellow
		gh auth login
	}
}

function Read-Secret([string]$name, [string]$default = "") {
	$val = Read-Host -AsSecureString "Digite o valor para $name (deixe vazio para pular)"
	if (-not $val) { return $null }
	$bstr = [Runtime.InteropServices.Marshal]::SecureStringToBSTR($val)
	try { [Runtime.InteropServices.Marshal]::PtrToStringAuto($bstr) } finally { [Runtime.InteropServices.Marshal]::ZeroFreeBSTR($bstr) }
}

function Set-Secret([string]$name, [string]$value, [string]$envName) {
	if ([string]::IsNullOrWhiteSpace($value)) { return }
	if ([string]::IsNullOrWhiteSpace($envName)) {
		gh secret set $name --body "$value" | Out-Null
	} else {
		gh secret set $name --env $envName --body "$value" | Out-Null
	}
	Write-Host "✓ Secret definido: $name" -ForegroundColor Green
}

Write-Host "Configuração de GitHub Secrets" -ForegroundColor Cyan
Write-Host "================================" -ForegroundColor Cyan

Ensure-GhCli

$repo = (gh repo view --json nameWithOwner -q .nameWithOwner 2>$null)
if (-not $repo) { $repo = Read-Host "Informe owner/repo (ex.: amplabusiness/contta_novo)" }
gh repo view $repo 1>$null 2>$null || (Write-Error "Repositório inválido: $repo"; exit 1)

Write-Host "Alvo: $repo" -ForegroundColor Gray
gh repo set-default $repo | Out-Null

$scopeChoice = Read-Host "Salvar secrets em (1) Repository ou (2) Environment 'production'? [1/2]"
$envName = if ($scopeChoice -eq '2') { 'production' } else { '' }
if ($envName) {
	Write-Host "Usando Environment: $envName" -ForegroundColor Gray
	# cria env se não existir
	gh api --method PUT repos/$repo/environments/$envName 1>$null 2>$null
}

Write-Host "\nVercel (portal)" -ForegroundColor Yellow
$VERCEL_TOKEN      = Read-Secret 'VERCEL_TOKEN'
$VERCEL_ORG_ID     = Read-Secret 'VERCEL_ORG_ID'
$VERCEL_PROJECT_ID = Read-Secret 'VERCEL_PROJECT_ID'

Write-Host "\nRender (serviços)" -ForegroundColor Yellow
$RENDER_API_TOKEN          = Read-Secret 'RENDER_API_TOKEN'
$KEYCLOAK_ADMIN_PASSWORD   = Read-Secret 'KEYCLOAK_ADMIN_PASSWORD'
$MONGODB_URI               = Read-Secret 'MONGODB_URI'
$RABBITMQ_URL              = Read-Secret 'RABBITMQ_URL'
$OIDC_ISSUER               = Read-Secret 'OIDC_ISSUER (opcional)'
$CORS_ORIGINS              = Read-Secret 'CORS_ORIGINS (opcional)'
$PRODUCTION_URL            = Read-Secret 'PRODUCTION_URL (opcional)'

Write-Host "\nRender (IDs/URLs opcionais para smoke/redeploy)" -ForegroundColor Yellow
$RENDER_SERVICE_ID_KEYCLOAK   = Read-Secret 'RENDER_SERVICE_ID_KEYCLOAK (opcional)'
$RENDER_SERVICE_ID_SEARCHAPI  = Read-Secret 'RENDER_SERVICE_ID_SEARCHAPI (opcional)'
$RENDER_SERVICE_ID_EXCELPARSER= Read-Secret 'RENDER_SERVICE_ID_EXCELPARSER (opcional)'
$RENDER_SERVICE_ID_CONSUMERXML= Read-Secret 'RENDER_SERVICE_ID_CONSUMERXML (opcional)'
$RENDER_URL_KEYCLOAK          = Read-Secret 'RENDER_URL_KEYCLOAK (opcional)'
$RENDER_URL_SEARCHAPI         = Read-Secret 'RENDER_URL_SEARCHAPI (opcional)'
$RENDER_URL_EXCELPARSER       = Read-Secret 'RENDER_URL_EXCELPARSER (opcional)'

Write-Host "\nGravando secrets..." -ForegroundColor Cyan

# Vercel
Set-Secret 'VERCEL_TOKEN'      $VERCEL_TOKEN      $envName
Set-Secret 'VERCEL_ORG_ID'     $VERCEL_ORG_ID     $envName
Set-Secret 'VERCEL_PROJECT_ID' $VERCEL_PROJECT_ID $envName

# Render core
Set-Secret 'RENDER_API_TOKEN'          $RENDER_API_TOKEN        $envName
Set-Secret 'KEYCLOAK_ADMIN_PASSWORD'   $KEYCLOAK_ADMIN_PASSWORD $envName
Set-Secret 'MONGODB_URI'               $MONGODB_URI             $envName
Set-Secret 'RABBITMQ_URL'              $RABBITMQ_URL            $envName

# Opcionais de configuração
Set-Secret 'OIDC_ISSUER'     $OIDC_ISSUER     $envName
Set-Secret 'CORS_ORIGINS'    $CORS_ORIGINS    $envName
Set-Secret 'PRODUCTION_URL'  $PRODUCTION_URL  $envName

# Opcionais de suporte (IDs/URLs)
Set-Secret 'RENDER_SERVICE_ID_KEYCLOAK'    $RENDER_SERVICE_ID_KEYCLOAK     $envName
Set-Secret 'RENDER_SERVICE_ID_SEARCHAPI'   $RENDER_SERVICE_ID_SEARCHAPI    $envName
Set-Secret 'RENDER_SERVICE_ID_EXCELPARSER' $RENDER_SERVICE_ID_EXCELPARSER  $envName
Set-Secret 'RENDER_SERVICE_ID_CONSUMERXML' $RENDER_SERVICE_ID_CONSUMERXML  $envName
Set-Secret 'RENDER_URL_KEYCLOAK'           $RENDER_URL_KEYCLOAK            $envName
Set-Secret 'RENDER_URL_SEARCHAPI'          $RENDER_URL_SEARCHAPI           $envName
Set-Secret 'RENDER_URL_EXCELPARSER'        $RENDER_URL_EXCELPARSER         $envName

Write-Host "\nConcluído. Secrets configurados." -ForegroundColor Green
