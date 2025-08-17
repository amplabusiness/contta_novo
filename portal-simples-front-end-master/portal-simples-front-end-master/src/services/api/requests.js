import axios from 'axios';
import { appAxiosInstance, authAxiosInstance } from '@/services/api';

export const getAccessToken = async data => {
  const response = await authAxiosInstance.post(`access/getaccesstoken`, data);
  return response.data;
};

export const getNewCompany = async (cnpj, token) => {
  const response = await appAxiosInstance.get(
    `information/company/${cnpj}/${token}`,
  );
  return response.data;
};

export const getConfirmNewCompany = async (
  cnpj,
  token,
  confirmRegister,
  id,
) => {
  const response = await appAxiosInstance.get(
    `information/company/${cnpj}/${token}?confirmarCadastro=${confirmRegister}&userIdTerceiro=${id}`,
  );
  return response.data;
};

export const getAllCompanies = async token => {
  const response = await appAxiosInstance.get(`company/getall/${token}`);
  return response.data;
};

export const getCompanyDashboardValues = async (id, date) => {
  const response = await appAxiosInstance.get(
    `dashboard/getbycompany?EmpresaId=${id}&RequestDate=${date}`,
  );
  return response.data;
};

export const getCompanyDashboardAnnualIncome = async (id, date) => {
  const response = await appAxiosInstance.get(
    `company/faturamento?empresaId=${id}&dataAtual=${date}`,
  );
  return response.data;
};

export const getAllNfs = async (id, operation, date, page, remainQuery) => {
  const response = await appAxiosInstance.get(
    `nfe/GetAllNfeT?Documento=${id}&Operation=${operation}&Data=${date}&Pagina=${page}&QtdPorPagina=10&${remainQuery}`,
  );
  return response.data;
};

export const getAllServiceNfs = async (id, operation, date) => {
  const response = await appAxiosInstance.get(
    `nfe/getallNfeServico?Documento=${id}&Operation=${operation}&Data=${date}`,
  );
  return response.data;
};

export const getAllTransportNfs = async (id, operation, date) => {
  const response = await appAxiosInstance.get(
    `nfe/getallNfeMod57?Documento=${id}&Operation=${operation}&Data=${date}`,
  );
  return response.data;
};

export const getAllProductsByOption = async (id, option, date) => {
  const response = await appAxiosInstance.get(
    `produtos/getall?EmpresaId=${id}&IcmsSt=${option.includes(
      'icmsSt',
    )}&Beneficio=${option.includes('beneficio')}
    &Imune=${option.includes('imune')}&Insento=${option.includes(
      'isento',
    )}&InsencaoCesta=${option.includes(
      'isencaoReducaoCestaBasica',
    )}&Insencao=${option.includes(
      'isencaoReducao',
    )}&Monofasico=${option.includes(
      'monofasico',
    )}&DateEmiss=${date}&DeparaProd=false&Alterado=${option.includes('Edit')}`,
  );
  return response.data;
};

export const getCalculation = async (id, date, companyTypeUrl) => {
  const response = await appAxiosInstance.get(
    `nfe/getallapuracao?CompanyId=${id}&DataEmissao=${date}&${companyTypeUrl}`,
  );
  return response.data;
};

export const getUsersInfo = async token => {
  const response = await appAxiosInstance.get(
    `user/getinfomation?Token=${token}`,
  );
  return response.data;
};

export const getInitialUserConfiguration = async token => {
  const response = await authAxiosInstance.get(`configuration?token=${token}`);
  return response.data;
};

export const getReclassificationInfo = async (companyId, nfeId, operation) => {
  const response = await appAxiosInstance.get(
    `produtos/reclassificacao?EmpresaId=${companyId}&NfeId=${nfeId}&Modelo=${operation}`,
  );
  return response.data;
};

export const getCompanyInvoiceInfo = async (cnpj, token) => {
  const response = await appAxiosInstance.get(
    `information/companyDest?cnpj=${cnpj}&tokenUser=${token}&Manual=true`,
  );
  return response.data;
};

