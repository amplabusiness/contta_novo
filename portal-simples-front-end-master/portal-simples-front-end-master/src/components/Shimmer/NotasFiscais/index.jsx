import Skeleton from '@/components/Shimmer/Skeleton';

import { Container } from './styles';

const NotasFicaisShimmer = () => {
  return (
    <Container>
      <Skeleton className="first-table" />
      <span>
        <Skeleton className="tab" />
        <Skeleton className="tab" />
        <Skeleton className="tab" />
        <Skeleton className="tab" />
      </span>
      <Skeleton className="second-table" />
    </Container>
  );
};

export default NotasFicaisShimmer;
