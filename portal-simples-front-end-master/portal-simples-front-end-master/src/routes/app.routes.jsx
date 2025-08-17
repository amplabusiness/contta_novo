import { lazy } from 'react';
import { Switch, Redirect } from 'react-router-dom';

import CustomRoute from '@/routes/components/CustomRoute';

const AjusteApuracao = lazy(() => import('@/pages/app/AjusteApuracao'));
const Apuracao = lazy(() => import('@/pages/app/Apuracao'));
const BlocoE = lazy(() => import('@/pages/app/BlocoE'));
const ConfiguracaoUsuario = lazy(() =>
  import('@/pages/app/ConfiguracaoUsuario'),
);
const ConsultaNotasFiscais = lazy(() =>
  import('@/pages/app/ConsultaNotasFiscais'),
);
const DadosEmpresa = lazy(() => import('@/pages/app/DadosEmpresa'));
const Dashboard = lazy(() => import('@/pages/app/Dashboard'));
const DetalhamentoSimples = lazy(() =>
  import('@/pages/app/DetalhamentoSimples'),
);
const EdicaoUsuarioVinculado = lazy(() =>
  import('@/pages/app/EdicaoUsuarioVinculado'),
);
const Empresas = lazy(() => import('@/pages/app/Empresas'));
const EmpresasSocio = lazy(() => import('@/pages/app/EmpresasSocio'));
const EscolhaModeloNotaFiscal = lazy(() =>
  import('@/pages/app/PreenchimentoNotaFiscal'),
);
const Home = lazy(() => import('@/pages/app/Home'));
const IcmsSt = lazy(() => import('@/pages/app/IcmsSt'));
const ListagemProdutos = lazy(() => import('@/pages/app/ListagemProdutos'));
const NotaFiscalServico = lazy(() =>
  import('@/pages/app/PreenchimentoNotaFiscal/pages/ModeloServico'),
);
const NotaFiscalVenda = lazy(() =>
  import('@/pages/app/PreenchimentoNotaFiscal/pages/ModeloVenda'),
);
const NotasFiscaisComum = lazy(() => import('@/pages/app/NotasFiscaisComum'));
const NotasFiscaisServico = lazy(() =>
  import('@/pages/app/NotasFiscaisServico'),
);
const NotasFiscaisTransporte = lazy(() =>
  import('@/pages/app/NotasFiscaisTransporte'),
);
const PisCofins = lazy(() => import('@/pages/app/PisCofins'));
const Produtos = lazy(() => import('@/pages/app/Produtos'));
const ReclassificacaoProdutos = lazy(() =>
  import('@/pages/app/ReclassificacaoProdutos'),
);
const UploadXmls = lazy(() => import('@/pages/app/UploadXmls'));
const UsuariosVinculados = lazy(() => import('@/pages/app/UsuariosVinculados'));

