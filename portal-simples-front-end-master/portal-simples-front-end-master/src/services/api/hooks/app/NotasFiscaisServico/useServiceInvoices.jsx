import { useQuery } from 'react-query';
import { useSelector } from 'react-redux';

import { getAllServiceNfs } from '@/services/api/requests';

const useServiceInvoices = operation => {
  const { id } = useSelector(state => state.activeCompanyState);
  const { date } = useSelector(state => state.referenceDateState);

  const query = useQuery(
    ['notasFiscaisServico', id, operation, date],
    () => getAllServiceNfs(id, operation, date),
    {
      staleTime: 60000,
      refetchInterval: false,
      refetchOnWindowFocus: false,
    },
  );

  return query;
};

export default useServiceInvoices;
