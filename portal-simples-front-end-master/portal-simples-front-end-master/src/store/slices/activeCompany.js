import { createSlice } from '@reduxjs/toolkit';
import { notification } from 'antd';

import { checkCompanyType } from '@/utils/simplesNacional/checkCompanyType';
import {
  calculateCommonSimples,
  calculateServiceSimples,
  calculateMunicipalTransportSimples,
  calculateStateAndInterstateTransportSimples,
} from '@/utils/simplesNacional/calculateCompanySimples';

const SIMPLES_MAX_VALUE = 4800000;

const initialState = {
  id: '',
  name: '',
  alias: '',
  cnpj: '',
  annex: '',
  primaryCode: '',
  hasStock: false,
  companyType: '',
  isOutFromSimples: false,
  data: {},
};

const activeCompanySlice = createSlice({
  name: 'activeCompany',
  initialState,
  reducers: {
    setCompanyInfo: (state, action) => {
      return { ...state, ...action.payload };
    },
    setCompanyHasStock: (state, action) => {
      state.hasStock = action.payload;
    },
    setCompanyData: (state, action) => {
      state.data = action.payload;
    },
    setCompanyFoundationDate: (state, action) => {
      state.data.simplesNacional.dateFounded = action.payload;
    },
    setCompanyIsOutFromSimples: (state, action) => {
      state.isOutFromSimples = action.payload;
    },
    resetState: () => {
      return initialState;
    },
  },
});

const { reducer, actions } = activeCompanySlice;

export const {
  setCompanyInfo,
  setCompanyHasStock,
  setCompanyData,
  setCompanyFoundationDate,
  setCompanyIsOutFromSimples,
  resetState,
} = actions;

