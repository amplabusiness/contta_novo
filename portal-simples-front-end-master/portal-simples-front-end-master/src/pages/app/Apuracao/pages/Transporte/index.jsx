import { Tabs } from 'antd';

import OverviewTab from '@/pages/app/Apuracao/pages/Transporte/components/OverviewTab';

const ApuracaoTransporte = () => {
  return (
    <Tabs defaultActiveKey="1" type="card">
      <Tabs.TabPane key="1" tab="Visão Geral">
        <OverviewTab />
      </Tabs.TabPane>
    </Tabs>
  );
};

export default ApuracaoTransporte;
