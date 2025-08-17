import { Link } from 'react-router-dom';

import useSendPasswordRecoveryEmail from '@/services/api/hooks/auth/EsqueciMinhaSenha/useSendPasswordRecoveryEmail';

import logo from '@/assets/images/logoContta.png';

import Form from '@/pages/auth/EsqueciMinhaSenha/components/Form';

import { Container, Card, Title, BackToLogin } from './styles';

const EsqueciMinhaSenha = () => {
  const { mutate, isLoading } = useSendPasswordRecoveryEmail();

  const onSubmit = values => {
    const { email } = values;

    mutate(email);
  };

  return (
    <Container>
      <Card>
        <center>
          <img src={logo} alt="Contta Inteligência Fiscal" />
        </center>
        <Title>Esqueci minha senha</Title>
        <Form onSubmit={onSubmit} isLoading={isLoading} />
        <BackToLogin>
          <p>Lembrou da senha?</p>
          <Link to="/login">Voltar ao Login</Link>
        </BackToLogin>
      </Card>
    </Container>
  );
};

export default EsqueciMinhaSenha;
