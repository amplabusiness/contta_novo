import { useEffect, useState } from 'react';
import PropTypes from 'prop-types';
import { Button, Row, Tabs, Table } from 'antd';
import { IoAddCircle } from 'react-icons/io5';
import { TiDelete } from 'react-icons/ti';

import {
  blocoE100Columns,
  blocoE110Columns,
  blocoE111Columns,
  blocoE113Columns,
  blocoE115Columns,
  blocoE116Columns,
} from '@/pages/app/BlocoE/constants';

import AdjustmentListModal from '@/pages/app/BlocoE/View/components/AdjustmentListModal';
import ConfirmationModal from '@/pages/app/BlocoE/View/components/ConfirmationModal';

import { Container, Title } from '@/styles/global';

const { TabPane } = Tabs;

const BlocoEView = ({ data }) => {
  const [records, setRecords] = useState({});
  const [isAdjustmentListModalOpen, setIsAdjustmentListModalOpen] =
    useState(false);
  const [isConfirmationModalOpen, setIsConfirmationModalOpen] = useState(false);

  useEffect(() => {
    const {
      registroE100 = null,
      registroE110 = null,
      registroE111 = null,
      registroE113 = null,
      registroE115 = null,
      registroE116 = null,
    } = data;

    const registroE100Data = registroE100 ? [registroE100] : [];
    const registroE110Data = registroE110 ? [registroE110] : [];
    const registroE113Data = registroE113 ? [registroE113] : [];
    const registroE115Data = registroE115 ? [registroE115] : [];
    const registroE116Data = registroE116 ? [registroE116] : [];

    // E111 pode ser retornado como um array ou um objeto
    let registroE111Data = [];

    if (registroE111) {
      if (Array.isArray(registroE111)) {
        registroE111Data = registroE111;
      } else {
        registroE111Data = [registroE111];
      }
    }

    const updatedRecords = {
      e100: registroE100Data,
      e110: registroE110Data,
      e111: registroE111Data,
      e113: registroE113Data,
      e115: registroE115Data,
      e116: registroE116Data,
    };

    setRecords(updatedRecords);
  }, [data]);

  const openAdjustmentListModal = () => setIsAdjustmentListModalOpen(true);

  const openConfirmationModal = () => setIsConfirmationModalOpen(true);

  const closeAdjustmentListModal = () => setIsAdjustmentListModalOpen(false);

  const closeConfirmationModal = () => setIsConfirmationModalOpen(false);

  const onConfirmAdjustmentListModal = selectedAdjustments => {
    setRecords(prevState => ({
      ...prevState,
      e111: [...prevState.e111, ...selectedAdjustments],
    }));
  };

  const mainBlocoE111Columns = [
    ...blocoE111Columns,
    {
      title: 'Ação',
      dataIndex: 'action',
      key: 'action',
      render: (text, record) => (
        <Button
          type="link"
          onClick={() => {
            const filteredAdjustments = records.e111.filter(
              item => item.id !== record.id,
            );

            setRecords(prevState => ({
              ...prevState,
              e111: filteredAdjustments,
            }));
          }}
          style={{ padding: 0, height: '100%' }}
        >
          <TiDelete size={24} color="#ff0033" />
        </Button>
      ),
    },
  ];

  return (
    <Container>
      <Title>
        <h2>Registro - E100</h2>
        <p>
          Abaixo encontram-se as informações referentes ao período e aos valores
          de apuração da empresa.
        </p>
      </Title>

      <Tabs defaultActiveKey="1" type="card" style={{ marginTop: 30 }}>
        <TabPane key="1" tab="Período de Apuração (E100)">
          <Table
            columns={blocoE100Columns}
            dataSource={records.e100}
            pagination={{ pageSize: 5, showSizeChanger: false }}
            size="small"
            rowKey="id"
            scroll={{ x: 'max-content' }}
          />
        </TabPane>
      </Tabs>
      <Tabs defaultActiveKey="1" type="card" style={{ marginTop: 30 }}>
        <TabPane key="1" tab="Valores de Apuração (E110)">
          <Table
            columns={blocoE110Columns}
            dataSource={records.e110}
            pagination={{ pageSize: 5, showSizeChanger: false }}
            size="small"
            rowKey="id"
            scroll={{ x: 'max-content' }}
          />
        </TabPane>
      </Tabs>
      <Tabs defaultActiveKey="1" type="card" style={{ marginTop: 30 }}>
        <TabPane key="1" tab="Ajuste/Benefício/Incentivo (E111)">
          <Table
            columns={mainBlocoE111Columns}
            dataSource={records.e111}
            pagination={{ pageSize: 5, showSizeChanger: false }}
            size="small"
            rowKey="id"
            scroll={{ x: 'max-content' }}
          />
          <Button
            type="primary"
            onClick={openAdjustmentListModal}
            style={{ marginTop: 20 }}
          >
            <IoAddCircle size={18} color="#fff" style={{ marginRight: 6 }} />
            Registro
          </Button>
          <AdjustmentListModal
            open={isAdjustmentListModalOpen}
            onOk={onConfirmAdjustmentListModal}
            onCancel={closeAdjustmentListModal}
          />
        </TabPane>
        <TabPane
          key="2"
          tab="Informações Adicionais - Valores Declaratórios (E115)"
        >
          <Table
            columns={blocoE115Columns}
            dataSource={records.e115}
            pagination={{ pageSize: 5, showSizeChanger: false }}
            size="small"
            rowKey="id"
            scroll={{ x: 'max-content' }}
          />
        </TabPane>
        <TabPane
          key="3"
          tab="Obrigações do ICMS recolhido ou a recolher  - Operações Próprias (E116)"
        >
          <Table
            columns={blocoE116Columns}
            dataSource={records.e116}
            pagination={{ pageSize: 5, showSizeChanger: false }}
            size="small"
            rowKey="id"
            scroll={{ x: 'max-content' }}
          />
        </TabPane>
      </Tabs>
      <Tabs defaultActiveKey="1" type="card" style={{ marginTop: 30 }}>
        <TabPane key="1" tab="Identificação dos Documentos Fiscais (E113)">
          <Table
            columns={blocoE113Columns}
            dataSource={records.e113}
            pagination={{ pageSize: 5, showSizeChanger: false }}
            size="small"
            rowKey="id"
            scroll={{ x: 'max-content' }}
          />
        </TabPane>
      </Tabs>
      <Row align="middle" justify="center">
        <Button
          type="primary"
          onClick={openConfirmationModal}
          style={{ marginTop: 20 }}
        >
          Confirmar informações
        </Button>
        <ConfirmationModal
          open={isConfirmationModalOpen}
          onOk={closeConfirmationModal}
          onCancel={closeConfirmationModal}
          EBlockData={records}
        />
      </Row>
    </Container>
  );
};

BlocoEView.propTypes = {
  data: PropTypes.object,
};

BlocoEView.defaultProps = {
  data: null,
};

export default BlocoEView;
