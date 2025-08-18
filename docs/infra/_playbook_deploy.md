# Playbook de Deploy (DEV/STG/PROD)

## 1) Provedores e projetos
- Vercel: criar projeto do frontend (Next.js) com Preview Deploys.
- Render: criar serviços (web/worker/Keycloak) via Dockerfile/render.yaml.
- Atlas: criar clusters dev/stg/prod com usuário dedicado.
- CloudAMQP: instâncias dev/stg/prod com vhosts e DLQ.
- Keycloak: realms conttatax-dev/stg/prod; clients web (public) e api (confidential).

## 2) Variáveis e segredos (modelo em `.env.schema`)
- Vercel: Project → Settings → Environment Variables (Preview/Production) — NEXT_PUBLIC_*, OIDC_*, API_URL.
- Render: Service → Environment → Environment Variables (MONGO_URI, AMQP_URL, KEYCLOAK_*…).
- Atlas: SRV connection string; IP allowlist; usuário app.
- CloudAMQP: pegar URL amqps e criar filas (nfes.import, sped.process, tax.calc, reports.build) + DLQ.
- Keycloak: configurar clients, redirect URIs, roles, mappers; pegar JWKS URL.

## 3) Deploys
- Front (Vercel): conectar GitHub repo; cada PR gera Preview; merge em main → produção.
- APIs/Workers (Render): apontar para o repo; usar `render.yaml` ou Dockerfile; configurar deploy hooks se quiser CI acionar.

## 4) Validação pós-deploy
- Front: abrir / e /login (redireciona para Keycloak; callback ok).
- API: GET /health = 200; rota protegida → 401 sem token; 200 com JWT.
- Worker: publicar mensagem de teste em `tax.calc`; ver consumo/logs.
- DB: inserir/consultar doc teste; conferir índices básicos.

## 5) Observabilidade
- Logs: Render logs; Serilog/Console nas APIs.
- Métricas: CloudAMQP metrics; Atlas Performance Advisor.
- Backups: Atlas snapshots/continuous; restore simulado trimestral.
