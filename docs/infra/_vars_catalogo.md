# Catálogo de Variáveis (origem e uso)

Nome | Descrição | Utilizado em (arquivo/código) | Serviço | Ambientes
---|---|---|---|---
MONGODB_URI | Conn string Mongo | `docker-compose.yml` (searchapi), Node `database.ts` | Search API | dev/stg/prod
AMQP_URL | URL AMQP (CloudAMQP) | (definir no Render) | Workers/APIs | dev/stg/prod
RABBITMQ_* | Host/Port/User/Pass/VHost/Queue | `docker-compose.yml` + ConsumerXml appsettings | ConsumerXml | dev
OIDC_ISSUER | Emissor OIDC | `docker-compose.yml` (searchapi/website) | Front/API | dev/stg/prod
OIDC_AUDIENCE | Audience JWT | `docker-compose.yml` (searchapi) | APIs | dev/stg/prod
KEYCLOAK_URL/REALM | Base/Realm | `docker-compose.yml` | Auth | dev/stg/prod
KEYCLOAK_ADMIN(_PASSWORD) | Admin credenciais | `docker-compose.yml` (dev) | Keycloak | dev
NEXT_PUBLIC_* | Variáveis públicas do front | website .env / compose | Front | dev/stg/prod

Sinalize variáveis órfãs/duplicadas ao consolidar envs nos provedores e remover hardcodes em código.
