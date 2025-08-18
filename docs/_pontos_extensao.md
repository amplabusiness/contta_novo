# Pontos de Extensão — Núcleo C# (Agendador/ERP)

Lista objetiva dos pontos onde é seguro/razoável evoluir o sistema com baixo acoplamento.

## Motor fiscal (Simples/PGDASD)

- `Domain/Entities/NfeAgg/CalcularSimples.cs`: classe vazia — criar serviço de cálculo com interface (ex.: `ISimplesCalculator`) e implementar por UF/Anexo.
- `Infra/Repositories/ImpostosRepository`: oferecer métodos agregadores para insumos de cálculo (por período, CFOP, CST/CSOSN, NCM, operação).

## Autenticação/Autorização

- `Api/Startup.cs`: trocar JWT Bearer estático por OpenIdConnect com Keycloak; validar `Authority` (Issuer) e `Audience`; políticas por roles/grupos (realm roles).
- `AddAuthorization`: mover políticas para constantes e centralizar claims/roles.

## CORS e configuração

- `Api/Startup.cs`: substituir origens fixas por `CORS_ORIGINS` (lista separada por vírgula) via env; permitir HEAD/OPTIONS e credenciais quando necessário.

## Observabilidade

- Padronizar logs com correlation-id e enrichers (Serilog); adicionar métricas (Prometheus) e HealthChecks customizados (Mongo, RabbitMQ).

## Resiliência

- `ConsumerXml`: adicionar retries com backoff e DLQ requeue opcional por tipo de erro; telemetria de consumo (taxa, latência, NACKs).
- `NfeRepository`: revisar `try/catch`; retornar Result/OneOf em vez de rethrow sem contexto; transacionar inserções relacionadas (onde aplicável).

## Persistência/Mongo

- Remover connection string hardcoded de `DebitarValorSimples`; padronizar acesso via `IOptions`/ENV; evitar `static` contexts, preferir DI/singleton de `IMongoClient`.

## API e contratos

- `Controllers/*`: documentar com Swagger anotations; validar entradas com FluentValidation; versionar API.
