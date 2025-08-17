import { BrowserRouter } from 'react-router-dom';

import AuthRoutes from '@/routes/auth.routes';

const AuthenticateContainer = () => {
  return (
    <BrowserRouter>
      <AuthRoutes />
    </BrowserRouter>
  );
};

export default AuthenticateContainer;
