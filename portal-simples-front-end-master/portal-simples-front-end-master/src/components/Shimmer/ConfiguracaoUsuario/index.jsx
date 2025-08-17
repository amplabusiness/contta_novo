import { Col, Row } from 'antd';

import Skeleton from '@/components/Shimmer/Skeleton';

import { Container } from './styles';

const ConfiguracaoUsuarioShimmer = () => {
  return (
    <Container>
      <Row gutter={[24, 0]}>
        <Col xs={24} md={12} style={{ marginTop: 20 }}>
          <Skeleton className="title" />
          <Skeleton className="subtitle" />
          <Row gutter={[24, 0]}>
            <Col xs={24} md={8} style={{ marginTop: 20 }}>
              <Skeleton className="input" />
            </Col>
            <Col xs={24} md={6} style={{ marginTop: 20 }}>
              <Skeleton className="button" />
            </Col>
          </Row>
        </Col>
        <Col xs={24} md={12} style={{ marginTop: 20 }}>
          <Skeleton className="title" />
          <Skeleton className="subtitle" />
          <Row gutter={[24, 0]}>
            <Col xs={24} md={8} style={{ marginTop: 20 }}>
              <Skeleton className="input" />
            </Col>
            <Col xs={24} md={8} style={{ marginTop: 20 }}>
              <Skeleton className="input" />
            </Col>
            <Col xs={24} md={6} style={{ marginTop: 20 }}>
              <Skeleton className="button" />
            </Col>
          </Row>
        </Col>
      </Row>
      <Row gutter={[24, 0]} style={{ marginTop: 60 }}>
        <Col xs={24} md={12} style={{ marginTop: 20 }}>
          <Skeleton className="title" />
          <Skeleton className="subtitle" />
          <Row gutter={[24, 0]}>
            <Col xs={24} md={8} style={{ marginTop: 20 }}>
              <Skeleton className="input" />
            </Col>
            <Col xs={24} md={8} style={{ marginTop: 20 }}>
              <Skeleton className="input" />
            </Col>
            <Col xs={24} md={6} style={{ marginTop: 20 }}>
              <Skeleton className="button" />
            </Col>
          </Row>
        </Col>
        <Col xs={24} md={12} style={{ marginTop: 20 }}>
          <Skeleton className="title" />
          <Skeleton className="subtitle" />
          <Row gutter={[24, 0]}>
            <Col xs={24} md={8} style={{ marginTop: 20 }}>
              <Skeleton className="input" />
            </Col>
            <Col xs={24} md={8} style={{ marginTop: 20 }}>
              <Skeleton className="input" />
            </Col>
            <Col xs={24} md={6} style={{ marginTop: 20 }}>
              <Skeleton className="button" />
            </Col>
          </Row>
        </Col>
      </Row>
      <Row gutter={[24, 0]} style={{ marginTop: 60 }}>
        <Col xs={24} md={12} style={{ marginTop: 20 }}>
          <Skeleton className="title" />
          <Skeleton className="subtitle" />
          <Row gutter={[24, 0]}>
            <Col xs={24} md={8} style={{ marginTop: 20 }}>
              <Skeleton className="input" />
            </Col>
            <Col xs={24} md={8} style={{ marginTop: 20 }}>
              <Skeleton className="input" />
            </Col>
            <Col xs={24} md={6} style={{ marginTop: 20 }}>
              <Skeleton className="button" />
            </Col>
          </Row>
        </Col>
      </Row>
    </Container>
  );
};

export default ConfiguracaoUsuarioShimmer;
