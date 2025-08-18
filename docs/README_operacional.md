# README Operacional — Apuração Mensal e Relatórios

Este guia resume como executar a apuração mês a mês, gerar relatórios e validar SPED a partir dos componentes existentes.

## Entrada: NF-e/NFS-e
- Importação NF-e 55: `Corporate.Contta.Schedule.Infra/Repositories/NfeRepository.cs`
  - Método: `CreateXmlNfe(NfeProc nota)` — idempotência por `CodBarra`.
- Importação manual (venda/serviço): `Insert(NfVendaManual ...)`, `InsertNfeServico(...)`.

## Bases e agregações
- Mapas NF-e → entidade: `IntegrationXmlMode55.CreateEntidadeMongoNotaFiscal` (VBC, VBCST, VNF, VProd, VDesc, VFrete, VIPI, VPIS, VCOFINS).
- Agregação Simples: `IntegrationTbSimplesNfManual.ValidacaoEntardaSaida` e `ValidacaoTbSimplesServico` → coleção `TbSimples`.

## Livros fiscais (visão CFOP)
- `NfeRepository.GetAllNfe(empresaId, data, "Venda"|"Entrada")` → `LivroFiscal` e rodapés por CFOP.

## Apuração ICMS/IPI (Bloco E — EFD ICMS/IPI)
- `NfeRepository.GetRegistrosBlE(companyId, competencia)`
  - Monta `RegistroE110/E111/E113/E115/E116` para apuração.

## SPED — Estruturas
- Projeto: `Corporate.Contta.Schedule.SpedContta`
  - EFD ICMS/IPI: `ConttaEFD/BlocoC.cs`, `BlocoE.cs`, `Bloco1.cs`, `BlocoH.cs`.

## Validação mínima recomendada
- Conciliação ST: somatório `VL_BC_ICMS_ST` (C170/C176) = `E210.VlBcIcmsSt`.
- Inventário H005 coerente com mudanças de regime (MOT_INV=04).
- Perfis e blocos obrigatórios presentes.

## Parâmetros e regras por UF
- Atual: `IntegrationXmlMode55.GetUf` (mapeamento CUF→UF fixo).
- Próximo: externalizar em `IParametrosFiscaisProvider` (por UF/CNAE e vigência) e usar em serviços de cálculo.

## Execução mensal (sugestão)
1) Ingerir NF-e/NFS-e do mês.
2) Rodar agregações (Simples): validar `TbSimples`.
3) Gerar livros via `GetAllNfe` e revisar totais por CFOP.
4) Gerar apuração (Bloco E) via `GetRegistrosBlE`.
5) Gerar/validar SPED com `SpedContta` (validadores a serem adicionados: `ISpedValidator`).

## Próximos incrementos (gap-closing)
- Implementar `ISimplesCalculator` (RBT12/anexo/efetiva/partilha) + tabelas versionadas.
- Introduzir `ICalculoBaseIcms` (regras por UF/CFOP/regime) parametrizadas por vigência.
- Adicionar `ISpedValidator` com conciliações básicas e regras por perfil.

## Troubleshooting
- Idempotência: `NfeRepository.NotaJaFoiGravada(chave)`.
- Segredos: remover URIs hardcoded e usar variáveis de ambiente.
