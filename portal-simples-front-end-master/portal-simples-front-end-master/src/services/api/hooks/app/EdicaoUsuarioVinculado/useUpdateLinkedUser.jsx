import { notification } from 'antd';
import { useMutation } from 'react-query';

import { putUpdateUser } from '@/services/api/requests';

const useUpdateLinkedUser = () => {
  const mutation = useMutation(values => putUpdateUser(values), {
    onSuccess: () => {
      notification.success({
        message: 'Sucesso',
        description: 'Usuário atualizado com sucesso!',
      });
    },
    onError: () => {
      notification.error({
        message: 'Erro',
        description: 'Não foi possível atualizar o usuário no momento.',
      });
    },
  });

  return mutation;
};

export default useUpdateLinkedUser;
