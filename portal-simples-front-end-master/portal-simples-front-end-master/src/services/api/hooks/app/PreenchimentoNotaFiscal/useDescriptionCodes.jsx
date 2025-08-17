import { useQuery } from 'react-query';

import { getDescriptionCodes } from '@/services/api/requests';

const useDescriptionCodes = () => {
  const query = useQuery('codigosDescricoes', getDescriptionCodes, {
    refetchInterval: false,
    refetchOnWindowFocus: false,
  });

  return query;
};

export default useDescriptionCodes;
