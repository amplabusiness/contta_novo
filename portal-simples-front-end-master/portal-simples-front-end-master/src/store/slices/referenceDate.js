import { createSlice } from '@reduxjs/toolkit';

const initialState = {
  date: new Date().toISOString(),
};

const referenceDateSlice = createSlice({
  name: 'referenceDate',
  initialState,
  reducers: {
    setReferenceDate: (state, action) => {
      state.date = action.payload;
    },
  },
});

const { reducer, actions } = referenceDateSlice;

export const { setReferenceDate } = actions;

export default reducer;
