import { useState } from 'react';
import { BsFilePost } from 'react-icons/bs';
import { IoLaptopOutline } from 'react-icons/io5';

import {
  Container,
  Wrapper,
  Heading,
  ContentContainer,
  Card,
  IconContainer,
  ItemsContainer,
  Item,
  FeatureBox,
  FeatureButton,
} from './styles';

const services = [
  {
    id: '1',
    icon: <BsFilePost size={40} color="#4076b1" />,
    title: 'e-Malote',
    description:
      'A solução mais prática para baixar os XMLs das NF-e, CTe, NFS-e emitidas com o CNPJ da sua empresa ou do seu cliente.',
  },
  {
    id: '2',
    icon: <IoLaptopOutline size={40} color="#4076b1" />,
    title: 'Portal',
    description:
      'Ferramenta web para o cálculo, apuração e envio das informações à Receita Federal com preenchimento automatizado no Portal do Simples.',
  },
];

interface IFeatures {
  [key: string]: JSX.Element;
}

interface IFeaturesObject extends IFeatures {
  eMalote: JSX.Element;
  Portal: JSX.Element;
}

const features: IFeaturesObject = {
  eMalote: (
    <p>
      <span>
        Resgatar os XMLs de forma automática no momento da sua emissão;
      </span>
      <span>
        Resgatar em lote os XMLs mais antigos com facilidade sem necessidade de
        manifestação;
      </span>
      <span>
        Acessar rapidamente de forma simples o download em lote das NF-e através
        do CNPJ da empresa;
      </span>
      <span>
        Consultar a autencidade das notas com relatório completo do status na
        Receita Federal em lote;
      </span>
      <span>Organizar os Darfes em formato digital;</span>
      <span>
        E o melhor, você pode armazenar tudo de forma organizada por ano, mês e
        CNPJ.
      </span>
    </p>
  ),
  Portal: (
    <p>
      <span>Resgatar os 12 últimos faturamentos de forma automatizada;</span>
      <span>
        Apurar os impostos do Simples Nacional da sua empresas fazendo a
        segregação das receitas de forma automatizada;
      </span>
      <span>
        identificar alíquota zero, monofásico, substituição tributária, isenção
        do PIS e da COFINS;
      </span>
      <span>Identificar produtos com Substituição Tributária (ST);</span>
      <span>Fazer o ressarcimento do ICMS-ST;</span>
      <span>
        Identificar qual a alíquota e qual o anexo da tabela do Simples
        Nacional;
      </span>
      <span>
        Apurar e emitir a GNRE para todas as unidades da federação, dentre
        outras funcionalidades.
      </span>
    </p>
  ),
};

const ServicesSection: React.FC = () => {
  const [activeItem, setActiveItem] = useState('eMalote');

  const changeActiveItem = (selectedItem: string) => {
    if (selectedItem === activeItem) {
      return;
    }

    setActiveItem(selectedItem);
  };

  return (
    <Container id="services">
      <Wrapper>
        <Heading>
          <h2>Nossos serviços</h2>
          <p>
            Esses são os serviços que a Contta Inteligência Fiscal oferece a
            você que busca um melhor planejamento para a carga tributária de sua
            empresa.
          </p>
        </Heading>
        <ContentContainer type="cards">
          {services.map(service => (
            <Card key={service.id}>
              <IconContainer>{service.icon}</IconContainer>
              <h2>{service.title}</h2>
              <p>{service.description}</p>
            </Card>
          ))}
        </ContentContainer>
        <ContentContainer type="descriptions">
          <ItemsContainer>
            <Item
              active={activeItem === 'eMalote'}
              onClick={() => changeActiveItem('eMalote')}
            >
              <BsFilePost size={24} />
              e-Malote
            </Item>
            <Item
              active={activeItem === 'Portal'}
              onClick={() => changeActiveItem('Portal')}
            >
              <IoLaptopOutline size={24} />
              Portal
            </Item>
          </ItemsContainer>
          <FeatureBox>
            <h2>Características</h2>
            {features[activeItem]}
            <FeatureButton>Detalhes</FeatureButton>
          </FeatureBox>
        </ContentContainer>
      </Wrapper>
    </Container>
  );
};

export default ServicesSection;
