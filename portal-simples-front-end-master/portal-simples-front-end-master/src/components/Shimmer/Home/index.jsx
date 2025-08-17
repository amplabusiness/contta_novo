import Skeleton from '@/components/Shimmer/Skeleton';

import { Container } from './styles';

const HomeShimmer = () => {
  return (
    <Container>
      <Skeleton className="title" />
      <Skeleton className="subtitle" />

      <Skeleton className="input" />
      <Skeleton className="table" />
    </Container>
  );
};

export default HomeShimmer;
