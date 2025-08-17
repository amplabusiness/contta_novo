import { useQuery } from 'react-query';
import { useSelector } from 'react-redux';

import { getDashboardInvoices } from '@/services/api/requests';

const useInvoicesQuantity = ({ enabled }) => {
  const { id } = useSelector(state => state.activeCompanyState);
  const { date } = useSelector(state => state.referenceDateState);

  const query = useQuery(
    ['dashboardInvoicesQuantity', id, date],
    () => getDashboardInvoices(id, date),
    {
      enabled,
      refetchInterval: false,
      refetchOnWindowFocus: false,
      select: data => {
        return [
          {
            id: data.notaInicial,
            numInicial: data.notaInicial,
            numFinal: data.notaFinal,
            notasCanceladas: data.notaCanceladas,
            notasInutilizadas: data.notasInutilizada,
            notasFaltantes: data.notasFaltante,
            total: data.totalNotas,
            numCanceladas: data.listaNfCancel,
            numSaidas: data.listaNfSaida,
          },
        ];
      },
    },
  );

  return query;
};

export default useInvoicesQuantity;
