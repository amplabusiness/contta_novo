import { useState } from 'react';
import PropTypes from 'prop-types';
import { Col, Form as AntForm, Input, Row } from 'antd';

import Form from '@/components/Form';

import { ConfirmButton } from './styles';

const { Item: FormItem } = AntForm;

const LoginForm = ({ loading, onSubmit }) => {
  const [validationMode, setValidationMode] = useState({
    email: 'onBlur',
  });

  const changeValidationMode = fieldName => {
    setValidationMode(prevState => ({ ...prevState, [fieldName]: 'onChange' }));
  };

  return (
    <Form
      name="login-form"
      initialValues={{ email: '', password: '' }}
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
              { required: true, message: 'Campo obrigatório' },
              { type: 'email', message: 'E-mail inválido' },
            ]}
            validateTrigger={validationMode.email}
          >
            <Input
              type="email"
              onBlur={() => {
                changeValidationMode('email');
              }}
            />
          </FormItem>
        </Col>
        <Col xs={24}>
          <FormItem
            name="password"
            label="Senha"
            rules={[{ required: true, message: 'Campo obrigatório' }]}
          >
            <Input.Password />
          </FormItem>
        </Col>
      </Row>
      <FormItem noStyle>
        <ConfirmButton htmlType="submit" type="primary" loading={loading}>
          Entrar
        </ConfirmButton>
      </FormItem>
    </Form>
  );
};

LoginForm.propTypes = {
  onSubmit: PropTypes.func.isRequired,
  loading: PropTypes.bool.isRequired,
};

export default LoginForm;
