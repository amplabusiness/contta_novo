import { notification } from 'antd';
import { useMutation, useQueryClient } from 'react-query';
import { useSelector } from 'react-redux';

import { putUpdateDeparaProduct } from '@/services/api/requests';

const useUpdateProduct = () => {
  const { id } = useSelector(state => state.activeCompanyState);

  const queryClient = useQueryClient();
  const mutation = useMutation(values => putUpdateDeparaProduct(values), {
    onSuccess: async () => {
      await queryClient.refetchQueries(['produtosDepara', id]);

      notification.success({
        message: 'Sucesso',
        description: 'Produto atualizado com sucesso!',
      });
    },
    onError: () => {
      notification.error({
        message: 'Erro',
        description: 'Não foi possível atualizar o produto no momento.',
      });
    },
  });

  return mutation;
};

export default useUpdateProduct;
