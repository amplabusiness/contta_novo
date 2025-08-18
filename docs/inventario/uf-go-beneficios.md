# UF=GO — Reconhecimento e Benefícios

## Reconhecimento de UF
- `IntegrationXmlMode55.GetUf(...)` mapeia `cUF` para siglas, incluindo `52` → `GO`.
- `NFE.UFEnv` é preenchido no Adapter.

## Benefícios/ajustes GO
- Não há condicionais por UF em rotinas de cálculo/apuração do Simples no backend atual.
- Cadastros/flags existem (ImpostosProd, ImpostoReducao/Imune/Antecipacao), mas não aplicados a regras por UF.

## Pontos de extensão sugeridos
- Motor `ISimplesCalculator` aplicar redução de base/crédito outorgado por UF.
- Regras de GO parametrizadas (vigência) em coleção `TaxBenefits` com `uf`, `cfop/ncm`, `tipo`, `percentual`, `effectiveFrom/To`.
- Validação SPED (E110/E111) refletindo ajustes GO.
