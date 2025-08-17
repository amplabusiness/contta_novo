import PropTypes from 'prop-types';
import { Table } from 'antd';
import { FaEye } from 'react-icons/fa';

import { listagemProdutosColumns } from '@/pages/app/ListagemProdutos/constants';
import { currencyFormatter } from '@/utils/formatters';

import { Container, Title } from '@/styles/global';
import { Total } from './styles';

const ListagemProdutosView = ({ data }) => {
  const nfesSum = data.reduce((acc, curr) => acc + curr.valorNfe, 0);

  const columns = [
    ...listagemProdutosColumns,
    {
      title: 'Visualizar',
      dataIndex: 'view',
      key: 'view',
      render: (text, record) => (
        <a href={record.danfe} target="_blank" rel="noopener noreferrer">
          <FaEye size={18} color="#3276b1" />
        </a>
      ),
    },
  ];

  return (
    <Container>
      <Title>
        <h2>Listagem</h2>
        <p>
          Abaixo encontra-se a listagem de produtos juntamente com seu total.
          Verifique todas as informações apresentadas na tabela.
        </p>
      </Title>
      <Table
        columns={columns}
        dataSource={data}
        pagination={{ pageSize: 5 }}
        size="small"
        rowKey="codigoProduto"
        scroll={{ x: 'max-content' }}
        style={{ marginTop: 20 }}
      />
      <Total>
        <p>Total:</p>
        <span>{currencyFormatter(nfesSum)}</span>
      </Total>
    </Container>
  );
};

ListagemProdutosView.propTypes = {
  data: PropTypes.array.isRequired,
};

export default ListagemProdutosView;
