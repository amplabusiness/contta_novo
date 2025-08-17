import { createSlice } from '@reduxjs/toolkit';

const initialState = {
  companies: [],
};

const companiesSlice = createSlice({
  name: 'companies',
  initialState,
  reducers: {
    setCompanies: (state, action) => {
      state.companies = action.payload;
    },
  },
});

const { reducer, actions } = companiesSlice;

export const { setCompanies } = actions;

export const setInitialStateSE = data => dispatch => {
  const sortedCompaniesByName = Array.isArray(data)
    ? data.sort((a, b) => {
        const nameA = a.name.toLowerCase();
        const nameB = b.name.toLowerCase();

        if (nameA < nameB) {
          return -1;
        }

        if (nameA > nameB) {
          return 1;
        }

        return 0;
      })
    : [];

  dispatch(setCompanies(sortedCompaniesByName));
};

export default reducer;
