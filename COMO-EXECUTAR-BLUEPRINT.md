# 🔑 Como Obter RENDER_API_TOKEN e Executar Blueprint Deploy

## 🎯 Passo 1: Obter RENDER_API_TOKEN

### No Dashboard do Render:
1. **Acesse:** https://dashboard.render.com/
2. **Login** com sua conta Render
3. **Clique** no seu avatar (canto superior direito)
4. **Selecione:** "Account Settings"
5. **Menu lateral:** "API Keys"
6. **Clique:** "Create API Key"
7. **Nome:** "Contta Deploy Automation"
8. **Clique:** "Create"
9. **COPIE** o token gerado (começa com `rnd_...`)

### ⚠️ IMPORTANTE:
- O token só é mostrado UMA VEZ
- Guarde em local seguro
- NÃO compartilhe publicamente

## 🚀 Passo 2: Executar Blueprint Deploy

### No PowerShell (Windows):
```powershell
# Navegue para o diretório do projeto
cd "caminho\para\contta_novo"

# Execute o script com seu token
.\scripts\auto-blueprint-deploy.ps1 -RenderApiToken "rnd_seu_token_aqui"
```

### No Terminal (Linux/Mac):
```bash
# Navegue para o diretório do projeto
cd /caminho/para/contta_novo

# Torne o script executável
chmod +x scripts/auto-blueprint-deploy.sh

# Execute com seu token
./scripts/auto-blueprint-deploy.sh "rnd_seu_token_aqui"
```

## 📊 O que vai acontecer automaticamente:

### 1. Blueprint Deploy Iniciado:
```
🚀 EXECUTANDO BLUEPRINT DEPLOY AUTOMATICAMENTE
===============================================
Repo: amplabusiness/contta_novo
Blueprint: render.yaml

📋 Criando Blueprint Deploy...
✅ Blueprint Deploy iniciado!
```

### 2. Service IDs Extraídos:
```
📋 Service IDs extraídos:
RENDER_SERVICE_ID_KEYCLOAK=srv-xxxxxxxxxx
RENDER_SERVICE_ID_SEARCHAPI=srv-yyyyyyyyyy
RENDER_SERVICE_ID_EXCELPARSER=srv-zzzzzzzzzz
RENDER_SERVICE_ID_CONSUMERXML=srv-wwwwwwwwww
```

### 3. Monitoramento em Tempo Real:
```
🔄 Monitorando progresso dos deploys...
[Keycloak] Status: build_in_progress
[Search API] Status: build_in_progress
[Excel Parser] Status: build_in_progress
[Consumer Worker] Status: build_in_progress

[Keycloak] Status: live
✅ [Keycloak] Deploy concluído!
[Search API] Status: live
✅ [Search API] Deploy concluído!
...
```

### 4. URLs Finais:
```
🎯 BLUEPRINT DEPLOY CONCLUÍDO!
URLs dos serviços:
- Keycloak: https://contta-keycloak-staging.onrender.com
- Search API: https://contta-searchapi-staging.onrender.com
- Excel Parser: https://contta-excelparser-staging.onrender.com
```

## 🔧 Passo 3: Configurar GitHub Secrets (Opcional)

### Para automação completa via GitHub Actions:
1. **GitHub:** Settings → Secrets and variables → Actions
2. **New repository secret** para cada um:

```
RENDER_API_TOKEN = rnd_seu_token_aqui
RENDER_SERVICE_ID_KEYCLOAK = srv_id_copiado_do_script
RENDER_SERVICE_ID_SEARCHAPI = srv_id_copiado_do_script
RENDER_SERVICE_ID_EXCELPARSER = srv_id_copiado_do_script
RENDER_SERVICE_ID_CONSUMERXML = srv_id_copiado_do_script
```

## ⚡ Execução Rápida (Copy & Paste)

### Windows PowerShell:
```powershell
# 1. Abra PowerShell como Administrador
# 2. Cole e execute:

$renderToken = Read-Host "Cole seu RENDER_API_TOKEN" -AsSecureString
$tokenPlain = [Runtime.InteropServices.Marshal]::PtrToStringAuto([Runtime.InteropServices.Marshal]::SecureStringToBSTR($renderToken))

# Navegar para o diretório (ajuste o caminho)
cd "C:\caminho\para\contta_novo"

# Executar o script
.\scripts\auto-blueprint-deploy.ps1 -RenderApiToken $tokenPlain
```

### Linux/Mac Terminal:
```bash
# 1. Abra Terminal
# 2. Cole e execute:

echo "Cole seu RENDER_API_TOKEN:"
read -s RENDER_TOKEN

# Navegar para o diretório (ajuste o caminho)
cd /caminho/para/contta_novo

# Executar o script
chmod +x scripts/auto-blueprint-deploy.sh
./scripts/auto-blueprint-deploy.sh "$RENDER_TOKEN"
```

## 🎯 Resultado Final

### Após 15-30 minutos:
- ✅ **4 serviços** deployados no Render
- ✅ **URLs ativas** e funcionais
- ✅ **Service IDs** prontos para GitHub
- ✅ **Sistema operacional** 100%

## 🔄 Troubleshooting

### Se der erro de permissão (Windows):
```powershell
Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser
```

### Se der erro de token inválido:
- Verifique se copiou o token completo
- Confirme se o token não expirou
- Gere um novo token se necessário

### Se builds falharem:
- O script mostrará os logs de erro
- Verifique as variáveis de ambiente
- Acesse o dashboard do Render para mais detalhes

## 📞 Suporte

### Em caso de problemas:
1. Verifique os logs do script
2. Acesse dashboard.render.com
3. Verifique a seção "Activity" dos serviços
4. Logs detalhados em cada serviço

**🚀 PRONTO! Execute o comando e aguarde a automação fazer o resto!**
