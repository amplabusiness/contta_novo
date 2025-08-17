import { Button, Result } from 'antd';
import { useHistory, useLocation } from 'react-router-dom';

import useMemberCompanies from '@/services/api/hooks/app/EmpresasSocios/useMemberCompanies';

import Shimmer from '@/components/Shimmer/EmpresasSocio';
import ErrorMessage from '@/components/ErrorMessage';

import View from '@/pages/app/EmpresasSocio/View';

const EmpresasSocio = () => {
  const { goBack } = useHistory();
  const { state } = useLocation();
  const { memberName, memberCpf } = state;

  const { isLoading, isError, data, error } = useMemberCompanies(
    memberName,
    memberCpf,
  );

  if (isLoading) {
    return <Shimmer />;
  }

  if (isError) {
    const { data: errorData } = error.response;

    if (errorData.includes('Não contem empresa vinculada')) {
      return (
        <Result
          status="error"
          title="Nenhuma empresa encontrada"
          subTitle="O sócio selecionado não possui outras empresas vinculadas"
          extra={[
            <Button key="back" type="primary" onClick={goBack}>
              Voltar
            </Button>,
          ]}
        />
      );
    }

    return <ErrorMessage />;
  }

  return <View data={data} />;
};

export default EmpresasSocio;
