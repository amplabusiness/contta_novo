import { Col, Row } from 'antd';

import Skeleton from '@/components/Shimmer/Skeleton';

import { Container } from './styles';

const DadosEmpresaShimmer = () => {
  return (
    <Container>
      <Row gutter={[24, 0]}>
        <Col xs={24} xl={16}>
          <Skeleton className="primary-box first" />
          <Skeleton className="primary-box second" />
          <Skeleton className="primary-box third" />
        </Col>
        <Col xs={24} xl={8}>
          <Skeleton className="secondary-box first" />
          <Skeleton className="secondary-box second" />
          <Skeleton className="secondary-box third" />
        </Col>
      </Row>
    </Container>
  );
};

export default DadosEmpresaShimmer;
