import PropTypes from 'prop-types';
import {
  Button,
  Col,
  Collapse,
  Form as AntForm,
  Input,
  Result,
  Row,
} from 'antd';
import { parseISO } from 'date-fns';

import useNewIcmsStTax from '@/services/api/hooks/app/IcmsSt/useNewIcmsStTax';
import { dateFormatter } from '@/utils/formatters';

import Form from '@/components/Form';
import { DatePickerInput } from '@/components/Form/Input';

const { Item: FormItem } = AntForm;

const EditAntecipacaoForm = ({ data = null, onSubmit }) => {
  const mutation = useNewIcmsStTax('antEncTributacao');

  return data?.length === 0 || !data ? (
    <Result
      title="Nenhum NCM cadastrado"
      subTitle={
        <p style={{ fontSize: '1rem' }}>
          Utilize a aba &quot;Confirmação&quot; para cadastrar um novo NCM.
        </p>
      }
    />
  ) : (
    <Collapse accordion>
      {data?.map((item, index) => {
        const { ncm, descricao, dataInicial, dataFinal } = item;

        return (
          <Collapse.Panel
            key={index}
            header={`${ncm} - Válido até ${dateFormatter(dataFinal)}`}
          >
            <Form
              name="edit-antecipacao-form"
              initialValues={{
                [`ncm-${index}`]: ncm ?? '',
                [`descricao-${index}`]: descricao ?? '',
                [`dataInicial-${index}`]: dataInicial
                  ? parseISO(dataInicial)
                  : '',
                [`dataFinal-${index}`]: dataFinal ? parseISO(dataFinal) : '',
              }}
              onFinish={onSubmit}
            >
              <Row gutter={[24, 0]}>
                <Col xs={24} md={3}>
                  <FormItem
                    name={`ncm-${index}`}
                    label="NCM"
                    rules={[
                      {
                        required: true,
                        message: 'Campo obrigatório',
                      },
                    ]}
                  >
                    <Input />
                  </FormItem>
                </Col>

                <Col xs={24} md={13}>
                  <FormItem
                    name={`descricao-${index}`}
                    label="Descrição"
                    rules={[
                      {
                        required: true,
                        message: 'Campo obrigatório',
                      },
                    ]}
                  >
                    <Input />
                  </FormItem>
                </Col>

                <Col xs={24} md={3}>
                  <FormItem
                    name={`dataInicial-${index}`}
                    label="Data Inicial"
                    rules={[
                      {
                        required: true,
                        message: 'Campo obrigatório',
                      },
                    ]}
                  >
                    <DatePickerInput format="DD/MM/YYYY" />
                  </FormItem>
                </Col>

                <Col xs={24} md={3}>
                  <FormItem
                    shouldUpdate={(prevValues, currValues) =>
                      prevValues[`dataInicial-${index}`] !==
                      currValues[`dataInicial-${index}`]
                    }
                    noStyle
                  >
                    {({ getFieldValue }) => {
                      const initialDate = getFieldValue(`dataInicial-${index}`);

                      return (
                        <FormItem
                          name={`dataFinal-${index}`}
                          label="Data Final"
                        >
                          <DatePickerInput
                            format="DD/MM/YYYY"
                            disabledDate={current => current < initialDate}
                          />
                        </FormItem>
                      );
                    }}
                  </FormItem>
                </Col>

                <Col
                  xs={24}
                  md={2}
                  style={{
                    marginTop: 20,
                  }}
                >
                  <FormItem noStyle>
                    <Button
                      type="primary"
                      htmlType="submit"
                      disabled={mutation.isLoading}
                      style={{
                        marginTop: 20,
                      }}
                    >
                      Alterar
                    </Button>
                  </FormItem>
                </Col>
              </Row>
            </Form>
          </Collapse.Panel>
        );
      })}
    </Collapse>
  );
};

EditAntecipacaoForm.propTypes = {
  data: PropTypes.array,
  onSubmit: PropTypes.func.isRequired,
};

EditAntecipacaoForm.defaultProps = {
  data: null,
};

export default EditAntecipacaoForm;
