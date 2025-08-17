import { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import { Col, Form, Row, Select } from 'antd';

import { CurrencyInput } from '@/components/Form/Input';

const { Item: FormItem } = Form;

const Demonstrative = ({ form }) => {
  const [ufs, setUfs] = useState([]);
  const [providedServiceCities, setProvidedServiceCities] = useState([]);
  const [taxedServiceCities, setTaxedServiceCities] = useState([]);

  useEffect(() => {
    const fetchUfs = async () => {
      const response = await fetch(
        'https://servicodados.ibge.gov.br/api/v1/localidades/estados?orderBy=nome',
      );
      const data = await response.json();

      setUfs(
        data.map(uf => ({ key: uf.sigla, label: uf.sigla, value: uf.sigla })),
      );
    };

    fetchUfs();
  }, []);

  const fetchProvidedServiceCities = async uf => {
    const response = await fetch(
      `https://servicodados.ibge.gov.br/api/v1/localidades/estados/${uf}/municipios`,
    );
    const data = await response.json();

    setProvidedServiceCities(
      data.map(city => ({
        key: city.nome,
        label: city.nome,
        value: city.nome,
      })),
    );
  };

  const fetchTaxedServiceCities = async uf => {
    const response = await fetch(
      `https://servicodados.ibge.gov.br/api/v1/localidades/estados/${uf}/municipios`,
    );
    const data = await response.json();

    setTaxedServiceCities(
      data.map(city => ({
        key: city.nome,
        label: city.nome,
        value: city.nome,
      })),
    );
  };

  const updateLiquidValue = () => {
    const servicesValue = form.getFieldValue('servicesValue');
    const discountValue = form.getFieldValue('unconditionalDiscount');
    const issqnValue = form.getFieldValue('retainedIssqn');
    const federalRetentionsValue = form.getFieldValue('federalRetentions');

    form.setFields([
      {
        name: 'liquidValue',
        value:
          servicesValue - discountValue - issqnValue - federalRetentionsValue,
      },
    ]);
  };

  return (
    <Row gutter={[24, 0]}>
      <Col xs={24} md={2}>
        <FormItem name="providedServiceUf" label="UF">
          <Select
            onSelect={value => {
              form.setFields([
                {
                  name: 'providedServiceCity',
                  value: '',
                },
              ]);

              fetchProvidedServiceCities(value);
            }}
          >
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
          shouldUpdate={(prevValues, currValues) =>
            prevValues.providedServiceUf !== currValues.providedServiceUf
          }
          noStyle
        >
          {({ getFieldValue }) => {
            const hasUf = !!getFieldValue('providedServiceUf');

            return (
              <FormItem
                name="providedServiceCity"
                label="Serviço Prestado em"
                rules={[{ required: true, message: 'Campo obrigatório' }]}
              >
                <Select disabled={!hasUf}>
                  {providedServiceCities.map(item => (
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

      <Col xs={24} md={2}>
        <FormItem name="taxedServiceUf" label="UF">
          <Select
            onSelect={value => {
              form.setFields([
                {
                  name: 'taxedServiceCity',
                  value: '',
                },
              ]);

              fetchTaxedServiceCities(value);
            }}
          >
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
          shouldUpdate={(prevValues, currValues) =>
            prevValues.taxedServiceUf !== currValues.taxedServiceUf
          }
          noStyle
        >
          {({ getFieldValue }) => {
            const hasUf = !!getFieldValue('taxedServiceUf');

            return (
              <FormItem
                name="taxedServiceCity"
                label="Imposto Devido em"
                rules={[{ required: true, message: 'Campo obrigatório' }]}
              >
                <Select disabled={!hasUf}>
                  {taxedServiceCities.map(item => (
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

      <Col xs={24} md={4}>
        <FormItem
          name="servicesValue"
          label="Valor dos Serviços"
          rules={[{ required: true, message: 'Campo obrigatório' }]}
        >
          <CurrencyInput onChange={updateLiquidValue} />
        </FormItem>
      </Col>

      <Col xs={24} md={4}>
        <FormItem
          name="unconditionalDiscount"
          label="Desconto"
          rules={[{ required: true, message: 'Campo obrigatório' }]}
        >
          <CurrencyInput onChange={updateLiquidValue} />
        </FormItem>
      </Col>

      <Col xs={24} md={4}>
        <FormItem
          name="retainedIssqn"
          label="ISSQN Retido"
          rules={[{ required: true, message: 'Campo obrigatório' }]}
        >
          <CurrencyInput onChange={updateLiquidValue} />
        </FormItem>
      </Col>

      <Col xs={24} md={4}>
        <FormItem
          name="liquidValue"
          label="Valor Líquido"
          rules={[{ required: true, message: 'Campo obrigatório' }]}
        >
          <CurrencyInput disabled />
        </FormItem>
      </Col>

      <Col xs={24} md={4}>
        <FormItem
          name="federalRetentions"
          label="Retenções Federais"
          rules={[{ required: true, message: 'Campo obrigatório' }]}
        >
          <CurrencyInput onChange={updateLiquidValue} />
        </FormItem>
      </Col>
    </Row>
  );
};

Demonstrative.propTypes = {
  form: PropTypes.object.isRequired,
};

export default Demonstrative;
