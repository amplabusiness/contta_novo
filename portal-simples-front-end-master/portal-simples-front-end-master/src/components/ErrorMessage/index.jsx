import { Button } from 'antd';
import { IoWarning } from 'react-icons/io5';
import { useHistory } from 'react-router-dom';

import { Container } from '@/styles/global';
import { Card, IconContainer, Title, Text } from './styles';

const ErrorMessage = () => {
  const { goBack } = useHistory();

  return (
    <Container>
      <Card>
        <IconContainer>
          <IoWarning color="#fff" size={60} />
        </IconContainer>
        <Title>Algo deu errado!</Title>
        <Text>
          Não conseguimos processar sua solicitação. Por favor, tente novamente
          mais tarde.
        </Text>
        <Button type="primary" onClick={goBack}>
          Voltar
        </Button>
      </Card>
    </Container>
  );
};

export default ErrorMessage;
