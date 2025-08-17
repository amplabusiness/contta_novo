import { useLocation } from 'react-router-dom';

import useProductsListing from '@/services/api/hooks/app/ListagemProdutos/useProductsListing';

import Shimmer from '@/components/Shimmer/ListagemProdutos';
import ErrorMessage from '@/components/ErrorMessage';

import ListagemProdutosView from '@/pages/app/ListagemProdutos/View';

const ListagemProdutos = () => {
  const { state } = useLocation();
  const { type, group, nfeIds } = state;

  const query = useProductsListing(type, group, nfeIds);
  const { isLoading, isError, data } = query;

  if (isLoading) {
    return <Shimmer />;
  }

  if (isError) {
    return <ErrorMessage />;
  }

  return <ListagemProdutosView data={data} />;
};

export default ListagemProdutos;
