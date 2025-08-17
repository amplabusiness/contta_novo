import { useQuery } from 'react-query';

import { getRegisteredCfops } from '@/services/api/requests';

const useRegisteredCfops = () => {
  const query = useQuery('cfopsBase', getRegisteredCfops, {
    refetchInterval: false,
    refetchOnWindowFocus: false,
  });

  return query;
};

export default useRegisteredCfops;
