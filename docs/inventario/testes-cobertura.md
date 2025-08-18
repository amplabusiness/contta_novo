# Cobertura de Testes — Situação e Gaps

## Existente
- `Corporate.Contta.Schedule.UnitTest/InformationRepositoryTest.cs` — placeholder, sem asserts.

## Ausente (prioritário criar)
- Simples Nacional:
  - Alíquota efetiva e partilha por anexo (faixa/deduzir), RBT12 rolling, Fator R.
  - Bases por CFOP (mercadorias/serviços) e benefícios/isenções.
- ICMS ST e Monofásicos:
  - Decisão por NCM/CEST, recolhimento/credito, complementos.
- SPED (Bloco E):
  - Cruzamentos C170→E510/E110/E111/E116, ajustes e saldos.

## Estratégia
- Introduzir `ISimplesCalculator` com contrato testável; mocks para tabelas (anexos/benefícios).
- Fixtures de XML para pipeline Adapter→Repo e asserts em coleções (NFE/Produtos/Impostos).
- Validadores SPED com casos felizes e bordas (nota cancelada, devolução, ST).
