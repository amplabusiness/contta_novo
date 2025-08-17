import PropTypes from 'prop-types';
import { Table } from 'antd';

import { notasFiscaisServicoColumns } from '@/pages/app/NotasFiscaisServico/constants';

import EmptyTable from '@/components/EmptyTable';

import { Title } from '@/styles/global';
import { Container } from './styles';

const NotasFiscaisServicosView = ({ data = [] }) => {
  return (
    <Container>
      <Title>
        <h2>Listagem das Notas Fiscais de Serviço</h2>
        <p>
          Abaixo encontram-se todas as notas fiscais de serviço da empresa
          ativa. Use o botão de Visualizar para ver a nota em um PDF.
        </p>
      </Title>
      {data.length > 0 ? (
        <Table
          columns={notasFiscaisServicoColumns}
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
        <EmptyTable title="Nenhuma nota encontrada" />
      )}
    </Container>
  );
};

NotasFiscaisServicosView.propTypes = {
  data: PropTypes.array,
};

NotasFiscaisServicosView.defaultProps = {
  data: [],
};

export default NotasFiscaisServicosView;
