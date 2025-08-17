import { cpfCnpjFormatter, currencyFormatter } from '@/utils/formatters';

export const notasFiscaisSimplificadasTransporteColumns = [
  {
    title: 'Nº Nota Fiscal',
    dataIndex: 'numeroNotaFiscal',
    key: 'numeroNotaFiscal',
  },
  {
    title: 'CNPJ',
    dataIndex: 'cnpj',
    key: 'cnpj',
    render: (text, record) => <span>{cpfCnpjFormatter(text)}</span>,
  },
  {
    title: 'Tipo de Frete',
    dataIndex: 'tipoFrete',
    key: 'tipoFrete',
  },
  {
    title: 'Estado',
    dataIndex: 'uf',
    key: 'uf',
  },
  {
    title: 'Município',
    dataIndex: 'municipio',
    key: 'municipio',
  },
  {
    title: 'Tipo CTE',
    dataIndex: 'tipoCte',
    key: 'tipoCte',
  },
  {
    title: 'Valor Desconto',
    dataIndex: 'valorDesconto',
    key: 'valorDesconto',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Valor ICMS',
    dataIndex: 'valorIcms',
    key: 'valorIcms',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Valor Total Serviço',
    dataIndex: 'valorTotalServico',
    key: 'valorTotalServico',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Valor Total',
    dataIndex: 'valorTotal',
    key: 'valorTotal',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
];
