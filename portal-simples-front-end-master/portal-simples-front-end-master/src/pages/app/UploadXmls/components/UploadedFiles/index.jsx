import PropTypes from 'prop-types';
import { List, Popconfirm } from 'antd';
import { FiTrash2 } from 'react-icons/fi';

import { formatToBrazilianNumber } from '@/utils/formatters';

import FileInfo from '@/pages/app/UploadXmls/components/FileInfo';

import { FooterContainer, DeleteAllItems } from './styles';

const UploadedFiles = ({ files, setFiles }) => {
  const calculateFilesSize = uploadedFiles => {
    return (
      uploadedFiles
        .map(file => file.size)
        .reduce((acc, current) => acc + current) / 1000
    );
  };

  const deleteOneFile = filepath => {
    setFiles(files.filter(file => file.path !== filepath));
  };

  const wereFilesUploaded = files.length > 0;

  return (
    <List
      header={
        <h2 style={{ margin: 0, fontSize: '1rem', fontWeight: 'normal' }}>
          Arquivos Selecionados
        </h2>
      }
      footer={
        wereFilesUploaded && (
          <FooterContainer>
            <p>
              {files.length} arquivo(s) enviado(s) -{' '}
              {formatToBrazilianNumber(calculateFilesSize(files))} KB no total{' '}
            </p>
            <Popconfirm
              title="Tem certeza em excluir todos os arquivos?"
              onConfirm={() => setFiles([])}
              onCancel={() => {}}
              okText="Sim"
            >
              <DeleteAllItems>
                <FiTrash2 size={16} color="#fff" />
              </DeleteAllItems>
            </Popconfirm>
          </FooterContainer>
        )
      }
      bordered
      pagination={{
        pageSize: 4,
        showSizeChanger: false,
        size: 'small',
        hideOnSinglePage: true,
      }}
      locale={{
        emptyText: 'Os arquivos selecionados por você aparecerão aqui',
      }}
      dataSource={files}
      renderItem={item => (
        <List.Item>
          <FileInfo file={item} deleteOneFile={deleteOneFile} />
        </List.Item>
      )}
    />
  );
};

UploadedFiles.propTypes = {
  files: PropTypes.array.isRequired,
  setFiles: PropTypes.func.isRequired,
};

export default UploadedFiles;
