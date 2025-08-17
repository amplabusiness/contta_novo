import PropTypes from 'prop-types';
import { Col, Row } from 'antd';

import { cpfCnpjFormatter } from '@/utils/formatters';

import { Title } from '@/styles/global';
import { Container, Content } from './styles';

const CompanyDetails = ({ companyDetails }) => {
  return (
    <Container>
      <Title>
        <h2>Dados da Empresa</h2>
        <p>
          Antes de prosseguir com as alterações nos produtos, certifique-se que
          a empresa selecionada é a desejada.
        </p>
      </Title>
      <Row
        gutter={[24, 20]}
        align="middle"
        justify="center"
        style={{ marginTop: 20, marginBottom: 20 }}
      >
        <Col xs={24} md={6}>
          <Content>
            <p>Razão Social</p>
            <span>{companyDetails.razaoSocial}</span>
          </Content>
        </Col>
        <Col xs={24} md={6}>
          <Content>
            <p>Nome Fantasia</p>
            <span>{companyDetails.fantasia}</span>
          </Content>
        </Col>
        <Col xs={24} md={4}>
          <Content>
            <p>CNPJ</p>
            <span>{cpfCnpjFormatter(companyDetails.cnpj)}</span>
          </Content>
        </Col>
        <Col xs={24} md={4}>
          <Content>
            <p>Modelo da NFE</p>
            <span>{companyDetails.modeloNfe}</span>
          </Content>
        </Col>
      </Row>
    </Container>
  );
};

CompanyDetails.propTypes = {
  companyDetails: PropTypes.object.isRequired,
};

export default CompanyDetails;
