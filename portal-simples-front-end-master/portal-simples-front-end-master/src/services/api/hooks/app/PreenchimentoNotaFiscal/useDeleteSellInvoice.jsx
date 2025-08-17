import { notification } from 'antd';
import { useMutation, useQueryClient } from 'react-query';
import { useSelector } from 'react-redux';

import { deleteSellInvoice } from '@/services/api/requests';

const useDeleteSellInvoice = () => {
  const { id } = useSelector(state => state.activeCompanyState);

  const queryClient = useQueryClient();
  const mutation = useMutation(values => deleteSellInvoice(values), {
    onSuccess: async () => {
      await queryClient.refetchQueries(['notasVenda', id]);

      notification.success({
        message: 'Sucesso',
        description: 'Nota deletada com sucesso!',
      });
    },
    onError: () => {
      notification.error({
        message: 'Erro',
        description: 'Não foi possível deletar a nota no momento.',
      });
    },
  });

  return mutation;
};

export default useDeleteSellInvoice;
