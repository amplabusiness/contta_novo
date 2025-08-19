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

rem O script PowerShell agora apenas prepara o payload JSON em arquivo temporário
set "payload_file=%TEMP%\render-blueprint-payload.json"
del /f /q "%payload_file%" >nul 2>&1

powershell -NoProfile -ExecutionPolicy Bypass -File "scripts\auto-blueprint-deploy.ps1" -MongoUri "!mongo_uri!" > "%payload_file%"

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

:end
popd
pause
