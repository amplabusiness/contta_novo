import { currencyFormatter } from '@/utils/formatters';

export const listagemProdutosColumns = [
  {
    title: 'Nº NFe',
    dataIndex: 'numeroNfe',
    key: 'numeroNfe',
  },
  {
    title: 'Código',
    dataIndex: 'codigoProduto',
    key: 'codigoProduto',
  },
  {
    title: 'Nome',
    dataIndex: 'nomeProduto',
    key: 'nomeProduto',
  },
  {
    title: 'NCM',
    dataIndex: 'ncm',
    key: 'ncm',
  },
  {
    title: 'CFOP',
    dataIndex: 'cfop',
    key: 'cfop',
  },
  {
    title: 'Valor',
    dataIndex: 'valorNfe',
    key: 'valorNfe',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Lei',
    dataIndex: 'lei',
    key: 'lei',
  },
];
