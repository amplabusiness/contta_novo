import { cpfCnpjFormatter, dateFormatter } from '@/utils/formatters';

export const empresasCadastradasColumns = [
  {
    title: 'CNPJ/CPF',
    dataIndex: 'cnpj',
    key: 'cnpj',
    render: (text, record) => <span>{cpfCnpjFormatter(text)}</span>,
  },
  {
    title: 'Nome Fantasia',
    dataIndex: 'alias',
    key: 'alias',
  },
  {
    title: 'Data de Fundação',
    dataIndex: 'founded',
    key: 'founded',
    render: (text, record) => <span>{dateFormatter(text)}</span>,
  },
  {
    title: 'Telefone',
    dataIndex: 'phone',
    key: 'phone',
  },
  {
    title: 'Estado',
    dataIndex: ['address', 'state'],
    key: 'state',
  },
];

export const fileTypes = ['csv', 'xlsx', 'xls'];

export const spreadsheetHeaders = [
  {
    key: 'name',
    title: 'Razão Social',
  },
  {
    key: 'cnpj',
    title: 'CNPJ/CPF',
  },
  {
    key: 'alias',
    title: 'Nome Fantasia',
  },
  {
    key: 'founded',
    title: 'Data do Fundação',
  },
  {
    key: 'phone',
    title: 'Telefone',
  },
  {
    key: ['address', 'state'],
    title: 'Estado',
  },
];
