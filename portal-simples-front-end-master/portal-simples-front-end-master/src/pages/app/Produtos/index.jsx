import { useSelector } from 'react-redux';

import useProducts from '@/services/api/hooks/app/Produtos/useProducts';

import Shimmer from '@/components/Shimmer/Produtos';
import ErrorMessage from '@/components/ErrorMessage';

import View from '@/pages/app/Produtos/View';
import Warning from '@/pages/app/Produtos/View/components/Warning';

const Produtos = () => {
  const { hasStock } = useSelector(state => state.activeCompanyState);

  const { isLoading, isError, data } = useProducts();

  if (hasStock === 'notStarted') {
    return <Warning />;
  }

  if (isLoading) {
    return <Shimmer />;
  }

  if (isError) {
    return <ErrorMessage />;
  }

  return <View products={data} />;
};

export default Produtos;
