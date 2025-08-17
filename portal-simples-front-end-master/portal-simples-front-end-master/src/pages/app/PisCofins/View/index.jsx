import { Tabs } from 'antd';

import { usePisCofinsContext } from '@/contexts/PisCofinsContext';

import Confirmation from '@/pages/app/PisCofins/View/components/Confirmation';
import Alteration from '@/pages/app/PisCofins/View/components/Alteration';

import { Container } from '@/styles/global';

const PisCofinsView = () => {
  const { resetState } = usePisCofinsContext();

  return (
    <Container>
      <Tabs
        defaultActiveKey="1"
        type="card"
        destroyInactiveTabPane
        onChange={() => {
          resetState();
        }}
      >
        <Tabs.TabPane tab="Confirmação" key="1">
          <Confirmation />
        </Tabs.TabPane>
        <Tabs.TabPane tab="Alteração" key="2">
          <Alteration />
        </Tabs.TabPane>
      </Tabs>
    </Container>
  );
};

export default PisCofinsView;
