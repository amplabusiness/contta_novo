import { useEffect } from 'react';
import PropTypes from 'prop-types';
import { notification } from 'antd';
import { useSelector } from 'react-redux';
import { Redirect, Route } from 'react-router-dom';

const CustomRoute = ({
  title,
  shouldRenderOnlyWithActiveCompany = false,
  ...rest
}) => {
  const { id } = useSelector(state => state.activeCompanyState);
  const oneCompanyIsSelected = !!id;

  useEffect(() => {
    document.title = title;

    window.scrollTo({ top: 0, behavior: 'smooth' });
  }, [title]);

  useEffect(() => {
    if (shouldRenderOnlyWithActiveCompany) {
      if (!oneCompanyIsSelected) {
        notification.warning({
          message: 'Aviso',
          description:
            'Certifique-se de selecionar uma empresa antes de entrar nessa tela.',
        });
      }
    }
  }, [oneCompanyIsSelected, shouldRenderOnlyWithActiveCompany]);

  if (shouldRenderOnlyWithActiveCompany) {
    if (!oneCompanyIsSelected) {
      return <Redirect to="/home" />;
    }
  }

  return <Route {...rest} />;
};

CustomRoute.propTypes = {
  title: PropTypes.string.isRequired,
  shouldRenderOnlyWithActiveCompany: PropTypes.bool,
};

CustomRoute.defaultProps = {
  shouldRenderOnlyWithActiveCompany: false,
};

export default CustomRoute;
