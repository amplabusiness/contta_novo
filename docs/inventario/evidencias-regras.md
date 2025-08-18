# Inventário de Regras Críticas — Evidências em Código (arquivo, classe, método e linha)

Nota sobre linhas: o workspace contém duplicatas de projetos (com e sem “(1)”) e o provedor VFS pode deslocar numeração. Para cada regra, cito caminho, classe e método e adiciono âncoras de código estáveis. Onde a regra não existe, indico onde criar e proponho a interface. Se você quiser, no próximo passo eu adiciono comentários `// anchor:` no código e fixo as linhas exatas.

## 1) Tipo de operação da NF-e (Venda, Entrada, Devolução)

- Arquivo: `agendador-back-end-master/agendador-back-end-master/Corporate.Contta.Schedule.Infra/Models/Adapter/IntegrationXmlMode55.cs`
  - Classe: `Corporate.Contta.Schedule.Infra.Models.Adapter.IntegrationXmlMode55`
  - Método: `CreateEntidadeMongoNotaFiscal(NfeProc notaFiscalEletronicaMod55)`
  - Linha (use âncoras): buscar por `tipoNfe = "Venda";` e por `ModeloNota`/`NatOp`.
  - Evidência: define `ModeloTipo` com base em `ModeloNota` e natureza da operação (NatOp), distinguindo “Venda”, “Entrada”, “DevolucaoSaida”, “DevolucaoCompra”.
  - Âncoras:
    - `else if (notaFiscalEletronicaMod55.ModeloNota == "Saida") tipoNfe = "Venda";`
    - `else if (notaFiscalEletronicaMod55.ModeloNota == "Entrada") tipoNfe = "Entrada";`
    - `else if (tranferenciaMercadoria.Contains("Devolucao") ... ) tipoNfe = "DevolucaoSaida"; else tipoNfe = "DevolucaoCompra";`

- Arquivo: `agendador-back-end-master/agendador-back-end-master/Corporate.Contta.Schedule.Infra/Repositories/NfeRepository.cs`
  - Classe: `Corporate.Contta.Schedule.Infra.Repositories.NfeRepository`
  - Método: `CreateXmlNfe(Domain.Entities.ModeloXml.NotaFiscalEletronicaMod55.NfeProc nota)`
  - Linha (use âncoras): `public async Task<bool> CreateXmlNfe(` e `nota.ModeloNota = "Saida";`/`"Entrada"`.
  - Evidência: determina “Saida” x “Entrada” e encaminha persistência de produtos conforme o tipo.

## 2) Base de cálculo (ICMS e totais de NF)

- Arquivo: `.../Infra/Models/Adapter/IntegrationXmlMode55.cs`
  - Classe: `IntegrationXmlMode55`
  - Método: `CreateEntidadeMongoNotaFiscal`
  - Linha (use âncoras): `nfe = new NFE { ... }` com mapeamentos de `Total.ICMSTot` → `VBC`, `VBCST`, `VNF`, `VProd`, `VDesc`, `VFrete`, `VSeg`, `VIPI`, `VPIS`, `VCOFINS`.
  - Evidência: composição direta de campos de base e totais da NF-e.

- Arquivo: `.../Infra/Repositories/NfeRepository.cs`
  - Classe: `NfeRepository`
  - Método: `GetBaseCalculo(Guid companyId, DateTime dateClose)`
  - Linha (use âncora): `public async Task<TotalNfe> GetBaseCalculo(`.
  - Evidência: soma `BaseCAlIcms`, `VtTotalNfe`, `VlAproxTributos` por empresa.

- Arquivo: `.../Infra/Repositories/NfeRepository.cs`
  - Classe: `NfeRepository`
  - Métodos: `ObtenhaItems(...)`, `ObtenhaItemsFornec(...)`, `ObtenhaAnalytical(...)`
  - Linha (use âncoras): `IcmsStCalculationBasis`, `IcmsStAliquot`, `IcmsStValue`, `IpiValue`.
  - Evidência: mapeamento de base/aliquota/valor de ICMS ST e IPI por item e visão analítica.

