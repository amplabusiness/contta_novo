import { useQuery } from 'react-query';
import { useSelector } from 'react-redux';

import { getAllTransportNfs } from '@/services/api/requests';

const useTransportInvoices = operacao => {
  const { id } = useSelector(state => state.activeCompanyState);
  const { date } = useSelector(state => state.referenceDateState);

  const query = useQuery(
    ['notasFiscaisTransporte', id, operacao, date],
    () => getAllTransportNfs(id, operacao, date),
    {
      staleTime: 60000,
      refetchInterval: false,
      refetchOnWindowFocus: false,
    },
  );

  return query;
};

export default useTransportInvoices;
