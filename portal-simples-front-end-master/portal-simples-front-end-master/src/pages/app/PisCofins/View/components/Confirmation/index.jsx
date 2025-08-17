import { Col, Divider, Row } from 'antd';

import usePisCofins from '@/services/api/hooks/app/PisCofins/usePisCofins';
import { usePisCofinsContext } from '@/contexts/PisCofinsContext';

import ErrorMessage from '@/components/ErrorMessage';
import Shimmer from '@/components/Shimmer/PisCofins';

import Law from '@/pages/app/PisCofins/View/components/Confirmation/components/Law';
import Ncms from '@/pages/app/PisCofins/View/components/Confirmation/components/Ncms';
import Products from '@/pages/app/PisCofins/View/components/Confirmation/components/Products';

import { Container, Title } from '@/styles/global';
import { Content } from './styles';

const PisCofinsConfirmation = () => {
  const {
    state: { activeNcm },
  } = usePisCofinsContext();

  const { isLoading, isFetching, isError } = usePisCofins('monofasico');

  if (isLoading || isFetching) {
    return <Shimmer />;
  }

  if (isError) {
    return <ErrorMessage />;
  }

  return (
    <Container>
      <Title>
        <h2>Configuração por Produto</h2>
        <p>
          Nessa aba, você irá confirmar a natureza seu produtos monofásicos.
          Basta seguir os passos que serão mostrados.
        </p>
      </Title>

      <Divider />

      <Content>
        <Row gutter={[24, 0]}>
          <Col xs={24} lg={6}>
            <Ncms />
          </Col>
          {activeNcm && (
            <>
              <Col xs={24} lg={10}>
                <Products />
              </Col>
              <Col xs={24} lg={8}>
                <Law />
              </Col>
            </>
          )}
        </Row>
      </Content>
    </Container>
  );
};

export default PisCofinsConfirmation;
