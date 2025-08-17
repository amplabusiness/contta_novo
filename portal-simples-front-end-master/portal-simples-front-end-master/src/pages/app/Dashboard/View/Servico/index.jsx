import { Col, Table, Tooltip, Row } from 'antd';
import { MdSearch } from 'react-icons/md';
import { useSelector } from 'react-redux';

import { currencyFormatter } from '@/utils/formatters';
import {
  dashboardImpostosColumns,
  dashboardImpostosEfetivosColumns,
} from '@/pages/app/Dashboard/constants';

import Box from '@/pages/app/Dashboard/View/components/Box';
import UpdateFoundationDate from '@/pages/app/Dashboard/View/components/UpdateFoundationDate';
import ProvideAnnualIncome from '@/pages/app/Dashboard/View/components/ProvideAnnualIncome';
import UpdateAnnualIncome from '@/pages/app/Dashboard/View/components/UpdateAnnualIncome';

import { Container } from '@/styles/global';
import { DisabledLink, Section, SectionHeader, StyledLink } from './styles';

const DashboardServico = () => {
  const { annex } = useSelector(state => state.activeCompanyState);
  const {
    valorContabil = {},
    simplesNacional = {},
    faturamentoAnual = {},
  } = useSelector(state => state.activeCompanyState.data);

  const { totalAnual = 0 } = faturamentoAnual;
  const {
    notaServicoPrestador = 0,
    notaDevolucaoPrestacao = 0,
    baseCalculo = 0,
  } = valorContabil;
  const {
    aliquotaEfetiva = '0,00%',
    valorDas = 0,
    impostos,
    impostosEfetivos,
  } = simplesNacional;

  return (
    <Container>
      <Section>
        <SectionHeader>
          <h2>Simples Nacional</h2>
          <p>Números referentes ao Simples Nacional da empresa</p>
        </SectionHeader>
        <Row gutter={[24, 0]}>
          <Box
            size={6}
            icon="calculator"
            iconColor="#7F3E00"
            values={[
              {
                value: currencyFormatter(notaServicoPrestador),
                label: 'Receita dos Serviços',
              },
            ]}
            extraContent={
              <>
                {notaServicoPrestador > 0 ? (
                  <Tooltip title="Visualizar notas">
                    <StyledLink
                      to={{
                        pathname: '/dashboard/servicos',
                        search: '?operacao=Prestador',
                      }}
                    >
                      <MdSearch size={24} color="#fff" />
                    </StyledLink>
                  </Tooltip>
                ) : (
                  <DisabledLink>
                    <MdSearch size={24} color="#333" />
                  </DisabledLink>
                )}
              </>
            }
          />
          <Box
            size={6}
            icon="money"
            iconColor="#D00000"
            values={[
              {
                value: currencyFormatter(notaDevolucaoPrestacao),
                label: 'Valor de Dedução do Serviço',
              },
            ]}
          />
          <Box
            size={6}
            icon="money"
            iconColor="#D00000"
            values={[
              {
                value: currencyFormatter(baseCalculo),
                label: 'Base de Cálculo',
              },
            ]}
            extraContent={
              <>
                <UpdateFoundationDate />
              </>
            }
          />
          <Box
            size={6}
            icon="percent"
            iconColor="#FB8500"
            values={[
              {
                value: aliquotaEfetiva,
                label: 'Alíquota Efetiva',
              },
            ]}
          />
          <Box
            size={6}
            icon="money"
            iconColor="#38B000"
            values={[
              {
                value: currencyFormatter(valorDas),
                label: 'Simples a Pagar',
              },
            ]}
          />
          <Box
            size={6}
            icon="money"
            iconColor="#38B000"
            values={[
              {
                value: currencyFormatter(totalAnual),
                label: 'Faturamento Acumulado 12 meses',
              },
            ]}
            extraContent={
              <>
                <ProvideAnnualIncome />
                <UpdateAnnualIncome />
              </>
            }
          />
        </Row>
      </Section>
      <Row gutter={[24, 0]}>
        <Col xs={24} md={12} style={{ marginTop: 40 }}>
          <Section>
            <SectionHeader>
              <h2>Repartição dos Tributos</h2>
              <p>Esses são os impostos do Simples Nacional</p>
            </SectionHeader>
            <Table
              columns={dashboardImpostosColumns[annex]}
              dataSource={[{ id: 'impostos', ...impostos }]}
              pagination={false}
              rowKey="id"
              size="small"
              scroll={{ x: 'max-content' }}
              style={{ marginTop: 20 }}
            />
          </Section>
        </Col>
        <Col xs={24} md={12} style={{ marginTop: 40 }}>
          <Section>
            <SectionHeader>
              <h2>Repartição Efetiva dos Tributos</h2>
              <p>Valores reais dos impostos do Simples Nacional</p>
            </SectionHeader>
            <Table
              columns={dashboardImpostosEfetivosColumns[annex]}
              dataSource={[{ id: 'impostosEfetivos', ...impostosEfetivos }]}
              pagination={false}
              rowKey="id"
              size="small"
              scroll={{ x: 'max-content' }}
              style={{ marginTop: 20 }}
            />
          </Section>
        </Col>
      </Row>
    </Container>
  );
};

export default DashboardServico;
