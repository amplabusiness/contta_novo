import { Col, Row } from 'antd';

import Skeleton from '@/components/Shimmer/Skeleton';

import { Container } from './styles';

const TabelaEmpresasShimmer = () => {
  return (
    <Container>
      <Row>
        <Col xs={24}>
          <Skeleton className="input" />
        </Col>
      </Row>
      <Skeleton className="table" />
    </Container>
  );
};

export default TabelaEmpresasShimmer;
