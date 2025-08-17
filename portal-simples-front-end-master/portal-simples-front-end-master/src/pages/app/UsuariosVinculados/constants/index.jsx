import { cpfCnpjFormatter } from '@/utils/formatters';

export const usuariosVincularEmpresasColumns = [
  {
    title: 'Razão Social',
    dataIndex: 'name',
    key: 'name',
  },
  {
    title: 'CNPJ/CPF',
    dataIndex: 'cnpj',
    key: 'cnpj',
    render: (text, record) => <span>{cpfCnpjFormatter(text)}</span>,
  },
];

export const usuariosCadastradosColumns = [
  {
    title: 'Nome',
    dataIndex: 'name',
    key: 'name',
  },
  {
    title: 'CPF',
    dataIndex: 'document',
    key: 'document',
    render: (text, record) => (
      <span>{text ? cpfCnpjFormatter(text) : '-'}</span>
    ),
  },
  {
    title: 'Cargo',
    dataIndex: 'role',
    key: 'role',
  },
];

export const spreadsheetHeaders = [
  { key: 'name', title: 'Nome' },
  { key: 'document', title: 'CPF' },
  { key: 'role', title: 'Cargo' },
];

export const roleOptions = [
  { key: 'Assistente', label: 'Assistente', value: 'Assistente' },
  { key: 'Diretor', label: 'Diretor', value: 'Diretor' },
  { key: 'CEO', label: 'CEO', value: 'CEO' },
];
