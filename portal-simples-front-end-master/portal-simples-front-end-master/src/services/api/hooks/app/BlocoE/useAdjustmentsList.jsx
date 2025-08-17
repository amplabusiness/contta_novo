import { useQuery } from 'react-query';
import { useSelector } from 'react-redux';

import { getAdjustmentsList } from '@/services/api/requests';

const useAdjustmentsList = ({ enabled }) => {
  const { id } = useSelector(state => state.activeCompanyState);

  const query = useQuery(['listaAjustes', id], () => getAdjustmentsList(id), {
    enabled,
    refetchInterval: false,
    refetchOnWindowFocus: false,
  });

  return query;
};

export default useAdjustmentsList;
