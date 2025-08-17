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

import { dateFormatter } from '@/utils/formatters';

import Form from '@/components/Form';

const { Item: FormItem } = AntForm;

const ExigSuspensaTables = ({ data = null, onSubmit }) => {
  return data?.length === 0 || !data ? (
    <Result
      title="Nenhum imposto cadastrado"
      subTitle={
        <p style={{ fontSize: '1rem' }}>
          Utilize a aba &quot;Confirmação&quot; para cadastrar um novo imposto.
        </p>
      }
    />
  ) : (
    <Collapse accordion>
      {data?.map((item, index) => {
        const {
          nomeImposto,
          motivo,
          numPocesso,
          vara,
          uf,
          municipio,
          dataInicial,
          dataFinal,
        } = item;

        return (
          <Collapse.Panel
            header={`${nomeImposto} - Válida até ${dateFormatter(dataFinal)}`}
            key={index}
          >
            <Form
              name="edit-exig-suspensa-form"
              initialValues={{
                [`nomeImposto-${index}`]: nomeImposto,
                [`motivo-${index}`]: motivo,
                [`numPocesso-${index}`]: numPocesso,
                [`vara-${index}`]: vara,
                [`uf-${index}`]: uf,
                [`municipio-${index}`]: municipio,
                [`dataInicial-${index}`]: dateFormatter(dataInicial),
                [`dataFinal-${index}`]: dateFormatter(dataFinal),
              }}
              onFinish={onSubmit}
            >
              <Row gutter={[24, 0]}>
                <Col xs={24} md={3}>
                  <FormItem name={`nomeImposto-${index}`} label="Imposto">
                    <Input disabled />
                  </FormItem>
                </Col>

                <Col xs={24} md={4}>
                  <FormItem
                    name={`motivo-${index}`}
                    label="Motivo da Suspensão"
                  >
                    <Input disabled />
                  </FormItem>
                </Col>

                <Col xs={24} md={4}>
                  <FormItem
                    name={`numProcesso-${index}`}
                    label="Número do Processo"
                  >
                    <Input disabled />
                  </FormItem>
                </Col>

                <Col xs={24} md={4}>
                  <FormItem name={`vara-${index}`} label="Vara">
                    <Input disabled />
                  </FormItem>
                </Col>

                <Col xs={24} md={2}>
                  <FormItem name={`uf-${index}`} label="UF">
                    <Input disabled />
                  </FormItem>
                </Col>

                <Col xs={24} md={4}>
                  <FormItem name={`municipio-${index}`} label="Município">
                    <Input disabled />
                  </FormItem>
                </Col>

                <Col xs={24} md={3}>
                  <FormItem name={`dataInicial-${index}`} label="Data Inicial">
                    <Input disabled />
                  </FormItem>
                </Col>

                <Col xs={24} md={3}>
                  <FormItem name={`dataFinal-${index}`} label="Data Final">
                    <Input disabled />
                  </FormItem>
                </Col>

                <Col
                  xs={24}
                  md={3}
                  style={{
                    marginTop: 20,
                    display: 'flex',
                    alignItems: 'center',
                  }}
                >
                  <FormItem noStyle>
                    <Button type="primary" htmlType="submit">
                      Invalidar
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

ExigSuspensaTables.propTypes = {
  data: PropTypes.array,
  onSubmit: PropTypes.func.isRequired,
};

ExigSuspensaTables.defaultProps = {
  data: null,
};

export default ExigSuspensaTables;
