import {
  cpfCnpjFormatter,
  currencyFormatter,
  dateFormatter,
} from '@/utils/formatters';

export const modeloVendaColumns = [
  {
    title: 'Código',
    key: 'code',
    dataIndex: 'code',
    align: 'center',
  },
  {
    title: 'Descrição do Produto/Serviço',
    key: 'description',
    dataIndex: 'description',
    align: 'center',
  },
  {
    title: 'NCM/SH',
    key: 'ncmSh',
    dataIndex: 'ncmSh',
    align: 'center',
  },
  {
    title: 'CST',
    key: 'cst',
    dataIndex: 'cst',
    align: 'center',
  },
  {
    title: 'Unid.',
    key: 'unit',
    dataIndex: 'unit',
    align: 'center',
  },
  {
    title: 'Qtd.',
    key: 'quantity',
    dataIndex: 'quantity',
    align: 'center',
  },
  {
    title: 'Vlr. Unitário',
    key: 'unitValue',
    dataIndex: 'unitValue',
    align: 'center',
    render: (text, record) => (
      <span>{text ? currencyFormatter(text) : '-'}</span>
    ),
  },
  {
    title: 'Vlr. Total',
    key: 'totalValue',
    dataIndex: 'totalValue',
    align: 'center',
    render: (text, record) => (
      <span>{text ? currencyFormatter(text) : '-'}</span>
    ),
  },
  {
    title: 'BC. ICMS',
    key: 'bcIcms',
    dataIndex: 'bcIcms',
    align: 'center',
    render: (text, record) => (
      <span>{text ? currencyFormatter(text) : '-'}</span>
    ),
  },
  {
    title: 'Vlr. ICMS',
    key: 'icmsValue',
    dataIndex: 'icmsValue',
    align: 'center',
    render: (text, record) => (
      <span>{text ? currencyFormatter(text) : '-'}</span>
    ),
  },
  {
    title: 'Vlr. IPI',
    key: 'ipiValue',
    dataIndex: 'ipiValue',
    align: 'center',
    render: (text, record) => (
      <span>{text ? currencyFormatter(text) : '-'}</span>
    ),
  },
  {
    title: 'Alíq. ICMS',
    key: 'icmsAliquot',
    dataIndex: 'icmsAliquot',
    align: 'center',
  },
  {
    title: 'Alíq. IPI',
    key: 'ipiAliquot',
    dataIndex: 'ipiAliquot',
    align: 'center',
  },
];

export const unitTypes = [
  { key: 'AMPOLA', label: 'AMPOLA', value: 'AMPOLA' },
  { key: 'BALDE', label: 'BALDE', value: 'BALDE' },
  { key: 'BANDEJ', label: 'BANDEJ', value: 'BANDEJ' },
  { key: 'BARRA', label: 'BARRA', value: 'BARRA' },
  { key: 'BISNAG', label: 'BISNAG', value: 'BISNAG' },
  { key: 'BLOCO', label: 'BLOCO', value: 'BLOCO' },
  { key: 'BOBINA', label: 'BOBINA', value: 'BOBINA' },
  { key: 'BOMBEAR', label: 'BOMBEAR', value: 'BOMBEAR' },
  { key: 'CÁPSULAS', label: 'CÁPSULAS', value: 'CÁPSULAS' },
  { key: 'CARRINHO', label: 'CARRINHO', value: 'CARRINHO' },
  { key: 'CENTO', label: 'CENTO', value: 'CENTO' },
  { key: 'CJ', label: 'CJ', value: 'CJ' },
  { key: 'CM', label: 'CM', value: 'CM' },
  { key: 'CM2', label: 'CM2', value: 'CM2' },
  { key: 'CX', label: 'CX', value: 'CX' },
  { key: 'CX2', label: 'CX2', value: 'CX2' },
  { key: 'CX3', label: 'CX3', value: 'CX3' },
  { key: 'CX5', label: 'CX5', value: 'CX5' },
  { key: 'CX10', label: 'CX10', value: 'CX10' },
  { key: 'CX15', label: 'CX15', value: 'CX15' },
  { key: 'CX20', label: 'CX20', value: 'CX20' },
  { key: 'CX25', label: 'CX25', value: 'CX25' },
  { key: 'CX50', label: 'CX50', value: 'CX50' },
  { key: 'CX100', label: 'CX100', value: 'CX100' },
  { key: 'DISP', label: 'DISP', value: 'DISP' },
  { key: 'DUZIA', label: 'DUZIA', value: 'DUZIA' },
  { key: 'EMBAL', label: 'EMBAL', value: 'EMBAL' },
  { key: 'FARDO', label: 'FARDO', value: 'FARDO' },
  { key: 'FOLHA', label: 'FOLHA', value: 'FOLHA' },
  { key: 'FRASCO', label: 'FRASCO', value: 'FRASCO' },
  { key: 'GALAO', label: 'GALAO', value: 'GALAO' },
  { key: 'GF', label: 'GF', value: 'GF' },
  { key: 'GRAMAS', label: 'GRAMAS', value: 'GRAMAS' },
  { key: 'JOGO', label: 'JOGO', value: 'JOGO' },
  { key: 'KG', label: 'KG', value: 'KG' },
  { key: 'KIT', label: 'KIT', value: 'KIT' },
  { key: 'LATA', label: 'LATA', value: 'LATA' },
  { key: 'LITRO', label: 'LITRO', value: 'LITRO' },
  { key: 'M', label: 'M', value: 'M' },
  { key: 'M2', label: 'M2', value: 'M2' },
  { key: 'M3', label: 'M3', value: 'M3' },
  { key: 'MILHEI', label: 'MILHEI', value: 'MILHEI' },
  { key: 'ML', label: 'ML', value: 'ML' },
  { key: 'MWH', label: 'MWH', value: 'MWH' },
  { key: 'PACOTE', label: 'PACOTE', value: 'PACOTE' },
  { key: 'PALETE', label: 'PALETE', value: 'PALETE' },
  { key: 'PARES', label: 'PARES', value: 'PARES' },
  { key: 'PC', label: 'PC', value: 'PC' },
  { key: 'AMIGO', label: 'AMIGO', value: 'AMIGO' },
  { key: 'K', label: 'K', value: 'K' },
  { key: 'RESMA', label: 'RESMA', value: 'RESMA' },
  { key: 'ROLO', label: 'ROLO', value: 'ROLO' },
  { key: 'SACO', label: 'SACO', value: 'SACO' },
  { key: 'SACOLA', label: 'SACOLA', value: 'SACOLA' },
  { key: 'TAMBOR', label: 'TAMBOR', value: 'TAMBOR' },
  { key: 'TANQUE', label: 'TANQUE', value: 'TANQUE' },
  { key: 'TON', label: 'TON', value: 'TON' },
  { key: 'TUBO', label: 'TUBO', value: 'TUBO' },
  { key: 'UNID', label: 'UNID', value: 'UNID' },
  { key: 'VASIL', label: 'VASIL', value: 'VASIL' },
  { key: 'VIDRO', label: 'VIDRO', value: 'VIDRO' },
];

