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

export const roleOptions = [
  { key: 'Assistente', label: 'Assistente', value: 'Assistente' },
  { key: 'Diretor', label: 'Diretor', value: 'Diretor' },
  { key: 'CEO', label: 'CEO', value: 'CEO' },
];
