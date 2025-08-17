import { currencyFormatter, dateFormatter } from '@/utils/formatters';

export const blocoE100Columns = [
  {
    title: 'Data Inicial',
    dataIndex: 'dataInicial',
    key: 'dataInicial',
    render: (text, record) => <span>{dateFormatter(text)}</span>,
  },
  {
    title: 'Data Final',
    dataIndex: 'dataFinal',
    key: 'dataFinal',
    render: (text, record) => <span>{dateFormatter(text)}</span>,
  },
];

export const blocoE110Columns = [
  {
    title: 'Valor Total dos Débitos do Imposto',
    dataIndex: 'valorDebitoImpostos',
    key: 'valorDebitoImpostos',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Valor Total Ajustes a Débito (Doc Fiscal)',
    dataIndex: 'valorAjustesDebitoDocFiscal',
    key: 'valorAjustesDebitoDocFiscal',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Valor Total Ajustes a Débito',
    dataIndex: 'valorAjustesDebito',
    key: 'valorAjustesDebito',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Valor Total dos Estornos de Créditos',
    dataIndex: 'valorEstornosCreditos',
    key: 'valorEstornosCreditos',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Valor Total dos Créditos do Imposto',
    dataIndex: 'valorCreditoImpostos',
    key: 'valorCreditoImpostos',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Valor Total Ajustes a Créditos (Doc Fiscal)',
    dataIndex: 'valorAjustesCreditoDocFiscal',
    key: 'valorAjustesCreditoDocFiscal',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Valor Total Ajustes a Crédito',
    dataIndex: 'valorAjustesCredito',
    key: 'valorAjustesCredito',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Valor Total dos Estornos de Débitos',
    dataIndex: 'valorEstornosDebitos',
    key: 'valorEstornosDebitos',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Saldo Credor Anterior',
    dataIndex: 'saldoCredorAnterior',
    key: 'saldoCredorAnterior',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Valor do Saldo Devedor',
    dataIndex: 'valorSaldoDevedor',
    key: 'valorSaldoDevedor',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Valor das Deduções',
    dataIndex: 'valorDeducoes',
    key: 'valorDeducoes',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Valor do ICMS a Recolher',
    dataIndex: 'valorIcmsRecolher',
    key: 'valorIcmsRecolher',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Valor do Saldo Credor ICMS a Recolher',
    dataIndex: 'valorSaldoCredorIcms',
    key: 'valorSaldoCredorIcms',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Extra Apuração',
    dataIndex: 'extraApuracao',
    key: 'extraApuracao',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
];

export const blocoE111Columns = [
  {
    title: 'Código do Ajuste',
    dataIndex: 'codAjuste',
    key: 'codAjuste',
  },
  {
    title: 'Descrição Complementar',
    dataIndex: 'descricaoAjuste',
    key: 'descricaoAjuste',
  },
  {
    title: 'Valor do Ajuste',
    dataIndex: 'valorAjuste',
    key: 'valorAjuste',
    render: (text, record) => (
      <span>{currencyFormatter(text ?? record.vlTotalNfe)}</span>
    ),
  },
];

export const blocoE113Columns = [
  {
    title: 'Código do Participante',
    dataIndex: 'codParticipante',
    key: 'codParticipante',
  },
  {
    title: 'Código do Modelo do Documento',
    dataIndex: 'codModeloDocumento',
    key: 'codModeloDocumento',
  },
  {
    title: 'Série',
    dataIndex: 'serie',
    key: 'serie',
  },
  {
    title: 'Subsérie',
    dataIndex: 'subserie',
    key: 'subserie',
  },
  {
    title: 'Número do Documento',
    dataIndex: 'numeroDocumento',
    key: 'numeroDocumento',
  },
  {
    title: 'Data de Emissão',
    dataIndex: 'dataEmissao',
    key: 'dataEmissao',
    render: (text, record) => <span>{dateFormatter(text)}</span>,
  },
  {
    title: 'Código do Item',
    dataIndex: 'codItem',
    key: 'codItem',
  },
  {
    title: 'Valor de Ajuste do Item',
    dataIndex: 'valorAjusteItem',
    key: 'valorAjusteItem',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Chave',
    dataIndex: 'chave',
    key: 'chave',
  },
];

export const blocoE115Columns = [
  {
    title: 'Código da Informação',
    dataIndex: 'codInformacao',
    key: 'codInformacao',
  },
  {
    title: 'Valor da Informação',
    dataIndex: 'valorInformacao',
    key: 'valorInformacao',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Descrição Complementar',
    dataIndex: 'descComplementar',
    key: 'descComplementar',
  },
];

export const blocoE116Columns = [
  {
    title: 'Código do ICMS',
    dataIndex: 'codIcms',
    key: 'codIcms',
  },
  {
    title: 'Valor do ICMS',
    dataIndex: 'valorIcms',
    key: 'valorIcms',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Data de Vencimento do ICMS',
    dataIndex: 'dataVencimentoIcms',
    key: 'dataVencimentoIcms',
    render: (text, record) => <span>{dateFormatter(text)}</span>,
  },
  {
    title: 'Código da Receita',
    dataIndex: 'codReceita',
    key: 'codReceita',
  },
  {
    title: 'Número do Processo',
    dataIndex: 'numeroProcesso',
    key: 'numeroProcesso',
  },
  {
    title: 'Origem do Processo',
    dataIndex: 'origemProcesso',
    key: 'origemProcesso',
  },
  {
    title: 'Descrição do Processo',
    dataIndex: 'descProcesso',
    key: 'descProcesso',
  },
  {
    title: 'Descrição Complementar',
    dataIndex: 'descComplementar',
    key: 'descComplementar',
  },
  {
    title: 'Mês de Referência',
    dataIndex: 'mesReferencia',
    key: 'mesReferencia',
  },
];
