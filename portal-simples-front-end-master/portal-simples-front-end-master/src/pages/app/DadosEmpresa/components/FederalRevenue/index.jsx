import PropTypes from 'prop-types';
import { Col, Form as AntForm, Input, Row } from 'antd';

import { cpfCnpjFormatter, dateFormatter } from '@/utils/formatters';

import Form from '@/components/Form';

import { Card } from '@/pages/app/DadosEmpresa/styles';

const { Item: FormItem } = AntForm;

const FederalRevenue = ({ company }) => {
  const formattedLastUpdateDate = dateFormatter(
    company.simplesNacional.lastUpdate,
    "'Atualizado em' P 'às' HH:mm",
    'Nenhuma data de atualização encontrada',
  );

  const formattedCnpj = cpfCnpjFormatter(company.cnpj);

  const legalNature = `${company.legalNature.code} - ${company.legalNature.description}`;

  const openingDate = dateFormatter(company.founded);

  const situationDate = dateFormatter(company.registration.statusDate);

  const companyPhone = company.phone ? company.phone.replace(/\s/g, '') : null;
  const phoneDDD = companyPhone ? companyPhone.slice(0, 4) : '';
  const phoneNumber = companyPhone ? companyPhone.slice(4) : '';

  return (
    <Card>
      <h2>Receita Federal</h2>

      <span>{formattedLastUpdateDate}</span>

      <Form
        name="federal-revenue-form"
        initialValues={{
          cnpj: formattedCnpj,
          nome: company.name,
          fantasia: company.alias,
          natureza: legalNature,
          porte: company.size,
          dataAbertura: openingDate,
          situacao: company.registration.status,
          dataSituacao: situationDate,
          capital: company.capital,
          email: company.email,
          ddd: phoneDDD,
          telefone: phoneNumber,
        }}
        style={{ marginTop: 10 }}
      >
        <Row gutter={[24, 0]}>
          <Col xs={24} md={8}>
            <FormItem name="cnpj" label="CNPJ">
              <Input disabled />
            </FormItem>
          </Col>

          <Col xs={24} md={16}>
            <FormItem name="nome" label="Razão Social">
              <Input disabled />
            </FormItem>
          </Col>

          <Col xs={24} md={10}>
            <FormItem name="fantasia" label="Nome Fantasia">
              <Input disabled />
            </FormItem>
          </Col>

          <Col xs={24} md={10}>
            <FormItem name="natureza" label="Natureza Juridica">
              <Input disabled />
            </FormItem>
          </Col>

          <Col xs={24} md={4}>
            <FormItem name="porte" label="Porte">
              <Input disabled />
            </FormItem>
          </Col>

          <Col xs={24} md={6}>
            <FormItem name="dataAbertura" label="Data de Abertura">
              <Input disabled />
            </FormItem>
          </Col>

          <Col xs={24} md={6}>
            <FormItem name="situacao" label="Situação Cadastral">
              <Input disabled />
            </FormItem>
          </Col>

          <Col xs={24} md={6}>
            <FormItem name="dataSituacao" label="Data da Situação">
              <Input disabled />
            </FormItem>
          </Col>

          <Col xs={24} md={6}>
            <FormItem name="capital" label="Capital Social">
              <Input disabled />
            </FormItem>
          </Col>

          <Col xs={24} md={8}>
            <FormItem name="email" label="E-mail">
              <Input disabled />
            </FormItem>
          </Col>

          <Col xs={24} md={4}>
            <FormItem name="ddd" label="DDD">
              <Input disabled />
            </FormItem>
          </Col>

          <Col xs={24} md={4}>
            <FormItem name="telefone" label="Telefone">
              <Input disabled />
            </FormItem>
          </Col>
        </Row>
      </Form>
    </Card>
  );
};

FederalRevenue.propTypes = {
  company: PropTypes.object.isRequired,
};

export default FederalRevenue;
