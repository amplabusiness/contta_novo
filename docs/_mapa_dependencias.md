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

---

## Tabela por projeto (internas e NuGet)

- Corporate.Contta.Schedule.Api (ASP.NET Core 3.1)
  - Internas: Application, Domain, Infra, WebGerarPDF
  - NuGet: AutoMapper, FluentValidation, MediatR, Microsoft.AspNetCore.Authentication.JwtBearer, Microsoft.IdentityModel.Tokens, MongoDB.Bson, Serilog.*, Swashbuckle, Newtonsoft.Json
  - Motivo: API HTTP com autenticação Bearer; mediatr/DI; logging; Swagger; JSON.

- Corporate.Contta.Schedule.Application (netstandard2.1)
  - Internas: Domain, Infra
  - NuGet: MediatR, MailKit/MimeKit, MongoDB.Bson
  - Motivo: Handlers/DTOs; envio de e-mail; serialização BSON em mensagens.

- Corporate.Contta.Schedule.Domain (netstandard2.0)
  - Internas: —
  - NuGet: AspNetCore.Http.Features, AspNetCore.Mvc.Core, MongoDB.Bson, Newtonsoft.Json
  - Motivo: tipos/contratos compartilhados; compatibilidade web; BSON/JSON.

- Corporate.Contta.Schedule.Infra (netstandard2.0)
  - Internas: Domain
  - NuGet: AngleSharp, AutoMapper, EPPlus, MongoDB.Driver, Polly, RabbitMQ.Client, RestSharp, Sanatana.MongoDb, StackExchange.Redis, System.IdentityModel.Tokens.Jwt
  - Motivo: infraestrutura (DB, filas, cache, exportação, crawling, resiliência).

- Corporate.Contta.Schedule.SpedContta (netcoreapp3.1)
  - Internas: —
  - NuGet: EPPlus, MongoDB.Driver
  - Motivo: geração/validação SPED lendo Mongo e gerando planilhas/arquivos.

- ConttaContribuicoes/SpedBr.Common/SpedBr.EfdContribuicoes (.NET Framework 4.6)
  - Internas: SpedBr.Common
  - NuGet: System
  - Motivo: modelos SPED PIS/COFINS reutilizados.

- ConsumerXml (net8.0)
  - Internas: —
  - NuGet: Newtonsoft.Json, RabbitMQ.Client, Serilog, Microsoft.Extensions.Configuration*
  - Motivo: consumidor/worker de fila.

Outros (Node/TS): contta-search-api, contta-excel-parser

## Grafo de camadas (mermaid)

```mermaid
flowchart LR
  C[Domain] --> B[Application]
  B[Application] --> D[Infra]
  D[Infra] --> A[API]

  %% Mapeamento para projetos
  C --- C1[Corporate.Contta.Schedule.Domain]
  B --- B1[Corporate.Contta.Schedule.Application]
  D --- D1[Corporate.Contta.Schedule.Infra]
  A --- A1[Corporate.Contta.Schedule.Api]

  %% SPED lê/grava via Infra
  E[SPED] --- E1[Corporate.Contta.Schedule.SpedContta]
  E --> D
```
overview and intent
Put the system in production with fully automated deploy (Render for backends, Vercel for frontends), modernize auth to Keycloak OIDC + PKCE, purge repo secrets, configure Render environment groups, and use e-contta.com.br.
Produce deep-dive docs: NF-e import, tax tables, base de cálculo, Simples/Presumido/Real, SPED cross-checks.
Run “Investigator mode” to locate logic with exact file paths, signatures, and diagrams; generate inventory docs.
New evidence checklist: prove in code regime choice, Simples segregation (monofásico/ST/isento), RBT12/Annex selection, base de cálculo composition, jobs/updates/logs, SPED generation/validation coverage, UF/segment params and vigência/flags; add minimal unit tests for critical cases; document login/token flow.
stack and artifacts
Backend: ASP.NET Core 3.x, MongoDB, RabbitMQ, Redis, Serilog, Swagger; NF-e/NFS-e pipelines; SPED structures.
Identity: moving to Keycloak OIDC + PKCE (current JWTs are loose).
Data: CFOP/CSOSN static lists; crawlers for aliquots/annex; lacking versioned tables.
Workers: XML consumer; SPED console components.
Docs: architecture, flows, checklists, investigator pack, NF-e ingestion diagram already created.
latest commands and key findings
I opened key files to ground the “evidence-oriented” checklist:

