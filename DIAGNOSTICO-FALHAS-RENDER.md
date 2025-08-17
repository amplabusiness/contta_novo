# 🚨 DIAGNÓSTICO DE FALHAS - Blueprint Deploy

## Status Atual: 4 Serviços com Falha de Deploy

### ❌ Serviços Falharam:
- contta-searchapi-staging (falha na implantação)
- contta-excelparser-staging (falha na implantação)  
- contta-keycloak-staging (falha na implantação)
- contta-website-staging (falha na implantação)

## 🔍 Causas Prováveis das Falhas:

### 1. **Problema de Root Directory**
O Blueprint pode não estar configurando os diretórios corretamente para um monorepo.

### 2. **Variáveis de Ambiente Ausentes**
Serviços dependem de variáveis que não foram configuradas.

### 3. **Dockerfile Path Issues**
Caminhos dos Dockerfiles podem estar incorretos.

## 🔧 CORREÇÃO IMEDIATA

### Estratégia: Deploy Individual por Serviço

#### Passo 1: Keycloak (Mais Simples)
1. **New Web Service**
2. **Repository**: `amplabusiness/contta_novo`
3. **Root Directory**: `.docker/keycloak`
4. **Dockerfile Path**: `Dockerfile`
5. **Environment Variables**:
   ```
   KEYCLOAK_ADMIN = admin
   KEYCLOAK_ADMIN_PASSWORD = ConttaKeycloak2025!@#
   ```

#### Passo 2: Search API
1. **New Web Service**
2. **Repository**: `amplabusiness/contta_novo`
3. **Root Directory**: `contta-search-api-main/contta-search-api-main`
4. **Dockerfile Path**: `Dockerfile`
5. **Environment Variables**:
   ```
   NODE_ENV = production
   PORT = 5001
   MONGODB_URI = [sua_mongodb_uri]
   OIDC_ISSUER = https://[keycloak_url]/realms/contta
   OIDC_AUDIENCE = contta-portal
   CORS_ORIGINS = http://localhost:3000,https://[portal_url]
   ```

#### Passo 3: Excel Parser
1. **New Web Service**
2. **Repository**: `amplabusiness/contta_novo`
3. **Root Directory**: `contta-excel-parser-main/contta-excel-parser-main`
4. **Dockerfile Path**: `Dockerfile`
5. **Environment Variables**:
   ```
   NODE_ENV = production
   PORT = 5002
   OIDC_ISSUER = https://[keycloak_url]/realms/contta
   OIDC_AUDIENCE = contta-portal
   PRODUCTION_URL = https://[portal_url]
   ```

#### Passo 4: Consumer Worker
1. **New Worker**
2. **Repository**: `amplabusiness/contta_novo`
3. **Root Directory**: `agendador-back-end-master/agendador-back-end-master/ConsumerXml`
4. **Dockerfile Path**: `Dockerfile`
5. **Environment Variables**:
   ```
   RABBITMQ_URL = [sua_cloudamqp_url]
   RABBITMQ_QUEUE = Modelo55
   RABBITMQ_PREFETCH = 20
   RabbitMQ__Durable = true
   RabbitMQ__Exclusive = false
   RabbitMQ__AutoDelete = false
   RabbitMQ__DeadLetterExchange = dlx.nfe
   RabbitMQ__DeadLetterRoutingKey = Modelo55.dlq
   ```

## 🎯 Plano de Recuperação Rápida

### Ordem de Deploy:
1. **Keycloak** (primeiro - outros dependem dele)
2. **Search API** (depende do Keycloak)
3. **Excel Parser** (depende do Keycloak)
4. **Consumer Worker** (independente)

### Website: Use Vercel
- O `contta-website-staging` deve ir para Vercel (não Render)
- Já temos workflow configurado para isso

## 🔍 Debug dos Logs

### Para cada serviço falhado:
1. **Clique no serviço**
2. **Aba "Logs"**
3. **Identifique o erro específico**
4. **Corrija e redeploy**

## ⚡ AÇÃO IMEDIATA

### Opção 1: Corrigir Blueprint
1. **Delete** os serviços falhados
2. **Ajuste** o `render.yaml`
3. **Retry** Blueprint Deploy

### Opção 2: Deploy Manual (Recomendado)
1. **New Web Service** para cada um
2. **Configuração manual** dos Root Directories
3. **Deploy individual** com controle total

## 📋 Checklist de Correção

- [ ] Verificar logs de cada serviço falhado
- [ ] Confirmar Root Directories corretos
- [ ] Configurar variáveis de ambiente necessárias
- [ ] Deploy individual por serviço
- [ ] Testar conectividade entre serviços
- [ ] Mover Website para Vercel

## 🎯 PRÓXIMO PASSO

**Comece pelo Keycloak** (mais simples e outros dependem dele):
1. Delete o serviço falhado
2. New Web Service
3. Root Directory: `.docker/keycloak`
4. Configure KEYCLOAK_ADMIN_PASSWORD
5. Deploy e verifique sucesso

Depois seguimos com os outros serviços um por vez.
