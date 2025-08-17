import { Col, Row } from 'antd';

import Skeleton from '@/components/Shimmer/Skeleton';

import { Container } from './styles';

const IcmsStShimmer = () => {
  return (
    <Container>
      <Row gutter={[24, 0]}>
        <Col xs={24} lg={6}>
          <Skeleton className="title" />
          <Skeleton className="subtitle" />
          <Skeleton className="input" />
          <Skeleton className="table" />
          <Skeleton className="button" />
        </Col>
        <Col xs={24} lg={10}>
          <Skeleton className="title" />
          <Skeleton className="subtitle" />
          <Skeleton className="input" />
          <Skeleton className="table" />
          <Skeleton className="button" />
        </Col>
        <Col xs={24} lg={8}>
          <Skeleton className="law" />
        </Col>
      </Row>
    </Container>
  );
};

export default IcmsStShimmer;
