import { anexos, faixas } from '@/utils/simplesNacional/tables';

// Cálculo do Simples da empresa comum
export const calculateCommonSimples = ({ anexo, rendaAnual, basesCalculo }) => {
  const { baseComum = 0, baseIcms = 0, basePisCofins = 0 } = basesCalculo;

  const faixasAnexo = anexos.filter(item => item.anexo === anexo)[0];
  let faixaDaEmpresa = {};

  faixas.forEach((item, index) => {
    if (rendaAnual === 0) {
      const [primeiraFaixa] = faixasAnexo.faixas;
      faixaDaEmpresa = primeiraFaixa;
    } else if (rendaAnual > item.inicial && rendaAnual <= item.final) {
      faixaDaEmpresa = faixasAnexo.faixas[index];
    }
  });

  const { aliquota, deducao, impostos } = faixaDaEmpresa;

  const aliquotaEfetiva = (rendaAnual * aliquota - deducao) / rendaAnual || 0;

  const impostosEfetivosAliquotas = {};
  const impostosEfetivosValores = {};
  const impostosBasesCalculo = {};

  Object.keys(impostos).forEach(key => {
    const valorImposto = impostos[key];

    // TODO: quando não há o valor do imposto, existe uma regra
    // para o cálculo. Implementar essa regra de acordo com o Manual
    if (valorImposto === '-') {
      impostosEfetivosAliquotas[key] = '0,00%';
      impostosEfetivosValores[key] = 0;
    } else {
      const impostoEfetivo = (aliquotaEfetiva * parseFloat(valorImposto)) / 100;
      impostosEfetivosAliquotas[key] = `${(impostoEfetivo * 100)
        .toFixed(3)
        .replace('.', ',')}%`;

      // Cálculo dos valores do ICMS e do PIS/Cofins
      if (key === 'icms') {
        impostosEfetivosValores[key] = Number(
          (impostoEfetivo * baseIcms).toFixed(2),
        );
        impostosBasesCalculo[key] = baseIcms;
      } else if (key === 'pis' || key === 'cofins') {
        impostosEfetivosValores[key] = Number(
          (impostoEfetivo * basePisCofins).toFixed(2),
        );
        impostosBasesCalculo[key] = basePisCofins;
      } else {
        impostosEfetivosValores[key] = Number(
          (impostoEfetivo * baseComum).toFixed(2),
        );
        impostosBasesCalculo[key] = baseComum;
      }
    }
  });

  // Valor do Simples Nacional
  const valorDas = Object.values(impostosEfetivosValores).reduce(
    (acc, curr) => acc + curr,
    0,
  );

  const aliquotaEfetivaFormatada = `${(aliquotaEfetiva * 100).toFixed(
    2,
  )}%`.replace('.', ',');

  return {
    deducao,
    aliquotaEfetiva: aliquotaEfetivaFormatada,
    valorDas,
    impostos,
    impostosEfetivosAliquotas,
    impostosEfetivosValores,
    impostosBasesCalculo,
  };
};

// Cálculo do Simples da empresa de serviço
export const calculateServiceSimples = ({ anexo, rendaAnual, rendaMensal }) => {
  const faixasAnexo = anexos.filter(item => item.anexo === anexo)[0];
  let faixaDaEmpresa = {};

  faixas.forEach((item, index) => {
    if (rendaAnual === 0) {
      const [primeiraFaixa] = faixasAnexo.faixas;
      faixaDaEmpresa = primeiraFaixa;
    } else if (rendaAnual > item.inicial && rendaAnual <= item.final) {
      faixaDaEmpresa = faixasAnexo.faixas[index];
    }
  });

  const { aliquota, deducao, impostos } = faixaDaEmpresa;

  const aliquotaEfetiva = (rendaAnual * aliquota - deducao) / rendaAnual || 0;
  const valorDas = rendaMensal * aliquotaEfetiva;

  const impostosEfetivos = {};

  Object.keys(impostos).forEach(key => {
    const valorImposto = impostos[key];
    const impostoEfetivo = (aliquotaEfetiva * parseFloat(valorImposto)) / 100;
    impostosEfetivos[key] = Number((impostoEfetivo * rendaMensal).toFixed(2));
  });

  const aliquotaEfetivaFormatada = `${(aliquotaEfetiva * 100).toFixed(
    2,
  )}%`.replace('.', ',');

  return {
    deducao,
    aliquotaEfetiva: aliquotaEfetivaFormatada,
    valorDas,
    impostos,
    impostosEfetivos,
  };
};

