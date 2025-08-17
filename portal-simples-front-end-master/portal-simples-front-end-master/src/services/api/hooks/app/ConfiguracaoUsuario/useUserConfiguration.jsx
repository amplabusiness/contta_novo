import { useQuery } from 'react-query';
import { useSelector } from 'react-redux';

import { useConfiguracaoUsuarioContext } from '@/contexts/ConfiguracaoUsuarioContext';
import { getConfiguration } from '@/services/api/requests';

const useUserConfiguration = () => {
  const { id } = useSelector(state => state.activeCompanyState);

  const { setInitialBooksData } = useConfiguracaoUsuarioContext();

  const query = useQuery(
    ['configuracaoUsuario', id],
    () => getConfiguration(id),
    {
      refetchInterval: false,
      refetchOnWindowFocus: false,
      onSuccess: data => setInitialBooksData(data),
    },
  );

  return query;
};

export default useUserConfiguration;
