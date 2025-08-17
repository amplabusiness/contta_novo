import PropTypes from 'prop-types';
import { Col, Form as AntForm, Input, Row } from 'antd';

import { dateFormatter } from '@/utils/formatters';

import Form from '@/components/Form';

import { Card } from '@/pages/app/DadosEmpresa/styles';
import { RegistrationsTitle, EmptyText } from './styles';

const { Item: FormItem } = AntForm;

const StateRegistrations = ({ company }) => {
  const formattedLastUpdateDate = dateFormatter(
    company.sintegra.lastUpdate,
    "'Atualizado em' P 'às' HH:mm",
    'Nenhuma data de atualização encontrada',
  );

  const companyRegistrations = company.sintegra.registrations;
  const enabledRegistrations = companyRegistrations.filter(
    item => item.enabled,
  );
  const disabledRegistrations = companyRegistrations.filter(
    item => !item.enabled,
  );

  return (
    <Card>
      <h2>Inscrições Estaduais</h2>

      <span>{formattedLastUpdateDate}</span>

      <Form name="state-registrations-form" style={{ marginTop: 10 }}>
        <RegistrationsTitle>Habilitadas</RegistrationsTitle>

        <Row gutter={[24, 0]} style={{ marginBottom: 20 }}>
          {enabledRegistrations.length > 0 ? (
            enabledRegistrations.map((registration, index) => {
              const registrationValue = `${registration.state}: ${registration.number}`;

              return (
                <Col key={registration.number} xs={24}>
                  <FormItem>
                    <Input value={registrationValue} disabled />
                  </FormItem>
                </Col>
              );
            })
          ) : (
            <Col xs={24}>
              <EmptyText>Nenhuma inscrição habilitada</EmptyText>
            </Col>
          )}
        </Row>

        <RegistrationsTitle>Desabilitadas</RegistrationsTitle>

        <Row gutter={[24, 0]} style={{ marginBottom: 20 }}>
          {disabledRegistrations.length > 0 ? (
            disabledRegistrations.map((registration, index) => {
              const registrationValue = `${registration.state}: ${registration.number}`;

              return (
                <Col key={registration.number} xs={24}>
                  <FormItem>
                    <Input value={registrationValue} disabled />
                  </FormItem>
                </Col>
              );
            })
          ) : (
            <Col xs={24}>
              <EmptyText>Nenhuma inscrição desabilitada</EmptyText>
            </Col>
          )}
        </Row>
      </Form>
    </Card>
  );
};

StateRegistrations.propTypes = {
  company: PropTypes.object.isRequired,
};

export default StateRegistrations;
