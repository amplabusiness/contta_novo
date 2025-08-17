import axios from 'axios';

import { store } from '@/store';
import { logoutSE } from '@/store/slices/user';

// Permite configurar a URL da API via variável de ambiente do CRA
// Ex.: REACT_APP_API_BASE_URL=http://localhost:8080/api/
const API_BASE_URL =
  process.env.REACT_APP_API_BASE_URL || 'https://api.contta.com.br/api/';

const appAxiosInstance = axios.create({
  baseURL: API_BASE_URL,
});

appAxiosInstance.interceptors.request.use(function (config) {
  const token = localStorage.getItem('@contta:token');

  if (!token) {
    setTimeout(() => {
      store.dispatch(logoutSE());
    }, 500);
  }

  const jwtToken = `Bearer ${token}`;

  config.headers.Authorization = jwtToken;

  return config;
});

const authAxiosInstance = axios.create({
  baseURL: API_BASE_URL,
});

export { appAxiosInstance, authAxiosInstance };
