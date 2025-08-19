# contta_novo

![CI Backend](https://github.com/amplabusiness/contta_novo/actions/workflows/ci-backend.yml/badge.svg)
![CI .NET](https://github.com/amplabusiness/contta_novo/actions/workflows/ci-dotnet.yml/badge.svg)
![CodeQL](https://github.com/amplabusiness/contta_novo/actions/workflows/codeql.yml/badge.svg)
![CI Frontend](https://github.com/amplabusiness/contta_novo/actions/workflows/ci-frontend.yml/badge.svg)
![Deploy Railway](https://github.com/amplabusiness/contta_novo/actions/workflows/deploy-railway.yml/badge.svg)
![Smoke Tests](https://github.com/amplabusiness/contta_novo/actions/workflows/smoke-post-deploy.yml/badge.svg)

Monorepo com múltiplos apps (frontend e backend) e automações de deploy.

Leia primeiro: docs/USAGE.md (como usar as APIs e frontends).

## Apps principais
- Website (Next.js): `contta-website-main/contta-website-main`
- Portal (Create React App): `portal-contta-master/portal-contta-master`
- Search API (Node/Express): `contta-search-api-main/contta-search-api-main`
- Excel Parser (Node/Express): `contta-excel-parser-main/contta-excel-parser-main`
- Keycloak (OIDC): `.docker/keycloak` (com realm import)

## Variáveis de ambiente
Sempre versionamos exemplos com `.env.example` e não versionamos `.env` reais.

- Frontend (Website Next.js): `NEXT_PUBLIC_*` (não coloque segredos)
  - Arquivo: `contta-website-main/contta-website-main/.env.example`
- Frontend (Portal CRA): `REACT_APP_*` (não coloque segredos)
  - Arquivo: `portal-contta-master/portal-contta-master/.env.example`
- Backend (Search API): `MONGODB_URI`, `OIDC_ISSUER`, `OIDC_AUDIENCE`, etc.
  - Arquivo: `contta-search-api-main/contta-search-api-main/.env.example`
- Backend (Excel Parser): `OIDC_ISSUER`, `OIDC_AUDIENCE`, etc.
  - Arquivo: `contta-excel-parser-main/contta-excel-parser-main/.env.example`

## Deploy
- Vercel (frontends): veja `docs/VERCEL-DEPLOY.md`.
- Render (APIs/Keycloak): veja `docs/DEPLOY-STAGING.md` e `render.yaml`.
 - Railway (APIs): veja `docs/RAILWAY-DEPLOY.md`.

## CI/CD
- Vercel via GitHub Actions: `.github/workflows/vercel-website.yml` e `.github/workflows/vercel-portal.yml`
- Adicione os secrets no GitHub (Actions → Secrets):
  - `VERCEL_TOKEN`, `VERCEL_ORG_ID`, `VERCEL_PROJECT_ID_WEBSITE`, `VERCEL_PROJECT_ID_PORTAL`
 - Railway via GitHub Actions: `.github/workflows/deploy-railway.yml` (necessita secrets RAILWAY_*)
 - Smoke tests pós-deploy: `.github/workflows/smoke-post-deploy.yml` (necessita secrets SMOKE_* URLs)

## Desenvolvimento local
- Docker Compose com Keycloak, MailHog, Mongo, RabbitMQ, APIs e Website: `docker-compose.yml`
- Guia rápido: `README-RUN.md`
