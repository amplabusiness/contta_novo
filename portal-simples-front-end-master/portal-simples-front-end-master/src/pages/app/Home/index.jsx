import { useState } from 'react';
import { useSelector } from 'react-redux';

import useUpdateConfiguration from '@/services/api/hooks/app/shared/useUpdateConfiguration';
import useHome from '@/services/api/hooks/app/Home/useHome';

import ErrorMessage from '@/components/ErrorMessage';
import Shimmer from '@/components/Shimmer/Home';
import Tour from '@/components/Tour';

import View from '@/pages/app/Home/View';

const Home = () => {
  const { dashboardTutorial: isHomeTutorialCompleted } = useSelector(
    state => state.configurationsState,
  );

  const [isTourActive, setIsTourActive] = useState(!isHomeTutorialCompleted);

  const { mutate } = useUpdateConfiguration('dashboardTutorial');

  const { isLoading, isError, data } = useHome();

  const onTourEnd = () => {
    mutate();

    setIsTourActive(false);
  };

  const formattedData = Array.isArray(data)
    ? data.map(item => {
        const { validadeCertificado } = item;

        if (validadeCertificado) {
          const [date, hours] = validadeCertificado.split(' ');
          const correctCertificateValidationDate = `${new Date(
            date,
          ).toLocaleDateString('pt-BR')} ${hours}`;

          return {
            ...item,
            validadeCertificado: correctCertificateValidationDate,
          };
        }
        return item;
      })
    : [];

  const sortedData =
    formattedData.length > 0
      ? data.sort((a, b) => {
          const nameA = a.razaoSocial.toLowerCase();
          const nameB = b.razaoSocial.toLowerCase();

          if (nameA < nameB) {
            return -1;
          }

          if (nameA > nameB) {
            return 1;
          }

          return 0;
        })
      : formattedData;

  const tourSteps = [
    {
      content:
        'Agradecemos por escolher o Contta! Nesse breve passo-a-passo iremos lhe mostrar como cadastrar sua primeira empresa no sistema e mostrar suas informações.',
      position: 'center',
    },
    {
      selector: '#my-companies-button',
      content:
        'Utilize esse botão para gerenciar suas empresas, isto é, cadastrar, visualizar informações básicas ou deletar uma determinada empresa.',
    },
    {
      selector: '#download-emalote-button',
      content:
        'Após cadastrar sua primeira empresa, será necessário realizar o download do e-Malote. Para isto, utilize esse botão.',
      position: 'bottom',
    },
    {
      selector: '#dashboard-link',
      content:
        'Depois da instalação do e-Malote, a Dashboard mostrará todas as informações relacionadas ao Simples Nacional da empresa. Para acessar a Dashboard, utilize esse link.',
      position: 'right',
    },
    {
      content:
        'Pronto, você está preparado para usar o Contta! Mais uma vez agradecemos por nos escolher!',
      position: 'center',
    },
  ];

  if (isLoading) {
    return <Shimmer />;
  }

  if (isError) {
    return <ErrorMessage />;
  }

  return (
    <>
      <Tour isActive={isTourActive} steps={tourSteps} onEnd={onTourEnd} />
      <View data={sortedData} />
    </>
  );
};

export default Home;
