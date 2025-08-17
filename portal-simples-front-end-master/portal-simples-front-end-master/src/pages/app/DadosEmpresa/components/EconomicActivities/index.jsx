import PropTypes from 'prop-types';
import { Col, Form as AntForm, Input, Row } from 'antd';

import { dateFormatter } from '@/utils/formatters';

import Form from '@/components/Form';

import { Card } from '@/pages/app/DadosEmpresa/styles';

const { Item: FormItem } = AntForm;

const EconomicActivities = ({ company }) => {
  const formattedLastUpdateDate = dateFormatter(
    company.simplesNacional.lastUpdate,
    "'Atualizado em' P 'às' HH:mm",
    'Nenhuma data de atualização encontrada',
  );

  const primaryActivityDescription = `${company.primaryActivity.code} - ${company.primaryActivity.description}`;

  return (
    <Card>
      <h2>Atividades Econômicas</h2>

      <span>{formattedLastUpdateDate}</span>

      <Form
        name="primary-activity-form"
        initialValues={{
          primaria: primaryActivityDescription,
          'anexo-primaria': company.primaryActivity.anexo,
        }}
        style={{ marginTop: 10 }}
      >
        <Row gutter={[24, 0]}>
          <Col xs={24} md={16}>
            <FormItem name="primaria" label="Atividade Primária">
              <Input disabled />
            </FormItem>
          </Col>

          <Col xs={24} md={8}>
            <FormItem name="anexo-primaria" label="Anexo">
              <Input disabled />
            </FormItem>
          </Col>
        </Row>
      </Form>

      {company.secondaryActivities.map((item, index) => {
        const secondaryActivityDescription = `${item.code} - ${item.description}`;

        return (
          <Form
            key={item.code}
            name={`secondary-activity-${index}-form`}
            initialValues={{
              [`secundaria-${index}`]: secondaryActivityDescription,
              [`anexo-secundaria-${index}`]: item.descriptionAnexo,
            }}
          >
            <Row gutter={[24, 0]}>
              <Col xs={24} md={16}>
                <FormItem
                  name={`secundaria-${index}`}
                  label="Atividade Secundária"
                >
                  <Input disabled />
                </FormItem>
              </Col>

              <Col xs={24} md={8}>
                <FormItem name={`anexo-secundaria-${index}`} label="Anexo">
                  <Input disabled />
                </FormItem>
              </Col>
            </Row>
          </Form>
        );
      })}
    </Card>
  );
};

EconomicActivities.propTypes = {
  company: PropTypes.object.isRequired,
};

export default EconomicActivities;
