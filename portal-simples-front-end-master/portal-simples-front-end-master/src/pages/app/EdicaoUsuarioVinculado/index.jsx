import { useParams } from 'react-router-dom';

import useLinkedUser from '@/services/api/hooks/app/EdicaoUsuarioVinculado/useLinkedUser';

import Form from '@/pages/app/EdicaoUsuarioVinculado/components/Form';

import Shimmer from '@/components/Shimmer/UsuariosVinculados';
import ErrorMessage from '@/components/ErrorMessage';

import { Container, Title } from '@/styles/global';

const EdicaoUsuario = () => {
  const { id } = useParams();

  const { isLoading, isError, data } = useLinkedUser(id);

  if (isLoading) {
    return <Shimmer />;
  }

  if (isError) {
    return <ErrorMessage />;
  }

  return (
    <Container>
      <Title>
        <h2>Dados do Usuário</h2>
        <p>
          Esses são os dados do usuário selecionado. Você pode modificar as
          informações atualizando os valores nos campos abaixo.
        </p>
      </Title>
      <Form user={data} />
    </Container>
  );
};

export default EdicaoUsuario;
