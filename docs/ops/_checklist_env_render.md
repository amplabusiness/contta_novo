# Checklist de Variáveis de Ambiente — Render Blueprint

> Use este checklist para garantir que todos os segredos/variáveis obrigatórios estão cadastrados no painel do Render antes do deploy.

## Variáveis globais (envVarGroups: contta-production)
- [ ] OIDC_ISSUER
- [ ] CORS_ORIGINS
- [ ] PRODUCTION_URL
- [ ] MONGODB_URI
- [ ] RABBITMQ_URL

## Segredos obrigatórios por serviço

### contta-keycloak-staging
- [ ] KEYCLOAK_ADMIN
- [ ] KEYCLOAK_ADMIN_PASSWORD

### contta-searchapi-staging
- [ ] NODE_ENV (production)
- [ ] PORT (5001)
- [ ] OIDC_AUDIENCE (contta-portal)

### contta-excelparser-staging
- [ ] NODE_ENV (production)
- [ ] PORT (5002)
- [ ] OIDC_AUDIENCE (contta-portal)

### contta-consumerxml-staging
- [ ] RABBITMQ_QUEUE (Modelo55)
- [ ] RABBITMQ_PREFETCH (20)
- [ ] RabbitMQ__Durable ("true")
- [ ] RabbitMQ__Exclusive ("false")
- [ ] RabbitMQ__AutoDelete ("false")
- [ ] RabbitMQ__DeadLetterExchange (dlx.nfe)
- [ ] RabbitMQ__DeadLetterRoutingKey (Modelo55.dlq)

## Variáveis de integração/infra
- [ ] RENDER_API_TOKEN (se for usar deploy via API)

---

**Importante:**
- Nunca coloque segredos no repositório.
- Sempre cadastre/atualize pelo painel do Render (ou API segura).
- Marque cada item acima ao configurar.
