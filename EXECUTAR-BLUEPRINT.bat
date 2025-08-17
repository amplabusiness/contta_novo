@echo off
echo 🔑 RENDER API TOKEN SETUP E EXECUÇÃO AUTOMÁTICA
echo ================================================
echo.

echo 📋 Passo 1: Obter RENDER_API_TOKEN
echo 1. Acesse: https://dashboard.render.com/
echo 2. Login na sua conta
echo 3. Avatar (canto superior direito) → Account Settings
echo 4. Menu lateral → API Keys
echo 5. Create API Key → Nome: "Contta Deploy" → Create
echo 6. COPIE o token (rnd_...)
echo.

set /p token="Cole seu RENDER_API_TOKEN aqui: "rnd_QYyuv9vDdFpAaG7tbBVpWsKRec0K

if "%token%"=="" (
    echo ❌ Token não fornecido!
    pause
    exit /b 1
)

echo.
echo ⚡ Executando Blueprint Deploy...
echo.

powershell -ExecutionPolicy Bypass -File "scripts\auto-blueprint-deploy.ps1" -RenderApiToken "%token%"

echo.
echo 🎯 Execução concluída!
echo Verifique os logs acima para Service IDs e URLs.
echo.
pause
