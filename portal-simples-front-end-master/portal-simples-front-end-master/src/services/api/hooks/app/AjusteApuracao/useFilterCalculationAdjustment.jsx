import { notification } from 'antd';
import { useMutation } from 'react-query';

import { postEBlockAdjustment } from '@/services/api/requests';

const useFilterCalculationAdjustment = () => {
  const mutation = useMutation(values => postEBlockAdjustment(values), {
    onSuccess: () => {
      notification.success({
        message: 'Sucesso',
        description:
          'O total da NF-e com os filtros informados está localizado no formulário abaixo. Utilize-o para definir uma nova base de cálculo.',
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

export default useFilterCalculationAdjustment;
