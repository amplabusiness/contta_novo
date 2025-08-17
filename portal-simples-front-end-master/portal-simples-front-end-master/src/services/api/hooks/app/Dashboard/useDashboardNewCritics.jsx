import { useQuery } from 'react-query';
import { useSelector } from 'react-redux';

import { getNewCritics } from '@/services/api/requests';

const useDashboardNewCritics = () => {
  const { id, companyType } = useSelector(state => state.activeCompanyState);
  const { date } = useSelector(state => state.referenceDateState);
  const { clickedDownLoadButton } = useSelector(
    state => state.configurationsState,
  );
  const { isAdmin } = useSelector(state => state.userState);

  const enabled =
    companyType === 'comum' &&
    (isAdmin ? !!(id && clickedDownLoadButton) : true);

  const query = useQuery(['novasCriticas', id, date], () => getNewCritics(id), {
    enabled,
    refetchInterval: false,
    refetchOnWindowFocus: false,
  });

  return query;
};

export default useDashboardNewCritics;
