import { combineReducers } from 'redux';

import LoginReducer from './loginReducer';
import UsuarioReducer from './usuarioReducer';

export default combineReducers({
  LoginReducer,
  UsuarioReducer,
});
