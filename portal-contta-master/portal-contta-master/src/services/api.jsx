import { create } from 'apisauce';

const baseURL = process.env.REACT_APP_API_BASE_URL || 'http://api.contta.com.br/api';

const api = create({
  baseURL,
});

api.addRequestTransform((request) => {
  request.headers['Authorization'] = `Bearer ${localStorage.getItem('@contta:token')}`;
});

api.addResponseTransform((response) => {
  if (!response.ok) throw response.data;
});

export default api;
