import Skeleton from '@/components/Shimmer/Skeleton';

import { Container } from './styles';

const BlocoEShimmer = () => {
  return (
    <Container>
      <Skeleton className="title" />
      <Skeleton className="subtitle" />

      <div className="tabs">
        <Skeleton className="tab" />
      </div>
      <Skeleton className="table" />

      <div className="tabs">
        <Skeleton className="tab" />
      </div>
      <Skeleton className="table" />

      <div className="tabs">
        <Skeleton className="tab" />
        <Skeleton className="tab" />
        <Skeleton className="tab" />
      </div>
      <Skeleton className="table" />

      <div className="tabs">
        <Skeleton className="tab" />
      </div>
      <Skeleton className="table" />
    </Container>
  );
};

export default BlocoEShimmer;
