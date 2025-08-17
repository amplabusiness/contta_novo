import PropTypes from 'prop-types';
import { Col, Divider, Form as AntForm, Row } from 'antd';
import { differenceInMonths, format, parseISO } from 'date-fns';
import { useSelector } from 'react-redux';

import Form from '@/components/Form';
import { CurrencyInput } from '@/components/Form/Input';

import { Container } from './styles';

const { Item: FormItem } = AntForm;

const NewCompany = ({ form, handleSubmit }) => {
  const { date } = useSelector(state => state.referenceDateState);
  const {
    data: { simplesNacional = {} },
  } = useSelector(state => state.activeCompanyState);
  const { dateFounded = null } = simplesNacional;

  const dateDifference = differenceInMonths(
    parseISO(date),
    parseISO(dateFounded),
  );

  const fieldName = format(new Date(), "MMM'/'yyyy");

  return (
    <Container>
      <h2>
        {dateDifference > 1
          ? `Faturamento acumulado nos últimos ${dateDifference} meses`
          : dateDifference === 1
          ? 'Faturamento acumulado no último mês'
          : 'Faturamento acumulado nesse mês'}
      </h2>

      <Divider />

      <Form
        name="new-company-income-form"
        form={form}
        initialValues={{ [fieldName]: 0 }}
        onFinish={handleSubmit('new')}
      >
        <Row gutter={[24, 0]}>
          <Col xs={24} md={8}>
            <FormItem name={fieldName} label="Faturamento acumulado">
              <CurrencyInput />
            </FormItem>
          </Col>

          <Col
            xs={24}
            md={16}
            style={{
              marginTop: 20,
              display: 'flex',
              alignItems: 'center',
            }}
          >
            <FormItem noStyle shouldUpdate>
              {({ getFieldValue }) => {
                const value = getFieldValue(fieldName);

                if (value === 0) {
                  <p>
                    <strong>Atenção:</strong> Você informou faturamento zerado.
                    Antes de fechar, verifique se o valor está correto.
                  </p>;
                }

                return null;
              }}
            </FormItem>
          </Col>
        </Row>
      </Form>
    </Container>
  );
};

NewCompany.propTypes = {
  form: PropTypes.object.isRequired,
  handleSubmit: PropTypes.func.isRequired,
};

export default NewCompany;
