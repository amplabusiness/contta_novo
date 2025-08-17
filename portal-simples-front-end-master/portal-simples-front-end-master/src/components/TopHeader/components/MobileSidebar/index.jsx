import { useState } from 'react';
import { Button, notification } from 'antd';
import { FiCalendar, FiLogOut } from 'react-icons/fi';
import { GoThreeBars } from 'react-icons/go';
import { useQueryClient } from 'react-query';
import { useSelector, useDispatch } from 'react-redux';
import { useHistory, useLocation } from 'react-router-dom';

import useViewportWidth from '@/hooks/useViewportWidth';
import { logoutSE } from '@/store/slices/user';
import links from '@/constants/links';

import Notification from '@/components/TopHeader/components/Notification';
import ReferenceDate from '@/components/TopHeader/components/ReferenceDate';
import UserName from '@/components/UserName';

import {
  OpenButton,
  CustomDrawer,
  List,
  ListItem,
  LogoutButton,
} from './styles';

const MobileSidebar = () => {
  const { id, companyType } = useSelector(state => state.activeCompanyState);
  const { faturamentoAnual = {} } = useSelector(
    state => state.activeCompanyState.data,
  );
  const dispatch = useDispatch();
  const oneCompanyIsSelected = !!id;
  const { totalAnual = 0 } = faturamentoAnual;

  const [isOpen, setIsOpen] = useState(false);

  const queryClient = useQueryClient();

  const { pathname } = useLocation();
  const { push } = useHistory();

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

  const openSidebar = () => {
    setIsOpen(true);
  };

  const closeSidebar = () => {
    setIsOpen(false);
  };

  const navigate = link => {
    const differentPage = link !== pathname;
    const isHomeLink = link === '/home';

    if (
      (differentPage && isHomeLink) ||
      (differentPage && oneCompanyIsSelected)
    ) {
      closeSidebar();
      push(link);
    }
  };

  return isMobile ? (
    <>
      <OpenButton type="primary" htmlType="button" onClick={openSidebar}>
        <GoThreeBars color="#fff" size={16} />
      </OpenButton>
      <CustomDrawer
        visible={isOpen}
        placement="right"
        onClose={closeSidebar}
        closable={false}
      >
        <List>
          {links.map((item, index) => {
            // Verifica se o link pode ser mostrado de acordo com o tipo da empresa
            if (item.disabledWhen.includes(companyType)) {
              return null;
            }

            // Verifica se o link só pode ser mostrado quando há um valor alto de faturamento
            if (item.onlyEnabledOnHighIncomeValue) {
              const minValue = 3600000;

              if (totalAnual <= minValue) {
                return null;
              }
            }

            const isActive = pathname.includes(item.link);

            return (
              <ListItem
                key={item.id}
                active={isActive}
                disabled={index === 0 ? false : !oneCompanyIsSelected}
                onClick={() => navigate(item.link)}
              >
                <div>{item.icon}</div>
                <span>{item.title}</span>
              </ListItem>
            );
          })}
          <ListItem>
            <FiCalendar size={20} color="#999c9e" />
            <ReferenceDate />
          </ListItem>
          <ListItem>
            <Notification />
            <p>Notificações</p>
          </ListItem>
          <ListItem>
            <LogoutButton type="button" onClick={() => logoutConfirmation()}>
              <FiLogOut size={20} color="#999c9e" />
              <p>Sair</p>
            </LogoutButton>
          </ListItem>
          <UserName />
        </List>
      </CustomDrawer>
    </>
  ) : null;
};

export default MobileSidebar;
