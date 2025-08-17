# Deploy checklist (Staging/Prod)

Use esta lista para garantir que todos os componentes estejam prontos e com variáveis/segredos corretos.

## 1) Repositório e CI/CD
- [ ] Branch principal conectada às plataformas (Vercel, Render ou Railway).
- [ ] Workflows GitHub Actions habilitados:
  - [ ] `.github/workflows/deploy-railway.yml`
  - [ ] `.github/workflows/vercel-website.yml`
  - [ ] `.github/workflows/vercel-portal.yml`
  - [ ] `.github/workflows/smoke-post-deploy.yml`
- [ ] Secrets no GitHub:
  - [ ] `VERCEL_TOKEN`, `VERCEL_ORG_ID`, `VERCEL_PROJECT_ID_WEBSITE`, `VERCEL_PROJECT_ID_PORTAL`
  - [ ] `RAILWAY_TOKEN`, `RAILWAY_PROJECT_ID_*`, `RAILWAY_SERVICE_ID_*`
  - [ ] `SMOKE_SEARCH_API_URL`, `SMOKE_EXCEL_API_URL`, `SMOKE_WEBSITE_URL`, `SMOKE_PORTAL_URL`

## 2) Provedores
- [ ] MongoDB Atlas: cluster, usuário, IP allowlist (Render/Railway), string SRV copiada.
- [ ] CloudAMQP: instância criada (amqps URL), vhost, usuário com permissões mínimas.
- [ ] Vercel: 2 projetos (Website/Portal), diretórios root corretos.
- [ ] Render ou Railway: serviços criados (Search API / Excel Parser) via Docker.
- [ ] Keycloak (Render/Fly): realm `contta` importado, client `contta-portal` com PKCE e redirect URIs.

## 3) Variáveis de ambiente
- Backends (Railway/Render):
  - [ ] `NODE_ENV=production`
  - [ ] `PORT` (Search: 5001; Excel: 5002)
  - [ ] `MONGODB_URI` (Search API)
  - [ ] `OIDC_ISSUER=https://<KEYCLOAK_HOST>/realms/contta`
  - [ ] `OIDC_AUDIENCE=contta-portal`
- Consumer .NET:
  - [ ] `RABBITMQ_HOST` e `RABBITMQ_PORT` OU `RABBITMQ_URL` (amqps) no container
  - [ ] `RABBITMQ_QUEUE=Modelo55`
  - [ ] `RABBITMQ_PREFETCH=20`
  - [ ] `RabbitMQ__Durable=true`, `RabbitMQ__Exclusive=false`, `RabbitMQ__AutoDelete=false`
  - [ ] `RabbitMQ__DeadLetterExchange=dlx.nfe`, `RabbitMQ__DeadLetterRoutingKey=Modelo55.dlq`
- Portal (Vercel):
  - [ ] `REACT_APP_OIDC_ISSUER`, `REACT_APP_OIDC_CLIENT_ID`
  - [ ] `REACT_APP_API_SEARCH_URL`, `REACT_APP_API_EXCEL_URL`

## 4) Testes e validações
- [ ] Health endpoints: `/health` funcionando em Search/Excel.
- [ ] JWT de Keycloak aceito nas APIs (audience/issuer corretos).
- [ ] Consumer consumindo fila `Modelo55` e publicando logs.
- [ ] Portal faz login via OIDC e acessa APIs.

## 5) Observabilidade e segurança
- [ ] Logs e métricas ativados (Sentry/Datadog opcional).
- [ ] Backups: snapshots do Atlas.
- [ ] Rotação de segredos planejada.
- [ ] CORS restrito em produção.

