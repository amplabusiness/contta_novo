import { notification } from 'antd';
import { useMutation } from 'react-query';
import { useHistory } from 'react-router-dom';

import { postNewUser } from '@/services/api/requests';

const useNewUser = () => {
  const { push } = useHistory();

  const mutation = useMutation(values => postNewUser(values), {
    onSuccess: () => {
      notification.success({
        message: 'Sucesso',
        description: 'Cadastro realizado com sucesso!',
      });

      push('/login');
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

export default useNewUser;
