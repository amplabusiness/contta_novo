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

const AntecipacaoTables = ({ data = null }) => {
  const { mutate, isLoading } = useUpdateIcmsStTax('antEncTributacao');

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
      title="Nenhum NCM cadastrado"
      subTitle={
        <p style={{ fontSize: '1rem' }}>
          Utilize a aba anterior para cadastrar um novo NCM.
        </p>
      }
    />
  ) : (
    <Collapse accordion>
      {data?.map((item, index) => {
        const { _id, ncm, descricao, dataInicial, dataFinal, status } = item;
        const taxStatus = status.toLowerCase();
        const isActive = taxStatus === 'ativo';

        const isExpiredTable =
          differenceInDays(parseISO(dataFinal), new Date()) < 0;

        const canBeInvalidated = !isExpiredTable && isActive;

        return (
          <Collapse.Panel
            header={`${ncm} - Válido até ${dateFormatter(dataFinal)}`}
            key={index}
          >
            <Form
              name="antecipacao-tables-form"
              initialValues={{
                [`ncm-${index}`]: ncm,
                [`descricao-${index}`]: descricao,
                [`dataInicial-${index}`]: dateFormatter(dataInicial),
                [`dataFinal-${index}`]: dateFormatter(dataFinal),
              }}
            >
              <Row gutter={[24, 0]}>
                <Col xs={24} md={3}>
                  <FormItem name={`ncm-${index}`} label="NCM">
                    <Input disabled />
                  </FormItem>
                </Col>

                <Col xs={24} md={12}>
                  <FormItem name={`descricao-${index}`} label="Descrição">
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
                    marginTop: 20,
                    display: 'flex',
                    gap: 24,
                    alignItems: 'flex-end',
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

AntecipacaoTables.propTypes = {
  data: PropTypes.oneOfType([PropTypes.array, PropTypes.string]),
};

AntecipacaoTables.defaultProps = {
  data: null,
};

export default AntecipacaoTables;
