import {
  currencyFormatter,
  percentageFormatter,
  cpfCnpjFormatter,
} from '@/utils/formatters';

export const notasFiscaisServicoColumns = [
  {
    title: 'CNPJ/CPF',
    dataIndex: 'cnpjCpf',
    key: 'cnpjCpf',
    render: (text, record) => <span>{cpfCnpjFormatter(text)}</span>,
  },
  {
    title: 'Razão Social',
    dataIndex: 'razaoSocial',
    key: 'razaoSocial',
  },
  {
    title: 'Retenções',
    dataIndex: 'retencoes',
    key: 'retencoes',
    children: [
      {
        title: 'COFINS',
        dataIndex: 'cofins',
        key: 'cofins',
      },
      {
        title: 'INSS',
        dataIndex: 'inss',
        key: 'inss',
      },
      {
        title: 'IR',
        dataIndex: 'ir',
        key: 'ir',
      },
      {
        title: 'CSLL',
        dataIndex: 'csll',
        key: 'csll',
      },
    ],
  },
  {
    title: 'Desconto',
    dataIndex: 'desconto',
    key: 'desconto',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Alíquota',
    dataIndex: 'aliquota',
    key: 'aliquota',
    render: (text, record) => <span>{percentageFormatter(text)}</span>,
  },
  {
    title: 'Valor ISS',
    dataIndex: 'valorIss',
    key: 'valorIss',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Valor PIS',
    dataIndex: 'valorPis',
    key: 'valorPis',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Valor COFINS',
    dataIndex: 'valorCofins',
    key: 'valorCofins',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Valor IR',
    dataIndex: 'valorIr',
    key: 'valorIr',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Valor CSLL',
    dataIndex: 'valorCsll',
    key: 'valorCsll',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Valor Dedução',
    dataIndex: 'valorDeducao',
    key: 'valorDeducao',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Total NF-e',
    dataIndex: 'totalNFe',
    key: 'totalNFe',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
];
