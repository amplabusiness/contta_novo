import PropTypes from 'prop-types';
import { Button, Col, Form as AntForm, Input, Radio, Row } from 'antd';
import { useSelector } from 'react-redux';

import { dateFormatter } from '@/utils/formatters';

import Form from '@/components/Form';

import { Card } from '@/pages/app/DadosEmpresa/styles';

const { Item: FormItem } = AntForm;

const NationalSimple = ({ company, handleSubmit, isLoading }) => {
  const { id } = useSelector(state => state.activeCompanyState);

  const isTheActiveCompany = company.id === id;

  const formattedLastUpdateDate = dateFormatter(
    company.simplesNacional.lastUpdate,
    "'Atualizado em' P 'às' HH:mm",
    'Nenhuma data de atualização encontrada',
  );

  const isSimplesOpting = company.simplesNacional.simplesOptant ? 'Sim' : 'Não';
  const isMeiOpting = company.simplesNacional.simeiOptant ? 'Sim' : 'Não';

  const simplesInclusionDate = dateFormatter(
    company.simplesNacional.simplesIncluded,
  );
  const simplesExclusionDate = dateFormatter(
    company.simplesNacional.simplesExcluded,
  );

  const regimeOptions = [
    { label: 'Competência', value: 'competencia' },
    { label: 'Caixa', value: 'caixa' },
  ];
  const currentRegime = company.regimeBox
    ? 'caixa'
    : company.regimeCompetence
    ? 'competencia'
    : '';

  return (
    <Card>
      <h2>Simples Nacional</h2>

      <span>{formattedLastUpdateDate}</span>

      <Form
        name="national-simple-form"
        initialValues={{
          optanteSimples: isSimplesOpting,
          optanteMei: isMeiOpting,
          dataInclusaoSimples: simplesInclusionDate,
          dataExclusaoSimples: simplesExclusionDate,
          regime: currentRegime,
        }}
        onFinish={handleSubmit(2)}
        style={{ marginTop: 10 }}
      >
        <Row gutter={[24, 0]}>
          <Col xs={24} md={12}>
            <FormItem name="optanteSimples" label="Optante Simples">
              <Input disabled />
            </FormItem>
          </Col>

          <Col xs={24} md={12}>
            <FormItem name="optanteMei" label="Optante MEI">
              <Input disabled />
            </FormItem>
          </Col>

          <Col xs={24} md={12}>
            <FormItem name="dataInclusaoSimples" label="Data Inclusão Simples">
              <Input disabled />
            </FormItem>
          </Col>

          <Col xs={24} md={12}>
            <FormItem name="dataExclusaoSimples" label="Data Exclusão Simples">
              <Input disabled />
            </FormItem>
          </Col>

          <Col xs={24}>
            <FormItem
              name="regime"
              label="Regime"
              rules={[{ required: true, message: 'Selecione um regime' }]}
            >
              <Radio.Group options={regimeOptions} />
            </FormItem>
          </Col>

          <Col xs={24}>
            <FormItem noStyle>
              <Button
                type="primary"
                htmlType="submit"
                loading={isLoading}
                disabled={!isTheActiveCompany}
                style={{ marginTop: 10 }}
              >
                Confirmar
              </Button>
            </FormItem>
          </Col>
        </Row>
      </Form>
    </Card>
  );
};

NationalSimple.propTypes = {
  company: PropTypes.object.isRequired,
  handleSubmit: PropTypes.func.isRequired,
  isLoading: PropTypes.bool.isRequired,
};

export default NationalSimple;
