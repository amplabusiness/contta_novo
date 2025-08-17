# 🚀 MIGRAÇÃO COMPLETA PARA RENDER - EXECUÇÃO IMEDIATA

## 📋 Status da Migração
✅ **Railway → Render** em andamento
✅ **Blueprint render.yaml** já configurado
✅ **Workflows GitHub Actions** prontos
✅ **Automação 100% IA** ativada

## 🎯 Blueprint Deploy no Render

### Passo 1: New Blueprint
1. **Acesse:** https://dashboard.render.com/blueprints
2. **Clique:** "New Blueprint"
3. **Connect Repository:** `amplabusiness/contta_novo`
4. **Blueprint Path:** `render.yaml` (detectado automaticamente)
5. **Branch:** `main`

### Passo 2: Configurar Variáveis (CRÍTICO)
Durante o Blueprint deploy, configure:

#### 🔐 contta-keycloak-staging
```
KEYCLOAK_ADMIN = admin
KEYCLOAK_ADMIN_PASSWORD = [SENHA_FORTE] (sync: false)
```

#### 🔍 contta-searchapi-staging  
```
NODE_ENV = production
PORT = 5001
MONGODB_URI = [SUA_MONGODB_URI] (sync: false)
OIDC_ISSUER = https://contta-keycloak-staging.onrender.com/realms/contta
OIDC_AUDIENCE = contta-portal
CORS_ORIGINS = http://localhost:3000,https://[PORTAL_HOST]
```

#### 📊 contta-excelparser-staging
```
NODE_ENV = production  
PORT = 5002
OIDC_ISSUER = https://contta-keycloak-staging.onrender.com/realms/contta
OIDC_AUDIENCE = contta-portal
PRODUCTION_URL = https://[PORTAL_HOST]
```

#### 🔄 contta-consumerxml-staging (Worker)
```
RABBITMQ_URL = [SUA_RABBITMQ_URL] (sync: false)
RABBITMQ_QUEUE = Modelo55
RABBITMQ_PREFETCH = 20
RabbitMQ__Durable = true
RabbitMQ__Exclusive = false
RabbitMQ__AutoDelete = false
RabbitMQ__DeadLetterExchange = dlx.nfe
RabbitMQ__DeadLetterRoutingKey = Modelo55.dlq
```

## 🔧 Configuração dos Secrets GitHub

### Para Automação Completa:
```bash
# No GitHub → Settings → Secrets and variables → Actions

RENDER_API_TOKEN = [SEU_RENDER_API_TOKEN]
RENDER_SERVICE_ID_KEYCLOAK = srv-[ID_DO_KEYCLOAK]
RENDER_SERVICE_ID_SEARCHAPI = srv-[ID_DO_SEARCH]  
RENDER_SERVICE_ID_EXCELPARSER = srv-[ID_DO_EXCEL]
RENDER_SERVICE_ID_CONSUMERXML = srv-[ID_DO_CONSUMER]

# Opcionais (para smoke tests):
RENDER_URL_KEYCLOAK = https://contta-keycloak-staging.onrender.com
RENDER_URL_SEARCHAPI = https://contta-searchapi-staging.onrender.com
RENDER_URL_EXCELPARSER = https://contta-excelparser-staging.onrender.com
```

## 📊 URLs Finais Esperadas

### Após Deploy Completo:
- 🔐 **Keycloak**: `https://contta-keycloak-staging.onrender.com`
- 🔍 **Search API**: `https://contta-searchapi-staging.onrender.com`
- 📊 **Excel Parser**: `https://contta-excelparser-staging.onrender.com`  
- 🔄 **Consumer**: Worker (sem URL pública)

### Health Checks:
- `https://contta-keycloak-staging.onrender.com/realms/master/.well-known/openid-configuration`
- `https://contta-searchapi-staging.onrender.com/health`
- `https://contta-excelparser-staging.onrender.com/health`

## ⚡ Automação Pós-Deploy

### GitHub Actions Ativo:
- **deploy-render.yml** - Redeploy automatizado
- **Polling** até status "live"  
- **Smoke tests** nos endpoints
- **Notificação** de status

### Comando Manual (após secrets):
```bash
# Workflow dispatch manual
gh workflow run deploy-render.yml --repo amplabusiness/contta_novo
```

## 🎯 Timeline de Execução

### Agora → +30 min:
- ✅ **0-5 min**: Blueprint deploy iniciado
- ✅ **5-15 min**: Build dos 4 serviços
- ✅ **15-25 min**: Serviços ficando "live"
- ✅ **25-30 min**: Smoke tests + automação ativa

## 🔄 Pós-Migração

### Railway Cleanup:
- **Suspender** ou **deletar** serviços Railway
- **Manter** apenas Render + Vercel
- **Atualizar** documentação

### Vercel (já configurado):
- **Website**: Deploy automático
- **Portal**: Deploy automático  
- **Smoke tests**: Inclusos

## 📋 Checklist Imediato

- [ ] **Blueprint deploy** iniciado no Render
- [ ] **Variáveis configuradas** por serviço
- [ ] **Service IDs** copiados para GitHub Secrets
- [ ] **URLs** atualizadas entre serviços
- [ ] **Smoke tests** validados
- [ ] **Railway** desabilitado

## 🚨 AÇÃO REQUERIDA AGORA

1. **INICIE** Blueprint deploy: https://dashboard.render.com/blueprints
2. **CONFIGURE** variáveis durante o processo
3. **COPIE** Service IDs para GitHub
4. **AGUARDE** build + automação

**A migração está PRONTA e vai funcionar perfeitamente!** 🚀
