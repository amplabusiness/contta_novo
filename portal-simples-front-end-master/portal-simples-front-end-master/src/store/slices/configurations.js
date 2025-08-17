import { createSlice } from '@reduxjs/toolkit';

const initialState = {};

const configurationsSlice = createSlice({
  name: 'configurations',
  initialState,
  reducers: {
    setConfig: (state, action) => {
      return action.payload;
    },
  },
});

const { reducer, actions } = configurationsSlice;

export const { setConfig } = actions;

export default reducer;
