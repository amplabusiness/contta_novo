import { Card, Table } from 'antd';
import { useSelector } from 'react-redux';

import { dashboardImpostosEfetivosColumns } from '@/pages/app/DetalhamentoSimples/constants';
import { currencyFormatter } from '@/utils/formatters';

import { Container, Title } from '@/styles/global';
import { CardHeader, Content } from './styles';

const DetalhamentoSimples = () => {
  const {
    annex,
    data: { simplesNacional = {} },
  } = useSelector(state => state.activeCompanyState);

  const {
    valorDas = 0,
    impostos = {},
    impostosBasesCalculo = {},
    impostosEfetivosAliquotas = {},
    impostosEfetivosValores = {},
  } = simplesNacional;

  const data = [
    {
      id: 'aliquota',
      rowTitle: 'Alíquota Nominal',
      ...impostos,
    },
    {
      id: 'baseCalculo',
      rowTitle: 'Base de Cálculo',
      ...impostosBasesCalculo,
    },
    {
      id: 'aliquotaEfetiva',
      rowTitle: 'Alíquota Efetiva',
      ...impostosEfetivosAliquotas,
    },
    {
      id: 'valorImposto',
      rowTitle: 'Valor do Imposto',
      ...impostosEfetivosValores,
    },
  ];

  const columns = [
    {
      title: '',
      dataIndex: 'rowTitle',
      key: 'rowTitle',
      render: (text, record) => (
        <span style={{ fontWeight: 'bold' }}>{text}</span>
      ),
    },
    ...dashboardImpostosEfetivosColumns[annex],
  ];

  return (
    <Container>
      <Title>
        <h2>Detalhamento do Simples Nacional</h2>
        <p>
          Abaixo encontram-se todos os valores que compõe o valor do Simples
          Nacional, separados por anexo.
        </p>
      </Title>

      <Content>
        <Card
          title={
            <CardHeader>
              <p>
                <strong>Anexo:</strong> {annex}
              </p>
              <p>
                <strong>Valor DAS:</strong> {currencyFormatter(valorDas)}
              </p>
            </CardHeader>
          }
        >
          <Table
            dataSource={data}
            columns={columns}
            pagination={false}
            size="small"
            rowKey="id"
            scroll={{ x: 'max-content' }}
          />
        </Card>
      </Content>
    </Container>
  );
};

export default DetalhamentoSimples;
