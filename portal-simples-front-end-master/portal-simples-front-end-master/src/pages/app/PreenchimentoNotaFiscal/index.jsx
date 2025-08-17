import { Col, Row } from 'antd';
import { FaFileInvoice } from 'react-icons/fa';
import { useSelector } from 'react-redux';
import { useHistory } from 'react-router-dom';

import SellInvoiceList from '@/pages/app/PreenchimentoNotaFiscal/components/SellInvoiceList';
import ServiceInvoiceList from '@/pages/app/PreenchimentoNotaFiscal/components/ServiceInvoiceList';

import { Container, Title } from '@/styles/global';
import { Card, IconContainer } from './styles';

const PreenchimentoNotaFiscal = () => {
  const { companyType } = useSelector(state => state.activeCompanyState);

  const { push } = useHistory();

  return (
    <Container>
      <Title>
        <h2>Escolha do Modelo</h2>
        <p>
          Caso queira registrar uma nova nota fiscal, é necessário que você
          escolha um dos modelo listados abaixo.
        </p>
      </Title>
      <Row
        gutter={[40, 0]}
        align="middle"
        justify="center"
        style={{ margin: '20px 0' }}
      >
        {companyType === 'comum' && (
          <Col xs={24} md={12} lg={8}>
            <Card onClick={() => push('/modeloNotaFiscal/venda')}>
              <IconContainer>
                <FaFileInvoice size={32} />
              </IconContainer>
              <h2>Venda</h2>
              <p>
                Usada para registrar transações comerciais originadas de pessoas
                jurídicas; destaque dos impostos de ICMS, PIS, COFINS, IPI e o
                Imposto sobre Importação.
              </p>
            </Card>
          </Col>
        )}
        <Col xs={24} md={12} lg={8}>
          <Card onClick={() => push('/modeloNotaFiscal/servico')}>
            <IconContainer>
              <FaFileInvoice size={32} />
            </IconContainer>
            <h2>Serviço</h2>
            <p>
              É usada na comprovação da prestação de serviços. Esses serviços
              podem ser feitos de uma pessoa jurídica a outra ou entre uma
              empresa e um consumidor.
            </p>
          </Card>
        </Col>
      </Row>

      {companyType === 'comum' && <SellInvoiceList />}

      <ServiceInvoiceList />
    </Container>
  );
};

export default PreenchimentoNotaFiscal;
