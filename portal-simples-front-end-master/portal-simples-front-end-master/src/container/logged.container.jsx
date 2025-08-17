import { BrowserRouter } from 'react-router-dom';

import useAuthorizations from '@/services/api/hooks/app/Empresas/useAuthorizations';
import useCompanies from '@/services/api/hooks/app/Empresas/useCompanies';

import AppRoutes from '@/routes/app.routes';

import Footer from '@/components/Footer';
import Header from '@/components/Header';
import Navigation from '@/components/Navigation';
import TopHeader from '@/components/TopHeader';

const LoggedContainer = () => {
  // Verifica continuamente se existem autorizações para o usuário responder
  useAuthorizations();
  // Traz todas as empresas cadastradas do usuário
  useCompanies({ executeOnSuccessCallback: true });

  return (
    <BrowserRouter>
      <Navigation />
      <div id="page-wrapper">
        <TopHeader />
        <div style={{ padding: '0 15px 80px 15px' }}>
          <Header />
          <AppRoutes />
          <Footer />
        </div>
      </div>
    </BrowserRouter>
  );
};

export default LoggedContainer;
