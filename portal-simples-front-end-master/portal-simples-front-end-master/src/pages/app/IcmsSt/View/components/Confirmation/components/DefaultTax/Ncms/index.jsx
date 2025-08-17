import { useState, useEffect } from 'react';
import { Button, Divider, Input, message, Modal, Table } from 'antd';

import { useIcmsStContext } from '@/contexts/IcmsStContext';
import useConfirmAllRegimeProducts from '@/services/api/hooks/app/shared/useConfirmAllRegimeProducts';
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

  const { mutateAsync, isLoading } = useConfirmAllRegimeProducts(currentTax);

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

  const confirmAllProducts = async () => {
    const key = 'confirm_message_key';

    try {
      message.loading({
        content: `Confirmando os produtos... o processo pode levar de 10 a 15min.`,
        duration: 0,
        key,
      });

      const data = products.map(product => {
        const { id, codProduto } = product;

        return {
          produtoId: id,
          codProduto,
          ncmMono: false,
          icmsSt: false,
          isento: false,
          imune: false,
          beneficios: false,
          isencaoReducao: false,
          [currentTax]: true,
          modificado: true,
        };
      });

      await mutateAsync(data);

      message.success({
        content: `Produtos confirmados com sucesso!`,
        duration: 2.5,
        key,
      });

      resetState();
    } catch (error) {
      message.error({
        content: `Não foi possível confirmar os produtos no momento`,
        duration: 2.5,
        key,
      });
    }
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
        <p>
          Confirme todos os produtos como {defaultTaxesTexts[currentTax]} ou
          filtre-os pelo NCM.
        </p>
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
            title="Envio de todos os produtos"
            footer={[
              <Button key="cancel" onClick={closeModal} disabled={isLoading}>
                Cancelar
              </Button>,
              <Button
                key="confirmar"
                type="primary"
                onClick={confirmAllProducts}
                disabled={isLoading}
              >
                Confirmar envio
              </Button>,
            ]}
            onCancel={closeModal}
          >
            <ModalContent>
              <h2>Aviso</h2>
              <p>
                Você selecionou a opção de confirmar que todos os produtos
                encontrados pelo sistema presentes nessa lista são{' '}
                {defaultTaxesTexts[currentTax]}. Caso queira confirmar a
                natureza de produtos específicos, feche esse aviso e selecione
                um NCM. Caso queria continuar, clique no botão{' '}
                <strong>Confirmar envio</strong>.
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