export const getDeparaProducts = async id => {
  const response = await appAxiosInstance.get(
    `produtos/depara?EmpresaId=${id}&DeparaProd=true`,
  );
  return response.data;
};

export const getProductsByFilter = async (id, searchQuery) => {
  const response = await appAxiosInstance.get(
    `produtos/codprod?Document=${id}&${searchQuery}`,
  );
  return response.data;
};

export const getNewCritics = async id => {
  const response = await appAxiosInstance.get(
    `criticas/criticasnovas?CompanyId=${id}&CriticasNovas=true&CriticasAntigas=false`,
  );
  return response.data;
};

export const getInvoicesQuery = async query => {
  const response = await appAxiosInstance.get(`nfe/consultanfe?${query}`);
  return response.data;
};

export const getConfiguration = async id => {
  const response = await appAxiosInstance.get(
    `configurationfh?empresaId=${id}`,
  );
  return response.data;
};

export const getRevenueSegregation = async (id, date) => {
  const response = await appAxiosInstance.get(
    `dashboard/detalhamentoApuracao?CompanyId=${id}&DhEmiss=${date}`,
  );
  return response.data;
};

export const getDashboardTaxes = async (id, date) => {
  const response = await appAxiosInstance.get(
    `dashboard/impdashboard?CompanyId=${id}&DhEmiss=${date}`,
  );
  return response.data;
};

export const getSendPasswordEmail = async email => {
  const response = await authAxiosInstance.get(
    `user/passwordchangerequest?email=${email}`,
  );
  return response.data;
};

export const getResetPassword = async query => {
  const response = await authAxiosInstance.get(
    `user/redefinePasswordbyemail?${query}`,
  );
  return response.data;
};

export const getAllIcmsStTaxes = async (id, option) => {
  const response = await appAxiosInstance.get(
    `impostos/getall?EmpresaId=${id}&AntecipacapTri=${option.includes(
      'antEncTributacao',
    )}&Exigibilidade=${option.includes('exigSuspensa')}&Imune=${option.includes(
      'imune',
    )}&Insento=${option.includes('isento')}&CestaBasica=${option.includes(
      'isencaoReducaoCestaBasica',
    )}&Reducao=${option.includes('isencaoReducao')}&Alterado=${option.includes(
      'Edit',
    )}`,
  );
  return response.data;
};

export const getDescriptionCodes = async () => {
  const response = await appAxiosInstance.get('nfe/tbservico');
  return response.data;
};

export const getEBlock = async (id, date) => {
  const response = await appAxiosInstance.get(
    `agblocoe?CompanyInformationId=${id}&DateInition=${date}`,
  );
  return response.data;
};

export const getComplementaryEBlock = async (id, date) => {
  const response = await appAxiosInstance.get(
    `nfe/GetRegistrosBlE?EmpresaId=${id}&Data=${date}`,
  );
  return response.data;
};

export const getCfops = async () => {
  const response = await appAxiosInstance.get('impostos/getallCfop');
  return response.data;
};

export const getProductsListing = async (type, group, nfeIds) => {
  const response = await appAxiosInstance.get(
    `dashboard/detalhamentoApuracaoicmspisnfe?Tipo=${type}&Grupo=${group}&${nfeIds}`,
  );
  return response.data;
};

export const getRegisteredCfops = async () => {
  const response = await appAxiosInstance.get('impostos/getallCfopsm');
  return response.data;
};

export const getAllSellInvoices = async id => {
  const response = await appAxiosInstance.get(
    `nfe/getallNfeVendamanual?CompanyInformation=${id}`,
  );
  return response.data;
};

export const getAllServiceInvoices = async id => {
  const response = await appAxiosInstance.get(
    `nfe/getallNfeServmanual?CompanyInformation=${id}`,
  );
  return response.data;
};

export const getHomePageCompanyInfo = async (token, date) => {
  const response = await appAxiosInstance.get(
    `dashboard/gethome?token=${token}&dataOperacao=${date}`,
  );
  return response.data;
};

export const getUserById = async id => {
  const response = await appAxiosInstance.get(`user/getUser?Id=${id}`);
  return response.data;
};

