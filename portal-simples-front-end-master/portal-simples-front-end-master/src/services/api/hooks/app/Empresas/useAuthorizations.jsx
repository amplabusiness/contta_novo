import { Modal, notification } from 'antd';
import { useQuery } from 'react-query';
import { useSelector } from 'react-redux';

import useConfirmNewCompany from '@/services/api/hooks/app/Empresas/useConfirmNewCompany';
import useAuthorizationRequest from '@/services/api/hooks/app/Empresas/useAuthorizationRequest';
import { getAuthorizations } from '@/services/api/requests';

const useAuthorizations = () => {
  const { id: userId, masterId } = useSelector(state => state.userState);
  const id = masterId ?? userId;

  const confirmNewCompanyMutation = useConfirmNewCompany();
  const authorizationRequestMutation = useAuthorizationRequest();

  const query = useQuery(['autorizacoes', id], () => getAuthorizations(id), {
    refetchInterval: 120000,
    refetchOnWindowFocus: false,
    onSuccess: data => {
      if (!data) {
        return;
      }

      const { userId: requestedUserId, cnpj, nomeUsuario, nameClient } = data;
      const content = (
        <p>
          O usuário <strong>{nomeUsuario}</strong> solicitou autorização para
          cadastrar a empresa <strong>{nameClient}</strong> em sua conta. Você
          autoriza essa operação?
        </p>
      );

      Modal.confirm({
        title: 'Autorização solicitada',
        content,
        onOk: async close => {
          await confirmNewCompanyMutation.mutateAsync({
            cnpj,
            confirmRegister: true,
            id: requestedUserId,
          });

          notification.success({
            message: 'Sucesso',
            description: 'Solicitação aceita com sucesso!',
          });
        },
        okText: 'Confirmar',
        onCancel: async close => {
          const dataQuery = `userId=${requestedUserId}&autorizationAdminId=${requestedUserId}&desativar=false`;

          await authorizationRequestMutation.mutateAsync(dataQuery);

          notification.info({
            message: 'Sucesso',
            description: 'Solicitação rejeitada com sucesso!',
          });
        },
      });
    },
  });

  return query;
};

export default useAuthorizations;
