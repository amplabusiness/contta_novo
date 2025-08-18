# Smoke Tests

- Frontend
  - GET / (200) e renderiza Home
  - /login → redireciona para Keycloak, após login volta ao site com sessão
- API
  - GET /health → 200 OK
  - Rota protegida sem JWT → 401; com JWT do Keycloak → 200
- Worker
  - Publicar mensagem dummy em `tax.calc` e verificar consumo no log
- DB
  - Inserir documento em `NFE` e consultar por `CodBarra`
- Infra
  - DNS/domínios OK (Vercel/Render); TLS válido
