import { useRef } from 'react';
import { Col, notification, Row } from 'antd';
import { FiUpload, FiDownload } from 'react-icons/fi';
import { useSelector } from 'react-redux';

import useUploadProductsSheet from '@/services/api/hooks/app/Produtos/useUploadProductsSheet';
import useDownloadCompanyProductsSheet from '@/services/api/hooks/app/Produtos/useDownloadCompanyProductsSheet';
import { fileTypes } from '@/pages/app/Produtos/constants';

import { Container, Card, CustomButton } from './styles';

const Warning = () => {
  const { id, name } = useSelector(state => state.activeCompanyState);

  const inputFileRef = useRef(null);

  const uploadSheetMutation = useUploadProductsSheet();
  const downloadSheetMutation = useDownloadCompanyProductsSheet();

  const activateUpload = () => {
    if (inputFileRef.current) {
      inputFileRef.current.click();
    }
  };

  const handleFileUpload = e => {
    const [file] = e.target.files;
    inputFileRef.current.value = '';

    if (file) {
      const fileType = file.name.split('.')[1];
      const isPermitted = fileTypes.includes(fileType);

      if (isPermitted) {
        const formData = new FormData();
        const firstName = name.split(' ')[0];

        formData.append('file', file);
        formData.append('nomeCliente', firstName);

        uploadSheetMutation.mutate(formData);
      } else {
        notification.error({
          message: 'Erro',
          description:
            'Formato inválido. Certifique-se de enviar uma planilha Excel.',
        });
      }
    }
  };

  const handleSheetDownload = async () => {
    try {
      await downloadSheetMutation.mutateAsync(id);
    } catch (error) {
      notification.error({
        message: 'Erro',
        description: 'Não foi possível baixar a planilha no momento.',
      });
    }
  };

  return (
    <Container>
      <Card>
        <p>
          Verificamos que a tabela referente ao estoque{' '}
          <strong>ainda não foi enviada</strong>. Você pode enviar uma tabela
          utilizando nosso modelo. Clique em <strong>Baixar Modelo</strong> para
          baixá-lo
        </p>
        <Row gutter={[24, 20]}>
          <Col
            xs={24}
            md={12}
            style={{
              marginTop: 10,
              display: 'flex',
              justifyContent: 'center',
            }}
          >
            <CustomButton
              color="#3276b1"
              onClick={activateUpload}
              loading={uploadSheetMutation.isLoading}
              disabled={downloadSheetMutation.isLoading}
            >
              <input
                type="file"
                ref={inputFileRef}
                onChange={handleFileUpload}
              />
              <FiUpload size={14} />
              Exportar
            </CustomButton>
          </Col>
          <Col
            xs={24}
            md={12}
            style={{
              marginTop: 10,
              display: 'flex',
              justifyContent: 'center',
            }}
          >
            <CustomButton
              color="#43aa8b"
              onClick={handleSheetDownload}
              loading={downloadSheetMutation.isLoading}
              disabled={uploadSheetMutation.isLoading}
            >
              <FiDownload size={14} />
              Baixar Modelo
            </CustomButton>
          </Col>
        </Row>
      </Card>
    </Container>
  );
};

export default Warning;
