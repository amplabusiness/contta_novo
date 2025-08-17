import { notification } from 'antd';
import { useMutation } from 'react-query';
import { useDispatch, useSelector } from 'react-redux';

import { postUploadProductsSheet } from '@/services/api/requests';
import { setCompanyHasStock } from '@/store/slices/activeCompany';

const useUploadProductsSheet = () => {
  const { id } = useSelector(state => state.activeCompanyState);
  const dispatch = useDispatch();

  const mutation = useMutation(values => postUploadProductsSheet(id, values), {
    onSuccess: () => dispatch(setCompanyHasStock('success')),
    onError: () => {
      notification.error({
        message: 'Erro',
        description: 'Operação não disponível no momento.',
      });
    },
  });

  return mutation;
};

export default useUploadProductsSheet;
