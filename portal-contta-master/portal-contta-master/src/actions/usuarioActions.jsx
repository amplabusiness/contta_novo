import { notification } from 'antd';
import history from '../helpers/history';
import swal from 'sweetalert';

import { postNewUser } from '../util/requests';

import { USUARIO_GET, USUARIO_GET_ALL, USUARIO_LOADING } from '../util/actionTypes';

export const setLoading = (loading) => (dispatch) => {
  dispatch({ type: USUARIO_LOADING, payload: loading });
};

export const setRecords = (records) => (dispatch) => {
  dispatch({ type: USUARIO_GET_ALL, payload: records });
};

export const setRecord = (record) => (dispatch) => {
  dispatch({ type: USUARIO_GET, payload: record });
};

export const setEstadoInicial = () => (dispatch) => {
  dispatch({ type: USUARIO_GET, payload: {} });
};

export const getAllUsers = () => (dispatch) => {
  try {
  } catch (error) {
    return error;
  }
};

export const getUserData = () => (dispatch) => {
  try {
  } catch (error) {
    return error;
  }
};

export const adicionarUsuario = (values) => async (dispatch) => {
  try {
    dispatch([setLoading(true)]);

    const data = {
      name: values.name,
      email: values.email,
      password: values.password,
      document: values.document,
      picture: '',
      isActive: true,
    };

    const response = await postNewUser(data);

    if (response.status == 200) {
      dispatch([setLoading(false), history.push('/login')]);
      swal('Cadastro realizado com sucesso!', { icon: 'success' });
    } else {
      dispatch([setLoading(false)]);
      notification['error']({
        message: 'Cadastro',
        description: response.data.mensagem,
      });
    }
  } catch (error) {
    dispatch([setLoading(false)]);
    return null;
  }
};

export const atualizarUsuario = (values) => async (dispatch) => {
  try {
  } catch {
    return;
  }
};

export const deletaUsuario = (usuarioId) => async (dispatch) => {
  try {
  } catch (error) {
    return error;
  }
};
