import { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import { Button, Col, Form as AntForm, Input, Row, Select } from 'antd';

import {
  exigSuspensaTaxesOptions,
  exigSuspensaSuspensionOptions,
} from '@/pages/app/IcmsSt/constants';

import Form from '@/components/Form';
import { DatePickerInput } from '@/components/Form/Input';

const { Item: FormItem } = AntForm;

const ExigSuspensaForm = ({
  form,
  taxesAlreadySelected,
  onSubmit,
  isLoading,
}) => {
  const [ufs, setUfs] = useState([]);
  const [cities, setCities] = useState([]);

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

  const fetchCities = async uf => {
    const response = await fetch(
      `https://servicodados.ibge.gov.br/api/v1/localidades/estados/${uf}/municipios`,
    );
    const data = await response.json();

    setCities(
      data.map(city => ({
        key: city.nome,
        label: city.nome,
        value: city.nome,
      })),
    );
  };

  const permittedTaxes = exigSuspensaTaxesOptions.filter(
    item => !taxesAlreadySelected.includes(item.value),
  );

  const handleSubmit = values => {
    setUfs([]);
    setCities([]);

    onSubmit(values);
  };

  return (
    <Form
      name="exig-suspensa-confirmation-form"
      form={form}
      initialValues={{
        nomeImposto: '',
        motivo: '',
        numProcesso: '',
        vara: '',
        uf: '',
        municipio: '',
        dataInicial: '',
        dataFinal: '',
      }}
      onFinish={handleSubmit}
    >
      <Row gutter={[24, 0]}>
        <Col xs={24} md={3}>
          <FormItem
            name="nomeImposto"
            label="Imposto"
            rules={[{ required: true, message: 'Campo obrigatório' }]}
          >
            <Select>
              {permittedTaxes.map(item => (
                <Select.Option key={item.key} value={item.value}>
                  {item.label}
                </Select.Option>
              ))}
            </Select>
          </FormItem>
        </Col>

        <Col xs={24} md={4}>
          <FormItem
            name="motivo"
            label="Motivo da Suspensão"
            rules={[{ required: true, message: 'Campo obrigatório' }]}
          >
            <Select>
              {exigSuspensaSuspensionOptions.map(item => (
                <Select.Option key={item.key} value={item.value}>
                  {item.label}
                </Select.Option>
              ))}
            </Select>
          </FormItem>
        </Col>

        <Col xs={24} md={4}>
          <FormItem
            name="numProcesso"
            label="Número do Processo"
            rules={[{ required: true, message: 'Campo obrigatório' }]}
          >
            <Input />
          </FormItem>
        </Col>

        <Col xs={24} md={3}>
          <FormItem
            name="vara"
            label="Vara"
            rules={[{ required: true, message: 'Campo obrigatório' }]}
          >
            <Input />
          </FormItem>
        </Col>

        <Col xs={24} md={2}>
          <FormItem
            name="uf"
            label="UF"
            rules={[{ required: true, message: 'Campo obrigatório' }]}
          >
            <Select onSelect={value => fetchCities(value)}>
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
              prevValues.uf !== currValues.uf
            }
            noStyle
          >
            {({ getFieldValue }) => {
              const hasSelectedUf = !!getFieldValue('uf');

              return (
                <FormItem
                  name="municipio"
                  label="Município"
                  rules={[{ required: true, message: 'Campo obrigatório' }]}
                >
                  <Select disabled={!hasSelectedUf}>
                    {cities.map(item => (
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

        <Col xs={24} md={3}>
          <FormItem
            name="dataInicial"
            label="Data Inicial"
            rules={[
              {
                required: true,
                message: 'Campo obrigatório',
              },
            ]}
          >
            <DatePickerInput format="DD/MM/YYYY" />
          </FormItem>
        </Col>

        <Col xs={24} md={3}>
          <FormItem
            shouldUpdate={(prevValues, currValues) =>
              prevValues.dataInicial !== currValues.dataInicial
            }
            noStyle
          >
            {({ getFieldValue }) => {
              const initialDate = getFieldValue('dataInicial');

              return (
                <FormItem name="dataFinal" label="Data Final">
                  <DatePickerInput
                    format="DD/MM/YYYY"
                    disabledDate={current => current < initialDate}
                  />
                </FormItem>
              );
            }}
          </FormItem>
        </Col>

        <Col
          xs={24}
          md={2}
          style={{
            marginTop: 20,
          }}
        >
          <FormItem noStyle>
            <Button
              type="primary"
              htmlType="submit"
              loading={isLoading}
              style={{
                marginTop: 20,
              }}
            >
              Confirmar
            </Button>
          </FormItem>
        </Col>
      </Row>
    </Form>
  );
};

ExigSuspensaForm.propTypes = {
  form: PropTypes.object.isRequired,
  taxesAlreadySelected: PropTypes.array.isRequired,
  onSubmit: PropTypes.func.isRequired,
  isLoading: PropTypes.bool.isRequired,
};

export default ExigSuspensaForm;
