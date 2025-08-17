import React from 'react';
import { Switch, Route, Redirect } from 'react-router-dom';

//login
import Login from '../pages/login';

//cadastro
import Cadastro from '../pages/cadastro';

//dashboard
import Dashboard from '../pages/dashboard';

const PrivateRouter = ({ component: Component, ...rest }) => (
  <Route
    {...rest}
    render={(props) =>
      !!localStorage.getItem('@contta:token') ? (
        <Component {...props} />
      ) : (
        <Redirect
          to={{
            exact: true,
            pathname: '/login',
            state: { from: props.location },
          }}
        />
      )
    }
  />
);

const Routes = () => (
  <Switch>
    {/* Login */}
    <Route exact path="/login" component={Login} />
    {/* Cadastro */}
    <Route exact path="/cadastro" component={Cadastro} />
    {/* Usuário */}
    <PrivateRouter exact path="/" component={Dashboard} />
  </Switch>
);

export default Routes;
