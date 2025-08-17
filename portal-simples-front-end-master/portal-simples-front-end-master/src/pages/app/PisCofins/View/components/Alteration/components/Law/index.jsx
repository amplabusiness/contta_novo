import { usePisCofinsContext } from '@/contexts/PisCofinsContext';

import { Title } from '@/styles/global';
import { Text } from './styles';

const Law = () => {
  const {
    state: { products },
  } = usePisCofinsContext();

  return (
    <>
      <Title>
        <h2>Lei</h2>
        <p>Lei sobre os produtos com o NCM selecionado.</p>
      </Title>
      <Text>{products[0] && products[0].lei}</Text>
    </>
  );
};

export default Law;
