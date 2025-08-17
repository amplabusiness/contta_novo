import { Suspense, useEffect } from 'react';
import { useSelector, useDispatch } from 'react-redux';

import { logoutSE } from '@/store/slices/user';
import APP_VERSION from '@/constants/appVersion';

import AuthenticateContainer from '@/container/authenticate.container';
import LoggedContainer from '@/container/logged.container';

import ErrorBoundary from '@/components/ErrorBoundary';
import Loading from '@/components/Loading';

const Container = () => {
  const { logged } = useSelector(state => state.userState);
  const dispatch = useDispatch();

  // Força o usuário a logar novamente caso a versão esteja desatualizada
  useEffect(() => {
    const lastVersion = localStorage.getItem('@contta:version');

    if (lastVersion !== APP_VERSION) {
      localStorage.setItem('@contta:version', APP_VERSION);

      dispatch(logoutSE());
    }
  }, [dispatch]);

  return (
    <ErrorBoundary>
      <Suspense fallback={<Loading />}>
        {logged ? <LoggedContainer /> : <AuthenticateContainer />}
      </Suspense>
    </ErrorBoundary>
  );
};

export default Container;
