# Deploy Staging – Vercel / Render / MongoDB Atlas / CloudAMQP

Este guia descreve como publicar o ambiente de staging com variáveis seguras.

## 1) Provisionamento

- Vercel (Portal/Website)
- Render (APIs Node e Keycloak) – alternativa: Railway/Fly
- MongoDB Atlas (cluster M0/M10) – DB contta
- CloudAMQP (RabbitMQ gerenciado) – plano starter
- S3 compatível (Backblaze B2/AWS) – opcional

## 2) Secrets e variáveis por ambiente

- Atlas
  - MONGODB_URI=mongodb+srv://<user>:<pass>@<cluster>/<db>?retryWrites=true&w=majority
- Keycloak
  - KEYCLOAK_ADMIN=admin
  - KEYCLOAK_ADMIN_PASSWORD=… (Render secret)
- APIs
  - OIDC_ISSUER=https://<KEYCLOAK_HOST>/realms/contta
  - OIDC_AUDIENCE=contta-portal
  - OPENAI_API_KEY=… (para Orchestrator IA)
- Portal (Vercel)
  - REACT_APP_AUTH_MODE=oidc
  - REACT_APP_OIDC_AUTHORITY=https://<KEYCLOAK_HOST>/realms/contta
  - REACT_APP_OIDC_CLIENT_ID=contta-portal
  - REACT_APP_SEARCH_API_BASE_URL=https://<SEARCH_HOST>
  - REACT_APP_EXCEL_API_BASE_URL=https://<EXCEL_HOST>

## 3) Render – blueprint

- Arquivo `render.yaml` no repositório contém 4 serviços:
  - contta-keycloak-staging (Dockerfile em `.docker/keycloak`)
  - contta-searchapi-staging (Dockerfile em `contta-search-api-main/...`)
  - contta-excelparser-staging (Dockerfile em `contta-excel-parser-main/...`)
  - contta-website-staging (Node build)
- Crie o Blueprint no Render e conecte ao GitHub. Defina secrets ausentes (sync: false) antes do deploy.

## 4) Vercel – sites

- Conecte o repositório. Configure ambiente “Preview/Production”.
- Defina as variáveis REACT_APP_* conforme seção 2.
- Ative proteção de preview se necessário.

## 5) MongoDB Atlas

- Crie o cluster, database `contta` e um usuário com acesso a rede do Render/Vercel.
- Copie a string de conexão (SRV). Atualize a env `MONGODB_URI` nos serviços.

## 6) CloudAMQP

- Crie a instância, gere a URL amqps. Use para Consumer .NET e quaisquer produtores.

## 7) Keycloak (staging)

- O serviço no Render usa a imagem com `realm-export.json` para importar o realm.
- Após subir, valide o endpoint: `https://<KEYCLOAK_HOST>/realms/contta/.well-known/openid-configuration`.
- Crie/ajuste o client `contta-portal` (PKCE, redirect do Vercel) e roles.

## 8) Teste de ponta a ponta

1) Obtenha um token no Keycloak (fluxo do Portal) e chame `/health` e rotas protegidas das APIs.
2) Teste o endpoint de convite `/api/admin/invite` com um usuário com role `admin` e confirme o e-mail.
3) Acesse o Portal no Vercel, faça login OIDC e use as funcionalidades.

## 9) CI/CD (opcional nesta fase)

- GitHub Actions: deploy para Render via API e Vercel via token, em branches `staging`/`main`.
- Proteja secrets no GitHub (OIDC_ISSUER, MONGODB_URI, etc.).

## 10) Suporte/Operação

- Monitore logs e health. Configure alertas (Render Health checks, Vercel Analytics, Atlas Alerts).
- Defina backups/snapshots no Atlas. Planeje rotação de segredos.

---

Com isso, o staging fica replicável e seguro. Na próxima sessão, posso provisionar tudo e conectar os repositórios para o primeiro deploy automatizado.
