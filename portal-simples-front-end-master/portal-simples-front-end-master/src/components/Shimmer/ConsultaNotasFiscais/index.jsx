import Skeleton from '@/components/Shimmer/Skeleton';

import { Container } from './styles';

const ConsultaNotasFiscaisShimmer = () => {
  return (
    <Container>
      <Skeleton className="table" />
    </Container>
  );
};

export default ConsultaNotasFiscaisShimmer;
