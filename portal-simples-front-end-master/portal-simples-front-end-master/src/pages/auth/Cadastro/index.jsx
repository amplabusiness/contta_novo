import { Link } from 'react-router-dom';

import useNewUser from '@/services/api/hooks/auth/Cadastro/useNewUser';
import logo from '@/assets/images/logoContta.png';

import Form from '@/pages/auth/Cadastro/components/Form';

import { Container, Card, BackLink } from './styles';

const Cadastro = () => {
  const { mutate, isLoading } = useNewUser();

  const onSubmit = values => {
    const data = {
      group: 1,
      name: values.name.trim(),
      email: values.email.trim(),
      password: values.password.trim(),
      tokenAcesso: values.tokenAcesso.trim(),
      userComum: false,
    };

    mutate(data);
  };

  return (
    <Container>
      <Card>
        <center>
          <img src={logo} alt="Contta Inteligência Fiscal" />
          <br />
        </center>
        <Form onSubmit={onSubmit} loading={isLoading} />
        <BackLink>
          Já possui uma conta?
          <Link to="/login">Entre agora</Link>
        </BackLink>
      </Card>
    </Container>
  );
};

export default Cadastro;
