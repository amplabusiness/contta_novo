using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities;
using Corporate.Contta.Schedule.Domain.Entities.CertificateAgg;
using Corporate.Contta.Schedule.Domain.Entities.ConfigurationFhAgg;
using Corporate.Contta.Schedule.Domain.Entities.DashboardAgg;
using Corporate.Contta.Schedule.Domain.Entities.DashboardAgg.Apuracoes;
using Corporate.Contta.Schedule.Domain.Entities.Imporsto;
using Corporate.Contta.Schedule.Domain.Entities.ImpostoAgg;
using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using Corporate.Contta.Schedule.Domain.Entities.Product;
using Corporate.Contta.Schedule.Domain.Entities.ServicoEntityAgg;
using Corporate.Contta.Schedule.Domain.Entities.UserAgg;
using Corporate.Contta.Schedule.Infra.Repositories.Base;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Infra.Repositories
{
    public class ApuracaoRepository : BaseRepository<Apuracao>, IApuracaoRepository
    {
        private static MongoDBContext<Apuracao> _dbContext = new MongoDBContext<Apuracao>();
        private static MongoDBContext<NFE> _dbContextNf = new MongoDBContext<NFE>();
        private static MongoDBContext<ServicoEntity> _dbContextService = new MongoDBContext<ServicoEntity>();
        private static MongoDBContext<Produtos> _dbContextProd = new MongoDBContext<Produtos>();
        private static MongoDBContext<TbCFOP> _dbContexCfop = new MongoDBContext<TbCFOP>();
        private static MongoDBContext<ProdutosFornecedor> _dbContextProdFornec = new MongoDBContext<ProdutosFornecedor>();
        private static MongoDBContext<Home> _dbContextHome = new MongoDBContext<Home>();
        private static MongoDBContext<Certificado> _dbContextCertificado = new MongoDBContext<Certificado>();
        private static MongoDBContext<ConfigurationFh> _dbContextConfFh = new MongoDBContext<ConfigurationFh>();
        private static MongoDBContext<CompanyInformation> _dbContextCompany = new MongoDBContext<CompanyInformation>();
        private static MongoDBContext<FaturamentoEmpresa> _dbContextFaturamentoEmpresa = new MongoDBContext<FaturamentoEmpresa>();
        private static MongoDBContext<User> _dbContextUser = new MongoDBContext<User>();
        private static MongoDBContext<TbCfopGeral> _dbContextCfop = new MongoDBContext<TbCfopGeral>();

        public ApuracaoRepository() : base(_dbContext) { }

        public async Task<List<Apuracao>> GetAllApuracao(string userToken, DateTime dataEmissao, Guid companyId)
        {
            IMongoClient mongoClient = new MongoClient("mongodb://contta:contta123456@192.46.218.34:27017/?authSource=admin&readPreference=primary&ssl=false");
            IMongoDatabase database = mongoClient.GetDatabase("conttadb");
            IMongoCollection<TbCFOP> _collectionTbCFOP = database.GetCollection<TbCFOP>("TbCFOP");

            var listCfopSimplesDTO = _collectionTbCFOP.Find(c => c.CFOP > 0).ToList();
            //var listCfopSimplesDTO = _dbContexCfop.GetColection.Find(c => c.CFOP > 0).ToList();
            var listCfopSimplesDTOO = listCfopSimplesDTO.Select(c => c.CFOP).ToList();

            List<double> listVlProdVenda = new List<double>();
            List<double> listVlTlDescontoVenda = new List<double>();
            List<double> VlTlSeguroVenda = new List<double>();
            List<double> VlTlFreteVenda = new List<double>();
            List<double> listCfop = new List<double>();

            List<double> listVlProdCompra = new List<double>();
            List<double> listVlTlDescontoCompra = new List<double>();
            List<double> VlTlSeguroCompra = new List<double>();
            List<double> VlTlFreteCompra = new List<double>();

            var inicioMes = new DateTime(dataEmissao.Year, dataEmissao.Month, 1);
            var fimMes = inicioMes.AddMonths(1).AddDays(-1);
            List<Apuracao> listApuracaoAgg = new List<Apuracao>();
            List<ApuracaoSaida> listApuracaoSaida = new List<ApuracaoSaida>();
            List<ApuracaoEntrada> listApuracaoEntrada = new List<ApuracaoEntrada>();

            List<NFE> listNfeVenda = new List<NFE>();
            List<NFE> listNfeEntrada = new List<NFE>();
            List<ProdutosFornecedor> listProdutosFornecedor = new List<ProdutosFornecedor>();

            List<double> listaProdutosFim = new List<double>();


            try
            {
                var queryNfeVendas = from nfeVendas in _dbContextNf.GetColection.AsQueryable().Where(c => c.CompanyInformation == companyId && c.DhEmi.HasValue && c.DhEmi.Value >= inicioMes && c.DhEmi.Value <= fimMes && c.ModeloTipo == "Venda" && c.Modelo == "55").ToList()
                                     join produtos in _dbContextProd.GetColection.AsQueryable().Where(c => c.CompanyInformation == companyId).ToList() on nfeVendas.Id equals produtos.NfeId
                                     where /*listCfopSimplesDTO.Any(c => c.CFOP == produtos.Cfop) &&*/ nfeVendas.ModeloTipo == "Venda"
                                     select new NotasPorProdutos { Nfe = nfeVendas, Produtos = produtos };

                var queryNfeEntrada = from nfeEntrada in _dbContextNf.GetColection.AsQueryable().Where(c => c.CompanyInformation == companyId && c.DhEmi.HasValue && c.DhEmi.Value >= inicioMes && c.DhEmi.Value <= fimMes && c.ModeloTipo == "Entrada" && c.Modelo == "55").ToList()
                                      join produtosFornec in _dbContextProdFornec.GetColection.AsQueryable().Where(c => c.CompanyInformation == companyId).ToList() on nfeEntrada.Id equals produtosFornec.NfeId
                                      where nfeEntrada.ModeloTipo == "Entrada"
                                      select new NotasPorProdutos { Nfe = nfeEntrada, ProdutosFornec = produtosFornec };


                var listaCfopProdutosEntarda = queryNfeEntrada.Select(c => c.ProdutosFornec).Select(c => c.Cfop).Distinct().ToList();
                var listaCfopProdutosSaida = queryNfeVendas.Select(c => c.Produtos).Select(c => c.Cfop).Distinct().ToList();
                var listaCfop = _dbContextCfop.GetColection.Find(c => c.Descricao != null).ToList();

                foreach (var cfop in listaCfopProdutosSaida)
                {
                    GetCfop getCfop = new GetCfop();

                      var calculoSimples = false;
                    var listaCfopDevo = getCfop.GetListaCfopDevolucaoVendas();

                    if (listCfopSimplesDTO.Any(c => c.CFOP == cfop))
                    {
                        calculoSimples = true;
                    }

                    if(listaCfopDevo.Any(c => c == cfop))
                    {
                        calculoSimples = true;
                    }

                    var produto = queryNfeVendas.Select(x => x.Produtos).Where(c => c.Cfop == cfop).ToList();

                    var result = queryNfeVendas.Where(c => c.Produtos.Cfop == cfop ).Select(x => x.Nfe.VtTotalNfe).Distinct().ToList();

                    List<NFE> lstAgrupadoSaida = queryNfeVendas.Where(c => c.Produtos.Cfop == cfop && c.Nfe.CompanyInformation.Equals(companyId))
                                                   .GroupBy(i => i.Nfe.Id)                                                   
                                                   .Select(j => new NFE()
                                                   {
                                                       Id = j.First().Nfe.Id,
                                                       VlIpi = j.First().Nfe.VlIpi,
                                                       VlOutDes = j.First().Nfe.VlOutDes,
                                                       VtIcmsSt = j.First().Nfe.VtIcmsSt,
                                                       VtTotalNfe = j.First().Nfe.VtTotalNfe,
                                                       TotalFrete =j.First().Produtos.VlTlFrete,
                                                       TotalSeguro = j.First().Produtos.VlTlSeguro,
                                                       TotalDesconto = j.First().Produtos.VlTlDesconto,
                                                       TotalProdutos = j.First().Produtos.VlProduto                                                      

                                                   })
                                                   .ToList();

                    var teste = lstAgrupadoSaida.Select(c => c.VtTotalNfe).ToList();

                    var descCfop = "";
                    var descricaoCfopDto = listaCfop.Where(c => c.Codigo == cfop).FirstOrDefault();
                    if (descricaoCfopDto != null)
                        descCfop = descricaoCfopDto.Descricao;
                    listaProdutosFim.Add(cfop);

                    listApuracaoSaida.Add(new ApuracaoSaida
                    {
                        LintNfeId = queryNfeVendas.Where(c => c.Produtos.Cfop == cfop).Select(x => x.Nfe.Id).Distinct().ToList(),
                        QtdNfe = lstAgrupadoSaida.Count(),
                        TotalProdutos = lstAgrupadoSaida.Sum(qv => qv.TotalProdutos),
                        TotalDesconto = lstAgrupadoSaida.Sum(c => c.TotalDesconto),
                        TotalSeguro = lstAgrupadoSaida.Sum(c => c.TotalSeguro),
                        TotalFrete = lstAgrupadoSaida.Sum(c => c.TotalFrete),
                        TotalIpi = lstAgrupadoSaida.Sum(c => c.VlIpi),
                        Outros = lstAgrupadoSaida.Sum(c => c.VlOutDes),
                        TotalIcmsSt = lstAgrupadoSaida.Sum(c => c.VtIcms),
                        TotalNfe = result.Sum(c => c),
                        Cfop = cfop,
                        CalculoSimples = calculoSimples,
                        DescricaoCfop = descCfop
                    });                                      
                }

                var idsEntrada = new List<Guid>();

                foreach (var item in listaCfopProdutosEntarda)
                {
                    List<NFE> lstAgrupadoEntrada = queryNfeEntrada.Where(c => c.ProdutosFornec.Cfop == item)
                                                        .GroupBy(i => i.Nfe.Id)
                                                        .Select(j => new NFE()
                                                        {
                                                            Id = j.First().Nfe.Id,
                                                            VlIpi = j.First().Nfe.VlIpi,
                                                            VlOutDes = j.First().Nfe.VlOutDes,
                                                            VtIcmsSt = j.First().Nfe.VtIcmsSt,
                                                            VtTotalNfe = j.First().Nfe.VtTotalNfe
                                                        })
                                                        .ToList();

                    var descCfop = "";
                    var descricaoCfopDto = listaCfop.Where(c => c.Codigo == item).FirstOrDefault();
                    if (descricaoCfopDto != null)
                        descCfop = descricaoCfopDto.Descricao;
                    listApuracaoEntrada.Add(new ApuracaoEntrada
                    {
                        LintNfeId = queryNfeEntrada.Where(c => c.ProdutosFornec.Cfop == item).Select(x => x.Nfe.Id).Distinct().ToList(),

                        QtdNfe = queryNfeEntrada.Where(c => c.ProdutosFornec.Cfop == item).Select(x => x.Nfe.Id).Distinct().Count(),

                        TotalProdutos = queryNfeEntrada.Where(c => c.ProdutosFornec.Cfop == item).Select(qv => qv.ProdutosFornec).Sum(qv => qv.VlProduto),
                        TotalDesconto = queryNfeEntrada.Where(c => c.ProdutosFornec.Cfop == item).Select(qv => qv.ProdutosFornec).Sum(qv => qv.VlTlDesconto),
                        TotalSeguro = queryNfeEntrada.Where(c => c.ProdutosFornec.Cfop == item).Select(qv => qv.ProdutosFornec).Sum(qv => qv.VlTlSeguro),
                        TotalFrete = queryNfeEntrada.Where(c => c.ProdutosFornec.Cfop == item).Select(qv => qv.ProdutosFornec).Sum(qv => qv.VlTlFrete),
                        TotalIpi = lstAgrupadoEntrada.Sum(qv => qv.VlIpi),
                        Outros = lstAgrupadoEntrada.Sum(qv => qv.VlOutDes),
                        TotalIcmsSt = lstAgrupadoEntrada.Sum(qv => qv.VtIcmsSt),
                        TotalIcmsDesc = lstAgrupadoEntrada.Sum(qv => qv.VtIcmsSt),
                        TotalNfe = lstAgrupadoEntrada.Sum(qv => qv.VtTotalNfe),
                        Cfop = item,
                        DescricaoCfop = descCfop


                    });
                }


                listApuracaoAgg.Add(new Apuracao() { ApuracaoSaida = listApuracaoSaida, ApuracaoEntrada = listApuracaoEntrada });


                return listApuracaoAgg;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ApuracaoCte> GetAllApuracaoCte(string companyIdUser, DateTime dataEmissao, Guid companyId)
        {
            var inicioMes = new DateTime(dataEmissao.Year, dataEmissao.Month, 1);
            var fimMes = inicioMes.AddMonths(1).AddDays(-1);
            var cteDto = new ApuracaoCte();

            var queryNfeVendas = _dbContextNf.GetColection.Find(c => c.CompanyInformation == companyId && c.DhEmi.HasValue && c.DhEmi.Value >= inicioMes && c.DhEmi.Value <= fimMes && c.Modelo == "57").ToList();

            var listaNfeVendas = queryNfeVendas.Where(c => c.ModeloTipo == "Venda").ToList();
            var listaNfeEntarda = queryNfeVendas.Where(c => c.ModeloTipo == "Entrada").ToList();

            foreach (var item in listaNfeVendas)
            {
                cteDto.ApuracaoSaida.Cnpj = item.CNPJEmitente;
                cteDto.ApuracaoSaida.Estado = item.UFEnv;
                cteDto.ApuracaoSaida.NumNfe = item.Nnfe.ToString();
                cteDto.ApuracaoSaida.TipoCte = item.NatOperacao;
                cteDto.ApuracaoSaida.TipoFrete = "";
                cteDto.ApuracaoSaida.TotalNfe = listaNfeVendas.Select(c => c.VPrest).Sum();
                cteDto.ApuracaoSaida.VlDesconto = listaNfeVendas.Select(c => c.VlTotalDesc).Sum();
                cteDto.ApuracaoSaida.VlIcms = listaNfeVendas.Select(c => c.VtIcms).Sum();
                cteDto.ApuracaoSaida.VlTotalServico = listaNfeVendas.Select(c => c.VtTotalNfe).Sum();

                break;
            }

            foreach (var item in listaNfeEntarda)
            {
                cteDto.ApuracaoEntrada.Cnpj = item.CNPJEmitente;
                cteDto.ApuracaoEntrada.Estado = item.UFEnv;
                cteDto.ApuracaoEntrada.NumNfe = item.Nnfe.ToString();
                cteDto.ApuracaoEntrada.TipoCte = item.NatOperacao;
                cteDto.ApuracaoEntrada.TipoFrete = "";
                cteDto.ApuracaoEntrada.TotalNfe = listaNfeEntarda.Select(c => c.VPrest).Sum();
                cteDto.ApuracaoEntrada.VlDesconto = listaNfeEntarda.Select(c => c.VlTotalDesc).Sum();
                cteDto.ApuracaoEntrada.VlIcms = listaNfeEntarda.Select(c => c.VtIcms).Sum();
                cteDto.ApuracaoEntrada.VlTotalServico = listaNfeEntarda.Select(c => c.VtTotalNfe).Sum();

                break;
            }

            return cteDto;
        }

        public async Task<ApuracaoService> GetAllApuracaoServico(string companyIdUser, DateTime dataEmissao, Guid companyId)
        {
            var nfeServico = new ApuracaoService();
            var inicioMes = new DateTime(dataEmissao.Year, dataEmissao.Month, 1);
            var fimMes = inicioMes.AddMonths(1).AddDays(-1);
            var queryServiceEntrda = _dbContextService.GetColection.Find(c => c.CompanyInformation == companyId && c.DataEmissao >= inicioMes && c.DataEmissao <= fimMes && c.ModeloNota.Equals("Tomador")).ToList();
            var queryServiceSaida = _dbContextService.GetColection.Find(c => c.CompanyInformation == companyId && c.DataEmissao >= inicioMes && c.DataEmissao <= fimMes && c.ModeloNota.Equals("Prestador")).ToList();

            foreach (var item in queryServiceEntrda)
            {
                nfeServico.ApuracaoEntrada.CnpjEmitente = item.CnpjEmitente;
                nfeServico.ApuracaoEntrada.CodigoMunicipio = item.CodigoMunicipio;
                nfeServico.ApuracaoEntrada.MunicipioIncidencia = item.MunicipioIncidencia;
                nfeServico.ApuracaoEntrada.ValorToatlInss = queryServiceEntrda.Select(c => c.ValorIss).Sum();
                nfeServico.ApuracaoEntrada.ValorTotalDeducoes = queryServiceEntrda.Select(c => c.ValorDeducoes).Sum();
                nfeServico.ApuracaoEntrada.ValorTotalServicos = queryServiceEntrda.Select(c => c.ValorServicos).Sum();
                nfeServico.ApuracaoEntrada.ValorTotalIss = queryServiceEntrda.Select(c => c.ValorIss).Sum();
            }

            foreach (var item in queryServiceSaida)
            {
                nfeServico.ApuracaoSaida.CnpjEmitente = item.CnpjEmitente;
                nfeServico.ApuracaoSaida.CodigoMunicipio = item.CodigoMunicipio;
                nfeServico.ApuracaoSaida.MunicipioIncidencia = item.MunicipioIncidencia;
                nfeServico.ApuracaoSaida.ValorToatlInss = queryServiceSaida.Select(c => c.ValorIss).Sum();
                nfeServico.ApuracaoSaida.ValorTotalDeducoes = queryServiceSaida.Select(c => c.ValorDeducoes).Sum();
                nfeServico.ApuracaoSaida.ValorTotalServicos = queryServiceSaida.Select(c => c.ValorServicos).Sum();
                nfeServico.ApuracaoSaida.ValorTotalIss = queryServiceSaida.Select(c => c.ValorIss).Sum();

            }

            return nfeServico;
        }

        public async Task<List<Home>> GetHome(string userId, DateTime dataOperacao)
        {
            try
            {
                var listcompanyInformation = new List<CompanyInformation>();

                var listaCOmpnayDto = _dbContextCompany.GetColection.Find(_ => _.Active).ToList();

                var user = _dbContextUser.GetColection.Find(c => c.Id.Equals(userId)).FirstOrDefault();

                if (user.Group == Domain.Enum.UserGroup.Administrator)
                    listcompanyInformation = listaCOmpnayDto.Where(c => c.ListUserId.Contains(userId.ToString())).ToList();
                else
                {
                    foreach (var item in user.CompanyId)
                    {
                        var empresaDto = listaCOmpnayDto.Where(c => c.Id.Equals(item.Value)).FirstOrDefault();
                        if (empresaDto != null)
                            listcompanyInformation.Add(empresaDto);
                    }
                }


                var listHome = new List<Home>();
                var homeCad = new Home();
                //listcompanyInformation = _dbContextCompany.GetColection.Find(c => c.ListUserId.Contains(userId)).ToList();
                var listHomeDto = _dbContextHome.GetColection.Find(c => c.CompanyId.Value != null).ToList();
                var listaCertificado = _dbContextCertificado.GetColection.Find(c => c.DataCadastro != null).ToList();
                var listFechamento = _dbContextConfFh.GetColection.Find(c => c.CompanyInformation != null).ToList();
                var listFaturamentoEmpresa = _dbContextFaturamentoEmpresa.GetColection.Find(c => c.FaturamentoFechado).ToList();

                foreach (var item in listcompanyInformation)
                {
                    var certificadoDto = listaCertificado.FirstOrDefault(c => c.CNPJ.Trim() == item.Cnpj);

                    var fechamentoSimples = listFechamento.FirstOrDefault(c => c.CompanyInformation == item.Id);

                    var fechamentoAnul = listFaturamentoEmpresa.FirstOrDefault(c => c.CompanyInformation == item.Id);

                    var listHomeCriado = listHomeDto.Where(c => c.CompanyId.Equals(item.Id)).Select(c => c.DataFechamento).ToList();

                    if (listHomeCriado.Count > 0)
                    {
                        homeCad = listHomeDto.Where(c => c.CompanyId.Equals(item.Id) && c.DataFechamento.Year == dataOperacao.Year && c.DataFechamento.Month == dataOperacao.Month).FirstOrDefault();
                    }
                    if (homeCad == null)
                    {
                        homeCad = listHomeDto.Where(c => c.CompanyId.Equals(item.Id)).FirstOrDefault();
                    }

                    if (homeCad != null)
                    {
                        var homeDtodas = new Home();
                        homeDtodas.Aliquota = homeCad.Aliquota;
                        homeDtodas.Das = homeCad.Das;
                        homeDtodas.Declaracao = homeCad.Declaracao;
                        homeDtodas.Extrato = homeCad.Extrato;
                        homeDtodas.CompanyId = item.Id;
                        homeDtodas.Status = homeCad.Status;
                        homeDtodas.Faturamento = homeCad.Faturamento;

                        homeDtodas.RazaoSocial = item.Name;
                        if (fechamentoSimples != null)
                        {
                            homeDtodas.DataFechamento = fechamentoSimples.FechamentoSimples.DataFechamento;
                        }
                        else
                        {
                            homeDtodas.DataFechamento = dataOperacao;
                        }
                        if (certificadoDto != null)
                        {
                            homeDtodas.ValidadeCertificado = certificadoDto.NonAfter.ToString();
                        }
                        else
                        {
                            homeDtodas.ValidadeCertificado = "";
                        }

                        listHome.Add(homeDtodas);

                    }

                    var empresaHomeCad = listHome.Where(c => c.CompanyId.Equals(item.Id)).FirstOrDefault();
                    if (empresaHomeCad == null)
                    {
                        var homeDto = new Home();
                        homeDto.Aliquota = 0;
                        homeDto.Das = false;
                        homeDto.Declaracao = false;
                        homeDto.Extrato = false;
                        homeDto.CompanyId = item.Id;
                        homeDto.Status = false;

                        homeDto.RazaoSocial = item.Name;
                        if (fechamentoSimples != null)
                        {
                            homeDto.DataFechamento = fechamentoSimples.FechamentoSimples.DataFechamento;
                        }
                        else
                        {
                            homeDto.DataFechamento = dataOperacao;
                        }
                        if (certificadoDto != null)
                        {
                            homeDto.ValidadeCertificado = certificadoDto.NonAfter.ToString();
                        }
                        else
                        {
                            homeDto.ValidadeCertificado = "";
                        }
                        if (fechamentoAnul != null)
                        {
                            homeDto.Faturamento = fechamentoAnul.Faturamentos.Sum(c => c.ValorFaturamento);

                        }
                        else
                        {
                            homeDto.Faturamento = 0;
                        }

                        listHome.Add(homeDto);

                    }
                }

                return listHome;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<bool> NewHome(double aliquata, double fat12Messes, Guid companyId, DateTime dataEm)
        {
            try
            {
                var homeDbDto = _dbContextHome.GetColection.Find(c => c.CompanyId.Equals(companyId)).ToList();
                var homeDb = homeDbDto.Where(c => c.DataFechamento.Year == dataEm.Year && c.DataFechamento.Month == dataEm.Month).FirstOrDefault();
                if (homeDb == null)
                {
                    var empresaDto = _dbContextCompany.GetColection.Find(c => c.Id.Equals(companyId)).FirstOrDefault();
                    var certificado = _dbContextCertificado.GetColection.Find(c => c.CNPJ == empresaDto.Cnpj).FirstOrDefault();
                    var fechamento = _dbContextConfFh.GetColection.Find(c => c.CompanyInformation.Equals(companyId)).FirstOrDefault();

                    var homeDto = new Home();
                    homeDto.Aliquota = aliquata;
                    homeDto.Das = false;
                    homeDto.Declaracao = false;
                    homeDto.Extrato = false;
                    homeDto.Faturamento = fat12Messes;
                    homeDto.CompanyId = companyId;
                    homeDto.DataFechamento = fechamento.FechamentoSimples.DataFechamento;
                    homeDto.Status = false;
                    homeDto.ValidadeCertificado = null;
                    homeDto.RazaoSocial = empresaDto.Name;
                    homeDto.Id = Guid.NewGuid();
                    homeDto.DataFechamento = dataEm;

                    _dbContextHome.GetColection.InsertOne(homeDto);

                }
                else
                {
                    var empresaDto = _dbContextCompany.GetColection.Find(c => c.Id.Equals(companyId)).FirstOrDefault();
                    var certificado = _dbContextCertificado.GetColection.Find(c => c.CNPJ == empresaDto.Cnpj).FirstOrDefault();
                    var fechamento = _dbContextConfFh.GetColection.Find(c => c.CompanyInformation.Equals(companyId)).FirstOrDefault();

                    var update = Builders<Home>.Update.Set(c => c.Aliquota, aliquata)
                                                      .Set(c => c.Faturamento, fat12Messes)
                                                      .Set(c => c.DataFechamento, dataEm);

                    var updateResult = await _dbContextHome.GetColection.UpdateOneAsync(c => c.Id == homeDb.Id, update);
                    if (updateResult.ModifiedCount > 0)
                    {

                    }
                }

                return true;
            }
            catch (Exception ex)
            {

                return false; ;
            }

        }
    }
}
