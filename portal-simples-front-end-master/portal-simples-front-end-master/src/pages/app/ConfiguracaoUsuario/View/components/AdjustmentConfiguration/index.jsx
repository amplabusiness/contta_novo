import { Button, Col } from 'antd';
import { useSelector } from 'react-redux';
import { useHistory } from 'react-router-dom';

import { Title } from '@/styles/global';
import { Content } from '@/pages/app/ConfiguracaoUsuario/View/styles';

const AdjustmentConfiguration = () => {
  const { faturamentoAnual = {} } = useSelector(
    state => state.activeCompanyState.data,
  );
  const { totalAnual = 0 } = faturamentoAnual;

  const isBlocoECompany = totalAnual > 3600000;

  const { push } = useHistory();

  return isBlocoECompany ? (
    <Col xs={24} md={12} style={{ marginTop: 20 }}>
      <Title>
        <h2>Tabela de Códigos de Ajustes da Apuração do ICMS</h2>
        <p>
          Clique no botão abaixo para ser redirecionado a tela de ajuste da
          apuração.
        </p>
      </Title>
      <Content>
        <Button
          type="primary"
          onClick={() => push('/ajusteApuracao')}
          style={{ marginTop: 20 }}
        >
          Redirecionar
        </Button>
      </Content>
    </Col>
  ) : null;
};

export default AdjustmentConfiguration;
