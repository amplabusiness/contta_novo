import { create } from 'apisauce';

const api = create({
  // baseURL: "http://192.168.0.18:5000/api"
  // baseURL: 'http://192.168.1.23:5000/api',
  // baseURL: 'http://192.168.0.183:5000/api',
});

// const bearer = window.REACT_APP_TOKEN ? window.REACT_APP_TOKEN : 'localhost';

api.addRequestTransform((request) => {
  request.headers[
    'Authorization'
  ] = `Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiJlMjhkMjJlZC1iN2FmLTQ2NGEtOWNlMi1iNjRjZjYwZGJiNGQiLCJ0ZW5hbnRJZCI6IjdjZWMwY2ExLTMyZTUtNGVkZC05ZTczLWI3MDFiOWNhZDA0ZCIsInVzZXJJZCI6IiIsInVzZXJQZXJzb25JZCI6IiIsImNsaWVudElkIjoiIiwicm9sZSI6ImludGVncmF0b3IiLCJuYmYiOjE1OTYzMjA4MjIsImV4cCI6MjUzNDAyMzAwODAwLCJpYXQiOjE1OTYzMjA4MjIsImlzcyI6Imh0dHA6Ly9vbW5peC5mb3JzaGNvbW1lcmNlLmNvbS5iciIsImF1ZCI6Im9tbml4In0.SkbiIQpHNn0R2yVwZICDYtU1d52Zl5LTBNGY2S8N7fIRU8Cl_aUMQl3uqDgPRkjYzCpaTAg-ECoqSAU6S4aN9g`;
});

// HIPER JN
// "tenantId": "e838d89e-78cb-4c1a-b9d4-ce2bd1021fed"

// COMERCIAL JD
// "tenantId": "fe96c48f-15b8-4c57-9127-f087fca11810"

// PARANA
// `Bearer "tenantId": "81c7fb66-cacd-4aaa-ad57-492ef66d5f32"

// SHM
// "tenantId": "e5cb9a86-ea57-470b-9df2-cfdfeeffbcb8"

// VERDAO
// "tenantId": "7cec0ca1-32e5-4edd-9e73-b701b9cad04d"

// ALVORADA
// "tenantId": "22ad0c86-835c-4784-9560-6fe7aacbcb93"

api.addResponseTransform((response) => {
  if (!response.ok) throw response.data;
});

export default api;
