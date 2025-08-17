import { useSelector } from 'react-redux';

import ApuracaoComum from '@/pages/app/Apuracao/pages/Comum';
import ApuracaoServico from '@/pages/app/Apuracao/pages/Servico';
import ApuracaoTransporte from '@/pages/app/Apuracao/pages/Transporte';

import { Container } from '@/styles/global';

const Apuracao = () => {
  const { companyType } = useSelector(state => state.activeCompanyState);

  const apuracaoPages = {
    comum: <ApuracaoComum />,
    servico: <ApuracaoServico />,
    transporte: <ApuracaoTransporte />,
  };

  return <Container>{apuracaoPages[companyType]}</Container>;
};

export default Apuracao;
