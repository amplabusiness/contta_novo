import { notification } from 'antd';
import { useMutation } from 'react-query';

import { getSendPasswordEmail } from '@/services/api/requests';

const useSendPasswordRecoveryEmail = () => {
  const mutation = useMutation(values => getSendPasswordEmail(values), {
    onSuccess: () => {
      notification.success({
        message: 'Sucesso',
        description:
          'Entre no link enviado em seu email para alterar sua senha.',
      });
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

export default useSendPasswordRecoveryEmail;
