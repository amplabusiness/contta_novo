# Modo Investigador — Q&A

Mensagens curtas para localizar e entender o código.

## 1) class *Calculation*
- Arquivo: `Corporate.Contta.Schedule.Domain/Entities/FullNfeAgg/TaxCalculation.cs`
  - Papel: DTO para exibir cálculo tributário na FullNFE. Não contém lógica.

## 2) Alíquota efetiva do Simples
- Não implementada no código.
- Pontos:
  - `Domain/Entities/NfeAgg/CalcularSimples.cs` — vazio (local ideal para motor).
  - `Infra/Models/TbSimplesNacional/IntegrationTbSimplesNfManual.cs` — agrega base mensal (não calcula alíquota/partilha).
  - `Infra/Tools/CrawlerAliquota.cs` + `Domain/Entities/NfeAgg/TabelaExterna.cs` — fonte de faixas, alíquota nominal e parcela a deduzir.
- Assinatura sugerida: `SimplesResultado CalcularAliquotaEfetiva(Guid companyId, int ano, int mes, decimal rbt12, string anexo, string uf, decimal baseCalculoMes)`

## 3) Fluxo de importação NF-e (XML → entidades)
- Consumer → Adapter → Repositório.
- Consumer: `ConsumerXml/Program.cs` (RabbitMQ, MessageParser.ParseNfeProc)
- Adapter: `Infra/Models/Adapter/IntegrationXmlMode55.cs` (NFE/Produtos/Impostos)
- Repo: `Infra/Repositories/NfeRepository.cs` (CreateXmlNfe, Insert*, _impostos.Insert)
- Diagrama: ver `docs/nfe-importacao-xml.md` (Mermaid).

## 4) Tabelas de impostos (carregamento, cache, invalidação)
- CFOP/CSOSN: listas estáticas em código (`ImpostoAgg/Cfop.cs`, `ImpostoAgg/CsosnSimples.cs`).
- Anexos do Simples: scraping on-demand (`CrawlerAliquota.cs`) → `TabelaExterna` em memória.
- NCM: `CrawlerConsultaNcm.cs` → lista em memória.
- Versionamento/cache: inexistente (sugerir persistência + vigência + cache com invalidação).

## 5) Pontos que usam CFOP e NCM
- CFOP: `NfeRepository` (livros/ajustes) e `IntegrationTbSimplesNfManual` (base do Simples). NCM: apenas armazenado; sem regra decisória ativa.

## 6) UF=GO e benefícios/ajustes
- UF reconhecida no Adapter (cUF "52" → "GO").
- Benefícios (redução de base, crédito outorgado, Protege): não localizados na lógica atual.

## 7) Testes automatizados
- Encontrado: `Corporate.Contta.Schedule.UnitTest/InformationRepositoryTest.cs` (placeholder).
- Ausentes: base/partilha Simples, ST, monofásicos, regras por UF/benefícios, validadores SPED.
