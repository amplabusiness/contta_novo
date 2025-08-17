import { Link } from 'react-router-dom';

import { currencyFormatter } from '@/utils/formatters';

export const apuracaoColumns = [
  {
    title: 'Descrição',
    dataIndex: 'descricaoCfop',
    key: 'descricaoCfop',
    width: '70%',
  },
  {
    title: 'Valor',
    dataIndex: 'totalNfe',
    key: 'totalNfe',
    width: '20%',
    render: (text, record) => (
      <span style={{ color: '#5EAEEA', fontWeight: 600 }}>
        {currencyFormatter(text)}
      </span>
    ),
  },
];

export const apuracaoSaldoColumns = [
  {
    title: 'Total',
    dataIndex: 'description',
    key: 'description',
    width: '80%',
    render: (text, record) => <span style={{ fontWeight: 700 }}>{text}</span>,
  },
  {
    title: 'Valor',
    dataIndex: 'value',
    key: 'value',
    width: '20%',
    render: (text, record) => {
      const isPercentageRow =
        record.description.toLowerCase() === 'porcentagem';

      return (
        <span style={{ color: '#5EAEEA', fontWeight: 600 }}>
          {isPercentageRow
            ? `${Number(text).toFixed(2)}%`
            : currencyFormatter(text)}
        </span>
      );
    },
  },
];

export const apuracaoSegregacaoIcmsStColumns = [
  {
    title: 'ICMS/ST',
    dataIndex: ['icmsSt', 'total'],
    key: 'icmsSt',
    render: (text, record) =>
      text > 0 ? (
        <Link
          to={{
            pathname: '/apuracao/listagemProdutos',
            state: {
              nfeIds: record.icmsSt.listIdNfe,
              group: 'IcmsStPisConfins',
              type: 'IcmsSt',
            },
          }}
        >
          {currencyFormatter(text)}
        </Link>
      ) : (
        <span>{currencyFormatter(text)}</span>
      ),
  },
  {
    title: 'Isento',
    dataIndex: ['isento', 'total'],
    key: 'isento',
    render: (text, record) =>
      text > 0 ? (
        <Link
          to={{
            pathname: '/apuracao/listagemProdutos',
            state: {
              nfeIds: record.isento.listIdNfe,
              group: 'IcmsStPisConfins',
              type: 'Isento',
            },
          }}
        >
          {currencyFormatter(text)}
        </Link>
      ) : (
        <span>{currencyFormatter(text)}</span>
      ),
  },
  {
    title: 'Imune',
    dataIndex: ['imune', 'total'],
    key: 'imune',
    render: (text, record) =>
      text > 0 ? (
        <Link
          to={{
            pathname: '/apuracao/listagemProdutos',
            state: {
              nfeIds: record.imune.listIdNfe,
              group: 'IcmsStPisConfins',
              type: 'Imune',
            },
          }}
        >
          {currencyFormatter(text)}
        </Link>
      ) : (
        <span>{currencyFormatter(text)}</span>
      ),
  },
  {
    title: 'Exigibilidade Suspensa',
    dataIndex: ['exigSuspensa', 'total'],
    key: 'exigibilidadeSupensa',
    render: (text, record) =>
      text > 0 ? (
        <Link
          to={{
            pathname: '/apuracao/listagemProdutos',
            state: {
              nfeIds: record.exigSuspensa.listIdNfe,
              group: 'IcmsStPisConfins',
              type: 'ExigSuspensa',
            },
          }}
        >
          {currencyFormatter(text)}
        </Link>
      ) : (
        <span>{currencyFormatter(text)}</span>
      ),
  },
  {
    title: 'Lançamento de Ofício',
    dataIndex: ['lancamentoOficio', 'total'],
    key: 'lancamentoOficio',
    render: (text, record) =>
      text > 0 ? (
        <Link
          to={{
            pathname: '/apuracao/listagemProdutos',
            state: {
              nfeIds: record.lancamentoOficio.listIdNfe,
              group: 'IcmsStPisConfins',
              type: 'LancamentoOficio',
            },
          }}
        >
          {currencyFormatter(text)}
        </Link>
      ) : (
        <span>{currencyFormatter(text)}</span>
      ),
  },
  {
    title: 'Isenção/Redução',
    dataIndex: ['isencaoReducao', 'total'],
    key: 'isencaoReducao',
    render: (text, record) =>
      text > 0 ? (
        <Link
          to={{
            pathname: '/apuracao/listagemProdutos',
            state: {
              nfeIds: record.isencaoReducao.listIdNfe,
              group: 'IcmsStPisConfins',
              type: 'IsencaoReducao',
            },
          }}
        >
          {currencyFormatter(text)}
        </Link>
      ) : (
        <span>{currencyFormatter(text)}</span>
      ),
  },
  {
    title: 'Isenção/Redução Cesta Básica',
    dataIndex: ['isencaoReducaoCestaBasica', 'total'],
    key: 'isencaoReducaoCestaBasica',
    render: (text, record) =>
      text > 0 ? (
        <Link
          to={{
            pathname: '/apuracao/listagemProdutos',
            state: {
              nfeIds: record.isencaoReducaoCestaBasica.listIdNfe,
              group: 'IcmsStPisConfins',
              type: 'IsencaoReducaoCestaBasica',
            },
          }}
        >
          {currencyFormatter(text)}
        </Link>
      ) : (
        <span>{currencyFormatter(text)}</span>
      ),
  },
  {
    title: 'Antecipação com Encerr. Tributação',
    dataIndex: ['antEncTributacao', 'total'],
    key: 'antecipacaoEncerrTributacao',
    render: (text, record) =>
      text > 0 ? (
        <Link
          to={{
            pathname: '/apuracao/listagemProdutos',
            state: {
              nfeIds: record.antEncTributacao.listIdNfe,
              group: 'IcmsStPisConfins',
              type: 'AntEncTributacao',
            },
          }}
        >
          {currencyFormatter(text)}
        </Link>
      ) : (
        <span>{currencyFormatter(text)}</span>
      ),
  },
];

