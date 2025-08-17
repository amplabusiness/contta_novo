import {
  currencyFormatter,
  cpfCnpjFormatter,
  dateFormatter,
} from '@/utils/formatters';

export const consultaNotasFiscaisColumns = [
  {
    title: 'Nº NF-e',
    dataIndex: 'nnfe',
    key: 'nnfe',
  },
  {
    title: 'CNPJ',
    dataIndex: 'cnpj',
    key: 'cnpj',
    render: (text, record) => <span>{cpfCnpjFormatter(text)}</span>,
  },
  {
    title: 'Razão Social',
    dataIndex: 'razaoSocial',
    key: 'razaoSocial',
  },
  {
    title: 'Tipo NF-e',
    dataIndex: 'tipNfe',
    key: 'tipNfe',
  },
  {
    title: 'Valor NF-e',
    dataIndex: 'vtTotalNfe',
    key: 'vtTotalNfe',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Data de Emissão',
    dataIndex: 'dhEmi',
    key: 'dhEmi',
    render: (text, record) => <span>{dateFormatter(text)}</span>,
  },
];

export const tiposNotasFiscais = [
  { key: 'Entrada', label: 'Entrada', value: 'Entrada' },
  { key: 'Venda', label: 'Saída', value: 'Venda' },
];

export const ufs = [
  { key: 'AC', label: 'AC', value: 'AC' },
  { key: 'AL', label: 'AL', value: 'AL' },
  { key: 'AP', label: 'AP', value: 'AP' },
  { key: 'AM', label: 'AM', value: 'AM' },
  { key: 'BA', label: 'BA', value: 'BA' },
  { key: 'CE', label: 'CE', value: 'CE' },
  { key: 'ES', label: 'ES', value: 'ES' },
  { key: 'GO', label: 'GO', value: 'GO' },
  { key: 'MA', label: 'MA', value: 'MA' },
  { key: 'MT', label: 'MT', value: 'MT' },
  { key: 'MS', label: 'MS', value: 'MS' },
  { key: 'MG', label: 'MG', value: 'MG' },
  { key: 'PA', label: 'PA', value: 'PA' },
  { key: 'PB', label: 'PB', value: 'PB' },
  { key: 'PR', label: 'PR', value: 'PR' },
  { key: 'PE', label: 'PE', value: 'PE' },
  { key: 'PI', label: 'PI', value: 'PI' },
  { key: 'RJ', label: 'RJ', value: 'RJ' },
  { key: 'RN', label: 'RN', value: 'RN' },
  { key: 'RS', label: 'RS', value: 'RS' },
  { key: 'RO', label: 'RO', value: 'RO' },
  { key: 'RR', label: 'RR', value: 'RR' },
  { key: 'SC', label: 'SC', value: 'SC' },
  { key: 'SP', label: 'SP', value: 'SP' },
  { key: 'SE', label: 'SE', value: 'SE' },
  { key: 'TO', label: 'TO', value: 'TO' },
  { key: 'DF', label: 'DF', value: 'DF' },
];
