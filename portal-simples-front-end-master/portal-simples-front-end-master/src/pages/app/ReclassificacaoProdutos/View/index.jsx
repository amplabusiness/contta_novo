import PropTypes from 'prop-types';
import { FiArrowLeft } from 'react-icons/fi';
import { useHistory } from 'react-router-dom';

import ChangeInfo from '@/pages/app/ReclassificacaoProdutos/View/components/ChangeInfo';
import CompanyDetails from '@/pages/app/ReclassificacaoProdutos/View/components/CompanyDetails';

import { Container, GoBackButton } from './styles';

const ReclassificacaoProdutosView = ({ data }) => {
  const { goBack } = useHistory();

  const { listaProdutos, ...companyDetails } = data;

  return (
    <Container>
      <GoBackButton type="link" onClick={() => goBack()}>
        <FiArrowLeft size={18} color="#3276b1" />
        <p>Voltar</p>
      </GoBackButton>

      <CompanyDetails companyDetails={companyDetails} />

      <ChangeInfo products={listaProdutos} />
    </Container>
  );
};

ReclassificacaoProdutosView.propTypes = {
  data: PropTypes.object.isRequired,
};

export default ReclassificacaoProdutosView;