export const apuracaoSegregacaoPisCofinsColumns = [
  {
    title: 'Exigibilidade Suspensa',
    dataIndex: ['exigSuspensaMono', 'total'],
    key: 'exigibilidadeSupensa',
    render: (text, record) =>
      text > 0 ? (
        <Link
          to={{
            pathname: '/apuracao/listagemProdutos',
            state: {
              nfeIds: record.exigSuspensaMono.listIdNfe,
              group: 'IcmsStPisConfins',
              type: 'ExigSuspensaMono',
            },
          }}
        >
          {currencyFormatter(text)}
        </Link>
      ) : (
        <span>{currencyFormatter(text)}</span>
      ),
  },
  {
    title: 'Lançamento de Ofício',
    dataIndex: ['lancamentoOficioMono', 'total'],
    key: 'lancamentoOficio',
    render: (text, record) =>
      text > 0 ? (
        <Link
          to={{
            pathname: '/apuracao/listagemProdutos',
            state: {
              nfeIds: record.lancamentoOficioMono.listIdNfe,
              group: 'IcmsStPisConfins',
              type: 'LancamentoOficioMono',
            },
          }}
        >
          {currencyFormatter(text)}
        </Link>
      ) : (
        <span>{currencyFormatter(text)}</span>
      ),
  },
  {
    title: 'Substituição Tributária',
    dataIndex: ['subTributariaMono', 'total'],
    key: 'substituicaoTributaria',
    render: (text, record) =>
      text > 0 ? (
        <Link
          to={{
            pathname: '/apuracao/listagemProdutos',
            state: {
              nfeIds: record.subTributariaMono.listIdNfe,
              group: 'IcmsStPisConfins',
              type: 'SubTributariaMono',
            },
          }}
        >
          {currencyFormatter(text)}
        </Link>
      ) : (
        <span>{currencyFormatter(text)}</span>
      ),
  },
  {
    title: 'Tributação Monofásica',
    dataIndex: ['ncmMono', 'total'],
    key: 'tributacaoMonofasica',
    render: (text, record) =>
      text > 0 ? (
        <Link
          to={{
            pathname: '/apuracao/listagemProdutos',
            state: {
              nfeIds: record.ncmMono.listIdNfe,
              group: 'IcmsStPisConfins',
              type: 'NcmMono',
            },
          }}
        >
          {currencyFormatter(text)}
        </Link>
      ) : (
        <span>{currencyFormatter(text)}</span>
      ),
  },
];

