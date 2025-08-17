import { Tooltip } from 'antd';

import { currencyFormatter } from '@/utils/formatters';

export const dashboardFaturamentoColumns = [
  {
    title: '',
    dataIndex: 'title',
    key: 'title',
    align: 'center',
    render: (text, record) => <strong>{text}</strong>,
  },
  {
    title: 'Janeiro',
    dataIndex: ['january', 'value'],
    key: 'january',
    align: 'center',
    render: (text, record) =>
      record.january.isStrange ? (
        <Tooltip title="Estouro de Caixa">
          <span style={{ color: '#ff0033' }}>{currencyFormatter(text)}</span>
        </Tooltip>
      ) : (
        <span>{currencyFormatter(text)}</span>
      ),
  },
  {
    title: 'Fevereiro',
    dataIndex: ['february', 'value'],
    key: 'february',
    align: 'center',
    render: (text, record) =>
      record.february.isStrange ? (
        <Tooltip title="Estouro de Caixa">
          <span style={{ color: '#ff0033' }}>{currencyFormatter(text)}</span>
        </Tooltip>
      ) : (
        <span>{currencyFormatter(text)}</span>
      ),
  },
  {
    title: 'Março',
    dataIndex: ['march', 'value'],
    key: 'march',
    align: 'center',
    render: (text, record) =>
      record.march.isStrange ? (
        <Tooltip title="Estouro de Caixa">
          <span style={{ color: '#ff0033' }}>{currencyFormatter(text)}</span>
        </Tooltip>
      ) : (
        <span>{currencyFormatter(text)}</span>
      ),
  },
  {
    title: 'Abril',
    dataIndex: ['april', 'value'],
    key: 'april',
    align: 'center',
    render: (text, record) =>
      record.april.isStrange ? (
        <Tooltip title="Estouro de Caixa">
          <span style={{ color: '#ff0033' }}>{currencyFormatter(text)}</span>
        </Tooltip>
      ) : (
        <span>{currencyFormatter(text)}</span>
      ),
  },
  {
    title: 'Maio',
    dataIndex: ['may', 'value'],
    key: 'may',
    align: 'center',
    render: (text, record) =>
      record.may.isStrange ? (
        <Tooltip title="Estouro de Caixa">
          <span style={{ color: '#ff0033' }}>{currencyFormatter(text)}</span>
        </Tooltip>
      ) : (
        <span>{currencyFormatter(text)}</span>
      ),
  },
  {
    title: 'Junho',
    dataIndex: ['june', 'value'],
    key: 'june',
    align: 'center',
    render: (text, record) =>
      record.june.isStrange ? (
        <Tooltip title="Estouro de Caixa">
          <span style={{ color: '#ff0033' }}>{currencyFormatter(text)}</span>
        </Tooltip>
      ) : (
        <span>{currencyFormatter(text)}</span>
      ),
  },
  {
    title: 'Julho',
    dataIndex: ['july', 'value'],
    key: 'july',
    align: 'center',
    render: (text, record) =>
      record.july.isStrange ? (
        <Tooltip title="Estouro de Caixa">
          <span style={{ color: '#ff0033' }}>{currencyFormatter(text)}</span>
        </Tooltip>
      ) : (
        <span>{currencyFormatter(text)}</span>
      ),
  },
  {
    title: 'Agosto',
    dataIndex: ['august', 'value'],
    key: 'august',
    align: 'center',
    render: (text, record) =>
      record.august.isStrange ? (
        <Tooltip title="Estouro de Caixa">
          <span style={{ color: '#ff0033' }}>{currencyFormatter(text)}</span>
        </Tooltip>
      ) : (
        <span>{currencyFormatter(text)}</span>
      ),
  },
  {
    title: 'Setembro',
    dataIndex: ['september', 'value'],
    key: 'september',
    align: 'center',
    render: (text, record) =>
      record.september.isStrange ? (
        <Tooltip title="Estouro de Caixa">
          <span style={{ color: '#ff0033' }}>{currencyFormatter(text)}</span>
        </Tooltip>
      ) : (
        <span>{currencyFormatter(text)}</span>
      ),
  },
  {
    title: 'Outubro',
    dataIndex: ['october', 'value'],
    key: 'october',
    align: 'center',
    render: (text, record) =>
      record.october.isStrange ? (
        <Tooltip title="Estouro de Caixa">
          <span style={{ color: '#ff0033' }}>{currencyFormatter(text)}</span>
        </Tooltip>
      ) : (
        <span>{currencyFormatter(text)}</span>
      ),
  },
  {
    title: 'Novembro',
    dataIndex: ['november', 'value'],
    key: 'november',
    align: 'center',
    render: (text, record) =>
      record.november.isStrange ? (
        <Tooltip title="Estouro de Caixa">
          <span style={{ color: '#ff0033' }}>{currencyFormatter(text)}</span>
        </Tooltip>
      ) : (
        <span>{currencyFormatter(text)}</span>
      ),
  },
  {
    title: 'Dezembro',
    dataIndex: ['december', 'value'],
    key: 'december',
    align: 'center',
    render: (text, record) =>
      record.december.isStrange ? (
        <Tooltip title="Estouro de Caixa">
          <span style={{ color: '#ff0033' }}>{currencyFormatter(text)}</span>
        </Tooltip>
      ) : (
        <span>{currencyFormatter(text)}</span>
      ),
  },
];

