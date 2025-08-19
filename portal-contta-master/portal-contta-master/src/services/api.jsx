import { create } from 'apisauce';

function detectBaseURL() {
  const envBase = process.env.REACT_APP_API_BASE_URL;
  if (envBase) return envBase;
  if (typeof window !== 'undefined') {
    const host = window.location.host.toLowerCase();
    if (host.includes('vercel.app')) {
      return 'https://contta-searchapi-staging.onrender.com/api';
    }
  }
  return 'http://api.contta.com.br/api';
}

const baseURL = detectBaseURL();

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
