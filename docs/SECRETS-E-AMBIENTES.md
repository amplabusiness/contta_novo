# Segredos e Ambientes (Checklist)

Este checklist resume os segredos/variáveis que precisam ser configurados no GitHub, Vercel e Render para o deploy 100% automatizado.

## GitHub Actions (Secrets)
- VERCEL_TOKEN qC0cu15AzqUrqmNpAEWOnyNT
- VERCEL_ORG_ID
- VERCEL_PROJECT_ID prj_AU0ftVr2HaFTzna1k3wQ3Dakuwn3
- RENDER_API_TOKEN rnd_ZxxPh9V5W4Odi8dSkTC3LMG5dl5I
- RENDER_SERVICE_ID_KEYCLOAK (opcional)
- RENDER_SERVICE_ID_SEARCHAPI (opcional)
- RENDER_SERVICE_ID_EXCELPARSER (opcional)
- RENDER_SERVICE_ID_CONSUMERXML (opcional)
- RENDER_SERVICE_ID_ROOT (opcional)
- RENDER_URL_KEYCLOAK (opcional, smoke)
- RENDER_URL_SEARCHAPI (opcional, smoke)
- RENDER_URL_EXCELPARSER (opcional, smoke)
- RENDER_URL_ROOT (opcional, smoke)

## Vercel (Variáveis de Ambiente do Projeto do Portal)
- NEXT_PUBLIC_OIDC_ISSUER = https://<KEYCLOAK_HOST>/realms/contta
- NEXT_PUBLIC_OIDC_CLIENT_ID = contta-portal
- NEXT_PUBLIC_PORTAL_URL = https://<SEU_DOMINIO_PORTAL>
- NEXT_PUBLIC_SEARCH_API_URL = https://<HOST_SEARCH_API> (se aplicável)
- NEXT_PUBLIC_EXCEL_PARSER_API_URL = https://<HOST_EXCEL_PARSER> (se aplicável)

## Render (Env/Secrets por serviço)
- OIDC_ISSUER = https://<KEYCLOAK_HOST>/realms/contta
- CORS_ORIGINS = https://<SEU_DOMINIO_PORTAL>[,https://<DOMINIO_STAGING>]
- MONGODB_URI = mongodb+srv://... (ou URL do Mongo gerenciado)
- RABBITMQ_URL/AMQP_URL = amqps://...
- PRODUCTION_URL = https://<HOST_DA_API>
- Outras variáveis específicas definidas no render.yaml

## Keycloak (Cliente OIDC: contta-portal)
- redirectUris: https://<SEU_DOMINIO_PORTAL>/auth/callback (e opcionalmente https://*.vercel.app/auth/callback)
- webOrigins: https://<SEU_DOMINIO_PORTAL> (e opcionalmente https://*.vercel.app)

## Dicas
- Não comitar valores reais de segredos no repositório. Use os cofres de cada plataforma.
- Mantenha OIDC_ISSUER, CORS e PRODUCTION_URL alinhados aos domínios de produção.
- Após configurar, rode os workflows “Deploy Portal (Vercel)” e “Deploy Services (Render)” e verifique os smoke checks.

### Recuperação rápida de deploy (local)
- Defina `RENDER_API_TOKEN` como variável de ambiente no PowerShell.
- Rode: `scripts/render-redeploy.ps1 -ServiceId <ID_DO_SERVICO>`
