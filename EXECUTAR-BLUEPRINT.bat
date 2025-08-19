@echo off
setlocal EnableExtensions EnableDelayedExpansion
chcp 65001 >nul

REM Garante que caminhos relativos funcionem ao clicar duas vezes
pushd "%~dp0"

echo 🔑 Deploy 1‑Clique (Render Blueprint)
echo =====================================
echo.
echo Como obter o token:
echo  - https://dashboard.render.com/ → Account Settings → API Keys → Create API Key
echo.

REM Permite usar variável de ambiente já definida
if not "!RENDER_API_TOKEN!"=="" (
    set "token=!RENDER_API_TOKEN!"
) else (
    set /p token="Cole seu RENDER_API_TOKEN (rnd_xK09wNZxoorEcseTDW3UtissBCby): "
)

if "%token%"=="" (
        echo ❌ Token não fornecido!
        goto :end
)

echo.
echo ⚡ Iniciando deploy do Blueprint no Render...
echo.

powershell -NoProfile -ExecutionPolicy Bypass -File "scripts\auto-blueprint-deploy.ps1" -RenderApiToken "%token%"

echo.
echo 🎯 Processo finalizado. Consulte as URLs e status nos logs acima.
echo.

:end
popd
pause
