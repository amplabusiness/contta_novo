# Contta codebase inventory and integration blueprint

This document maps the apps/services in this workspace and outlines how they fit together and can be integrated.

## Products and apps (by folder)

- portal-contta-master (React, CRA)
  - Portal web principal (dashboard/gestão). Dependencies: Ant Design 3.x, Redux, Axios.
- portal-simples-front-end-master (React, CRACO)
  - Portal “Simples” (versão mais nova de stack React). Ant Design 4.x, React 17.
- contta-website-main (Next.js)
  - Site institucional/marketing. Next + styled-components.

- contta-search-api-main (Node.js/Express + Mongoose)
  - API de busca/consulta (MongoDB). Scripts: dev/build/start.
- contta-excel-parser-main (Node.js/Express)
  - Serviço para upload e parsing de Excel (xlsx) expondo endpoints HTTP.
- Contta-Api-master/Contta-Api (ASP.NET Core 3.1 + MongoDB)
  - “Contta Api” (Inteligência CNPJ). Swagger, MongoDB.Driver.
- agendador-back-end-master/Corporate.Contta.Schedule.Api (ASP.NET Core)
  - API do agendador/serviços corporativos (integra com Domain/Infra/Application/SpedContta).

- agendador-back-end-master/ConsumerXml (Console .NET 8)
  - Consumer RabbitMQ de NFe (modelo 55, nfeProc). Suporta XML/JSON, QoS/ack, logs.
- emalotedescktop-master (Worker .NET Core 3.1, Windows Service capable)
  - Serviço “Emalote”: integra RabbitMQ, S3, SQLite/PDF. Gera/arquiva PDFs e possivelmente dispara e-mails/malotes.
- robo-eco-master (Console .NET Core 3.1)
  - Robô Econet: crawler/scraper com Mongo/LiteDB, NodeJS bridge (Jering.Javascript.NodeJS).
- agendador-back-end-master/Corporate.Contta.Schedule.SpedContta (+ WebGerarPDF)
  - Bibliotecas/serviços auxiliares (SPED, geração de PDF).

- downloader-xml-sefaz-go-main (sem manifesto)
  - Utilitário para baixar XMLs da SEFAZ-GO (detalhes não documentados).
- recupera-pis-cofins-main (sem manifesto)
  - Utilitário de recuperação fiscal (detalhes não documentados).
- auth-ecac-main, cnpj-main (somente README)
  - Placeholders/utilitários relacionados a autenticação e dados CNPJ.
- bak-contta-main
  - Artefatos de backup (tgz/rar).

## Observações de stack e maturidade

- Front-ends: 2 portais React (CRA/CRACO) e 1 site Next.js. Portal Simples é tecnicamente mais novo que PortalContta.
- Back-ends: misto Node (Express) + .NET (3.1 e 8). MongoDB é comum; RabbitMQ presente em ConsumerXml/Emalote.
- Variação de versões: várias libs defasadas (.NET Core 3.1 EOL, dependências beta/alpha em Emalote).

## Integração: visão de alto nível

- Autenticação/autorização unificada
  - Centralizar JWT/OAuth2 (ex.: Keycloak/IdentityServer/Auth0). Front-ends usam o mesmo provedor.

- Gateway/API composition
  - Colocar Traefik/NGINX como API Gateway (rota por host/path):
    - /api/search → contta-search-api-main
    - /api/excel → contta-excel-parser-main
    - /api/cnpj → Contta-Api (ASP.NET)
    - /api/schedule → Corporate.Contta.Schedule.Api

- Barramento de eventos (RabbitMQ)
  - ConsumerXml publica eventos “NFe.Processed” (resumo e/ou XML bruto) após validação.
  - Emalote consome e gera PDF/malote; publica “Document.Generated” com links S3 e status.
  - Portal consulta status via APIs ou recebe via websockets/sse (opcional).

- Dados e contratos
  - Padronizar MongoDB (uma instância para dev), coleções por domínio. Definir esquemas (JSON Schema) para payloads (NFe, Document, Company).
  - Definir contratos de eventos (JSON Schema/Avro) versionados (compatibilidade forward/backward).

- Observabilidade
  - Logs estruturados (Serilog nas .NET, Winston/Morgan no Node) enviados para ELK/Graylog.
  - Métricas Prometheus + Grafana. Health checks em todos os serviços.

## Mapa rápido de dependências

- PortalContta / Portal Simples
  - Consomem: /api/* via gateway; autenticam via OIDC.
- Website (Next)
  - Público; opcionalmente consome /api públicos.
- ConsumerXml (RabbitMQ → Mongo/S3 via Emalote)
  - Entrada: fila RabbitMQ (Modelo55). Saída: eventos/coleção Mongo.
- Emalote
  - Entrada: eventos RabbitMQ; Saída: PDFs (S3), registros (Mongo/SQLite), eventos de status.
- Search API / Excel Parser / Contta Api / Schedule Api
  - Dados em Mongo; expõem endpoints REST sob gateway; podem publicar/consumir eventos conforme evolução.

## Riscos e alinhamentos

- Divergência de versões (.NET 3.1 vs 8; dependências beta). Plano de upgrade recomendado.
- Duplicidade de front-ends (PortalContta vs Portal Simples). Definir o “principal” e descontinuar o outro ou mantê-los por segmento.
- Falta de documentação em utilitários (downloader, recupera-pis-cofins). Levantar requisitos antes de integrar.

## Roadmap sugerido (curto → médio prazo)

1) Dev environment orquestrado
   - Criar docker-compose com: mongo, rabbitmq, contta-search-api, contta-excel-parser, Contta-Api, schedule-api, consumerxml, (emalote opcional), portal(es), website.
   - Padronizar .env e variáveis.

2) Gateway + Auth
   - Subir Traefik/NGINX como gateway, configurar rotas e CORS. Integrar OIDC Provider.

3) Contratos e eventos
   - Definir JSON Schemas para NFe.Processed e Document.Generated. Implementar publicação/consumo consistente.

4) Upgrades e hardening
   - Migrar .NET Core 3.1 → .NET 8 (Contta-Api, Emalote, RoboEconet, Schedule.Api). Atualizar dependências Node.
   - Adicionar health checks, logs estruturados, métricas.

5) Consolidação de front-ends
   - Escolher Portal principal (ou conviver PortalContta e Simples por público-alvo). Unificar autenticação, temas e navegação.

## Decisões pendentes para você validar

- Qual portal será o “principal” (PortalContta vs Portal Simples)?
- Adotaremos um provedor de identidade (Keycloak/Outro)?
- Precisamos de DLQ padrão no RabbitMQ (dead-letter) em todas as filas? (recomendado)

—
Gerado automaticamente para dar visibilidade do ecossistema e acelerar a integração.
