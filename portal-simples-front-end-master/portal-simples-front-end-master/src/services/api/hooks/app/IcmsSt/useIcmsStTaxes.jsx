import { useQuery } from 'react-query';
import { useSelector } from 'react-redux';

import { getAllIcmsStTaxes } from '@/services/api/requests';

const useIcmsStTaxes = option => {
  const { id } = useSelector(state => state.activeCompanyState);

  const query = useQuery(
    ['impostosIcmsSt', id, option],
    () => getAllIcmsStTaxes(id, option),
    {
      refetchInterval: false,
      refetchOnWindowFocus: false,
    },
  );

  return query;
};

export default useIcmsStTaxes;
