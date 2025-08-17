import { useState } from 'react';
import PropTypes from 'prop-types';
import { Button, Col, Form as AntForm, Modal, Row } from 'antd';
import { GoPencil } from 'react-icons/go';

import Form from '@/components/Form';
import { DatePickerInput } from '@/components/Form/Input';

import { Container, ActionButton } from './styles';

const { Item: FormItem } = AntForm;

const ChangeDate = ({ nf }) => {
  const [visible, setVisible] = useState(false);

  const [form] = AntForm.useForm();

  const openModal = () => setVisible(true);

  const closeModal = () => setVisible(false);

  const handleSubmit = async values => {
    const data = {
      id: nf.id,
      ...values,
    };

    alert(JSON.stringify(data, null, 2));

    closeModal();
  };

  return (
    <Container>
      <ActionButton onClick={openModal}>
        <GoPencil color="#fff" />
      </ActionButton>

      <Modal
        title="Alterar data de emissão da NF-e"
        visible={visible}
        onCancel={closeModal}
        footer={[
          <Button key="cancel" type="default" onClick={closeModal}>
            Cancelar
          </Button>,
          <Button
            key="ok"
            type="primary"
            onClick={() => {
              form.submit();
            }}
          >
            Enviar
          </Button>,
        ]}
        okText="Alterar"
        width={500}
        destroyOnClose
      >
        <Form
          name="change-nf-date-form"
          form={form}
          initialValues={{ dataAtual: new Date(nf.emissionDate), novaData: '' }}
          onFinish={handleSubmit}
        >
          <Row gutter={[24, 0]} align="middle" justify="center">
            <Col xs={24} md={12}>
              <FormItem name="dataAtual" label="Data Atual">
                <DatePickerInput format="DD/MM/YYYY" disabled />
              </FormItem>
            </Col>

            <Col xs={24} md={12}>
              <FormItem
                name="novaData"
                label="Nova Data"
                rules={[{ required: true, message: 'Campo obrigatório' }]}
              >
                <DatePickerInput format="DD/MM/YYYY" />
              </FormItem>
            </Col>
          </Row>
        </Form>
      </Modal>
    </Container>
  );
};

ChangeDate.propTypes = {
  nf: PropTypes.object.isRequired,
};

export default ChangeDate;
