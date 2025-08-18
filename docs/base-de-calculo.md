## D) Base de Cálculo — produto x serviço

Checklist de cobertura:
- Base por operação (produto/serviço) — Done
- Considera descontos, frete, seguro, outras despesas, ST, IPI — Done
- Diferenças por regime e por UF — Done
- Método(s) responsável(is) e teste associado — Done
- O que faz o crawler-econet e os outros “econet” — Done

### Produto (NF-e Mod 55)

Há dois "tipos" de base usados no sistema:

1) Base fiscal (ICMS) vinda do XML
- Campo: `NFE.BaseCAlIcms` (por nota) e `Impostos.BCIcms` (por item)
- Origem: `total.ICMSTot.vBC` e ICMS do item no XML
- Código: `EntidadeXmlToEntidadeMongodbMod55.CreateEntidadeMongoNotaFiscal()` popula `BaseCAlIcms` e `BaseCalIcmsSt` diretamente do XML
- Observação: o sistema não reprocessa a fórmula do vBC; confia no valor do XML (que, por regra da NF-e, considera vProd, vDesc, vFrete, vSeg, vOutro e, em alguns casos, IPI)

2) Base para apuração do Simples (TbSimples)
- Cálculo: soma dos itens elegíveis por CFOP
  - Fórmula efetiva no código: `base += item.VlProduto - item.VlTlDesconto`
  - Onde: `IntegrationTbSimplesNfManual.CreateTbNfeSaidaManual()` + `ValidacaoEntardaSaida()`
  - Como os campos são preenchidos:
    - XML: `CriateProdutos()` define `VlProduto = prod.vProd`, `VlTlDesconto = prod.vDesc`, `VlTlFrete = prod.vFrete`, `VlTlSeguro = prod.vSeg`, `OutrasDespesas = prod.vOutro`
    - Manual: `NfeRepository.InsertNfe()` define `item.VlProduto = (Quantidade*VlUnitario) - VlTlDesconto - OutrasDespesas`
- Inclusões/Exclusões nesta base (Simples):
  - Descontos: SIM (subtraídos)
  - Frete: NÃO (ignorado na soma — não é adicionado)
  - Seguro: NÃO (ignorado)
  - Outras despesas: NÃO (ignorado; no fluxo manual ainda é subtraído antes, ver Observação)
  - ST: NÃO (não compõe a base do Simples)
  - IPI: NÃO (não compõe a base do Simples)
- Observação importante (fluxo manual): o código subtrai `VlTlDesconto` dentro de `VlProduto` e novamente no somatório (`VlProduto - VlTlDesconto`), causando desconto em dobro se `VlTlDesconto > 0`.

### Serviço (NFS-e/serviço manual)
- Base utilizada: valor líquido do demonstrativo
  - Fórmula: `LiquidValue = ServicesValue - UnconditionalDiscount`
  - Onde é gravado: `NfeRepository.AddNfeServico()` (`BaseCalculo = nfServico.Demonstrative.LiquidValue`)
  - Onde é somado para o Simples: `IntegrationTbSimplesNfManual.CreateTbNfeServicoPrestador()` e `ValidacaoTbSimplesServico()` somam `valorSaida` (o líquido) em `ValorContabil.BaseCalculo`
- Inclusões/Exclusões:
  - Descontos incondicionados: SIM (reduzem a base)
  - Frete/Seguro/Outras: NÃO aplicáveis no demonstrativo usado
  - ISS/ST/IPI: não entram na base de faturamento do Simples no código atual; apenas valores de serviço
- Observação: há classificação do frete (intra/intermunicipal/interestadual) para fins de indicadores em `ValorContabil`, mas isso não altera a base do serviço.

### Diferenças por regime e por UF
- Regime tributário (CRT, monofásico etc.): não há lógica que altere a base conforme regime; flags (ex.: `NcmMono`, `IcmsSt`, `Beneficios`) não são usadas para recalcular base.
- UF: não há variação da fórmula por UF. A UF é armazenada (`NFE.UFEnv`) e usada apenas em classificações de frete/relatórios.

### Métodos responsáveis (com caminhos)
- Produto (Simples/apuração):
  - `Corporate.Contta.Schedule.Api.Extension.IntegrationTbSimplesNfManual.CreateTbNfeSaidaManual`
  - `Corporate.Contta.Schedule.Api.Extension.IntegrationTbSimplesNfManual.ValidacaoEntardaSaida`
- Serviço (Simples/apuração):
  - `Corporate.Contta.Schedule.Api.Extension.IntegrationTbSimplesNfManual.CreateTbNfeServicoPrestador`
  - `Corporate.Contta.Schedule.Api.Extension.IntegrationTbSimplesNfManual.ValidacaoTbSimplesServico`
- Mapeamento do XML para totais/base ICMS:
  - `Corporate.Contta.Schedule.Infra.Models.Adapter.EntidadeXmlToEntidadeMongodbMod55.CreateEntidadeMongoNotaFiscal`
  - Itens: `CriateProdutos` (vProd, vDesc, vFrete, vSeg, vOutro)
- Fluxo NF-e manual (onde pode haver desconto em dobro):
  - `Corporate.Contta.Schedule.Infra.Repositories.NfeRepository.InsertNfe`

### Teste associado
- Não há testes unitários que validem o cálculo de base. O projeto de teste presente (`Corporate.Contta.Schedule.UnitTest/InformationRepositoryTest.cs`) está vazio para este tema.

### crawler-econet e outros “econet”
- `crawler-econet-main/`: projeto placeholder (README genérico). Não há crawler implementado neste workspace.
- `robo-eco-master/RoboEconet`: robô .NET para estruturar e persistir dados fiscais (ex.: PIS/COFINS por NCM) vindos do portal Econet:
  - DTOs: `RoboEconet/Models/Dto/*` (NCM, ICMS, PIS/COFINS, CST, EFD, observações)
  - Adapter: `Infra/Adapter/EntidadePisConfinsToEntidadeMongodb` mapeia dados raspados para documentos Mongo
  - Persistência: `Infra/Data/Repositorios/PisConfinsRepository.Create()` grava coleções de PIS/COFINS por NCM
  - Uso esperado: abastecer tabelas auxiliares (PIS/COFINS, possivelmente ICMS) para consulta nas apurações

### Pontos de atenção e melhorias
- Corrigir fórmula no fluxo manual para não subtrair desconto duas vezes.
- Considerar frete/seguro/“vOutro” conforme regras (ex.: compor ou não a base do Simples de acordo com interpretação/CFOP).
- Criar testes unitários mínimos para: produto (XML e manual) e serviço, cobrindo descontos/frete/seguro/outros.
- Se necessário, usar as tabelas do RoboEconet (PIS/COFINS, ICMS/CEST/MVA) para ajustar bases por regime/UF.
