import { Fragment, useState } from 'react';
import PropTypes from 'prop-types';
import {
  Button,
  Col,
  Divider,
  Form,
  Input,
  notification,
  Popconfirm,
  Select,
  Spin,
  Row,
  Checkbox,
  Radio,
  InputNumber,
} from 'antd';
import { FiPlus } from 'react-icons/fi';
import { useSelector } from 'react-redux';

import useDebounce from '@/hooks/useDebounce';
import { getProductsByFilter } from '@/services/api/requests';
import {
  unitTypes,
  cstOptions,
  icmsTaxationOptions,
} from '@/pages/app/PreenchimentoNotaFiscal/constants';

import { CurrencyInput, PercentageInput } from '@/components/Form/Input';

import { ProductTitle } from './styles';

const { Item: FormItem, List: FormList } = Form;

const ProductsDetails = ({ form }) => {
  const { id } = useSelector(state => state.activeCompanyState);

  const [isFetchingProducts, setIsFetchingProducts] = useState(false);
  const [isFetchingNcms, setIsFetchingNcms] = useState(false);
  const [isFetchingCfops, setIsFetchingCfops] = useState(false);

  const [ncmOptions, setNcmOptions] = useState([]);
  const [cfopOptions, setCfopOptions] = useState([]);
  const [productsOptions, setProductsOptions] = useState([]);

  const [units, setUnits] = useState(() => unitTypes);
  const [newUnit, setNewUnit] = useState('');

  const [debouncedSearch] = useDebounce(500);

  const handleAddNewUnit = () => {
    const upperCasedUnit = newUnit.toUpperCase();
    const newData = {
      key: upperCasedUnit,
      label: upperCasedUnit,
      value: upperCasedUnit,
    };

    const sortedUnits = [...units, newData].sort((a, b) =>
      a.value.localeCompare(b.value),
    );

    setUnits(sortedUnits);

    setNewUnit('');
  };

  const handleProductSearch = (index, searchTerm) => {
    debouncedSearch(async () => {
      try {
        setIsFetchingProducts(true);

        const searchOption = form.getFieldValue([
          'products',
          index,
          'searchOption',
        ]);

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

  const handleNcmSearch = searchTerm => {
    debouncedSearch(() => {
      setIsFetchingNcms(true);

  fetch(`${process.env.REACT_APP_SEARCH_API_BASE_URL || 'https://contta-search-api.herokuapp.com/api'}/ncm?q=${searchTerm}`)
        .then(response => response.json())
        .then(data => setNcmOptions(data))
        .catch(err => {
          notification.error({
            message: 'Erro',
            description: 'NCM não encontrado',
          });
        })
        .finally(() => {
          setIsFetchingNcms(false);
        });
    });
  };

  const handleCfopSearch = searchTerm => {
    debouncedSearch(() => {
      setIsFetchingCfops(true);

  fetch(`${process.env.REACT_APP_SEARCH_API_BASE_URL || 'https://contta-search-api.herokuapp.com/api'}/cfop?q=${searchTerm}`)
        .then(response => response.json())
        .then(data => setCfopOptions(data))
        .catch(err => {
          notification.error({
            message: 'Erro',
            description: 'CFOP não encontrado',
          });
        })
        .finally(() => {
          setIsFetchingCfops(false);
        });
    });
  };

  const updateTotalProductValue = fieldIndex => {
    const isBothFieldsTouched = form.isFieldsTouched([
      ['products', fieldIndex, 'unitValue'],
      ['products', fieldIndex, 'totalValue'],
    ]);

    if (isBothFieldsTouched) {
      const values = form.getFieldValue(['products', fieldIndex]);
      const totalValue = values.quantity * values.unitValue;

      form.setFields([
        {
          name: ['products', fieldIndex, 'totalValue'],
          value: totalValue,
        },
      ]);
    }
  };

  return (
    <FormList name="products">
      {(fields, { add, remove }) => (
        <>
          {fields.map((field, index) => (
            <Fragment key={index}>
              <ProductTitle>{`${index + 1}º`} Produto</ProductTitle>

              <Row gutter={[24, 0]}>
                <Col xs={24}>
                  <FormItem name={[index, 'isNew']} valuePropName="checked">
                    <Checkbox>Esse é um novo produto</Checkbox>
                  </FormItem>
                </Col>
              </Row>

              <FormItem
                shouldUpdate={(prevValues, currValues) => {
                  const prevProducts = prevValues.products[index];
                  const currProducts = currValues.products[index];

                  if (prevProducts && currProducts) {
                    return prevProducts.isNew !== currProducts.isNew;
                  }

                  return false;
                }}
                noStyle
              >
                {({ getFieldValue }) => {
                  const isNewProduct = getFieldValue([
                    'products',
                    index,
                    'isNew',
                  ]);

                  if (isNewProduct) {
                    return null;
                  }

                  return (
                    <Row gutter={[24, 0]}>
                      <Col xs={24} lg={6}>
                        <FormItem
                          name={[index, 'searchOption']}
                          label="Pesquisar produto por"
                        >
                          <Radio.Group
                            onChange={() => {
                              form.setFields([
                                {
                                  name: ['products', index, 'searchTerm'],
                                  value: '',
                                },
                              ]);

                              setProductsOptions([]);
                            }}
                            disabled={isNewProduct}
                          >
                            <Radio value="code">Código</Radio>
                            <Radio value="description">Descrição</Radio>
                          </Radio.Group>
                        </FormItem>
                      </Col>

                      <Col xs={24} lg={10}>
                        <FormItem
                          name={[index, 'searchTerm']}
                          label="Termo de pesquisa"
                        >
                          <Select
                            onSearch={value => {
                              if (value) {
                                handleProductSearch(index, value);
                              }
                            }}
                            onSelect={(_, opt) => {
                              const { value, children } = opt;

                              form.setFields([
                                {
                                  name: ['products', index, 'code'],
                                  value,
                                },
                                {
                                  name: ['products', index, 'description'],
                                  value: children,
                                },
                                {
                                  name: ['products', index, 'searchTerm'],
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
                  );
                }}
              </FormItem>

              <Row gutter={[24, 0]}>
                <Col xs={24} md={4}>
                  <FormItem
                    shouldUpdate={(prevValues, currValues) => {
                      const prevProducts = prevValues.products[index];
                      const currProducts = currValues.products[index];

                      if (prevProducts && currProducts) {
                        return prevProducts.isNew !== currProducts.isNew;
                      }

                      return false;
                    }}
                    noStyle
                  >
                    {({ getFieldValue }) => {
                      const isNewProduct = getFieldValue([
                        'products',
                        index,
                        'isNew',
                      ]);

                      return (
                        <FormItem
                          name={[index, 'code']}
                          label="Código"
                          rules={[
                            {
                              required: true,
                              message: 'Campo obrigatório',
                            },
                          ]}
                        >
                          <Input disabled={!isNewProduct} />
                        </FormItem>
                      );
                    }}
                  </FormItem>
                </Col>

                <Col xs={24} md={14}>
                  <FormItem
                    shouldUpdate={(prevValues, currValues) => {
                      const prevProducts = prevValues.products[index];
                      const currProducts = currValues.products[index];

                      if (prevProducts && currProducts) {
                        return prevProducts.isNew !== currProducts.isNew;
                      }

                      return false;
                    }}
                    noStyle
                  >
                    {({ getFieldValue }) => {
                      const isNewProduct = getFieldValue([
                        'products',
                        index,
                        'isNew',
                      ]);

                      return (
                        <FormItem
                          name={[index, 'description']}
                          label="Descrição do Produto/Serviço"
                          rules={[
                            {
                              required: true,
                              message: 'Campo obrigatório',
                            },
                          ]}
                        >
                          <Input disabled={!isNewProduct} />
                        </FormItem>
                      );
                    }}
                  </FormItem>
                </Col>

                <Col xs={24} md={6}>
                  <FormItem
                    name={[index, 'ncmSh']}
                    label="NCM/SH"
                    rules={[
                      {
                        required: true,
                        message: 'Campo obrigatório',
                      },
                    ]}
                  >
                    <Select
                      onSearch={value => {
                        if (value) {
                          handleNcmSearch(value);
                        }
                      }}
                      showSearch
                      defaultActiveFirstOption={false}
                      showArrow={false}
                      filterOption={false}
                      notFoundContent={
                        isFetchingNcms ? <Spin size="small" /> : null
                      }
                    >
                      {ncmOptions.map(item => (
                        <Select.Option key={item.key} value={item.value}>
                          {item.label}
                        </Select.Option>
                      ))}
                    </Select>
                  </FormItem>
                </Col>

                <Col xs={24} md={2}>
                  <FormItem name={[index, 'cst']} label="CST">
                    <Select
                      showSearch
                      filterOption={(input, option) =>
                        option.value
                          .toLowerCase()
                          .startsWith(input.toLowerCase())
                      }
                    >
                      {cstOptions.map(item => (
                        <Select.Option key={item.key} value={item.value}>
                          {item.label}
                        </Select.Option>
                      ))}
                    </Select>
                  </FormItem>
                </Col>

                <Col xs={24} md={2}>
                  <FormItem name={[index, 'icmsTaxation']} label="CST/ICMS">
                    <Select
                      showSearch
                      filterOption={(input, option) =>
                        option.value
                          .toLowerCase()
                          .startsWith(input.toLowerCase())
                      }
                    >
                      {icmsTaxationOptions.map(item => (
                        <Select.Option key={item.key} value={item.value}>
                          {item.label}
                        </Select.Option>
                      ))}
                    </Select>
                  </FormItem>
                </Col>

                <Col xs={24} md={4}>
                  <FormItem
                    name={[index, 'cfop']}
                    label="CFOP"
                    rules={[
                      {
                        required: true,
                        message: 'Campo obrigatório',
                      },
                    ]}
                  >
                    <Select
                      onSearch={value => {
                        if (value) {
                          handleCfopSearch(value);
                        }
                      }}
                      showSearch
                      defaultActiveFirstOption={false}
                      showArrow={false}
                      filterOption={false}
                      notFoundContent={
                        isFetchingCfops ? <Spin size="small" /> : null
                      }
                    >
                      {cfopOptions.map(item => (
                        <Select.Option key={item.key} value={item.value}>
                          {item.label}
                        </Select.Option>
                      ))}
                    </Select>
                  </FormItem>
                </Col>

                <Col xs={24} md={4}>
                  <FormItem
                    name={[index, 'unit']}
                    label="Unidade"
                    rules={[
                      {
                        required: true,
                        message: 'Campo obrigatório',
                      },
                    ]}
                  >
                    <Select
                      showSearch
                      filterOption={(input, option) =>
                        option.children
                          .toLowerCase()
                          .indexOf(input.toLowerCase()) >= 0
                      }
                      dropdownRender={menu => (
                        <>
                          {menu}
                          <Divider />
                          <div
                            style={{
                              padding: '0 10px 10px 10px',
                              display: 'flex',
                              gap: 10,
                              alignItems: 'center',
                            }}
                          >
                            <Input
                              name="new-unit"
                              value={newUnit}
                              placeholder="Nova Unidade"
                              onChange={event => setNewUnit(event.target.value)}
                            />
                            <Button
                              type="primary"
                              htmlType="button"
                              icon={<FiPlus size={18} />}
                              onClick={handleAddNewUnit}
                            />
                          </div>
                        </>
                      )}
                    >
                      {units.map(item => (
                        <Select.Option key={item.key} value={item.value}>
                          {item.label}
                        </Select.Option>
                      ))}
                    </Select>
                  </FormItem>
                </Col>

                <Col xs={24} md={4}>
                  <FormItem
                    name={[index, 'quantity']}
                    label="Quantidade"
                    rules={[
                      {
                        required: true,
                        message: 'Campo obrigatório',
                      },
                    ]}
                  >
                    <InputNumber
                      min={0}
                      onChange={() => {
                        updateTotalProductValue(index);
                      }}
                      style={{ width: '100%' }}
                    />
                  </FormItem>
                </Col>

                <Col xs={24} md={4}>
                  <FormItem
                    name={[index, 'unitValue']}
                    label="Vlr. Unitário"
                    rules={[
                      {
                        required: true,
                        message: 'Campo obrigatório',
                      },
                    ]}
                  >
                    <CurrencyInput
                      onChange={() => {
                        updateTotalProductValue(index);
                      }}
                    />
                  </FormItem>
                </Col>

                <Col xs={24} md={4}>
                  <FormItem
                    name={[index, 'totalValue']}
                    label="Vlr. Total"
                    rules={[
                      {
                        required: true,
                        message: 'Campo obrigatório',
                      },
                    ]}
                  >
                    <CurrencyInput disabled />
                  </FormItem>
                </Col>

                <Col xs={24} md={4}>
                  <FormItem name={[index, 'bcIcms']} label="BC ICMS">
                    <CurrencyInput />
                  </FormItem>
                </Col>

                <Col xs={24} md={4}>
                  <FormItem name={[index, 'icmsValue']} label="Vlr. ICMS">
                    <CurrencyInput />
                  </FormItem>
                </Col>

                <Col xs={24} md={4}>
                  <FormItem name={[index, 'ipiValue']} label="Vlr. IPI">
                    <CurrencyInput />
                  </FormItem>
                </Col>

                <Col xs={24} md={4}>
                  <FormItem name={[index, 'icmsAliquot']} label="Alíq. ICMS">
                    <InputNumber disabled style={{ width: '100%' }} />
                  </FormItem>
                </Col>

                <Col xs={24} md={4}>
                  <FormItem name={[index, 'ipiAliquot']} label="Alíq. IPI">
                    <PercentageInput />
                  </FormItem>
                </Col>
              </Row>

              {fields.length > 1 ? (
                <Popconfirm
                  title="Tem certeza que deseja deletar esse produto?"
                  placement="rightTop"
                  onConfirm={() => remove(field.name)}
                  okText="Sim"
                >
                  <Button type="danger">Remover Produto</Button>
                </Popconfirm>
              ) : null}
            </Fragment>
          ))}

          <Row style={{ marginTop: 40 }}>
            <Button
              type="primary"
              htmlType="button"
              onClick={() => {
                setNcmOptions([]);
                setCfopOptions([]);
                setProductsOptions([]);

                add({
                  isNew: true,
                  searchOption: 'code',
                  searchTerm: '',
                  code: '',
                  description: '',
                  ncmSh: '',
                  cst: '',
                  icmsTaxation: '',
                  cfop: '',
                  unit: '',
                  quantity: 0,
                  unitValue: 0,
                  totalValue: 0,
                  bcIcms: 0,
                  icmsValue: 0,
                  ipiValue: 0,
                  icmsAliquot: '',
                  ipiAliquot: '',
                });
              }}
            >
              Adicionar produto
            </Button>
          </Row>
        </>
      )}
    </FormList>
  );
};

ProductsDetails.propTypes = {
  form: PropTypes.object.isRequired,
};

export default ProductsDetails;
