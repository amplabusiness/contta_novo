@echo off
setlocal EnableExtensions EnableDelayedExpansion
chcp 65001 > nul

pushd "%~dp0"

echo 🔑 Deploy 1-Clique (Render Blueprint)
echo =====================================
echo.

:GetToken
if not "!RENDER_API_TOKEN!"=="" (
    set "token=!RENDER_API_TOKEN!"
) else (
    set /p token="Cole seu RENDER_API_TOKEN: "
)
if "!token!"=="" (
    echo ❌ Token não fornecido.
    goto GetToken
)

:GetMongoUri
if not "!MONGODB_URI!"=="" (
    set "mongo_uri=!MONGODB_URI!"
    echo Detectado MONGODB_URI no ambiente. Usando esse valor (nao sera solicitado).
) else (
    echo.
    echo Exemplo: mongodb+srv://sergio:ZvP7aCCNPndstbZU@cluster0.uczjtjv.mongodb.net/
    set /p mongo_uri="mongodb+srv://sergio:ZvP7aCCNPndstbZU@cluster0.uczjtjv.mongodb.net/"
)
if "!mongo_uri!"=="" (
    echo ❌ MONGODB_URI não fornecida.
    goto GetMongoUri
)

echo.
echo ⚡ Preparando e iniciando o deploy no Render (sem Keycloak)...
echo.

rem Preparar arquivos temporários
set "payload_file=%TEMP%\render-blueprint-payload.json"
set "response_file=%TEMP%\render-blueprint-response.json"
set "temp_ps1=%TEMP%\gen-payload-%RANDOM%.ps1"
del /f /q "%payload_file%" >nul 2>&1
del /f /q "%response_file%" >nul 2>&1
del /f /q "%temp_ps1%" >nul 2>&1

rem Variáveis opcionais vindas do ambiente
set "cors_env=!CORS_ORIGINS!"
set "prod_env=!PRODUCTION_URL!"
set "rabbit_env=!RABBITMQ_URL!"

rem Gerar script PowerShell temporário para criar o payload
(
    echo param(^[string^]$MongoUri,^[string^]$CorsOrigins,^[string^]$ProductionUrl,^[string^]$RabbitUrl)
    echo $ErrorActionPreference = 'Stop'
    echo $templatePath = Join-Path (Get-Location) 'scripts/blueprint-payload.template.json'
    echo if(-not (Test-Path $templatePath)) { Write-Error "Template nao encontrado: $templatePath"; exit 1 }
    echo $payloadJson = Get-Content -Path $templatePath -Raw -Encoding UTF8
    echo function Normalize-MongoUri { param(^[string^]$uri)
    echo   if([string]::IsNullOrWhiteSpace($uri)) { return $uri }
    echo   $hasPath = $false
    echo   if($uri -match '^[^?]*/([^/?]+)\?') { $hasPath = $true }
    echo   elseif($uri -match '^[^?]*/([^/?]+)$') { $hasPath = ($Matches[1] -ne '') }
    echo   elseif($uri -match '^[^?]*/$') { $hasPath = $false }
    echo   else { if($uri -notmatch '/[^/]+$' -and $uri -notmatch '/[^/?]+\?') { $hasPath = $false } }
    echo   if(-not $hasPath) {
    echo     if($uri -match '\?$') { return ($uri -replace '\?$', '/contta?retryWrites=true^&w=majority') }
    echo     elseif($uri -match '/\?$') { return ($uri -replace '/\?$', '/contta?retryWrites=true^&w=majority') }
    echo     elseif($uri -match '/$') { return ($uri + 'contta?retryWrites=true^&w=majority') }
    echo     elseif($uri -match '\?') { return ($uri -replace '\?', '/contta?') }
    echo     else { return ($uri + '/contta?retryWrites=true^&w=majority') }
    echo   }
    echo   return $uri
    echo }
    echo function Esc(^[string^]$s) { if($null -eq $s) { return '' } ^&^& ($s ^| ConvertTo-Json -Compress) ^|
    echo   ForEach-Object { if($_.StartsWith('"') -and $_.EndsWith('"')) { $_.Substring(1, $_.Length-2) } else { $_ } } }
    echo $mongoNorm = Normalize-MongoUri $MongoUri
    echo if([string]::IsNullOrWhiteSpace($CorsOrigins)) { $CorsOrigins = '*.vercel.app,https://localhost:3000' }
    echo if([string]::IsNullOrWhiteSpace($ProductionUrl)) { $ProductionUrl = '' }
    echo if([string]::IsNullOrWhiteSpace($RabbitUrl)) { $RabbitUrl = '' }
    echo $payloadJson = $payloadJson.Replace('__MONGODB_URI__', (Esc $mongoNorm)).Replace('__CORS_ORIGINS__', (Esc $CorsOrigins)).Replace('__PRODUCTION_URL__', (Esc $ProductionUrl)).Replace('__RABBITMQ_URL__', (Esc $RabbitUrl))
    echo [Console]::OutputEncoding = [System.Text.Encoding]::UTF8
    echo Write-Output $payloadJson
) > "%temp_ps1%"

rem Executar o script temporário para gerar o payload
powershell -NoProfile -ExecutionPolicy Bypass -File "%temp_ps1%" -MongoUri "!mongo_uri!" -CorsOrigins "!cors_env!" -ProductionUrl "!prod_env!" -RabbitUrl "!rabbit_env!" > "%payload_file%"

if not exist "%payload_file%" (
    echo ❌ Falha ao gerar o payload JSON. Verifique o script PowerShell.
    goto end
)

for %%A in ("%payload_file%") do if %%~zA lss 10 (
    echo ❌ Payload JSON ficou vazio. Verifique o script PowerShell.
    goto end
)

echo.
echo Enviando blueprint para Render...
echo.
for /f "tokens=2 delims==" %%S in ('curl.exe -sS -X POST ^
    -H "Authorization: Bearer !token!" ^
    -H "Content-Type: application/json" ^
    -H "Accept: application/json" ^
    --data-binary @"%payload_file%" ^
    "https://api.render.com/v1/blueprints" ^
    -o "%response_file%" ^
    -w "HTTP_STATUS=%%{http_code}"') do set "http_status=%%S"

echo Status HTTP: %http_status%
echo Resposta:
type "%response_file%"

if not "%http_status%"=="200" if not "%http_status%"=="201" (
        echo.
        echo ❌ Falha ao acionar o deploy via Render API (HTTP %http_status%).
        goto cleanup
)

echo.
echo ✅ Requisicao enviada com sucesso.

rem Usa curl.exe para fazer a chamada da API com arquivo (@), evitando truncamento e problemas de escape
curl.exe -sS --fail-with-body -X POST ^
  -H "Authorization: Bearer !token!" ^
  -H "Content-Type: application/json" ^
  -H "Accept: application/json" ^
  --data-binary @"%payload_file%" ^
  "https://api.render.com/v1/blueprints"

set "curl_errorlevel=%ERRORLEVEL%"

echo.
if not "%curl_errorlevel%"=="0" (
    echo ❌ Falha ao acionar o deploy via Render API. Codigo de erro: %curl_errorlevel%
    echo Veja a saida acima para detalhes.
    goto cleanup
)

echo ✅ Requisicao enviada com sucesso.

echo.
echo.
echo 🎯 Deploy iniciado. O status pode ser acompanhado no dashboard do Render:
echo https://dashboard.render.com
echo.

:cleanup
del /f /q "%payload_file%" >nul 2>&1
del /f /q "%response_file%" >nul 2>&1
del /f /q "%temp_ps1%" >nul 2>&1

:end
popd
pause
