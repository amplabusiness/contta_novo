## Importação NF-e (XML) — mapeamentos, diferenças e fluxo

Este documento descreve como o XML da NF-e (modelo 55) é consumido, validado, mapeado e persistido, incluindo diferenças de operação (entrada/saída/devolução/ajuste/cancelamento) e onde ficam NCM, CFOP, CSOSN/CST e GTIN no pipeline atual.

### Principais classes envolvidas

- Adapter
  - Corporate.Contta.Schedule.Infra.Models.Adapter.EntidadeXmlToEntidadeMongodbMod55
    - CretaEntidadeMongoEmpresaEmitente(Emit)
    - CretaEntidadeMongoEmpresaDestinatario(Dest)
    - CreateEntidadeMongoNotaFiscal(NfeProc)
    - CriateProdutos(List<Det>, DateTime? DhEmt)
    - CriateImpostos(List<Det>)
    - CriateProdutosFornecedor(List<Det>)
- Repositório orquestrador
  - Corporate.Contta.Schedule.Infra.Repositories.NfeRepository
    - CreateXmlNfe(NfeProc) → fluxo principal de ingestão
    - Insert/InsertProdutos/InsertFornec, GetByFilterCanceladas, InsertAjusteNfe, etc.

### Mapeamentos de TAGs → entidades

1) ide/total/emit/dest → NFE
- UFEnv: ide.cUF mapeado por tabela (11=RO, 12=AC, …, 52=GO, 53=DF)
- TPag/VPag: nfeProc.NFe.infNFe.pag.detPag.[tPag|vPag] (apenas versão 4.00)
- CodBarra: infNFe.Id sem o prefixo "NFe" (Substring(3))
- Datas: DhEmi (ide.dhEmi), DhSaida (ide.dhSaiEnt)
- Totais: total.ICMSTot.[vBC, vBCST, vTotTrib, vCOFINS, vIPI, vOutro, vPIS, vDesc, vFrete, vProd, vSeg, vICMS, vST, vNF]
- Identificação/gerais: ide.[mod, natOp, indFinal, série, nNF, indPres, tpNF, finNFe]
- CNPJEmitente: emit.CNPJ
- ModeloTipo: lógica por tipo de operação (ver abaixo)

2) emit → EmpresaEmit
- Endereço: enderEmit.[xBairro, CEP, xMun, xCpl, xLgr, nro, UF]
- CNPJ: emit.CNPJ normalizado (remove pontuação)
- XFant, XNome, IE, CRT

3) dest → EmpresaDest
- Endereço: enderDest.[xBairro, CEP, xMun, xCpl, xLgr, nro, UF]
- CNPJ/CPF: normalizados (remove pontuação)
- XFant, XNome, IE, IEST; IdEstrangeiro → Estrangeiro=true/false

4) det.prod → Produtos/ProdutosFornecedor
- Código/descrição: prod.[cProd, xProd]
- NCM: prod.NCM → Produtos.NcmProd
- GTIN: prod.CEAN → Produtos.Ean (GTIN); obs: CEANTrib não é mapeado hoje
- CFOP: prod.CFOP → Produtos.Cfop
- Unidade/quantidade/valores: prod.[uCom, qCom, vUnCom, vProd, vDesc, vFrete, vOutro, vUnTrib, qTrib, uTrib]
- Pedidos: prod.[nItemPed, xPed]
- Origem: prod.orig
- Tributos aproximados: prod.vTotTrib → Produtos.VlAproxTributos
- CSOSN (Simples): imposto.ICMS.ICMSSN102?.CSOSN → Produtos.Csons (int) [senão 0]
- Flags fiscais em Produtos (NcmMono/IcmsSt/Beneficios/Isento/Imune/etc.) são inicializadas como false no parser (não há lógica de ativação via XML hoje)

5) det.imposto → Impostos (por item)
- ICMS20: vICMS → VlIcms; vBC → BCIcms; pICMS → AliqIcms; CST → SituTributaria; orig → Origem; modBC → ModBcIcms; pRedBC → PerRedBcIcmsSt
- ICMSSN102: orig → Origem; CSOSN → Csosn
- ICMS60: orig → Origem; CST → SituTributaria; vICMSSTRet → VlBcIcmsSt
- IPI.IPITrib: vIPI → Ipi; pIPI → AliquotaIpi
- Observação: PIS/COFINS em nível de item não são populados pelo adapter; o consumo posterior usa campos (VPIS/VCOFINS) que podem permanecer 0 se não preenchidos em outro ponto.

