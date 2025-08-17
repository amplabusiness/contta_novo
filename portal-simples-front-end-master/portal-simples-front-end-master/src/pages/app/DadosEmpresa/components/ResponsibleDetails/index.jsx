import { useState } from 'react';
import PropTypes from 'prop-types';
import { Button, Col, Form as AntForm, Input, Row, Select } from 'antd';
import { useSelector } from 'react-redux';

import Form from '@/components/Form';

import { Card } from '@/pages/app/DadosEmpresa/styles';
import { CPFInput, PhoneInput } from '@/components/Form/Input';

const { Item: FormItem } = AntForm;

const ResponsibleDetails = ({ company, handleSubmit, isLoading }) => {
  const { id } = useSelector(state => state.activeCompanyState);

  const [validationMode, setValidationMode] = useState({
    responsibleCpf: 'onBlur',
    responsibleEmail: 'onBlur',
    responsibleTelefone: 'onBlur',
  });

  const changeValidationMode = fieldName => {
    setValidationMode(prevState => ({ ...prevState, [fieldName]: 'onChange' }));
  };

  const membersName = company.membership.map(member => ({
    key: member.name,
    label: member.name,
    value: member.name,
  }));

  const isTheActiveCompany = company.id === id;

  return (
    <Card>
      <Form
        name="responsible-details-form"
        initialValues={{
          responsibleName: '',
          responsibleCpf: '',
          responsibleEmail: '',
          responsibleTelefone: '',
          simpleCode: '',
          qualificationResp: '',
        }}
        onFinish={handleSubmit(1)}
        onFinishFailed={() => {
          const newValidationMode = Object.keys(validationMode).reduce(
            (obj, curr) => ({ ...obj, [curr]: 'onChange' }),
            {},
          );

          setValidationMode(newValidationMode);
        }}
        style={{ marginTop: 10 }}
      >
        <h2>Dados do Responsável</h2>

        <Row gutter={[24, 0]}>
          <Col xs={24} md={10}>
            <FormItem
              name="responsibleName"
              label="Nome"
              rules={[{ required: true, message: 'Campo obrigatório' }]}
            >
              <Select>
                {membersName.map(item => (
                  <Select.Option key={item.key} value={item.value}>
                    {item.label}
                  </Select.Option>
                ))}
              </Select>
            </FormItem>
          </Col>

          <Col xs={24} md={6}>
            <FormItem
              name="responsibleCpf"
              label="CPF"
              rules={[
                { required: true, message: 'Campo obrigatório' },
                {
                  pattern: /^^\d\d\d.\d\d\d.\d\d\d-\d\d$/,
                  message: 'CPF inválido',
                },
              ]}
              validateTrigger={validationMode.responsibleCpf}
            >
              <CPFInput onBlur={() => changeValidationMode('responsibleCpf')} />
            </FormItem>
          </Col>

          <Col xs={24} md={8}>
            <FormItem
              name="responsibleEmail"
              label="E-mail"
              rules={[
                { required: true, message: 'Campo obrigatório' },
                {
                  type: 'email',
                  message: 'E-mail inválido',
                },
              ]}
              validateTrigger={validationMode.responsibleEmail}
            >
              <Input onBlur={() => changeValidationMode('responsibleEmail')} />
            </FormItem>
          </Col>

          <Col xs={24} md={6}>
            <FormItem
              name="responsibleTelefone"
              label="Telefone"
              rules={[
                { required: true, message: 'Campo obrigatório' },
                {
                  pattern:
                    /(^[(]\d\d[)] \d \d\d\d\d-\d\d\d\d$)|(^[(]\d\d[)] \d\d\d\d-\d\d\d\d$)/,
                  message: 'Telefone inválido',
                },
              ]}
              validateTrigger={validationMode.responsibleTelefone}
            >
              <PhoneInput
                onBlur={() => changeValidationMode('responsibleTelefone')}
              />
            </FormItem>
          </Col>

          <Col xs={24} md={6}>
            <FormItem
              name="simpleCode"
              label="Código do Simples"
              rules={[{ required: true, message: 'Campo obrigatório' }]}
            >
              <Input />
            </FormItem>
          </Col>

          <Col xs={24} md={8}>
            <FormItem
              name="qualificationResp"
              label="Qualificação do Responsável"
              rules={[{ required: true, message: 'Campo obrigatório' }]}
            >
              <Input />
            </FormItem>
          </Col>

          <Col
            xs={24}
            md={4}
            style={{
              marginTop: 18,
              display: 'flex',
              alignItems: 'center',
            }}
          >
            <FormItem noStyle>
              <Button
                htmlType="submit"
                type="primary"
                loading={isLoading}
                disabled={!isTheActiveCompany}
              >
                Alterar
              </Button>
            </FormItem>
          </Col>
        </Row>
      </Form>
    </Card>
  );
};

ResponsibleDetails.propTypes = {
  company: PropTypes.object.isRequired,
  handleSubmit: PropTypes.func.isRequired,
  isLoading: PropTypes.bool.isRequired,
};

export default ResponsibleDetails;
