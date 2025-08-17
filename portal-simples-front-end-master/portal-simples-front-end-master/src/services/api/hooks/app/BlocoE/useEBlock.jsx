import { useQuery } from 'react-query';
import { useSelector } from 'react-redux';

import { getEBlock, getComplementaryEBlock } from '@/services/api/requests';

const useEBlock = () => {
  const { id } = useSelector(state => state.activeCompanyState);
  const { date } = useSelector(state => state.referenceDateState);

  const mainQuery = useQuery(['eBlock', id, date], () => getEBlock(id, date), {
    refetchInterval: false,
    refetchOnWindowFocus: false,
    select: data => ({
      registroE100: data.e100,
      registroE110: data.e110,
      registroE111: data.e111,
      registroE113: data.e113,
      registroE115: data.e115,
      registroE116: data.e116,
    }),
  });

  const mainQueryHasNoData = mainQuery.isSuccess
    ? Object.values(mainQuery.data).includes(null)
    : false;

  const complementaryQuery = useQuery(
    ['complementaryEBlock', id, date],
    () => getComplementaryEBlock(id, date),
    {
      enabled: mainQueryHasNoData,
      refetchInterval: false,
      refetchOnWindowFocus: false,
    },
  );

  const isLoading = mainQuery.isLoading || complementaryQuery.isLoading;
  const isError = mainQuery.isError || complementaryQuery.isError;
  const data = mainQueryHasNoData ? complementaryQuery.data : mainQuery.data;

  return { isLoading, isError, data };
};

export default useEBlock;
