import { ConfigProvider } from 'antd';
import OidcAuthProvider from '@/auth/oidcProvider';
import antd_ptBR from 'antd/lib/locale-provider/pt_BR';
import { QueryClientProvider } from 'react-query';
import { Provider } from 'react-redux';
import { PersistGate } from 'redux-persist/integration/react';

import { store, persistor } from '@/store';
import { queryClient } from '@/services/queryClient';

import Container from '@/container';

import GlobalStyles from '@/styles/global';

const App = () => {
  return (
    <ConfigProvider locale={antd_ptBR}>
      <Provider store={store}>
        <PersistGate persistor={persistor}>
          <QueryClientProvider client={queryClient}>
            <OidcAuthProvider>
              <GlobalStyles />
              <Container />
            </OidcAuthProvider>
          </QueryClientProvider>
        </PersistGate>
      </Provider>
    </ConfigProvider>
  );
};

export default App;