- Lacuna e proposta:
  - Não há serviço formal de base de cálculo com regras condicionais (ex.: inclusão de IPI na base por UF/CFOP/regime).
  - Criar `Corporate.Contta.Schedule.Domain/Services/Calculo/ICalculoBaseIcms.cs`:
    - `decimal CalcularBaseIcms(NFE nfe, Produtos item, ContextoFiscal ctx);`
    - `decimal CalcularBaseIcmsSt(NFE nfe, Produtos item, ContextoFiscal ctx);`
    - `ContextoFiscal` com CFOP, UFs, regime, flags e vigência.

## 3) Segregação Simples: monofásico, ST, isento/benefício

- Arquivo: `.../Infra/Models/Adapter/IntegrationXmlMode55.cs`
  - Classe: `IntegrationXmlMode55`
  - Método: `CriateProdutos(List<Det> dets, DateTime? DhEmt)`
  - Linha (use âncoras): `produtos.NcmMono = false;`, `produtos.IcmsSt = false;`, `produtos.Beneficios = false;`, `produtos.Isento = false;`.
  - Evidência: flags existem, mas não há lógica de detecção.

- Proposta (onde criar):
  - `Domain/Services/Simples/IMonofasicoDetector.cs`
    - `bool EhMonofasico(Produtos item, string ncm, string cest, string uf, DateTime data);`
  - `Domain/Services/ICmsSt/IStDetector.cs`
    - `bool TemSt(Produtos item, NFE nfe, ContextoFiscal ctx, out decimal baseSt, out decimal valorSt);`
  - `Domain/Services/Beneficios/IBeneficioFiscalDetector.cs`
    - `BeneficioFiscal Detectar(Produtos item, NFE nfe, ContextoFiscal ctx);`
  - Integrar logo após `CriateProdutos` no pipeline de importação.

## 4) Simples Nacional: RBT12, anexo, alíquota nominal x efetiva, partilha (DAS)

- Arquivo esperado (ausente): `Corporate.Contta.Schedule.Domain/Entities/TbSimplesNacional/TabelaExterna.cs`
  - Evidência de ausência: não localizado; não há motor do Simples com tabelas versionadas/vigência.

- Agregação mensal existente:
  - Arquivo: `.../Infra/Models/TbSimplesNacional/IntegrationTbSimplesNfManual.cs`
    - Métodos: `CreateTbNfeSaidaManual`, `CreateTbNfeServicoPrestador`, `ValidacaoEntardaSaida`, `ValidacaoTbSimplesServico`.
    - Linha (use âncoras): `IMongoClient mongoClient = new MongoClient("mongodb://contta:contta123456@`, `ValorContabil`, `BaseCalculo`.
    - Evidência: atualiza `TbSimples` (base e valores), sem cálculo de alíquota efetiva/partilha.

- Proposta (onde criar):
  - `Domain/Services/Simples/ISimplesCalculator.cs`
    - `AnexoSimples DeterminarAnexo(string cnae, ProdutoServicoTipo tipo, bool possuiSubAnexo, DateTime competencia);`
    - `decimal CalcularRbt12(Guid companyId, DateTime competencia);`
    - `AliquotaNominalEfetiva CalcularAliquotas(decimal rbt12, AnexoSimples anexo, decimal fatorR);`
    - `DASPartilha CalcularPartilha(AliquotaEfetiva efetiva, UF uf, DateTime comp);`
  - `Domain/Services/Simples/ITabelaAnexoProvider.cs`
    - `TabelaAnexo ObterTabela(AnexoSimples anexo, DateTime competencia);` (com vigência/cache)
  - Persistência: coleção `SimplesAnexos` com (Anexo, Faixa, Receita, Aliquota, Deduzir, VigenciaInicio/Fim).

