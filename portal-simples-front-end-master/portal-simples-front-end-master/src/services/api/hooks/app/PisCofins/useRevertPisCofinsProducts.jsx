import { useMutation, useQueryClient } from 'react-query';
import { useSelector } from 'react-redux';

import { putRevertRegimeProducts } from '@/services/api/requests';

const useRevertPisCofinsProducts = () => {
  const { id } = useSelector(state => state.activeCompanyState);
  const { date } = useSelector(state => state.referenceDateState);

  const queryClient = useQueryClient();
  const mutation = useMutation(data => putRevertRegimeProducts(data), {
    onSuccess: () =>
      queryClient.refetchQueries([
        'produtosPisCofins',
        id,
        'monofasicoEdit',
        date,
      ]),
  });

  return mutation;
};

export default useRevertPisCofinsProducts;
