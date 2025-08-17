import { notification } from 'antd';
import { useMutation } from 'react-query';
import { useLocation } from 'react-router-dom';

import { postParseJsonToExcel } from '@/services/api/requests';
import { dateFormatter } from '@/utils/formatters';

const useParseJsonToExcel = () => {
  const { pathname } = useLocation();

  const mutation = useMutation(values => postParseJsonToExcel(values), {
    onSuccess: file => {
      const url = window.URL.createObjectURL(new Blob([file]));
      const link = document.createElement('a');
      link.setAttribute('href', url);

      const filename = `planilha_${pathname
        .split('/')
        .join('_')}_${dateFormatter(new Date().toISOString(), 'HHmm')}.xlsx`;

      link.setAttribute('download', filename);
      document.body.appendChild(link);
      link.click();
      link.remove();
    },
    onError: () => {
      notification.error({
        message: 'Erro',
        description: 'Não foi possível fazer o download do arquivo no momento.',
      });
    },
  });

  return mutation;
};

export default useParseJsonToExcel;
