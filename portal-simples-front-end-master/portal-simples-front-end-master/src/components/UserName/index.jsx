import { FaUserCircle } from 'react-icons/fa';
import { useSelector } from 'react-redux';

import useViewportWidth from '@/hooks/useViewportWidth';

import { Container, MobileContainer } from './styles';

const UserName = () => {
  const { name } = useSelector(state => state.userState);
  const firstName = name.split(' ')[0];

  const { isMobile } = useViewportWidth();

  return isMobile ? (
    <MobileContainer>
      <FaUserCircle size={18} color="#676a6c" />
      <span className="username">{firstName}</span>
    </MobileContainer>
  ) : (
    <Container>
      <FaUserCircle size={18} color="#676a6c" />
      <span className="username">{firstName}</span>
    </Container>
  );
};

export default UserName;
