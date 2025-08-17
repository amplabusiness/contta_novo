import React from 'react';
import ReactDOM from 'react-dom';
import * as serviceWorker from './serviceWorker';

import { LocaleProvider } from 'antd';
import ptBR from 'antd/lib/locale-provider/pt_BR';

import { Provider } from 'react-redux';

import Main from './components/theme/main';

const devTools = window.__REDUX_DEVTOOLS_EXTENSION__ && window.__REDUX_DEVTOOLS_EXTENSION__();

ReactDOM.render(
  <LocaleProvider locale={ptBR}>
    <Main />
  </LocaleProvider>,
  document.getElementById('root'),
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
