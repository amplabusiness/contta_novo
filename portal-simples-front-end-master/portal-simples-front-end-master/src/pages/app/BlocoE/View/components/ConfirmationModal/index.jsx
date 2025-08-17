import { useEffect, useState } from 'react';
import PropTypes from 'prop-types';
import { Button, Col, Form as AntForm, Modal, Row, Spin } from 'antd';
import { useSelector } from 'react-redux';
import { useHistory } from 'react-router-dom';

import useGenerateEBlockData from '@/services/api/hooks/app/BlocoE/useGenerateEBlockData';
import useRegisterEBlock from '@/services/api/hooks/app/BlocoE/useRegisterEBlock';

import Form from '@/components/Form';
import { CurrencyInput } from '@/components/Form/Input';

const { Item: FormItem } = AntForm;

const ConfirmationModal = ({ open, onOk, onCancel, EBlockData }) => {
  const { id } = useSelector(state => state.activeCompanyState);

  const [formValues, setFormValues] = useState({});

  const { push } = useHistory();

  const [form] = AntForm.useForm();

  const {
    mutate: generateEBlockDataMutate,
    isLoading: isGenerateEBlockDataLoading,
  } = useGenerateEBlockData();
  const { mutate: registerEBlockMutate, isLoading: isRegisterEBlockLoading } =
    useRegisterEBlock();

  useEffect(() => {
    if (open) {
      const adjustmentCodesList = EBlockData.e111.map(item => ({
        codAjust: item.codAjuste,
        valorAjuste: item.valorAjuste ?? item.vlTotalNfe,
      }));
      const debitBalanceValue = EBlockData.e110[0].valorSaldoDevedor;
      const dataToMutate = { adjustmentCodesList, debitBalanceValue };

      generateEBlockDataMutate(dataToMutate, {
        onSuccess: data => {
          setFormValues(data);
        },
      });
    }
  }, [open, generateEBlockDataMutate, EBlockData]);

  const onSubmit = values => {
    const data = {
      companyInformationId: id,
      e100: EBlockData.e100[0],
      e110: {
        ...values,
      },
      e111: EBlockData.e111.map(item => ({
        codAjuste: item.codAjuste,
        descComplementar: item.descComplementar ?? item.descricaoAjuste,
        valorAjuste: item.valorAjuste ?? item.vlTotalNfe,
      })),
      e113: EBlockData.e113[0],
      e115: EBlockData.e115[0],
      e116: EBlockData.e116[0],
    };

    registerEBlockMutate(data, {
      onSuccess: () => {
        push('/dashboard', {
          simplesValue: data.e110.valorSaldoDevedor,
        });
      },
    });
  };

  return (
    <Modal
      visible={open}
      title="Confirmação do Bloco E"
      onOk={onOk}
      onCancel={onCancel}
      width={800}
      footer={[
        <Button
          key="cancel"
          type="default"
          onClick={onCancel}
          disabled={isRegisterEBlockLoading}
        >
          Cancelar
        </Button>,
        <Button
          key="confirm"
          type="primary"
          loading={isRegisterEBlockLoading}
          onClick={() => {
            form.submit();
          }}
        >
          Enviar
        </Button>,
      ]}
      centered
      destroyOnClose
    >
      {isGenerateEBlockDataLoading ? (
        <Row align="middle" justify="center" style={{ margin: '30px 0' }}>
          <Spin size="large" />
        </Row>
      ) : (
        <Form
          name="confirm-e-block-form"
          form={form}
          initialValues={{
            ...formValues,
            valorRecolhidos: 0,
          }}
          onFinish={onSubmit}
        >
          <Row gutter={[24, 0]}>
            <Col xs={24} md={12}>
              <FormItem
                name="valorDebitoImpostos"
                label="Valor Total dos Débitos do Imposto"
              >
                <CurrencyInput disabled />
              </FormItem>
            </Col>

            <Col xs={24} md={12}>
              <FormItem
                name="valorAjustesDebitoDocFiscal"
                label="Valor Total AJustes a Débito (doc. fiscal)"
              >
                <CurrencyInput disabled />
              </FormItem>
            </Col>

            <Col xs={24} md={12}>
              <FormItem
                name="valorAjustesDebito"
                label="Valor Total dos AJustes a Débito"
              >
                <CurrencyInput disabled />
              </FormItem>
            </Col>

            <Col xs={24} md={12}>
              <FormItem
                name="valorEstornosCreditos"
                label="Valor Total dos Estornos de Créditos"
              >
                <CurrencyInput disabled />
              </FormItem>
            </Col>

            <Col xs={24} md={12}>
              <FormItem
                name="valorCreditoImpostos"
                label="Valor Total dos Créditos do Imposto"
              >
                <CurrencyInput disabled />
              </FormItem>
            </Col>

            <Col xs={24} md={12}>
              <FormItem
                name="valorAjustesCreditoDocFiscal"
                label="Valor Total AJustes a Crédito (doc. fiscal)"
              >
                <CurrencyInput disabled />
              </FormItem>
            </Col>

            <Col xs={24} md={12}>
              <FormItem
                name="valorAjustesCredito"
                label="Valor Total dos AJustes a Crédito"
              >
                <CurrencyInput disabled />
              </FormItem>
            </Col>

            <Col xs={24} md={12}>
              <FormItem
                name="valorEstornosDebitos"
                label="Valor Total dos Estornos de Débitos"
              >
                <CurrencyInput disabled />
              </FormItem>
            </Col>

            <Col xs={24} md={12}>
              <FormItem
                name="saldoCredorAnterior"
                label="Saldo Credor do Período Anterior"
              >
                <CurrencyInput disabled />
              </FormItem>
            </Col>

            <Col xs={24} md={12}>
              <FormItem name="valorSaldoDevedor" label="Valor do Saldo Devedor">
                <CurrencyInput disabled />
              </FormItem>
            </Col>

            <Col xs={24} md={12}>
              <FormItem name="valorDeducoes" label="Valor Total das Deduções">
                <CurrencyInput disabled />
              </FormItem>
            </Col>

            <Col xs={24} md={12}>
              <FormItem
                name="valorIcmsRecolher"
                label="Valor Total do ICMS a Recolher"
              >
                <CurrencyInput disabled />
              </FormItem>
            </Col>

            <Col xs={24} md={12}>
              <FormItem
                name="valorSaldoCredorIcms"
                label="Valor do Saldo Credor do ICMS"
              >
                <CurrencyInput disabled />
              </FormItem>
            </Col>

            <Col xs={24} md={12}>
              <FormItem
                name="valorRecolhidos"
                label="Valores Recolhidos ou a Recolher"
              >
                <CurrencyInput disabled />
              </FormItem>
            </Col>
          </Row>
        </Form>
      )}
    </Modal>
  );
};

ConfirmationModal.propTypes = {
  open: PropTypes.bool.isRequired,
  onOk: PropTypes.func.isRequired,
  onCancel: PropTypes.func.isRequired,
  EBlockData: PropTypes.object.isRequired,
};

export default ConfirmationModal;
