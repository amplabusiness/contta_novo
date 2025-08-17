import { notification } from 'antd';
import { useMutation } from 'react-query';

import { postManualServiceInvoice } from '@/services/api/requests';

const useNewServiceInvoice = () => {
  const mutation = useMutation(values => postManualServiceInvoice(values), {
    onSuccess: () => {
      notification.success({
        message: 'Sucesso',
        description: 'Nota registrada com sucesso!',
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

export default useNewServiceInvoice;
