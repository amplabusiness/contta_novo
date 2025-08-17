import { useQuery } from 'react-query';
import { useSelector } from 'react-redux';

import { getRevenueSegregation } from '@/services/api/requests';

const useRevenueSegregation = () => {
  const { id, companyType } = useSelector(state => state.activeCompanyState);
  const { date } = useSelector(state => state.referenceDateState);

  const query = useQuery(
    ['segregacaoReceita', id, date],
    () => getRevenueSegregation(id, date),
    {
      enabled: companyType === 'comum',
      refetchInterval: false,
      refetchOnWindowFocus: false,
      select: data => {
        const {
          detalhamentoIcmsSt,
          detalhamentoPisConfins,
          detalhamentoNfeGeral,
        } = data;

        const initialRevenueSegregationData = {
          icmsSt: [{ id: 'icmsSt', ...detalhamentoIcmsSt }],
          pisCofins: [{ id: 'pisCofins', ...detalhamentoPisConfins }],
          invoices: [{ id: 'invoices', ...detalhamentoNfeGeral }],
        };

        return initialRevenueSegregationData;
      },
    },
  );

  return query;
};

export default useRevenueSegregation;
