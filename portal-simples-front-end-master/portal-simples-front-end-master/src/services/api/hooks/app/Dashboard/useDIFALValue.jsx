import { useQuery } from 'react-query';
import { useSelector } from 'react-redux';

import { getDIFALValue } from '@/services/api/requests';

const useDIFALValue = () => {
  const { id } = useSelector(state => state.activeCompanyState);
  const { date } = useSelector(state => state.referenceDateState);

  const query = useQuery(
    ['valorDifal', id, date],
    () => getDIFALValue(id, date),
    {
      refetchInterval: false,
      refetchOnWindowFocus: false,
    },
  );

  return query;
};

export default useDIFALValue;