### Detecção de tipo de operação (ModeloTipo)
- Se ide.natOp == "TRANSFERENCIAPARAFILIAL" → TranMercadoria = true
- Caso contrário:
  - "Saida" (empresa emitente existe no cadastro) → ModeloTipo = "Venda"
  - "Entrada" → ModeloTipo = "Entrada"
  - Se natOp contém "Devolucao/DEVOLUCAO" e ModeloNota == "Saida" → "DevolucaoSaida"
  - Senão → "DevolucaoCompra"

### Diferenças por operação
- Saída (Venda): itens gravados em Produtos; destinatário é o cliente (EmpresaDest)
- Entrada: itens gravados em ProdutosFornecedor; destinatário é a própria empresa (pelo contexto)
- Devolução: mesma estrutura de NFE, com ModeloTipo diferenciado conforme regra acima; exibidas em relatórios (ex.: Livro Fiscal) para compensações
- Ajuste: entidade AjusteNfe com filtros por CFOP/NCM/CST e totalizadores (“Total por Produtos”, “Total por Base”, “Total por nota Fiscal”) via NfeRepository.InsertAjusteNfe
- Cancelamento: coleção NfeCanceldas; GetByFilterCanceladas cruza RefNfe (chave) com NFE.CodBarra e respeita ModeloNota do evento

### Fluxo completo de ingestão (CreateXmlNfe)
1) Determinar se a NF é de Saída ou Entrada consultando a empresa emitente (ExistsCompany)
2) Preencher nota.ModeloNota e nota.CnpjEmitente conforme o passo 1
3) Converter XML em entidades:
   - NFE via CreateEntidadeMongoNotaFiscal

```mermaid
flowchart TD
  A[RabbitMQ mensagem XML NFeProc] --> B[ConsumerXml/Program.cs\nMessageParser.ParseNfeProc]
  B --> C[IntegrationXmlMode55\nCreateEntidadeMongoNotaFiscal → NFE\nCriateProdutos/CriarProdutosFornecedor → Produtos\nCriateImpostos → Impostos]
  C --> D[NfeRepository.CreateXmlNfe]
  D -->|Verifica idempotência por CodBarra| E{Já gravado?}
  E -- Sim --> F[Descarta/retorna false]
  E -- Não --> G[EmpresaEmit/EmpresaDest\nObterPorCnpj -> Insert se não existir]
  G --> H[Insert(NFE)]
  H --> I[InsertProdutos/InsertFornec]
  I --> J[_impostos.Insert(Impostos)]
  J --> K[OK/true]
```
   - Upsert de EmpresaEmit e EmpresaDest (por CNPJ/CPF) e vincular os Ids na NFE
4) Persistir NFE (evita duplicação por CodBarra/chave)
5) Persistir itens:
   - Saída → Produtos
   - Entrada/Devolução compra → ProdutosFornecedor
6) Persistir impostos por item via CriateImpostos
7) Demais projeções/consultas usam ProductRepository/ImpostosRepository para montar visões (NfeT, FullNFE, Livro Fiscal, etc.)

### Campos fiscais principais e onde aparecem
- NCM: det.prod.NCM → Produtos.NcmProd / relatórios
- CFOP: det.prod.CFOP → Produtos.Cfop / validações e agregações
- CSOSN: det.imposto.ICMS.ICMSSN102.CSOSN → Produtos.Csons e Impostos.Csosn
- CST: det.imposto.ICMS.[ICMS20|ICMS60].CST → Impostos.SituTributaria (e consumo em views)
- GTIN (EAN/CEAN): det.prod.CEAN → Produtos.Ean (observação: CEANTrib não mapeado)

### Observações e limitações conhecidas
- Versão 4.00: somente detPag.[tPag,vPag] é lido; outras variantes de pagamento não são mapeadas
- Flags de benefícios/monofásico/isenções em Produtos não são derivadas automaticamente do XML
- PIS/COFINS por item não são populados no adapter
- Há uso do namespace com typo (Corporate.Contta.Schedule.Domain.Entities.Imporsto) em pontos do código; padronizar para Imposto
- Há uma conexão MongoDB hardcoded em NfeRepository.DebitarValorSimples (deve ir para variáveis de ambiente)

### Validações úteis (próximos passos sugeridos)
- Validar schema NF-e 4.00 antes do parse
- Normalizar leitura de pagamentos (v3 vs v4)
- Popular PIS/COFINS no nível do item quando disponível
- Mapear CEANTrib além de CEAN
- Unificar CST/CSOSN e CEST (se necessário) nas estruturas de saída
