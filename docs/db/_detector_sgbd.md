# Detector de SGBD e Camada de Acesso a Dados

Este relatório consolida o SGBD e a tecnologia de acesso a dados detectados no monorepo, com âncoras de arquivos/classes/métodos.

## 1) SGBD detectado

- Banco principal: MongoDB
  - Evidências:
    - NuGet no backend .NET: `MongoDB.Driver` e `Sanatana.MongoDb`
      - `agendador-back-end-master/.../Corporate.Contta.Schedule.Infra/Corporate.Contta.Schedule.Infra.csproj`
        - PackageReference Include="MongoDB.Driver" Version="2.10.1"
        - PackageReference Include="MongoDB.Bson" Version="2.13.0-beta1"
        - PackageReference Include="Sanatana.MongoDb" Version="2.3.0"
    - Connection string (compose):
      - `docker-compose.yml` → env `MONGODB_URI: mongodb://mongo:27017/contta`
    - Uso de ODM no Node.js:
      - `contta-search-api-main/contta-search-api-main/src/config/database.ts` → `mongoose.connect(process.env.MONGODB_URI)`
    - Uso direto de MongoClient (hardcoded) no .NET:
      - `Corporate.Contta.Schedule.Infra/Repositories/NfeRepository.cs` → método `DebitarValorSimples(...)`
      - `Corporate.Contta.Schedule.Infra/Models/TbSimplesNacional/IntegrationTbSimplesNfManual.cs` → métodos `CreateTbNfeSaidaManual(...)` e `CreateTbNfeServicoPrestador(...)`

Conclusão: Não há evidências de SGBDs relacionais (SQL Server/PostgreSQL/MySQL/Oracle) em uso nos projetos auditados. O ecossistema é centrado em MongoDB.

## 2) Connection strings — onde estão

- Variáveis de ambiente (compose):
  - Arquivo: `docker-compose.yml`
    - Chave: `MONGODB_URI`
    - Valor exemplo: `mongodb://mongo:27017/contta`
- Hardcoded em código (remover/parametrizar):
  - Arquivo: `agendador-back-end-master/.../Corporate.Contta.Schedule.Infra/Repositories/NfeRepository.cs`
    - Método: `DebitarValorSimples(...)`
    - Trecho: `new MongoClient("mongodb://contta:contta123456@192.46.218.34:27017/...")`
  - Arquivo: `agendador-back-end-master/.../Corporate.Contta.Schedule.Api/Extension/IntegrationTbSimplesNfManual.cs`
    - Métodos: `CreateTbNfeSaidaManual(...)`, `CreateTbNfeServicoPrestador(...)`
    - Trecho: `new MongoClient("mongodb://contta:contta123456@192.46.218.34:27017/...")`
- Node.js
  - Arquivo: `contta-search-api-main/.../src/config/database.ts`
    - Chave: `MONGODB_URI` (via `process.env`)

Observação: Não foram encontrados `appsettings.*.json` com chaves ConnectionStrings para bancos relacionais nas APIs .NET; RabbitMQ e Serilog sim.

## 3) ORM/ODM utilizado

- .NET (APIs e serviços):
  - ODM: `MongoDB.Driver` (oficial) com `MongoDB.Bson`
  - Utilitário: `Sanatana.MongoDb`
  - Padrão de acesso: Repositórios com `MongoDBContext<T>` estático e `GetColection` + `Builders<T>.Filter/Update`
    - Ex.: `Corporate.Contta.Schedule.Infra/Repositories/NfeRepository.cs` (vários `MongoDBContext<T>` estáticos)
  - Uso direto de `MongoClient` em pontos específicos (vide acima)
- Node.js (Search API):
  - ODM: `mongoose`
  - Arquivo: `contta-search-api-main/.../src/config/database.ts`

Não há EF Core, Dapper, NHibernate, System.Data.SqlClient, Npgsql, MySql.Data, Oracle.*.

## 4) DbContext(s) e Migrations

- EF Core: não detectado → não há `DbContext`, `Migrations`, `IEntityTypeConfiguration`, `OnModelCreating`, nem comandos `dotnet ef` aplicáveis.
- Mongo “contexts” observados:
  - `MongoDBContext<T>` (classe presente no projeto Infra; usada como `static` nos repositórios)
  - Exemplos de coleções acessadas: `NFE`, `Produtos`, `EmpresaDest`, `ServicoEntity`, `TbDashboardClientes`, `FaturamentoEmpresa`, etc.

## 5) Comandos úteis (EF Core)

Caso EF Core venha a ser introduzido no futuro, os comandos seriam:

```powershell
# Listar DbContexts (se EF Core estiver presente)
dotnet ef dbcontext list

# Listar migrações (se houver)
dotnet ef migrations list

# Informações do DbContext
dotnet ef dbcontext info
```

No estado atual, esses comandos não se aplicam, pois EF Core não foi encontrado.

## 6) Recomendações imediatas

- Remover URIs hardcoded de `MongoClient` e centralizar em `IOptions<AppSettings>` ou variáveis de ambiente (Render/Vercel/compose).
- Unificar criação de `IMongoClient` via DI (singleton) e injetar `IMongoDatabase`/coleções; evitar `MongoDBContext<T>` estático.
- Padronizar nomes de banco/coleções via configuração; documentar no dossiê operacional.
- Para auditoria: registrar string de conexão em secrets/ENV e não em código; habilitar logs de pool e timeouts.
