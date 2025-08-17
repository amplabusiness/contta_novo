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
  MunicipalRegistrationInput,
} from '@/components/Form/Input';

const { Item: FormItem } = Form;

const ClientDetails = ({ form }) => {
  const [isLoading, setIsLoading] = useState(false);

  const handleCompanySearch = async cnpj => {
    try {
      setIsLoading(true);

      const cnpjPattern =
        /(^\d\d.\d\d\d.\d\d\d[/]\d\d\d\d-\d\d$)|(^\d\d\d\d\d\d\d\d\d\d\d\d\d\d)/;

      if (!cnpjPattern.test(cnpj)) {
        return;
      }

      const token = getUserToken();
      const reformatedCnpj = removeNonNumericChars(cnpj);
      const { result } = await getCompanyInvoiceInfo(reformatedCnpj, token);

      const {
        razaoSocial,
        inscricaoMunicipal,
        cep,
        bairro,
        endereco,
        logradouro,
        cidade,
        uf,
      } = result;

      const address = (endereco || logradouro) ?? 'SEM INFORMAÇÃO';

      form.setFieldsValue({
        document: cnpj,
        name: razaoSocial,
        citySubscription: inscricaoMunicipal || '',
        zipCode: cep,
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
        document: cnpj,
        name: '',
        zipCode: '',
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
        zipCode: value,
        address: '',
        neighborhood: '',
        city: '',
        state: '',
      });

      setIsLoading(false);
    } else {
      const { logradouro, complemento, bairro, localidade, uf } = data;

      form.setFieldsValue({
        zipCode: value,
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
            name="document"
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
              prevValue.document !== currValue.document
            }
            noStyle
          >
            {({ getFieldValue }) => {
              const cnpjCpfValue = getFieldValue('document');
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
            shouldUpdate={(prevValue, currValue) =>
              prevValue.document !== currValue.document
            }
            noStyle
          >
            {({ getFieldValue }) => {
              const cnpjCpfValue = getFieldValue('document');
              const isCpf = cnpjCpfValue.length === 11;

              return (
                <FormItem
                  name="citySubscription"
                  label="Inscrição Municipal"
                  rules={[
                    {
                      required: !isCpf,
                      message: 'Campo obrigatório',
                    },
                  ]}
                >
                  <MunicipalRegistrationInput disabled={isCpf} />
                </FormItem>
              );
            }}
          </FormItem>
        </Col>

        <Col xs={24} md={4}>
          <FormItem
            shouldUpdate={(prevValue, currValue) =>
              prevValue.document !== currValue.document
            }
            noStyle
          >
            {({ getFieldValue }) => {
              const cnpjCpfValue = getFieldValue('document');
              const isCpf = cnpjCpfValue.length === 11;

              return (
                <FormItem
                  name="zipCode"
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

        <Col xs={24} md={8}>
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

        <Col xs={24} md={2}>
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

        <Col xs={24} md={6}>
          <FormItem
            name="neighborhood"
            label="Bairro"
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
      </Row>
    </>
  );
};

ClientDetails.propTypes = {
  form: PropTypes.object.isRequired,
};

export default ClientDetails;
