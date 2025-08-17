import { useState } from 'react';
import { Button, Col, Form as AntForm, Modal, Row, Tooltip } from 'antd';
import { parseISO } from 'date-fns';
import { FaPencilAlt } from 'react-icons/fa';
import { useSelector, useDispatch } from 'react-redux';

import useUpdateCompanyFoundationDate from '@/services/api/hooks/app/Dashboard/useUpdateCompanyFoundationDate';
import { setCompanyFoundationDate } from '@/store/slices/activeCompany';

import Form from '@/components/Form';
import { DatePickerInput } from '@/components/Form/Input';

import { UpdateButton } from './styles';

const { Item: FormItem } = AntForm;

const UpdateFoundationDate = () => {
  const { id } = useSelector(state => state.activeCompanyState);
  const { simplesNacional = {} } = useSelector(
    state => state.activeCompanyState.data,
  );
  const { clickedDownLoadButton } = useSelector(
    state => state.configurationsState,
  );
  const dispatch = useDispatch();

  const { dateFounded = null } = simplesNacional;

  const [isVisible, setIsVisible] = useState(false);

  const [form] = AntForm.useForm();

  const { mutate, isLoading } = useUpdateCompanyFoundationDate();

  const openModal = () => {
    setIsVisible(true);
  };

  const closeModal = () => {
    setIsVisible(false);
  };

  const onSubmit = values => {
    const { novaData } = values;
    const parsedDate = novaData.toISOString();

    const urlObject = {
      empresaId: id,
      novaData: parsedDate,
    };

    const url = new URLSearchParams(urlObject).toString();

    mutate(url, {
      onSuccess: () => {
        /* Único jeito que encontrei para atualizar a data de fundação da empresa.
         * Por algum motivo os refetches não estão funcionando normalmente.
         */
        dispatch(setCompanyFoundationDate(parsedDate));

        closeModal();
      },
    });
  };

  if (!clickedDownLoadButton) {
    return null;
  }

  return (
    <>
      <Tooltip title="Atualizar data de fundação da empresa" placement="left">
        <UpdateButton onClick={openModal}>
          <FaPencilAlt size={18} color="#fff" />
        </UpdateButton>
      </Tooltip>
      <Modal
        title="Atualização da data de fundação"
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
          name="update-foundation-date-form"
          form={form}
          initialValues={{
            dataAtual: dateFounded ? parseISO(dateFounded) : '',
            novaData: '',
          }}
          onFinish={onSubmit}
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
    </>
  );
};

export default UpdateFoundationDate;
