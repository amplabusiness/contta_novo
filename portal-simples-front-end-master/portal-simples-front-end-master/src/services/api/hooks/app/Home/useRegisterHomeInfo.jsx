import { useMutation } from 'react-query';

import { postHomePageCompanyInfo } from '@/services/api/requests';

const useRegisterHomeInfo = () => {
  const mutation = useMutation(values => postHomePageCompanyInfo(values));

  return mutation;
};

export default useRegisterHomeInfo;
