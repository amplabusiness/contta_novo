import { notification } from 'antd';
import history from '../helpers/history';
import { getAccessToken } from '../util/requests';

import { LOGIN_LOADING, LOGIN_LOGGED } from '../util/actionTypes';

export const setLoading = (loading) => (dispatch) => {
  dispatch({ type: LOGIN_LOADING, payload: loading });
};

export const setLogged = (logged) => (dispatch) => {
  dispatch({ type: LOGIN_LOGGED, payload: logged });
};

export const auth = (values, setSubmitting) => async (dispatch) => {
  try {
    dispatch([setLoading(true), setSubmitting(true)]);

    const data = {
      email: values.email,
      password: values.password,
    };

    const response = await getAccessToken(data);

    if (response?.status == 200) {
      localStorage.setItem('@contta:token', response.data.token);
      dispatch(setLoading(false));
      dispatch([setLogged(true), setSubmitting(false), history.push('/dashboard')]);
    } else {
      notification['error']({
        message: 'Login',
        description: response.data.mensagem,
      });
    }
  } catch (error) {
    notification['error']({
      message: 'Login',
      description: error,
    });
    dispatch([setLoading(false), setSubmitting(false)]);
  }
};

export const logout = () => (dispatch) => {
  try {
    dispatch(setLoading(true));
    localStorage.clear();
    dispatch([setLoading(false), setLogged(false), history.push('/login')]);
  } catch (error) {
    notification['error']({
      message: 'Login',
      description: error,
    });
    dispatch(setLoading(false));
  }
};
