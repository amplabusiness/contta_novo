# Como usar a aplicação (monorepo contta_novo)

Esta página resume como consumir as APIs em Render e como usar o Portal/Website já publicados.

## Endpoints em Staging (Render)

- Search API
  - Base: https://contta-searchapi-staging.onrender.com
  - Health: GET /health
  - Rotas (todas sob /api e com auth bypass se AUTH_DISABLED=true):
    - GET /api/ncm
    - GET /api/cfop
    - GET /api/ajusteApuracao
    - POST /api/admin/invite (requer role admin quando AUTH_DISABLED=false)
- Excel Parser
  - Base: https://contta-excelparser-staging.onrender.com
  - Health: GET /health
  - Rotas:
    - POST /api/v1/parse-json (gera xlsx)

Observação: Com AUTH_DISABLED=true, as rotas não exigem JWT. Quando decidir ativar Keycloak, ajuste AUTH_DISABLED=false e configure OIDC_ISSUER/OIDC_AUDIENCE via Render.

## Exemplo de uso (curl)

- NCM (Search API):
  curl -s https://contta-searchapi-staging.onrender.com/api/ncm | jq . | head

- Excel (gera planilha):
  curl -s -X POST \
    -H "Content-Type: application/json" \
    -o planilha.xlsx \
    https://contta-excelparser-staging.onrender.com/api/v1/parse-json \
    --data '{"0":{"headers":[{"key":"id","title":"Id"}],"items":[{"id":1},{"id":2}],"name":"Aba1"}}'

## Variáveis de ambiente

- Search API (Render):
  - MONGODB_URI (obrigatório)
  - AUTH_DISABLED (true/false)
  - CORS_ORIGINS (opcional)
  - OIDC_ISSUER / OIDC_AUDIENCE (quando AUTH_DISABLED=false)
- Excel Parser (Render):
  - AUTH_DISABLED (true/false)
  - CORS_ORIGINS (opcional)
  - MONGODB_URI (opcional; habilita health db=connected)

## Frontends

- Portal e Website foram implantados na Vercel. Use as URLs de produção informadas na aba Deployments do GitHub.
- Para desenvolvimento local, veja README-RUN.md e docker-compose.yml.

## Dicas

- CORS: defina CORS_ORIGINS com suas origens finais (ex.: https://app.suaempresa.com).
- Segurança: após validar staging, gire seu RENDER_API_TOKEN e ative JWT se necessário.
- Observabilidade: monitore /health e filas RabbitMQ (ConsumerXml) no broker.
