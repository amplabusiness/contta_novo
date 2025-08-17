import { useMutation } from 'react-query';
import { useSelector } from 'react-redux';

import { getCompanyBook } from '@/services/api/requests';

const useDownloadBook = () => {
  const { id } = useSelector(state => state.activeCompanyState);
  const { date } = useSelector(state => state.referenceDateState);

  const mutation = useMutation(bookType => getCompanyBook(id, date, bookType), {
    onSuccess: file => {
      const url = window.URL.createObjectURL(new Blob([file]));
      const link = document.createElement('a');
      link.setAttribute('href', url);
      const fileName = 'Livro.pdf';
      link.setAttribute('download', fileName);
      document.body.appendChild(link);
      link.click();
      link.remove();
    },
  });

  return mutation;
};

export default useDownloadBook;
