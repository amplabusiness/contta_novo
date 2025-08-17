import PropTypes from 'prop-types';
import {
  Button,
  Col,
  Collapse,
  Form as AntForm,
  Input,
  notification,
  Popconfirm,
  Result,
  Row,
} from 'antd';
import { differenceInDays, parseISO } from 'date-fns';

import useUpdateIcmsStTax from '@/services/api/hooks/app/IcmsSt/useUpdateIcmsStTax';
import { dateFormatter } from '@/utils/formatters';

import Form from '@/components/Form';

import { Status } from './styles';

const { Item: FormItem } = AntForm;

const ExigSuspensaTables = ({ data = null }) => {
  const { mutate, isLoading } = useUpdateIcmsStTax('exigSuspensa');

  const invalidateTable = id => {
    const values = {
      id,
      status: false,
    };

    mutate(values, {
      onSuccess: () => {
        notification.success({
          message: 'Sucesso',
          description: 'Regra invalidada com sucesso!',
        });
      },
      onError: () => {
        notification.error({
          message: 'Erro',
          description: 'Não foi possível invalidar a regra no momento',
        });
      },
    });
  };

  return data?.length === 0 || !data ? (
    <Result
      title="Nenhum imposto cadastrado"
      subTitle={
        <p style={{ fontSize: '1rem' }}>
          Utilize a aba anterior para cadastrar um novo imposto.
        </p>
      }
    />
  ) : (
    <Collapse accordion>
      {data?.map((item, index) => {
        const {
          _id,
          nomeImposto,
          motivo,
          numPocesso,
          vara,
          uf,
          municipio,
          dataInicial,
          dataFinal,
          status,
        } = item;
        const taxStatus = status.toLowerCase();
        const isActive = taxStatus === 'ativo';

        const isExpiredTable =
          differenceInDays(parseISO(dataFinal), new Date()) < 0;

        const canBeInvalidated = !isExpiredTable && isActive;

        return (
          <Collapse.Panel
            header={`${nomeImposto} - Válido até ${dateFormatter(dataFinal)}`}
            key={index}
          >
            <Form
              name="exig-supensa-tables-form"
              initialValues={{
                [`nomeImposto-${index}`]: nomeImposto,
                [`motivo-${index}`]: motivo,
                [`numProcesso-${index}`]: numPocesso,
                [`vara-${index}`]: vara,
                [`uf-${index}`]: uf,
                [`municipio-${index}`]: municipio,
                [`dataInicial-${index}`]: dateFormatter(dataInicial),
                [`dataFinal-${index}`]: dateFormatter(dataFinal),
              }}
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
                  md={21}
                  style={{
                    marginTop: 18,
                    display: 'flex',
                    gap: 24,
                    alignItems: 'center',
                  }}
                >
                  <FormItem noStyle>
                    <Status isActive={isActive}>
                      {isActive ? 'Ativa' : 'Desativada'}
                    </Status>
                    {canBeInvalidated && (
                      <Popconfirm
                        title="Tem certeza que deseja invalidar essa regra?"
                        onConfirm={() => invalidateTable(_id)}
                        okText="Sim"
                      >
                        <Button type="primary" disabled={isLoading}>
                          Invalidar regra
                        </Button>
                      </Popconfirm>
                    )}
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
  data: PropTypes.oneOfType([PropTypes.array, PropTypes.string]),
};

ExigSuspensaTables.defaultProps = {
  data: null,
};

export default ExigSuspensaTables;
