import { AppProps } from 'next/app';
import Head from 'next/head';
import { ThemeProvider } from 'styled-components';

import UIContextProvider from '../context/ui';

import theme from '../styles/theme';
import GlobalStyles from '../styles/global';

const App: React.FC<AppProps> = ({ Component, pageProps }) => {
  return (
    <>
      <Head>
        <link rel="icon" href="/favicon.ico" />
        <title>Contta Inteligência Fiscal</title>
      </Head>
      <UIContextProvider>
        <GlobalStyles />
        <ThemeProvider theme={theme}>
          <Component {...pageProps} />
        </ThemeProvider>
      </UIContextProvider>
    </>
  );
};

export default App;
