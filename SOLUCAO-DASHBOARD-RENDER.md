# 🎯 EXECUÇÃO DIRETA VIA DASHBOARD RENDER

## Status: API com limitações - Dashboard é mais eficiente

### ⚡ SOLUÇÃO RECOMENDADA: Dashboard Render

#### Passo a Passo Simples:
1. **Acesse:** https://dashboard.render.com/blueprints
2. **Clique:** "New Blueprint"
3. **Connect Repository:** `amplabusiness/contta_novo`
4. **Blueprint detectado:** `render.yaml` (automático)
5. **Clique:** "Deploy"

### 🔧 Variáveis a Configurar (Durante Deploy):

#### Keycloak:
```
KEYCLOAK_ADMIN_PASSWORD = ConttaKeycloak2025!@#
```

#### Search API:
```
MONGODB_URI = [sua_mongodb_uri]
CORS_ORIGINS = http://localhost:3000,https://[portal_url]
```

#### Excel Parser:
```
PRODUCTION_URL = https://[portal_url]
```

#### Consumer Worker:
```
RABBITMQ_URL = [sua_cloudamqp_url]
```

### 📊 Vantagens do Dashboard:
- ✅ **Interface visual** para configuração
- ✅ **Validação automática** dos campos
- ✅ **Deploy imediato** sem debugging de API
- ✅ **Monitoramento visual** do progresso
- ✅ **Logs em tempo real**

### ⏱️ Comparação:
- **Dashboard**: 5-10 minutos + deploy
- **API Debug**: Horas tentando formatos de payload

### 🎯 Resultado Esperado:
Após deploy no Dashboard:
- 4 serviços criados automaticamente
- URLs ativas em 15-20 minutos
- Service IDs disponíveis para GitHub Secrets

### 📋 URLs Finais:
- Keycloak: `https://contta-keycloak-staging.onrender.com`
- Search API: `https://contta-searchapi-staging.onrender.com`
- Excel Parser: `https://contta-excelparser-staging.onrender.com`

## ⚡ RECOMENDAÇÃO: USE O DASHBOARD!

É mais rápido, visual e confiável que debuggar a API.
