import { useState } from 'react';
import PropTypes from 'prop-types';
import {
  Button,
  Col,
  Form as AntForm,
  Input,
  notification,
  Row,
  Select,
  Spin,
} from 'antd';

import useDebounce from '@/hooks/useDebounce';

import Form from '@/components/Form';
import { DatePickerInput } from '@/components/Form/Input';

const { Item: FormItem } = AntForm;

const AntecipacaoForm = ({
  form,
  ncmsAlreadySelected,
  onSubmit,
  isLoading,
}) => {
  const [ncmOptions, setNcmOptions] = useState([]);
  const [isFetchingNcms, setIsFetchingNcms] = useState(false);

  const [debouncedSearch] = useDebounce(500);

  const handleSearch = value => {
    debouncedSearch(() => {
      setIsFetchingNcms(true);

  fetch(`${process.env.REACT_APP_SEARCH_API_BASE_URL || 'https://contta-search-api.herokuapp.com/api'}/ncm?q=${value}`)
        .then(response => response.json())
        .then(data => {
          const permittedNcms = data.filter(
            item => !ncmsAlreadySelected.includes(item.value),
          );

          setNcmOptions(permittedNcms);
        })
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

  const handleSubmit = async values => {
    setNcmOptions([]);

    await onSubmit(values);
  };

  return (
    <Form
      name="antecipacao-confirmation-form"
      form={form}
      initialValues={{
        ncm: '',
        descricao: '',
        dataInicial: '',
        dataFinal: '',
        lei: '',
      }}
      onFinish={handleSubmit}
    >
      <Row gutter={[24, 0]}>
        <Col xs={24} md={3}>
          <FormItem
            name="ncm"
            label="NCM"
            rules={[
              {
                required: true,
                message: 'Campo obrigatório',
              },
            ]}
          >
            <Select
              onSearch={handleSearch}
              onSelect={(value, { children }) => {
                const description = children.split('-')[1];

                form.setFieldsValue({ descricao: description });
              }}
              showSearch
              defaultActiveFirstOption={false}
              showArrow={false}
              filterOption={false}
              notFoundContent={isFetchingNcms ? <Spin size="small" /> : null}
            >
              {ncmOptions.map(item => (
                <Select.Option key={item.key} value={item.value}>
                  {item.label}
                </Select.Option>
              ))}
            </Select>
          </FormItem>
        </Col>

        <Col xs={24} md={4}>
          <FormItem
            name="descricao"
            label="Descrição"
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

        <Col xs={24} md={8}>
          <FormItem
            name="lei"
            label="Lei"
            rules={[
              {
                required: true,
                message: 'Campo obrigatório',
              },
            ]}
          >
            <Input />
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

AntecipacaoForm.propTypes = {
  form: PropTypes.object.isRequired,
  ncmsAlreadySelected: PropTypes.array.isRequired,
  onSubmit: PropTypes.func.isRequired,
  isLoading: PropTypes.bool.isRequired,
};

export default AntecipacaoForm;
