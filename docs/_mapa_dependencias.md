# Mapa de Dependências — Núcleo C# (Agendador/ERP)

Componentes, contratos e integrações mapeados a partir do código.

## Projetos e principais dependências

- Corporate.Contta.Schedule.Api
  - ASP.NET Core, Serilog, Swagger
  - DI (NativeInjectorConfig):
    - Repositórios: `UserRepository`, `CompanyRepository`, `NfeRepository`, `ProductRepository`, `ApuracaoRepository`, `EstoqueRepository`, `EmpresaEmitRepository`, `EmpresaDestRepository`, `NotificationRepository`, `ConfigurationFhRepository`, `ConfigurationUserRepository`, `DashboardHomeRepository`, `CriticasNovasRepository`, `ImpostosRepository`, `DetalhamentoApuracaoRepository`, `ImpostoProductRepository`, `SociosRepository`, `AgBlocoERepository`
    - Serviços: `IEmailService`, `ICacheService`
    - MediatR Handlers: `GetInfomationByDocumentHandler` (e demais no assembly)
  - AutoMapper: `AutoMapperConfig.GetMapperConfiguration()`

- Corporate.Contta.Schedule.Infra
  - MongoDB.Driver (vários `MongoDBContext<T>` estáticos)
  - Repositórios: `NfeRepository`, `ImpostosRepository`, `ProductRepository`, `EmpresaDestRepository`, `ServicoEntityRepository` etc.
  - Adapter XML NF-e: `IntegrationXmlMode55` (mapeia `NfeProc` → Entidades)

- Corporate.Contta.Schedule.Domain
  - Entidades: `NFE`, `Produtos`, `Impostos`, `EmpresaEmit`, `EmpresaDest`, `Difal`, `AjusteNfe`, `FullNFE` e agregados auxiliares
  - Classe placeholder: `CalcularSimples`

- ConsumerXml
  - RabbitMQ.Client, Serilog, Microsoft.Extensions.Configuration

## Integrações externas

- RabbitMQ
  - Conexão: `RABBITMQ_URL` (amqps://user:pass@host/vhost) ou parâmetros individuais (host, port, vhost, user, pass, queue, prefetch)
  - Consumo: `EventingBasicConsumer` com QoS

- MongoDB
  - Contextos por agregado (estáticos)
  - Ponto crítico: string de conexão fixa em `NfeRepository.DebitarValorSimples`

- Autenticação
  - JWT Bearer (sem validação de emissor/audiência) — previsto integrar com Keycloak OIDC

- Observabilidade
  - Serilog console

## Entradas/Saídas por componente

- ConsumerXml
  - In: fila RabbitMQ (mensagem XML NF-e 55)
  - Out: log Serilog; chamada a `NfeRepository` (indireta)

- NfeRepository
  - In: modelos `NfeProc`, consultas HTTP/API
  - Out: coleções Mongo (NFE, Produtos, Impostos, Empresas); totais/relatórios (Bloco E, livros, dashboards)

- API Controllers
  - In: HTTP (REST)
  - Out: DTOs agregados (NfeT, FullNFE, registros SPED, filtros por período/CFOP/NCM/CST etc.)
