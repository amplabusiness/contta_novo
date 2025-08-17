import { useState, useEffect } from 'react';
import { Button, Divider, Input, message, Modal, Table } from 'antd';

import { useIcmsStContext } from '@/contexts/IcmsStContext';
import useRevertIcmsProducts from '@/services/api/hooks/app/IcmsSt/useRevertIcmsProducts';
import {
  icmsStNcmsColumns,
  defaultTaxesTexts,
} from '@/pages/app/IcmsSt/constants';

import { Title } from '@/styles/global';
import { ModalContent } from './styles';

const Ncms = () => {
  const [ncm, setNcm] = useState('');
  const [results, setResults] = useState([]);
  const [isModalVisible, setIsModalVisible] = useState(false);

  const {
    state: { currentTax, ncms, activeNcm, products },
    filterProducts,
    resetState,
  } = useIcmsStContext();

  const { mutate, isLoading } = useRevertIcmsProducts(`${currentTax}Edit`);

  useEffect(() => {
    setNcm(activeNcm);
  }, [activeNcm]);

  useEffect(() => {
    setResults(ncms);
  }, [ncms]);

  const openModal = () => {
    setIsModalVisible(true);
  };

  const closeModal = () => {
    setIsModalVisible(false);
  };

  const handleNcmSelection = selectedNcm => {
    setNcm(selectedNcm);
    filterProducts(products, selectedNcm);
  };

  const handleSearch = e => {
    const data = e.target.value;
    setNcm(data);

    if (!data) {
      setResults(ncms);
      return;
    }

    setResults(ncms.filter(item => item.ncm.startsWith(data)));
  };

  const editAllProducts = () => {
    const key = 'edit_message_key';

    message.loading({
      content: `Revertendo os produtos...`,
      duration: 0,
      key,
    });

    const reversedProducts = products.map(item => ({
      produtoId: item.id,
      ncmMono: item.ncmMono,
      [currentTax]: false,
      modificado: false,
      empresaId: item.companyInformation,
      dataOperacao: new Date().toISOString(),
    }));

    mutate(reversedProducts, {
      onSuccess: () => {
        message.success({
          content: `Produtos revertidos com sucesso!`,
          duration: 2.5,
          key,
        });

        resetState();
      },
      onError: () => {
        message.error({
          content: `Não foi possível reverter os produtos no momento`,
          duration: 2.5,
          key,
        });
      },
    });
  };

  const mainColumns = [
    ...icmsStNcmsColumns,
    {
      title: '',
      dataIndex: 'action',
      align: 'center',
      render: (text, record) => (
        <Button onClick={() => handleNcmSelection(record.ncm)} size="small">
          Definir
        </Button>
      ),
    },
  ];

  return (
    <>
      <Title>
        <h2>Lista de NCMs</h2>
        <p>Reverta todos os produtos ou filtre-os pelo NCM.</p>
      </Title>

      {products.length > 0 && (
        <>
          <Button
            type="primary"
            onClick={openModal}
            style={{ marginTop: 20, width: '100%' }}
          >
            Confirmar todos os produtos
          </Button>
          <Modal
            visible={isModalVisible}
            title="Reversão de todos os produtos"
            footer={[
              <Button key="cancel" onClick={closeModal} disabled={isLoading}>
                Cancelar
              </Button>,
              <Button
                key="confirmar"
                type="primary"
                onClick={editAllProducts}
                disabled={isLoading}
              >
                Confirmar reversão
              </Button>,
            ]}
            onCancel={closeModal}
          >
            <ModalContent>
              <h2>Aviso</h2>
              <p>
                Você selecionou a opção de reverter todos os produtos
                encontrados para seu estado original, ou seja, não sendo
                {defaultTaxesTexts[currentTax]}. Caso queira editar produtos
                específicos, feche esse aviso e selecione um NCM. Caso queria
                continuar, clique no botão <strong>Confirmar reversão</strong>.
              </p>
            </ModalContent>
          </Modal>

          <Divider
            orientation="center"
            style={{ margin: '8px 0', color: '#c4c4c4' }}
          >
            ou
          </Divider>
        </>
      )}

      <Input
        value={ncm}
        onChange={handleSearch}
        placeholder="Código NCM"
        style={{ marginTop: products.length === 0 ? 14 : 0 }}
      />

      <Table
        columns={mainColumns}
        dataSource={results}
        pagination={{
          pageSize: 5,
        }}
        locale={{ emptyText: 'Nenhum resultado' }}
        size="small"
        style={{ marginTop: 20 }}
      />
    </>
  );
};

export default Ncms;
