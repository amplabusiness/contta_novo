import { Col, Form, Row, Tabs } from 'antd';
import { FiAlertCircle } from 'react-icons/fi';
import { useSelector } from 'react-redux';
import { useLocation } from 'react-router-dom';

import useNewServiceInvoice from '@/services/api/hooks/app/PreenchimentoNotaFiscal/useNewServiceInvoice';

import Stepper from '@/pages/app/PreenchimentoNotaFiscal/components/Stepper';
import Step from '@/pages/app/PreenchimentoNotaFiscal/components/Step';

import Activity from '@/pages/app/PreenchimentoNotaFiscal/pages/ModeloServico/components/Activity';
import ClientDetails from '@/pages/app/PreenchimentoNotaFiscal/pages/ModeloServico/components/ClientDetails';
import Demonstrative from '@/pages/app/PreenchimentoNotaFiscal/pages/ModeloServico/components/Demonstrative';

import { Container, Title } from '@/styles/global';
import { WarningCard, WarningTitle } from './styles';

const ModeloServico = () => {
  const { id } = useSelector(state => state.activeCompanyState);

  const [form] = Form.useForm();

  const { mutateAsync, isLoading } = useNewServiceInvoice();

  const { state } = useLocation();
  const isEditMode = !!state;
  const initialValues = isEditMode
    ? {
        document: state.taker.cnpj_Cpf,
        name: state.taker.name,
        citySubscription: state.taker.citySubscription,
        zipCode: state.taker.zipCode,
        address: state.taker.address,
        neighborhood: state.taker.neighborhood,
        city: state.taker.city,
        state: state.taker.state,
        ...state.activity,
        ...state.demonstrative,
      }
    : {
        document: '',
        name: '',
        citySubscription: '',
        zipCode: '',
        address: '',
        neighborhood: '',
        city: '',
        state: '',
        code: '',
        description: '',
        providedServiceCity: '',
        taxedServiceCity: '',
        servicesValue: 0,
        unconditionalDiscount: 0,
        retainedIssqn: 0,
        liquidValue: 0,
        federalRetentions: 0,
      };

  const handleSubmit = async values => {
    const data = {
      companyInformation: id,
      taker: {
        cnpj_Cpf: values.document,
        name: values.name,
        citySubscription: values.citySubscription,
        zipCode: values.zipCode,
        address: values.address,
        neighborhood: values.neighborhood,
        city: values.city,
        state: values.state,
      },
      activity: {
        code: values.code,
        description: values.description,
      },
      demonstrative: {
        providedServiceCity: values.providedServiceCity,
        taxedServiceCity: values.taxedServiceCity,
        servicesValue: values.servicesValue,
        unconditionalDiscount: values.unconditionalDiscount,
        retainedIssqn: values.retainedIssqn,
        liquidValue: values.liquidValue,
        federalRetentions: values.federalRetentions,
      },
    };

    await mutateAsync(data, {
      onSuccess: () => {
        form.resetFields();
      },
      onError: error => {
        throw error;
      },
    });
  };

  return (
    <Container>
      <Tabs defaultActiveKey="1" type="card">
        <Tabs.TabPane tab="Preenchimento" key="1">
          <Title>
            <h2>Preenchimento da Nota Fiscal</h2>
            <p>
              Preencha os campos abaixo referentes a nota fiscal que deseja
              emitir.
            </p>
          </Title>
          <Row align="middle" justify="center">
            <Col xs={24} md={12}>
              <WarningCard>
                <WarningTitle>
                  <FiAlertCircle size={43} />
                  <h2>Atenção</h2>
                </WarningTitle>
                <p>
                  Lembre-se que a nota fiscal a ser cadastrada será vinculada
                  com a empresa ativa no momento (aquela com o nome localizado
                  no topo da tela). Confirme antes de prosseguir.
                </p>
              </WarningCard>
            </Col>
          </Row>
          <Stepper
            initialValues={initialValues}
            form={form}
            onSubmit={handleSubmit}
            loading={isLoading}
            steps={['Dados Cliente', 'Atividade', 'Demonstrativo']}
          >
            <Step>
              <ClientDetails form={form} />
            </Step>

            <Step>
              <Activity form={form} />
            </Step>

            <Step>
              <Demonstrative form={form} />
            </Step>
          </Stepper>
        </Tabs.TabPane>
        <Tabs.TabPane tab="Alteração" key="2">
          <Title>
            <h2>Alteração da Nota Fiscal</h2>
            <p>Edite a nota fiscal.</p>
          </Title>
        </Tabs.TabPane>
      </Tabs>
    </Container>
  );
};

export default ModeloServico;
