import { FaLinkedinIn } from 'react-icons/fa';
import { IoMdMail } from 'react-icons/io';

import {
  Container,
  Wrapper,
  ContactsContainer,
  Heading,
  ContactBox,
} from './styles';

const ContactsSection: React.FC = () => {
  return (
    <Container id="contacts">
      <Wrapper>
        <Heading>
          <h2>Contatos</h2>
          <p>
            Caso você tenha mais dúvidas, entre em contato com a gente
            utilizando os meios listados.
          </p>
        </Heading>
        <ContactsContainer>
          <ContactBox
            link="email"
            href="mailto:conttadesenvolvimento@gmail.com"
            rel="noreferrer noopener"
            target="_blank"
          >
            <IoMdMail size={16} />
            Email
          </ContactBox>
          <ContactBox
            link="linkedin"
            href="https://www.linkedin.com/company/contta-intelig%C3%AAncia-fiscal"
            rel="noreferrer noopener"
            target="_blank"
          >
            <FaLinkedinIn size={16} />
            Linkedin
          </ContactBox>
        </ContactsContainer>
      </Wrapper>
    </Container>
  );
};

export default ContactsSection;
