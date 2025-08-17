import PropTypes from 'prop-types';
import { Table } from 'antd';
import { FiEye } from 'react-icons/fi';

import { consultaNotasFiscaisColumns } from '@/pages/app/ConsultaNotasFiscais/constants';

import Shimmer from '@/components/Shimmer/ConsultaNotasFiscais';
import EmptyTable from '@/components/EmptyTable';

const InvoicesTable = ({ data, isLoading }) => {
  const columns = [
    ...consultaNotasFiscaisColumns,
    {
      title: 'Ação',
      dataIndex: 'acao',
      key: 'acao',
      render: (text, record) => (
        <a href={record.danfe} target="_blank" rel="noopener noreferrer">
          <FiEye size={18} color="#3276b1" />
        </a>
      ),
    },
  ];

  if (isLoading) {
    return <Shimmer />;
  }

  return data.length > 0 ? (
    <Table
      columns={columns}
      dataSource={data}
      pagination={{ pageSize: 5, showSizeChanger: false }}
      size="small"
      rowKey="id"
      locale={{ emptyText: 'Os resultados aparecerão aqui' }}
      scroll={{ x: 'max-content' }}
      style={{ marginTop: 30 }}
    />
  ) : (
    <EmptyTable title="Nenhuma nota fiscal encontrada" />
  );
};

InvoicesTable.propTypes = {
  data: PropTypes.array.isRequired,
  isLoading: PropTypes.bool.isRequired,
};

export default InvoicesTable;
