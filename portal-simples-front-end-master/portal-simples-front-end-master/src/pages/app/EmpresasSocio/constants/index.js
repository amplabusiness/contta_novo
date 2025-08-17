import { cpfCnpjFormatter } from '@/utils/formatters';

export const empresasDadosSociosColumns = [
  {
    title: 'Nome',
    dataIndex: 'nome_socio',
    key: 'nome_socio',
  },
  {
    title: 'CNPJ',
    dataIndex: 'cnpj',
    key: 'cnpj',
    render: (text, record) => <span>{cpfCnpjFormatter(text)}</span>,
  },
];
