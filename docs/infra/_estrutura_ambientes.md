# Estrutura de Ambientes (dev / staging / prod)

Camada | Dev | Staging | Prod
---|---|---|---
Frontend | Vercel (preview) | Vercel (preview + domínio staging) | Vercel (produção + domínio)
APIs/Workers | Render (free/standard) | Render (standard) | Render (production)
DB (MongoDB Atlas) | Projeto “Dev” (M0/M10) | Projeto “Stage” | Projeto “Prod” (M30+)
Fila (CloudAMQP) | Instância Dev | Instância Stage | Instância Prod (ha/metrics)
Auth (Keycloak) | Realm conttatax-dev | conttatax-stg | conttatax-prod

Regra: nenhum secret em repositório. Use Environment Variables dos provedores.
