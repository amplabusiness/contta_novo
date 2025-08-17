import { FooterContainer } from './styles';

const Footer = () => {
  const year = new Date().getFullYear();

  return (
    <FooterContainer>
      <div>
        Developed by <strong>Contta Inteligência Fiscal</strong> &copy; {year}
      </div>
    </FooterContainer>
  );
};

export default Footer;
