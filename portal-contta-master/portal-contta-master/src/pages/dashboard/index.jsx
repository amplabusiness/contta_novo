import React, { Suspense } from 'react';

import Header from '../../components/theme/header';
import Content from '../../components/theme/content';

const Dashboard = () => {
  return (
    <Suspense
      fallback={
        <div>
          <br />
          Carregando...
        </div>
      }>
      <Header title="Usuários" />
      <Content>
        <p className="text-center" style={{ marginTop: '1em' }}>
          TELA INICIAL
        </p>
      </Content>
    </Suspense>
  );
};

export default Dashboard;
