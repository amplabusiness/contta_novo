import { Col, Row } from 'antd';

import Skeleton from '@/components/Shimmer/Skeleton';

import { Container } from './styles';

const ReclassificaoProdutosShimmer = () => {
  return (
    <Container>
      <Skeleton className="back-button" />
      <Skeleton className="title" />
      <Row gutter={[24, 0]} align="middle" justify="center">
        <Col xs={24} md={6}>
          <Skeleton className="company-info" />
        </Col>
        <Col xs={24} md={6}>
          <Skeleton className="company-info" />
        </Col>
        <Col xs={24} md={4}>
          <Skeleton className="company-info" />
        </Col>
        <Col xs={24} md={4}>
          <Skeleton className="company-info" />
        </Col>
      </Row>
      <Row gutter={[24, 0]} align="middle" justify="center">
        <Col xs={24} md={20}>
          <Skeleton className="inputs" />
          <Skeleton className="table" />
        </Col>
      </Row>
    </Container>
  );
};

export default ReclassificaoProdutosShimmer;
