import {
  currencyFormatter,
  dateFormatter,
  percentageFormatter,
} from '@/utils/formatters';

export const homeResumoEmpresasColumns = [
  {
    title: 'Faturamento',
    dataIndex: 'faturamento',
    key: 'faturamento',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Alíquota Efetiva',
    dataIndex: 'aliquota',
    key: 'aliquota',
    render: (text, record) => <span>{percentageFormatter(text * 100)}</span>,
  },
  {
    title: 'Data do Fechamento',
    dataIndex: 'dataFechamento',
    key: 'dataFechamento',
    render: (text, record) => <span>{dateFormatter(text)}</span>,
  },
  {
    title: 'Validade do Certificado',
    dataIndex: 'validadeCertificado',
    key: 'validadeCertificado',
  },
];

export const spreadsheetHeaders = [
  {
    key: 'razaoSocial',
    title: 'Razão Social',
  },
  {
    key: 'faturamento',
    title: 'Faturamento',
  },
  {
    key: 'aliquota',
    title: 'Alíquota Efetiva',
  },
  {
    key: 'dataFechamento',
    title: 'Data do Fechamento',
  },
  {
    key: 'validadeCertificado',
    title: 'Validade do Certificado',
  },
  {
    key: 'difal',
    title: 'DIFAL',
  },
  {
    key: 'declaracao',
    title: 'Declaração',
  },
  {
    key: 'das',
    title: 'DAS',
  },
  {
    key: 'extrato',
    title: 'Extrato',
  },
];
