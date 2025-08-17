import queryString from 'query-string';
import { useLocation } from 'react-router-dom';

import useServiceInvoices from '@/services/api/hooks/app/NotasFiscaisServico/useServiceInvoices';

import Shimmer from '@/components/Shimmer/NotasFiscais';
import ErrorMessage from '@/components/ErrorMessage';

import View from '@/pages/app/NotasFiscaisServico/View';

const NotasFiscaisServicos = () => {
  const { search } = useLocation();
  const { operacao = null } = queryString.parse(search);

  const { isLoading, isError, data } = useServiceInvoices(operacao);

  if (isLoading) {
    return <Shimmer />;
  }

  if (isError) {
    return <ErrorMessage />;
  }

  return <View data={data} />;
};

export default NotasFiscaisServicos;
