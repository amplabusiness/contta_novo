import { useMutation, useQueryClient } from 'react-query';
import { useSelector } from 'react-redux';

import { postNewIcmsStTax } from '@/services/api/requests';

const useNewIcmsStTax = option => {
  const { id } = useSelector(state => state.activeCompanyState);

  const possibleOptions = {
    exigSuspensa: 'newexigibilidade',
    isento: 'newinsento',
    imune: 'newimune',
    isencaoReducao: 'newreducao',
    isencaoReducaoCestaBasica: 'newredcestbasic',
    antEncTributacao: 'newantecipacao',
  };
  const selectedOption = possibleOptions[option];

  const queryClient = useQueryClient();
  const mutation = useMutation(
    values => postNewIcmsStTax(values, selectedOption),
    {
      onSuccess: () =>
        queryClient.refetchQueries(['impostosIcmsSt', id, option]),
    },
  );

  return mutation;
};

export default useNewIcmsStTax;
