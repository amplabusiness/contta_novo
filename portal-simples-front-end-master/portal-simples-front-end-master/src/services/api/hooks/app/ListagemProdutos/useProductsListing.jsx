import { useQuery } from 'react-query';

import { getProductsListing } from '@/services/api/requests';

const useProductsListing = (type, group, nfeIds) => {
  const idsQuery = nfeIds.map(item => `Ids=${item}`).join('&');

  const query = useQuery(
    ['listagemProdutos', type, group],
    () => getProductsListing(type, group, idsQuery),
    {
      refetchInterval: false,
      refetchOnWindowFocus: false,
    },
  );

  return query;
};

export default useProductsListing;
