# 🚨 Railway Deploy Failures - Diagnóstico e Correção

## 📊 Status Atual (Railway)
- ❌ contta-website-staging: Deployment failed  
- ❌ contta-keycloak-staging: Deployment failed
- ❌ contta-excelparser-staging: Deployment failed
- ❌ contta-searchapi-staging: Deployment failed

## 🔍 Possíveis Causas

### 1. Problemas de Build
- **Dockerfile paths incorretos** (similar ao Render)
- **Variáveis de ambiente ausentes**
- **Dependências faltando**

### 2. Problemas de Configuração
- **Root directory** não configurado
- **Build command** incorreto
- **Port binding** errado

### 3. Problemas de Recursos
- **Memory limits** ultrapassados
- **Build timeout**
- **Disk space**

## 🔧 Correções Imediatas

### Para cada serviço no Railway:

#### contta-keycloak-staging
```
Source: agendador-back-end-master/agendador-back-end-master
Root Directory: .docker/keycloak
Build: Dockerfile
Env Variables:
- KEYCLOAK_ADMIN=admin
- KEYCLOAK_ADMIN_PASSWORD=[SECRET]
Port: 8080
```

#### contta-searchapi-staging  
```
Source: agendador-back-end-master/agendador-back-end-master
Root Directory: contta-search-api-main/contta-search-api-main
Build: Dockerfile
Env Variables:
- NODE_ENV=production
- PORT=5001
- MONGODB_URI=[SECRET]
- OIDC_ISSUER=https://[KEYCLOAK_HOST]/realms/contta
- OIDC_AUDIENCE=contta-portal
- CORS_ORIGINS=http://localhost:3000,https://[PORTAL_HOST]
Port: 5001
```

#### contta-excelparser-staging
```
Source: agendador-back-end-master/agendador-back-end-master  
Root Directory: contta-excel-parser-main/contta-excel-parser-main
Build: Dockerfile
Env Variables:
- NODE_ENV=production
- PORT=5002
- PRODUCTION_URL=https://[PORTAL_HOST]
- OIDC_ISSUER=https://[KEYCLOAK_HOST]/realms/contta
- OIDC_AUDIENCE=contta-portal
Port: 5002
```

#### contta-website-staging
```
Source: agendador-back-end-master/agendador-back-end-master
Root Directory: contta-website-main/contta-website-main
Build: Dockerfile ou Node.js
Env Variables:
- NODE_ENV=production
Command: npm run build && npm run start
Port: 3000
```

## 🎯 Plano de Correção

### Passo 1: Verificar Root Directories
Para cada serviço no Railway:
1. **Settings → Source**
2. **Configurar Root Directory** conforme tabela acima
3. **Redeploy**

### Passo 2: Configurar Variáveis
1. **Settings → Variables**
2. **Adicionar** as env vars necessárias
3. **Salvar** e redeploy

### Passo 3: Verificar Dockerfiles
Se ainda houver falhas, verificar se os Dockerfiles existem nos caminhos corretos.

## 📋 Checklist de Verificação

### Por Serviço:
- [ ] **contta-keycloak-staging**
  - [ ] Root Dir: `.docker/keycloak`
  - [ ] Dockerfile presente
  - [ ] KEYCLOAK_ADMIN_PASSWORD definida
  - [ ] Port 8080

- [ ] **contta-searchapi-staging**  
  - [ ] Root Dir: `contta-search-api-main/contta-search-api-main`
  - [ ] Dockerfile presente
  - [ ] MONGODB_URI definida
  - [ ] OIDC vars definidas
  - [ ] Port 5001

- [ ] **contta-excelparser-staging**
  - [ ] Root Dir: `contta-excel-parser-main/contta-excel-parser-main`
  - [ ] Dockerfile presente  
  - [ ] OIDC vars definidas
  - [ ] Port 5002

- [ ] **contta-website-staging**
  - [ ] Root Dir: `contta-website-main/contta-website-main`
  - [ ] Build command correto
  - [ ] Port 3000

## 🚀 Alternativa: Usar Render
Se Railway continuar com problemas, recomendo:
1. **Mover para Render** (já configurado)
2. **Usar Blueprint Deploy** com `render.yaml`
3. **Manter Vercel** para frontend

## ⚡ Próximos Passos
1. **Corrigir Root Directories** no Railway
2. **Configurar variáveis de ambiente**
3. **Redeploy todos os serviços**
4. **Monitorar logs** para novas falhas
5. **Alternativamente**: migrar para Render

A configuração do Render já está pronta e testada - seria uma migração rápida!
