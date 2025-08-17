import { Col, Divider, Form as AntForm, Row, Select } from 'antd';

import { useIcmsStContext } from '@/contexts/IcmsStContext';
import { regimeOptions } from '@/pages/app/IcmsSt/constants';

import ExigSuspensa from '@/pages/app/IcmsSt/View/components/Confirmation/components/ExigSuspensa';
import Antecipacao from '@/pages/app/IcmsSt/View/components/Confirmation/components/Antecipacao';
import DefaultTax from '@/pages/app/IcmsSt/View/components/Confirmation/components/DefaultTax';

import Form from '@/components/Form';

import { Container, Title } from '@/styles/global';
import { Content } from './styles';

const { Item: FormItem } = AntForm;

const IcmsStConfirmation = () => {
  const {
    state: { currentTax },
    setCurrentTax,
    resetState,
  } = useIcmsStContext();

  const handleRegimeChange = value => {
    const { regime } = value;

    if (!['exigSuspensa', 'antEncTributacao'].includes(regime)) {
      resetState();
    }

    setCurrentTax(regime);
  };

  /* O componente DefaultTax compreende os seguintes impostos:
   * Benefícios, ICMS/ST, Imune, Isenção/Redução, Isenção/Redução Cesta Básica e Isento
   */
  const selectedPage = {
    exigSuspensa: <ExigSuspensa />,
    antEncTributacao: <Antecipacao />,
    default: <DefaultTax />,
  };

  return (
    <Container>
      <Title>
        <h2>Configuração por Produto</h2>
        <p>
          Nessa tela você irá confirmar a natureza seu produtos. Primeiro,
          selecione um regime entre os listados no campo abaixo. Depois siga os
          passos que serão mostrados.
        </p>
      </Title>

      <Form
        name="change-icms-regime-form"
        initialValues={{
          regime: currentTax,
        }}
        onValuesChange={handleRegimeChange}
      >
        <Content>
          <Row gutter={[24, 0]}>
            <Col xs={24} md={4}>
              <FormItem name="regime" label="Selecione o regime">
                <Select id="icms-change-regime" listHeight={128}>
                  {regimeOptions.map(item => (
                    <Select.Option key={item.key} value={item.value}>
                      {item.label}
                    </Select.Option>
                  ))}
                </Select>
              </FormItem>
            </Col>
          </Row>
        </Content>
      </Form>

      <Divider />

      {currentTax && (selectedPage[currentTax] ?? selectedPage.default)}
    </Container>
  );
};

export default IcmsStConfirmation;
