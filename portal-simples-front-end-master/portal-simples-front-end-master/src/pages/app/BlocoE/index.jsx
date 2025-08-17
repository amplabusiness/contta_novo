import { Result } from 'antd';

import useEBlock from '@/services/api/hooks/app/BlocoE/useEBlock';

import Shimmer from '@/components/Shimmer/BlocoE';
import ErrorMessage from '@/components/ErrorMessage';

import BlocoEView from '@/pages/app/BlocoE/View';

const BlocoE = () => {
  const { isLoading, isError, data } = useEBlock();

  if (isLoading) {
    return <Shimmer />;
  }

  if (isError) {
    return <ErrorMessage />;
  }

  if (!data) {
    return (
      <Result
        title="Nenhum registro encontrado"
        subTitle={
          <p style={{ fontSize: '1rem' }}>
            Não foram encontrados registros para o período selecionado. Por
            favor, tente realizar a busca utilizando uma outra data.
          </p>
        }
      />
    );
  }

  return <BlocoEView data={data} />;
};

export default BlocoE;
