import { useState } from 'react';
import { Button, Col, Form as AntForm, Modal, notification, Row } from 'antd';
import { useSelector, useDispatch } from 'react-redux';

import useNewCompany from '@/services/api/hooks/app/Empresas/useNewCompany';
import useAuthorizationRequest from '@/services/api/hooks/app/Empresas/useAuthorizationRequest';
import { removeNonNumericChars } from '@/utils/formatters';
import { setCompanyInfo, setCompanyData } from '@/store/slices/activeCompany';
import { checkCompanyType } from '@/utils/simplesNacional/checkCompanyType';

import Form from '@/components/Form';
import { CNPJInput } from '@/components/Form/Input';

import { Content, RegisterCompanyButton, ModalText } from './styles';

const { Item: FormItem } = AntForm;

const SingleRegister = () => {
  const { id: userId } = useSelector(state => state.userState);
  const dispatch = useDispatch();

  const [validationMode, setValidationMode] = useState({
    cnpj: 'onBlur',
  });
  const [hasAuthorizationError, setHasAuthorizationError] = useState(false);

  const [form] = AntForm.useForm();

  const newCompanyMutation = useNewCompany();
  const authorizationRequestMutation = useAuthorizationRequest();

  const changeValidationMode = fieldName => {
    setValidationMode(prevState => ({ ...prevState, [fieldName]: 'onChange' }));
  };

  const onSubmit = values => {
    const reformatedCnpj = removeNonNumericChars(values.cnpj);

    newCompanyMutation.mutate(reformatedCnpj, {
      onSuccess: data => {
        const { companyInformation } = data;

        const {
          id,
          name,
          alias,
          primaryActivity: { code, anexo },
          integradoEstoque,
        } = companyInformation;

        const annex = anexo.replace('(*)', '').trim();
        const companyType = checkCompanyType(companyInformation);

        dispatch(
          setCompanyInfo({
            id,
            name,
            alias,
            cnpj: reformatedCnpj,
            annex,
            primaryCode: code,
            hasStock: integradoEstoque,
            companyType,
          }),
        );
        dispatch(setCompanyData({}));

        notification.success({
          message: 'Sucesso',
          description: 'Cadastro realizado com sucesso!',
        });

        form.resetFields();
      },
      onError: error => {
        const { data, status } = error.response;

        // Verificando se o erro é de autorização
        if (
          status === 400 &&
          data === 'Empresa já esta cadastrada para outro Usuário Admin'
        ) {
          setHasAuthorizationError(true);
        } else {
          notification.error({
            message: 'Erro',
            description: 'Não foi possível cadastrar a empresa no momento.',
          });
        }
      },
    });
  };

  const onConfirmAuthorization = () => {
    const dataQuery = `userId=${userId}&autorizationAdminId=${userId}&desativar=true`;

    authorizationRequestMutation.mutate(dataQuery, {
      onSuccess: () => {
        notification.success({
          message: 'Sucesso',
          description:
            'Solicitação enviada com sucesso! Agora você deve esperar a resposta do outro administrador.',
        });

        setHasAuthorizationError(false);
      },
      onError: () => {
        notification.error({
          message: 'Erro',
          description: 'Não foi possível solicitar a autorização no momento.',
        });
      },
    });
  };

  return (
    <>
      <Form
        name="register-company-form"
        form={form}
        initialValues={{
          cnpj: '',
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
        <Content>
          <Row gutter={[0, 10]}>
            <Col xs={24}>
              <FormItem
                name="cnpj"
                label="CNPJ da Empresa"
                rules={[
                  {
                    required: true,
                    message: 'Campo obrigatório',
                  },
                  {
                    pattern: /^\d\d.\d\d\d.\d\d\d[/]\d\d\d\d-\d\d$/,
                    message: 'CNPJ inválido',
                  },
                ]}
                validateTrigger={validationMode.cnpj}
              >
                <CNPJInput
                  onBlur={() => {
                    changeValidationMode('cnpj');
                  }}
                />
              </FormItem>
            </Col>

            <Col xs={24}>
              <FormItem noStyle>
                <RegisterCompanyButton
                  htmlType="submit"
                  loading={newCompanyMutation.isLoading}
                >
                  <span>Cadastrar</span>
                </RegisterCompanyButton>
              </FormItem>
            </Col>
          </Row>
        </Content>
      </Form>

      <Modal
        title="Erro de Autorização"
        visible={hasAuthorizationError}
        onOk={onConfirmAuthorization}
        onCancel={() => setHasAuthorizationError(false)}
        footer={[
          <Button
            key="cancel"
            onClick={() => setHasAuthorizationError(false)}
            disabled={authorizationRequestMutation.isLoading}
          >
            Cancelar
          </Button>,
          <Button
            key="submit"
            type="primary"
            onClick={onConfirmAuthorization}
            disabled={authorizationRequestMutation.isLoading}
          >
            Pedir autorização
          </Button>,
        ]}
      >
        <ModalText>
          A empresa que você tentou cadastrar já possui um administrador. Caso
          você ainda queira cadastrar essa empresa em sua conta, será necessário
          fazer um pedido de autorização ao atual administrador. Clique no botão{' '}
          <strong>Pedir autorização</strong> para realizar esse processo.
        </ModalText>
      </Modal>
    </>
  );
};

export default SingleRegister;
