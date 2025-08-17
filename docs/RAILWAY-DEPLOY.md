# Deploy no Railway

Este repositório possui dois serviços Node hospedáveis no Railway:

- Search API: `contta-search-api-main/contta-search-api-main`
- Excel Parser: `contta-excel-parser-main/contta-excel-parser-main`

E um serviço .NET (ConsumerXml) opcional:

- ConsumerXml (.NET 8): `agendador-back-end-master/agendador-back-end-master/ConsumerXml`

## CLI & CI
- CLI: `npm i -g @railway/cli`
- CI (GitHub Actions): `.github/workflows/deploy-railway.yml`

### Secrets necessários (GitHub → Actions → Secrets)
- `RAILWAY_TOKEN`
- `RAILWAY_PROJECT_ID_SEARCH`, `RAILWAY_SERVICE_ID_SEARCH`
- `RAILWAY_PROJECT_ID_EXCEL`, `RAILWAY_SERVICE_ID_EXCEL`
 - `RAILWAY_PROJECT_ID_CONSUMER`, `RAILWAY_SERVICE_ID_CONSUMER`

## Variáveis de ambiente por serviço (no Railway)
- `NODE_ENV`, `PORT`
- `MONGODB_URI` (Search API)
- `OIDC_ISSUER`, `OIDC_AUDIENCE` (ambos)
 - `CORS_ORIGINS` (Search API, separado por vírgula)
 - ConsumerXml:
	 - Prefira `RABBITMQ_URL` (amqps do CloudAMQP) no formato `amqps://user:pass@host/vhost`
	 - Alternativas: `RABBITMQ_HOST`, `RABBITMQ_PORT`, `RABBITMQ_VHOST`, `RABBITMQ_USER`, `RABBITMQ_PASSWORD`
	 - `RABBITMQ_QUEUE=Modelo55`, `RABBITMQ_PREFETCH=20`
	 - `RabbitMQ__Durable=true`, `RabbitMQ__Exclusive=false`, `RabbitMQ__AutoDelete=false`
	 - `RabbitMQ__DeadLetterExchange=dlx.nfe`, `RabbitMQ__DeadLetterRoutingKey=Modelo55.dlq`

## Pipeline sugerido
1. Push em `main` dispara build e deploy via `deploy-railway.yml`.
2. Ao finalizar o deploy, dispare `repository_dispatch` do tipo `post-deploy` para rodar `smoke-post-deploy.yml`.
3. Para o ConsumerXml, habilite também o `deploy-consumerxml.yml` com os secrets de projeto/serviço.
