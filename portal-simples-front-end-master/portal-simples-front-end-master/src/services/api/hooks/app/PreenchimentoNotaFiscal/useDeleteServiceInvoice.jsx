import { notification } from 'antd';
import { useMutation, useQueryClient } from 'react-query';
import { useSelector } from 'react-redux';

import { deleteServiceInvoice } from '@/services/api/requests';

const useDeleteServiceInvoice = () => {
  const { id } = useSelector(state => state.activeCompanyState);

  const queryClient = useQueryClient();
  const mutation = useMutation(values => deleteServiceInvoice(values), {
    onSuccess: async () => {
      await queryClient.refetchQueries(['notasServico', id]);

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

export default useDeleteServiceInvoice;
