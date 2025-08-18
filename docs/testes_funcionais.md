# Caderno de Testes Funcionais (dados fictícios)

Objetivo: validar ponta a ponta os fluxos críticos fiscais/contábeis. Para cada cenário, cite caminho do arquivo, classe, método e âncora/linha quando aplicável. Se não existir, indique onde criar e a interface.

Convenção de referência em código: ver docs/inventario/evidencias-regras.md (âncoras e caminhos).

## 1) Venda varejo PF (ICMS cheio)
- Setup: Empresa do regime normal (ICMS próprio), NF-e 55 com CFOP 5102, consumidor final PF, sem benefícios.
- Entradas:
  - XML NF-e com `Ide.IndFinal=1`, `CFOP=5102`, itens sem ST.
- Passos:
  - Importar via `NfeRepository.CreateXmlNfe(...)` (Infra/Repositories/NfeRepository.cs). Anchor: `public async Task<bool> CreateXmlNfe(`.
  - Conferir bases/totais em `IntegrationXmlMode55.CreateEntidadeMongoNotaFiscal`. Anchor: `nfe = new NFE { BaseCAlIcms = ... }`.
- Esperado:
  - `NFE.BaseCAlIcms` > 0; `NFE.VtIcms` > 0; itens sem `IcmsSt*`.
  - Livro fiscal saída inclui CFOP 5102 (ver `NfeRepository.GetAllNfe(...)`).

## 2) Venda atacado (benefício fiscal)
- Setup: Empresa com benefício de redução de base (UF específica) ou crédito outorgado.
- Entradas: NF-e 55 com CFOP 5101/6101.
- Passos: importar; aplicar benefício (se parametrizado).
- Esperado:
  - Redução de base aplicada no motor (Lacuna). Onde criar: `Domain/Services/Calculo/ICalculoBaseIcms.cs` com regra por UF (parâmetros via `IParametrosFiscaisProvider`).

## 3) Produto monofásico (PIS/COFINS) no Simples Nacional
- Setup: Empresa SN; item NCM monofásico; CSOSN 102.
- Entradas: NF-e com item NCM monofásico.
- Passos:
  - `IntegrationXmlMode55.CriateProdutos(...)` cria flags (atualmente `false`).
  - Processe segregação via `IMonofasicoDetector` (proposto) após importação.
- Esperado:
  - Segregação correta no dashboard SN (`IntegrationTbSimplesNfManual.ValidacaoEntardaSaida`).
  - Reflexo no PGDAS (base monofásica apartada). Interface: `ISimplesCalculator` para efetiva/partilha.

## 4) Entrada com ST + devolução parcial
- Setup: NF-e de entrada com ST (ICMS60/ICMSST), depois devolução parcial (DevolucaoCompra/DevolucaoSaida).
- Entradas: 1 XML com ST, 1 XML de devolução.
- Passos: importar ambos; verificar classificação via `IntegrationXmlMode55.CreateEntidadeMongoNotaFiscal` (âncora Devolucao*).
- Esperado:
  - Itens com `IcmsStCalculationBasis/Value` em `NfeRepository.ObtenhaItems`.
  - Livro fiscal e Bloco E refletem ajustes; `NfeRepository.ObtenhaRegistroE110` atualiza débitos/créditos.

## 5) GO com redução de base/crédito outorgado
- Setup: UF GO; parâmetro de redução/credito outorgado vigente.
- Passos: importar NF-e; aplicar regra de GO.
- Esperado:
  - Base reduzida/crédito outorgado conforme param. Onde criar: `IParametrosFiscaisProvider` + `ICalculoBaseIcms`.
  - Testar com CFOP 5102/6102.

## 6) Presumido x Real — PIS/COFINS e EFD-Contribuições
- Setup: empresas em Lucro Presumido e Real.
- Entradas: vendas/entradas com CSTs típicos.
- Passos: calcular PIS/COFINS conforme regime (Lacuna).
- Esperado:
  - Geração de registros EFD-Contribuições coerentes. Onde criar: serviço de cálculo PIS/COFINS por regime; validar contra modelos em `ConttaContribuicoes`.

## 7) Geração/validação SPED — conciliação NF x SPED
- Setup: período com NF-e diversas.
- Passos: gerar Blocos (usar estruturas `Corporate.Contta.Schedule.SpedContta/ConttaEFD/Bloco*.cs`).
- Esperado:
  - Conciliação: somatório `VL_BC_ICMS_ST` (C176/C170) = `E210.VlBcIcmsSt`. Validador: `ISpedValidator` (proposto).

## 8) Devolução com CFOP de retorno
- Setup: NF-e com CFOP de devolução (ver `GetCfop.GetListaCfopDevolucaoVendas/Compras`).
- Passos: importar devolução; checar livro fiscal.
- Esperado:
  - Classificação correta e impacto no Bloco E (`registroE113` refs de doc.).

## 9) Transporte (CT-e 57) intramunicipal/intermunicipal/interestadual
- Setup: Notas modelo 57.
- Passos: `NfeRepository.GetAllNfeMod57(...)` define tipo de frete por UF/cidade da empresa.
- Esperado:
  - Campos `TipoFrete` corretos; totalizações por modalidade.

## 10) NF de serviço (NFS-e) prestador
- Setup: `InsertNfeServico`/`CreateTbNfeServicoPrestador` com tomador em mesma cidade/diferente/interestadual.
- Passos: processar e validar `ValorContabil` de frete intramunicipal/intermunicipal/interestadual.
- Esperado:
  - Atualização em `TbSimples` via `ValidacaoTbSimplesServico`.

---

Critérios gerais
- Para cada assert, cite arquivo, classe, método e âncora (use evidencias-regras.md).
- Se a regra não existir, indique o local de criação e a interface proposta (acima).
- Dados: usar CNPJs/NSUs fictícios; garantir idempotência com `NfeRepository.NotaJaFoiGravada`.
