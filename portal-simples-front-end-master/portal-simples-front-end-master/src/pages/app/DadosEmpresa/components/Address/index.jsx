import PropTypes from 'prop-types';
import { Col, Form as AntForm, Input, Row } from 'antd';

import { cepFormatter, dateFormatter } from '@/utils/formatters';

import Form from '@/components/Form';

import { Card } from '@/pages/app/DadosEmpresa/styles';

const { Item: FormItem } = AntForm;

const Address = ({ company }) => {
  const formattedLastUpdateDate = dateFormatter(
    company.simplesNacional.lastUpdate,
    "'Atualizado em' P 'às' HH:mm",
    'Nenhuma data de atualização encontrada',
  );

  const formattedZipCode = cepFormatter(company.address.zip);

  return (
    <Card>
      <h2>Endereço</h2>

      <span>{formattedLastUpdateDate}</span>

      <Form
        name="address-form"
        initialValues={{
          logradouro: company.address.street,
          complemento: company.address.details,
          numero: company.address.number,
          cep: formattedZipCode,
          bairro: company.address.neighborhood,
          cidade: company.address.city,
          codigoMunicipal: company.address.cityIbge,
          estado: company.address.state,
        }}
        style={{ marginTop: 10 }}
      >
        <Row gutter={[24, 0]}>
          <Col xs={24} md={8}>
            <FormItem name="logradouro" label="Logradouro">
              <Input disabled />
            </FormItem>
          </Col>

          <Col xs={24} md={12}>
            <FormItem name="complemento" label="Complemento">
              <Input disabled />
            </FormItem>
          </Col>

          <Col xs={24} md={4}>
            <FormItem name="numero" label="Número">
              <Input disabled />
            </FormItem>
          </Col>

          <Col xs={24} md={6}>
            <FormItem name="cep" label="CEP">
              <Input disabled />
            </FormItem>
          </Col>

          <Col xs={24} md={18}>
            <FormItem name="bairro" label="Bairro">
              <Input disabled />
            </FormItem>
          </Col>

          <Col xs={24} md={8}>
            <FormItem name="cidade" label="Cidade">
              <Input disabled />
            </FormItem>
          </Col>

          <Col xs={24} md={4}>
            <FormItem name="codigoMunicipal" label="Código Municipal">
              <Input disabled />
            </FormItem>
          </Col>

          <Col xs={24} md={4}>
            <FormItem name="estado" label="Estado">
              <Input disabled />
            </FormItem>
          </Col>
        </Row>
      </Form>
    </Card>
  );
};

Address.propTypes = {
  company: PropTypes.object.isRequired,
};

export default Address;