const LoggedRoutes = () => {
  return (
    <Switch>
      <CustomRoute
        exact
        path="/ajusteApuracao"
        title="Contta Simples - Ajuste da Apuração"
        shouldRenderOnlyWithActiveCompany
        component={AjusteApuracao}
      />
      <CustomRoute
        exact
        path="/apuracao"
        title="Contta Simples - Apuração"
        shouldRenderOnlyWithActiveCompany
        component={Apuracao}
      />
      <CustomRoute
        exact
        path="/blocoE"
        title="Contta Simples - Bloco E"
        shouldRenderOnlyWithActiveCompany
        component={BlocoE}
      />
      <CustomRoute
        exact
        path="/configuracaoUsuario"
        title="Contta Simples - Configuração do Usuário"
        shouldRenderOnlyWithActiveCompany
        component={ConfiguracaoUsuario}
      />
      <CustomRoute
        exact
        path="/consultaNotasFiscais"
        title="Contta Simples - Consulta de NFs"
        shouldRenderOnlyWithActiveCompany
        component={ConsultaNotasFiscais}
      />
      <CustomRoute
        exact
        path="/empresas/dadosEmpresa/:id"
        title="Contta Simples - Dados da Empresa"
        component={DadosEmpresa}
      />
      <CustomRoute
        exact
        path="/dashboard"
        title="Contta Simples - Dashboard"
        shouldRenderOnlyWithActiveCompany
        component={Dashboard}
      />
      <CustomRoute
        exact
        path="/dashboard/detalhamento"
        title="Contta Simples - Detalhamento do Simples Nacional"
        component={DetalhamentoSimples}
      />
      <CustomRoute
        exact
        path="/usuarios/edicaoUsuario/:id"
        title="Contta Simples - Edição do Usuário"
        shouldRenderOnlyWithActiveCompany
        component={EdicaoUsuarioVinculado}
      />
      <CustomRoute
        exact
        path="/empresas"
        title="Contta Simples - Empresas"
        component={Empresas}
      />
      <CustomRoute
        exact
        path="/empresas/dadosEmpresa/:id/empresasSocio"
        title="Contta Simples - Empresas dos Sócios"
        component={EmpresasSocio}
      />
      <CustomRoute
        exact
        path="/modeloNotaFiscal"
        title="Contta Simples - Escolha do Modelo"
        shouldRenderOnlyWithActiveCompany
        component={EscolhaModeloNotaFiscal}
      />
      <CustomRoute
        exact
        path="/home"
        title="Contta Simples - Home"
        component={Home}
      />
      <CustomRoute
        exact
        path="/icmsST"
        title="Contta Simples - ICMS/ST"
        shouldRenderOnlyWithActiveCompany
        component={IcmsSt}
      />
      <CustomRoute
        exact
        path="/apuracao/listagemProdutos"
        title="Contta Simples - Listagem de Produtos"
        shouldRenderOnlyWithActiveCompany
        component={ListagemProdutos}
      />
      <CustomRoute
        exact
        path="/modeloNotaFiscal/servico"
        title="Contta Simples - Nota Fiscal de Serviço"
        shouldRenderOnlyWithActiveCompany
        component={NotaFiscalServico}
      />
      <CustomRoute
        exact
        path="/modeloNotaFiscal/venda"
        title="Contta Simples - Nota Fiscal de Venda"
        shouldRenderOnlyWithActiveCompany
        component={NotaFiscalVenda}
      />
      <CustomRoute
        exact
        path="/dashboard/comum"
        title="Contta Simples - Notas Fiscais"
        shouldRenderOnlyWithActiveCompany
        component={NotasFiscaisComum}
      />
      <CustomRoute
        exact
        path="/dashboard/servicos"
        title="Contta Simples - Serviços"
        component={NotasFiscaisServico}
      />
      <CustomRoute
        exact
        path="/dashboard/transporte"
        title="Contta Simples - Transporte"
        component={NotasFiscaisTransporte}
      />
      <CustomRoute
        exact
        path="/pisCofins"
        title="Contta Simples - PIS/Cofins"
        shouldRenderOnlyWithActiveCompany
        component={PisCofins}
      />
      <CustomRoute
        exact
        path="/produtos"
        title="Contta Simples - Produtos"
        shouldRenderOnlyWithActiveCompany
        component={Produtos}
      />
      <CustomRoute
        exact
        path="/reclassificacaoProdutos"
        title="Contta Simples - Reclassificação dos Produtos"
        shouldRenderOnlyWithActiveCompany
        component={ReclassificacaoProdutos}
      />
      <CustomRoute
        exact
        path="/uploadXmls"
        title="Contta Simples - Upload de XMLs"
        shouldRenderOnlyWithActiveCompany
        component={UploadXmls}
      />
      <CustomRoute
        exact
        path="/usuarios"
        title="Contta Simples - Usuários"
        shouldRenderOnlyWithActiveCompany
        component={UsuariosVinculados}
      />
      <CustomRoute
        title="Contta Simples"
        component={() => <Redirect to="/home" />}
      />
    </Switch>
  );
};

export default LoggedRoutes;
