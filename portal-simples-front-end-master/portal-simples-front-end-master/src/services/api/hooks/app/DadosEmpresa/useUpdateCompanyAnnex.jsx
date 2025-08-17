import { notification } from 'antd';
import { useMutation, useQueryClient } from 'react-query';

import { putUpdateCompanyAnnex } from '@/services/api/requests';

const useUpdateCompanyAnnex = id => {
  const queryClient = useQueryClient();
  const mutation = useMutation(values => putUpdateCompanyAnnex(id, values), {
    onSuccess: async () => {
      await queryClient.refetchQueries('empresas');

      notification.success({
        message: 'Sucesso',
        description: 'Anexos atualizados com sucesso!',
      });
    },
    onError: () => {
      notification.error({
        message: 'Erro',
        description: 'Não foi possível atualizar os anexos no momento.',
      });
    },
  });

  return mutation;
};

export default useUpdateCompanyAnnex;
