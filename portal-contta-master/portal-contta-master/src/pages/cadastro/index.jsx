import React, { useRef } from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import CadastroForm from './cadastroForm';
import logo from '../../images/logoContta.png';

import { Typography } from 'antd';
import { adicionarUsuario } from '../../actions/usuarioActions';
import { Br } from '../../components/bootstrap';

const { Title } = Typography;

const Cadastro = ({ adicionarUsuarioAction, usuarioReducer }) => {
  const formRef = useRef();

  const onSubmit = (values) => {
    adicionarUsuarioAction(values);
  };

  return (
    <div>
      <header
        style={{
          width: '100%',
          zIndex: 500,
          height: 60,
          backgroundColor: '#fff',
          marginBottom: 30,
        }}>
        <div style={{ alignContent: 'start', marginLeft: 30 }}>
          <img style={{ maxHeight: 60 }} alt="" src={logo} />
        </div>
      </header>

      <section className="app wrap">
        <div className="container">
          <div className="card">
            <div className="card-body">
              <center>
                <Title level={3}>Cadastro de Novo Usuário</Title>
                <Br />
                <Br />
              </center>
              <CadastroForm ref={formRef} onSubmit={onSubmit} loading={usuarioReducer.loading} />
            </div>
          </div>
        </div>
        <div>
          <ul className="links-footer">
            <li>
              <Link
                to="/login"
                className="btn btn-block btn-link"
                style={{ color: '#fff', fontWeight: 'bold', marginTop: 20 }}>
                Voltar para o Login
              </Link>
            </li>
          </ul>
        </div>
      </section>
    </div>
  );
};

const mapStateToProps = (state) => ({
  usuarioReducer: state.UsuarioReducer,
});

const mapDispatchToProps = {
  adicionarUsuarioAction: adicionarUsuario,
};

export default connect(mapStateToProps, mapDispatchToProps)(Cadastro);
