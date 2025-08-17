import PropTypes from 'prop-types';
import { Popconfirm, Progress } from 'antd';
import { FiX } from 'react-icons/fi';

import { formatToBrazilianNumber } from '@/utils/formatters';

import { Container, Main, FileName, DeleteFile } from './styles';

const FileInfo = ({ file, deleteOneFile }) => {
  return (
    <Container>
      <Main>
        <FileName>{file.path}</FileName>
        <p>{formatToBrazilianNumber(file.size / 1000)} KB</p>
        <Popconfirm
          title="Tem certeza em excluir esse arquivo?"
          onConfirm={() => deleteOneFile(file.path)}
          onCancel={() => {}}
          okText="Sim"
        >
          <DeleteFile>
            <FiX size={16} color="#fff" />
          </DeleteFile>
        </Popconfirm>
      </Main>
      <Progress percent={100} />
    </Container>
  );
};

FileInfo.propTypes = {
  file: PropTypes.object.isRequired,
  deleteOneFile: PropTypes.func.isRequired,
};

export default FileInfo;
