import { useState } from 'react';
import PropTypes from 'prop-types';
import { Col, Form as AntForm, Input, Row } from 'antd';

import Form from '@/components/Form';

import { ConfirmButton } from './styles';

const { Item: FormItem } = AntForm;

const RecuperacaoSenhaForm = ({ onSubmit, isLoading }) => {
  const [validationMode, setValidationMode] = useState({
    password: 'onBlur',
    confirmPassword: 'onBlur',
  });

  const changeValidationMode = fieldName => {
    setValidationMode(prevState => ({ ...prevState, [fieldName]: 'onChange' }));
  };

  return (
    <Form
      name="recover-password-form"
      initialValues={{ password: '', confirmPassword: '' }}
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
            name="password"
            label="Nova senha"
            rules={[
              { required: true, message: 'Campo obrigatório' },
              { min: 8, message: 'Sua senha deve ter no mínimo 8 caracteres' },
            ]}
            validateTrigger={validationMode.password}
          >
            <Input.Password
              onBlur={() => {
                changeValidationMode('password');
              }}
            />
          </FormItem>
        </Col>
        <Col xs={24}>
          <FormItem
            name="confirmPassword"
            label="Confirmar senha"
            dependencies={['password']}
            rules={[
              { required: true, message: 'Campo obrigatório' },
              ({ getFieldValue }) => ({
                validator(_, value) {
                  if (!value || getFieldValue('password') === value) {
                    return Promise.resolve();
                  }
                  return Promise.reject(
                    new Error('As senhas devem ser iguais'),
                  );
                },
              }),
            ]}
            validateTrigger={validationMode.confirmPassword}
          >
            <Input.Password
              onBlur={() => {
                changeValidationMode('confirmPassword');
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

RecuperacaoSenhaForm.propTypes = {
  onSubmit: PropTypes.func.isRequired,
  isLoading: PropTypes.bool.isRequired,
};

export default RecuperacaoSenhaForm;