## 5) CFOP (listas e uso)

- Arquivo: `.../Domain/Entities/Imporsto/GetCfop.cs`
  - Classe: `GetCfop`
  - Métodos: `GetListaCfopVenda()`, `GetListaCfopEntradas()`, `GetListaCfopDevolucaoVendas()`, etc.
  - Linha: início de cada método com `List<double> cfop = new List<double> { ... }`.

- Arquivo: `.../Infra/Repositories/NfeRepository.cs`
  - Métodos: `GetAllNfe(...)`, `GetAllNfe(Guid empresaId, ...)` (livro fiscal)
  - Linha (use âncoras): `var listaCfopVenda = getCfop.GetListaCfopVenda();` e criação de `LivroFiscal`.

## 6) SPED: geração e vínculos

- Arquivo: `.../Infra/Repositories/NfeRepository.cs`
  - Métodos: `GetRegistrosBlE`, `ObtenhaRegistroBlE`, `ObtenhaRegistroE110`, `ObtenhaRegistroE111`, `ObtenhaRegistroE113`, `ObtenhaRegistroE115`, `ObtenhaRegistroE116`.
  - Linha (use âncoras): `RegistroE110`, `valorIcmsRecolher`, `registroBlE.registroE100`.

- Estruturas SPED:
  - Pastas: `.../Corporate.Contta.Schedule.SpedContta/ConttaEFD/Bloco*.cs` (ex.: `BlocoC.cs`, `BlocoE.cs`, `Bloco1.cs`, `BlocoH.cs`).

- Proposta (onde criar):
  - `Domain/Services/Sped/ISpedValidator.cs` para conciliações de ST e obrigatoriedades por perfil.

## 7) Parâmetros UF/segmento e vigência/flags

- Arquivo: `.../Infra/Models/Adapter/IntegrationXmlMode55.cs`
  - Método: `GetUf(...)`
  - Linha (use âncora): `if (notaFiscalEletronicaMod55.NFe.InfNFe.Ide.CUF == "52") uf = "GO";`.

- Proposta (onde criar):
  - `Domain/Configuration/IParametrosFiscaisProvider.cs` e arquivo/coleção versionada por UF/CNAE com vigência.
  - `IFeatureFlagService` para alternar regras por competência.

## 8) Login/Token — atual x Keycloak OIDC + PKCE

- Evidências:
  - Utilitário: `Corporate.Contta.Schedule.Infra/Tools/ConvertTokenId.cs` (indício de manipulação de token atual).

- Proposta (onde criar):
  - `Corporate.Contta.Schedule.Api/Auth/IAuthService.cs`
    - `Task<TokenValidationResult> ValidateOidcTokenAsync(string jwt);`
    - `Task<string> ExchangeCodeForTokenAsync(string code, string codeVerifier);`
  - Configurar `AddOpenIdConnect`/`AddJwtBearer` com Keycloak e PKCE.

## 9) Jobs/Workers — atualização, logs, rollback

- Arquivo: `.../Infra/Models/TbSimplesNacional/IntegrationTbSimplesNfManual.cs`
  - Métodos: `CreateTbNfeSaidaManual`, `CreateTbNfeServicoPrestador`, `ValidacaoEntardaSaida`, `ValidacaoTbSimplesServico`.
  - Evidência: aplicam mutações mensais em `TbSimples`; faltam logs estruturados e compensação.

- Proposta (onde criar):
  - `ISimplesDashboardUpdater` com operações idempotentes; logs Serilog; `ChangeLog` para rollback.

## 10) Segredos em código — remover e parametrizar

- Arquivos: `.../Infra/Repositories/NfeRepository.cs` (mét. `DebitarValorSimples`) e `.../Infra/Models/TbSimplesNacional/IntegrationTbSimplesNfManual.cs`
  - Linha (use âncora): `new MongoClient("mongodb://contta:contta123456@`.
  - Proposta: usar `appsettings` + secrets; `IMongoSettingsProvider` + `IOptions`.

