import { useMutation } from 'react-query';
import { useSelector } from 'react-redux';

import { getCompanyProductsSheet } from '@/services/api/requests';

const useDownloadCompanyProductsSheet = () => {
  const { name } = useSelector(state => state.activeCompanyState);

  const mutation = useMutation(values => getCompanyProductsSheet(values), {
    onSuccess: file => {
      const url = window.URL.createObjectURL(new Blob([file]));
      const link = document.createElement('a');
      link.setAttribute('href', url);
      const fileName = `ESTOQUE_${name}.xlsx`;
      link.setAttribute('download', fileName);
      document.body.appendChild(link);
      link.click();
      link.remove();
    },
  });

  return mutation;
};

export default useDownloadCompanyProductsSheet;
