import { notification } from 'antd';
import { useMutation, useQueryClient } from 'react-query';

import { putUpdateCompany } from '@/services/api/requests';

const useUpdateCompany = () => {
  const queryClient = useQueryClient();
  const mutation = useMutation(values => putUpdateCompany(values), {
    onSuccess: async () => {
      await queryClient.refetchQueries('empresas');

      notification.success({
        message: 'Sucesso',
        description: 'Informações atualizadas com sucesso!.',
      });
    },
    onError: () => {
      notification.error({
        message: 'Erro',
        description:
          'Não foi possível atualizar as informações da empresa no momento.',
      });
    },
  });

  return mutation;
};

export default useUpdateCompany;
