import { useState, useEffect } from 'react';
import { Checkbox, Button, Input, notification, Table } from 'antd';

import { useIcmsStContext } from '@/contexts/IcmsStContext';
import useRevertIcmsProducts from '@/services/api/hooks/app/IcmsSt/useRevertIcmsProducts';
import {
  icmsStProdutosIcmsColumns,
  defaultTaxesTexts,
} from '@/pages/app/IcmsSt/constants';

import { Title } from '@/styles/global';

const Products = () => {
  const [description, setDescription] = useState('');
  const [results, setResults] = useState([]);

  const {
    state: { currentTax, activeNcm, filteredProducts },
    changeProduct,
    changeAllProducts,
    confirmReversal,
  } = useIcmsStContext();

  const mutation = useRevertIcmsProducts(`${currentTax}Edit`);

  useEffect(() => {
    setResults(filteredProducts);
  }, [filteredProducts]);

  const handleSearch = e => {
    const data = e.target.value;
    setDescription(data);

    if (!data) {
      setResults(filteredProducts);
      return;
    }

    setResults(
      filteredProducts.filter(item =>
        item.descProduto.toLowerCase().includes(data.toLowerCase()),
      ),
    );
  };

  const handleProductChange = (e, product) => {
    const { id: productId } = product;
    const { checked } = e.target;

    changeProduct(filteredProducts, productId, currentTax, checked);
  };

  const handleAllProductsChange = e => {
    const { checked } = e.target;

    changeAllProducts(filteredProducts, currentTax, !checked);
  };

  const handleModification = (product = null) => {
    const productsToBeModified = product ? [product] : filteredProducts;
    const data = productsToBeModified.map(item => ({
      produtoId: item.id,
      ncmMono: item.ncmMono,
      modificado: item.modificado,
      [currentTax]: item[currentTax],
      empresaId: item.companyInformation,
      dataOperacao: new Date().toISOString(),
    }));

    mutation.mutate(data, {
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
    ...icmsStProdutosIcmsColumns,
    {
      title: () => {
        const currentTaxTitle = defaultTaxesTexts[currentTax];

        if (currentTaxTitle !== 'ICMS/ST') {
          return (
            <span style={{ textTransform: 'capitalize' }}>
              {currentTaxTitle}
            </span>
          );
        }

        return <span>{currentTaxTitle}</span>;
      },
      dataIndex: currentTax,
      key: currentTax,
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
          disabled={mutation.isLoading}
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
        onClick={() => handleModification()}
        disabled={mutation.isLoading}
      >
        Alterar todos
      </Button>
    </>
  );
};

export default Products;
