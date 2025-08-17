import { useState } from 'react';
import PropTypes from 'prop-types';
import {
  Button,
  Col,
  Form as AntForm,
  Input,
  Radio,
  Row,
  Select,
  Table,
} from 'antd';
import { useSelector } from 'react-redux';

import {
  usuariosVincularEmpresasColumns,
  roleOptions,
} from '@/pages/app/EdicaoUsuarioVinculado/constants';
import { removeNonNumericChars } from '@/utils/formatters';
import useUpdateLinkedUser from '@/services/api/hooks/app/EdicaoUsuarioVinculado/useUpdateLinkedUser';

import Form from '@/components/Form';
import { CPFInput } from '@/components/Form/Input';

import { Title } from '@/styles/global';

const { Item: FormItem } = AntForm;

const EditUserForm = ({ user }) => {
  const { companies } = useSelector(state => state.companiesState);

  const [form] = AntForm.useForm();

  const [validationMode, setValidationMode] = useState({
    document: 'onBlur',
    email: 'onBlur',
    password: 'onBlur',
    confirmPassword: 'onBlur',
  });

  const [linkedCompanies, setLinkedCompanies] = useState(() =>
    companies
      .map(item => item.id)
      .filter(item => user.companyId.includes(item)),
  );

  const { mutate, isLoading } = useUpdateLinkedUser();

  const changeValidationMode = fieldName => {
    setValidationMode(prevState => ({ ...prevState, [fieldName]: 'onChange' }));
  };

  const onSubmit = values => {
    const data = {
      id: user.id,
      group: 2,
      userMasterId: user.userMasterId,
      name: values.name.trim(),
      document: removeNonNumericChars(values.document.trim()),
      role: values.role.trim(),
      email: values.email.trim(),
      isActive: values.active === 'yes',
      password: values.password,
      companyId: linkedCompanies,
    };

    mutate(data);
  };

  return (
    <Form
      name="edit-linked-user-form"
      form={form}
      initialValues={{
        name: user.name,
        document: user.document,
        role: user.role,
        email: user.email,
        password: '',
        confirmPassword: '',
        active: user.isActive ? 'yes' : 'no',
      }}
      onFinish={onSubmit}
      onFinishFailed={() => {
        const newValidationMode = Object.keys(validationMode).reduce(
          (obj, curr) => ({ ...obj, [curr]: 'onChange' }),
          {},
        );

        setValidationMode(newValidationMode);
      }}
    >
      <>
        <Row gutter={[24, 0]} style={{ padding: '0 15px' }}>
          <Col xs={24} md={6}>
            <FormItem
              name="name"
              label="Nome"
              rules={[{ required: true, message: 'Campo obrigatório' }]}
            >
              <Input />
            </FormItem>
          </Col>

          <Col xs={24} md={6}>
            <FormItem
              name="document"
              label="CPF"
              rules={[
                { required: true, message: 'Campo obrigatório' },
                {
                  pattern:
                    /(^\d\d\d.\d\d\d.\d\d\d-\d\d$)|(^\d\d\d\d\d\d\d\d\d\d\d$)/,
                  message: 'CPF inválido',
                },
              ]}
              validateTrigger={validationMode.document}
            >
              <CPFInput onBlur={() => changeValidationMode('document')} />
            </FormItem>
          </Col>

          <Col xs={24} md={6}>
            <FormItem
              name="role"
              label="Cargo"
              rules={[{ required: true, message: 'Campo obrigatório' }]}
            >
              <Select>
                {roleOptions.map(item => (
                  <Select.Option key={item.key} value={item.value}>
                    {item.label}
                  </Select.Option>
                ))}
              </Select>
            </FormItem>
          </Col>

          <Col xs={24} md={6}>
            <FormItem
              name="email"
              label="E-mail"
              rules={[
                { required: true, message: 'Campo obrigatório' },
                {
                  type: 'email',
                  message: 'E-mail inválido',
                },
              ]}
              validateTrigger={validationMode.email}
            >
              <Input onBlur={() => changeValidationMode('email')} />
            </FormItem>
          </Col>

          <Col xs={24} md={6}>
            <FormItem
              name="password"
              label="Senha"
              rules={[
                {
                  min: 6,
                  message: 'Sua senha deve ter no mínimo 6 caracteres',
                },
              ]}
              validateTrigger={validationMode.password}
            >
              <Input.Password onBlur={() => changeValidationMode('password')} />
            </FormItem>
          </Col>

          <Col xs={24} md={6}>
            <FormItem
              name="confirmPassword"
              label="Confirmar Senha"
              dependencies={['password']}
              rules={[
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
                onBlur={() => changeValidationMode('confirmPassword')}
              />
            </FormItem>
          </Col>

          <Col xs={24} style={{ marginTop: 10 }}>
            <FormItem name="active" label="Ativo">
              <Radio.Group>
                <Radio value="yes">Sim</Radio>
                <Radio value="no">Não</Radio>
              </Radio.Group>
            </FormItem>
          </Col>
        </Row>

        <div style={{ marginTop: 10 }}>
          <Title>
            <h2>Empresas Vinculadas</h2>
            <p>
              Abaixo encontram-se todas as suas empresas. Você pode vinculá-las
              ou desvinculá-las do usuário selecionado.
            </p>
          </Title>
          <Table
            columns={usuariosVincularEmpresasColumns}
            dataSource={companies}
            pagination={{ pageSize: 5 }}
            size="small"
            scroll={{ x: 'max-content' }}
            rowKey="id"
            rowSelection={{
              type: 'checkbox',
              selectedRowKeys: linkedCompanies,
              onChange: selectedCompanies => {
                setLinkedCompanies(selectedCompanies);
              },
            }}
            style={{ marginTop: 20, padding: '0 15px' }}
          />
        </div>

        <Row gutter={[24, 0]} style={{ padding: '0 15px' }}>
          <Col xs={24} style={{ marginTop: 20 }}>
            <Button
              type="primary"
              htmlType="submit"
              className="pull-right"
              loading={isLoading}
            >
              Salvar
            </Button>
          </Col>
        </Row>
      </>
    </Form>
  );
};

EditUserForm.propTypes = {
  user: PropTypes.object.isRequired,
};

export default EditUserForm;
