import { useQuery } from 'react-query';
import { useSelector } from 'react-redux';

import { getAllServiceInvoices } from '@/services/api/requests';

const useManualServiceInvoices = () => {
  const { id } = useSelector(state => state.activeCompanyState);

  const query = useQuery(
    ['notasServico', id],
    () => getAllServiceInvoices(id),
    {
      refetchInterval: false,
      refetchOnWindowFocus: false,
    },
  );

  return query;
};

export default useManualServiceInvoices;