export const dashboardImpostosColumns = {
  'Anexo I': [
    {
      title: 'IRPJ',
      dataIndex: 'irpj',
    },
    {
      title: 'CSLL',
      dataIndex: 'csll',
    },
    {
      title: 'Cofins',
      dataIndex: 'cofins',
    },
    {
      title: 'PIS/PASEP',
      dataIndex: 'pis',
    },
    {
      title: 'CPP',
      dataIndex: 'cpp',
    },
    {
      title: 'ICMS',
      dataIndex: 'icms',
    },
  ],
  'Anexo II': [
    {
      title: 'IRPJ',
      dataIndex: 'irpj',
    },
    {
      title: 'CSLL',
      dataIndex: 'csll',
    },
    {
      title: 'Cofins',
      dataIndex: 'cofins',
    },
    {
      title: 'PIS/PASEP',
      dataIndex: 'pis',
    },
    {
      title: 'CPP',
      dataIndex: 'cpp',
    },
    {
      title: 'IPI',
      dataIndex: 'ipi',
    },
    {
      title: 'ICMS',
      dataIndex: 'icms',
    },
  ],
  'Anexo III': [
    {
      title: 'IRPJ',
      dataIndex: 'irpj',
    },
    {
      title: 'CSLL',
      dataIndex: 'csll',
    },
    {
      title: 'Cofins',
      dataIndex: 'cofins',
    },
    {
      title: 'PIS/PASEP',
      dataIndex: 'pis',
    },
    {
      title: 'CPP',
      dataIndex: 'cpp',
    },
    {
      title: 'ISS',
      dataIndex: 'iss',
    },
  ],
  'Anexo IV': [
    {
      title: 'IRPJ',
      dataIndex: 'irpj',
    },
    {
      title: 'CSLL',
      dataIndex: 'csll',
    },
    {
      title: 'Cofins',
      dataIndex: 'cofins',
    },
    {
      title: 'PIS/PASEP',
      dataIndex: 'pis',
    },
    {
      title: 'ICMS',
      dataIndex: 'icms',
    },
    {
      title: 'ISS',
      dataIndex: 'iss',
    },
  ],
  'Anexo V': [
    {
      title: 'IRPJ',
      dataIndex: 'irpj',
    },
    {
      title: 'CSLL',
      dataIndex: 'csll',
    },
    {
      title: 'Cofins',
      dataIndex: 'cofins',
    },
    {
      title: 'PIS/PASEP',
      dataIndex: 'pis',
    },
    {
      title: 'CPP',
      dataIndex: 'cpp',
    },
    {
      title: 'ISS',
      dataIndex: 'iss',
    },
  ],
  'Anexo I-III': [
    {
      title: 'IRPJ',
      dataIndex: 'irpj',
    },
    {
      title: 'CSLL',
      dataIndex: 'csll',
    },
    {
      title: 'Cofins',
      dataIndex: 'cofins',
    },
    {
      title: 'PIS/PASEP',
      dataIndex: 'pis',
    },
    {
      title: 'CPP',
      dataIndex: 'cpp',
    },
    {
      title: 'ICMS',
      dataIndex: 'icms',
    },
  ],
};

