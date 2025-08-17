import { useQuery } from 'react-query';
import { useSelector } from 'react-redux';

import { getDashboardTaxes } from '@/services/api/requests';

const useSegregations = () => {
  const { id, companyType } = useSelector(state => state.activeCompanyState);
  const { date } = useSelector(state => state.referenceDateState);

  const query = useQuery(
    ['impostosDashboard', id, date],
    () => getDashboardTaxes(id, date),
    {
      enabled: companyType === 'comum',
      refetchInterval: false,
      refetchOnWindowFocus: false,
    },
  );

  return query;
};

export default useSegregations;