NfeRepository.cs
Evidence: Idempotent NF-e insert via NotaJaFoiGravada(CodBarra); separates Venda vs Entrada vs Devolução; composes base de cálculo aggregates (GetBaseCalculo).
Simples link: Manual deletes adjust Simples via DebitarValorSimples with a hardcoded Mongo URI (mongodb://contta:contta123456@192.46.218.34…), confirming secrets in code and Simples monthly dashboard mutation.
SPED linkage: Builds Registro E110/E111/E113/E115/E116 from NFE data.
Items mapping shows ICMS ST fields present (IcmsStCalculationBasis, IcmsStAliquot, IcmsStValue).
IntegrationXmlMode55.cs
Evidence: Operation type detection (Venda, Entrada, DevolucaoSaida/Compra) via NatOp + ModeloNota; UF mapping via CUF; base calculation composition from Total.ICMSTot: VBC, VBCST, VNF, VProd, VDesc, VFrete, VSeg, VIPI, VPIS, VCOFINS.
Product mapping flags placeholders for Mono/ST/benefits/isento but they default false; CSOSN captured when ICMSSN102 present.
IntegrationTbSimplesNfManual.cs
Evidence: Simples aggregation into TbDashboardClientes with BaseCalculo, ValorSaidaMercadoria, NotaServicoPrestador; monthly grouping; duplicate hardcoded Mongo URI; joins CFOP to compute baseCalculo.
Update flows (ValidacaoEntardaSaida / ValidacaoTbSimplesServico) mutate monthly totals and mark NF-e integrated.
Corporate.Contta.Schedule.Domain/Entities/Imporsto/GetCfop.cs
Evidence: Extensive static CFOP lists for Venda/Entrada/Devolução/Remessa/Transferência/etc., used in books and Simples base aggregation.
Corporate.Contta.Schedule.Domain/Entities/TbSimplesNacional/TabelaExterna.cs
Not found at this path (file missing or moved), confirming earlier suspicion that a proper Simples effective rate engine and versioned tables are incomplete.
Checkpoint: Loaded 4 files successfully; 1 missing. Findings confirm idempotent NF-e ingestion, base de cálculo composition, ST fields in structures, Simples monthly aggregation via raw Mongo updates with hardcoded credentials, and CFOP-driven flows. Next step would be drafting the evidence doc sections and stubbing tests.

current state toward goals
Deployment/auth: Plan and docs exist; code still holds secrets and lacks Keycloak OIDC integration.
Evidence checklist: Substantial proof exists for ingestion, base de cálculo components, ST fields, CFOP usage, Simples monthly aggregation; gaps remain for Simples effective rate/RBT12/annex selection and table versioning.
SPED: Classes exist and are fed by repo methods; validation coverage tests are missing.
continuation plan
Write docs/login_token.md explaining current JWT handling vs target Keycloak OIDC + PKCE flow (strict validation, token storage, refresh).
Add minimal unit tests for:
GO carne/aves benefit flag behavior (currently missing).
Simples monofásico handling (flags are present but default false—add a test + TODO).
ICMS ST base and values in items and analytics.
Devolução paths (DevolucaoSaida/Compra) detection via NatOp and ModeloNota.
Replace hardcoded Mongo URIs with config; add environment var wiring; prepare Render/Vercel env groups.
Start a thin ISimplesCalculator contract to encapsulate RBT12, annex choice, and effective rate; back it with versioned tables (introduce storage + vigência fields).
