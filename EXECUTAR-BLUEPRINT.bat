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
    echo Exemplo: mongodb+srv://user:pass@cluster.mongodb.net/contta
    set /p mongo_uri="Cole sua MONGODB_URI: "
)
if "!mongo_uri!"=="" (
    echo ❌ MONGODB_URI não fornecida.
    goto GetMongoUri
)

:GetKeycloakPassword
if not "!KEYCLOAK_ADMIN_PASSWORD!"=="" (
    set "keycloak_password=!KEYCLOAK_ADMIN_PASSWORD!"
) else (
    echo.
    set /p keycloak_password="Defina a KEYCLOAK_ADMIN_PASSWORD: "
)
if "!keycloak_password!"=="" (
    echo ❌ Senha do Keycloak não fornecida.
    goto GetKeycloakPassword
)

echo.
echo ⚡ Preparando e iniciando o deploy no Render...
echo.

rem O script PowerShell agora apenas prepara o payload JSON
set "ps_command=powershell -NoProfile -ExecutionPolicy Bypass -File ""scripts\auto-blueprint-deploy.ps1"" -KeycloakAdminPassword ""!keycloak_password!"" -MongoUri ""!mongo_uri!"""

rem Executa o PowerShell e captura a saída (o payload JSON)
for /f "delims=" %%i in ('!ps_command!') do (
    set "payload=%%i"
)

if not defined payload (
    echo ❌ Falha ao gerar o payload JSON. Verifique o script PowerShell.
    goto end
)

rem Usa curl para fazer a chamada da API - mais robusto que Invoke-RestMethod
curl -X POST ^
  -H "Authorization: Bearer !token!" ^
  -H "Content-Type: application/json" ^
  -H "Accept: application/json" ^
  -d "!payload!" ^
  "https://api.render.com/v1/blueprints"

echo.
echo.
echo 🎯 Deploy iniciado. O status pode ser acompanhado no dashboard do Render:
echo https://dashboard.render.com
echo.

:end
popd
pause
