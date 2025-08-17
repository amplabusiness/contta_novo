import useUserConfiguration from '@/services/api/hooks/app/ConfiguracaoUsuario/useUserConfiguration';
import useCfops from '@/services/api/hooks/app/ConfiguracaoUsuario/useCfops';

import ConfiguracaoUsuarioContextProvider from '@/contexts/ConfiguracaoUsuarioContext';

import View from '@/pages/app/ConfiguracaoUsuario/View';

import Shimmer from '@/components/Shimmer/ConfiguracaoUsuario';
import ErrorMessage from '@/components/ErrorMessage';

const ConfiguracaoUsuario = () => {
  const userConfigurationsQuery = useUserConfiguration();
  const cfopsQuery = useCfops();

  const isLoading = userConfigurationsQuery.isLoading || cfopsQuery.isLoading;
  const isFetching =
    userConfigurationsQuery.isFetching || cfopsQuery.isFetching;
  const isError = userConfigurationsQuery.isError || cfopsQuery.isError;

  if (isLoading || isFetching) {
    return <Shimmer />;
  }

  if (isError) {
    return <ErrorMessage />;
  }

  return <View />;
};

const WrappedConfiguracaoUsuario = () => (
  <ConfiguracaoUsuarioContextProvider>
    <ConfiguracaoUsuario />
  </ConfiguracaoUsuarioContextProvider>
);

export default WrappedConfiguracaoUsuario;
