import {
  currencyFormatter,
  dateFormatter,
  percentageFormatter,
} from '@/utils/formatters';

export const notasFiscaisMainColumns = [
  {
    title: 'Cód. situação doc.',
    dataIndex: 'documentSituationCode',
    key: 'documentSituationCode',
    align: 'center',
    render: (text, record) => <span>{text ?? '-'}</span>,
  },
  {
    title: 'Série',
    dataIndex: 'series',
    key: 'series',
    align: 'center',
    render: (text, record) => <span>{text ?? '-'}</span>,
  },
  {
    title: 'Data da emissão',
    dataIndex: 'emissionDate',
    key: 'emissionDate',
    align: 'center',
    render: (text, record) => <span>{dateFormatter(text)}</span>,
    sorter: (a, b) => new Date(a.emissionDate) - new Date(b.emissionDate),
    sortDirections: ['ascend', 'descend', 'ascend'],
  },
  {
    title: 'Chave da NFe',
    dataIndex: 'nfeKey',
    key: 'nfeKey',
    align: 'center',
    render: (text, record) => <span>{text ?? '-'}</span>,
  },
  {
    title: 'Data da entrada ou saída',
    dataIndex: 'inOrOutDate',
    key: 'inOrOutDate',
    align: 'center',
    render: (text, record) => <span>{dateFormatter(text)}</span>,
  },
  {
    title: 'Valor total do documento',
    dataIndex: 'totalDocumentValue',
    key: 'totalDocumentValue',
    align: 'center',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Tipo de pagamento',
    dataIndex: 'paymentType',
    key: 'paymentType',
    align: 'center',
    render: (text, record) => <span>{text ?? 'Não informado'}</span>,
  },
  {
    title: 'Valor do desconto',
    dataIndex: 'discountValue',
    key: 'discountValue',
    align: 'center',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Valor do seguro',
    dataIndex: 'insuranceValue',
    key: 'insuranceValue',
    align: 'center',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Valor de outras despesas',
    dataIndex: 'otherExpendituresValue',
    key: 'otherExpendituresValue',
    align: 'center',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Base de cálculo do ICMS',
    dataIndex: 'icmsCalculationBasis',
    key: 'icmsCalculationBasis',
    align: 'center',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Valor do ICMS',
    dataIndex: 'icmsValue',
    key: 'icmsValue',
    align: 'center',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Base de cálculo do ICMS ST',
    dataIndex: 'icmsStCalculationBasis',
    key: 'icmsStCalculationBasis',
    align: 'center',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'ICMS retido por ST',
    dataIndex: 'retainedIcms',
    key: 'retainedIcms',
    align: 'center',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Valor do IPI',
    dataIndex: 'ipiValue',
    key: 'ipiValue',
    align: 'center',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Valor da COFINS',
    dataIndex: 'cofinsValue',
    key: 'cofinsValue',
    align: 'center',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'PIS retido por ST',
    dataIndex: 'retainedPis',
    key: 'retainedPis',
    align: 'center',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'COFINS retido por ST',
    dataIndex: 'retainedCofins',
    key: 'retainedCofins',
    align: 'center',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
];

export const notasFiscaisFirstTabColumns = [
  {
    title: 'Código do item',
    dataIndex: 'itemCode',
    key: 'itemCode',
    align: 'center',
    render: (text, record) => <span>{text ?? '-'}</span>,
  },
  {
    title: 'Descrição complementar',
    dataIndex: 'additionalDescription',
    key: 'additionalDescription',
    align: 'center',
    render: (text, record) => <span>{text ?? '-'}</span>,
  },
  {
    title: 'Qtd. do item',
    dataIndex: 'itemQuantity',
    key: 'itemQuantity',
    align: 'center',
    render: (text, record) => <span>{text ?? '-'}</span>,
  },
  {
    title: 'Un. de medida',
    dataIndex: 'measureCode',
    key: 'measureCode',
    align: 'center',
    render: (text, record) => <span>{text ?? '-'}</span>,
  },
  {
    title: 'Valor total do item',
    dataIndex: 'totalItemValue',
    key: 'totalItemValue',
    align: 'center',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Valor do desconto',
    dataIndex: 'discountValue',
    key: 'discountValue',
    align: 'center',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'CST/ICMS',
    dataIndex: 'cstIcms',
    key: 'cstIcms',
    align: 'center',
    render: (text, record) => <span>{text ?? percentageFormatter(0)}</span>,
  },
  {
    title: 'CFOP',
    dataIndex: 'cfop',
    key: 'cfop',
    align: 'center',
  },
  {
    title: 'Código da natureza da operação',
    dataIndex: 'operationNatureCode',
    key: 'operationNatureCode',
    align: 'center',
    render: (text, record) => <span>{text ?? '-'}</span>,
  },
  {
    title: 'Base de cálculo do ICMS',
    dataIndex: 'icmsCalculationBasis',
    key: 'icmsCalculationBasis',
    align: 'center',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Alíquota do ICMS (%)',
    dataIndex: 'icmsAliquot',
    key: 'icmsAliquot',
    align: 'center',
    render: (text, record) => (
      <span>{text ? percentageFormatter(text) : percentageFormatter(0)}</span>
    ),
  },
  {
    title: 'Valor do ICMS',
    dataIndex: 'icmsValue',
    key: 'icmsValue',
    align: 'center',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Base de cálculo do ICMS ST',
    dataIndex: 'icmsStCalculationBasis',
    key: 'icmsStCalculationBasis',
    align: 'center',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Alíquota do ICMS ST (%)',
    dataIndex: 'icmsStAliquot',
    key: 'icmsStAliquot',
    align: 'center',
    render: (text, record) => (
      <span>{text ? percentageFormatter(text) : percentageFormatter(0)}</span>
    ),
  },
  {
    title: 'Valor do ICMS ST',
    dataIndex: 'icmsStValue',
    key: 'icmsStValue',
    align: 'center',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'CST/IPI',
    dataIndex: 'cstIpi',
    key: 'cstIpi',
    align: 'center',
    render: (text, record) => (
      <span>{text ? percentageFormatter(text) : percentageFormatter(0)}</span>
    ),
  },
  {
    title: 'Alíquota do IPI (%)',
    dataIndex: 'ipiAliquot',
    key: 'ipiAliquot',
    align: 'center',
    render: (text, record) => (
      <span>{text ? percentageFormatter(text) : percentageFormatter(0)}</span>
    ),
  },
  {
    title: 'Valor do IPI',
    dataIndex: 'ipiValue',
    key: 'ipiValue',
    align: 'center',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'CST/PIS',
    dataIndex: 'cstPis',
    key: 'cstPis',
    align: 'center',
    render: (text, record) => <span>{text ?? percentageFormatter(0)}</span>,
  },
  {
    title: 'Valor da Cofins',
    dataIndex: 'cofinsValue',
    key: 'cofinsValue',
    align: 'center',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
];

export const notasFiscaisSecondTabColumns = [
  {
    title: 'CST/ICMS',
    dataIndex: 'cstIcms',
    key: 'cstIcms',
    align: 'center',
    render: (text, record) => <span>{text ?? '-'}</span>,
  },
  {
    title: 'CFOP',
    dataIndex: 'cfop',
    key: 'cfop',
    align: 'center',
  },
  {
    title: 'Alíquota do ICMS (%)',
    dataIndex: 'icmsAliquot',
    key: 'icmsAliquot',
    align: 'center',
    render: (text, record) => <span>{text ?? '-'}</span>,
  },
  {
    title: 'Valor da operação',
    dataIndex: 'operationValue',
    key: 'operationValue',
    align: 'center',
    render: (text, record) => <span>{text ?? '-'}</span>,
  },
  {
    title: 'Base de cálculo do ICMS',
    dataIndex: 'icmsCalculationBasis',
    key: 'icmsCalculationBasis',
    align: 'center',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Valor do ICMS',
    dataIndex: 'icmsValue',
    key: 'icmsValue',
    align: 'center',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Base de cálculo do ICMS ST',
    dataIndex: 'icmsStCalculationBasis',
    key: 'icmsStCalculationBasis',
    align: 'center',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Valor do ICMS ST',
    dataIndex: 'icmsStValue',
    key: 'icmsStValue',
    align: 'center',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Valor do IPI',
    dataIndex: 'ipiValue',
    key: 'ipiValue',
    align: 'center',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
];

export const invoicesSpreadsheetHeaders = [
  {
    key: 'documentNumber',
    title: 'Número do Documento',
  },
  {
    key: 'documentSituationCode',
    title: 'Cód. situação doc.',
  },
  {
    key: 'series',
    title: 'Série',
  },
  {
    key: 'emissionDate',
    title: 'Data da emissão',
  },
  {
    key: 'nfeKey',
    title: 'Chave da NFe',
  },
  {
    key: 'inOrOutDate',
    title: 'Data da entrada ou saída',
  },
  {
    key: 'totalDocumentValue',
    title: 'Valor total do documento',
  },
  {
    key: 'paymentType',
    title: 'Tipo de pagamento',
  },
  {
    key: 'discountValue',
    title: 'Valor do desconto',
  },
  {
    key: 'insuranceValue',
    title: 'Valor do seguro',
  },
  {
    key: 'otherExpendituresValue',
    title: 'Valor de outras despesas',
  },
  {
    key: 'icmsCalculationBasis',
    title: 'Base de cálculo do ICMS',
  },
  {
    key: 'icmsValue',
    title: 'Valor do ICMS',
  },
  {
    key: 'icmsStCalculationBasis',
    title: 'Base de cálculo do ICMS ST',
  },
  {
    key: 'retainedIcms',
    title: 'ICMS retido por ST',
  },
  {
    key: 'ipiValue',
    title: 'Valor do IPI',
  },
  {
    key: 'cofinsValue',
    title: 'Valor da COFINS',
  },
  {
    key: 'retainedPis',
    title: 'PIS retido por ST',
  },
  {
    key: 'retainedCofins',
    title: 'COFINS retido por ST',
  },
];

export const itemsSpreadsheetHeaders = [
  {
    key: 'itemCode',
    title: 'Código do item',
  },
  {
    key: 'additionalDescription',
    title: 'Descrição complementar',
  },
  {
    key: 'itemQuantity',
    title: 'Qtd. do item',
  },
  {
    key: 'measureCode',
    title: 'Un. de medida',
  },
  {
    key: 'totalItemValue',
    title: 'Valor total do item',
  },
  {
    key: 'discountValue',
    title: 'Valor do desconto',
  },
  {
    key: 'cstIcms',
    title: 'CST/ICMS',
  },
  {
    key: 'cfop',
    title: 'CFOP',
  },
  {
    key: 'operationNatureCode',
    title: 'Código da natureza da operação',
  },
  {
    key: 'icmsCalculationBasis',
    title: 'Base de cálculo do ICMS',
  },
  {
    key: 'icmsAliquot',
    title: 'Alíquota do ICMS (%)',
  },
  {
    key: 'icmsValue',
    title: 'Valor do ICMS',
  },
  {
    key: 'icmsStCalculationBasis',
    title: 'Base de cálculo do ICMS ST',
  },
  {
    key: 'icmsStAliquot',
    title: 'Alíquota do ICMS ST (%)',
  },
  {
    key: 'icmsStValue',
    title: 'Valor do ICMS ST',
  },
  {
    key: 'cstIpi',
    title: 'CST/IPI',
  },
  {
    key: 'ipiAliquot',
    title: 'Alíquota do IPI (%)',
  },
  {
    key: 'ipiValue',
    title: 'Valor do IPI',
  },
  {
    key: 'cstPis',
    title: 'CST/PIS',
  },
  {
    key: 'cofinsValue',
    title: 'Valor da Cofins',
  },
];

export const analyticalSpreadsheetHeaders = [
  {
    key: 'cstIcms',
    title: 'CST/ICMS',
  },
  {
    key: 'cfop',
    title: 'CFOP',
  },
  {
    key: 'icmsAliquot',
    title: 'Alíquota do ICMS (%)',
  },
  {
    key: 'operationValue',
    title: 'Valor da operação',
  },
  {
    key: 'icmsCalculationBasis',
    title: 'Base de cálculo do ICMS',
  },
  {
    key: 'icmsValue',
    title: 'Valor do ICMS',
  },
  {
    key: 'icmsStCalculationBasis',
    title: 'Base de cálculo do ICMS ST',
  },
  {
    key: 'icmsStValue',
    title: 'Valor do ICMS ST',
  },
  {
    key: 'ipiValue',
    title: 'Valor do IPI',
  },
];
