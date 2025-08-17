# Render Environment Group: contta-production

Crie um grupo de ambiente único para compartilhar variáveis entre os serviços do Blueprint. O `render.yaml` já referencia este grupo.

## Nome do grupo
- contta-production

## Variáveis do grupo (defina como Secret; sync desativado)
- OIDC_ISSUER = https://<KEYCLOAK_HOST>/realms/contta
- CORS_ORIGINS = https://<DOMINIO_PORTAL>[,https://<DOMINIO_STAGING>]
- PRODUCTION_URL = https://<HOST_PUBLICO_DA_API>
- MONGODB_URI = mongodb+srv://...
- RABBITMQ_URL = amqps://...

Observações
- Não comite valores; preencha somente no UI do Render.
- OIDC_AUDIENCE (contta-portal) permanece no serviço quando aplicável.

## Como criar no Render
1) Render → Settings → Environment Groups → New Group
2) Nome: `contta-production`
3) Adicione as variáveis acima (todas como Secret). Salve.

## Como anexar aos serviços
1) Render → Services → abra cada serviço:
   - contta-searchapi-staging
   - contta-excelparser-staging
   - contta-consumerxml-staging
2) Environment → Attach Environment Group → `contta-production` → Save.

Keycloak
- `contta-keycloak-staging` usa variáveis próprias (ex.: KEYCLOAK_ADMIN_PASSWORD). Não precisa do grupo.

## Depois de anexar
- Rode o workflow "Deploy Render Blueprint" no GitHub Actions para aplicar as variáveis e subir os serviços.
- Ajustes futuros no grupo propagam para todos os serviços anexados sem precisar mudar o repositório.
