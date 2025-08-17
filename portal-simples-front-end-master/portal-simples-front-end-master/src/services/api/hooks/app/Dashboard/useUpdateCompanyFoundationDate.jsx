import { notification } from 'antd';
import { useMutation } from 'react-query';

import { putUpdateCompanyFoundationDate } from '@/services/api/requests';

const useUpdateCompanyFoundationDate = () => {
  const mutation = useMutation(
    values => putUpdateCompanyFoundationDate(values),
    {
      onSuccess: () => {
        notification.success({
          message: 'Sucesso',
          description: 'Data de fundação atualizada com sucesso!',
        });
      },
      onError: () => {
        notification.error({
          message: 'Erro',
          description: 'Operação não disponível no momento',
        });
      },
    },
  );

  return mutation;
};

export default useUpdateCompanyFoundationDate;
