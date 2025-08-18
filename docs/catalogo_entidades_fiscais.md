# Catálogo de Entidades Fiscais (auto)

Campos críticos por entidade. Baseado nos namespaces Domain/Infra/SPED.

- Nota (NF-e/NFS-e)
  - Identificação: Id (Guid), Modelo (55/57/SE), Série, Número, Chave, Data Emissão/Entrada, Tipo (Entrada/Venda/Serviço)
  - Partes: Emitente (CNPJ/UF/IE), Destinatário (CNPJ/UF/IE/Município)
  - Totais: Valor Produtos, Frete, Seguro, Despesas, Desconto, IPI, ST, ICMS, PIS, COFINS, Total NF
  - Vinculações: Empresa (CompanyInformationId), Itens[], Ajustes Bloco E, Situação (Ativa/Cancelada)

- Item
  - ProdutoId, Quantidade, Unidade, Valor Unitário, Valor Total
  - CFOP, NCM, CEST, CST/CSOSN, Origem
  - Bases: BC ICMS, BC ST, BC PIS, BC COFINS, BC IPI
  - Alíquotas: ICMS, ST, PIS, COFINS, IPI
  - MVA, Redução BC, Créditos (NAT_BC_CRED), Indicadores monofásicos/aliq. zero/isento

- Produto
  - Código, Descrição, NCM, CEST, Cód. Serviço (NFS-e), Unidade, Ativo
  - Regras padrão por UF/CFOP/CST/CSOSN

- Regra Fiscal
  - Escopos: Regime (Simples/Presumido/Real), UF Origem/Destino, Operação (Venda/Serviço/Transporte)
  - Parâmetros: CFOP, CST/CSOSN, MVA, Redução, Alíquotas ICMS/PIS/COFINS/IPI, NAT_BC_CRED
  - Vigência: Início, Fim, Fonte legal

- Tabela Fiscal (externa)
  - Anexos Simples (faixa faturamento, alíquota nominal, dedução; partilha IRPJ/CSLL/COFINS/PIS/CPP/ICMS/ISS)
  - CFOP, CST/CSOSN, NCM/CEST, NAT_BC_CRED
  - Vigência/Versionamento

- Apuração
  - Período (Competência), CNPJ/Empresa, Livro (EFD ICMS/IPI, EFD Contrib., ECD/ECF)
  - E110: débitos/créditos ICMS; E510: IPI por CFOP/CST; DIFAL/FCP por UF (C101)
  - M210/M610: apuração PIS/COFINS; M100/M500: créditos; F100/F500**
  - Saldo Devedor/Transportar, Ajustes (E111/M220/M620), Obrigações (E316)

- Lote
  - Origem: Upload XML, Planilha, Integração
  - Quantidade, Período, Status Processamento, Falhas

- Cliente/Fornecedor
  - Documento (CNPJ/CPF), IE, UF/Município, Endereço
  - Vínculo com Empresas e Notas

Campos críticos gerais: CFOP, NCM, CSOSN/CST, MVA, alíquota, base, dedução, vigência, NAT_BC_CRED.
