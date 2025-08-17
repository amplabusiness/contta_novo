import { useMutation } from 'react-query';

import { postCompanyDocumentUpload } from '@/services/api/requests';
import { getUserToken } from '@/utils/userToken';

const useNewMultipleCompanies = () => {
  const userToken = getUserToken();
  const mutation = useMutation(values =>
    postCompanyDocumentUpload(values, userToken),
  );

  return mutation;
};

export default useNewMultipleCompanies;
