# F) Lucro Presumido e Lucro Real — bases, CFOP e validações SPED

Este capítulo localiza o que existe no código para Presumido/Real e descreve como devem ser apuradas as bases no Presumido (percentuais por atividade) e no Real (PIS/COFINS não cumulativos, créditos, CIAP, inventário), com amarrações a CFOP e validações com SPED.

## Cobertura e status
- [x] SPED Fiscal: Blocos C/E/G/H presentes (ICMS/IPI, CIAP, Inventário)
- [x] EFD Contribuições: Bloco M presente (apuração PIS/COFINS cumulativo e não cumulativo) e Bloco F (Presumido por caixa e detalhamentos)
- [ ] Motor de cálculo para Lucro Presumido (percentuais por atividade) — não encontrado
- [ ] Motor PIS/COFINS não cumulativos (bases e créditos) — não encontrado; apenas estruturas SPED
- [ ] Regras de vínculo CFOP→natureza (cumulativo/não cumulativo) e crédito — não encontrado

---

## Onde está no código
- EFD Contribuições — Apuração e créditos (Lucro Real): `Corporate.Contta.Schedule.SpedContta/ConttaContribuicoes/BlocoM.cs`
  - M100/M500: créditos de PIS e COFINS (base do crédito, alíquota, valor, ajustes, diferimentos, saldo)
  - M200/M600: totais da contribuição apurada, créditos utilizados e saldos
  - M210/M610: apuração por código de contribuição (base, alíquota, contribuições e ajustes)
  - M225/M625: detalhamento de ajustes e vinculação a CST/conta contábil
- EFD Contribuições — Presumido (cumulativo, por caixa/competência): `ConttaContribuicoes/BlocoF.cs`
  - F500/F550: consolidação de receitas por CST, com campos `CFOP`, base e alíquota; indicam como amarrar receitas por CFOP
  - F100: “demais documentos” com `NAT_BC_CRED`/`IND_ORIG_CRED` (base para créditos quando aplicável)
  - F600: retenções na fonte (PIS/COFINS)
- SPED Fiscal — Inventário: `ConttaEFD/BlocoH.cs`
  - H001/H005/H010/H020: data, valor total, itens do inventário e base/ICMS por item
- SPED Fiscal — CIAP (ICMS): `ConttaEFD/BlocoG.cs`
  - G110/G126: apropriação mensal e outros créditos de ICMS de ativo permanente (índice de participação `IND_PER_SAI`)
- SPED Fiscal — Apuração ICMS/IPI e CFOP por IPI: `ConttaEFD/BlocoE.cs` (E110, E210, E310, E510, E520)
  - E510 inclui `Cfop` e CST_IPI para consolidação por CFOP no IPI

Observação: estes arquivos representam os registros SPED (modelo de dados/serialização). Não há um motor que alimente/apure automaticamente Lucro Presumido/Real.

---

## Lucro Presumido — bases por atividade (IRPJ/CSLL) [lacuna]
- Esperado: percentuais sobre receita bruta por atividade (exemplos usuais — IRPJ: 8% comércio, 12% indústria, 32% serviços; CSLL: 12%/32% etc., com exceções), por CNAE/atividade.
- Situação atual: não há tabela/serviço implementado com percentuais por atividade nem cálculo da base de IRPJ/CSLL do Presumido.
- Entradas típicas:
  - Receitas por natureza/atividade (CNAE), por CFOP e tipo (produto/serviço)
  - Exclusões/adições legais (ex.: vendas canceladas, devoluções, tributos sobre vendas quando aplicável)
- Saídas esperadas:
  - Base de IRPJ/CSLL presumida por atividade e competência; cálculo do imposto aplicando alíquotas e adicionais
- Amarrações recomendadas no código:
  - Persistir tabela “Percentuais Presumido” por CNAE/atividade e vigência
  - Mapear CFOP→atividade/natureza de receita; agregar receitas por competência
  - Gerar visões/relatórios e cruzar com SPED Contribuições (F500/F550 por CFOP/CST)

---

## Lucro Real — PIS/COFINS não cumulativos
- O que existe no código (estruturas SPED):
  - Bloco M (EFD Contribuições) cobre apuração não cumulativa — bases (M210/M610), créditos (M100/M500) e totais (M200/M600)
  - Bloco F e F100 permitem detalhar receitas/documentos e base de créditos com `NAT_BC_CRED`, CSTs e contas contábeis
- O que falta (motor):
  - Regras para compor a base não cumulativa por código de contribuição (naturezas de receita) e para reconhecer créditos elegíveis por item/documento (aquisições, insumos, energia, aluguéis, ativo imobilizado, etc.)
  - Parametrização de CST PIS/COFINS e vinculação CFOP→(incidência/creditamento)
