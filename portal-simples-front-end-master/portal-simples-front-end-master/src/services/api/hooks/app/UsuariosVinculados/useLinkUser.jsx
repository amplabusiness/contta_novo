import { notification } from 'antd';
import { useMutation, useQueryClient } from 'react-query';

import { postNewUser } from '@/services/api/requests';

const useLinkUser = () => {
  const queryClient = useQueryClient();
  const mutation = useMutation(values => postNewUser(values), {
    onSuccess: async () => {
      await queryClient.refetchQueries('usuariosVinculados');

      notification.success({
        message: 'Sucesso',
        description: 'Usuário cadastrado com sucesso.',
      });
    },
    onError: () => {
      notification.error({
        message: 'Erro',
        description: 'Operação não disponível no momento.',
      });
    },
  });

  return mutation;
};

export default useLinkUser;
