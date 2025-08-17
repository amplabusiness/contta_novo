import { FiSearch } from 'react-icons/fi';
import { useHistory, useLocation } from 'react-router-dom';

import { StyledButton } from './styles';

const SmartSearchButton = () => {
  const { push } = useHistory();
  const { pathname } = useLocation();

  const pageUrl = '/consultaNotasFiscais';
  const isSamePage = pathname === pageUrl;

  return (
    <StyledButton onClick={() => push(pageUrl)} disabled={isSamePage}>
      <FiSearch />
      <span>Consulta de NF-e</span>
    </StyledButton>
  );
};

export default SmartSearchButton;
