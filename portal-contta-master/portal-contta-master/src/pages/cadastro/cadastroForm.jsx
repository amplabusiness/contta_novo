import React, { forwardRef } from 'react';
import { Formik, Field, ErrorMessage } from 'formik';
import { Row, Column, Br } from '../../components/bootstrap';
import { UsuarioSchema } from '../../util/validations';
import { Button } from 'antd';

const CadastroForm = forwardRef((props, ref) => (
  <Formik
    onSubmit={props.onSubmit}
    initialValues={{ name: '', email: '', password: '', document: '' }}
    ref={ref}
    validationSchema={UsuarioSchema}>
    {({ handleSubmit }) => (
      <form onSubmit={handleSubmit}>
        <div className="form-group">
          <Row>
            <Column col={6}>
              <label htmlFor="name">* Nome</label>
              <Field
                name="name"
                type="text"
                placeholder="Informe o Nome"
                className="form-control"
              />
              <ErrorMessage className="validation-errors pt-2" component="div" name="name" />
            </Column>

            <Column col={6}>
              <label htmlFor="email">* E-mail</label>
              <Field
                name="email"
                type="email"
                placeholder="Informe o e-mail"
                className="form-control"
              />
              <ErrorMessage className="validation-errors pt-2" component="div" name="email" />
            </Column>
          </Row>
          <Br />
          <Row>
            <Column col={6}>
              <label htmlFor="password">* Senha</label>
              <Field
                name="password"
                type="password"
                placeholder="Digite sua senha"
                className="form-control"
              />
              <ErrorMessage className="validation-errors pt-2" component="div" name="password" />
            </Column>
            <Column col={6}>
              <label htmlFor="document">* Inscrição:</label>
              <Field
                name="document"
                type="text"
                placeholder="Digite o número da inscrição"
                className="form-control"
              />
              <ErrorMessage className="validation-errors pt-2" component="div" name="document" />
            </Column>
          </Row>
          <Br />
          <Row>
            <Column col={12}>
              <Button
                htmlType="submit"
                type="button"
                className="btn btn-primary pull-right"
                loading={props.loading}>
                Salvar
              </Button>
            </Column>
          </Row>
        </div>
      </form>
    )}
  </Formik>
));

export default CadastroForm;
