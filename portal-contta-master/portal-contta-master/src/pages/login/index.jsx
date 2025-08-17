import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import history from '../../helpers/history';

import Form from './form';
import { Row, Column, Center } from '../../components/bootstrap';
import logo from '../../images/logoContta.png';

export class Login extends Component {
  render() {
    return (
      <Row>
        <Column col="4" />
        <Column col="4">
          <div className="animated fadeInDown" style={{ marginTop: 100 }}>
            <Row>
              <Column col="12">
                <div className="ibox-content" style={{ border: '1px solid #eee' }}>
                  <center>
                    <img style={{ maxHeight: 200 }} alt="" src={logo} />
                  </center>
                  {/* <h3 className="font-bold">
                    <Center>Seja bem-vindo!</Center>
                  </h3> */}
                  <Form />
                  {/* <Link to="#">
                    <p> Esqueceu sua senha?</p>
                  </Link> */}
                  <p className="text-center" style={{ marginTop: '1em' }}>
                    OU
                  </p>
                  <button
                    type="button"
                    className="btn btn-block btn-warning"
                    onClick={() => history.push('/cadastro')}>
                    Cadastre-se
                  </button>
                </div>
              </Column>
              <Column col="3" />
            </Row>
          </div>
        </Column>
        <Column col="4" />
      </Row>
    );
  }
}

export default Login;
