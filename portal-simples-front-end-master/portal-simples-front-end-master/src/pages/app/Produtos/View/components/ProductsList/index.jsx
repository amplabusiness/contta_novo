import { forwardRef } from 'react';
import PropTypes from 'prop-types';
import { Button, Col, Row, Table } from 'antd';

import { produtosColumns } from '@/pages/app/Produtos/constants';

import { Title } from '@/styles/global';
import { Container } from './styles';

const ProductsList = forwardRef(
  ({ products, activeProduct = null, setActiveProduct }, ref) => {
    const columns = [
      {
        title: '',
        dataIndex: 'action',
        key: 'action',
        align: 'center',
        width: '15%',
        render: (text, record) => (
          <Button
            type="primary"
            size="small"
            disabled={!!activeProduct}
            onClick={() => {
              setActiveProduct(record);

              setTimeout(() => {
                if (ref.current) {
                  ref.current.scrollIntoView({ behavior: 'smooth' });
                }
              }, 200);
            }}
          >
            Selecionar
          </Button>
        ),
      },
      ...produtosColumns,
    ];

    return (
      <Container>
        <Title>
          <h2>Depara de Produtos</h2>
          <p>
            Abaixo encontram-se todos os produtos da empresa que necessitam de
            uma operação de depara. Clique no botão Selecionar para modificar as
            informações de um produto específico.
          </p>
        </Title>
        <Row align="middle" justify="center">
          <Col xs={24} md={20}>
            <Table
              columns={columns}
              dataSource={products}
              pagination={{
                pageSize: 5,
                showSizeChanger: false,
              }}
              size="small"
              rowKey="id"
              rowClassName={(record, index) => {
                if (activeProduct) {
                  return activeProduct.id === record.id ? '' : 'disabled-row';
                }

                return '';
              }}
              scroll={{ x: 'max-content' }}
              style={{ marginTop: 30, minHeight: 298 }}
            />
          </Col>
        </Row>
      </Container>
    );
  },
);

ProductsList.propTypes = {
  products: PropTypes.array.isRequired,
  activeProduct: PropTypes.object,
  setActiveProduct: PropTypes.func.isRequired,
};

ProductsList.defaultProps = {
  activeProduct: null,
};

export default ProductsList;
