import { notification } from 'antd';
import { useMutation } from 'react-query';

import { postUploadXmlFiles } from '@/services/api/requests';

const useUploadXml = () => {
  const mutation = useMutation(values => postUploadXmlFiles(values), {
    onSuccess: () => {
      notification.success({
        message: 'Sucesso',
        description: 'XMLs enviados com sucesso!',
      });
    },
    onError: () => {
      notification.error({
        message: 'Erro',
        description: 'Operação não disponível no momento',
      });
    },
  });

  return mutation;
};

export default useUploadXml;
