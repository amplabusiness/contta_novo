import { useQuery } from 'react-query';
import { useSelector } from 'react-redux';

import { getAllSellInvoices } from '@/services/api/requests';

const useManualSellInvoices = () => {
  const { id } = useSelector(state => state.activeCompanyState);

  const query = useQuery(['notasVenda', id], () => getAllSellInvoices(id), {
    refetchInterval: false,
    refetchOnWindowFocus: false,
  });

  return query;
};

export default useManualSellInvoices;
