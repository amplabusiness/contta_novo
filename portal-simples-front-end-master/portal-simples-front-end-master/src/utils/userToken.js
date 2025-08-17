export const getUserToken = () => localStorage.getItem('@contta:token');

export const setUserToken = value =>
  localStorage.setItem('@contta:token', value);
