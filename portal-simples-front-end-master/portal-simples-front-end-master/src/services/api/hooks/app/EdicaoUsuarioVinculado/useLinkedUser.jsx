import { useQuery } from 'react-query';

import { getUserById } from '@/services/api/requests';

const useLinkedUser = id => {
  const query = useQuery(['usuarioVinculado', id], () => getUserById(id), {
    refetchInterval: false,
    refetchOnWindowFocus: false,
  });

  return query;
};

export default useLinkedUser;
