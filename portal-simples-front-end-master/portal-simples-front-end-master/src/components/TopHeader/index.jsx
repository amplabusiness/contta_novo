import { Button, notification } from 'antd';
import { useQueryClient } from 'react-query';
import { useDispatch } from 'react-redux';
import { FiLogOut } from 'react-icons/fi';

import useViewportWidth from '@/hooks/useViewportWidth';
import { logoutSE } from '@/store/slices/user';

import ChangeCompany from '@/components/TopHeader/components/ChangeCompany';
import DownloadEmalote from '@/components/TopHeader/components/DownloadEmalote';
import RefreshButton from '@/components/TopHeader/components/RefreshButton';
import ReferenceDate from '@/components/TopHeader/components/ReferenceDate';
import Notification from '@/components/TopHeader/components/Notification';
import MobileSidebar from '@/components/TopHeader/components/MobileSidebar';

import { TopHeaderContainer } from './styles';

const TopHeader = () => {
  const dispatch = useDispatch();

  const queryClient = useQueryClient();

  const { isMobile } = useViewportWidth();

  const logoutConfirmation = () => {
    const key = `open${Date.now()}`;
    const btn = (
      <Button
        type="primary"
        onClick={() => {
          notification.close(key);
          queryClient.clear();
          return dispatch(logoutSE());
        }}
      >
        Sim, eu tenho certeza
      </Button>
    );

    notification.open({
      message: 'Saída',
      description: 'Tem certeza que deseja sair?',
      btn,
      key,
    });
  };

  return (
    <TopHeaderContainer>
      <ChangeCompany />

      {isMobile ? (
        <MobileSidebar />
      ) : (
        <div className="nav-items">
          <RefreshButton />
          <ReferenceDate />
          <DownloadEmalote />
          <Notification />
          <Button
            type="text"
            onClick={() => logoutConfirmation()}
            style={{ padding: 0 }}
          >
            <FiLogOut size={20} color="#999c9e" />
          </Button>
        </div>
      )}
    </TopHeaderContainer>
  );
};

export default TopHeader;
