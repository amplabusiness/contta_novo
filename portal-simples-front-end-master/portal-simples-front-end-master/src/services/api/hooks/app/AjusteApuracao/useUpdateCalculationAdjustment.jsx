import { notification } from 'antd';
import { useMutation } from 'react-query';

import { putEBlockAdjustment } from '@/services/api/requests';

const useUpdateCalculationAdjustment = () => {
  const mutation = useMutation(values => putEBlockAdjustment(values), {
    onSuccess: () => {
      notification.success({
        message: 'Sucesso',
        description: 'Valor atualizado com sucesso!',
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

export default useUpdateCalculationAdjustment;
