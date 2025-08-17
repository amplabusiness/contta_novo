import { useQuery } from 'react-query';
import { useSelector } from 'react-redux';

import { getDeparaProducts } from '@/services/api/requests';

const useProducts = () => {
  const { id } = useSelector(state => state.activeCompanyState);
  const { hasStock } = useSelector(state => state.activeCompanyState);

  const query = useQuery(['produtosDepara', id], () => getDeparaProducts(id), {
    enabled: hasStock === 'success',
    refetchInterval: false,
    refetchOnWindowFocus: false,
  });

  return query;
};

export default useProducts;
