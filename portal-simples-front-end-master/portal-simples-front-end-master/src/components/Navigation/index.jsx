import { useSelector } from 'react-redux';
import { useHistory, useLocation } from 'react-router-dom';

import links from '@/constants/links';

import UserName from '@/components/UserName';

import logoImg from '@/assets/images/conttaAlt.png';

import { Container, Sidebar, Logo, Item } from './styles';

const Navigation = () => {
  const { id, companyType } = useSelector(state => state.activeCompanyState);
  const { faturamentoAnual = {} } = useSelector(
    state => state.activeCompanyState.data,
  );

  const oneCompanyIsSelected = !!id;
  const { totalAnual = 0 } = faturamentoAnual;

  const { pathname } = useLocation();
  const { push } = useHistory();

  const navigate = link => {
    const differentPage = link !== pathname;
    const isHomeLink = link === '/home';

    if (
      (differentPage && isHomeLink) ||
      (differentPage && oneCompanyIsSelected)
    ) {
      push(link);
    }
  };

  return (
    <Container>
      <Sidebar>
        <Logo to="/dashboard" disabled={!oneCompanyIsSelected}>
          <img src={logoImg} alt="Contta" />
          <p>Contta</p>
        </Logo>

        <ul>
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
              <Item
                key={item.id}
                active={isActive}
                disabled={index === 0 ? false : !oneCompanyIsSelected}
                onClick={() => navigate(item.link)}
              >
                <div className="icon">{item.icon}</div>
                <span className="title">{item.title}</span>
              </Item>
            );
          })}
        </ul>

        <UserName />
      </Sidebar>
    </Container>
  );
};

export default Navigation;
