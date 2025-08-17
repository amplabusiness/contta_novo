import { useState } from 'react';
import PropTypes from 'prop-types';
import { Button, Modal, Tooltip } from 'antd';
import { IoCheckmark, IoInformation } from 'react-icons/io5';

import { Container, ActionButton } from './styles';

const Correction = ({ exists, description = null }) => {
  const [visible, setVisible] = useState(false);

  const openModal = () => setVisible(true);

  const closeModal = () => setVisible(false);

  return (
    <Container>
      {exists ? (
        <>
          <ActionButton exists={exists} onClick={openModal}>
            <IoCheckmark color="#fff" />
          </ActionButton>
          <Modal
            title="Descrição da Carta de Correção"
            visible={visible}
            onOk={closeModal}
            onCancel={closeModal}
            footer={[
              <Button key="ok" type="primary" onClick={closeModal}>
                Ok
              </Button>,
            ]}
            destroyOnClose
          >
            {description}
          </Modal>
        </>
      ) : (
        <Tooltip title="Não informado">
          <ActionButton exists={exists}>
            <IoInformation color="#fff" />
          </ActionButton>
        </Tooltip>
      )}
    </Container>
  );
};

Correction.propTypes = {
  exists: PropTypes.bool.isRequired,
  description: PropTypes.string,
};

Correction.defaultProps = {
  description: null,
};

export default Correction;
