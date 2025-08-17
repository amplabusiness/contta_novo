import PropTypes from 'prop-types';

import CompaniesList from '@/pages/app/Home/View/components/CompaniesList';
import EmptyTable from '@/components/EmptyTable';

import { Container } from '@/styles/global';
import { Heading, Content } from './styles';

const HomeView = ({ data }) => {
  return (
    <Container>
      <Heading>
        <h2>Resumo das Empresas</h2>
        <p>
          A tabela abaixo apresenta um resumo de todas as suas empresas. Caso
          queira selecionar uma, basta clicar em seu nome
        </p>
      </Heading>
      <Content>
        {data.length > 0 ? (
          <CompaniesList companies={data} />
        ) : (
          <EmptyTable title="Nenhuma empresa encontrada" />
        )}
      </Content>
    </Container>
  );
};

HomeView.propTypes = {
  data: PropTypes.array.isRequired,
};

export default HomeView;
