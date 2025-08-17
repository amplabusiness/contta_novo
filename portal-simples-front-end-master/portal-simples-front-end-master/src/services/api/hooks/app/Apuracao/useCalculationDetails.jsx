import { useQuery } from 'react-query';
import { useSelector } from 'react-redux';

import { getCalculationDetails } from '@/services/api/requests';

const useCalculationDetails = () => {
  const { companyType } = useSelector(state => state.activeCompanyState);

  const query = useQuery('detalhesApuracao', () => getCalculationDetails(), {
    enabled: companyType === 'comum',
    refetchInterval: false,
    refetchOnWindowFocus: false,
  });

  return query;
};

export default useCalculationDetails;
