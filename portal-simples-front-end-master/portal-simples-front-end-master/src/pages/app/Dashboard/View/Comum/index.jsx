import { useEffect, useState } from 'react';
import { Col, Table, Tooltip, Row } from 'antd';
import { GiArchiveResearch } from 'react-icons/gi';
import { MdSearch } from 'react-icons/md';
import { useSelector } from 'react-redux';
import { useHistory, useLocation } from 'react-router-dom';

import useDashboardNewCritics from '@/services/api/hooks/app/Dashboard/useDashboardNewCritics';
import useSegregations from '@/services/api/hooks/app/Dashboard/useSegregations';
import useDIFALValue from '@/services/api/hooks/app/Dashboard/useDIFALValue';
import { currencyFormatter } from '@/utils/formatters';
import {
  dashboardPisCofinsColumns,
  dashboardIcmsStColumns,
  dashboardFaturamentoColumns,
} from '@/pages/app/Dashboard/constants';

import Box from '@/pages/app/Dashboard/View/components/Box';
import Critic from '@/pages/app/Dashboard/View/components/Critic';
import InvoicesQuantity from '@/pages/app/Dashboard/View/components/InvoicesQuantity';
import UpdateFoundationDate from '@/pages/app/Dashboard/View/components/UpdateFoundationDate';
import DownloadDIFAL from '@/pages/app/Dashboard/View/components/DownloadDIFAL';
import ProvideAnnualIncome from '@/pages/app/Dashboard/View/components/ProvideAnnualIncome';
import UpdateAnnualIncome from '@/pages/app/Dashboard/View/components/UpdateAnnualIncome';

import { Container } from '@/styles/global';
import { DisabledLink, Section, SectionHeader, StyledLink } from './styles';

const DashboardComum = () => {
  const {
    valorContabil = {},
    simplesNacional = {},
    faturamentosMensais = [],
    faturamentoAnual = {},
  } = useSelector(state => state.activeCompanyState.data);

  const [simplesValue, setSimplesValue] = useState(0);

  const { replace } = useHistory();
  const { state: eBlockState } = useLocation();

  const { data: newCriticsData } = useDashboardNewCritics();
  const { data: segregationsData } = useSegregations();
  const { data: difalData } = useDIFALValue();

  const { totalAnual = 0 } = faturamentoAnual;
  const {
    valorEntradaMercadoria = 0,
    valorSaidaMercadoria = 0,
    notaDevolucaoSaida = 0,
    notaServicoPrestador = 0,
    baseCalculo = 0,
  } = valorContabil;
  const {
    aliquotaEfetiva = '0,00%',
    valorDas = 0,
    impostosEfetivosAliquotas = {},
  } = simplesNacional;

  const { icms = '0,00%' } = impostosEfetivosAliquotas;

  const valorDifal = Array.isArray(difalData) ? 0 : difalData;

  const icmsStTaxes = [
    {
      id: 'icmsSt',
      ...segregationsData?.detalhamentoIcmsSt,
    },
  ];
  const pisCofinsTaxes = [
    {
      id: 'pisCofins',
      ...segregationsData?.detalhamentoPisConfins,
    },
  ];

  const newCritics = newCriticsData ?? {};

  useEffect(() => {
    if (eBlockState) {
      setSimplesValue(eBlockState.simplesValue);

      replace();
    } else {
      setSimplesValue(valorDas);
    }
    // eslint-disable-next-line
  }, []);

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
            icon="money"
            iconColor="#38B000"
            values={[
              {
                value: currencyFormatter(valorSaidaMercadoria),
                color: '#38B000',
                label: 'Saídas (Vendas)',
              },
            ]}
            extraContent={
              <>
                <InvoicesQuantity />
                {valorSaidaMercadoria > 0 ? (
                  <Tooltip title="Visualizar notas">
                    <StyledLink
                      to={{
                        pathname: '/dashboard/comum',
                        search: '?operacao=Venda',
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
                value: currencyFormatter(notaDevolucaoSaida),
                label: 'Valor de Devolução',
              },
            ]}
            extraContent={
              <>
                {notaDevolucaoSaida > 0 ? (
                  <Tooltip title="Visualizar notas">
                    <StyledLink
                      to={{
                        pathname: '/dashboard/comum',
                        search: '?operacao=DevolucaoSaida',
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
              {
                value: icms,
                label: 'ICMS',
              },
            ]}
          />
          <Box
            size={6}
            icon="money"
            iconColor="#38B000"
            values={[
              {
                value: currencyFormatter(simplesValue),
                label: 'Simples a Pagar',
              },
            ]}
            extraContent={
              simplesValue > 0 ? (
                <>
                  <Tooltip title="Mais detalhes">
                    <StyledLink to="/dashboard/detalhamento">
                      <GiArchiveResearch size={26} color="#fff" />
                    </StyledLink>
                  </Tooltip>
                </>
              ) : null
            }
          />
          <Box
            size={6}
            icon="money"
            iconColor="#38B000"
            values={[
              {
                value: currencyFormatter(valorDifal),
                label: 'DIFAL',
              },
            ]}
            showDifalDownload
            extraContent={
              <>
                <DownloadDIFAL />
              </>
            }
          />
          <Box
            size={6}
            icon="money"
            iconColor="#D00000"
            values={[
              {
                value: currencyFormatter(valorEntradaMercadoria),
                color: '#D00000',
                label: 'Entradas (Compras)',
              },
            ]}
            extraContent={
              <>
                {valorEntradaMercadoria > 0 ? (
                  <Tooltip title="Visualizar notas">
                    <StyledLink
                      to={{
                        pathname: '/dashboard/comum',
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
        <Col xs={24} lg={12}>
          <Section>
            <SectionHeader>
              <h2>Críticas</h2>
              <p>Critícas novas e recorrentes dos regimes listados abaixo</p>
            </SectionHeader>
            <Row gutter={[24, 0]}>
              <Critic title="Novas" critics={newCritics} />
              <Critic title="Recorrentes" critics={{}} />
            </Row>
          </Section>
        </Col>
        <Col xs={24} lg={12}>
          <Section>
            <SectionHeader>
              <h2>PIS/Cofins</h2>
              <p>Valores relacionados ao PIS/Cofins da empresa</p>
            </SectionHeader>
            <Table
              columns={dashboardPisCofinsColumns}
              dataSource={pisCofinsTaxes}
              size="small"
              pagination={false}
              rowKey="id"
              scroll={{ x: 'max-content' }}
              style={{ margin: '20px 0 50px 0' }}
            />
            <SectionHeader>
              <h2>ICMS/ST</h2>
              <p>Valores relacionados ao ICMS/ST da empresa</p>
            </SectionHeader>
            <Table
              columns={dashboardIcmsStColumns}
              dataSource={icmsStTaxes}
              size="small"
              pagination={false}
              rowKey="id"
              scroll={{ x: 'max-content' }}
              style={{ marginTop: 20 }}
            />
          </Section>
        </Col>
      </Row>
      <Section style={{ marginTop: 40 }}>
        <SectionHeader>
          <h2>Análise de Caixa</h2>
          <p>Valores correspondentes as vendas e compras mensais da empresa</p>
        </SectionHeader>
        <Table
          columns={dashboardFaturamentoColumns}
          dataSource={faturamentosMensais}
          size="small"
          pagination={false}
          rowKey="id"
          scroll={{ x: 'max-content' }}
          style={{ marginTop: 20 }}
        />
      </Section>
    </Container>
  );
};

export default DashboardComum;
