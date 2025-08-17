import { useState } from 'react';
import { useSelector } from 'react-redux';

import useUpdateConfiguration from '@/services/api/hooks/app/shared/useUpdateConfiguration';
import PisCofinsContextProvider from '@/contexts/PisCofinsContext';

import Tour from '@/components/Tour';

import View from '@/pages/app/PisCofins/View';

const PisCofins = () => {
  const { pisConfinsTutorial: isPisCofinsTutorialCompleted } = useSelector(
    state => state.configurationsState,
  );

  const [isTourActive, setIsTourActive] = useState(
    !isPisCofinsTutorialCompleted,
  );

  const { mutate } = useUpdateConfiguration('pisConfinsTutorial');

  const onTourEnd = () => {
    mutate();

    setIsTourActive(false);
  };

  const tourSteps = [
    {
      content:
        'Nessa tela você conseguirá modificar a natureza de seus produtos com relação ao PIS/Cofins, isto é, se são monofásicos ou não.',
      position: 'center',
    },
    {
      selector: '.ant-tabs-nav-list > .ant-tabs-tab:first-child',
      content:
        'Caso queira confirmar que seus produtos são monofásicos, utilize essa aba.',
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

const WrappedPisCofins = () => (
  <PisCofinsContextProvider>
    <PisCofins />
  </PisCofinsContextProvider>
);

export default WrappedPisCofins;
