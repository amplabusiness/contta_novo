import { Row } from 'antd';

import Skeleton from '@/components/Shimmer/Skeleton';

import { Container } from './styles';

const ListagemProdutosShimmer = () => {
  return (
    <Container>
      <Skeleton className="title" />
      <Skeleton className="subtitle" />
      <Skeleton className="table" />
      <Row gutter={[24, 0]} justify="end" style={{ marginTop: 30 }}>
        <Skeleton className="total" />
      </Row>
    </Container>
  );
};

export default ListagemProdutosShimmer;
