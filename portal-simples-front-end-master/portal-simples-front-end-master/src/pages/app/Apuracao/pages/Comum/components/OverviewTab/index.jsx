import { Divider, Row, Table } from 'antd';
import { Link } from 'react-router-dom';

import useCalculation from '@/services/api/hooks/app/Apuracao/useCalculation';
import {
  apuracaoColumns,
  apuracaoSaldoColumns,
  spreadsheetHeaders,
} from '@/pages/app/Apuracao/constants';

import EmptyTable from '@/components/EmptyTable';
import ErrorMessage from '@/components/ErrorMessage';
import ExportXLSX from '@/components/ExportXLSX';
import Shimmer from '@/components/Shimmer/Apuracao';

import { Card, Container } from './styles';

const OverviewTab = () => {
  const { data = {}, isLoading, isError } = useCalculation();

  const { entries = [], outs = [], total = [] } = data;

  const entradasColumns = [
    {
      title: 'CFOP',
      dataSource: 'cfop',
      key: 'cfop',
      width: '10%',
      render: (text, record) => {
        const isNormalRow = record.cfop !== 'TOTAL';

        if (isNormalRow) {
          const nfeIds = Array.isArray(record.lintNfeId)
            ? record.lintNfeId
            : [];
          const slicedNfeIds =
            nfeIds.length > 10 ? nfeIds.slice(0, 10) : nfeIds;

          return (
            <Link
              to={{
                pathname: '/dashboard/comum',
                search: '?operacao=Entrada',
                state: slicedNfeIds,
              }}
              style={{ color: '#3276b1' }}
            >
              {record.cfop}
            </Link>
          );
        }

        return <strong>{record.cfop}</strong>;
      },
    },
    ...apuracaoColumns,
  ];

  const saidasColumns = [
    {
      title: 'CFOP',
      dataSource: 'cfop',
      key: 'cfop',
      width: '10%',
      render: (text, record) => {
        const isNormalRow = record.cfop !== 'TOTAL';

        if (isNormalRow) {
          const nfeIds = Array.isArray(record.lintNfeId)
            ? record.lintNfeId
            : [];
          const slicedNfeIds =
            nfeIds.length > 10 ? nfeIds.slice(0, 10) : nfeIds;

          return (
            <Link
              to={{
                pathname: '/dashboard/comum',
                search: '?operacao=Venda',
                state: slicedNfeIds,
              }}
              style={{ color: '#3276b1' }}
            >
              {record.cfop}
            </Link>
          );
        }

        return <strong>{record.cfop}</strong>;
      },
    },
    ...apuracaoColumns,
  ];

  if (isError) {
    return <ErrorMessage />;
  }

  if (isLoading) {
    return <Shimmer />;
  }

  return (
    <Container>
      <Row align="center" justify="center">
        <ExportXLSX
          data={[
            {
              headers: spreadsheetHeaders,
              items: outs,
              name: 'Saídas',
            },
            {
              headers: spreadsheetHeaders,
              items: entries,
              name: 'Entradas',
            },
          ]}
        />
      </Row>
      <Card>
        <h2>Comparativo de entradas e saídas</h2>
        <Divider />
        {entries.length > 0 ? (
          <Table
            columns={entradasColumns}
            dataSource={entries}
            pagination={false}
            size="small"
            rowKey="cfop"
            scroll={{ x: 'max-content' }}
            rowClassName={(record, index) => {
              if (record.cfop === '') {
                return 'last-row';
              }

              return '';
            }}
          />
        ) : (
          <EmptyTable title="Nenhuma apuração de entradas encontrada" />
        )}
        {outs.length > 0 ? (
          <Table
            columns={saidasColumns}
            dataSource={outs}
            pagination={false}
            size="small"
            rowKey="cfop"
            scroll={{ x: 'max-content' }}
            rowClassName={(record, index) => {
              if (record.cfop === '') {
                return 'last-row';
              }

              if (record.calculoSimples) {
                return 'simples-row';
              }

              return '';
            }}
            style={{ marginTop: 20 }}
          />
        ) : (
          <EmptyTable
            title="Nenhuma apuração de saídas encontrada"
            style={{ marginTop: 20 }}
          />
        )}
      </Card>
      <Card>
        <h2>Saldo apurado</h2>
        <Divider />
        {total.length > 0 ? (
          <Table
            columns={apuracaoSaldoColumns}
            dataSource={total}
            pagination={false}
            size="small"
            rowKey="description"
            scroll={{ x: 'max-content' }}
          />
        ) : (
          <EmptyTable title="Nenhum saldo encontrado" />
        )}
      </Card>
    </Container>
  );
};

export default OverviewTab;
