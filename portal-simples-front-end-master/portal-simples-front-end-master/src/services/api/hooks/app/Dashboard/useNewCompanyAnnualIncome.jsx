import { notification } from 'antd';
import { useMutation, useQueryClient } from 'react-query';
import { useSelector } from 'react-redux';

import useRegisterHomeInfo from '@/services/api/hooks/app/Home/useRegisterHomeInfo';
import { postCompanyIncome } from '@/services/api/requests';
import {
  calculateCommonSimples,
  calculateServiceSimples,
} from '@/utils/simplesNacional/calculateCompanySimples';

const SIMPLES_MAX_VALUE = 4800000;

const useNewCompanyAnnualIncome = () => {
  const { id, annex, companyType, data } = useSelector(
    state => state.activeCompanyState,
  );
  const { date } = useSelector(state => state.referenceDateState);
  const { valorContabil = {} } = data;

  const homeMutation = useRegisterHomeInfo();

  const queryClient = useQueryClient();
  const mutation = useMutation(values => postCompanyIncome(values), {
    onSuccess: async response => {
      /*
       * Foi necessário recalcular o valor do Simples Nacional
       * aqui para que seja possível enviar a alíquota efetiva
       * atualizada para a Home
       */

      const { totalAnual } = response.result;

      if (totalAnual <= SIMPLES_MAX_VALUE) {
        let aliquot = 0;

        if (companyType === 'comum') {
          const { valorSaidaMercadoria } = valorContabil;

          const { aliquotaEfetiva } = calculateCommonSimples({
            anexo: annex,
            rendaAnual: totalAnual,
            rendaMensal: valorSaidaMercadoria,
          });

          aliquot = aliquotaEfetiva;
        } else if (companyType === 'servico') {
          const { notaServicoPrestador } = valorContabil;

          const { aliquotaEfetiva } = calculateServiceSimples({
            anexo: annex,
            rendaAnual: totalAnual,
            rendaMensal: notaServicoPrestador,
          });

          aliquot = aliquotaEfetiva;
        } else if (companyType === 'transporte') {
          /*
           *  Existem várias alíquotas quando a empresa é de transporte.
           * Preciso perguntar antes de implementar aqui.
           */
        }

        const parsedAliquot = parseFloat(aliquot.replace(',', '.')) / 100;
        const homeQuery = `aliquota=${parsedAliquot}&empresaId=${id}&fat12Messes=${totalAnual}&dataEmissao=${date}`;

        await homeMutation.mutateAsync(homeQuery);
      }

      await queryClient.refetchQueries(['faturamentoEmpresaAtiva', id, date]);

      notification.success({
        message: 'Sucesso',
        description: 'Faturamento enviado com sucesso!',
      });
    },
    onError: () => {
      notification.error({
        message: 'Erro',
        description:
          'Não foi possível enviar o faturamento da empresa no momento.',
      });
    },
  });

  return mutation;
};

export default useNewCompanyAnnualIncome;
