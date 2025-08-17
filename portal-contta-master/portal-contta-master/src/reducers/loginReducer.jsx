import { LOGIN_LOADING, LOGIN_LOGGED } from '../util/actionTypes';

const INITIAL_STATE = {
  loading: false,
  logged: !!localStorage.getItem('@contta:token'),
};

export default (state = INITIAL_STATE, action) => {
  switch (action.type) {
    case LOGIN_LOADING:
      return { ...state, loading: action.payload };
    case LOGIN_LOGGED:
      return { ...state, logged: action.payload };
    default:
      return state;
  }
};
