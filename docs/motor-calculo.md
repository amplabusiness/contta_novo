## Motor de Cálculo — estado atual, classes e fluxo

Esta nota técnica resume o “motor de cálculo” hoje no backend .NET, destacando classes, padrão aplicado, lacunas e próximos passos.

### Classes e interfaces mapeadas

- Domínio NFe e tributos por item
  - Corporate.Contta.Schedule.Domain.Entities.NfeAgg.NFE
  - Corporate.Contta.Schedule.Domain.Entities.NfeAgg.Impostos (campos: BCIcms, VlIcms, AliqIcms, VlBcIcmsSt, Ipi, AliquotaIpi, Cst/Csosn, etc.)
  - Corporate.Contta.Schedule.Domain.Entities.Product.Produtos / ProdutosFornecedor
  - Corporate.Contta.Schedule.Domain.Entities.NfeAgg.ImpostosProd (flags: NcmMono, IcmsSt, Beneficios, Isento, Imune, etc.)
- Tabelas/parametrizações
  - Corporate.Contta.Schedule.Domain.Entities.ImpostoAgg.Cfop (listas por operação)
  - Corporate.Contta.Schedule.Domain.Entities.ImpostoAgg.CsosnSimples (obs: namespace “Imporsto” em alguns pontos)
  - Corporate.Contta.Schedule.Domain.Entities.ExternalTableAgg.IcmsSt
  - Corporate.Contta.Schedule.Domain.Entities.ImpostoAgg.ImpostoImune/Insento/Reducao
- Apuração/visões
  - Corporate.Contta.Schedule.Domain.Entities.DashboardAgg.Apuracoes (Apuracao, ApuracaoService, etc.)
  - Corporate.Contta.Schedule.Domain.Entities.FullNfeAgg.* (TaxCalculation é placeholder)
  - Corporate.Contta.Schedule.Domain.Entities.NfeTAgg.* (NfeT e itens para tela analítica)
- Infra agregadora
  - Corporate.Contta.Schedule.Infra.Models.TbSimplesNacional.IntegrationTbSimplesNfManual (agregação mensal por CFOP para TbSimples)
  - Corporate.Contta.Schedule.Infra.Repositories.* (NfeRepository, ProductRepository, ImpostosRepository)

### Padrão aplicado hoje

- Não há um “rules engine” central para o Simples/ICMS; o que existe é:
  - Parse do XML e persistência de NFE/Produtos/Impostos
  - Uso de listas de CFOP e flags em Produtos para decisões simples (ex.: ProdutoBaySimples)
  - Agregação mensal em TbSimples (IntegrationTbSimplesNfManual) calculando BaseCalculo e ValorContábil por CFOP
  - Relatórios/visões (Livro Fiscal, NfeT, FullNFE) consomem dados já persistidos
- A classe CalcularSimples.cs existe, mas está vazia (somente DTOs Importos*). Não há estratégia por Anexo/UF nem tratamento de benefícios estaduais.

### Fluxo atual (resumo)
1) Ingestão XML → NFE/Produtos/Impostos (ver `docs/nfe-importacao-xml.md`)
2) Marcação/derivação simples
   - Produtos.Csons preenchido a partir de ICMSSN102.CSOSN quando houver
   - Flags fiscais em Produtos permanecem false (não há inferência automática)
3) Agregação para Simples
   - IntegrationTbSimplesNfManual monta totais por mês/CFOP (vendas/serviços/entradas), atualiza TbSimples. Também há débitos ao excluir notas (DebitarValorSimples) — com conexão Mongo hardcoded que deve ser externalizada
4) Relatórios
   - Livro Fiscal (NfeRepository.GetAllNfe) soma por CFOP; NfeT e FullNFE exibem dados analíticos e de produtos/impostos

### Lacunas identificadas
- Ausência de motor de regras (Strategy/Rules) por:
  - Regime Simples Nacional e Anexo (I-V), com faixas e partilhas
  - UF/benefícios (crédito presumido, redução base, monofásico, cesta básica)
  - Operação (venda/entrada/devolução/serviço) e natureza (tributada, isenta, imune)
- Flags fiscais em Produtos não são populadas por regras
- PIS/COFINS por item não é consolidado na ingestão
- Namespace “Imporsto” inconsistente

### Próximo desenho recomendado (resumo)
- Criar um Rules Engine leve:
  - Contract IRegraSimples: Avaliar(Produto, Impostos, ContextoEmpresa, ContextoUF) → ResultadoRegra
  - Estratégias por UF/Anexo/Natureza e tabelas auxiliares (IcmsSt, benefícios, CFOP)
  - Pipeline por item: Normalização → Enriquecimento (NCM/CEST/GTIN) → Regras ICMS/PIS/COFINS → Consolidação
- Preencher flags de Produtos a partir das regras
- Implementar cálculo de base/aliquota/valores por item e por documento; emitir trilhas (auditoria)

### Itens de manutenção imediata
- Externalizar conexão Mongo em DebitarValorSimples
- Corrigir namespace “Imporsto” → “Imposto” em todo o código
- Popular PIS/COFINS no adapter quando presente; mapear CEANTrib
- Completar `FullNfeAgg.TaxCalculation` com UF, município, base, alíquota e valores reais
