export const icmsStNcmsColumns = [
  {
    title: 'NCM',
    dataIndex: 'ncm',
    align: 'center',
  },
];

export const icmsStProdutosIcmsColumns = [
  {
    title: 'Código',
    dataIndex: 'codProduto',
    key: 'codProduto',
    align: 'center',
  },
  {
    title: 'Descrição',
    dataIndex: 'descProduto',
    key: 'descProduto',
    align: 'center',
  },
];

export const regimeOptions = [
  { key: 'icmsSt', label: 'ICMS/ST', value: 'icmsSt' },
  { key: 'beneficios', label: 'Benefícios', value: 'beneficios' },
  { key: 'isento', label: 'Isento', value: 'isento' },
  { key: 'imune', label: 'Imune', value: 'imune' },
  {
    key: 'exigSuspensa',
    label: 'Exigibilidade Suspensa',
    value: 'exigSuspensa',
  },
  { key: 'isencaoReducao', label: 'Isenção/Redução', value: 'isencaoReducao' },
  {
    key: 'isencaoReducaoCestaBasica',
    label: 'Isenção/Redução Cesta Básica',
    value: 'isencaoReducaoCestaBasica',
  },
  {
    key: 'antEncTributacao',
    label: 'Antecipação com Encerr. Tributação',
    value: 'antEncTributacao',
  },
];

export const exigSuspensaTaxesOptions = [
  { key: 'COFINS', label: 'COFINS', value: 'COFINS' },
  { key: 'CSLL', label: 'CSLL', value: 'CSLL' },
  { key: 'INSS/CPP', label: 'INSS/CPP', value: 'INSS/CPP' },
  { key: 'IRPJ', label: 'IRPJ', value: 'IRPJ' },
  { key: 'ISS', label: 'ISS', value: 'ISS' },
  { key: 'PIS', label: 'PIS', value: 'PIS' },
];

export const exigSuspensaSuspensionOptions = [
  {
    key: 'Liminar em Mandado de Segurança',
    label: 'Liminar em Mandado de Segurança',
    value: 'Liminar em Mandado de Segurança',
  },
  {
    key: 'Depósito Judicial',
    label: 'Depósito Judicial',
    value: 'Depósito Judicial',
  },
  {
    key: 'Antecipação de Tutela',
    label: 'Antecipação de Tutela',
    value: 'Antecipação de Tutela',
  },
  {
    key: 'Liminar em Medida Cautelar',
    label: 'Liminar em Medida Cautelar',
    value: 'Liminar em Medida Cautelar',
  },
];

export const defaultTaxesTexts = {
  icmsSt: 'ICMS/ST',
  beneficios: 'benefício',
  isento: 'isentos',
  imune: 'imunes',
  isencaoReducao: 'isenção/redução',
  isencaoReducaoCestaBasica: 'isenção/redução cesta básica',
};
