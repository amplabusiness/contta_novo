import { USUARIO_GET, USUARIO_GET_ALL, USUARIO_LOADING } from '../util/actionTypes';

const INITIAL_STATE = {
  loading: false,
  records: [],
  record: {},
};

export default (state = INITIAL_STATE, action) => {
  switch (action.type) {
    case USUARIO_LOADING:
      return { ...state, loading: action.payload };
    case USUARIO_GET_ALL:
      return { ...state, records: action.payload };
    case USUARIO_GET:
      return { ...state, record: action.payload };
    default:
      return state;
  }
};
