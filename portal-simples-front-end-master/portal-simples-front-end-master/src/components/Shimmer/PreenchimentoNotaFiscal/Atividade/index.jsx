import { Col, Row } from 'antd';

import Skeleton from '@/components/Shimmer/Skeleton';

import { Container } from './styles';

const AtividadeShimmer = () => {
  return (
    <Container>
      <Row gutter={[24, 0]}>
        <Col xs={24} md={4} style={{ marginTop: 20 }}>
          <Skeleton className="label" />
          <Skeleton className="input" />
        </Col>
        <Col xs={24} md={20} style={{ marginTop: 20 }}>
          <Skeleton className="label" />
          <Skeleton className="input" />
        </Col>
      </Row>
    </Container>
  );
};

export default AtividadeShimmer;
