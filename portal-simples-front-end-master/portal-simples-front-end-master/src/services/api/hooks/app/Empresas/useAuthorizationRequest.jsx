import { useMutation } from 'react-query';

import { putSendAuthorizationRequest } from '@/services/api/requests';

const useAuthorizationRequest = () => {
  const mutation = useMutation(values => putSendAuthorizationRequest(values));

  return mutation;
};

export default useAuthorizationRequest;
