import { useQueryClient, useMutation } from 'react-query';
import { useSelector } from 'react-redux';

import { putUpdateIcmsStTax } from '@/services/api/requests';

const useUpdateIcmsStTax = option => {
  const { id } = useSelector(state => state.activeCompanyState);

  const taxToBeUpdated =
    option === 'exigSuspensa' ? 'exigibilidade' : 'AntecipEncerr';

  const queryClient = useQueryClient();
  const mutation = useMutation(
    values => putUpdateIcmsStTax(taxToBeUpdated, values),
    {
      onSuccess: () => {
        queryClient.refetchQueries(['impostosIcmsSt', id, option]);
      },
    },
  );

  return mutation;
};

export default useUpdateIcmsStTax;
