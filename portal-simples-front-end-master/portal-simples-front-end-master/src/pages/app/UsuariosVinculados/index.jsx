import useLinkedUsers from '@/services/api/hooks/app/UsuariosVinculados/useLinkedUsers';

import Shimmer from '@/components/Shimmer/UsuariosVinculados';
import ErrorMessage from '@/components/ErrorMessage';

import View from '@/pages/app/UsuariosVinculados/View';

const UsuariosVinculados = () => {
  const { isLoading, isError, data } = useLinkedUsers();

  if (isLoading) {
    return <Shimmer />;
  }

  if (isError) {
    return <ErrorMessage />;
  }

  return <View data={data} />;
};

export default UsuariosVinculados;
