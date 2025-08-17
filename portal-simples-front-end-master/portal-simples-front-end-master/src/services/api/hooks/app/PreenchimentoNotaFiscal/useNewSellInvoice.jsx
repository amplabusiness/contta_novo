import { notification } from 'antd';
import { useMutation } from 'react-query';

import { postManualSellInvoice } from '@/services/api/requests';

const useNewSellInvoice = () => {
  const mutation = useMutation(values => postManualSellInvoice(values), {
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

export default useNewSellInvoice;