export const setInitialStateSE = companyData => (dispatch, getState) => {
  try {
    // Cálculo dos valores do Simples Nacional
    const { annex, companyType, isOutFromSimples } =
      getState().activeCompanyState;
    const { simplesNacional, valorContabil, faturamentoAnual } = companyData;
    const { totalAnual } = faturamentoAnual;

    let companySimplesValues = {};

    // Caso a empresa tenha ultrapassado o limite do Simples, não permitir o cálculo
    if (totalAnual > SIMPLES_MAX_VALUE) {
      throw new Error('A empresa ultrapassou o limite do Simples Nacional');
    }

    if (companyType === 'comum') {
      const { baseCalculo, baseIcms, baseConfins } = valorContabil;

      const {
        deducao,
        aliquotaEfetiva,
        valorDas,
        impostos,
        impostosEfetivosAliquotas,
        impostosEfetivosValores,
        impostosBasesCalculo,
      } = calculateCommonSimples({
        anexo: annex,
        rendaAnual: totalAnual,
        basesCalculo: {
          baseComum: baseCalculo,
          baseIcms,
          basePisCofins: baseConfins,
        },
      });

      companySimplesValues = {
        deducao,
        aliquotaEfetiva,
        valorDas,
        impostos,
        impostosEfetivosAliquotas,
        impostosEfetivosValores,
        impostosBasesCalculo,
      };
    } else if (companyType === 'servico') {
      const { baseCalculo } = valorContabil;

      const { deducao, aliquotaEfetiva, valorDas, impostos, impostosEfetivos } =
        calculateServiceSimples({
          anexo: annex,
          rendaAnual: totalAnual,
          rendaMensal: baseCalculo,
        });

      companySimplesValues = {
        deducao,
        aliquotaEfetiva,
        valorDas,
        impostos,
        impostosEfetivos,
      };
    } else if (companyType === 'transporte') {
      const {
        valorFreteIntramunicipal,
        valorFreteIntermunicipal,
        valorFreteInterestadual,
      } = valorContabil;

      const {
        aliquotaEfetiva: aliquotaEfetivaMunicipal,
        valorDas: valorDasMunicipal,
        impostosEfetivos: impostosEfetivosMunicipais,
      } = calculateMunicipalTransportSimples({
        rendaAnual: totalAnual,
        rendaMensal: valorFreteIntramunicipal,
      });

      const {
        aliquotaEfetiva: aliquotaEfetivaEstadual,
        valorDas: valorDasEstadual,
        impostosEfetivos: impostosEfetivosEstaduais,
      } = calculateStateAndInterstateTransportSimples({
        rendaAnual: totalAnual,
        rendaMensal: valorFreteIntermunicipal,
      });

      const {
        aliquotaEfetiva: aliquotaEfetivaInterestadual,
        valorDas: valorDasInterestadual,
        impostosEfetivos: impostosEfetivosInterestaduais,
      } = calculateStateAndInterstateTransportSimples({
        rendaAnual: totalAnual,
        rendaMensal: valorFreteInterestadual,
      });

      const valorDas =
        valorDasMunicipal + valorDasEstadual + valorDasInterestadual;

      companySimplesValues = {
        aliquotaEfetivaMunicipal,
        aliquotaEfetivaEstadual,
        aliquotaEfetivaInterestadual,
        valorDas,
        impostosEfetivosMunicipais,
        impostosEfetivosEstaduais,
        impostosEfetivosInterestaduais,
      };
    }

    const serializedSimplesNacional = {
      ...simplesNacional,
      ...companySimplesValues,
    };

    /* Mudança estrutural dos dados de faturamento
     * para um modelo que o Antd consiga entender
     */
    const { listFaturamentoMensaisEntrada, listFaturamentoMensaisSaida } =
      companyData;

    const months = [
      {
        id: 1,
        month: 'january',
      },
      {
        id: 2,
        month: 'february',
      },
      {
        id: 3,
        month: 'march',
      },
      {
        id: 4,
        month: 'april',
      },
      {
        id: 5,
        month: 'may',
      },
      {
        id: 6,
        month: 'june',
      },
      {
        id: 7,
        month: 'july',
      },
      {
        id: 8,
        month: 'august',
      },
      {
        id: 9,
        month: 'september',
      },
      {
        id: 10,
        month: 'october',
      },
      {
        id: 11,
        month: 'november',
      },
      {
        id: 12,
        month: 'december',
      },
    ];

    const entradas = Object.assign(
      {},
      ...months.map(item => {
        const currentEntry = listFaturamentoMensaisEntrada.filter(
          entry => entry.mes === item.id,
        )[0];

        if (currentEntry) {
          return {
            [item.month]: {
              value: currentEntry.valor,
            },
          };
        }

        return {
          [item.month]: {
            value: 0,
          },
        };
      }),
    );

    const saidas = Object.assign(
      {},
      ...months.map(item => {
        const currentEntry = listFaturamentoMensaisSaida.filter(
          entry => entry.mes === item.id,
        )[0];

        if (currentEntry) {
          return {
            [item.month]: {
              value: currentEntry.valor,
            },
          };
        }

        return {
          [item.month]: {
            value: 0,
          },
        };
      }),
    );

    const total = Object.assign(
      {},
      ...months.map(item => {
        const entryValue = entradas[item.month].value;
        const outValue = saidas[item.month].value;

        // 15% do valor de entrada. Esse valor deve ser menor que o valor de saída
        const diffBetweenValues = entryValue * 0.15;

        if (diffBetweenValues > outValue) {
          return {
            [item.month]: {
              isStrange: true,
              value: outValue - entryValue,
            },
          };
        }

        return {
          [item.month]: {
            value: outValue - entryValue,
          },
        };
      }),
    );

    const serializedFaturamentos = [
      {
        id: 'entradas',
        title: 'ENTRADAS',
        ...entradas,
      },
      {
        id: 'saidas',
        title: 'SAÍDAS',
        ...saidas,
      },
      {
        id: 'total',
        title: 'TOTAL',
        ...total,
      },
    ];

    const serializedData = {
      ...companyData,
      simplesNacional: serializedSimplesNacional,
      faturamentosMensais: serializedFaturamentos,
    };

    dispatch(setCompanyData(serializedData));

    if (isOutFromSimples) {
      dispatch(setCompanyIsOutFromSimples(false));
    }
  } catch (error) {
    const { message } = error;

    notification.error({
      message: 'Erro',
      description: message,
    });

    // Caso seja um erro causado pelo estouro do limite do Simples Nacional
    if (message.toLowerCase().includes('limite')) {
      dispatch(setCompanyIsOutFromSimples(true));
      dispatch(setCompanyData(companyData));
    }
  }
};

export const changeCompanySE = companyId => async (dispatch, getState) => {
  const { companies } = getState().companiesState;

  const foundCompany = companies.filter(company => company.id === companyId)[0];
  const {
    id,
    name,
    alias,
    cnpj,
    primaryActivity: { code, anexo },
    integradoEstoque,
  } = foundCompany;

  const annex = anexo.replace('(*)', '').trim();
  const companyType = checkCompanyType(foundCompany);

  dispatch(
    setCompanyInfo({
      id,
      name,
      alias,
      cnpj,
      annex,
      primaryCode: code,
      hasStock: integradoEstoque,
      companyType,
    }),
  );
};

export default reducer;
