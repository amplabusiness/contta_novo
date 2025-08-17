import { useState, useEffect, useCallback } from 'react';
import { Button, Col, Form as AntForm, Modal, Row, Tooltip } from 'antd';
import { differenceInMonths, parseISO } from 'date-fns';
import { FaPencilAlt } from 'react-icons/fa';
import { useSelector } from 'react-redux';

import useUpdateCompanyAnnualIncome from '@/services/api/hooks/app/Dashboard/useUpdateCompanyAnnualIncome';

import Form from '@/components/Form';
import { CurrencyInput } from '@/components/Form/Input';

import { UpdateButton } from './styles';

const { Item: FormItem } = AntForm;

const UpdateAnnualIncome = () => {
  const { id: companyId } = useSelector(state => state.activeCompanyState);
  const { simplesNacional = {}, faturamentoAnual = {} } = useSelector(
    state => state.activeCompanyState.data,
  );
  const { id: incomeId, faturamentos } = useSelector(
    state => state.activeCompanyState.data.faturamentoAnual,
  );
  const { date } = useSelector(state => state.referenceDateState);

  const [isVisible, setIsVisible] = useState(false);
  const [lastMonths, setLastMonths] = useState([]);
  const [fieldNames, setFieldNames] = useState({});

  const [form] = AntForm.useForm();

  const { mutate, isLoading } = useUpdateCompanyAnnualIncome();

  const { faturamentoFechado = false } = faturamentoAnual;

  // Diferença entre a data de referência e a data de fundação da empresa
  const { dateFounded } = simplesNacional;
  const differenceBetweenDates = differenceInMonths(
    parseISO(date),
    parseISO(dateFounded),
  );
  const isNewCompany =
    differenceBetweenDates >= 0 && differenceBetweenDates < 12;

  const getLastTwelveMonths = useCallback(() => {
    const foundMonths = Array.from(Array(faturamentos.length).keys()).map(
      (item, index) => {
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
      },
    );

    const foundFieldNames = foundMonths
      .map(item => item.name)
      .reduce((result, item, index) => {
        result[item] = faturamentos[index].valorFaturamento;
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

  const openModal = () => {
    setIsVisible(true);
  };

  const closeModal = () => {
    setIsVisible(false);
  };

  const onSubmit = values => {
    const data = {
      id: incomeId,
      companyInformation: companyId,
      faturamentoFechado: true,
      fechamentoEmp: isNewCompany,
      faturamentos: Object.entries(values)
        .slice(0, -1)
        .map(([key, value], index) => {
          const monthIncomeId = faturamentos[index].id;

          const rawDate = `01/${key}`;
          // Ex. de valor do rawDate: 01/June/2019
          const parsedDate = new Date(rawDate);
          const incomeDate = parsedDate.toISOString();
          const incomeYear = parsedDate.getFullYear();
          const incomeValue = value;

          return {
            dataFaturamento: incomeDate,
            ano: incomeYear,
            valorFaturamento: incomeValue,
            id: monthIncomeId,
          };
        }),
    };

    mutate(data);
  };

  if (!faturamentoFechado) {
    return null;
  }

  return (
    <>
      <Tooltip title="Atualizar faturamento anual">
        <UpdateButton onClick={openModal}>
          <FaPencilAlt size={18} color="#fff" />
        </UpdateButton>
      </Tooltip>
      <Modal
        title="Atualização do faturamento anual"
        visible={isVisible}
        onOk={closeModal}
        onCancel={closeModal}
        footer={[
          <Button
            key="cancel"
            type="default"
            onClick={closeModal}
            disabled={isLoading}
          >
            Cancelar
          </Button>,
          <Button
            key="confirm"
            type="primary"
            onClick={() => {
              form.submit();
            }}
            disabled={isLoading}
          >
            Enviar
          </Button>,
        ]}
        width={800}
        destroyOnClose
        bodyStyle={{ padding: '44px 24px' }}
      >
        <Form
          name="update-annual-income-form"
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
          onFinish={onSubmit}
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
      </Modal>
    </>
  );
};

export default UpdateAnnualIncome;
