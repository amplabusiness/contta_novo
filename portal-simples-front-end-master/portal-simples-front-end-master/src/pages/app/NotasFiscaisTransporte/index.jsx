import queryString from 'query-string';
import { useLocation } from 'react-router-dom';

import useTransportInvoices from '@/services/api/hooks/app/NotasFiscaisTransporte/useTransportInvoices';

import Shimmer from '@/components/Shimmer/NotasFiscais';
import ErrorMessage from '@/components/ErrorMessage';

import View from '@/pages/app/NotasFiscaisTransporte/View';

const NotasFiscaisTransporte = () => {
  const { search } = useLocation();
  const { operacao = null } = queryString.parse(search);

  const { isLoading, isError, data } = useTransportInvoices(operacao);

  if (isLoading) {
    return <Shimmer />;
  }

  if (isError) {
    return <ErrorMessage />;
  }

  return <View data={data} />;
};

export default NotasFiscaisTransporte;
