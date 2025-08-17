import { useMutation, useQueryClient } from 'react-query';
import { useSelector } from 'react-redux';

import { putRevertRegimeProducts } from '@/services/api/requests';

const useRevertIcmsProducts = option => {
  const { id } = useSelector(state => state.activeCompanyState);
  const { date } = useSelector(state => state.referenceDateState);

  const queryClient = useQueryClient();
  const mutation = useMutation(data => putRevertRegimeProducts(data), {
    onSuccess: () =>
      queryClient.refetchQueries(['produtosIcms', id, option, date]),
  });

  return mutation;
};

export default useRevertIcmsProducts;
