/**
 * @function checkCompanyType
 * Verifica qual é o tipo da empresa de acordo com os campos "Servico" e "Transportadora"
 *
 * @param {object} empresa Todos os dados da empresa
 *
 * @returns {string} Tipo da empresa (Comum, Serviço ou Transporte)
 *
 * @example
 *   checkCompanyType(Anexo II)
 *   // => comum
 */
export const checkCompanyType = empresa => {
  const { servico, transportadora } = empresa;

  let companyType = 'comum';

  if (servico) {
    companyType = 'servico';
  } else if (transportadora) {
    companyType = 'transporte';
  }

  return companyType;
};
