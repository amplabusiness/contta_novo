import { useSelector } from 'react-redux';

import useDashboardCompany from '@/services/api/hooks/app/Dashboard/useDashboardCompany';

import Shimmer from '@/components/Shimmer/Dashboard';
import ErrorMessage from '@/components/ErrorMessage';
import OutFromSimples from '@/pages/app/Dashboard/components/OutFromSimples';
import EmaloteWarning from '@/pages/app/Dashboard/components/EmaloteWarning';
import DashboardComum from '@/pages/app/Dashboard/View/Comum';
import DashboardServico from '@/pages/app/Dashboard/View/Servico';
import DashboardTransporte from '@/pages/app/Dashboard/View/Transporte';

const Dashboard = () => {
  const { companyType, isOutFromSimples } = useSelector(
    state => state.activeCompanyState,
  );
  const { clickedDownLoadButton } = useSelector(
    state => state.configurationsState,
  );

  const { companyValuesQuery, companyIncomeQuery } = useDashboardCompany();

  const isLoading =
    companyValuesQuery.isLoading || companyIncomeQuery.isLoading;

  const isFetching =
    companyValuesQuery.isFetching || companyIncomeQuery.isFetching;

  const isError = companyValuesQuery.isError || companyIncomeQuery.isError;

  if (isLoading || isFetching) {
    return <Shimmer />;
  }

  if (isError) {
    return <ErrorMessage />;
  }

  // Se a empresa estiver fora do Simples, retornar um aviso
  if (isOutFromSimples) {
    return <OutFromSimples />;
  }

  // Se o usuário não tiver baixado o e-Malote, retornar um aviso
  if (!clickedDownLoadButton) {
    return <EmaloteWarning />;
  }

  // Dashboard de Serviço caso a empresa seja de Serviço
  if (companyType === 'servico') {
    return <DashboardServico />;
  }

  // Dashboard de Transporte caso a empresa seja de Transporte
  if (companyType === 'transporte') {
    return <DashboardTransporte />;
  }

  // Caso não seja nenhuma das anteriores, então a empresa é Comum
  return <DashboardComum />;
};

export default Dashboard;
