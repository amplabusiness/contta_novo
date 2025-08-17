import { create } from 'apisauce';

const api = create({
  baseURL: 'http://api.contta.com.br/api',
});

api.addRequestTransform((request) => {
  request.headers['Authorization'] = `Bearer ${localStorage.getItem('@contta:token')}`;
});

api.addResponseTransform((response) => {
  if (!response.ok) throw response.data;
});

export default api;
