# Produção rápida com Vercel (front) + Render (APIs)

Use os domínios da Vercel que você já tem e as APIs em Render (staging) por enquanto.

## 1) CORS nas APIs (Render)

- Defina CORS_ORIGINS nos dois serviços com os domínios Vercel:
  - conttanovo-git-main-sergio-carneiro-leaos-projects.vercel.app
  - conttanovo-6yias0ak0-sergio-carneiro-leaos-projects.vercel.app
- Exemplo de valor (separe por vírgula):
  https://conttanovo-git-main-sergio-carneiro-leaos-projects.vercel.app,https://conttanovo-6yias0ak0-sergio-carneiro-leaos-projects.vercel.app

## 2) Portal (Vercel)

- Configure as variáveis de ambiente (Production) no projeto do Portal:
  - REACT_APP_API_BASE_URL=https://contta-searchapi-staging.onrender.com/api
  - (opcional) REACT_APP_API_EXCEL_URL=https://contta-excelparser-staging.onrender.com
- Reimplante o Portal após salvar as variáveis.

## 3) Teste ponta-a-ponta

- Abra o domínio da Vercel e execute uma ação que consome a API.
- Acompanhe /health das APIs e o console do navegador (CORS).

## 4) Quando migrar para produção de verdade

- Ative AUTH_DISABLED=false e configure OIDC_ISSUER/OIDC_AUDIENCE nas APIs.
- Aponte domínios finais e ajuste CORS para eles.
- Atualize REACT_APP_API_BASE_URL no Portal para as novas URLs.
