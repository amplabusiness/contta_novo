import React, { Component } from 'react';
import { withFormik } from 'formik';
import { Button } from 'antd';

import { Row, Form } from '../../components/bootstrap';
import { InputText, InputPassword } from '../../components/form';
import { LoginSchema } from '../../util/validations';

//redux
import { connect } from 'react-redux';
import { auth } from '../../actions/loginActions';

class InnerForm extends Component {
  constructor(props) {
    super(props);
    this.handleChange = this.handleChange.bind(this);
  }

  handleChange(name, value) {
    this.props.setFieldValue(name, value);
  }

  render() {
    const { values, errors, isSubmitting, handleSubmit, handleChange } = this.props;

    return (
      <Form handleSubmit={handleSubmit} className="m-t">
        <Row>
          <InputText
            name="email"
            handleChange={handleChange}
            col={12}
            required
            error={errors.email}
            value={values.email}
            label="E-mail"
          />
        </Row>
        <Row>
          <InputPassword
            name="password"
            handleChange={handleChange}
            col={12}
            required
            error={errors.password}
            value={values.password}
            label="Senha"
          />
        </Row>
        <Button htmlType="submit" className="col-md-12" type="primary" loading={isSubmitting}>
          Entrar
        </Button>
      </Form>
    );
  }
}

const LoginForm = withFormik({
  validateOnChange: false,
  mapPropsToValues: ({ email, password }) => ({
    email: email || '',
    password: password || '',
  }),
  validationSchema: LoginSchema,
  handleSubmit(values, { props, setSubmitting }) {
    props.loginActions(values, setSubmitting);
  },
})(InnerForm);

const mapDispatchToProps = { loginActions: auth };

export default connect(null, mapDispatchToProps)(LoginForm);
