import PropTypes from 'prop-types';
import { Button, Checkbox, Col, Form as AntForm, Row } from 'antd';
import { useSelector } from 'react-redux';

import useUpdateCompanyAnnex from '@/services/api/hooks/app/DadosEmpresa/useUpdateCompanyAnnex';

import Form from '@/components/Form';

import { Card } from '@/pages/app/DadosEmpresa/styles';

const { Item: FormItem } = AntForm;

const SimpleAnnexes = ({ company }) => {
  const { id } = useSelector(state => state.activeCompanyState);

  const { mutate, isLoading } = useUpdateCompanyAnnex(company.id);

  const isTheActiveCompany = company.id === id;

  const onSubmit = values => {
    const { anexo } = values;

    const data = anexo.map(item => ({ descricao: item }));

    mutate(data);
  };

  const checkedAnnex =
    Array.isArray(company.anexo) && company.anexo.length > 0
      ? company.anexo.map(item => item.descricao.replace('(*)', '').trim())
      : [];

  const options = [
    'Anexo I',
    'Anexo II',
    'Anexo III',
    'Anexo IV',
    'Anexo V',
    'Anexo VI',
  ];

  return (
    <Card>
      <h2>Anexos do Simples</h2>
      <Form
        name="simple-annexes-form"
        initialValues={{
          anexo: checkedAnnex,
        }}
        onFinish={onSubmit}
      >
        <Row gutter={[24, 0]}>
          <Col xs={24}>
            <FormItem name="anexo">
              <Checkbox.Group options={options} />
            </FormItem>
          </Col>

          <Col xs={24} md={8} style={{ marginTop: 20 }}>
            <Button
              type="primary"
              htmlType="submit"
              loading={isLoading}
              disabled={!isTheActiveCompany}
            >
              Alterar
            </Button>
          </Col>
        </Row>
      </Form>
    </Card>
  );
};

SimpleAnnexes.propTypes = {
  company: PropTypes.object.isRequired,
};

export default SimpleAnnexes;
