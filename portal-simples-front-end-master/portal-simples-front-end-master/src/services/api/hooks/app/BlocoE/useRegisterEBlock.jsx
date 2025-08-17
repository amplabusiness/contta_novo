import { notification } from 'antd';
import { useMutation } from 'react-query';

import { postRegisterEBlock } from '@/services/api/requests';

const useRegisterEBlock = () => {
  const mutation = useMutation(values => postRegisterEBlock(values), {
    onSuccess: () => {
      notification.success({
        message: 'Sucesso',
        description: 'Informações registradas com sucesso!',
      });
    },
  });

  return mutation;
};

export default useRegisterEBlock;
