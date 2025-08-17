import { Tabs } from 'antd';

import OverviewTab from '@/pages/app/Apuracao/pages/Comum/components/OverviewTab';
import SegregationTab from '@/pages/app/Apuracao/pages/Comum/components/SegregationTab';

const ApuracaoComum = () => {
  return (
    <Tabs defaultActiveKey="1" type="card">
      <Tabs.TabPane key="1" tab="Visão Geral">
        <OverviewTab />
      </Tabs.TabPane>
      <Tabs.TabPane key="2" tab="Segregação da Receita">
        <SegregationTab />
      </Tabs.TabPane>
    </Tabs>
  );
};

export default ApuracaoComum;
