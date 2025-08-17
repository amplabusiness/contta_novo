import { useQuery } from 'react-query';
import { useSelector, useDispatch } from 'react-redux';

import {
  getCompanyDashboardValues,
  getCompanyDashboardAnnualIncome,
} from '@/services/api/requests';
import { setInitialStateSE } from '@/store/slices/activeCompany';

const useCompanyDashboard = () => {
  const { id } = useSelector(state => state.activeCompanyState);
  const { date } = useSelector(state => state.referenceDateState);
  const { clickedDownLoadButton } = useSelector(
    state => state.configurationsState,
  );
  const { isAdmin } = useSelector(state => state.userState);
  const dispatch = useDispatch();

  const companyValuesQuery = useQuery(
    ['dadosEmpresaAtiva', id, date],
    () => getCompanyDashboardValues(id, date),
    {
      enabled: isAdmin ? !!(id && clickedDownLoadButton) : true,
      refetchInterval: false,
      refetchOnWindowFocus: false,
      refetchOnMount: true,
    },
  );

  const companyValuesData = companyValuesQuery.data;

  const companyIncomeQuery = useQuery(
    ['faturamentoEmpresaAtiva', id, date],
    () => getCompanyDashboardAnnualIncome(id, date),
    {
      enabled: !!companyValuesData,
      refetchInterval: false,
      refetchOnWindowFocus: false,
      refetchOnMount: true,
      onSuccess: data => {
        const companyData = {
          ...companyValuesData,
          faturamentoAnual: {
            ...data,
          },
        };

        dispatch(setInitialStateSE(companyData));
      },
    },
  );

  return { companyValuesQuery, companyIncomeQuery };
};

export default useCompanyDashboard;
