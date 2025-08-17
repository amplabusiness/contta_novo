import { useState } from 'react';
import PropTypes from 'prop-types';
import { Col, Form as AntForm, Input, Row } from 'antd';

import Form from '@/components/Form';
import { TokenInput } from '@/components/Form/Input';

import { ConfirmButton } from './styles';

const { Item: FormItem } = AntForm;

const CadastroForm = ({ onSubmit, loading }) => {
  const [validationMode, setValidationMode] = useState({
    email: 'onBlur',
    password: 'onBlur',
    tokenAcesso: 'onBlur',
  });

  const changeValidationMode = fieldName => {
    setValidationMode(prevState => ({ ...prevState, [fieldName]: 'onChange' }));
  };

  return (
    <Form
      name="register-user-form"
      initialValues={{ name: '', email: '', password: '', tokenAcesso: '' }}
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
            name="name"
            label="Nome"
            rules={[
              {
                required: true,
                message: 'Campo obrigatório',
              },
            ]}
          >
            <Input placeholder="Informe seu nome" />
          </FormItem>
        </Col>
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
              placeholder="Informe seu e-mail"
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
            rules={[
              {
                required: true,
                message: 'Campo obrigatório',
              },
              {
                min: 6,
                message: 'Sua senha deve ter no mínimo 6 caracteres',
              },
            ]}
            validateTrigger={validationMode.password}
          >
            <Input.Password
              placeholder="Informe sua senha"
              onBlur={() => {
                changeValidationMode('password');
              }}
            />
          </FormItem>
        </Col>
        <Col xs={24}>
          <FormItem
            name="tokenAcesso"
            label="Token"
            rules={[
              {
                required: true,
                message: 'Campo obrigatório',
              },
              {
                pattern: /^\d\d\d\d\d\d\d\d$/,
                message: 'Token inválido',
              },
            ]}
            validateTrigger={validationMode.tokenAcesso}
          >
            <TokenInput
              placeholder="Informe seu token"
              onBlur={() => {
                changeValidationMode('tokenAcesso');
              }}
            />
          </FormItem>
        </Col>
      </Row>

      <FormItem noStyle>
        <ConfirmButton htmlType="submit" type="primary" loading={loading}>
          Cadastrar
        </ConfirmButton>
      </FormItem>
    </Form>
  );
};

CadastroForm.propTypes = {
  onSubmit: PropTypes.func.isRequired,
  loading: PropTypes.bool.isRequired,
};

export default CadastroForm;
