import { Tabs } from 'antd';
import { IoList } from 'react-icons/io5';

import useIcmsStTaxes from '@/services/api/hooks/app/IcmsSt/useIcmsStTaxes';

import Shimmer from '@/components/Shimmer/IcmsSt';
import ErrorMessage from '@/components/ErrorMessage';

import Tables from '@/pages/app/IcmsSt/View/components/Alteration/components/ExigSuspensa/Tables';

import { Container, TabTitle } from './styles';

const ExigSuspensa = () => {
  const query = useIcmsStTaxes('exigSuspensaEdit');
  const { isLoading, isError, data } = query;

  if (isLoading) {
    return <Shimmer />;
  }

  if (isError) {
    return <ErrorMessage />;
  }

  const handleSubmit = async values => {
    const submitData = {};

    Object.keys(values).forEach(item => {
      const formattedKey = item.split('-')[0];
      submitData[formattedKey] = values[item];
    });

    alert(JSON.stringify(submitData, null, 2));
  };

  return (
    <Container>
      <Tabs defaultActiveKey="1" style={{ overflow: 'visible' }}>
        <Tabs.TabPane
          key="1"
          tab={
            <TabTitle>
              <IoList /> Impostos Cadastrados
            </TabTitle>
          }
        >
          <Tables data={data} onSubmit={handleSubmit} />
        </Tabs.TabPane>
      </Tabs>
    </Container>
  );
};

export default ExigSuspensa;
