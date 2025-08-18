## G) SPED — visão geral, blocos implementados e conferências cruzadas

Este dossiê inventaria os módulos SPED presentes no repositório e lista os principais blocos/registros já modelados, com âncoras de reconciliação para cruzamentos de NF-e/NFS-e, EFD ICMS/IPI, EFD-Contribuições, ECD e ECF. Também define um checklist de conferências automatizáveis.

### Escopo no código

- EFD ICMS/IPI (Fiscal) — namespace `Contta.SpedFiscal` em `Corporate.Contta.Schedule.SpedContta/ConttaEFD/`
  - Bloco C: Documentos fiscais I – mercadorias
    - `BlocoC.RegistroC100` (capa NF), `RegistroC170` (itens: CFOP, CST_ICMS, bases e tributos), `RegistroC101` (DIFAL/FCP), e demais complementos (C110…C174).
  - Bloco E: Apuração ICMS e IPI
    - `BlocoE.RegistroE110` (apuração ICMS próprias), `E210` (ST), `E310`/`E316` (DIFAL/FCP EC 87/15), `E500`/`E510` (consolidação IPI por CFOP/CST).
  - Bloco G: CIAP (ativo permanente)
    - `BlocoG.RegistroG110` (índice IND_PER_SAI e apropriação), `G125` (bens/componente), `G126` (outros créditos), `G130`/`G140` (docs/itens).
  - Bloco H: Inventário físico
    - `BlocoH.RegistroH005` (totais e motivo), `H010` (itens: quantidade, valor, CST_ICMS), `H020` (complemento ICMS), `H990` (fechamento).

- EFD-Contribuições — namespace `Contta.EfdContribuicoes` em `Corporate.Contta.Schedule.SpedContta/ConttaContribuicoes/`
  - Bloco F (Presumido/Outros docs):
    - `BlocoF.RegistroF100` (outros docs: CST_PIS/COFINS, bases, NAT_BC_CRED), `F500`/`F550` (consolidação por CFOP/conta), `F600` (retenções).
  - Bloco M (Apuração e Créditos Não Cumulativos):
    - `BlocoM.RegistroM100`/`M500` (créditos PIS/COFINS), `M200`/`M600` (totalizações), `M210`/`M610` (apuração por código de contribuição), `M220`/`M620` e `M225`/`M625` (ajustes/detalhes).

- ECD — namespace `Contta.SpedEcd` em `Corporate.Contta.Schedule.SpedContta/ConttaECD/`
  - Bloco 0: identificação e cadastros
    - `Bloco0.Registro0000` (período, CNPJ, UF), `0001`, `0007`, `0020`, `0035`, `0150`, `0180`, `0990`.

- ECF — diretório `Corporate.Contta.Schedule.SpedContta/ConttaECF/`
  - Presença de arquivos: `BlocoJ.cs`, `BlocoK.cs`, `BlocoL.cs`, `BlocoM.cs`, `BlocoN.cs`, `BlocoP.cs`, `BlocoQ.cs`, `BlocoT.cs`, `BlocoU.cs`, `BlocoW.cs`, `BlocoX.cs`, `BlocoY.cs` (todos baseados em `RegistroBaseSped`).

Notas:
- As classes expostas acima derivam de `RegistroBaseSped` e usam o atributo `SpedCampos` para mapear layout e tipos.
- Há mapeamentos auxiliares em `MapBlocos/` e DTOs NFe em `Contex/NfeDTOs/` que ajudam a popular Blocos C, E e G.

## Chaves de reconciliação

- CFOP (Fiscal): `C170.CFOP` e `E510.CFOP`.
- CST ICMS: `C170.CST_ICMS` e `H020.CST_ICMS` (inventário com base/ICMS).
- PIS/COFINS: `C170.CST_PIS/CST_COFINS`, `F100.CST_*`, `M210/M610` (apuração), bases `VL_BC_*` e contribuições `VL_*`.
- Natureza do crédito: `F100.NAT_BC_CRED`.
- Contas contábeis: `C170.COD_CTA`, `E510`/`F500`/`F550`. 

## Conferências cruzadas sugeridas

1) NF-e/NFS-e × EFD ICMS/IPI
- Itens NF (C170): somatórios por CFOP devem conciliar com `E510.VL_CONT_IPI` e base/tributos de IPI; ICMS apura com `E110`.
- DIFAL e FCP: `C101` × `E310/E316` por UF, com validação de `VL_ICMS_UF_DEST`, `VL_ICMS_UF_REM` e FCP.
- Inventário: `H010` totaliza `H005.VL_INV` na data `DT_INV` coerente com o período; CST e bases (`H020`) compatíveis com tributação dos itens.

