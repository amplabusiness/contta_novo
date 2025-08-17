import { useMutation, useQueryClient } from 'react-query';
import { useSelector } from 'react-redux';

import { putUpdateRegimeProducts } from '@/services/api/requests';

const useUpdatePisCofinsProducts = () => {
  const { id } = useSelector(state => state.activeCompanyState);
  const { date } = useSelector(state => state.referenceDateState);

  const queryClient = useQueryClient();
  const mutation = useMutation(data => putUpdateRegimeProducts(id, data), {
    onSuccess: () =>
      queryClient.refetchQueries(['produtosPisCofins', id, 'monofasico', date]),
  });

  return mutation;
};

export default useUpdatePisCofinsProducts;
