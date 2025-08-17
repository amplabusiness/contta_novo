import { useQuery } from 'react-query';
import { useSelector } from 'react-redux';

import { usePisCofinsContext } from '@/contexts/PisCofinsContext';
import { getAllProductsByOption } from '@/services/api/requests';

const usePisCofins = option => {
  const { id } = useSelector(state => state.activeCompanyState);
  const { date } = useSelector(state => state.referenceDateState);

  const { setInitialState } = usePisCofinsContext();

  const query = useQuery(
    ['produtosPisCofins', id, option, date],
    () => getAllProductsByOption(id, option, date),
    {
      refetchInterval: false,
      refetchOnWindowFocus: false,
      onSuccess: data => {
        setInitialState(data);
      },
    },
  );

  return query;
};

export default usePisCofins;
