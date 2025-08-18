## Motor de Cálculo — Dossiê técnico (estado atual)

Este documento descreve objetivamente o que existe hoje no backend para cálculo fiscal/Simples, quais classes e repositórios participam, como o fluxo está implementado, e as lacunas para evoluir para um motor formal de regras.

### objetivo

- Mapear classes/interfaces envolvidas, padrão usado, fluxo de dados e resultados persistidos.
- Apontar ausência de regras específicas (ex.: GO/benefícios) e propor melhorias seguras.

### principais entidades (Domain)

- Cabeçalho e itens de NF-e
  - `Corporate.Contta.Schedule.Domain/Entities/NfeAgg/Nfe.cs` (NFE): totais e metadados da nota.
  - `Corporate.Contta.Schedule.Domain/Entities/Product/Produtos*.cs`: itens (cliente e fornecedor).
- Impostos e marcações do item
  - `NfeAgg/Impostos.cs`: ICMS, ST, IPI, PIS/COFINS, CST/CSOSN, bases e alíquotas por item/NF.
  - `NfeAgg/ImpostosProd.cs`: flags de enquadramento (IcmsSt, NcmMono, Isento, Imune, Beneficios, IsencaoReducao...).
- Simples Nacional (dash/placeholder)
  - `NfeAgg/CalcularSimples.cs`: classe vazia; contém apenas DTOs Importos* (Csll, Irpj, Cofins, PisPasep, Cpp, Icms) sem lógica.
  - `NfeAgg/TbDashboardClientes.cs`: documento “TbSimples”: `ValorContabil`, `FaturamentoMensais*`, `SimplesNacionalDash`.
- Tabelas de apoio
  - `ImpostoAgg/Cfop.cs`: listas de CFOP por operação (venda/entrada/devolução/...)
  - `ImpostoAgg/CsosnSimples.cs`: lista de CSOSN (namespace está com typo: `Entities.Imporsto`).
  - `ExternalTableAgg/IcmsSt.cs`: tabela CEST/NCM com MVAs.
  - `ImpostoAgg/ImpostoImune.cs`, `ImpostoReducao.cs`, `ImpostoInsento.cs` (classe chama-se `ImpostoAntecipacao`): cadastros de políticas/benefícios.

### repositórios e integração (Infra)

- `Infra/Repositories/NfeRepository.cs` (núcleo do fluxo atual)
  - Ingestão de XML 55: cria `NFE`, `Produtos`, `Impostos` (via adapter) e persiste.
  - Monta visões (NfeT, FullNFE, Livro Fiscal, Bloco E) consultando `ImpostosRepository` e `ProductRepository`.
  - NF manual: após inserir, chama integração do Simples.
- `Infra/Models/TbSimplesNacional/IntegrationTbSimplesNfManual.cs` (motor atual do Simples)
  - Calcula base de cálculo mensal somando itens com CFOP elegíveis (`tbCFOP`).
  - Atualiza documento `TbSimples` do mês (por empresa) com `ValorContabil` (saídas, serviços, fretes, devoluções) e `BaseCalculo`.
  - Serviços (prestador): classifica frete intra/inter/interestadual por UF/município do tomador vs. empresa.
- `Infra/Repositories/ProductRepository.cs`
  - Consulta/atualiza itens; aplica flags fiscais (IcmsSt, NcmMono, Beneficios, Imune/Isento...); filtra por CFOP.
- `Infra/Repositories/ImpostosRepository.cs` + `Domain/Contracts/Repositories/IImpostosRepository.cs`
  - CRUD de `Impostos` por produto/NF/empresa.

### padrão atual (estado)

- Sem Rules Engine/Strategy centralizado para Simples; regras implícitas e acopladas em repositórios e integração.
- `CalcularSimples.cs` e `TaxCalculation.cs` são placeholders.
- Base de cálculo do Simples é derivada por agregação mensal (CFOP + valores de itens) em `IntegrationTbSimplesNfManual`.

### fluxo de dados resumido

1) Ingestão XML (modelo 55): adapter → `NFE` + `Produtos` + `Impostos` → persistência.
2) Visões/relatórios: NfeT/FullNFE/Bloco E montados lendo `Impostos`/`Produtos`.
3) Simples Nacional:
   - NF manual e serviços chamam `IntegrationTbSimplesNfManual` para somar `BaseCalculo` e atualizar `TbSimples` (por mês/empresa).

### regras GO e benefícios

- Regras específicas de Goiás (UF GO) não foram localizadas (nenhum condicional por “GO/Goiás” ou por UF nas fórmulas do Simples).
- Flags e cadastros de benefícios existem, mas não foram aplicados nas fórmulas do Simples na integração atual.

### lacunas e melhorias propostas (seguros)

1) Introduzir um motor do Simples com Strategy/Rules
   - Criar `ISimplesCalculator` e estratégias por Anexo/UF (considerar RBT12, sublimites, partilha, ST/DIFAL e serviços).
   - Inputs: CompanyId, período, RBT12, anexos/atividade, UF/município, flags (benefícios/imune/isento), bases (mercadorias/serviços/fretes).
   - Outputs: base de cálculo efetiva, alíquota efetiva, partilha por tributo (IRPJ, CSLL, PIS, COFINS, CPP, ICMS/ISS) e trilha (trace) de regra.

2) Aplicar benefícios e isenções
   - Consumir `ImpostoReducao`/`ImpostoImune`/`ImpostoAntecipacao` e `ImpostosProd` para ajustar base/aliquota antes do cálculo.

3) Higiene de código/config
   - Corrigir namespace `Entities.Imporsto` → `Entities.ImpostoAgg` (com migração controlada).
   - Externalizar conexões MongoDB hardcoded em `ProductRepository`, `IntegrationTbSimplesNfManual`, `NfeRepository.DebitarValorSimples` para variáveis de ambiente.

4) Qualidade
   - Adicionar testes unitários (happy path + benefícios/isenções + ausência de regras GO) e contratos de entrada/saída.

### contrato sugerido (rascunho)

- Entrada
  - `companyId: Guid`, `periodo: (ano, mes)`, `rbt12: decimal`, `uf: string`, `municipioIbge: string`, `anexo: string`, `bases: { mercadorias, servicos, fretes }`, `flags: { st, difal, beneficios, imune, isento }`.
- Saída
  - `aliquotaEfetiva: decimal`, `baseCalculoEfetiva: decimal`, `valoresPorTributo: { irpj, csll, pis, cofins, cpp, icms/iss }`, `trace: Regra[]`.

### próximos passos

- Criar `ISimplesCalculator` + implementação inicial (Anexo I/mercadorias) usando a base já computada em `TbSimples`.
- Aplicar benefícios simples (redução base/isenção) e registrar `trace` de regra no documento do mês.
- Corrigir namespaces e extrair strings de conexão para configs de ambiente.
