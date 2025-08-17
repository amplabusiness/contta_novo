import { useState, useEffect } from 'react';
import Image from 'next/image';
import { Link } from 'react-scroll';

import { Container, Wrapper, Nav, List, ListItem } from './styles';

const Header: React.FC = () => {
  const [scrolled, setScrolled] = useState(false);

  const handleScroll = () => {
    if (window.pageYOffset >= 100) {
      setScrolled(true);
    } else {
      setScrolled(false);
    }
  };

  useEffect(() => {
    window.addEventListener('scroll', handleScroll);

    return () => window.removeEventListener('scroll', handleScroll);
  }, []);

  return (
    <Container scrolled={scrolled}>
      <Wrapper>
  <Image src="/images/logo.png" width={135} height={80} alt="Contta logo" />
        <Nav>
          <List scrolled={scrolled}>
            <ListItem>
              <Link
                activeClass="active"
                to="services"
                spy
                smooth
                duration={1000}
              >
                <button type="button">Serviços</button>
              </Link>
            </ListItem>
            <ListItem>
              <Link
                activeClass="active"
                to="contacts"
                spy
                smooth
                duration={1000}
              >
                <button type="button">Contatos</button>
              </Link>
            </ListItem>
          </List>
        </Nav>
      </Wrapper>
    </Container>
  );
};

export default Header;
