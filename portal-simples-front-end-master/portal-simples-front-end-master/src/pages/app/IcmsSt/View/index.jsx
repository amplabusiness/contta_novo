import { Tabs } from 'antd';

import { useIcmsStContext } from '@/contexts/IcmsStContext';

import Confirmation from '@/pages/app/IcmsSt/View/components/Confirmation';
import Alteration from '@/pages/app/IcmsSt/View/components/Alteration';

import { Container } from '@/styles/global';

const IcmsStView = () => {
  const { resetState } = useIcmsStContext();

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

export default IcmsStView;
