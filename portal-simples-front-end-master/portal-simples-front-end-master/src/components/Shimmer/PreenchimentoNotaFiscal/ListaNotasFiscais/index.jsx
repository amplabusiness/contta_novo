import Skeleton from '@/components/Shimmer/Skeleton';

import { Container } from './styles';

const ListaNotasFiscaisShimmer = () => {
  return (
    <Container>
      <Skeleton className="title" />
      <Skeleton className="subtitle" />
      <Skeleton className="input" />
      <Skeleton className="table" />
    </Container>
  );
};

export default ListaNotasFiscaisShimmer;
