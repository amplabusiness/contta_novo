import { lazy } from 'react';
import { Switch, Redirect } from 'react-router-dom';

import CustomRoute from '@/routes/components/CustomRoute';

const Cadastro = lazy(() => import('@/pages/auth/Cadastro'));
const EsqueciMinhaSenha = lazy(() => import('@/pages/auth/EsqueciMinhaSenha'));
const Login = lazy(() => import('@/pages/auth/Login'));
const RecuperacaoSenha = lazy(() => import('@/pages/auth/RecuperacaoSenha'));

const AuthRoutes = () => {
  return (
    <Switch>
      <CustomRoute
        exact
        path="/cadastro"
        title="Contta Simples"
        component={Cadastro}
      />
      <CustomRoute
        exact
        path="/esqueciMinhaSenha"
        title="Contta Simples"
        component={EsqueciMinhaSenha}
      />
      <CustomRoute
        exact
        path="/login"
        title="Contta Simples"
        component={Login}
      />
      <CustomRoute
        exact
        path="/recuperacaoSenha"
        title="Contta Simples"
        component={RecuperacaoSenha}
      />
      <CustomRoute
        title="Contta Simples"
        component={() => <Redirect to="/login" />}
      />
    </Switch>
  );
};

export default AuthRoutes;
