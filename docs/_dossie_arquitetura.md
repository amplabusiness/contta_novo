# Dossiê Técnico Executivo — Núcleo C# (Agendador/ERP)

Este dossiê resume a arquitetura real identificada na pasta `agendador-back-end-master`, com foco em NF-e, camadas, integrações e riscos.

## Visão geral

- Solução: `Corporate.Contta.Schedule.Service.sln` (múltiplos projetos)
- Camadas:
  - API HTTP: `Corporate.Contta.Schedule.Api` (ASP.NET Core)
  - Domínio: `Corporate.Contta.Schedule.Domain`
  - Infra: `Corporate.Contta.Schedule.Infra` (MongoDB, Repositórios, Adapters XML)
  - Worker: `ConsumerXml` (RabbitMQ → parse NF-e)
  - Utilitários: `Corporate.Contta.Schedule.SpedContta`, `WebGerarPDF`
- Infraestrutura: MongoDB, RabbitMQ, Serilog

## Fluxo NF-e (Modelo 55)

1) Consumo
- `ConsumerXml/Program.cs`: lê `RABBITMQ_URL` ou campos individuais; QueueDeclarePassive→fallback declare; QoS (prefetch); desserializa via `MessageParser.ParseNfeProc`; loga sumário; ACK/NACK.

2) Mapeamento e persistência
- `Infra/Models/Adapter/IntegrationXmlMode55.cs`: converte `NfeProc` → `NFE`, `Produtos`, `Impostos`, `EmpresaEmit/Dest`; decide `ModeloTipo` (Venda/Entrada/Devolução); preenche bases/valores.
- `Infra/Repositories/NfeRepository.cs`: `CreateXmlNfe` faz upsert de emit/dest, insere NFE, itens (Fornecedor/Venda), impostos; idempotência por `CodBarra`; consultas agregadas (blocos SPED, dashboards, filtros).
- `Infra/Repositories/ImpostosRepository.cs`: consulta por `produtoId+nfeId` e por `company+nfe`.

3) API
- `Api/Startup.cs`: Controllers, HealthChecks, Swagger, CORS hardcoded, JWT Bearer com validações frouxas (Issuer/Audience desligados); DI em `NativeInjectorConfig.cs` (repositórios/serviços/MediatR/AutoMapper).

## Observabilidade e robustez

- Serilog no worker e API.
- `ConsumerXml`: NACK sem requeue para evitar poison loop; QoS por `Prefetch`.
- Try/catch em repositórios com rethrow sem contexto adicional.
- Contextos Mongo estáticos por agregado.

## Riscos e dívidas

- Credenciais Mongo hardcoded em `NfeRepository.DebitarValorSimples` (crítico): mover para segredos/env.
- JWT sem ValidateIssuer/Audience: alinhar com Keycloak (OIDC) e validar emissor/audiência.
- CORS com origens fixas: parametrizar via env.
- `CalcularSimples` vazio (motor fiscal Simples pendente).
- Namespace `Entities.Imporsto` (typo) em `CsosnSimples`.
- Pastas duplicadas `agendador-back-end-master (1)`: higienizar.

## Referências

- Worker: `agendador-back-end-master/.../ConsumerXml/Program.cs`
- API: `Corporate.Contta.Schedule.Api/Program.cs`, `Startup.cs`, `NativeInjectorConfig.cs`
- Adapter NF-e: `Infra/Models/Adapter/IntegrationXmlMode55.cs`
- Repositórios: `Infra/Repositories/NfeRepository.cs`, `ImpostosRepository.cs`
- Domínio: `Domain/Entities/NfeAgg/*`, `ImpostoAgg/*`
