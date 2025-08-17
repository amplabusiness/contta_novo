import { useRef } from 'react';

import useFilterCalculationAdjustment from '@/services/api/hooks/app/AjusteApuracao/useFilterCalculationAdjustment';
import useUpdateCalculationAdjustment from '@/services/api/hooks/app/AjusteApuracao/useUpdateCalculationAdjustment';

import FilterForm from '@/pages/app/AjusteApuracao/components/FilterForm';
import CalculationForm from '@/pages/app/AjusteApuracao/components/CalculationForm';

import { Container, Title } from '@/styles/global';
import { Content } from './styles';

const AjusteApuracao = () => {
  const filterMutation = useFilterCalculationAdjustment();
  const calculationMutation = useUpdateCalculationAdjustment();

  const calculationFormRef = useRef(null);

  const onFilterSubmit = async values => {
    await filterMutation.mutateAsync(values, {
      onSuccess: () => {
        if (calculationFormRef.current) {
          calculationFormRef.current.scrollIntoView({ behavior: 'smooth' });
        }
      },
      onError: error => {
        throw error;
      },
    });
  };

  const { data: filterData } = filterMutation;
  const hasFilterData = !!filterData;
  const isLoading = calculationMutation.isLoading || filterMutation.isLoading;

  const onCalculationSubmit = async values => {
    // Esse submit só irá rodar caso a primeira mutação já tenha sido concluída
    if (hasFilterData) {
      const { id } = filterData;

      const data = {
        ajusteId: id,
        ...values,
      };

      await calculationMutation.mutateAsync(data, {
        onError: error => {
          throw error;
        },
      });
    }
  };

  return (
    <Container>
      <Title>
        <h2>Filtragem de Notas Fiscais</h2>
        <p>
          Utilize os campos abaixo para filtrar as notas fiscais desejadas. Após
          isso, insira o valor da alíquota para o cálculo total.
        </p>
      </Title>

      <Content>
        <FilterForm onSubmit={onFilterSubmit} isLoading={isLoading} />
      </Content>

      {hasFilterData && (
        <div ref={calculationFormRef}>
          <Title>
            <h2>Total dos Valores Encontrado</h2>
            <p>
              Insira a alíquota de dedução abaixo para o cálculo do valor total
              de acordo com a filtragem realizada acima.
            </p>
          </Title>

          <Content>
            <CalculationForm
              nfeValue={filterData.vlTotalNfe}
              onSubmit={onCalculationSubmit}
              isLoading={isLoading}
            />
          </Content>
        </div>
      )}
    </Container>
  );
};

export default AjusteApuracao;
