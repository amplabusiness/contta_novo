import { notification } from 'antd';
import { useMutation, useQueryClient } from 'react-query';
import { useSelector } from 'react-redux';

import { patchUserConfiguration } from '@/services/api/requests';

const usePatchUserConfiguration = () => {
  const { id } = useSelector(state => state.activeCompanyState);

  const queryClient = useQueryClient();
  const mutation = useMutation(values => patchUserConfiguration(values), {
    onSuccess: data => {
      queryClient.setQueryData(['configuracaoUsuario', id], () => data);

      notification.success({
        message: 'Sucesso',
        description: 'Configurações atualizadas com sucesso!',
      });
    },
    onError: () => {
      notification.error({
        message: 'Erro',
        description: 'Não foi possível atualizar as configurações no momento.',
      });
    },
  });

  return mutation;
};

export default usePatchUserConfiguration;
