import { forwardRef, useState } from 'react';
import PropTypes from 'prop-types';
import {
  Button,
  Col,
  Form as AntForm,
  Input,
  notification,
  Popconfirm,
  Radio,
  Row,
  Select,
  Spin,
} from 'antd';
import { useSelector } from 'react-redux';

import useUpdateProduct from '@/services/api/hooks/app/Produtos/useUpdateProduct';
import useDebounce from '@/hooks/useDebounce';
import { getProductsByFilter } from '@/services/api/requests';

import Form from '@/components/Form';

import { Container, Info, CancelButton } from './styles';

const { Item: FormItem } = AntForm;

const ProductForm = forwardRef(
  ({ activeProduct = null, setActiveProduct }, ref) => {
    const { id } = useSelector(state => state.activeCompanyState);

    const [form] = AntForm.useForm();

    const [isFetchingProducts, setIsFetchingProducts] = useState(false);
    const [productsOptions, setProductsOptions] = useState([]);

    const mutation = useUpdateProduct();

    const [debouncedSearch] = useDebounce(500);

    const handleProductSearch = searchTerm => {
      debouncedSearch(async () => {
        try {
          setIsFetchingProducts(true);

          const searchOption = form.getFieldValue('searchOption');

          let searchQuery = '';

          if (searchOption === 'code') {
            searchQuery = `CodProd=${searchTerm}`;
          } else {
            searchQuery = `DescProd=${searchTerm.toUpperCase()}`;
          }

          const data = await getProductsByFilter(id, searchQuery);

          if (data.length === 0) {
            throw new Error();
          }

          const formattedData = data.map(item => ({
            key: item.id,
            label: item.descricao,
            value: item.codProd,
          }));

          setProductsOptions(formattedData);
        } catch (error) {
          notification.error({
            message: 'Erro',
            description: 'Nenhum produto encontrado.',
          });

          setProductsOptions([]);
        } finally {
          setIsFetchingProducts(false);
        }
      });
    };

    const onSubmit = values => {
      const data = {
        empresaId: id,
        depara: true,
        codProFornecedor: activeProduct.codProduto,
        codProCliente: values.code,
        marca: values.brand,
      };

      mutation.mutate(data, {
        onSuccess: () => {
          form.resetFields();

          setActiveProduct(null);
        },
      });
    };

    return (
      <div ref={ref}>
        {activeProduct && (
          <Container>
            <Info>
              <h2>Produto selecionado</h2>
              <p>{activeProduct.descProduto}</p>
            </Info>

            <Form
              name="product-form"
              form={form}
              initialValues={{
                searchOption: 'code',
                searchTerm: '',
                code: '',
                description: '',
                brand: '',
              }}
              onValuesChange={changedValues => {
                const wasSearchOptionChanged =
                  Object.keys(changedValues).includes('searchOption');

                if (wasSearchOptionChanged) {
                  setProductsOptions([]);
                }
              }}
              onFinish={onSubmit}
            >
              <Row gutter={[24, 0]}>
                <Col xs={24} lg={6}>
                  <FormItem name="searchOption" label="Pesquisar produto por">
                    <Radio.Group>
                      <Radio value="code">Código</Radio>
                      <Radio value="description">Descrição</Radio>
                    </Radio.Group>
                  </FormItem>
                </Col>

                <Col xs={24} lg={10}>
                  <FormItem name="searchTerm" label="Termo de pesquisa">
                    <Select
                      onSearch={value => {
                        if (value) {
                          handleProductSearch(value);
                        }
                      }}
                      onSelect={(_, opt) => {
                        const { value, children } = opt;

                        form.setFields([
                          {
                            name: 'code',
                            value,
                          },
                          {
                            name: 'description',
                            value: children,
                          },
                          {
                            name: 'searchTerm',
                            value: '',
                          },
                        ]);

                        setProductsOptions([]);
                      }}
                      showSearch
                      defaultActiveFirstOption={false}
                      showArrow={false}
                      filterOption={false}
                      notFoundContent={
                        isFetchingProducts ? <Spin size="small" /> : null
                      }
                    >
                      {productsOptions.map(item => (
                        <Select.Option key={item.key} value={item.value}>
                          {item.label}
                        </Select.Option>
                      ))}
                    </Select>
                  </FormItem>
                </Col>
              </Row>

              <Row gutter={[24, 0]}>
                <Col xs={24} md={4}>
                  <FormItem
                    name="code"
                    label="Código"
                    rules={[{ required: true, message: 'Campo obrigatório' }]}
                  >
                    <Input disabled />
                  </FormItem>
                </Col>

                <Col xs={24} md={10}>
                  <FormItem
                    name="description"
                    label="Descrição"
                    rules={[{ required: true, message: 'Campo obrigatório' }]}
                  >
                    <Input disabled />
                  </FormItem>
                </Col>

                <Col xs={24} md={4}>
                  <FormItem name="brand" label="Marca">
                    <Input />
                  </FormItem>
                </Col>

                <Col
                  xs={24}
                  lg={6}
                  style={{
                    marginTop: 20,
                    display: 'flex',
                    alignItems: 'center',
                  }}
                >
                  <Button
                    htmlType="submit"
                    type="primary"
                    loading={mutation.isLoading}
                    disabled={isFetchingProducts}
                  >
                    Confirmar
                  </Button>
                  <Popconfirm
                    title={<span>Tem certeza que deseja cancelar?</span>}
                    onConfirm={() => setActiveProduct(null)}
                  >
                    <CancelButton
                      htmlType="button"
                      type="primary"
                      disabled={isFetchingProducts || mutation.isLoading}
                    >
                      Cancelar
                    </CancelButton>
                  </Popconfirm>
                </Col>
              </Row>
            </Form>
          </Container>
        )}
      </div>
    );
  },
);

ProductForm.propTypes = {
  activeProduct: PropTypes.object,
  setActiveProduct: PropTypes.func.isRequired,
};

ProductForm.defaultProps = {
  activeProduct: null,
};

export default ProductForm;
