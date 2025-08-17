import { useState } from 'react';
import PropTypes from 'prop-types';
import { Col, Form as AntForm, Input, Row } from 'antd';

import Form from '@/components/Form';

import { ConfirmButton } from './styles';

const { Item: FormItem } = AntForm;

const EsqueciMinhaSenhaForm = ({ onSubmit, isLoading }) => {
  const [validationMode, setValidationMode] = useState({
    email: 'onBlur',
  });

  const changeValidationMode = fieldName => {
    setValidationMode(prevState => ({ ...prevState, [fieldName]: 'onChange' }));
  };

  return (
    <Form
      name="forgot-password-form"
      initialValues={{ email: '' }}
      onFinish={onSubmit}
      onFinishFailed={() => {
        const newValidationMode = Object.keys(validationMode).reduce(
          (obj, curr) => ({ ...obj, [curr]: 'onChange' }),
          {},
        );

        setValidationMode(newValidationMode);
      }}
    >
      <Row gutter={[24, 0]}>
        <Col xs={24}>
          <FormItem
            name="email"
            label="E-mail"
            rules={[
              {
                required: true,
                message: 'Campo obrigatório',
              },
              {
                type: 'email',
                message: 'E-mail inválido',
              },
            ]}
            validateTrigger={validationMode.email}
          >
            <Input
              type="email"
              placeholder="Informe o e-mail cadastrado"
              onBlur={() => {
                changeValidationMode('email');
              }}
            />
          </FormItem>
        </Col>
      </Row>

      <FormItem noStyle>
        <ConfirmButton type="primary" htmlType="submit" loading={isLoading}>
          Confirmar
        </ConfirmButton>
      </FormItem>
    </Form>
  );
};

EsqueciMinhaSenhaForm.propTypes = {
  onSubmit: PropTypes.func.isRequired,
  isLoading: PropTypes.bool.isRequired,
};

export default EsqueciMinhaSenhaForm;
