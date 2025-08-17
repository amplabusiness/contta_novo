import { useState } from 'react';
import PropTypes from 'prop-types';
import { Modal, Table } from 'antd';

import useAdjustmentsList from '@/services/api/hooks/app/BlocoE/useAdjustmentsList';
import { blocoE111Columns } from '@/pages/app/BlocoE/constants';

import { Container } from './styles';

const AdjustmentListModal = ({ open, onOk, onCancel }) => {
  const [selectedAdjustments, setSelectedAdjustments] = useState([]);

  const { data, isLoading } = useAdjustmentsList({ enabled: open });

  const closeModal = () => {
    setSelectedAdjustments([]);
    onCancel();
  };

  const onConfirm = () => {
    onOk(selectedAdjustments);

    closeModal();
  };

  const currData = data ?? [];
  const selectedAdjustmentsLength = selectedAdjustments.length;
  const selectedAdjustmentsInfo = `${
    selectedAdjustmentsLength === 0
      ? 'Nenhum ajuste selecionado'
      : selectedAdjustmentsLength === 1
      ? `${selectedAdjustmentsLength} ajuste selecionado`
      : `${selectedAdjustmentsLength} ajustes selecionados`
  }`;

  return (
    <Modal
      visible={open}
      title="Lista de Ajustes"
      onOk={onConfirm}
      onCancel={closeModal}
      width={1000}
      okText="Confirmar"
      destroyOnClose
    >
      <Container>
        {isLoading ? (
          <h3>Carregando...</h3>
        ) : (
          <>
            <h3>
              Utilize as <strong>checkboxes</strong> para selecionar os ajustes
              que deseja cadastrar.
            </h3>
            <Table
              columns={blocoE111Columns}
              dataSource={currData}
              size="small"
              pagination={{ pageSize: 5, showSizeChanger: false }}
              rowKey="id"
              scroll={{ x: 'max-content' }}
              rowSelection={{
                type: 'checkbox',
                onChange: (selectedRowKeys, selectedRows) => {
                  setSelectedAdjustments(selectedRows);
                },
              }}
              style={{ margin: '20px 0 10px' }}
            />
            <h3>{selectedAdjustmentsInfo}</h3>
          </>
        )}
      </Container>
    </Modal>
  );
};

AdjustmentListModal.propTypes = {
  open: PropTypes.bool.isRequired,
  onOk: PropTypes.func.isRequired,
  onCancel: PropTypes.func.isRequired,
};

export default AdjustmentListModal;
