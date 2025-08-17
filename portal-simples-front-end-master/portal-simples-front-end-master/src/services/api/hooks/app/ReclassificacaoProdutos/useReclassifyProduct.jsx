import { notification } from 'antd';
import { useMutation, useQueryClient } from 'react-query';
import { useSelector } from 'react-redux';
import { useLocation } from 'react-router-dom';

import {
  putOutProductsReclassification,
  putEntryProductsReclassification,
} from '@/services/api/requests';

const useReclassifyProduct = operation => {
  const { id, name, alias, cnpj } = useSelector(
    state => state.activeCompanyState,
  );

  const { state: nfeId } = useLocation();

  const queryClient = useQueryClient();
  const mutation = useMutation(
    values => {
      const isOutReclassification = operation === 'Venda';

      const productsList = isOutReclassification
        ? {
            ListProdutos: values,
          }
        : {
            razaoSocial: name,
            fantasia: alias,
            modeloNfe: '55',
            cnpj,
            listaProdutos: values.map(item => ({
              id: item.id,
              ncmProd: item.ncmProd,
              cfop: item.cfop,
            })),
          };

      return isOutReclassification
        ? putOutProductsReclassification(id, productsList)
        : putEntryProductsReclassification(productsList);
    },
    {
      onSuccess: async () => {
        await queryClient.refetchQueries([
          'reclassificacao',
          id,
          nfeId,
          operation,
        ]);

        notification.success({
          message: 'Sucesso',
          description: 'Modificação confirmado com sucesso!',
        });
      },
      onError: () => {
        notification.error({
          message: 'Erro',
          description: 'Não foi possível modificar o produto no momento.',
        });
      },
    },
  );

  return mutation;
};

export default useReclassifyProduct;
