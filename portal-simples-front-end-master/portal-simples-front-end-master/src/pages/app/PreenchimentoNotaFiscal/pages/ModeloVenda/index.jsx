import { Col, Form, Row, Tabs } from 'antd';
import { parseISO } from 'date-fns';
import { FiAlertCircle } from 'react-icons/fi';
import { useSelector } from 'react-redux';
import { useLocation } from 'react-router-dom';

import useNewSellInvoice from '@/services/api/hooks/app/PreenchimentoNotaFiscal/useNewSellInvoice';
import { removeNonNumericChars } from '@/utils/formatters';

import Stepper from '@/pages/app/PreenchimentoNotaFiscal/components/Stepper';
import Step from '@/pages/app/PreenchimentoNotaFiscal/components/Step';

import ClientDetails from '@/pages/app/PreenchimentoNotaFiscal/pages/ModeloVenda/components/ClientDetails';
import ProductsDetails from '@/pages/app/PreenchimentoNotaFiscal/pages/ModeloVenda/components/ProductsDetails';
import TaxCalculation from '@/pages/app/PreenchimentoNotaFiscal/pages/ModeloVenda/components/TaxCalculation';

import { Container, Title } from '@/styles/global';
import { WarningCard, WarningTitle } from './styles';

const ModeloVenda = () => {
  const { id } = useSelector(state => state.activeCompanyState);

  const [form] = Form.useForm();

  const { mutateAsync, isLoading } = useNewSellInvoice();

  const { state } = useLocation();
  const isEditMode = !!state;
  const initialValues = isEditMode
    ? {
        ...state.receiver,
        emissionDate: parseISO(state.receiver.emissionDate),
        inOutDate: parseISO(state.receiver.inOutDate),
        products: state.products.map(product => ({
          isNew: true,
          searchOption: 'code',
          searchTerm: '',
          ...product,
        })),
        ...state.taxes,
      }
    : {
        cnpjCpf: '',
        name: '',
        stateSubscription: '',
        emissionDate: new Date(),
        inOutDate: new Date(),
        cep: '',
        address: '',
        neighborhood: '',
        city: '',
        state: '',
        phoneFax: '',
        products: [
          {
            isNew: true,
            searchOption: 'code',
            searchTerm: '',
            code: '',
            description: '',
            ncmSh: '',
            cst: '',
            icmsTaxation: '',
            cfop: '',
            unit: '',
            quantity: 0,
            unitValue: 0,
            totalValue: 0,
            bcIcms: 0,
            icmsValue: 0,
            ipiValue: 0,
            icmsAliquot: '',
            ipiAliquot: '',
          },
        ],
        bcIcms: 0,
        icmsValue: 0,
        bcIcmsSt: 0,
        icmsStValue: 0,
        ipiValue: 0,
        totalProductsValue: 0,
        other: 0,
        discount: 0,
        totalInvoiceValue: 0,
      };

  const handleSubmit = async values => {
    const formattedReceiver = {
      cnpjCpf: values.cnpjCpf,
      name: values.name,
      stateSubscription: removeNonNumericChars(values.stateSubscription),
      emissionDate: new Date(values.emissionDate).toISOString(),
      inOutDate: new Date(values.inOutDate).toISOString(),
      cep: values.cep,
      address: values.address,
      neighborhood: values.neighborhood,
      city: values.city,
      state: values.state,
      phoneFax: values.phoneFax,
    };

    const formattedProducts = values.products.map(product => {
      const cst = product.cst + product.icmsTaxation;

      delete product.icmsTaxation;
      delete product.isNew;
      delete product.searchOption;
      delete product.searchTerm;

      return {
        ...product,
        quantity: String(product.quantity),
        cst,
      };
    });

    const formattedTaxes = {
      bcIcms: values.bcIcms,
      icmsValue: values.icmsValue,
      bcIcmsSt: values.bcIcmsSt,
      icmsStValue: values.icmsStValue,
      ipiValue: values.ipiValue,
      totalProductsValue: values.totalProductsValue,
      other: values.other,
      discount: values.discount,
      totalInvoiceValue: values.totalInvoiceValue,
    };

    const data = {
      companyInformation: id,
      receiver: formattedReceiver,
      products: formattedProducts,
      taxes: formattedTaxes,
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
            steps={['Dados Cliente', 'Produtos', 'Cálculo Imposto']}
          >
            <Step>
              <ClientDetails form={form} />
            </Step>

            <Step>
              <ProductsDetails form={form} />
            </Step>

            <Step>
              <TaxCalculation form={form} />
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

export default ModeloVenda;
