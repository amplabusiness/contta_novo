import PropTypes from 'prop-types';
import { ConfigProvider } from 'antd';
import ptBR from 'antd/lib/locale-provider/pt_BR';
import { PersistGate } from 'redux-persist/integration/react';
import { QueryClient, QueryClientProvider } from 'react-query';
import { Provider } from 'react-redux';
import { BrowserRouter } from 'react-router-dom';
import { render } from '@testing-library/react';

import { store, persistor } from '@/store';

const WrappedApp = ({ children }) => {
  const queryClient = new QueryClient();

  return (
    <ConfigProvider locale={ptBR}>
      <Provider store={store}>
        <PersistGate persistor={persistor}>
          <QueryClientProvider client={queryClient}>
            <BrowserRouter>{children}</BrowserRouter>
          </QueryClientProvider>
        </PersistGate>
      </Provider>
    </ConfigProvider>
  );
};

WrappedApp.propTypes = {
  children: PropTypes.node.isRequired,
};

const renderWithProvider = (ui, options) => {
  render(ui, { wrapper: WrappedApp, ...options });
};

export * from '@testing-library/react';
export { renderWithProvider as render };
