import { useQuery } from 'react-query';

import { getUsersInfo } from '@/services/api/requests';
import { getUserToken } from '@/utils/userToken';

const useLinkedUsers = () => {
  const userToken = getUserToken();
  const query = useQuery('usuariosVinculados', () => getUsersInfo(userToken), {
    refetchInterval: false,
    refetchOnWindowFocus: false,
  });

  return query;
};

export default useLinkedUsers;
