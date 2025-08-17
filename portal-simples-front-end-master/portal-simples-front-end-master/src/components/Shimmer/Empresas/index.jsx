import Skeleton from '@/components/Shimmer/Skeleton';

import { Container } from './styles';

const Empresas = () => {
  return (
    <Container>
      <Skeleton className="input" />
      <Skeleton className="table" />
    </Container>
  );
};

export default Empresas;
