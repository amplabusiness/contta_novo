import { useQuery } from 'react-query';
import { useSelector } from 'react-redux';

import { useIcmsStContext } from '@/contexts/IcmsStContext';
import { getAllProductsByOption } from '@/services/api/requests';

const useIcmsSt = option => {
  const { date } = useSelector(state => state.referenceDateState);
  const { id } = useSelector(state => state.activeCompanyState);

  const { setInitialState } = useIcmsStContext();

  const query = useQuery(
    ['produtosIcms', id, option, date],
    () => getAllProductsByOption(id, option, date),
    {
      refetchInterval: false,
      refetchOnWindowFocus: false,
      onSuccess: data => {
        setInitialState(data);
      },
    },
  );

  return query;
};

export default useIcmsSt;
