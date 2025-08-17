import { notification } from 'antd';
import { useMutation, useQueryClient } from 'react-query';

import { postNewCfop } from '@/services/api/requests';

const useNewCfop = () => {
  const queryClient = useQueryClient();
  const mutation = useMutation(values => postNewCfop(values), {
    onSuccess: async () => {
      await queryClient.refetchQueries('cfopsBase');

      notification.success({
        message: 'Sucesso',
        description: 'CFOP registrado com sucesso!',
      });
    },
    onError: error => {
      const { data } = error.response;
      let errorMessage = '';

      if (data.includes('já cadastrado')) {
        errorMessage =
          'O CFOP solicitado já está cadastrado na base do Simples.';
      } else {
        errorMessage = 'Não foi possível registrar o CFOP no momento.';
      }

      notification.error({
        message: 'Erro',
        description: errorMessage,
      });
    },
  });

  return mutation;
};

export default useNewCfop;
