import { useQuery } from 'react-query';
import { useSelector } from 'react-redux';

import { getCalculation } from '@/services/api/requests';

const useCalculation = () => {
  const { id, companyType } = useSelector(state => state.activeCompanyState);
  const { date } = useSelector(state => state.referenceDateState);

  let remainUrl = '';

  if (companyType === 'comum') {
    remainUrl = 'Mod57=false&Mod55=true&Service=false';
  } else if (companyType === 'servico') {
    remainUrl = 'Mod57=false&Mod55=false&Service=true';
  } else if (companyType === 'transporte') {
    remainUrl = 'Mod57=true&Mod55=false&Service=false';
  }

  const query = useQuery(
    ['apuracao', id, date],
    () => getCalculation(id, date, remainUrl),
    {
      refetchInterval: false,
      refetchOnWindowFocus: false,
      select: data => {
        if (companyType === 'comum') {
          const { apuracaoSaida, apuracaoEntrada } = data[0];

          const hasEntriesData = apuracaoEntrada.length > 0;
          let entriesTotal = {};

          if (hasEntriesData) {
            entriesTotal =
              apuracaoEntrada.length > 1
                ? {
                    cfop: '',
                    descricaoCfop: 'Total de Entradas',
                    totalNfe: apuracaoEntrada.reduce(
                      (total, current) => total + current.totalNfe,
                      0,
                    ),
                  }
                : {
                    cfop: '',
                    descricaoCfop: 'Total de Entradas',
                    totalNfe: apuracaoEntrada[0].totalNfe,
                  };
          } else {
            entriesTotal = {
              cfop: '',
              descricaoCfop: 'Total de Entradas',
              totalNfe: 0,
            };
          }

          const hasOutsData = apuracaoSaida.length > 0;
          let outsTotal = {};

          if (hasOutsData) {
            outsTotal =
              apuracaoSaida.length > 1
                ? {
                    cfop: '',
                    descricaoCfop: 'Total de Saídas',
                    totalNfe: apuracaoSaida.reduce(
                      (total, current) => total + current.totalNfe,
                      0,
                    ),
                  }
                : {
                    cfop: '',
                    descricaoCfop: 'Total de Saídas',
                    totalNfe: apuracaoSaida[0].totalNfe,
                  };
          } else {
            outsTotal = {
              cfop: '',
              descricaoCfop: 'Total de Saídas',
              totalNfe: 0,
            };
          }

          const sortedEntries = hasEntriesData
            ? apuracaoEntrada.sort((a, b) => a.cfop - b.cfop)
            : apuracaoEntrada;
          const sortedOuts = hasOutsData
            ? apuracaoSaida.sort((a, b) => a.cfop - b.cfop)
            : apuracaoSaida;

          const entries = [...sortedEntries, entriesTotal];
          const outs = [...sortedOuts, outsTotal];
          const total = [
            {
              description: 'Total de Entradas',
              value: entriesTotal.totalNfe,
            },
            {
              description: 'Total de Saídas',
              value: outsTotal.totalNfe,
            },
            {
              description: 'Resultado',
              value: outsTotal.totalNfe - entriesTotal.totalNfe,
            },
            {
              description: 'Porcentagem',
              value:
                entriesTotal.totalNfe > 0
                  ? ((outsTotal.totalNfe - entriesTotal.totalNfe) /
                      entriesTotal.totalNfe) *
                    100
                  : (outsTotal.totalNfe - entriesTotal.totalNfe) * 100,
            },
          ];

          const initialOverviewData = {
            entries,
            outs,
            total,
          };

          return initialOverviewData;
        }

        const { apuracaoSaida, apuracaoEntrada } = data;

        const initialOverviewData = {
          entries: [{ id: 'apuracaoEntrada', ...apuracaoEntrada }],
          outs: [{ id: 'apuracaoSaida', ...apuracaoSaida }],
        };

        return initialOverviewData;
      },
    },
  );

  return query;
};

export default useCalculation;
