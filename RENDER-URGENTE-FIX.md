# 🚨 CORREÇÃO URGENTE - Render Service Configuration

## Problema Identificado
Service ID: `srv-d2gsleidbo4c73agmvhg`
Erro: `failed to read dockerfile: open Dockerfile: no such file or directory`

## Causa
O serviço está tentando fazer build na **raiz do repositório**, mas o Dockerfile está em um **subdiretório**.

## 🔧 CORREÇÃO IMEDIATA

### Passo 1: Identifique o Serviço
Pelo Service ID `srv-d2gsleidbo4c73agmvhg`, este parece ser um dos serviços principais.

### Passo 2: Corrija no Painel Render
Acesse: **Settings → Build & Deploy**

**Para cada serviço, configure:**

#### Se for Keycloak:
- **Root Directory**: `.docker/keycloak`
- **Dockerfile Path**: `Dockerfile`

#### Se for Search API:
- **Root Directory**: `contta-search-api-main/contta-search-api-main`
- **Dockerfile Path**: `Dockerfile`

#### Se for Excel Parser:
- **Root Directory**: `contta-excel-parser-main/contta-excel-parser-main`
- **Dockerfile Path**: `Dockerfile`

#### Se for Consumer Worker:
- **Root Directory**: `agendador-back-end-master/agendador-back-end-master/ConsumerXml`
- **Dockerfile Path**: `Dockerfile`

### Passo 3: Manual Deploy
Após ajustar a configuração:
1. Clique em **Manual Deploy**
2. Aguarde o build terminar
3. Verifique se o serviço fica "Live"

## 🎯 SOLUÇÃO DEFINITIVA: Blueprint Deploy

### Recomendação
Use o **Blueprint Deploy** com o arquivo `render.yaml` deste repo:
1. No painel Render → **New → Blueprint**
2. Conecte ao repo `amplabusiness/contta_novo`
3. Selecione o arquivo `render.yaml`
4. Deploy todos os serviços de uma vez com as configurações corretas

## ⚡ AÇÃO IMEDIATA NECESSÁRIA
1. **Corrija o Root Directory** do serviço `srv-d2gsleidbo4c73agmvhg`
2. **Manual Deploy** para testar
3. **Repita para todos os serviços** com erro similar

Isso deve resolver o erro de Dockerfile imediatamente!
