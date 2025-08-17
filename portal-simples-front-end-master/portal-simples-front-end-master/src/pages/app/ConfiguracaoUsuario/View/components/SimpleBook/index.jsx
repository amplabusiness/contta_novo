import PropTypes from 'prop-types';
import { Button, Col, Form as AntForm, Row } from 'antd';
import { parseISO } from 'date-fns';

import { useConfiguracaoUsuarioContext } from '@/contexts/ConfiguracaoUsuarioContext';

import Form from '@/components/Form';
import { DatePickerInput } from '@/components/Form/Input';

import { Title } from '@/styles/global';
import { Content } from '@/pages/app/ConfiguracaoUsuario/View/styles';

const { Item: FormItem } = AntForm;

const SimpleBook = ({ onSubmit, isLoading }) => {
  const {
    state: { books },
  } = useConfiguracaoUsuarioContext();
  const { simple } = books;
  const closingDate = simple.dataFechamento ?? null;

  return (
    <Col xs={24} md={12} style={{ marginTop: 20 }}>
      <Title>
        <h2>Fechamento do Simples Nacional</h2>
        <p>
          O campo abaixo informa a data de fechamento do Simples Nacional da
          empresa ativa.
        </p>
      </Title>
      <Content>
        <Form
          name="simple-book-form"
          initialValues={{
            simplesDate: closingDate ? parseISO(closingDate) : '',
          }}
          onFinish={onSubmit('simples')}
        >
          <Row gutter={[24, 0]}>
            <Col xs={24} md={8}>
              <FormItem
                name="simplesDate"
                label="Data de Fechamento"
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

            <Col
              xs={24}
              md={6}
              style={{
                marginTop: 16,
                display: 'flex',
                alignItems: 'center',
              }}
            >
              <FormItem noStyle>
                <Button type="primary" htmlType="submit" disabled={isLoading}>
                  Confirmar
                </Button>
              </FormItem>
            </Col>
          </Row>
        </Form>
      </Content>
    </Col>
  );
};

SimpleBook.propTypes = {
  onSubmit: PropTypes.func.isRequired,
  isLoading: PropTypes.bool.isRequired,
};

export default SimpleBook;