// Cálculo do Simples da empresa de transporte estadual e/ou interestadual
export const calculateStateAndInterstateTransportSimples = ({
  rendaAnual,
  rendaMensal,
}) => {
  const faixasAnexoI = anexos.find(item => item.anexo === 'Anexo I');
  const faixasAnexoIII = anexos.find(item => item.anexo === 'Anexo III');
  let faixaEmpresaAnexoI = {};
  let faixaEmpresaAnexoIII = {};

  // Encontrado os valores do Anexo I para a faixa da empresa
  faixas.forEach((item, index) => {
    if (rendaAnual === 0) {
      const [primeiraFaixa] = faixasAnexoI.faixas;
      faixaEmpresaAnexoI = primeiraFaixa;
    } else if (rendaAnual > item.inicial && rendaAnual <= item.final) {
      faixaEmpresaAnexoI = faixasAnexoI.faixas[index];
    }
  });

  const {
    aliquota: aliquotaAnexoI,
    deducao: deducaoAnexoI,
    impostos: impostosAnexoI,
  } = faixaEmpresaAnexoI;

  const aliquotaEfetivaAnexoI =
    (rendaAnual * aliquotaAnexoI - deducaoAnexoI) / rendaAnual || 0;

  const icmsNominalAnexoI = impostosAnexoI.icms;
  const icmsEfetivoAnexoI =
    aliquotaEfetivaAnexoI * (parseFloat(icmsNominalAnexoI) / 100);
  const valorIcmsEfetivoAnexoI = Number(
    (icmsEfetivoAnexoI * rendaMensal).toFixed(2),
  );

  // Encontrado os valores do Anexo III para a faixa da empresa
  faixas.forEach((item, index) => {
    if (rendaAnual === 0) {
      const [primeiraFaixa] = faixasAnexoIII.faixas;
      faixaEmpresaAnexoIII = primeiraFaixa;
    } else if (rendaAnual > item.inicial && rendaAnual <= item.final) {
      faixaEmpresaAnexoIII = faixasAnexoIII.faixas[index];
    }
  });

  const {
    aliquota: aliquotaAnexoIII,
    deducao: deducaoAnexoIII,
    impostos: impostosAnexoIII,
  } = faixaEmpresaAnexoIII;

  const aliquotaEfetivaAnexoIII =
    (rendaAnual * aliquotaAnexoIII - deducaoAnexoIII) / rendaAnual || 0;

  const issNominalAnexoIII = impostosAnexoIII.iss;
  const issEfetivoAnexoIII =
    aliquotaEfetivaAnexoIII * (parseFloat(issNominalAnexoIII) / 100);

  const impostosEfetivosAnexoIII = {};

  Object.keys(impostosAnexoIII).forEach(key => {
    const valorImposto = impostosAnexoIII[key];
    const impostoEfetivo =
      (aliquotaEfetivaAnexoIII * parseFloat(valorImposto)) / 100;
    impostosEfetivosAnexoIII[key] = Number(
      (impostoEfetivo * rendaMensal).toFixed(2),
    );
  });

  // Encontrando os impostos finais
  const impostosEfetivosFinais = {
    ...impostosEfetivosAnexoIII,
    icms: valorIcmsEfetivoAnexoI,
  };
  delete impostosEfetivosFinais.iss;

  // Encontrando o valor real do DAS
  const aliquotaEfetivaFinal =
    aliquotaEfetivaAnexoIII - issEfetivoAnexoIII + icmsEfetivoAnexoI;
  const aliquotaEfetivaFinalFormatada = `${(aliquotaEfetivaFinal * 100).toFixed(
    2,
  )}%`.replace('.', ',');
  const valorDas = aliquotaEfetivaFinal * rendaMensal;

  return {
    aliquotaEfetiva: aliquotaEfetivaFinalFormatada,
    valorDas,
    impostosEfetivos: impostosEfetivosFinais,
  };
};

// Cálculo do Simples da empresa de transporte municipal
export const calculateMunicipalTransportSimples = ({
  rendaAnual,
  rendaMensal,
}) => {
  const faixasAnexo = anexos.find(item => item.anexo === 'Anexo III');
  let faixaDaEmpresa = {};

  faixas.forEach((item, index) => {
    if (rendaAnual === 0) {
      const [primeiraFaixa] = faixasAnexo.faixas;
      faixaDaEmpresa = primeiraFaixa;
    } else if (rendaAnual > item.inicial && rendaAnual <= item.final) {
      faixaDaEmpresa = faixasAnexo.faixas[index];
    }
  });

  const { aliquota, deducao, impostos } = faixaDaEmpresa;

  const aliquotaEfetiva = (rendaAnual * aliquota - deducao) / rendaAnual || 0;
  const valorDas = rendaMensal * aliquotaEfetiva;

  const impostosEfetivos = {};

  Object.keys(impostos).forEach(key => {
    const valorImposto = impostos[key];
    const impostoEfetivo = (aliquotaEfetiva * parseFloat(valorImposto)) / 100;
    impostosEfetivos[key] = Number((impostoEfetivo * rendaMensal).toFixed(2));
  });

  const aliquotaEfetivaFormatada = `${(aliquotaEfetiva * 100).toFixed(
    2,
  )}%`.replace('.', ',');

  return {
    aliquotaEfetiva: aliquotaEfetivaFormatada,
    valorDas,
    impostosEfetivos,
  };
};
