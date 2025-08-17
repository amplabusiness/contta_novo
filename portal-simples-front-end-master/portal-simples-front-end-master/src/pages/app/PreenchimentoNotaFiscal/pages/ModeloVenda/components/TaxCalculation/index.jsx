import { useEffect } from 'react';
import PropTypes from 'prop-types';
import { Col, Form, Row } from 'antd';

import { CurrencyInput } from '@/components/Form/Input';

const { Item: FormItem } = Form;

const TaxCalculation = ({ form }) => {
  useEffect(() => {
    const products = form.getFieldValue('products');

    const totalizers = products.reduce((obj, curr) => {
      obj.bcIcmsSum = obj.bcIcmsSum + curr.bcIcms || curr.bcIcms;
      obj.icmsValueSum = obj.icmsValueSum + curr.icmsValue || curr.icmsValue;
      obj.ipiValueSum = obj.ipiValueSum + curr.ipiValue || curr.ipiValue;
      obj.totalValueSum =
        obj.totalValueSum + curr.totalValue || curr.totalValue;

      return obj;
    }, {});

    form.setFields([
      {
        name: 'bcIcms',
        value: totalizers.bcIcmsSum,
      },
      {
        name: 'icmsValue',
        value: totalizers.icmsValueSum,
      },
      {
        name: 'ipiValue',
        value: totalizers.ipiValueSum,
      },
      {
        name: 'totalProductsValue',
        value: totalizers.totalValueSum,
      },
      {
        name: 'totalInvoiceValue',
        value: totalizers.totalValueSum,
      },
    ]);

    // eslint-disable-next-line
  }, []);

  const updateInvoiceValue = () => {
    const isBothFieldsTouched = form.isFieldsTouched(['other', 'discount']);

    if (isBothFieldsTouched) {
      const otherValue = form.getFieldValue('other');
      const discountValue = form.getFieldValue('discount');
      const invoiceValue = form.getFieldValue('totalProductsValue');

      form.setFields([
        {
          name: 'totalInvoiceValue',
          value: otherValue - discountValue + invoiceValue,
        },
      ]);
    }
  };

  return (
    <Row gutter={[24, 0]}>
      <Col xs={24} md={2}>
        <FormItem name="bcIcms" label="BC ICMS">
          <CurrencyInput disabled />
        </FormItem>
      </Col>
      <Col xs={24} md={2}>
        <FormItem name="icmsValue" label="Vlr. ICMS">
          <CurrencyInput disabled />
        </FormItem>
      </Col>
      <Col xs={24} md={2}>
        <FormItem name="bcIcmsSt" label="BC ICMS ST">
          <CurrencyInput disabled />
        </FormItem>
      </Col>
      <Col xs={24} md={2}>
        <FormItem name="icmsStValue" label="Vlr. ICMS ST">
          <CurrencyInput disabled />
        </FormItem>
      </Col>
      <Col xs={24} md={2}>
        <FormItem name="ipiValue" label="Vlr. IPI">
          <CurrencyInput disabled />
        </FormItem>
      </Col>
      <Col xs={24} md={4}>
        <FormItem name="totalProductsValue" label="Vlr. Total Produtos">
          <CurrencyInput disabled />
        </FormItem>
      </Col>
      <Col xs={24} md={4}>
        <FormItem name="other" label="Outras Despesas Acessórias">
          <CurrencyInput onChange={updateInvoiceValue} />
        </FormItem>
      </Col>
      <Col xs={24} md={2}>
        <FormItem name="discount" label="Desconto">
          <CurrencyInput onChange={updateInvoiceValue} />
        </FormItem>
      </Col>
      <Col xs={24} md={4}>
        <FormItem name="totalInvoiceValue" label="Vlr. Total Nota">
          <CurrencyInput disabled />
        </FormItem>
      </Col>
    </Row>
  );
};

TaxCalculation.propTypes = {
  form: PropTypes.object.isRequired,
};

export default TaxCalculation;