export const apuracaoSegregacaoNotasColumns = [
  {
    title: 'Canceladas',
    dataIndex: ['canceladas', 'total'],
    key: 'canceladas',
    render: (text, record) =>
      text > 0 ? (
        <Link
          to={{
            pathname: '/apuracao/listagemProdutos',
            state: {
              nfeIds: record.canceladas.listIdNfe,
              group: 'IcmsStPisConfins',
              type: 'Cancelada',
            },
          }}
        >
          {currencyFormatter(text)}
        </Link>
      ) : (
        <span>{currencyFormatter(text)}</span>
      ),
  },
  {
    title: 'Devolução',
    dataIndex: ['devolucao', 'total'],
    key: 'devolucao',
    render: (text, record) =>
      text > 0 ? (
        <Link
          to={{
            pathname: '/apuracao/listagemProdutos',
            state: {
              nfeIds: record.devolucao.listIdNfe,
              group: 'IcmsStPisConfins',
              type: 'Devolucao',
            },
          }}
        >
          {currencyFormatter(text)}
        </Link>
      ) : (
        <span>{currencyFormatter(text)}</span>
      ),
  },
  {
    title: 'Transferida para Filiais',
    dataIndex: ['transferenciaMercadoria', 'total'],
    key: 'transferidaFiliais',
    render: (text, record) =>
      text > 0 ? (
        <Link
          to={{
            pathname: '/apuracao/listagemProdutos',
            state: {
              nfeIds: record.transferenciaMercadoria.listIdNfe,
              group: 'IcmsStPisConfins',
              type: 'Transferencia',
            },
          }}
        >
          {currencyFormatter(text)}
        </Link>
      ) : (
        <span>{currencyFormatter(text)}</span>
      ),
  },
];

