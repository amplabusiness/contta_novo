import { Col, Row } from 'antd';

import Skeleton from '@/components/Shimmer/Skeleton';

import { Container } from './styles';

const ProdutosShimmer = () => {
  return (
    <Container>
      <Skeleton className="title" />
      <Skeleton className="subtitle" />
      <Row align="middle" justify="center">
        <Col xs={24} md={20}>
          <Skeleton className="table" />
        </Col>
      </Row>
    </Container>
  );
};

export default ProdutosShimmer;
