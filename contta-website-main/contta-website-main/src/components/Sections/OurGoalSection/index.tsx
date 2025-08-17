import Image from 'next/image';

import { Container, Wrapper, ImageContainer, Heading } from './styles';

const OurGoalSection: React.FC = () => {
  return (
    <Container id="goal">
      <Wrapper>
        <ImageContainer>
          <Image src="/images/goal.svg" width={400} height={300} alt="Objetivo" />
        </ImageContainer>
        <Heading>
          <h2>Nosso objetivo</h2>
          <p>
            A principal missão da Contta Inteligência Fiscal é facilitar todo o
            processo de planejamento tributário de uma empresa. Visando cumprir
            esse objetivo, nós desenvolvemos algumas soluções e ferramentas
            informatizadas que buscam identificar possíveis alternativas que
            proporcionem redução lícita da pesada e complexa carga tributária
            brasileira.
          </p>
        </Heading>
      </Wrapper>
    </Container>
  );
};

export default OurGoalSection;
