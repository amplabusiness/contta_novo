import { Col, Row } from 'antd';

import { useIcmsStContext } from '@/contexts/IcmsStContext';
import useIcmsSt from '@/services/api/hooks/app/IcmsSt/useIcmsSt';

import ErrorMessage from '@/components/ErrorMessage';
import Shimmer from '@/components/Shimmer/IcmsSt';

import Law from '@/pages/app/IcmsSt/View/components/Confirmation/components/DefaultTax/Law';
import Ncms from '@/pages/app/IcmsSt/View/components/Confirmation/components/DefaultTax/Ncms';
import Products from '@/pages/app/IcmsSt/View/components/Confirmation/components/DefaultTax/Products';

import { Container } from './styles';

const DefaultTax = () => {
  const {
    state: { currentTax, activeNcm },
  } = useIcmsStContext();

  const { isLoading, isFetching, isError } = useIcmsSt(currentTax);

  if (isLoading || isFetching) {
    return <Shimmer />;
  }

  if (isError) {
    return <ErrorMessage />;
  }

  return (
    <Container>
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
    </Container>
  );
};

export default DefaultTax;
