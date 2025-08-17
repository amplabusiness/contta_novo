import { Col, Table, Tooltip, Row } from 'antd';
import { MdSearch } from 'react-icons/md';
import { useSelector } from 'react-redux';

import { currencyFormatter } from '@/utils/formatters';
import {
  dashboardImpostosEfetivosColumns,
  dashboardTransporteBaseCalculoColumns,
} from '@/pages/app/Dashboard/constants';

import Box from '@/pages/app/Dashboard/View/components/Box';
import ProvideAnnualIncome from '@/pages/app/Dashboard/View/components/ProvideAnnualIncome';
import UpdateAnnualIncome from '@/pages/app/Dashboard/View/components/UpdateAnnualIncome';
import UpdateFoundationDate from '@/pages/app/Dashboard/View/components/UpdateFoundationDate';

import { Container } from '@/styles/global';
import { DisabledLink, Section, SectionHeader, StyledLink } from './styles';

const DashboardTransporte = () => {
  const {
    valorContabil = {},
    simplesNacional = {},
    faturamentoAnual = {},
  } = useSelector(state => state.activeCompanyState.data);

  const { totalAnual = 0 } = faturamentoAnual;
  const {
    notaServicoPrestador = 0,
    notaDevolucaoSaida = 0,
    valorFreteIntramunicipal = 0,
    valorFreteIntermunicipal = 0,
    valorFreteInterestadual = 0,
    baseCalculo = 0,
  } = valorContabil;

  const {
    valorDas = 0,
    aliquotaEfetivaMunicipal,
    aliquotaEfetivaEstadual,
    aliquotaEfetivaInterestadual,
    impostosEfetivosMunicipais,
    impostosEfetivosEstaduais,
    impostosEfetivosInterestaduais,
  } = simplesNacional;

  const calculationBasisValues = [
    {
      id: 'calculationBasis',
      intramunicipal: valorFreteIntramunicipal,
      intermunicipal: valorFreteIntermunicipal,
      interestadual: valorFreteInterestadual,
      total:
        valorFreteIntramunicipal +
        valorFreteIntermunicipal +
        valorFreteInterestadual,
    },
  ];

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
                label: 'Receita dos Serviços de Transporte',
              },
            ]}
            extraContent={
              <>
                {notaServicoPrestador > 0 ? (
                  <Tooltip title="Visualizar notas">
                    <StyledLink
                      to={{
                        pathname: '/dashboard/transporte',
                        search: '?operacao=Entrada',
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
                value: currencyFormatter(notaDevolucaoSaida),
                label: 'Valor de Dedução do Serviço',
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
                value: aliquotaEfetivaMunicipal,
                label: 'Alíquota Efetiva Intramunicipal',
              },
            ]}
            extraContent={
              <>
                {notaServicoPrestador > 0 ? (
                  <Tooltip title="Visualizar notas">
                    <StyledLink
                      to={{
                        pathname: '/dashboard/transporte',
                        search: '?operacao=Entrada',
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
            icon="percent"
            iconColor="#FB8500"
            values={[
              {
                value: aliquotaEfetivaEstadual,
                label: 'Alíquota Efetiva Intermunicipal',
              },
            ]}
            extraContent={
              <>
                {notaServicoPrestador > 0 ? (
                  <Tooltip title="Visualizar notas">
                    <StyledLink
                      to={{
                        pathname: '/dashboard/transporte',
                        search: '?operacao=Entrada',
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
            icon="percent"
            iconColor="#FB8500"
            values={[
              {
                value: aliquotaEfetivaInterestadual,
                label: 'Alíquota Efetiva Interestadual',
              },
            ]}
            extraContent={
              <>
                {notaServicoPrestador > 0 ? (
                  <Tooltip title="Visualizar notas">
                    <StyledLink
                      to={{
                        pathname: '/dashboard/transporte',
                        search: '?operacao=Entrada',
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
            iconColor="#38B000"
            values={[
              {
                value: currencyFormatter(valorDas),
                label: 'Simples a Pagar',
              },
            ]}
          />
        </Row>
      </Section>
      <Row gutter={[24, 0]}>
        <Col xs={24} md={12} style={{ marginTop: 40 }}>
          <Section>
            <SectionHeader>
              <h2>Total da Base de Cálculo</h2>
              <p>Valores referentes as bases de cálculo da empresa</p>
            </SectionHeader>
            <Table
              columns={dashboardTransporteBaseCalculoColumns}
              dataSource={calculationBasisValues}
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
              <h2>Repartição Efetiva dos Tributos (Municipal)</h2>
              <p>Valores reais dos impostos do Simples Nacional</p>
            </SectionHeader>
            <Table
              columns={dashboardImpostosEfetivosColumns['Anexo III']}
              dataSource={[
                {
                  id: 'impostosEfetivosMunicipais',
                  ...impostosEfetivosMunicipais,
                },
              ]}
              pagination={false}
              rowKey="id"
              size="small"
              scroll={{ x: 'max-content' }}
              style={{ marginTop: 20 }}
            />
          </Section>
        </Col>
        <Col xs={24} md={12} style={{ marginTop: 60 }}>
          <Section>
            <SectionHeader>
              <h2>Repartição Efetiva dos Tributos (Estadual)</h2>
              <p>Valores reais dos impostos do Simples Nacional</p>
            </SectionHeader>
            <Table
              columns={dashboardImpostosEfetivosColumns['Anexo I-III']}
              dataSource={[
                {
                  id: 'impostosEfetivosEstaduais',
                  ...impostosEfetivosEstaduais,
                },
              ]}
              pagination={false}
              rowKey="id"
              size="small"
              scroll={{ x: 'max-content' }}
              style={{ marginTop: 20 }}
            />
          </Section>
        </Col>
        <Col xs={24} md={12} style={{ marginTop: 60 }}>
          <Section>
            <SectionHeader>
              <h2>Repartição Efetiva dos Tributos (Interestadual)</h2>
              <p>Valores reais dos impostos do Simples Nacional</p>
            </SectionHeader>
            <Table
              columns={dashboardImpostosEfetivosColumns['Anexo I-III']}
              dataSource={[
                {
                  id: 'impostosEfetivosInterestaduais',
                  ...impostosEfetivosInterestaduais,
                },
              ]}
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

export default DashboardTransporte;
