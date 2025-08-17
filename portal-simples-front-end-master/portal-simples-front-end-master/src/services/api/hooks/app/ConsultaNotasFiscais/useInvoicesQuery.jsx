import { useMutation } from 'react-query';

import { getInvoicesQuery } from '@/services/api/requests';

const useInvoicesQuery = () => {
  const mutation = useMutation(values => getInvoicesQuery(values));

  return mutation;
};

export default useInvoicesQuery;
