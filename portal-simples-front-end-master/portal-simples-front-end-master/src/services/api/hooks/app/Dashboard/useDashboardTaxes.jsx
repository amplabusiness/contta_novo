import { useQuery } from 'react-query';
import { useSelector } from 'react-redux';

import { getDashboardTaxes } from '@/services/api/requests';

const useDashboardTaxes = () => {
  const { id, companyType } = useSelector(state => state.activeCompanyState);
  const { date } = useSelector(state => state.referenceDateState);
  const { clickedDownLoadButton } = useSelector(
    state => state.configurationsState,
  );
  const { isAdmin } = useSelector(state => state.userState);

  const enabled =
    companyType === 'comum' &&
    (isAdmin ? !!(id && clickedDownLoadButton) : true);

  const query = useQuery(
    ['impostosDashboard', id, date],
    () => getDashboardTaxes(id, date),
    {
      enabled,
      refetchInterval: false,
      refetchOnWindowFocus: false,
    },
  );

  return query;
};

export default useDashboardTaxes;
