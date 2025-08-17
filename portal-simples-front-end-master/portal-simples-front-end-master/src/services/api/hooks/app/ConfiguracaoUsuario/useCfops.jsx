import { useQuery } from 'react-query';

import { useConfiguracaoUsuarioContext } from '@/contexts/ConfiguracaoUsuarioContext';
import { getCfops } from '@/services/api/requests';

const useCfops = () => {
  const { setInitialCfopsData } = useConfiguracaoUsuarioContext();

  const query = useQuery('listaCfops', getCfops, {
    refetchInterval: false,
    refetchOnWindowFocus: false,
    onSuccess: data => setInitialCfopsData(data),
  });

  return query;
};

export default useCfops;
