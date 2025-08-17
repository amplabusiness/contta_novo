import { useQuery } from 'react-query';
import { notification } from 'antd';
import { useDispatch } from 'react-redux';

import { getAllCompanies } from '@/services/api/requests';
import { setInitialStateSE } from '@/store/slices/companies';
import { getUserToken } from '@/utils/userToken';

const useCompanies = ({ executeOnSuccessCallback }) => {
  const dispatch = useDispatch();

  const userToken = getUserToken();

  const query = useQuery('empresas', () => getAllCompanies(userToken), {
    staleTime: 1000000,
    refetchInterval: false,
    refetchOnWindowFocus: false,
    onSuccess: data => {
      if (executeOnSuccessCallback) {
        dispatch(setInitialStateSE(data));
      }
    },
    onError: () => {
      notification.error({
        message: 'Erro',
        description:
          'Não foi possível trazer as empresas cadastradas no momento.',
      });
    },
  });

  return query;
};

export default useCompanies;
