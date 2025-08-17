import { useQuery } from 'react-query';
import { useSelector } from 'react-redux';
import { useLocation } from 'react-router-dom';

import { getReclassificationInfo } from '@/services/api/requests';

const useProductsReclassification = operation => {
  const { id } = useSelector(state => state.activeCompanyState);

  const { state: nfeId } = useLocation();

  const query = useQuery(
    ['reclassificacao', id, nfeId, operation],
    () => getReclassificationInfo(id, nfeId, operation),
    {
      refetchInterval: false,
      refetchOnWindowFocus: false,
    },
  );

  return query;
};

export default useProductsReclassification;
