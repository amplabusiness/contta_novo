import { combineReducers } from '@reduxjs/toolkit';

import activeCompanyState from './activeCompany';
import companiesState from './companies';
import configurationsState from './configurations';
import referenceDateState from './referenceDate';
import userState from './user';

const appReducer = combineReducers({
  activeCompanyState,
  companiesState,
  configurationsState,
  referenceDateState,
  userState,
});

const rootReducer = (state, action) => {
  if (action.type === 'user/LOGOUT') {
    state = undefined;
  }

  return appReducer(state, action);
};

export default rootReducer;
