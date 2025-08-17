import { FaBuilding } from 'react-icons/fa';
import { useHistory, useLocation } from 'react-router-dom';

import { StyledButton } from './styles';

const MyCompaniesButton = () => {
  const { push } = useHistory();
  const { pathname } = useLocation();

  const pageUrl = '/empresas';
  const isSamePage = pathname === pageUrl;

  return (
    <StyledButton
      id="my-companies-button"
      onClick={() => push(pageUrl)}
      disabled={isSamePage}
    >
      <FaBuilding />
      <span>Minhas Empresas</span>
    </StyledButton>
  );
};

export default MyCompaniesButton;
