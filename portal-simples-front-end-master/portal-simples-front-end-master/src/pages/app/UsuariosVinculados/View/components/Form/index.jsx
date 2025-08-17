import { useState } from 'react';
import {
  Button,
  Checkbox,
  Col,
  Form as AntForm,
  Input,
  Row,
  Select,
  Table,
} from 'antd';
import { useSelector } from 'react-redux';

import useLinkUser from '@/services/api/hooks/app/UsuariosVinculados/useLinkUser';
import {
  usuariosVincularEmpresasColumns,
  roleOptions,
} from '@/pages/app/UsuariosVinculados/constants';
import { removeNonNumericChars, capitalizeWords } from '@/utils/formatters';
import { getUserToken } from '@/utils/userToken';

import Form from '@/components/Form';
import { CPFInput } from '@/components/Form/Input';

const { Item: FormItem } = AntForm;

const UsuarioForm = () => {
  const { companies } = useSelector(state => state.companiesState);

  const [form] = AntForm.useForm();

  const [validationMode, setValidationMode] = useState({
    document: 'onBlur',
    email: 'onBlur',
    password: 'onBlur',
    confirmPassword: 'onBlur',
  });

  const [willLinkCompanies, setWillLinkCompanies] = useState(false);
  const [linkedCompanies, setLinkedCompanies] = useState([]);

  const { mutate, isLoading } = useLinkUser();

  const changeValidationMode = fieldName => {
    setValidationMode(prevState => ({ ...prevState, [fieldName]: 'onChange' }));
  };

  const handleWillBindCompanies = e => {
    const { checked } = e.target;

    setWillLinkCompanies(checked);

    if (!checked) {
      setLinkedCompanies([]);
    }
  };

  const onSubmit = values => {
    const masterToken = getUserToken();

    const data = {
      group: 2,
      name: capitalizeWords(values.name.trim()),
      document: removeNonNumericChars(values.document.trim()),
      role: values.role.trim(),
      email: values.email.trim(),
      password: values.password.trim(),
      companyId: linkedCompanies,
      tokenAcesso: '',
      token: masterToken,
      userComum: true,
    };

    mutate(data, {
      onSuccess: () => {
        setWillLinkCompanies(false);
        form.resetFields();
      },
    });
  };

  return (
    <Form
      name="link-user-form"
      form={form}
      initialValues={{
        name: '',
        document: '',
        role: '',
        email: '',
        password: '',
        confirmPassword: '',
      }}
      onFinish={onSubmit}
      onFinishFailed={() => {
        const newValidationMode = Object.keys(validationMode).reduce(
          (obj, curr) => ({ ...obj, [curr]: 'onChange' }),
          {},
        );

        setValidationMode(newValidationMode);
      }}
      style={{ marginTop: 10, padding: '0 15px' }}
    >
      <Row gutter={[24, 0]}>
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
            <Input.Password onBlur={() => changeValidationMode('password')} />
          </FormItem>
        </Col>

        <Col xs={24} md={6}>
          <FormItem
            name="confirmPassword"
            label="Confirmar Senha"
            dependencies={['password']}
            rules={[
              {
                required: true,
                message: 'Campo obrigatório',
              },
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
      </Row>

      <Row>
        <Col xs={12} style={{ margin: '10px 0' }}>
          <Checkbox
            checked={willLinkCompanies}
            onChange={handleWillBindCompanies}
          >
            Vincular empresas
          </Checkbox>
        </Col>
      </Row>

      {willLinkCompanies && (
        <Table
          columns={usuariosVincularEmpresasColumns}
          dataSource={companies}
          pagination={{ pageSize: 5 }}
          size="small"
          scroll={{ x: 'max-content' }}
          rowKey="id"
          rowSelection={{
            type: 'checkbox',
            onChange: selectedCompanies => {
              setLinkedCompanies(selectedCompanies);
            },
          }}
          style={{ marginTop: 20 }}
        />
      )}

      <Row>
        <Col xs={12} style={{ marginTop: 20 }}>
          <Button type="primary" htmlType="submit" loading={isLoading}>
            Cadastrar
          </Button>
        </Col>
      </Row>
    </Form>
  );
};

export default UsuarioForm;
