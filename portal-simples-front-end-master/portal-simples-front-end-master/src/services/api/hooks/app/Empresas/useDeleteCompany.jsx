import { notification } from 'antd';
import { useMutation, useQueryClient } from 'react-query';
import { useSelector } from 'react-redux';

import { deleteCompany } from '@/services/api/requests';

const useDeleteCompany = () => {
  const { id } = useSelector(state => state.userState);

  const queryClient = useQueryClient();
  const mutation = useMutation(values => deleteCompany(values, id), {
    onSuccess: async () => {
      await queryClient.refetchQueries('empresas');

      notification.success({
        message: 'Sucesso',
        description: 'Empresa deletada com sucesso!',
      });
    },
    onError: () => {
      notification.error({
        message: 'Erro',
        description: 'Não foi possível deletar a empresa no momento.',
      });
    },
  });

  return mutation;
};

export default useDeleteCompany;
