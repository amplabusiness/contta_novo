import { useEffect, useCallback } from 'react';
import PropTypes from 'prop-types';
import { notification } from 'antd';
import { useDropzone } from 'react-dropzone';
import { FiUploadCloud } from 'react-icons/fi';

import { Container } from './styles';

const UploadFiles = ({ files, setFiles }) => {
  const onDrop = useCallback(
    acceptedFiles => {
      if (acceptedFiles.length === 0) {
        return;
      }

      if (acceptedFiles.length > 50) {
        notification.warning({
          message: 'Aviso',
          description:
            'O limite máximo de arquivos é 50. Por favor, tente novamente.',
        });
      } else {
        setFiles([...files, ...acceptedFiles]);
      }
    },
    [files, setFiles],
  );

  const { getRootProps, getInputProps, isDragActive, fileRejections } =
    useDropzone({
      onDrop,
      accept: ['text/xml'],
    });

  useEffect(() => {
    if (fileRejections.length > 0) {
      notification.warning({
        message: 'Aviso',
        description:
          'Um ou mais arquivos que você selecionou não possuem formato XML. Por favor, verifique-os novamente.',
      });
    }
  }, [fileRejections]);

  return (
    <Container {...getRootProps()} isDragActive={isDragActive}>
      <input {...getInputProps()} />
      <FiUploadCloud size={72} color="#3276b1" />
      {isDragActive ? (
        <p>Solte os arquivos aqui</p>
      ) : (
        <p>
          <span>Clique ou arraste</span> seus arquivos e solte aqui
        </p>
      )}
    </Container>
  );
};

UploadFiles.propTypes = {
  files: PropTypes.array.isRequired,
  setFiles: PropTypes.func.isRequired,
};

export default UploadFiles;
