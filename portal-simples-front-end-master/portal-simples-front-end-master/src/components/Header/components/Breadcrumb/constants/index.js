const BREADCRUMB_NAMES = {
  ajusteApuracao: 'Ajustes',
  apuracao: 'Apuração',
  blocoE: 'Blocos',
  comum: 'Comum',
  configuracaoUsuario: 'Configuração',
  confirmarNotas: 'Confirmação',
  consultaNotasFiscais: 'Consulta',
  dadosEmpresa: 'Dados da Empresa',
  dashboard: 'Dashboard',
  detalhamento: 'Detalhamento',
  empresas: 'Empresas',
  empresasSocio: 'Empresas do Sócio',
  home: 'Home',
  icmsST: 'ICMS/ST',
  listagemProdutos: 'Listagem',
  modeloNotaFiscal: 'Escolha do Modelo',
  notasFiscais: 'Notas Fiscais',
  pisCofins: 'PIS/Cofins',
  produtos: 'Produtos',
  reclassificacaoProdutos: 'Reclassificação',
  servico: 'Nota Fiscal - Serviço',
  servicos: 'Serviços',
  transporte: 'Transporte',
  uploadXmls: 'Upload - XMLs',
  usuarios: 'Usuários',
  venda: 'Nota Fiscal - Venda',
};

const NON_CONSTANT_BREADCRUMB_NAMES = {
  dadosEmpresa: 'Dados da Empresa',
  edicaoUsuario: 'Edição do Usuário',
  empresasSocio: 'Empresas do Sócio',
};

export const getBreadcrumbTitle = pathname => {
  let breadcrumbTitle = BREADCRUMB_NAMES[pathname];

  if (breadcrumbTitle) {
    return breadcrumbTitle;
  }

  Object.entries(NON_CONSTANT_BREADCRUMB_NAMES).forEach(([key, value]) => {
    if (pathname.includes(key)) {
      breadcrumbTitle = value;
    }
  });

  return breadcrumbTitle;
};
