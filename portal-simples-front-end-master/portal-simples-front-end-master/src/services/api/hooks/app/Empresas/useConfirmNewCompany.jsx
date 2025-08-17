import { useMutation } from 'react-query';

import { getConfirmNewCompany } from '@/services/api/requests';
import { getUserToken } from '@/utils/userToken';

const useConfirmNewCompany = () => {
  const userToken = getUserToken();

  const mutation = useMutation(values => {
    const { cnpj, confirmRegister, id } = values;

    return getConfirmNewCompany(cnpj, userToken, confirmRegister, id);
  });

  return mutation;
};

export default useConfirmNewCompany;
