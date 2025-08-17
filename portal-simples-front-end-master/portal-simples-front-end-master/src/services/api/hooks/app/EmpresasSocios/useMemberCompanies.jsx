import { useQuery } from 'react-query';

import { getMemberCompanies } from '@/services/api/requests';

const useMemberCompanies = (name, cpf) => {
  const query = useQuery(
    ['empresasSocio', name, cpf],
    () => getMemberCompanies(name, cpf),
    {
      refetchInterval: false,
      refetchOnWindowFocus: false,
    },
  );

  return query;
};

export default useMemberCompanies;
