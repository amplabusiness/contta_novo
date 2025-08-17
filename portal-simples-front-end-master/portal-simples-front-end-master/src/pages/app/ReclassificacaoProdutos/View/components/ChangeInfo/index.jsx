import { useEffect, useState } from 'react';
import PropTypes from 'prop-types';
import {
  Button,
  Col,
  Form as AntForm,
  notification,
  Radio,
  Row,
  Select,
  Spin,
  Table,
} from 'antd';
import queryString from 'query-string';
import { FaWrench } from 'react-icons/fa';
import { useLocation } from 'react-router-dom';

import useDebounce from '@/hooks/useDebounce';
import useReclassifyProduct from '@/services/api/hooks/app/ReclassificacaoProdutos/useReclassifyProduct';
import { reclassificacaoProdutosColumns } from '@/pages/app/ReclassificacaoProdutos/constants';

import Form from '@/components/Form';

import { Title } from '@/styles/global';

const { Item: FormItem } = AntForm;

const ChangeInfo = ({ products }) => {
  const [totalProducts, setTotalProducts] = useState([]);
  const [productsToBeChanged, setProductsToBeChanged] = useState([]);
  const [options, setOptions] = useState([]);

  const [isFetchingOptions, setIsFetchingOptions] = useState(false);

  const [form] = AntForm.useForm();

  const { search } = useLocation();
  const { operacao } = queryString.parse(search);

  const { mutate, isLoading } = useReclassifyProduct(operacao);

  const [debouncedSearch] = useDebounce(500);

  useEffect(() => {
    setTotalProducts(products);
  }, [products]);

  const handleSearch = value => {
    debouncedSearch(() => {
      const selectedOption = form.getFieldValue('option');

      setIsFetchingOptions(true);

      fetch(
  `${process.env.REACT_APP_SEARCH_API_BASE_URL || 'https://contta-search-api.herokuapp.com/api'}/${selectedOption}?q=${value}`,
      )
        .then(response => response.json())
        .then(data => setOptions(data))
        .catch(err => {
          console.log(err);
        })
        .finally(() => setIsFetchingOptions(false));
    });
  };

  const changeProducts = values => {
    const { option, newValue } = values;

    const newValueWithOnlyNumbers = newValue.replace(/\D/g, '');
    const optionToBeChangedName = option === 'ncm' ? 'ncmProd' : 'cfop';

    const updatedProducts = totalProducts.map(item => {
      const isProductSelectedToBeChanged = productsToBeChanged.includes(
        item.id,
      );

      if (isProductSelectedToBeChanged) {
        return {
          ...item,
          [optionToBeChangedName]:
            optionToBeChangedName === 'ncmProd'
              ? newValueWithOnlyNumbers
              : Number(newValueWithOnlyNumbers),
        };
      }

      return item;
    });

    notification.success({
      message: 'Sucesso',
      description: 'Produtos selecionados foram modificados com sucesso!',
    });

    setTotalProducts(updatedProducts);
  };

  const confirmUpdateProducts = () => {
    mutate(totalProducts);
  };

  return (
    <>
      <Title>
        <h2>Alteração dos Produtos</h2>
        <p>
          O primeiro passo é selecionar quais produtos da tabela abaixo você
          deseja alterar. Logo após, selecione qual informação do(s) produto(s)
          você deseja alterar, NCM ou CFOP.
        </p>
      </Title>

      <Table
        columns={reclassificacaoProdutosColumns}
        dataSource={totalProducts}
        pagination={{ pageSize: 5 }}
        size="small"
        rowKey="id"
        rowSelection={{
          type: 'checkbox',
          onChange: selectedProductsIds => {
            setProductsToBeChanged(selectedProductsIds);
          },
        }}
        scroll={{ x: 'max-content' }}
        style={{ margin: '30px 0' }}
      />

      <Form
        name="reclassification-form"
        form={form}
        initialValues={{ option: 'ncm', newValue: '' }}
        onValuesChange={changedValues => {
          const wasOptionChanged =
            Object.keys(changedValues).includes('option');

          if (wasOptionChanged) {
            setOptions([]);
            form.setFieldsValue({
              newValue: '',
            });
          }
        }}
        onFinish={changeProducts}
      >
        <Row gutter={[24, 0]} align="middle" justify="center">
          <FormItem name="option">
            <Radio.Group>
              <Radio value="ncm">NCM</Radio>
              <Radio value="cfop">CFOP</Radio>
            </Radio.Group>
          </FormItem>
        </Row>

        <Row gutter={[24, 0]} align="middle" justify="center">
          <Col xs={24} md={6}>
            <FormItem
              shouldUpdate={(prevValues, currValues) =>
                prevValues.option !== currValues.option
              }
              noStyle
            >
              {({ getFieldValue }) => {
                const selectedOption = getFieldValue('option');

                return (
                  <FormItem
                    name="newValue"
                    label={`Novo ${selectedOption.toUpperCase()}`}
                  >
                    <Select
                      onSearch={value => {
                        if (value) {
                          handleSearch(value);
                        }
                      }}
                      showSearch
                      defaultActiveFirstOption={false}
                      showArrow={false}
                      filterOption={false}
                      notFoundContent={
                        isFetchingOptions ? <Spin size="small" /> : null
                      }
                      listHeight={128}
                    >
                      {options.map(item => (
                        <Select.Option key={item.key} value={item.value}>
                          {item.label}
                        </Select.Option>
                      ))}
                    </Select>
                  </FormItem>
                );
              }}
            </FormItem>
          </Col>
        </Row>

        <Row gutter={[24, 0]} align="middle" justify="center">
          <Col xs={24} md={6}>
            <FormItem
              shouldUpdate={(prevValues, currValues) =>
                prevValues.newValue !== currValues.newValue
              }
              noStyle
            >
              {({ getFieldValue }) => {
                const isNewValueFieldEmpty = getFieldValue('newValue') === '';

                return (
                  <Button
                    type="primary"
                    htmlType="submit"
                    icon={
                      <FaWrench
                        size={14}
                        color="#fff"
                        style={{ marginRight: 8 }}
                      />
                    }
                    disabled={isNewValueFieldEmpty || isLoading}
                    style={{ width: '100%' }}
                  >
                    Modificar
                  </Button>
                );
              }}
            </FormItem>
          </Col>
        </Row>

        <Row gutter={[24, 0]} align="middle" justify="center">
          <Col xs={24} md={6}>
            <FormItem
              shouldUpdate={(prevValues, currValues) =>
                prevValues.newValue !== currValues.newValue
              }
              noStyle
            >
              {({ getFieldValue }) => {
                const isNewValueFieldEmpty = getFieldValue('newValue') === '';

                return (
                  <Button
                    type="primary"
                    htmlType="button"
                    onClick={confirmUpdateProducts}
                    loading={isLoading}
                    disabled={isNewValueFieldEmpty}
                    style={{ marginTop: 20, width: '100%' }}
                  >
                    Confirmar alterações
                  </Button>
                );
              }}
            </FormItem>
          </Col>
        </Row>
      </Form>
    </>
  );
};

ChangeInfo.propTypes = {
  products: PropTypes.array.isRequired,
};

export default ChangeInfo;
