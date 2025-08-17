import { PageHeader } from 'antd';
import { useLocation } from 'react-router-dom';

import { getPageTitle } from '@/components/Header/constants';

import Breadcrumb from '@/components/Header/components/Breadcrumb';
import MyCompaniesButton from '@/components/Header/components/MyCompaniesButton';
import SmartSearchButton from '@/components/Header/components/SmartSearchButton';
import DashboardLink from '@/components/Header/components/DashboardLink';

const Header = () => {
  const { pathname } = useLocation();

  return (
    <PageHeader
      className="page-header"
      title={getPageTitle(pathname)}
      ghost={false}
      backIcon={false}
      extra={[<SmartSearchButton key="1" />, <MyCompaniesButton key="2" />]}
    >
      <Breadcrumb />
      {pathname !== '/dashboard' && <DashboardLink />}
    </PageHeader>
  );
};

export default Header;
