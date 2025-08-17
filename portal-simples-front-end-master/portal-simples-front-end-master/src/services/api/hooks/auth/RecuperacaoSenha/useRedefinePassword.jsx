import { notification } from 'antd';
import { useMutation } from 'react-query';
import { useHistory } from 'react-router-dom';

import { getResetPassword } from '@/services/api/requests';

const useRedefinePassword = () => {
  const { push } = useHistory();

  const mutation = useMutation(values => getResetPassword(values), {
    onSuccess: () => {
      notification.success({
        message: 'Sucesso',
        description: 'Senha redefinida com sucesso!',
      });

      push('/login');
    },
    onError: () => {
      notification.error({
        message: 'Erro',
        description: 'Operação não disponível no momento',
      });
    },
  });

  return mutation;
};

export default useRedefinePassword;
