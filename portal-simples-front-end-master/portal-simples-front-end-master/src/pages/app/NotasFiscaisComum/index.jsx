import queryString from 'query-string';
import { useLocation } from 'react-router-dom';

import NotasFiscaisComumContextProvider, {
  useNotasFiscaisComumContext,
} from '@/contexts/NotasFiscaisComumContext';
import useInvoices from '@/services/api/hooks/app/NotasFiscaisComum/useInvoices';

import View from '@/pages/app/NotasFiscaisComum/View';
import Shimmer from '@/components/Shimmer/NotasFiscais';
import ErrorMessage from '@/components/ErrorMessage';

const NotasFiscais = () => {
  const { search, state: nfeIdList } = useLocation();
  const { operacao } = queryString.parse(search);

  const {
    state: { currentPage },
  } = useNotasFiscaisComumContext();

  const { isLoading, isError, isFetching, data } = useInvoices(
    operacao,
    currentPage,
    nfeIdList,
  );

  if (isLoading) {
    return <Shimmer />;
  }

  if (isError) {
    return <ErrorMessage />;
  }

  const invoices = data?.itens
    ? data?.itens
        .map(item => {
          const info = { ...item };
          info.cfop = info.items[0]?.cfop || '-';

          return info;
        })
        .sort((a, b) => a.documentNumber - b.documentNumber)
    : [];

  return <View isFetching={isFetching} invoices={invoices} />;
};

const WrappedNotasFiscais = () => (
  <NotasFiscaisComumContextProvider>
    <NotasFiscais />
  </NotasFiscaisComumContextProvider>
);

export default WrappedNotasFiscais;
