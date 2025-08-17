import { Col, Row } from 'antd';

import Skeleton from '@/components/Shimmer/Skeleton';

import { Container } from './styles';

const UsuariosVinculadosShimmer = () => {
  return (
    <Container>
      <Skeleton className="title" />
      <Skeleton className="subtitle" />
      <Row gutter={[24, 0]} style={{ paddingLeft: 15 }}>
        <Col xs={24} md={6} style={{ marginTop: 20 }}>
          <Skeleton className="label" />
          <Skeleton className="input" />
        </Col>
        <Col xs={24} md={6} style={{ marginTop: 20 }}>
          <Skeleton className="label" />
          <Skeleton className="input" />
        </Col>
        <Col xs={24} md={6} style={{ marginTop: 20 }}>
          <Skeleton className="label" />
          <Skeleton className="input" />
        </Col>
        <Col xs={24} md={6} style={{ marginTop: 20 }}>
          <Skeleton className="label" />
          <Skeleton className="input" />
        </Col>
        <Col xs={24} md={6} style={{ marginTop: 20 }}>
          <Skeleton className="label" />
          <Skeleton className="input" />
        </Col>
        <Col xs={24} md={6} style={{ marginTop: 20 }}>
          <Skeleton className="label" />
          <Skeleton className="input" />
        </Col>
      </Row>

      <Skeleton className="checkbox" />
      <Skeleton className="button" />
      <Skeleton className="title" />
      <Skeleton className="subtitle" />
      <Skeleton className="table" />
    </Container>
  );
};

export default UsuariosVinculadosShimmer;
