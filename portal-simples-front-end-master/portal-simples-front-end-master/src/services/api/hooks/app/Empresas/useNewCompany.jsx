import { useMutation, useQueryClient } from 'react-query';

import { getNewCompany } from '@/services/api/requests';
import { getUserToken } from '@/utils/userToken';

const useNewCompany = () => {
  const userToken = getUserToken();

  const queryClient = useQueryClient();
  const mutation = useMutation(values => getNewCompany(values, userToken), {
    onSuccess: () => queryClient.refetchQueries('empresas'),
  });

  return mutation;
};

export default useNewCompany;