export const dashboardImpostosEfetivosColumns = {
  'Anexo I': [
    {
      title: 'IRPJ',
      dataIndex: 'irpj',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'CSLL',
      dataIndex: 'csll',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'Cofins',
      dataIndex: 'cofins',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'PIS/PASEP',
      dataIndex: 'pis',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'CPP',
      dataIndex: 'cpp',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'ICMS',
      dataIndex: 'icms',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
  ],
  'Anexo II': [
    {
      title: 'IRPJ',
      dataIndex: 'irpj',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'CSLL',
      dataIndex: 'csll',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'Cofins',
      dataIndex: 'cofins',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'PIS/PASEP',
      dataIndex: 'pis',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'CPP',
      dataIndex: 'cpp',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'IPI',
      dataIndex: 'ipi',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'ICMS',
      dataIndex: 'icms',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
  ],
  'Anexo III': [
    {
      title: 'IRPJ',
      dataIndex: 'irpj',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'CSLL',
      dataIndex: 'csll',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'Cofins',
      dataIndex: 'cofins',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'PIS/PASEP',
      dataIndex: 'pis',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'CPP',
      dataIndex: 'cpp',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'ISS',
      dataIndex: 'iss',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
  ],
  'Anexo IV': [
    {
      title: 'IRPJ',
      dataIndex: 'irpj',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'CSLL',
      dataIndex: 'csll',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'Cofins',
      dataIndex: 'cofins',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'PIS/PASEP',
      dataIndex: 'pis',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'ICMS',
      dataIndex: 'icms',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'ISS',
      dataIndex: 'iss',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
  ],
  'Anexo V': [
    {
      title: 'IRPJ',
      dataIndex: 'irpj',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'CSLL',
      dataIndex: 'csll',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'Cofins',
      dataIndex: 'cofins',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'PIS/PASEP',
      dataIndex: 'pis',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'CPP',
      dataIndex: 'cpp',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'ISS',
      dataIndex: 'iss',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
  ],
  'Anexo I-III': [
    {
      title: 'IRPJ',
      dataIndex: 'irpj',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'CSLL',
      dataIndex: 'csll',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'Cofins',
      dataIndex: 'cofins',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'PIS/PASEP',
      dataIndex: 'pis',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'CPP',
      dataIndex: 'cpp',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'ICMS',
      dataIndex: 'icms',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
  ],
};

export const dashboardPisCofinsColumns = [
  {
    title: 'Exig. Suspensa',
    dataIndex: ['exigSuspensaMono', 'total'],
    key: 'exigibilidadeSupensa',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Lanç. de Ofício',
    dataIndex: ['lancamentoOficioMono', 'total'],
    key: 'lancamentoOficio',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Subst. Tributária',
    dataIndex: ['subTributariaMono', 'total'],
    key: 'substituicaoTributaria',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Tribut. Monofásica',
    dataIndex: ['ncmMono', 'total'],
    key: 'tributacaoMonofasica',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
];

export const dashboardIcmsStColumns = [
  {
    title: 'ICMS/ST',
    dataIndex: ['icmsSt', 'total'],
    key: 'icmsSt',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Isento',
    dataIndex: ['isento', 'total'],
    key: 'isento',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Imune',
    dataIndex: ['imune', 'total'],
    key: 'imune',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Exig. Suspensa',
    dataIndex: ['exigSuspensa', 'total'],
    key: 'exigibilidadeSupensa',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
];

export const dashboardTransporteBaseCalculoColumns = [
  {
    title: 'Intramunicipal',
    dataIndex: 'intramunicipal',
    key: 'intramunicipal',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Intermunicipal',
    dataIndex: 'intermunicipal',
    key: 'intermunicipal',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Interestadual',
    dataIndex: 'interestadual',
    key: 'interestadual',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Total',
    dataIndex: 'total',
    key: 'total',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
];

export const dashboardInvoicesQuantityColumns = [
  {
    title: 'Número Inicial',
    dataIndex: 'numInicial',
    key: 'numInicial',
  },
  {
    title: 'Número Final',
    dataIndex: 'numFinal',
    key: 'numFinal',
  },
  {
    title: 'Notas Canceladas',
    dataIndex: 'notasCanceladas',
    key: 'notasCanceladas',
  },
  {
    title: 'Notas Inutilizadas',
    dataIndex: 'notasInutilizadas',
    key: 'notasInutilizadas',
  },
  {
    title: 'Notas Faltantes',
    dataIndex: 'notasFaltantes',
    key: 'notasFaltantes',
  },
  {
    title: 'Total',
    dataIndex: 'total',
    key: 'total',
  },
];
