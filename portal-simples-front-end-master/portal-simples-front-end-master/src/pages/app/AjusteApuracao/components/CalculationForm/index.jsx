import PropTypes from 'prop-types';
import { Button, Col, Form as AntForm, Row } from 'antd';

import Form from '@/components/Form';
import { CurrencyInput, PercentageInput } from '@/components/Form/Input';

const { Item: FormItem } = AntForm;

const CalculationForm = ({ nfeValue, onSubmit, isLoading }) => {
  const [form] = AntForm.useForm();

  const updateCalculationValue = () => {
    const nfeVal = form.getFieldValue('totalNfe');
    const aliquotVal = Number(form.getFieldValue('aliquota')) / 100;

    form.setFields([
      {
        name: 'totalCalculo',
        value: nfeVal * aliquotVal,
      },
    ]);
  };

  const onCalculationFormSubmit = async values => {
    try {
      const data = {
        ...values,
        aliquota: Number(values.aliquota) / 100,
      };

      await onSubmit(data);

      form.resetFields();
    } catch (error) {
      //
    }
  };

  return (
    <Form
      name="calculation-adjustment-form"
      form={form}
      initialValues={{
        totalNfe: nfeValue,
        aliquota: 0,
        totalCalculo: 0,
      }}
      onFinish={onCalculationFormSubmit}
    >
      <Row gutter={[24, 0]}>
        <Col xs={24} md={4}>
          <FormItem name="totalNfe" label="Total da NF-e">
            <CurrencyInput disabled />
          </FormItem>
        </Col>

        <Col xs={24} md={4}>
          <FormItem name="aliquota" label="Alíquota (%)">
            <PercentageInput onChange={updateCalculationValue} />
          </FormItem>
        </Col>

        <Col xs={24} md={4}>
          <FormItem name="totalCalculo" label="Total do Cálculo">
            <CurrencyInput disabled />
          </FormItem>
        </Col>

        <Col
          xs={24}
          md={3}
          style={{ marginTop: 16, display: 'flex', alignItems: 'center' }}
        >
          <FormItem noStyle>
            <Button
              type="primary"
              htmlType="submit"
              disabled={isLoading}
              style={{ width: '100%' }}
            >
              Confirmar
            </Button>
          </FormItem>
        </Col>
      </Row>
    </Form>
  );
};

CalculationForm.propTypes = {
  nfeValue: PropTypes.number.isRequired,
  onSubmit: PropTypes.func.isRequired,
  isLoading: PropTypes.bool.isRequired,
};

export default CalculationForm;
