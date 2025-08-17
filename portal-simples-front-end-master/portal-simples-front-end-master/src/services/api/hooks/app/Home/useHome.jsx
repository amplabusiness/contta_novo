import { useQuery } from 'react-query';
import { useSelector } from 'react-redux';

import { getUserToken } from '@/utils/userToken';
import { getHomePageCompanyInfo } from '@/services/api/requests';

const useHome = () => {
  const { date } = useSelector(state => state.referenceDateState);

  const token = getUserToken();

  const query = useQuery(
    ['dadosHome', date],
    () => getHomePageCompanyInfo(token, date),
    {
      staleTime: 1000000,
      refetchInterval: false,
      refetchOnWindowFocus: false,
    },
  );

  return query;
};

export default useHome;
