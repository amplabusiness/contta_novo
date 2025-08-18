# Relatório de Diferenças — GitLab (orig) vs GitHub (atual)

Nota: Rode os comandos abaixo localmente para comparar repositórios. Este arquivo consolida o que for encontrado e já antecipa diferenças detectadas nesta base.

## Como comparar (local)

```powershell
# 1) clone as duas origens
git clone <url-gitlab> erp-orig
git clone <url-github> erp-atual

# 2) comparar divergências (no repo erp-atual)
cd erp-atual
git remote add orig ../erp-orig
git fetch orig

# 3) ver diferenças resumidas (ajuste branch)
git log --oneline --graph --decorate --all --boundary ..orig/main
git diff --stat orig/main..main
```

## 1) Divergências por arquivos/pastas (antecipação neste monorepo)

- CI/CD
  - Encontrado: `.gitlab-ci.yml` em:
    - `agendador-back-end-master/agendador-back-end-master/.gitlab-ci.yml`
    - `agendador-back-end-master (1)/agendador-back-end-master/.gitlab-ci.yml`
    - `portal-simples-front-end-master/portal-simples-front-end-master/.gitlab-ci.yml`
  - Não encontrado: `.github/workflows/*.yml` → pipelines GitHub Actions ausentes.
- Deploy/Infra
  - `render.yaml` na raiz: presente.
  - Dockerfiles: múltiplos (API .NET, Node APIs, Website, ConsumerXml, Keycloak em `./.docker/keycloak/Dockerfile`).
  - vercel.json: não encontrado (Vercel costuma autoconfigurar Next.js, mas recomendamos explicitar).
- Config/ENV
  - Vários `appsettings*.json` (APIs .NET) e `.env.example` (front/Node).
  - `docker-compose.yml`: orquestra RabbitMQ, Mongo, Keycloak e serviços Node.

## 2) Mudanças que impactam deploy/infra (riscos)

- Falta de GitHub Actions → deploy automatizado no GitHub não existe ainda.
- Segredos hardcoded em C# (MongoClient) devem ser migrados para ENV no provedor.
- vercel.json ausente → padronizar builds/preview para frontend.
- render.yaml existe, mas revisar plano/healthcheck/envVars para cada serviço.

## 3) Ações recomendadas para unificar

- Adotar GitHub Actions como pipeline principal (template em `docs/infra/templates/github-actions-api.yml`).
- Manter `.gitlab-ci.yml` apenas se necessário; caso contrário, descontinuar para evitar divergência.
- Criar `vercel.json` no(s) front(s) ou documentar a detecção automática (e variáveis por ambiente).
- Revisar `render.yaml` e criar Environment Groups no Render; usar `sync:false` para segredos.
- Migrar todos segredos para Vercel/Render/Atlas/CloudAMQP/Keycloak.

## 4) Diferenças por área

- API (.NET)
  - Dockerfile presente; pipelines GitLab possivelmente antigos; faltam workflows GitHub.
  - Dependem de MongoDB e RabbitMQ.
- Frontend (Next/React)
  - Dockerfile presente; sem vercel.json; há .env.example.
- Workers (ConsumerXml)
  - Dockerfile e appsettings; usa RabbitMQ e Mongo; precisa ENV padronizado.
- DB
  - MongoDB (compose: `MONGODB_URI`); Atlas recomendado para PROD.
- Auth
  - Keycloak (Dockerfile); configurar Realms/Clients por ambiente.

Preencha esta seção com o output real dos comandos git assim que rodar a comparação local.
