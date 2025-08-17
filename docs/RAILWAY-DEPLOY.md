# Deploy no Railway

Este repositório possui dois serviços Node hospedáveis no Railway:

- Search API: `contta-search-api-main/contta-search-api-main`
- Excel Parser: `contta-excel-parser-main/contta-excel-parser-main`

## CLI & CI
- CLI: `npm i -g @railway/cli`
- CI (GitHub Actions): `.github/workflows/deploy-railway.yml`

### Secrets necessários (GitHub → Actions → Secrets)
- `RAILWAY_TOKEN`
- `RAILWAY_PROJECT_ID_SEARCH`, `RAILWAY_SERVICE_ID_SEARCH`
- `RAILWAY_PROJECT_ID_EXCEL`, `RAILWAY_SERVICE_ID_EXCEL`

## Variáveis de ambiente por serviço (no Railway)
- `NODE_ENV`, `PORT`
- `MONGODB_URI` (Search API)
- `OIDC_ISSUER`, `OIDC_AUDIENCE` (ambos)

## Pipeline sugerido
1. Push em `main` dispara build e deploy via `deploy-railway.yml`.
2. Ao finalizar o deploy, dispare `repository_dispatch` do tipo `post-deploy` para rodar `smoke-post-deploy.yml`.