export const cstOptions = [
  { key: '0', label: '0 - Nacional', value: '0' },
  {
    key: '1',
    label: '1 - Estrangeira, Importação direta',
    value: '1',
  },
  {
    key: '2',
    label: '2 - Estrangeira, Adquirida no mercado interno',
    value: '2',
  },
  {
    key: '3',
    label:
      '3 - Nacional, mercadoria ou bem com Conteúdo de Importação superior a 40% (quarenta por cento) e igual ou inferior a 70% (setenta por cento)',
    value: '3',
  },
  {
    key: '4',
    label:
      '4 - Nacional, cuja produção tenha sido feita em conformidade com os processos produtivos básicos de que tratam o Decreto-Lei nº 288/1967 , e as Leis nºs 8.248/1991, 8.387/1991, 10.176/2001 e 11.484/2007',
    value: '4',
  },
  {
    key: '5',
    label:
      '5 - Nacional, mercadoria ou bem com Conteúdo de Importação inferior ou igual a 40%',
    value: '5',
  },
  {
    key: '6',
    label:
      '6 - Estrangeira, Importação direta, sem similar nacional, constante em lista de Resolução Camex e gás natural',
    value: '6',
  },
  {
    key: '7',
    label:
      '7 - Estrangeira, Adquirida no mercado interno, sem similar nacional, constante em lista de Resolução Camex e gás natural',
    value: '7',
  },
  {
    key: '8',
    label:
      '8 - Nacional, Mercadoria ou bem com Conteúdo de Importação superior a 70% (setenta por cento)',
    value: '8',
  },
];

export const icmsTaxationOptions = [
  {
    key: '00',
    label: '00 – Tributada integralmente',
    value: '00',
  },
  {
    key: '10',
    label: '10 – Tributada e com cobrança do ICMS por substituição tributária',
    value: '10',
  },
  {
    key: '20',
    label: '20 – Com redução de base de cálculo',
    value: '20',
  },
  {
    key: '30',
    label:
      '30 – Isenta ou não tributada e com cobrança do ICMS por substituição tributária',
    value: '30',
  },
  {
    key: '40',
    label: '40 – Isenta',
    value: '40',
  },
  {
    key: '41',
    label: '41 – Não tributada',
    value: '41',
  },
  {
    key: '50',
    label: '50 – Suspensão',
    value: '50',
  },
  {
    key: '51',
    label: '51 – Diferimento',
    value: '51',
  },
  {
    key: '60',
    label: '60 – ICMS cobrado anteriormente por substituição tributária',
    value: '60',
  },
  {
    key: '70',
    label:
      '70 – Com redução de base de cálculo e cobrança do ICMS por substituição tributária',
    value: '70',
  },
  {
    key: '90',
    label: '90 – Outras',
    value: '90',
  },
];

export const sellInvoicesColumns = [
  {
    title: 'CNPJ/CPF',
    dataIndex: ['receiver', 'cnpjCpf'],
    key: 'cnpjCpf',
    render: (text, record) => <span>{cpfCnpjFormatter(text)}</span>,
  },
  {
    title: 'Nome',
    dataIndex: ['receiver', 'name'],
    key: 'nome',
  },
  {
    title: 'Data de Emissão',
    dataIndex: ['receiver', 'emissionDate'],
    key: 'dataEmissao',
    render: (text, record) => <span>{dateFormatter(text)}</span>,
  },
  {
    title: 'Valor da Nota Fiscal',
    dataIndex: ['taxes', 'totalInvoiceValue'],
    key: 'valorNotaFiscal',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
];

export const serviceInvoicesColumns = [
  {
    title: 'CNPJ/CPF',
    dataIndex: ['taker', 'cnpj_Cpf'],
    key: 'cnpjCpf',
    render: (text, record) => <span>{cpfCnpjFormatter(text)}</span>,
  },
  {
    title: 'Nome',
    dataIndex: ['taker', 'name'],
    key: 'nome',
  },
  {
    title: 'Data de Emissão',
    dataIndex: 'dataEmissão',
    key: 'dataEmissão',
    render: (text, record) => <span>{dateFormatter(text)}</span>,
  },
  {
    title: 'Valor da Nota Fiscal',
    dataIndex: ['demonstrative', 'liquidValue'],
    key: 'valorNotaFiscal',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
];
