import { useMutation } from 'react-query';

import { postGenerateEBlockData } from '@/services/api/requests';

const useGenerateEBlockData = () => {
  const mutation = useMutation(values => {
    const { adjustmentCodesList, debitBalanceValue } = values;

    return postGenerateEBlockData(adjustmentCodesList, debitBalanceValue);
  });

  return mutation;
};

export default useGenerateEBlockData;
