import { useQuery } from 'react-query';
import { useSelector } from 'react-redux';

import { useNotasFiscaisComumContext } from '@/contexts/NotasFiscaisComumContext';
import { getAllNfs } from '@/services/api/requests';

const useInvoices = (operation, page, nfeIdList) => {
  const { id } = useSelector(state => state.activeCompanyState);
  const { date } = useSelector(state => state.referenceDateState);

  const { setInitialState } = useNotasFiscaisComumContext();

  const remainQuery =
    nfeIdList || nfeIdList?.length > 0
      ? `apuracao=true${nfeIdList.map(item => `&ListNfe=${item}`).join('')}`
      : 'apuracao=false';

  const query = useQuery(
    ['notasFiscaisComum', id, operation, date, page],
    () => getAllNfs(id, operation, date, page, remainQuery),
    {
      staleTime: 60000,
      keepPreviousData: true,
      refetchInterval: false,
      refetchOnWindowFocus: false,
      onSuccess: data => setInitialState(data),
    },
  );

  return query;
};

export default useInvoices;
