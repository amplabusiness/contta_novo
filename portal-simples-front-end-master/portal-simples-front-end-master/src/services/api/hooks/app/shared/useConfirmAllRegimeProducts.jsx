import { useMutation, useQueryClient } from 'react-query';
import { useSelector } from 'react-redux';

import { putConfirmAllRegimeProducts } from '@/services/api/requests';

const useConfirmAllRegimeProducts = regimeOption => {
  const { id } = useSelector(state => state.activeCompanyState);
  const { date } = useSelector(state => state.referenceDateState);

  const queryClient = useQueryClient();
  const mutation = useMutation(
    values => putConfirmAllRegimeProducts(id, date, values),
    {
      onSuccess: async () => {
        const productsOption =
          regimeOption === 'monofasico' ? 'produtosPisCofins' : 'produtosIcms';

        await queryClient.refetchQueries([
          productsOption,
          id,
          regimeOption,
          date,
        ]);
      },
    },
  );

  return mutation;
};

export default useConfirmAllRegimeProducts;
