import Skeleton from '@/components/Shimmer/Skeleton';

import { Container } from './styles';

const EmpresasSocioShimmer = () => {
  return (
    <Container>
      <Skeleton className="back-button" />

      <Skeleton className="title" />
      <Skeleton className="subtitle" />

      <Skeleton className="table" />
    </Container>
  );
};

export default EmpresasSocioShimmer;
