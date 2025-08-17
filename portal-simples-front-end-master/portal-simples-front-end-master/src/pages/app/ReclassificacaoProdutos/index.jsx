import queryString from 'query-string';
import { useLocation } from 'react-router-dom';

import useProductsReclassification from '@/services/api/hooks/app/ReclassificacaoProdutos/useProductsReclassification';

import Shimmer from '@/components/Shimmer/ReclassificacaoProdutos';
import ErrorMessage from '@/components/ErrorMessage';

import View from './View';

const ReclassificacaoProdutos = () => {
  const { search } = useLocation();
  const { operacao } = queryString.parse(search);

  const { data, isLoading, isError } = useProductsReclassification(operacao);

  if (isLoading) {
    return <Shimmer />;
  }

  if (isError) {
    return <ErrorMessage />;
  }

  return <View data={data} />;
};

export default ReclassificacaoProdutos;
