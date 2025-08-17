import { useState, useEffect, useCallback } from 'react';
import PropTypes from 'prop-types';
import { Col, Form as AntForm, Row } from 'antd';
import { useSelector } from 'react-redux';

import Form from '@/components/Form';
import { CurrencyInput } from '@/components/Form/Input';

const { Item: FormItem } = AntForm;

const OldCompany = ({ form, handleSubmit }) => {
  const { faturamentos } = useSelector(
    state => state.activeCompanyState.data.faturamentoAnual,
  );
  const { date } = useSelector(state => state.referenceDateState);

  const [lastMonths, setLastMonths] = useState([]);
  const [fieldNames, setFieldNames] = useState({});

  const getLastTwelveMonths = useCallback(() => {
    const foundMonths = Array.from(Array(12).keys()).map((item, index) => {
      const selectedMonth = new Date(date);
      selectedMonth.setDate(1);
      selectedMonth.setMonth(selectedMonth.getMonth() - (index + 1));

      const fullMonthBr = selectedMonth.toLocaleDateString('pt-BR', {
        month: 'long',
      });
      const fullMonthEn = selectedMonth.toLocaleDateString('en-US', {
        month: 'long',
      });
      const year = selectedMonth.getFullYear();

      const name = `${fullMonthEn}/${year}`;
      const label = `${
        fullMonthBr.charAt(0).toUpperCase() + fullMonthBr.slice(1, 3)
      }/${year}`;

      return {
        name,
        label,
      };
    });

    const foundFieldNames = foundMonths
      .map(item => item.name)
      .reduce((result, item, index) => {
        result[item] = faturamentos[index]
          ? faturamentos[index].valorFaturamento
          : 0;
        return result;
      }, {});

    const total = Object.values(foundFieldNames).reduce(
      (acc, curr) => acc + curr,
      0,
    );

    setLastMonths(foundMonths);
    setFieldNames({ ...foundFieldNames, total });
  }, [date, faturamentos]);

  useEffect(() => {
    // Variável que evita mudanças de estado quando o componente já foi desmontado
    let isMounted = true;

    if (isMounted) {
      getLastTwelveMonths();
    }

    return () => {
      isMounted = false;
    };
  }, [getLastTwelveMonths]);

  return (
    lastMonths.length > 0 && (
      <>
        <h2 style={{ margin: '0 0 10px 0' }}>
          Faturamento dos últimos 12 meses
        </h2>

        <Form
          name="old-company-income-form"
          form={form}
          initialValues={fieldNames}
          onValuesChange={(_, values) => {
            const sum = Object.values(values)
              .slice(0, -1)
              .reduce((acc, curr) => Number(curr) + acc, 0);

            form.setFields([
              {
                name: 'total',
                value: sum,
              },
            ]);
          }}
          onFinish={handleSubmit('old')}
        >
          <Row gutter={[24, 0]}>
            {lastMonths.map(item => (
              <Col key={item.name} xs={24} md={8}>
                <FormItem name={item.name} label={item.label}>
                  <CurrencyInput />
                </FormItem>
              </Col>
            ))}

            <Col xs={24} md={8}>
              <FormItem name="total" label="Total">
                <CurrencyInput disabled />
              </FormItem>
            </Col>

            <Col xs={24} md={16} style={{ marginTop: 20 }}>
              <FormItem noStyle shouldUpdate>
                {({ getFieldsValue }) => {
                  if (Object.values(getFieldsValue(true)).includes(0)) {
                    return (
                      <FormItem>
                        <strong>Atenção:</strong> Você informou faturamento
                        zerado e um ou mais meses. Antes de fechar, verifique se
                        os valores estão corretos.
                      </FormItem>
                    );
                  }

                  return null;
                }}
              </FormItem>
            </Col>
          </Row>
        </Form>
      </>
    )
  );
};

OldCompany.propTypes = {
  form: PropTypes.object.isRequired,
  handleSubmit: PropTypes.func.isRequired,
};

export default OldCompany;
