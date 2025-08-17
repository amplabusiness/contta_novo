import { Col, Row, Table } from 'antd';

import useRevenueSegregation from '@/services/api/hooks/app/Apuracao/useRevenueSegregation';
import {
  apuracaoSegregacaoIcmsStColumns,
  apuracaoSegregacaoPisCofinsColumns,
  apuracaoSegregacaoNotasColumns,
} from '@/pages/app/Apuracao/constants';

import ErrorMessage from '@/components/ErrorMessage';
import Shimmer from '@/components/Shimmer/Apuracao';

import { Title } from '@/styles/global';
import { Container } from './styles';

const SegregationTab = () => {
  const { data = {}, isLoading, isError } = useRevenueSegregation();

  const { icmsSt = [], pisCofins = [], invoices = [] } = data;

  if (isError) {
    return <ErrorMessage />;
  }

  if (isLoading) {
    return <Shimmer />;
  }

  return (
    <Container>
      <Title>
        <h2>ICMS/ST</h2>
        <p>Valores da apuração para os produtos que são ICMS/ST</p>
      </Title>
      <Row gutter={[24, 0]}>
        <Col xs={24}>
          <Table
            columns={apuracaoSegregacaoIcmsStColumns}
            dataSource={icmsSt}
            pagination={false}
            size="small"
            rowKey="id"
            scroll={{ x: 'max-content' }}
            style={{ marginTop: 20 }}
          />
        </Col>
      </Row>

      <Row gutter={[24, 0]}>
        <Col xs={24} lg={16} style={{ marginTop: 40 }}>
          <Title>
            <h2>PIS/Cofins</h2>
            <p>Valores da apuração para os produtos que são PIS/Cofins</p>
          </Title>
          <Table
            columns={apuracaoSegregacaoPisCofinsColumns}
            dataSource={pisCofins}
            pagination={false}
            size="small"
            rowKey="id"
            scroll={{ x: 'max-content' }}
            style={{ marginTop: 20 }}
          />
        </Col>
        <Col xs={24} lg={8} style={{ marginTop: 40 }}>
          <Title>
            <h2>Notas Fiscais</h2>
            <p>Valores da apuração de suas notas fiscais</p>
          </Title>
          <Table
            columns={apuracaoSegregacaoNotasColumns}
            dataSource={invoices}
            pagination={false}
            size="small"
            rowKey="id"
            scroll={{ x: 'max-content' }}
            style={{ marginTop: 20 }}
          />
        </Col>
      </Row>
    </Container>
  );
};

export default SegregationTab;
