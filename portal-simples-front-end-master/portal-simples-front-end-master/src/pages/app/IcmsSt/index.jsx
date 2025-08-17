import { useState } from 'react';
import { useSelector } from 'react-redux';

import useUpdateConfiguration from '@/services/api/hooks/app/shared/useUpdateConfiguration';
import IcmsStContextProvider from '@/contexts/IcmsStContext';

import Tour from '@/components/Tour';

import View from '@/pages/app/IcmsSt/View';

const IcmsSt = () => {
  const { substituicaoTutorial: isIcmsTutorialCompleted } = useSelector(
    state => state.configurationsState,
  );

  const [isTourActive, setIsTourActive] = useState(!isIcmsTutorialCompleted);

  const { mutate } = useUpdateConfiguration('substituicaoTutorial');

  const onTourEnd = () => {
    mutate();

    setIsTourActive(false);
  };

  const tourSteps = [
    {
      content:
        'Nessa tela você conseguirá modificar a natureza de seus produtos com relação a um determinado regime.',
      position: 'center',
    },
    {
      selector: '.ant-tabs-nav-list > .ant-tabs-tab:first-child',
      content:
        'Caso queira confirmar a natureza de seus produtos em relação a um regime, utilize essa aba.',
    },
    {
      selector: '#icms-change-regime',
      content:
        'Utilize esse seletor para escolher um regime. Após a seleção, siga os passos que serão indicados abaixo.',
    },
    {
      selector: '.ant-tabs-nav-list > .ant-tabs-tab:nth-child(2)',
      content:
        'Caso queira desfazer as confirmações feitas na aba anterior, utilize essa aba.',
    },
  ];

  return (
    <>
      <View />
      <Tour isActive={isTourActive} steps={tourSteps} onEnd={onTourEnd} />
    </>
  );
};

const WrappedIcmsSt = () => (
  <IcmsStContextProvider>
    <IcmsSt />
  </IcmsStContextProvider>
);

export default WrappedIcmsSt;
