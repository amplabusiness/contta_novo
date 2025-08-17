import { useIcmsStContext } from '@/contexts/IcmsStContext';

import { Title } from '@/styles/global';
import { Text } from './styles';

const Law = () => {
  const {
    state: { products },
  } = useIcmsStContext();

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
