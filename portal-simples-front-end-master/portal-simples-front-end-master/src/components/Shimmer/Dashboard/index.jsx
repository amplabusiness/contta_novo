import { Col, Row } from 'antd';

import Skeleton from '@/components/Shimmer/Skeleton';

import { Container } from './styles';

const HomeShimmer = () => {
  return (
    <Container>
      <Skeleton className="title" />
      <Skeleton className="subtitle" />
      <Row gutter={[24, 0]}>
        <Col xs={24} sm={12} xl={6}>
          <Skeleton className="box" />
        </Col>
        <Col xs={24} sm={12} xl={6}>
          <Skeleton className="box" />
        </Col>
        <Col xs={24} sm={12} xl={6}>
          <Skeleton className="box" />
        </Col>
        <Col xs={24} sm={12} xl={6}>
          <Skeleton className="box" />
        </Col>
        <Col xs={24} sm={12} xl={6}>
          <Skeleton className="box" />
        </Col>
        <Col xs={24} sm={12} xl={6}>
          <Skeleton className="box" />
        </Col>
        <Col xs={24} sm={12} xl={6}>
          <Skeleton className="box" />
        </Col>
        <Col xs={24} sm={12} xl={6}>
          <Skeleton className="box" />
        </Col>
        <Col xs={24} sm={12} xl={6}>
          <Skeleton className="box" />
        </Col>
      </Row>
      <Row gutter={[24, 0]}>
        <Col xs={24} lg={12}>
          <Skeleton className="title" />
          <Skeleton className="subtitle" />
          <Row gutter={[24, 0]}>
            <Col xs={24} sm={12}>
              <Skeleton className="box" />
            </Col>
            <Col xs={24} sm={12}>
              <Skeleton className="box" />
            </Col>
          </Row>
        </Col>
        <Col xs={24} lg={12}>
          <Skeleton className="title" />
          <Skeleton className="subtitle" />
          <Skeleton className="box" />
        </Col>
      </Row>
      <Row gutter={[24, 0]} style={{ marginTop: 40 }}>
        <Col xs={24}>
          <Skeleton className="title" />
          <Skeleton className="subtitle" />
          <Skeleton className="box" />
        </Col>
      </Row>
    </Container>
  );
};

export default HomeShimmer;
