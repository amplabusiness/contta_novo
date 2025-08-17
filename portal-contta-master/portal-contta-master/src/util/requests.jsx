import api from '../services/api';

export async function getAccessToken(data) {
  try {
    const response = await api.post(`/access/getaccesstoken`, data);
    return response;
  } catch (error) {
    return error;
  }
}

export async function postNewUser(data) {
  try {
    const response = await api.post(`/user/newuser`, data);
    return response;
  } catch (error) {
    return error;
  }
}

export async function putUpdateUser(data) {
  try {
    const response = await api.put(`/user/updateuser`, data);
    return response;
  } catch (error) {
    return error;
  }
}
