import { useState } from 'react';
import PropTypes from 'prop-types';
import { Col, Form, Input, notification, Row } from 'antd';

import { getCompanyInvoiceInfo } from '@/services/api/requests';
import { removeNonNumericChars } from '@/utils/formatters';
import { getUserToken } from '@/utils/userToken';

import Loading from '@/components/Loading';
import {
  CEPInput,
  CNPJCPFInput,
  DatePickerInput,
  PhoneInput,
  StateRegistrationInput,
} from '@/components/Form/Input';

const { Item: FormItem } = Form;

const ClientDetails = ({ form }) => {
  const [isLoading, setIsLoading] = useState(false);

  const handleCompanySearch = async cnpj => {
    try {
      setIsLoading(true);

      const cnpjPattern =
        /(^\d\d.\d\d\d.\d\d\d[/]\d\d\d\d-\d\d$)|(^\d\d\d\d\d\d\d\d\d\d\d\d\d\d$)/;

      if (!cnpjPattern.test(cnpj)) {
        return;
      }

      const token = getUserToken();
      const reformatedCnpj = removeNonNumericChars(cnpj);
      const { result } = await getCompanyInvoiceInfo(reformatedCnpj, token);

      const {
        razaoSocial,
        incrEstadual,
        cep,
        endereco,
        logradouro,
        bairro,
        cidade,
        uf,
      } = result;

      const address = (endereco || logradouro) ?? 'SEM INFORMAÇÃO';

      form.setFieldsValue({
        cnpjCpf: cnpj,
        name: razaoSocial,
        stateSubscription: incrEstadual,
        cep,
        address,
        neighborhood: bairro,
        city: cidade,
        state: uf,
      });
    } catch (error) {
      notification.error({
        message: 'Erro',
        description: 'Empresa não encontrada.',
      });

      form.setFieldsValue({
        cnpjCpf: cnpj,
        name: '',
        cep: '',
        address: '',
        neighborhood: '',
        city: '',
        state: '',
      });
    } finally {
      setIsLoading(false);
    }
  };

  const handleCepSearch = async value => {
    setIsLoading(true);

    const response = await fetch(`https://viacep.com.br/ws/${value}/json/`);
    const data = await response.json();

    if (data.erro) {
      notification.error({
        message: 'Erro',
        description: 'Endereço não encontrado.',
      });

      form.setFieldsValue({
        cep: value,
        address: '',
        neighborhood: '',
        city: '',
        state: '',
      });

      setIsLoading(false);
    } else {
      const { logradouro, complemento, bairro, localidade, uf } = data;

      form.setFieldsValue({
        cep: value,
        address: `${logradouro} ${complemento}`,
        neighborhood: bairro,
        city: localidade,
        state: uf,
      });

      setIsLoading(false);
    }
  };

  return (
    <>
      {isLoading && <Loading />}
      <Row gutter={[24, 0]}>
        <Col xs={24} md={4}>
          <FormItem
            name="cnpjCpf"
            label="CNPJ/CPF"
            rules={[
              {
                required: true,
                message: 'Campo obrigatório',
              },
              {
                pattern:
                  /(^\d\d\d\d\d\d\d\d\d\d\d\d\d\d$)|(^\d\d\d\d\d\d\d\d\d\d\d$)/,
                message: 'CNPJ/CPF inválido',
              },
            ]}
            validateTrigger={['onBlur']}
          >
            <CNPJCPFInput
              onChange={event => {
                const { value: cnpj } = event.target;

                if (String(cnpj).length === 14) {
                  handleCompanySearch(cnpj);
                }
              }}
            />
          </FormItem>
        </Col>
        <Col xs={24} md={16}>
          <FormItem
            shouldUpdate={(prevValue, currValue) =>
              prevValue.cnpjCpf !== currValue.cnpjCpf
            }
            noStyle
          >
            {({ getFieldValue }) => {
              const cnpjCpfValue = getFieldValue('cnpjCpf');
              const isCpf = cnpjCpfValue.length === 11;

              return (
                <FormItem
                  name="name"
                  label="Nome/Razão Social"
                  rules={[
                    {
                      required: true,
                      message: 'Campo obrigatório',
                    },
                  ]}
                >
                  <Input disabled={!isCpf} />
                </FormItem>
              );
            }}
          </FormItem>
        </Col>
        <Col xs={24} md={4}>
          <FormItem
            name="stateSubscription"
            label="Inscrição Estadual"
            rules={[
              {
                required: true,
                message: 'Campo obrigatório',
              },
              {
                pattern: /(^\d\d.\d\d\d.\d\d\d-\d$)|(^\d\d\d\d\d\d\d\d\d$)/,
                message: 'Inscrição inválida',
              },
            ]}
            validateTrigger={['onBlur']}
          >
            <StateRegistrationInput />
          </FormItem>
        </Col>
        <Col xs={24} md={4}>
          <FormItem
            name="emissionDate"
            label="Data Emissão"
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
        <Col xs={24} md={4}>
          <FormItem
            name="inOutDate"
            label="Data Entrada/Saída"
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
        <Col xs={24} md={4}>
          <FormItem
            shouldUpdate={(prevValue, currValue) =>
              prevValue.cnpjCpf !== currValue.cnpjCpf
            }
            noStyle
          >
            {({ getFieldValue }) => {
              const cnpjCpfValue = getFieldValue('cnpjCpf');
              const isCpf = cnpjCpfValue.length === 11;

              return (
                <FormItem
                  name="cep"
                  label="CEP"
                  rules={[
                    {
                      required: true,
                      message: 'Campo obrigatório',
                    },
                    {
                      pattern: /(^\d\d\d\d\d-\d\d\d$)|(^\d\d\d\d\d\d\d\d$)/,
                      message: 'CEP inválido',
                    },
                  ]}
                  validateTrigger={['onBlur']}
                >
                  <CEPInput
                    onChange={event => {
                      const { value: cep } = event.target;
                      const cepOnlyNumber = cep.replace(/\D/g, '');

                      if (cepOnlyNumber.length === 8 && isCpf) {
                        handleCepSearch(cepOnlyNumber);
                      }
                    }}
                    disabled={!isCpf}
                  />
                </FormItem>
              );
            }}
          </FormItem>
        </Col>
        <Col xs={24} md={12}>
          <FormItem
            name="address"
            label="Endereço"
            rules={[
              {
                required: true,
                message: 'Campo obrigatório',
              },
            ]}
          >
            <Input disabled />
          </FormItem>
        </Col>
        <Col xs={24} md={8}>
          <FormItem
            name="neighborhood"
            label="Bairro/Distrito"
            rules={[
              {
                required: true,
                message: 'Campo obrigatório',
              },
            ]}
          >
            <Input disabled />
          </FormItem>
        </Col>
        <Col xs={24} md={8}>
          <FormItem
            name="city"
            label="Município"
            rules={[
              {
                required: true,
                message: 'Campo obrigatório',
              },
            ]}
          >
            <Input disabled />
          </FormItem>
        </Col>
        <Col xs={24} md={4}>
          <FormItem
            name="state"
            label="UF"
            rules={[
              {
                required: true,
                message: 'Campo obrigatório',
              },
            ]}
          >
            <Input disabled />
          </FormItem>
        </Col>
        <Col xs={24} md={4}>
          <FormItem
            name="phoneFax"
            label="Fone/Fax"
            rules={[
              {
                pattern:
                  /(^[(]\d\d[)] \d \d\d\d\d-\d\d\d\d$)|(^[(]\d\d[)] \d\d\d\d-\d\d\d\d$)/,
                message: 'Telefone inválido',
              },
            ]}
            validateTrigger={['onBlur']}
          >
            <PhoneInput />
          </FormItem>
        </Col>
      </Row>
    </>
  );
};

ClientDetails.propTypes = {
  form: PropTypes.object.isRequired,
};

export default ClientDetails;
