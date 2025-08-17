# 🎯 Template de Configuração Render Blueprint

## Copy & Paste para Variáveis do Blueprint

### contta-keycloak-staging
```
KEYCLOAK_ADMIN=admin
KEYCLOAK_ADMIN_PASSWORD=ConttaKeycloak2025!@#
```
*Marque KEYCLOAK_ADMIN_PASSWORD como: sync: false*

### contta-searchapi-staging
```
NODE_ENV=production
PORT=5001
MONGODB_URI=mongodb+srv://usuario:senha@cluster.mongodb.net/contta?retryWrites=true&w=majority
OIDC_ISSUER=https://contta-keycloak-staging.onrender.com/realms/contta
OIDC_AUDIENCE=contta-portal
CORS_ORIGINS=http://localhost:3000,https://contta-portal.vercel.app
```
*Marque MONGODB_URI como: sync: false*

### contta-excelparser-staging
```
NODE_ENV=production
PORT=5002
OIDC_ISSUER=https://contta-keycloak-staging.onrender.com/realms/contta
OIDC_AUDIENCE=contta-portal
PRODUCTION_URL=https://contta-portal.vercel.app
```

### contta-consumerxml-staging
```
RABBITMQ_URL=amqps://usuario:senha@instancia.cloudamqp.com/vhost
RABBITMQ_QUEUE=Modelo55
RABBITMQ_PREFETCH=20
RabbitMQ__Durable=true
RabbitMQ__Exclusive=false
RabbitMQ__AutoDelete=false
RabbitMQ__DeadLetterExchange=dlx.nfe
RabbitMQ__DeadLetterRoutingKey=Modelo55.dlq
```
*Marque RABBITMQ_URL como: sync: false*

## ⚡ Valores Placeholder

### Substitua os valores:
- `ConttaKeycloak2025!@#` → Sua senha admin Keycloak
- `mongodb+srv://usuario:senha@cluster...` → Sua MongoDB URI do Atlas
- `amqps://usuario:senha@instancia...` → Sua CloudAMQP URL
- `https://contta-portal.vercel.app` → URL real do Portal no Vercel

## 🔄 Após Blueprint Deploy

### Copie os Service IDs gerados:
```
RENDER_SERVICE_ID_KEYCLOAK=srv-xxxxxxxxxx
RENDER_SERVICE_ID_SEARCHAPI=srv-yyyyyyyyyy  
RENDER_SERVICE_ID_EXCELPARSER=srv-zzzzzzzzzz
RENDER_SERVICE_ID_CONSUMERXML=srv-wwwwwwwwww
```

### Cole no GitHub Secrets:
- Settings → Secrets and variables → Actions
- New repository secret para cada Service ID
- RENDER_API_TOKEN (seu token da API Render)

## 🎯 Resultado Final
4 serviços rodando no Render + automação GitHub Actions + smoke tests!
