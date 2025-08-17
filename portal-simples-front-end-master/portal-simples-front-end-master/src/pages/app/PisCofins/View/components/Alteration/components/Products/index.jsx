import { useState, useEffect } from 'react';
import { Button, Checkbox, Input, notification, Table } from 'antd';

import { usePisCofinsContext } from '@/contexts/PisCofinsContext';
import useRevertPisCofinsProducts from '@/services/api/hooks/app/PisCofins/useRevertPisCofinsProducts';

import { pisCofinsProdutosMonofasicoColumns } from '@/pages/app/PisCofins/constants';

import { Title } from '@/styles/global';

const Products = () => {
  const [description, setDescription] = useState('');
  const [results, setResults] = useState([]);

  const {
    state: { filteredProducts, activeNcm },
    changeProduct,
    changeAllProducts,
    confirmReversal,
  } = usePisCofinsContext();

  const { mutate, isLoading } = useRevertPisCofinsProducts();

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

    changeAllProducts(filteredProducts, 'ncmMono', !checked);
  };

  const handleModification = (product = null) => {
    const productsToBeModified = product ? [product] : filteredProducts;
    const data = productsToBeModified.map(item => ({
      produtoId: item.id,
      icmsSt: item.icmsSt,
      modificado: item.modificado,
      ncmMono: item.ncmMono,
      empresaId: item.companyInformation,
      dataOperacao: new Date().toISOString(),
    }));

    mutate(data, {
      onSuccess: () => {
        confirmReversal(filteredProducts, productsToBeModified);

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
          Alterar
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
        Desmarcar todos os produtos
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
        style={{ marginTop: 20 }}
      />
      <Button
        type="primary"
        className="pull-right mt-2"
        onClick={() => handleModification()}
        disabled={isLoading}
      >
        Alterar todos
      </Button>
    </>
  );
};

export default Products;
