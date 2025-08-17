# Deploy no Vercel (Monorepo)

Este repositório contém dois apps para o Vercel:

- Website (Next.js): `contta-website-main/contta-website-main`
- Portal (Create React App): `portal-contta-master/portal-contta-master`

## Passos rápidos

1. Conecte o repositório no Vercel.
2. Crie 2 projetos distintos e selecione as pastas acima como Root Directory.
3. Website (Next.js): preset Next.js; build padrão `next build`; output `.next`.
4. Portal (CRA): preset Create React App; build `yarn build` (ou `npm run build`); output `build`.
5. Defina variáveis de ambiente para o Portal (públicas):
   - `REACT_APP_OIDC_ISSUER`
   - `REACT_APP_OIDC_CLIENT_ID`
   - `REACT_APP_API_SEARCH_URL`
   - `REACT_APP_API_EXCEL_URL`
6. Não coloque segredos no Vercel do frontend. Guarde segredos apenas nos backends (Render, etc.).

## CI opcional via GitHub Actions + Vercel

- Configure os secrets no GitHub:
  - `VERCEL_TOKEN`, `VERCEL_ORG_ID`, `VERCEL_PROJECT_ID_WEBSITE`, `VERCEL_PROJECT_ID_PORTAL`.
- Workflows:
  - `.github/workflows/vercel-website.yml`
  - `.github/workflows/vercel-portal.yml`

Ambos fazem `vercel pull`, `vercel build` e `vercel deploy --prebuilt`.
