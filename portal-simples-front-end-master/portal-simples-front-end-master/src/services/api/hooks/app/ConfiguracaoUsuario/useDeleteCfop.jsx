import { notification } from 'antd';
import { useMutation, useQueryClient } from 'react-query';

import { deleteCfop } from '@/services/api/requests';

const useDeleteCfop = () => {
  const queryClient = useQueryClient();
  const mutation = useMutation(values => deleteCfop(values), {
    onSuccess: async () => {
      await queryClient.refetchQueries('cfopsBase');

      notification.success({
        message: 'Sucesso',
        description: 'CFOP deletado com sucesso!',
      });
    },
    onError: () => {
      notification.error({
        message: 'Erro',
        description: 'Não foi possível deletar o CFOP no momento.',
      });
    },
  });

  return mutation;
};

export default useDeleteCfop;
