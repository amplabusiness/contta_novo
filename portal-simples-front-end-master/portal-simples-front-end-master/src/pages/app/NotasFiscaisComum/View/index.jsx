import { useRef } from 'react';
import PropTypes from 'prop-types';
import { Button, Col, Row, Table, Tabs } from 'antd';
import { parseISO, format } from 'date-fns';
import ptBR from 'date-fns/locale/pt-BR';
import queryString from 'query-string';
import { FaCalendar, FaEye } from 'react-icons/fa';
import { useSelector } from 'react-redux';
import { Link, useLocation } from 'react-router-dom';

import { useNotasFiscaisComumContext } from '@/contexts/NotasFiscaisComumContext';
import {
  notasFiscaisMainColumns,
  notasFiscaisFirstTabColumns,
  notasFiscaisSecondTabColumns,
  invoicesSpreadsheetHeaders,
  itemsSpreadsheetHeaders,
  analyticalSpreadsheetHeaders,
} from '@/pages/app/NotasFiscaisComum/constants';

import Correction from '@/pages/app/NotasFiscaisComum/View/components/Correction';
import ChangeDate from '@/pages/app/NotasFiscaisComum/View/components/ChangeDate';
import EmptyTable from '@/components/EmptyTable';
import ExportXLSX from '@/components/ExportXLSX';

import { Container } from '@/styles/global';
import { SectionHeader } from './styles';

const NotasFiscaisView = ({ isFetching, invoices }) => {
  const { date } = useSelector(state => state.referenceDateState);

  const { search } = useLocation();
  const { operacao } = queryString.parse(search);

  const {
    state: { invoicesCount, currentItems, currentAnalytical },
    changePage,
    changeActiveInvoice,
  } = useNotasFiscaisComumContext();

  const tabsRef = useRef(null);

  const shownDate = format(parseISO(date), "MMMM '/' yyyy", { locale: ptBR });

  const mainColumns = [
    {
      title: '',
      dataIndex: 'action',
      key: 'action',
      align: 'center',
      render: (text, record) => (
        <Button
          type="primary"
          size="small"
          onClick={() => {
            changeActiveInvoice(invoices, record.id);

            setTimeout(() => {
              if (tabsRef.current) {
                tabsRef.current.scrollIntoView({ behavior: 'smooth' });
              }
            }, 100);
          }}
        >
          Selecionar
        </Button>
      ),
    },
    {
      title: 'Ação',
      dataIndex: 'action',
      key: 'action',
      align: 'center',
      render: (text, record) => (
        <a href={record.urlDanfe} target="_blank" rel="noopener noreferrer">
          <FaEye size={18} color="#3276b1" />
        </a>
      ),
    },
    {
      title: 'Número do documento',
      dataIndex: 'documentNumber',
      key: 'documentNumber',
      align: 'center',
      render: (text, record) => (
        <Link
          to={{
            pathname: '/reclassificacaoProdutos',
            search: `?operacao=${operacao}`,
            state: record.id,
          }}
        >
          {text ?? '-'}
        </Link>
      ),
      sorter: (a, b) => a.documentNumber - b.documentNumber,
      sortDirections: ['ascend', 'descend', 'ascend'],
    },
    {
      title: 'Carta Correção',
      dataIndex: 'carta',
      key: 'carta',
      align: 'center',
      render: (text, record) => (
        <Correction exists={text} description={record.descricaoCarta} />
      ),
    },
    ...notasFiscaisMainColumns.slice(0, 3),
    {
      title: 'Alterar data',
      dataIndex: 'changeDate',
      key: 'changeDate',
      align: 'center',
      render: (text, record) => <ChangeDate nf={record} />,
    },
    ...notasFiscaisMainColumns.slice(3),
  ];

  const hasActiveItemsOrAnalytical =
    currentItems.length > 0 || currentAnalytical.length > 0;

  return (
    <Container>
      <Row>
        <Col xs={24}>
          <SectionHeader>
            <span>
              {shownDate} <FaCalendar size={16} />
            </span>
            <ExportXLSX
              data={[
                {
                  headers: invoicesSpreadsheetHeaders,
                  items: invoices,
                  name: 'Notas Fiscais',
                },
                {
                  headers: itemsSpreadsheetHeaders,
                  items: currentItems,
                  name: 'Items',
                },
                {
                  headers: analyticalSpreadsheetHeaders,
                  items: currentAnalytical,
                  name: 'Analítico',
                },
              ]}
            />
          </SectionHeader>
        </Col>
      </Row>
      {invoices.length > 0 ? (
        <Table
          columns={mainColumns}
          dataSource={invoices}
          size="small"
          pagination={{
            pageSize: 10,
            showSizeChanger: false,
            total: invoicesCount,
            onChange: current => {
              changePage(current);
            },
          }}
          loading={isFetching}
          scroll={{ x: 'max-content' }}
          rowKey="id"
        />
      ) : (
        <EmptyTable title="Nenhuma nota fiscal encontrada" />
      )}
      {hasActiveItemsOrAnalytical && (
        <div ref={tabsRef}>
          <Tabs defaultActiveKey="1" type="card" style={{ marginTop: 20 }}>
            <Tabs.TabPane tab="Itens (C170)" key="1">
              {currentItems.length > 0 ? (
                <Table
                  columns={notasFiscaisFirstTabColumns}
                  dataSource={currentItems}
                  size="small"
                  pagination={{
                    pageSize: 5,
                    showSizeChanger: false,
                  }}
                  scroll={{ x: 'max-content' }}
                  rowKey="id"
                  style={{ marginTop: 10 }}
                />
              ) : (
                <EmptyTable title="Nenhum item encontrado na nota fiscal" />
              )}
            </Tabs.TabPane>
            <Tabs.TabPane tab="Analítico (C190)" key="2">
              {currentAnalytical.length > 0 ? (
                <Table
                  columns={notasFiscaisSecondTabColumns}
                  dataSource={currentAnalytical}
                  size="small"
                  pagination={{
                    pageSize: 5,
                    showSizeChanger: false,
                  }}
                  scroll={{ x: 'max-content' }}
                  rowKey="cfop"
                  style={{ marginTop: 10 }}
                />
              ) : (
                <EmptyTable title="Nenhum analítico encontrado na nota fiscal" />
              )}
            </Tabs.TabPane>
          </Tabs>
        </div>
      )}
    </Container>
  );
};

NotasFiscaisView.propTypes = {
  isFetching: PropTypes.bool.isRequired,
  invoices: PropTypes.array.isRequired,
};

export default NotasFiscaisView;
