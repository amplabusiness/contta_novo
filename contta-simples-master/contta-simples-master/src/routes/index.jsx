import React, { Suspense } from 'react';
import { HashRouter, Switch, Route, Redirect } from 'react-router-dom';

//login
// import Login from '../pages/login';

//ItemBag
//import ItemBagIndex from "../pages/ItemBag"

const Routes = () => (
  <Suspense fallback={<div>Carregando...</div>}>
    <Switch>{/* Main Page */}</Switch>
  </Suspense>
);

export default Routes;
