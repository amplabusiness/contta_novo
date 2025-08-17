import PropTypes from 'prop-types';
import { Table } from 'antd';
import { FaRegFilePdf } from 'react-icons/fa';

import { notasFiscaisSimplificadasTransporteColumns } from '@/pages/app/NotasFiscaisTransporte/constants';

import EmptyTable from '@/components/EmptyTable';

import { Container, Title } from '@/styles/global';
import { ViewButton } from './styles';

const NotasFiscaisTransporteView = ({ data = [] }) => {
  const columns = [
    ...notasFiscaisSimplificadasTransporteColumns,
    {
      title: 'Ação',
      dataIndex: 'acao',
      key: 'acao',
      render: (text, record) => (
        <ViewButton htmlType="button">
          <FaRegFilePdf size={14} />
          Visualizar
        </ViewButton>
      ),
    },
  ];

  return (
    <Container>
      <Title>
        <h2>Listagem de Notas Fiscais de Transporte</h2>
        <p>
          Abaixo encontram-se todas as notas fiscais de transporte da empresa
          ativa. Use o botão de Visualizar para ver a nota em um PDF.
        </p>
      </Title>
      {data.length > 0 ? (
        <Table
          columns={columns}
          dataSource={data}
          pagination={{
            pageSize: 5,
            showSizeChanger: false,
          }}
          size="small"
          rowKey="id"
          scroll={{ x: 'max-content' }}
          style={{ marginTop: 20 }}
        />
      ) : (
        <EmptyTable title="Nenhuma nota fiscal encontrada" />
      )}
    </Container>
  );
};

NotasFiscaisTransporteView.propTypes = {
  data: PropTypes.array,
};

NotasFiscaisTransporteView.defaultProps = {
  data: [],
};

export default NotasFiscaisTransporteView;
