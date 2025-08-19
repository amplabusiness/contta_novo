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

rem Solicitar RABBITMQ_URL (CloudAMQP) se não vier do ambiente
:GetRabbitUrl
if not "!RABBITMQ_URL!"=="" (
    set "rabbit_url=!RABBITMQ_URL!"
    echo Detectado RABBITMQ_URL no ambiente. Usando esse valor (nao sera solicitado).
) else (
    echo.
    echo Exemplo CloudAMQP: amqps://usuario:senha@host.rmq.cloudamqp.com/vhost
    set /p rabbit_url="Cole seu RABBITMQ_URL (CloudAMQP): "
)
if "!rabbit_url!"=="" (
    echo ❌ RABBITMQ_URL nao fornecida.
    goto GetRabbitUrl
)

echo.
echo ⚡ Preparando e iniciando o deploy no Render (sem Keycloak)...
echo.

rem Preparar arquivos temporários
set "payload_file=%TEMP%\render-blueprint-payload.json"
set "response_file=%TEMP%\render-blueprint-response.json"
del /f /q "%payload_file%" >nul 2>&1
del /f /q "%response_file%" >nul 2>&1

rem Gerar o payload usando o script PowerShell do repositório (versão simplificada)
powershell -NoProfile -ExecutionPolicy Bypass -File "scripts\auto-blueprint-deploy.ps1" -MongoUri "!mongo_uri!" -CorsOrigins "!CORS_ORIGINS!" -ProductionUrl "!PRODUCTION_URL!" -RabbitUrl "!rabbit_url!" > "%payload_file%"

if not exist "%payload_file%" (
    echo ❌ Falha ao gerar o payload JSON. Verifique o script PowerShell.
    goto end
)

for %%A in ("%payload_file%") do if %%~zA lss 10 (
    echo ❌ Payload JSON ficou vazio. Verifique o script PowerShell.
    goto end
)

@echo off
setlocal EnableExtensions EnableDelayedExpansion
pushd "%~dp0"

echo Deploy Render Blueprint
echo =======================
echo.

:GetToken
if not "%RENDER_API_TOKEN%"=="" (
    set "token=%RENDER_API_TOKEN%"
) else (
    set /p token="Cole seu RENDER_API_TOKEN: "
)
if "%token%"=="" (
    echo Token ausente.
    goto GetToken
)

set "payload_file=%TEMP%\render-blueprint-payload.json"
set "response_file=%TEMP%\render-blueprint-response.json"
del /f /q "%payload_file%" >nul 2>&1
del /f /q "%response_file%" >nul 2>&1

rem Gerar payload (Mongo e Rabbit serão obtidos do Render: env group e template)
powershell -NoProfile -ExecutionPolicy Bypass -File "scripts\auto-blueprint-deploy.ps1" -CorsOrigins "%CORS_ORIGINS%" -ProductionUrl "%PRODUCTION_URL%" > "%payload_file%"

if not exist "%payload_file%" (
    echo Falha ao gerar payload JSON.
    goto end
)
for %%A in ("%payload_file%") do if %%~zA lss 10 (
    echo Payload JSON vazio.
    goto end
)

echo Enviando blueprint para Render...
for /f "tokens=2 delims==" %%S in ('curl.exe -sS -X POST ^
    -H "Authorization: Bearer %token%" ^
    -H "Content-Type: application/json" ^
    -H "Accept: application/json" ^
    --data-binary @"%payload_file%" ^
    "https://api.render.com/v1/blueprints" ^
    -o "%response_file%" ^
    -w "HTTP_STATUS=%%{http_code}"') do set "http_status=%%S"

echo Status HTTP: %http_status%
type "%response_file%"

:cleanup
del /f /q "%payload_file%" >nul 2>&1
del /f /q "%response_file%" >nul 2>&1

:end
popd
pause
pause