export const getAuthorizations = async id => {
  const response = await appAxiosInstance.get(
    `company/getConfig?userAdminId=${id}`,
  );
  return response.data;
};

export const getCompanyProductsSheet = async id => {
  const response = await appAxiosInstance.get(
    `produtos/download?CompanyInformation=${id}`,
    {
      responseType: 'blob',
    },
  );
  return response.data;
};

export const getDIFALValue = async (id, date) => {
  const response = await appAxiosInstance.get(
    `impostos/difal?EmpresaId=${id}&periudo=${date}`,
  );
  return response.data;
};

export const getMemberCompanies = async (memberName, memberCpf) => {
  const response = await appAxiosInstance.get(
    `socio/getSocios?nome=${memberName}&cpf=${memberCpf}`,
  );
  return response.data;
};

export const getMemberCompany = async (token, cnpj) => {
  const response = await appAxiosInstance.get(
    `socio/getall?tokenUser=${token}&cnpj=${cnpj}`,
  );
  return response.data;
};

export const getCompanyBook = async (id, date, bookType) => {
  const response = await appAxiosInstance.get(
    `nfe/getallNfeLivro?empresaId=${id}&dataOperacao=${date}&tipoFiscal=${bookType}`,
    {
      responseType: 'blob',
    },
  );
  return response.data;
};

export const getAdjustmentCodes = async () => {
  const SEARCH_API_BASE =
    process.env.REACT_APP_SEARCH_API_BASE_URL ||
    'https://contta-search-api.herokuapp.com/api';
  const response = await fetch(`${SEARCH_API_BASE}/ajusteApuracao`);

  if (!response.ok) {
    throw new Error('Network error');
  }

  return response.json();
};

export const getCalculationDetails = async () => {
  const response = await appAxiosInstance.get(`teste`);
  return response.data;
};

export const getAdjustmentsList = async id => {
  const response = await appAxiosInstance.get(
    `nfe/getallTbE?empresaId=${id}&tipoFiscal=Saída`,
  );
  return response.data;
};

export const getDashboardInvoices = async (id, date) => {
  const response = await appAxiosInstance.get(
    `nfe/gettotalizadornfesaida?EmpresaId=${id}&Data=${date}`,
  );
  return response.data;
};

export const postNewUser = async data => {
  const response = await authAxiosInstance.post(`user/newuser`, data);
  return response.data;
};

export const postCompanyDocumentUpload = async (data, token) => {
  const response = await appAxiosInstance.post(
    `upload/newextable?token=${token}`,
    data,
  );
  return response.data;
};

export const postCompanyIncome = async data => {
  const response = await appAxiosInstance.post(
    'company/newfaturamento12',
    data,
  );
  return response.data;
};

export const postManualSellInvoice = async data => {
  const response = await appAxiosInstance.post('nfe/vendamanual', data);
  return response.data;
};

export const postManualServiceInvoice = async data => {
  const response = await appAxiosInstance.post('nfe/servicomanual', data);
  return response.data;
};

export const postNewIcmsStTax = async (data, selectedTax) => {
  const response = await appAxiosInstance.post(`impostos/${selectedTax}`, data);
  return response.data;
};

export const postEBlockAdjustment = async data => {
  const response = await appAxiosInstance.post('nfe/ajusteBlocoE', data);
  return response.data;
};

export const postUploadProductsSheet = async (id, data) => {
  const response = await appAxiosInstance.post(
    `upload/newextableEstoque?empresaId=${id}`,
    data,
    {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    },
  );
  return response.data;
};

export const postUploadXmlFiles = async data => {
  const response = await appAxiosInstance.post('upload/xml', data);
  return response.data;
};

export const postNewCfop = async data => {
  const response = await appAxiosInstance.post('impostos/newCfop', data);
  return response.data;
};

export const postHomePageCompanyInfo = async data => {
  const response = await appAxiosInstance.post(`dashboard/newHome?${data}`);
  return response.data;
};