---

## Interfaces propostas (contratos)

- `Domain/Services/Calculo/ICalculoBaseIcms.cs`
  - `decimal CalcularBaseIcms(NFE nfe, Produtos item, ContextoFiscal ctx);`
  - `decimal CalcularBaseIcmsSt(NFE nfe, Produtos item, ContextoFiscal ctx);`

- `Domain/Services/Simples/ISimplesCalculator.cs`
  - `decimal CalcularRbt12(Guid companyId, DateTime competencia);`
  - `AnexoSimples DeterminarAnexo(string cnae, ProdutoServicoTipo tipo, bool subAnexo, DateTime competencia);`
  - `AliquotaNominalEfetiva CalcularAliquotas(decimal rbt12, AnexoSimples anexo, decimal fatorR);`
  - `DASPartilha CalcularPartilha(AliquotaEfetiva efetiva, UF uf, DateTime competencia);`

- `Domain/Services/Simples/ITabelaAnexoProvider.cs`
  - `TabelaAnexo ObterTabela(AnexoSimples anexo, DateTime competencia);` (com vigência)

- `Domain/Services/Simples/IMonofasicoDetector.cs`
  - `bool EhMonofasico(Produtos item, string ncm, string cest, string uf, DateTime data);`

- `Domain/Services/ICmsSt/IStDetector.cs`
  - `bool TemSt(Produtos item, NFE nfe, ContextoFiscal ctx, out decimal baseSt, out decimal valorSt);`

- `Domain/Services/Beneficios/IBeneficioFiscalDetector.cs`
  - `BeneficioFiscal Detectar(Produtos item, NFE nfe, ContextoFiscal ctx);`

- `Domain/Services/Sped/ISpedValidator.cs`
  - `IEnumerable<SpedAlert> ValidarEfd(EfdDocumento doc);`

- `Api/Auth/IAuthService.cs`
  - `Task<TokenValidationResult> ValidateOidcTokenAsync(string jwt);`
  - `Task<string> ExchangeCodeForTokenAsync(string code, string codeVerifier);`

- `Domain/Configuration/IParametrosFiscaisProvider.cs`
  - `ParametrosFiscais Obter(string uf, string cnae, DateTime competencia);`

---

## Casos de teste mínimos (onde criar)

- Projeto: `agendador-back-end-master/.../Corporate.Contta.Schedule.UnitTest/`
  - `IntegrationXmlMode55Tests.OperationType_Should_Classify_Devolucao`
    - Alvo: `IntegrationXmlMode55.CreateEntidadeMongoNotaFiscal` (âncora “DevolucaoSaida/Compra”).
  - `BaseCalculoTests.Should_Compose_Totals_From_ICMSTot`
    - Alvo: atribuições de `VProd`, `VDesc`, `VFrete`, `VSeg`, `VIPI`.
  - `StMappingTests.Should_Map_IcmsSt_Basis_And_Value_On_Items`
    - Alvo: `ObtenhaItems`/`ObtenhaAnalytical` em `NfeRepository`.
  - `SimplesAggregationTests.Should_Update_BaseCalculo_On_Saida`
    - Alvo: `IntegrationTbSimplesNfManual.ValidacaoEntardaSaida`.
  - `SecretsTests.Should_Not_Contain_Hardcoded_Mongo_Uri`
    - Varre arquivos por `mongodb://` e falha se encontrado.

---

## Quando a IA “travar”

Não responda genericamente. Cite caminho do arquivo, classe, método e linha/âncora. Se não existir, sugira onde criar e qual interface implementar. Produza snippet curto, não wall of text.

---

Observação: Se desejar linha absoluta agora, eu insiro comentários `// anchor:` nos pontos-chave e retorno com os números exatos na sequência.
