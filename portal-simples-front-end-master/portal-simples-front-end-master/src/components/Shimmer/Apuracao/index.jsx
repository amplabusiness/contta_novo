import { Col, Row } from 'antd';

import Skeleton from '@/components/Shimmer/Skeleton';

import { Container } from './styles';

const ApuracaoShimmer = () => {
  return (
    <Container>
      <Skeleton className="title" />
      <Row gutter={[24, 0]}>
        <Col xs={24} style={{ marginTop: 20 }}>
          <Skeleton className="title secondary" />
          <Skeleton className="table" />
        </Col>
      </Row>
      <Row>
        <Col xs={24} style={{ marginTop: 20 }}>
          <Skeleton className="title secondary" />
          <Skeleton className="table" />
        </Col>
      </Row>
    </Container>
  );
};

export default ApuracaoShimmer;
