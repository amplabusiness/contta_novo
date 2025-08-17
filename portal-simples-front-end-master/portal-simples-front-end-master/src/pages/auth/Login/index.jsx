import { Divider } from 'antd';
import { useHistory, Link } from 'react-router-dom';

import useLogin from '@/services/api/hooks/auth/Login/useLogin';

import Form from '@/pages/auth/Login/components/Form';

import logo from '@/assets/images/logoContta.png';

import {
  Container,
  LoginBox,
  RegisterAccountButton,
  ForgoutPassword,
} from './styles';

const Login = () => {
  const { push } = useHistory();

  const { mutation, query } = useLogin();

  const onSubmit = values => {
    const data = {
      email: values.email.trim(),
      password: values.password.trim(),
    };

    mutation.mutate(data);
  };

  const isLoading = mutation.isLoading || query.isLoading || query.isFetching;

  return (
    <Container>
      <LoginBox>
        <center>
          <img src={logo} alt="Contta Inteligência Fiscal" />
        </center>
        <Form onSubmit={onSubmit} loading={isLoading} />
        <Divider plain>ou</Divider>
        <RegisterAccountButton type="primary" onClick={() => push('/cadastro')}>
          Cadastre-se
        </RegisterAccountButton>
        <ForgoutPassword>
          <Link to="/esqueciMinhaSenha">Esqueceu sua senha?</Link>
        </ForgoutPassword>
      </LoginBox>
    </Container>
  );
};

export default Login;
