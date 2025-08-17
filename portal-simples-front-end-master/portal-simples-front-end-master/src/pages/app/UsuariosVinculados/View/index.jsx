import PropTypes from 'prop-types';

import EmptyTable from '@/components/EmptyTable';

import UsuarioForm from '@/pages/app/UsuariosVinculados/View/components/Form';

import { Container, Title } from '@/styles/global';
import { Content } from './styles';
import UsersList from './components/UsersList';

const UsuariosVinculadosView = ({ data }) => {
  return (
    <Container>
      <Title>
        <h2>Cadastro de Usuário</h2>
        <p>
          Preencha os campos abaixo para cadastrar e vincular um novo usuário a
          sua conta. Você também pode vincular suas empresas cadastradas a esse
          usuário.
        </p>
      </Title>

      <UsuarioForm />

      <Content>
        <Title>
          <h2>Usuários Cadastros</h2>
          <p>Esses são os usuários já cadastrados e vinculados a sua conta.</p>
        </Title>

        {data.length > 0 ? (
          <UsersList users={data} />
        ) : (
          <EmptyTable title="Nenhum usuário encontrado" />
        )}
      </Content>
    </Container>
  );
};

UsuariosVinculadosView.propTypes = {
  data: PropTypes.oneOfType([PropTypes.array, PropTypes.string]).isRequired,
};

export default UsuariosVinculadosView;
