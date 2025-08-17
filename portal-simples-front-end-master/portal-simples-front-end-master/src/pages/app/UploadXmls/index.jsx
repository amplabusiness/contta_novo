import { useState } from 'react';
import { Col, Row } from 'antd';

import useUploadXml from '@/services/api/hooks/app/UploadXmls/useUploadXml';

import UploadFiles from '@/pages/app/UploadXmls/components/UploadFiles';
import UploadedFiles from '@/pages/app/UploadXmls/components/UploadedFiles';

import { Container, Title } from '@/styles/global';
import { SubmitButton } from './styles';

const UploadXmls = () => {
  const [files, setFiles] = useState([]);

  const { mutate, isLoading } = useUploadXml();

  const uploadFiles = () => {
    const formData = new FormData();

    for (let i = 0; i < files.length; i += 1) {
      formData.append('files', files[i]);
    }

    mutate(formData, {
      onSuccess: () => {
        setFiles([]);
      },
    });
  };

  return (
    <Container>
      <Title>
        <h2>Upload de XMLs</h2>
        <p>Utilize o campo abaixo para enviar um ou vários arquivos XMLs.</p>
      </Title>
      <Row gutter={[40, 40]} justify="center" style={{ marginTop: 30 }}>
        <Col xs={24} lg={8}>
          <UploadFiles files={files} setFiles={setFiles} />
        </Col>

        <Col xs={24} lg={8}>
          <SubmitButton
            type="primary"
            onClick={uploadFiles}
            disabled={files.length <= 0 || isLoading}
          >
            Enviar arquivos
          </SubmitButton>

          <UploadedFiles files={files} setFiles={setFiles} />
        </Col>
      </Row>
    </Container>
  );
};

export default UploadXmls;
