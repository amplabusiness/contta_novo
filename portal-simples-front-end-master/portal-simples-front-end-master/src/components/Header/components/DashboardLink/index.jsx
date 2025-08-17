import { MdDashboard } from 'react-icons/md';
import { useSelector } from 'react-redux';

import { CustomLink } from './styles';

const DashboardLink = () => {
  const { id } = useSelector(state => state.activeCompanyState);
  const hasActiveCompany = !!id;

  return (
    <CustomLink
      id="dashboard-link"
      to="/dashboard"
      disabled={!hasActiveCompany}
    >
      <MdDashboard />
      Ir para a Dashboard
    </CustomLink>
  );
};

export default DashboardLink;