export const apuracaoServicoColumns = [
  {
    title: 'CNPJ Emitente',
    dataIndex: 'cnpjEmitente',
    key: 'cnpjEmitente',
  },
  {
    title: 'Município de Incidência',
    dataIndex: 'municipioIncidencia',
    key: 'municipioIncidencia',
  },
  {
    title: 'Cód. Município',
    dataIndex: 'codigoMunicipio',
    key: 'codigoMunicipio',
  },
  {
    title: 'Valor ISS',
    dataIndex: 'valorTotalIss',
    key: 'valorTotalIss',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Valor INSS',
    dataIndex: 'valorToatlInss',
    key: 'valorToatlInss',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Deduções',
    dataIndex: 'valorTotalDeducoes',
    key: 'valorTotalDeducoes',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Valor Total do Serviço',
    dataIndex: 'valorTotalServicos',
    key: 'valorTotalServicos',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
];

export const apuracaoTransporteColumns = [
  {
    title: 'Nº Nota Fiscal',
    dataIndex: 'numNfe',
    key: 'numNfe',
  },
  {
    title: 'CNPJ',
    dataIndex: 'cnpj',
    key: 'cnpj',
  },
  {
    title: 'Tipo de Frete',
    dataIndex: 'tipoFrete',
    key: 'tipoFrete',
  },
  {
    title: 'Estado',
    dataIndex: 'estado',
    key: 'estado',
  },
  {
    title: 'Tipo CTE',
    dataIndex: 'tipoCte',
    key: 'tipoCte',
  },
  {
    title: 'Desconto',
    dataIndex: 'vlDesconto',
    key: 'vlDesconto',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Valor ICMS',
    dataIndex: 'vlIcms',
    key: 'vlIcms',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Valor Total Serviço',
    dataIndex: 'vlTotalServico',
    key: 'vlTotalServico',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Valor Total NFe',
    dataIndex: 'totalNfe',
    key: 'totalNfe',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
];

export const apuracaoDetalhamentoColumns = [
  {
    title: 'CFOP',
    dataIndex: 'cfop',
    key: 'cfop',
    sorter: (a, b) => a.cfop - b.cfop,
    sortDirections: ['ascend', 'descend', 'ascend'],
  },
  {
    title: 'Chave de Acesso',
    dataIndex: 'chaveAcesso',
    key: 'chaveAcesso',
  },
  {
    title: 'Natureza Operação',
    dataIndex: 'naturezaOperacao',
    key: 'naturezaOperacao',
  },
  {
    title: 'Mod',
    dataIndex: 'mod',
    key: 'mod',
  },
  {
    title: 'Série',
    dataIndex: 'serie',
    key: 'serie',
  },
  {
    title: 'Nº Doc',
    dataIndex: 'numDocumento',
    key: 'numDocumento',
  },
  {
    title: 'Data Emissão',
    dataIndex: 'dataEmissao',
    key: 'dataEmissao',
  },
  {
    title: 'Período',
    dataIndex: 'periodo',
    key: 'periodo',
  },
  {
    title: 'CNPJ Emitente',
    dataIndex: 'cnpjEmitente',
    key: 'cnpjEmitente',
  },
  {
    title: 'Razão Social Emitente',
    dataIndex: 'razaoSocialEmitente',
    key: 'razaoSocialEmitente',
  },
  {
    title: 'CNPJ Destinatário',
    dataIndex: 'cnpjDestinatario',
    key: 'cnpjDestinatario',
  },
  {
    title: 'Item',
    dataIndex: 'item',
    key: 'item',
  },
  {
    title: 'Item',
    dataIndex: 'item',
    key: 'item',
  },
  {
    title: 'Cód Item',
    dataIndex: 'codigoItem',
    key: 'codigoItem',
  },
  {
    title: 'Descrição Item',
    dataIndex: 'descricaoItem',
    key: 'descricaoItem',
  },
  {
    title: 'NCM',
    dataIndex: 'ncm',
    key: 'ncm',
  },
  {
    title: 'Valor Produto',
    dataIndex: 'valorProduto',
    key: 'valorProduto',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Valor Desconto',
    dataIndex: 'valorDesconto',
    key: 'valorDesconto',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Valor Acresc/Frete',
    dataIndex: 'valorAcresFrete',
    key: 'valorAcresFrete',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
  {
    title: 'Valor Líquido',
    dataIndex: 'valorLiquido',
    key: 'valorLiquido',
    render: (text, record) => <span>{currencyFormatter(text)}</span>,
  },
];

export const spreadsheetHeaders = [
  {
    key: 'cfop',
    title: 'CFOP',
  },
  {
    key: 'totalProdutos',
    title: 'Total dos Produtos',
  },
  {
    key: 'totalDesconto',
    title: 'Desconto',
  },
  {
    key: 'totalSeguro',
    title: 'Seguro',
  },
  {
    key: 'totalFrete',
    title: 'Frete',
  },
  {
    key: 'totalIpi',
    title: 'IPI',
  },
  {
    key: 'outros',
    title: 'Outros',
  },
  {
    key: 'totalIcmsSt',
    title: 'ICMS/ST',
  },
  {
    key: 'totalIcmsDesc',
    title: 'ICMS Destacado',
  },
  {
    key: 'totalNfe',
    title: 'Total das Notas Fiscais',
  },
];

export const detalhamentoSpreadsheetHeaders = [
  {
    key: 'cfop',
    title: 'CFOP',
  },
  {
    key: 'chaveAcesso',
    title: 'Chave de Acesso',
  },
  {
    key: 'naturezaOperacao',
    title: 'Natureza Operação',
  },
  {
    key: 'mod',
    title: 'Mod',
  },
  {
    key: 'serie',
    title: 'Série',
  },
  {
    key: 'numDocumento',
    title: 'Nº Doc',
  },
  {
    key: 'dataEmissao',
    title: 'Data Emissão',
  },
  {
    key: 'periodo',
    title: 'Período',
  },
  {
    key: 'cnpjEmitente',
    title: 'CNPJ Emitente',
  },
  {
    key: 'razaoSocialEmitente',
    title: 'Razão Social Emitente',
  },
  {
    key: 'cnpjDestinatario',
    title: 'CNPJ Destinatário',
  },
  {
    key: 'item',
    title: 'Item',
  },
  {
    key: 'item',
    title: 'Item',
  },
  {
    key: 'codigoItem',
    title: 'Cód Item',
  },
  {
    key: 'descricaoItem',
    title: 'Descrição Item',
  },
  {
    key: 'ncm',
    title: 'NCM',
  },
  {
    key: 'valorProduto',
    title: 'Valor Produto',
  },
  {
    key: 'valorDesconto',
    title: 'Valor Desconto',
  },
  {
    key: 'valorAcresFrete',
    title: 'Valor Acresc/Frete',
  },
  {
    key: 'valorLiquido',
    title: 'Valor Líquido',
  },
];
