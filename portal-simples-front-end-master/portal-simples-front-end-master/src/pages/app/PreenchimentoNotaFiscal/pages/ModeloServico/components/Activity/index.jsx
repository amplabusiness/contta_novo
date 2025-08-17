import PropTypes from 'prop-types';
import { Col, Form, Input, Select, Row } from 'antd';

import useDescriptionCodes from '@/services/api/hooks/app/PreenchimentoNotaFiscal/useDescriptionCodes';

import Shimmer from '@/components/Shimmer/PreenchimentoNotaFiscal/Atividade';

const { Item: FormItem } = Form;

const Activity = ({ form }) => {
  const { isLoading, data } = useDescriptionCodes();

  if (isLoading) {
    return <Shimmer />;
  }

  const codeOptions = data.map(item => ({
    key: String(item.descservico),
    value: String(item.codservico),
    label: String(item.codservico),
  }));

  return (
    <Row gutter={[24, 0]}>
      <Col xs={24} md={4}>
        <FormItem
          name="code"
          label="Código"
          rules={[
            {
              required: true,
              message: 'Campo obrigatório',
            },
          ]}
        >
          <Select
            onSelect={value => {
              const { key } = codeOptions.find(item => item.value === value);

              form.setFieldsValue({
                description: key,
              });
            }}
            showSearch
            filterOption={(input, option) =>
              option.children.toLowerCase().indexOf(input.toLowerCase()) >= 0
            }
            showArrow={false}
            listHeight={128}
          >
            {codeOptions.map(item => (
              <Select.Option key={item.key} value={item.value}>
                {item.label}
              </Select.Option>
            ))}
          </Select>
        </FormItem>
      </Col>

      <Col xs={24} md={20}>
        <FormItem
          name="description"
          label="Descrição"
          rules={[{ required: true, message: 'Campo obrigatório' }]}
        >
          <Input disabled />
        </FormItem>
      </Col>
    </Row>
  );
};

Activity.propTypes = {
  form: PropTypes.object.isRequired,
};

export default Activity;
