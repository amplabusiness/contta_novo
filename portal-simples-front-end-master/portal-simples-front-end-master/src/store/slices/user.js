import { createSlice } from '@reduxjs/toolkit';

import { setConfig } from '@/store/slices/configurations';
import { setUserToken } from '@/utils/userToken';

const initialState = {
  id: '',
  masterId: '',
  name: '',
  logged: false,
  isAdmin: false,
};

const userSlice = createSlice({
  name: 'user',
  initialState,
  reducers: {
    setId: (state, action) => {
      state.id = action.payload;
    },
    setMasterId: (state, action) => {
      state.masterId = action.payload;
    },
    setLogged: (state, action) => {
      state.logged = action.payload;
    },
    setIsAdmin: (state, action) => {
      state.isAdmin = action.payload;
    },
    setName: (state, action) => {
      state.name = action.payload;
    },
  },
});

const { reducer, actions } = userSlice;

export const { setId, setMasterId, setName, setLogged, setIsAdmin } = actions;

export const setInitialDataSE = (userConfig, userToken, user) => dispatch => {
  const { id, userMasterId, name, group } = user;

  setUserToken(userToken);

  if (group === 1) {
    dispatch(setIsAdmin(true));
  }

  dispatch(setConfig(userConfig));
  dispatch(setId(id));
  dispatch(setMasterId(userMasterId));
  dispatch(setName(name));
  dispatch(setLogged(true));
};

export const logoutSE = () => dispatch => {
  localStorage.removeItem('@contta:token');
  localStorage.removeItem('persist:root');

  dispatch({ type: 'user/LOGOUT' });
};

export default reducer;
