import { useState, useEffect } from 'react';
import { Button, Checkbox, Input, notification, Table } from 'antd';

import { usePisCofinsContext } from '@/contexts/PisCofinsContext';
import useUpdatePisCofinsProducts from '@/services/api/hooks/app/PisCofins/useUpdatePisCofinsProducts';

import { pisCofinsProdutosMonofasicoColumns } from '@/pages/app/PisCofins/constants';

import { Title } from '@/styles/global';

const Products = () => {
  const [description, setDescription] = useState('');
  const [results, setResults] = useState([]);

  const {
    state: { filteredProducts, activeNcm },
    changeProduct,
    changeAllProducts,
    confirmModification,
  } = usePisCofinsContext();

  const { mutate, isLoading } = useUpdatePisCofinsProducts();

  useEffect(() => {
    setResults(filteredProducts);
  }, [filteredProducts]);

  const handleSearch = e => {
    const { value } = e.target;
    setDescription(value);

    if (!value) {
      setResults(filteredProducts);
      return;
    }

    setResults(
      filteredProducts.filter(item =>
        item.descProduto.toLowerCase().includes(value.toLowerCase()),
      ),
    );
  };

  const handleProductChange = (e, product) => {
    const { id: productId } = product;
    const { checked } = e.target;

    changeProduct(filteredProducts, productId, 'ncmMono', checked);
  };

  const handleAllProductsChange = e => {
    const { checked } = e.target;

    changeAllProducts(filteredProducts, 'ncmMono', checked);
  };

  const handleModification = async (product = null) => {
    const data = {
      ListProdutos: product ? [product] : filteredProducts,
    };

    mutate(data, {
      onSuccess: () => {
        confirmModification(filteredProducts, data);

        notification.success({
          message: 'Sucesso',
          description: 'Modificação confirmado com sucesso!',
        });
      },
      onError: () => {
        notification.error({
          message: 'Erro',
          description: 'Não foi possível modificar o produto no momento.',
        });
      },
    });
  };

  const productsColumns = [
    ...pisCofinsProdutosMonofasicoColumns,
    {
      title: 'Monofásico',
      dataIndex: 'ncmMono',
      key: 'ncmMono',
      align: 'center',
      render: (text, record) => (
        <Checkbox
          checked={text}
          onChange={e => handleProductChange(e, record)}
        />
      ),
    },
    {
      title: 'Ação',
      dataIndex: 'action',
      key: 'action',
      align: 'center',
      render: (text, record) => (
        <Button
          type="button"
          onClick={() => handleModification(record)}
          size="small"
          disabled={isLoading}
        >
          Confirmar
        </Button>
      ),
    },
  ];

  return (
    <>
      <Title>
        <h2>Produtos com NCM {activeNcm}</h2>
        <p>Esses são seus produtos filtrados pelo NCM escolhido.</p>
      </Title>

      <Input
        value={description}
        onChange={handleSearch}
        placeholder="Descrição do Produto"
        style={{ marginTop: 14 }}
      />

      <Checkbox onChange={handleAllProductsChange} style={{ margin: '20px 0' }}>
        Marcar todos os produtos
      </Checkbox>

      <Table
        columns={productsColumns}
        dataSource={results}
        pagination={{
          defaultPageSize: 5,
        }}
        locale={{ emptyText: 'Nenhum resultado' }}
        rowKey="id"
        size="small"
        scroll={{ x: 'max-content' }}
      />
      <Button
        type="primary"
        className="pull-right mt-2"
        onClick={() => handleModification()}
        disabled={isLoading}
      >
        Confirmar todos
      </Button>
    </>
  );
};

export default Products;
