import { notification } from 'antd';
import { useQuery, useMutation } from 'react-query';
import { useDispatch } from 'react-redux';
import { useHistory } from 'react-router-dom';

import {
  getAccessToken,
  getInitialUserConfiguration,
} from '@/services/api/requests';
import { setInitialDataSE } from '@/store/slices/user';

const useLogin = () => {
  const dispatch = useDispatch();

  const { push } = useHistory();

  const DEV_BYPASS = process.env.REACT_APP_DEV_AUTH_BYPASS === 'true';

  const mutation = useMutation(async values => {
    if (DEV_BYPASS) {
      // Simula login em modo dev (sem backend)
      const fakeToken = 'dev-token-FAKE-ONLY-LOCAL';
      const fakeUser = {
        id: '00000000-0000-0000-0000-000000000000',
        userMasterId: '00000000-0000-0000-0000-000000000000',
        name: values?.email || 'Usuário Dev',
        group: 1, // 1 = admin
      };
      return { token: fakeToken, user: fakeUser };
    }
    return getAccessToken(values);
  }, {
    onError: () => {
      notification.error({
        message: 'Erro',
        description: 'Email e/ou senha inválido(s).',
      });
    },
  });

  const token = mutation.data?.token;
  const user = mutation.data?.user;

  const query = useQuery(
    'configuracaoUsuario',
    () => (DEV_BYPASS ? Promise.resolve({}) : getInitialUserConfiguration(token)),
    {
      refetchInterval: false,
      refetchOnWindowFocus: false,
      enabled: !!(token && user),
      onSuccess: userConfig => {
        dispatch(setInitialDataSE(userConfig, token, user));

        notification.success({
          message: 'Logado com sucesso',
          description: 'Seja bem vindo!',
        });

  push('/home');
      },
    },
  );

  return { mutation, query };
};

export default useLogin;
