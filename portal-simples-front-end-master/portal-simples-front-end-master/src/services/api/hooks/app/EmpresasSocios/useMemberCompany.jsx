import { notification } from 'antd';
import { useMutation } from 'react-query';

import { getMemberCompany } from '@/services/api/requests';
import { getUserToken } from '@/utils/userToken';

const useMemberCompany = () => {
  const userToken = getUserToken();

  const mutation = useMutation(values => getMemberCompany(userToken, values), {
    onError: () => {
      notification.error({
        message: 'Erro',
        description:
          'Não foi possível trazer os dados da empresa selecionada no momento.',
      });
    },
  });

  return mutation;
};

export default useMemberCompany;
