import { useQuery } from 'react-query';

import { getAdjustmentCodes } from '@/services/api/requests';

const useAdjustmentCodes = () => {
  const query = useQuery('codigosAjusteApuracao', getAdjustmentCodes, {
    refetchInterval: false,
    refetchOnWindowFocus: false,
  });

  return query;
};

export default useAdjustmentCodes;
