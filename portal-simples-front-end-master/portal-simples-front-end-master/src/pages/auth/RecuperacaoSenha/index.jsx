import { useLocation } from 'react-router-dom';
import queryString from 'query-string';

import useRedefinePassword from '@/services/api/hooks/auth/RecuperacaoSenha/useRedefinePassword';

import Form from '@/pages/auth/RecuperacaoSenha/components/Form';

import logo from '@/assets/images/logoContta.png';
import { Container, Card, Title } from './styles';

const RecuperacaoSenha = () => {
  const { search } = useLocation();
  const { token = null } = queryString.parse(search);

  const { mutate, isLoading } = useRedefinePassword();

  const onSubmit = values => {
    const { password } = values;

    const newPasswordQuery = `Token=${token}&NewPassword=${password}`;

    mutate(newPasswordQuery);
  };

  return (
    <Container>
      <Card>
        <center>
          <img src={logo} alt="Contta Inteligência Fiscal" />
        </center>
        <Title>Recuperação da senha</Title>
        <Form onSubmit={onSubmit} isLoading={isLoading} />
      </Card>
    </Container>
  );
};

export default RecuperacaoSenha;