2) PIS/COFINS × EFD-Contribuições
- Bases e contribuições: somatórios de `C170.VL_BC_PIS/COFINS` e `VL_PIS/COFINS` conciliam com `M210/M610.VL_BC_CONT` e `VL_CONT_PER`.
- Créditos não cumulativos: registros `M100/M500` (VL_BC, ALIQ, VL_CRED) devem ter lastro em `F100` via `NAT_BC_CRED` e contas (`COD_CTA`).
- Presumido (regime de caixa): `F500/F550` por CFOP/conta conciliam com recebimentos contábeis (contas de receita) e com totalizações `M200/M600`.

3) CIAP (ativo imobilizado)
- Índice `G110.IND_PER_SAI` = `G110.VL_TRIB_EXP / G110.VL_TOTAL`; checar replicação em `G126` por período.
- Apropriação mensal: `G110.ICMS_APROP` = `G110.SOM_PARC × IND_PER_SAI` e soma de `G126.VL_PARC_APROP` no período.

4) Amarrações contábeis (ECD/ECF)
- Períodos: `ECD 0000.DT_INI/DT_FIN` = períodos EFD; CNPJ/UF alinhados.
- Contas: `COD_CTA` em EFD Fiscal/Contribuições devem existir no plano (ECD I050/I155 — a implementar/usar nos blocos ECD correspondentes).
- ECF: blocos J/K/L/N/P/T/U/W/X/Y para apurações e demonstrações devem fechar com totalizações de EFDs (presença confirmada por arquivos).

## Checklist de validações automatizáveis

- CFOP
  - Somatórios por CFOP de `C170.VL_ITEM` vs `E510.VL_CONT_IPI` (mesma CFOP/CST_IPI).
  - Entradas/saídas por CFOP e CST_ICMS batem com `E110` (débito/crédito/estornos/ajustes).

- PIS/COFINS
  - `Σ C170.VL_BC_PIS` = `Σ M210.VL_BC_CONT` (cód. contribuição correlato) e idem para COFINS (`M610`).
  - Créditos `M100/M500` têm NAT_BC_CRED válido em `F100` e alíquota/base coerentes com CST.

- Inventário e CIAP
  - `Σ H010.VL_ITEM` = `H005.VL_INV` no `DT_INV` correto; CST/BC/ICMS compatíveis (`H020`).
  - `G110`, `G126` fecham os cálculos de apropriação com variações de saídas e exportação.

- DIFAL/FCP
  - `C101` × `E310/E316` por UF: diferenças tolerância zero após ajustes declaratórios.

- Contas contábeis
  - `COD_CTA` referenciados em blocos E/F/M existem no plano (ECD) e possuem saldo compatível nas demonstrações (ECF).

## Referências diretas aos arquivos

- EFD Fiscal: `ConttaEFD/BlocoC.cs`, `ConttaEFD/BlocoE.cs`, `ConttaEFD/BlocoG.cs`, `ConttaEFD/BlocoH.cs`.
- EFD-Contribuições: `ConttaContribuicoes/BlocoF.cs`, `ConttaContribuicoes/BlocoM.cs`.
- ECD: `ConttaECD/Bloco0.cs`.
- ECF: diretório `ConttaECF/` com `BlocoJ.cs`, `BlocoK.cs`, `BlocoL.cs`, `BlocoM.cs`, `BlocoN.cs`, `BlocoP.cs`, `BlocoQ.cs`, `BlocoT.cs`, `BlocoU.cs`, `BlocoW.cs`, `BlocoX.cs`, `BlocoY.cs`.

## Integração com documentos B–F

- B) Importação NF-e: fornece origem para `BlocoC` e bases PIS/COFINS dos itens.
- C) Tabelas tributárias: sustentam CFOP/CST, NAT_BC_CRED e repartições, com versionamento a ser persistido.
- D) Base de cálculo: critérios para `VL_BC_*` e rateios (descontos, frete, seguro, despesas, ST, IPI) refletidos em C170/E510/F500/F550.
- E) Simples Nacional: quando aplicável, conciliar faturamento RBT12 e partilha com ausência/presença de EFD-Contribuições; ISS (serviços) tratado fora de EFD ICMS.
- F) Presumido/Real: Bloco F (regime de caixa) e Bloco M (não cumulativo) são a espinha dorsal das apurações, com cross-check contra CFOP/CST e ECD/ECF.

## Próximos passos

- Implementar os validadores que varrem os registros acima e geram um relatório de divergências por período/CNPJ.
- Persistir e versionar tabelas externas (CFOP/CST, NAT_BC_CRED, anexos Simples) e parametrizações por UF/regime.
- Adicionar testes unitários para os cálculos de `E110`, `M210/M610`, `G110/G126` e reconciliações CFOP.