export const postParseJsonToExcel = async data => {
  const EXCEL_API_BASE =
    process.env.REACT_APP_EXCEL_API_BASE_URL ||
    'https://contta-excel-parser.herokuapp.com/api/v1';
  const response = await axios.post(`${EXCEL_API_BASE}/parse-json`, data, {
    responseType: 'arraybuffer',
  });
  return response.data;
};

export const postGenerateEBlockData = async (codes, debitBalanceValue) => {
  const response = await appAxiosInstance.post(
    `nfe/GetRegistrosBlE?valorSaldoDevedor=${debitBalanceValue}`,
    codes,
  );
  return response.data;
};

export const postRegisterEBlock = async data => {
  const response = await appAxiosInstance.post('agblocoe', data);
  return response.data;
};

export const patchUserConfiguration = async data => {
  const response = await appAxiosInstance.patch('configurationfh', data);
  return response.data;
};

export const putUpdateUser = async data => {
  const response = await appAxiosInstance.put(`user/updateuser`, data);
  return response;
};

export const putUpdateRegimeProducts = async (id, product) => {
  const response = await appAxiosInstance.put(
    `produtos/listupdateproduto?EmpresaId=${id}`,
    product,
  );
  return response.data;
};

export const putRevertRegimeProducts = async product => {
  const response = await appAxiosInstance.put(
    'produtos/updateListImpostos',
    product,
  );
  return response.data;
};

export const putUpdateDeparaProduct = async product => {
  const response = await appAxiosInstance.put(`produtos/deparaprod`, product);
  return response.data;
};

export const putOutProductsReclassification = async (companyId, products) => {
  const response = await appAxiosInstance.put(
    `produtos/listupdateproduto?EmpresaId=${companyId}`,
    products,
  );
  return response.data;
};

export const putEntryProductsReclassification = async products => {
  const response = await appAxiosInstance.put(
    'produtos/updateproduto',
    products,
  );
  return response.data;
};

export const putUpdateCompany = async data => {
  const response = await appAxiosInstance.put('company/updatecompany', data);
  return response;
};

export const putUpdateCompanyAnnex = async (id, data) => {
  const response = await appAxiosInstance.put(
    `company/anexo?empresaId=${id}`,
    data,
  );
  return response.data;
};

export const putEBlockAdjustment = async data => {
  const response = await appAxiosInstance.put('nfe/updatenfeajuste', data);
  return response.data;
};

export const putUpdateCompanyIncome = async data => {
  const response = await appAxiosInstance.put(
    'company/faturamento?fechado=true',
    data,
  );
  return response.data;
};

export const putUpdateCompanyFoundationDate = async data => {
  const response = await appAxiosInstance.put(`company/datacompany?${data}`);
  return response.data;
};

export const putUpdateUserConfiguration = async updatedConfig => {
  const response = await appAxiosInstance.put('configuration', updatedConfig);
  return response;
};

export const putUpdateIcmsStTax = async (tax, data) => {
  const response = await appAxiosInstance.put(`impostos/${tax}`, data);
  return response.data;
};

export const putSendAuthorizationRequest = async query => {
  const response = await appAxiosInstance.put(`company/updateConfig?${query}`);
  return response.data;
};

export const putConfirmAllRegimeProducts = async (id, date, data) => {
  const response = await appAxiosInstance.put(
    `produtos/confirlistprod?companyId=${id}&dhEm=${date}`,
    data,
  );
  return response.data;
};

export const deleteCompany = async (companyId, userId) => {
  const response = await appAxiosInstance.delete(
    `company/deletecompany?id=${companyId}&userId=${userId}`,
  );
  return response.data;
};

export const deleteCfop = async id => {
  const response = await appAxiosInstance.delete(
    `impostos/deleteCfop?CfopId=${id}`,
  );
  return response.data;
};

export const deleteSellInvoice = async id => {
  const response = await appAxiosInstance.delete(
    `nfe/deleteNfeVendamanual?id=${id}`,
  );
  return response.data;
};

export const deleteServiceInvoice = async id => {
  const response = await appAxiosInstance.delete(
    `nfe/deleteNfeServmanual?id=${id}`,
  );
  return response.data;
};
