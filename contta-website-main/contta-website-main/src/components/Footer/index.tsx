import { Container, Wrapper, Text } from './styles';

const Footer: React.FC = () => {
  const year = new Date().getFullYear();

  return (
    <Container>
      <Wrapper>
        <Text>
          Copyright © {year} Contta Inteligência Fiscal. Todos os direitos
          reservados.
        </Text>
      </Wrapper>
    </Container>
  );
};

export default Footer;
