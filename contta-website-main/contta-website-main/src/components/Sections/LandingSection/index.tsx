import { Link } from 'react-scroll';
import { IoPlay } from 'react-icons/io5';
import { FiChevronDown } from 'react-icons/fi';

import { useUIContext } from '../../../context/ui';

import {
  Container,
  Wrapper,
  Heading,
  ActionsContainer,
  ActionButton,
  VideoAction,
  ScrollIndicator,
} from './styles';

const LandingSection: React.FC = () => {
  const { openVideo } = useUIContext();

  return (
    <Container>
      <Wrapper>
        <Heading>
          <h1>
            A <span>solução web</span> para seu planejamento tributário
          </h1>
          <ActionsContainer>
            <Link activeClass="active" to="services" spy smooth duration={1000}>
              <ActionButton>Conheça nossos serviços</ActionButton>
            </Link>
            <VideoAction onClick={() => openVideo()}>
              <IoPlay size={32} color="#3276b1" />
              <p>Veja o vídeo</p>
            </VideoAction>
          </ActionsContainer>
        </Heading>
        <Link activeClass="active" to="goal" spy smooth duration={1000}>
          <ScrollIndicator>
            <FiChevronDown size={44} />
          </ScrollIndicator>
        </Link>
      </Wrapper>
    </Container>
  );
};

export default LandingSection;
