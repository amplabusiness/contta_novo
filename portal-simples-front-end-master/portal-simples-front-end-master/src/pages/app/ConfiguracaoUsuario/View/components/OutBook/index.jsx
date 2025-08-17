import PropTypes from 'prop-types';
import { Button, Col, Form as AntForm, InputNumber, Row } from 'antd';
import { parseISO } from 'date-fns';

import { useConfiguracaoUsuarioContext } from '@/contexts/ConfiguracaoUsuarioContext';

import Form from '@/components/Form';
import { DatePickerInput } from '@/components/Form/Input';

import { Title } from '@/styles/global';
import { Content } from '@/pages/app/ConfiguracaoUsuario/View/styles';

const { Item: FormItem } = AntForm;

const OutBook = ({ onSubmit, onBookDownload, isLoading }) => {
  const {
    state: { books },
  } = useConfiguracaoUsuarioContext();
  const { outBook } = books;
  const closingDate = outBook.dataFechamento ?? null;
  const lastCode = outBook.codUltimoEnviou ?? null;

  return (
    <Col xs={24} md={12} style={{ marginTop: 20 }}>
      <Title>
        <h2>Livro Fiscal de Saída</h2>
        <p>Informações referentes ao Livro Fiscal de Saída da empresa ativa.</p>
      </Title>
      <Content>
        <Form
          name="out-book-form"
          initialValues={{
            outBookDate: closingDate ? parseISO(closingDate) : '',
            outBookLastCode: lastCode,
          }}
          onFinish={onSubmit('outBook')}
        >
          <Row gutter={[24, 0]}>
            <Col xs={24} md={8}>
              <FormItem
                name="outBookDate"
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

            <Col xs={24} md={8}>
              <FormItem name="outBookLastCode" label="Nº Último Livro Impresso">
                <InputNumber style={{ width: '100%' }} />
              </FormItem>
            </Col>
            <Col
              xs={24}
              md={8}
              style={{
                marginTop: 20,
                display: 'flex',
                gap: 20,
                alignItems: 'center',
              }}
            >
              <Button type="primary" htmlType="submit" disabled={isLoading}>
                Confirmar
              </Button>
              <Button
                type="primary"
                htmlType="button"
                onClick={() => onBookDownload('Venda')}
                disabled={isLoading}
              >
                Baixar
              </Button>
            </Col>
          </Row>
        </Form>
      </Content>
    </Col>
  );
};

OutBook.propTypes = {
  onSubmit: PropTypes.func.isRequired,
  onBookDownload: PropTypes.func.isRequired,
  isLoading: PropTypes.bool.isRequired,
};

export default OutBook;
