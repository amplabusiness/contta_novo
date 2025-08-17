import { useState } from 'react';
import PropTypes from 'prop-types';
import {
  Button,
  Col,
  Divider,
  Form as AntForm,
  Input,
  notification,
  Row,
  Select,
} from 'antd';
import { useSelector } from 'react-redux';

import useDebounce from '@/hooks/useDebounce';
import useAdjustmentCodes from '@/services/api/hooks/app/AjusteApuracao/useAdjustmentCodes';
import {
  typeOptions,
  totalizersOptions,
  nfTypesOptions,
  cstOptions,
} from '@/pages/app/AjusteApuracao/constants';

import Form from '@/components/Form';
import { DatePickerInput } from '@/components/Form/Input';

const { Item: FormItem } = AntForm;

const FilterForm = ({ onSubmit, isLoading }) => {
  const { id } = useSelector(state => state.activeCompanyState);

  const [form] = AntForm.useForm();

  const [ncmOptions, setNcmOptions] = useState([]);
  const [cfopOptions, setCfopOptions] = useState([]);

  const [debouncedSearch] = useDebounce(500);

  const { data: adjustmentCodes } = useAdjustmentCodes();

  const onlyAdjustmentCodes = Array.isArray(adjustmentCodes)
    ? adjustmentCodes.map(item => ({
        key: item._id,
        label: item.code,
        value: item.code,
      }))
    : [];

  const handleCfopSearch = value => {
    debouncedSearch(() => {
  fetch(`${process.env.REACT_APP_SEARCH_API_BASE_URL || 'https://contta-search-api.herokuapp.com/api'}/cfop?q=${value}`)
        .then(response => response.json())
        .then(cfops => setCfopOptions(cfops))
        .catch(err => {
          notification.error({
            message: 'Erro',
            description: 'CFOP não encontrado',
          });

          setCfopOptions([]);
        });
    });
  };

  const handleNcmSearch = value => {
    debouncedSearch(() => {
  fetch(`${process.env.REACT_APP_SEARCH_API_BASE_URL || 'https://contta-search-api.herokuapp.com/api'}/ncm?q=${value}`)
        .then(response => response.json())
        .then(ncms => setNcmOptions(ncms))
        .catch(err => {
          notification.error({
            message: 'Erro',
            description: 'NCM não encontrado',
          });

          setNcmOptions([]);
        });
    });
  };

  const onFilterFormSubmit = async values => {
    try {
      const data = {
        companyInformation: id,
        dhEmiss: values.dataEmissao,
        codAjuste: values.codigo,
        descricaoAjuste: values.descricao,
        tipo: values.tipo,
        tipoNota: values.tipoNota,
        totalizador: values.totalizador,
        cfops: values.cfops,
        ncms: values.ncms,
        csts: values.csts,
      };

      await onSubmit(data);

      form.resetFields();
    } catch (error) {
      //
    }
  };

  return (
    <Form
      name="calculation-adjustment-filter-form"
      form={form}
      initialValues={{
        codigo: '',
        descricao: '',
        tipo: '',
        tipoNota: '',
        totalizador: '',
        dataEmissao: '',
        cfops: [],
        ncms: [],
        csts: [],
      }}
      onFinish={onFilterFormSubmit}
    >
      <Divider orientation="left" style={{ margin: '20px 0 0' }}>
        Dados Básicos
      </Divider>

      <Row gutter={[24, 0]}>
        <Col xs={24} md={4}>
          <FormItem
            name="codigo"
            label="Código"
            rules={[{ required: true, message: 'Campo obrigatório' }]}
          >
            <Select
              onSelect={value => {
                const { description } = adjustmentCodes.find(
                  item => item.code === value,
                );

                form.setFieldsValue({ descricao: description });
              }}
              showSearch
              filterOption={(input, option) =>
                option.children.toLowerCase().indexOf(input.toLowerCase()) >= 0
              }
              showArrow={false}
            >
              {onlyAdjustmentCodes.map(item => (
                <Select.Option key={item.key} value={item.value}>
                  {item.label}
                </Select.Option>
              ))}
            </Select>
          </FormItem>
        </Col>

        <Col xs={24} md={20}>
          <FormItem
            name="descricao"
            label="Descrição"
            rules={[{ required: true, message: 'Campo obrigatório' }]}
          >
            <Input disabled />
          </FormItem>
        </Col>

        <Col xs={24} md={4}>
          <FormItem
            name="tipo"
            label="Tipo"
            rules={[{ required: true, message: 'Campo obrigatório' }]}
          >
            <Select>
              {typeOptions.map(item => (
                <Select.Option key={item.key} value={item.value}>
                  {item.label}
                </Select.Option>
              ))}
            </Select>
          </FormItem>
        </Col>

        <Col xs={24} md={4}>
          <FormItem
            name="tipoNota"
            label="Tipo da Nota"
            rules={[{ required: true, message: 'Campo obrigatório' }]}
          >
            <Select>
              {nfTypesOptions.map(item => (
                <Select.Option key={item.key} value={item.value}>
                  {item.label}
                </Select.Option>
              ))}
            </Select>
          </FormItem>
        </Col>

        <Col xs={24} md={4}>
          <FormItem
            name="totalizador"
            label="Totalizador"
            rules={[{ required: true, message: 'Campo obrigatório' }]}
          >
            <Select>
              {totalizersOptions.map(item => (
                <Select.Option key={item.key} value={item.value}>
                  {item.label}
                </Select.Option>
              ))}
            </Select>
          </FormItem>
        </Col>

        <Col xs={24} md={4}>
          <FormItem name="dataEmissao" label="Data de Emissão">
            <DatePickerInput format="DD/MM/YYYY" />
          </FormItem>
        </Col>
      </Row>

      <Divider orientation="left" style={{ margin: '20px 0 0' }}>
        Que Contenham os Seguintes Parâmetros
      </Divider>

      <Row gutter={[24, 0]}>
        <Col xs={24} md={8}>
          <FormItem name="cfops" label="CFOP">
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
              notFoundContent={null}
              mode="tags"
              listHeight={128}
            >
              {cfopOptions.map(item => (
                <Select.Option key={item.key} value={item.value}>
                  {item.label}
                </Select.Option>
              ))}
            </Select>
          </FormItem>
        </Col>

        <Col xs={24} md={8}>
          <FormItem name="ncms" label="NCM">
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
              notFoundContent={null}
              mode="tags"
              listHeight={128}
            >
              {ncmOptions.map(item => (
                <Select.Option key={item.key} value={item.value}>
                  {item.label}
                </Select.Option>
              ))}
            </Select>
          </FormItem>
        </Col>

        <Col xs={24} md={8}>
          <FormItem name="csts" label="CST">
            <Select
              showSearch
              filterOption={(input, option) =>
                option.children.toLowerCase().indexOf(input.toLowerCase()) >= 0
              }
              showArrow={false}
              mode="tags"
              listHeight={128}
            >
              {cstOptions.map(item => (
                <Select.Option key={item.key} value={item.value}>
                  {item.label}
                </Select.Option>
              ))}
            </Select>
          </FormItem>
        </Col>
      </Row>

      <Row gutter={[24, 0]} align="center" justify="center">
        <Col
          xs={24}
          md={3}
          style={{
            margin: '40px 0',
          }}
        >
          <FormItem noStyle>
            <Button
              type="primary"
              htmlType="submit"
              disabled={isLoading}
              style={{
                width: '100%',
              }}
            >
              Consultar
            </Button>
          </FormItem>
        </Col>
      </Row>
    </Form>
  );
};

FilterForm.propTypes = {
  onSubmit: PropTypes.func.isRequired,
  isLoading: PropTypes.bool.isRequired,
};

export default FilterForm;
