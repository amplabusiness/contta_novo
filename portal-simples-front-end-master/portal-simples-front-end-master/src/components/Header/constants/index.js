const PAGE_TITLES = {
  '/ajusteApuracao': 'Ajuste da Apuração',
  '/apuracao': 'Apuração',
  '/apuracao/listagemProdutos': 'Listagem de Produtos',
  '/blocoE': 'Blocos E - ICMS Próprio',
  '/configuracaoUsuario': 'Configuração do Usuário',
  '/confirmarNotas': 'Confirmação',
  '/consultaNotasFiscais': 'Consulta de Notas Fiscais',
  '/dashboard': 'Dashboard',
  '/dashboard/comum': 'Dados das Notas Fiscais',
  '/dashboard/detalhamento': 'Detalhamento do Simples',
  '/dashboard/servicos': 'Notas de Serviços',
  '/dashboard/transporte': 'Notas de Transporte',
  '/empresas': 'Empresas',
  '/home': 'Home',
  '/icmsST': 'ICMS/ST',
  '/modeloNotaFiscal': 'Escolha do Modelo da Nota Fiscal',
  '/modeloNotaFiscal/servico': 'Nota Fiscal de Serviço',
  '/modeloNotaFiscal/venda': 'Nota Fiscal de Venda',
  '/pisCofins': 'PIS/Cofins',
  '/produtos': 'Produtos',
  '/reclassificacaoProdutos': 'Reclassificação dos Produtos',
  '/uploadXmls': 'Upload Manual de XMLs',
  '/usuarios': 'Usuários',
};

const NON_CONSTANT_PAGE_TITLES = {
  '/dadosEmpresa': 'Dados da Empresa',
  '/edicaoUsuario': 'Edição do Usuário',
  '/empresasSocio': 'Empresas do Sócio',
};

export const getPageTitle = pathname => {
  let pageTitle = PAGE_TITLES[pathname];

  if (pageTitle) {
    return pageTitle;
  }

  Object.entries(NON_CONSTANT_PAGE_TITLES).forEach(([key, value]) => {
    if (pathname.includes(key)) {
      pageTitle = value;
    }
  });

  return pageTitle;
};
