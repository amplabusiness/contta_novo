import { useRef } from 'react';
import { notification } from 'antd';
import { FaTable } from 'react-icons/fa';

import useNewMultipleCompanies from '@/services/api/hooks/app/Empresas/useNewMultipleCompanies';
import { fileTypes } from '@/pages/app/Empresas/constants';

import { Container, UploadLabel } from './styles';

const MultipleRegister = () => {
  const inputFileRef = useRef(null);

  const mutation = useNewMultipleCompanies();

  const handleUpload = async e => {
    const [file] = e.target.files;

    if (inputFileRef.current) {
      inputFileRef.current.value = '';
    }

    if (file) {
      const fileType = file.name.split('.')[1];
      const isPermitted = fileTypes.includes(fileType);

      if (isPermitted) {
        const formData = new FormData();
        formData.append('file', file);

        mutation.mutate(formData);

        notification.info({
          message: 'Informação',
          description:
            'Lista de empresas enviada com sucesso. Iremos processá-la e em breve as empresas serão cadastradas!',
        });
      } else {
        notification.error({
          message: 'Erro',
          description:
            'Formato inválido. Certifique-se de enviar uma planilha Excel.',
        });
      }
    }
  };

  return (
    <Container>
      <UploadLabel htmlFor="upload" disabled={mutation.isLoading}>
        <input
          type="file"
          name="upload"
          id="upload"
          ref={inputFileRef}
          onChange={handleUpload}
        />
        Enviar lista de CNPJs
        <FaTable />
      </UploadLabel>
    </Container>
  );
};

export default MultipleRegister;