- Entradas típicas:
  - NF-e/NFS-e com CFOP, CST PIS/COFINS, natureza de operação, valores (produto, frete, seguro, descontos), e identificação do insumo/bem/serviço
  - Cadastros auxiliares: mapeamento de `NAT_BC_CRED`, elegibilidade de crédito por NCM/CFOP/CST, reclassificações contábeis
- Saídas esperadas:
  - Registros M210/M610 por código de contribuição (base, alíquota, contribuição, ajustes)
  - Registros M100/M500 de créditos (base, alíquota, valor, saldos) e M200/M600 de totais
- Amarrações a CFOP e CST (no repo):
  - Em F500/F550 há campo `CFOP` — usar como chave de consolidação de receitas por natureza (cumulativo vs. não cumulativo)
  - Em F100 há `NAT_BC_CRED` e CSTs — base para classificar créditos não cumulativos

---

## CIAP (ICMS) — controle e impacto
- No repo: SPED Fiscal Bloco G (`ConttaEFD/BlocoG.cs`) com G110 (índice `IND_PER_SAI`) e G126 (outros créditos CIAP)
- Uso esperado: apropriação mensal (tipicamente 1/48 do crédito do ativo × `IND_PER_SAI`) alimentando a apuração de ICMS (Bloco E). Não impacta diretamente PIS/COFINS, mas integra consistência fiscal e controles de custo no Lucro Real.

---

## Inventário (Bloco H) — entradas/saídas e consistência
- No repo: `ConttaEFD/BlocoH.cs` com H005 (valor total), H010 (itens) e H020 (CST ICMS, base e valor ICMS por item)
- Consistências típicas:
  - Divergência de estoque vs. movimentação (entradas/saídas por CFOP) ao longo da competência
  - Amarração de custo (CMV) no IRPJ/CSLL do Lucro Real

---

## Vinculação a CFOP — orientações
- Para receitas: usar CFOP dos documentos (presentes em SPED Contrib F500/F550) para classificar cumulativo vs. não cumulativo e natureza por atividade
- Para créditos: usar CFOP de entrada + CST PIS/COFINS e `NAT_BC_CRED` (F100) para mapear elegibilidade de créditos
- Para IPI/ICMS: exemplos de CFOP em EFD Fiscal E510 (CFOP no IPI) e ajustes/diferencial em E110/E210/E310

---

## Validações cruzadas com SPED (checklist)
- PIS/COFINS (Real):
  - Soma de M210/M610 por código = M200/M600 (após ajustes)
  - Créditos M100/M500: saldo e utilização compatíveis; vínculos com F100 `NAT_BC_CRED`
  - Receitas F500/F550 por CFOP/CST compatíveis com livros de receitas e XMLs
- ICMS/IPI e CIAP:
  - E110/E210/E310 consistentes com G110/G126 (apropriação CIAP) e com movimentação por CFOP (Bloco C)
- Inventário:
  - H005/H010 compatíveis com saldo de estoque e CMV

---

## Conclusões e próximos passos
- Conclusão: o repositório contém modelos SPED (EFD Contribuições/Fiscal) suficientes para serialização e validações, mas não há motores de cálculo para Lucro Presumido (percentuais por atividade) nem para PIS/COFINS não cumulativos (bases e créditos).
- Próximos passos recomendados:
  1) Tabela “Percentuais Presumido” por CNAE/atividade/vigência (IRPJ/CSLL) + agregador por CFOP/atividade
  2) Parametrizar CST/CFOP→(incidência/creditamento) para PIS/COFINS e `NAT_BC_CRED`; criar regras de créditos por tipo de gasto/NCM
  3) Gerar e conciliar Blocos M (M100/M200/M210/M500/M600/M610) e F (F100/F500/F550) a partir dos documentos
  4) Consumir CIAP (Bloco G) e Inventário (Bloco H) nas consistências; adicionar testes e trilhas de regra

## Referências de arquivo
- `Corporate.Contta.Schedule.SpedContta/ConttaContribuicoes/BlocoM.cs`
- `Corporate.Contta.Schedule.SpedContta/ConttaContribuicoes/BlocoF.cs`
- `Corporate.Contta.Schedule.SpedContta/ConttaEFD/BlocoH.cs`
- `Corporate.Contta.Schedule.SpedContta/ConttaEFD/BlocoG.cs`
- `Corporate.Contta.Schedule.SpedContta/ConttaEFD/BlocoE.cs`
