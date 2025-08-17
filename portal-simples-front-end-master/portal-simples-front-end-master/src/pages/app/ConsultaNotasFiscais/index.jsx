import { useState, useEffect } from 'react';
import { RiFilter2Fill } from 'react-icons/ri';
import { useSelector } from 'react-redux';

import useInvoicesQuery from '@/services/api/hooks/app/ConsultaNotasFiscais/useInvoicesQuery';
import { removeNonNumericChars } from '@/utils/formatters';

import SearchForm from '@/pages/app/ConsultaNotasFiscais/components/SearchForm';
import InvoicesTable from '@/pages/app/ConsultaNotasFiscais/components/InvoicesTable';

import { Title } from '@/styles/global';
import { Container, Filters, FilterTitle } from './styles';

const ConsultaNotasFiscais = () => {
  const { id } = useSelector(state => state.activeCompanyState);

  const [results, setResults] = useState([]);

  const { mutate, data, isLoading } = useInvoicesQuery();

  useEffect(() => {
    if (data) {
      setResults(data);
    }
  }, [data]);

  const onSubmit = values => {
    const hasDate = !!values.dhEmiss;

    const obj = {
      ...values,
      companyId: id,
      cnpj: removeNonNumericChars(values.cnpj),
      dhEmiss: hasDate ? values.dhEmiss.toISOString() : '',
      nomeCli: values.nomeCli.toUpperCase(),
    };

    const queryObject = Object.fromEntries(
      Object.entries(obj).filter(([_, value]) => value !== ''),
    );

    const queryString = new URLSearchParams(queryObject).toString();

    mutate(queryString);
  };

  return (
    <Container>
      <Title>
        <h2>Lista de Notas Fiscais</h2>
        <p>
          Abaixo encontra-se uma tabela com a listagem de todas as notas fiscais
          encontradas. Caso queira filtrar os resultados, utilize as opções
          abaixo.
        </p>
      </Title>

      <Filters>
        <FilterTitle>
          <RiFilter2Fill size={28} color="#676a6c" />
          <p>Filtros</p>
        </FilterTitle>

        <SearchForm onSubmit={onSubmit} isLoading={isLoading} />
      </Filters>

      <InvoicesTable data={results} isLoading={isLoading} />
    </Container>
  );
};

export default ConsultaNotasFiscais;
