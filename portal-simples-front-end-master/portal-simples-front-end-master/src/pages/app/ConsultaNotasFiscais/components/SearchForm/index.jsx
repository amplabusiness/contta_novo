import PropTypes from 'prop-types';
import { Button, Col, Form as AntForm, Input, Row, Select } from 'antd';
import { FiSearch } from 'react-icons/fi';

import {
  tiposNotasFiscais,
  ufs,
} from '@/pages/app/ConsultaNotasFiscais/constants';

import Form from '@/components/Form';
import { CNPJCPFInput, DatePickerInput } from '@/components/Form/Input';

const { Item: FormItem } = AntForm;

const SearchForm = ({ onSubmit, isLoading }) => {
  return (
    <Form
      name="invoices-query-form"
      initialValues={{
        tipoNfe: '',
        descProduto: '',
        dhEmiss: null,
        uf: '',
        cnpj: '',
        nomeCli: '',
      }}
      onFinish={onSubmit}
    >
      <Row gutter={[24, 0]}>
        <Col xs={24} md={4}>
          <FormItem
            name="tipoNfe"
            label="Tipo da Nota"
            rules={[{ required: true, message: 'Campo obrigatório' }]}
          >
            <Select>
              {tiposNotasFiscais.map(item => (
                <Select.Option key={item.key} value={item.value}>
                  {item.label}
                </Select.Option>
              ))}
            </Select>
          </FormItem>
        </Col>

        <Col xs={24} md={4}>
          <FormItem name="descProduto" label="Nome Produto">
            <Input />
          </FormItem>
        </Col>

        <Col xs={24} md={4}>
          <FormItem name="dhEmiss" label="Data de Emissão">
            <DatePickerInput format="MM/YYYY" picker="month" />
          </FormItem>
        </Col>

        <Col xs={24} md={2}>
          <FormItem name="uf" label="UF">
            <Select>
              {ufs.map(item => (
                <Select.Option key={item.key} value={item.value}>
                  {item.label}
                </Select.Option>
              ))}
            </Select>
          </FormItem>
        </Col>

        <Col xs={24} md={4}>
          <FormItem
            name="cnpj"
            label="CPF/CNPJ"
            rules={[
              {
                pattern:
                  /(^\d\d\d\d\d\d\d\d\d\d\d\d\d\d$)|(^\d\d.\d\d\d.\d\d\d[/]\d\d\d\d[-]\d\d$)|(^\d\d\d.\d\d\d.\d\d\d[-]\d\d$)|(^\d\d\d\d\d\d\d\d\d\d\d$)/,
                message: 'CNPJ/CPF inválido',
              },
            ]}
          >
            <CNPJCPFInput />
          </FormItem>
        </Col>

        <Col xs={24} md={4}>
          <FormItem name="nomeCli" label="Nome do Cliente">
            <Input />
          </FormItem>
        </Col>

        <Col
          xs={24}
          md={2}
          style={{ marginTop: 16, display: 'flex', alignItems: 'center' }}
        >
          <FormItem noStyle>
            <Button
              type="primary"
              htmlType="submit"
              icon={<FiSearch size={16} color="#fff" />}
              loading={isLoading}
              style={{
                width: '100%',
              }}
            />
          </FormItem>
        </Col>
      </Row>
    </Form>
  );
};

SearchForm.propTypes = {
  onSubmit: PropTypes.func.isRequired,
  isLoading: PropTypes.bool.isRequired,
};

export default SearchForm;
