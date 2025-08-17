import { Col, Row, Table } from 'antd';
import { Link } from 'react-router-dom';

import useCalculation from '@/services/api/hooks/app/Apuracao/useCalculation';
import { apuracaoServicoColumns } from '@/pages/app/Apuracao/constants';

import EmptyTable from '@/components/EmptyTable';
import ErrorMessage from '@/components/ErrorMessage';
import Shimmer from '@/components/Shimmer/Apuracao';

import { Title } from '@/styles/global';
import { Container, SectionTitle } from './styles';

const OverviewTab = () => {
  const { data = {}, isLoading, isError } = useCalculation();

  const { entries = [], outs = [] } = data;

  if (isError) {
    return <ErrorMessage />;
  }

  if (isLoading) {
    return <Shimmer />;
  }

  return (
    <Container>
      <Title>
        <h2>Apuração</h2>
        <p>
          A apuração realizada para as aquisições e as prestações da empresa
          está listada abaixo.
        </p>
      </Title>
      <Row gutter={[24, 25]} style={{ marginTop: 25 }}>
        <Col xs={24}>
          <SectionTitle>
            <Link to="/dashboard/servicos?operacao=Prestador">Prestações</Link>
          </SectionTitle>
          {entries.length > 0 ? (
            <Table
              columns={apuracaoServicoColumns}
              dataSource={entries}
              pagination={false}
              size="small"
              rowKey="id"
              scroll={{ x: 'max-content' }}
              style={{ marginTop: 20 }}
            />
          ) : (
            <EmptyTable title="Nenhuma apuração de entradas encontrada" />
          )}
        </Col>
        <Col xs={24}>
          <SectionTitle>
            <Link to="/dashboard/servicos?operacao=Aquisicao">Aquisições</Link>
          </SectionTitle>
          {outs.length > 0 ? (
            <Table
              columns={apuracaoServicoColumns}
              dataSource={outs}
              pagination={false}
              size="small"
              rowKey="id"
              scroll={{ x: 'max-content' }}
              style={{ marginTop: 20 }}
            />
          ) : (
            <EmptyTable title="Nenhuma apuração de saídas encontrada" />
          )}
        </Col>
      </Row>
    </Container>
  );
};

export default OverviewTab;
