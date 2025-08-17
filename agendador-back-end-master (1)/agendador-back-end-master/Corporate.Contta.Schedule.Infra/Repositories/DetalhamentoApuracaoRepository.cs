using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities.DashboardAgg.Apuracoes;
using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using Corporate.Contta.Schedule.Infra.Repositories.Base;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Infra.Repositories
{
    struct NfeProd
    {
        public NFE Nfe { get; set; }
        public Produtos Produtos { get; set; }
    }
    public class DetalhamentoApuracaoRepository : IDetalhamentoApuracaoRepository
    {
        private static MongoDBContext<NFE> _dbContext = new MongoDBContext<NFE>();
        private static MongoDBContext<Produtos> _dbContextProduto = new MongoDBContext<Produtos>();
        private static MongoDBContext<NfeCanceldas> _dbContextNfCanceladas = new MongoDBContext<NfeCanceldas>();

        public async Task<DetalhamentoApuracao> GetDetalhamentoApuracao(Guid companyId, DateTime dhEmiss)
        {
            var inicioMes = new DateTime(dhEmiss.Year, dhEmiss.Month, 1);
            var fimMes = inicioMes.AddMonths(1).AddDays(-1);

            var query = from nfe in _dbContext.GetColection.AsQueryable().Where(c => c.CompanyInformation == companyId && c.DhEmi >= inicioMes && c.DhEmi <= fimMes).ToList()
                        join produtos in _dbContextProduto.GetColection.AsQueryable().Where(c=> c.Modificado).ToList() on nfe.Id equals produtos.NfeId
                        select new NfeProd {Nfe = nfe, Produtos = produtos };

            var detalhamentoApuracao = GerarDetalhamento(query,companyId,false);     

            return detalhamentoApuracao.Item1;
        }

        private Tuple<DetalhamentoApuracao, DetalhamentoImposto> GerarDetalhamento(IEnumerable<NfeProd> query, Guid companyId, bool somenteImposto)
        {
            var icmsSt = new DetalhamentoIcmsSt();
            #region detalhamentoIcmsSt
            var t = query.Select(c => c.Nfe).ToList();
            var x1 = query.Select(c => c.Produtos).ToList();
            icmsSt.IcmsSt.Total = query.Where(c => c.Produtos.IcmsSt).Sum(x => x.Produtos.VlProduto);
            icmsSt.IcmsSt.ListIdNfe = query.Where(c => c.Produtos.IcmsSt).Select(x => x.Nfe.Id).ToList();
            icmsSt.IcmsSt.ListIdProduto = query.Where(c => c.Produtos.IcmsSt).Select(x => x.Produtos.Id).ToList();

            icmsSt.AntEncTributacao.Total = query.Where(c => c.Produtos.AntEncTributacao).Sum(x => x.Produtos.VlProduto);
            icmsSt.AntEncTributacao.ListIdNfe = query.Where(c => c.Produtos.AntEncTributacao).Select(x => x.Nfe.Id).ToList();
            icmsSt.AntEncTributacao.ListIdProduto = query.Where(c => c.Produtos.AntEncTributacao).Select(x => x.Produtos.Id).ToList();

            icmsSt.Beneficios.Total = query.Where(c => c.Produtos.Beneficios).Sum(x => x.Produtos.VlProduto);
            icmsSt.Beneficios.ListIdNfe = query.Where(c => c.Produtos.Beneficios).Select(x => x.Nfe.Id).ToList();
            icmsSt.Beneficios.ListIdProduto = query.Where(c => c.Produtos.Beneficios).Select(x => x.Produtos.Id).ToList();

            icmsSt.ExigSuspensa.Total = query.Where(c => c.Produtos.ExigSuspensa).Sum(x => x.Produtos.VlProduto);
            icmsSt.ExigSuspensa.ListIdNfe = query.Where(c => c.Produtos.ExigSuspensa).Select(x => x.Nfe.Id).ToList();
            icmsSt.ExigSuspensa.ListIdProduto = query.Where(c => c.Produtos.ExigSuspensa).Select(x => x.Produtos.Id).ToList();

            icmsSt.Imune.Total = query.Where(c => c.Produtos.Imune).Sum(x => x.Produtos.VlProduto);
            icmsSt.Imune.ListIdNfe = query.Where(c => c.Produtos.Imune).Select(x => x.Nfe.Id).ToList();
            icmsSt.Imune.ListIdProduto = query.Where(c => c.Produtos.Imune).Select(x => x.Produtos.Id).ToList();

            icmsSt.IsencaoReducao.Total = query.Where(c => c.Produtos.IsencaoReducao).Sum(x => x.Produtos.VlProduto);
            icmsSt.IsencaoReducao.ListIdNfe = query.Where(c => c.Produtos.IsencaoReducao).Select(x => x.Nfe.Id).ToList();
            icmsSt.IsencaoReducao.ListIdProduto = query.Where(c => c.Produtos.IsencaoReducao).Select(x => x.Produtos.Id).ToList();

            icmsSt.IsencaoReducaoCestaBasica.Total = query.Where(c => c.Produtos.IsencaoReducaoCestaBasica).Sum(x => x.Produtos.VlProduto);
            icmsSt.IsencaoReducaoCestaBasica.ListIdNfe = query.Where(c => c.Produtos.IsencaoReducaoCestaBasica).Select(x => x.Nfe.Id).ToList();
            icmsSt.IsencaoReducaoCestaBasica.ListIdProduto = query.Where(c => c.Produtos.IsencaoReducaoCestaBasica).Select(x => x.Produtos.Id).ToList();

            icmsSt.LancamentoOficio.Total = query.Where(c => c.Produtos.LancamentoOficio).Sum(x => x.Produtos.VlProduto);
            icmsSt.LancamentoOficio.ListIdNfe = query.Where(c => c.Produtos.LancamentoOficio).Select(x => x.Nfe.Id).ToList();
            icmsSt.LancamentoOficio.ListIdProduto = query.Where(c => c.Produtos.LancamentoOficio).Select(x => x.Produtos.Id).ToList();

            icmsSt.Isento.Total = query.Where(c => c.Produtos.Isento).Sum(x => x.Produtos.VlProduto);
            icmsSt.Isento.ListIdNfe = query.Where(c => c.Produtos.Isento).Select(x => x.Nfe.Id).ToList();
            icmsSt.Isento.ListIdProduto = query.Where(c => c.Produtos.Isento).Select(x => x.Produtos.Id).ToList();

            #endregion
            var pisConfins = new DetalhamentoPisConfins();
            #region detalhamento PisConfins
            pisConfins.ExigSuspensaMono.Total = query.Where(c => c.Produtos.ExigSuspensaMono).Sum(x => x.Produtos.VlProduto);
            pisConfins.ExigSuspensaMono.ListIdNfe = query.Where(c => c.Produtos.ExigSuspensaMono).Select(x => x.Nfe.Id).ToList();
            pisConfins.ExigSuspensaMono.ListIdProduto = query.Where(c => c.Produtos.ExigSuspensaMono).Select(x => x.Produtos.Id).ToList();

            pisConfins.LancamentoOficioMono.Total = query.Where(c => c.Produtos.LancamentoOficioMono).Sum(x => x.Produtos.VlProduto);
            pisConfins.LancamentoOficioMono.ListIdNfe = query.Where(c => c.Produtos.LancamentoOficioMono).Select(x => x.Nfe.Id).ToList();
            pisConfins.LancamentoOficioMono.ListIdProduto = query.Where(c => c.Produtos.LancamentoOficioMono).Select(x => x.Produtos.Id).ToList();

            pisConfins.NcmMono.Total = query.Where(c => c.Produtos.NcmMono).Sum(x => x.Produtos.VlProduto);
            pisConfins.NcmMono.ListIdNfe = query.Where(c => c.Produtos.NcmMono).Select(x => x.Nfe.Id).ToList();
            pisConfins.NcmMono.ListIdProduto = query.Where(c => c.Produtos.NcmMono).Select(x => x.Produtos.Id).ToList();

            pisConfins.SubTributariaMono.Total = query.Where(c => c.Produtos.SubTributariaMono).Sum(x => x.Produtos.VlProduto);
            pisConfins.SubTributariaMono.ListIdNfe = query.Where(c => c.Produtos.SubTributariaMono).Select(x => x.Nfe.Id).ToList();
            pisConfins.SubTributariaMono.ListIdProduto = query.Where(c => c.Produtos.SubTributariaMono).Select(x => x.Produtos.Id).ToList();


            #endregion
            var NfeGeral = new DetalhamentoNfeGeral();
            if (!somenteImposto)
            {
                
                #region detalhamento Nfe Geral
                var canceladas = _dbContextNfCanceladas.GetColection.AsQueryable().Where(c => c.CompanyInformation == companyId && c.ModeloNota.Equals("Saida")).ToList();
                query.Select(c => c.Nfe).ToList().ForEach(c => {
                    if (canceladas.Exists(x => x.RefNfe.Equals(c.CodBarra)))
                    {
                        if (!NfeGeral.Canceladas.ListIdNfe.Exists(Nfe => Nfe == c.Id))
                        {
                            NfeGeral.Canceladas.Total += ValorNota(c);
                            NfeGeral.Canceladas.ListIdNfe.Add(c.Id);
                        }

                        //NfeGeral.Canceladas.ListIdProduto.AddRange(query.Where(p => p.Produtos.NfeId == c.Id).Select(p => p.Produtos.Id).ToList());
                    }

                });
                query.Where(c => c.Nfe.ModeloTipo.Equals("DevolucaoSaida")).ToList().ForEach(x => {
                    if (!NfeGeral.Devolucao.ListIdNfe.Exists(Nfe => Nfe == x.Nfe.Id))
                    {
                        NfeGeral.Devolucao.Total = ValorNota(x.Nfe);
                        NfeGeral.Devolucao.ListIdNfe.Add(x.Nfe.Id);
                    }

                });
                query.Where(c => c.Nfe.TranMercadoria).ToList().ForEach(c => {
                    if (!NfeGeral.TransferenciaMercadoria.ListIdNfe.Exists(Nfe => Nfe == c.Nfe.Id))
                    {
                        NfeGeral.TransferenciaMercadoria.Total = ValorNota(c.Nfe);
                        NfeGeral.TransferenciaMercadoria.ListIdNfe.Add(c.Nfe.Id);
                    }

                });
            }


            #endregion
            var detalhamentoApuracao = new DetalhamentoApuracao
            {

                DetalhamentoIcmsSt = icmsSt,
                DetalhamentoPisConfins = pisConfins,
                DetalhamentoNfeGeral = NfeGeral

            };
            var detalhamentoImposto = new DetalhamentoImposto
            {
                DetalhamentoIcmsSt = icmsSt,
                DetalhamentoPisConfins = pisConfins
            };

            return Tuple.Create(detalhamentoApuracao, detalhamentoImposto);
        }

        public async Task<DetalhamentoImposto> GetDetalhamentoImposto(Guid companyId, DateTime dhEmiss)
        {
            var inicioMes = new DateTime(dhEmiss.Year, dhEmiss.Month, 1);
            var fimMes = inicioMes.AddMonths(1).AddDays(-1);

            var query = from nfe in _dbContext.GetColection.AsQueryable().Where(c => c.CompanyInformation == companyId && c.DhEmi >= inicioMes && c.DhEmi <= fimMes).ToList()
                        join produtos in _dbContextProduto.GetColection.AsQueryable().Where(c => c.Modificado).ToList() on nfe.Id equals produtos.NfeId
                        select new NfeProd { Nfe = nfe, Produtos = produtos };
            var detalhamentoImposto = GerarDetalhamento(query, companyId, true);
            return detalhamentoImposto.Item2;
        }

        private double ValorNota(NFE Nfe)
        {
            return ((Nfe.VlTotalFrete + Nfe.VtTotalNfe) - (Nfe.VlTotalDesc + Nfe.VlOutDes));
        }

        public async Task<AgrupamentoDetalhamentoApuracao> GetAgrupamentoDetalhamentoApuracao(List<Guid> ids, string tipo, string grupo)
        {
            var agrupamentoDetalhamentoApuracao = new AgrupamentoDetalhamentoApuracao();
            var query = from nfe in _dbContext.GetColection.AsQueryable().Where(c => ids.Contains(c.Id.Value)).ToList()
                        join produtos in _dbContextProduto.GetColection.AsQueryable().Where(c => c.Modificado).ToList() on nfe.Id equals produtos.NfeId
                        select new NfeProd { Nfe = nfe, Produtos = produtos };

            if(grupo == "IcmsStPisConfins")
            {
                agrupamentoDetalhamentoApuracao.IcmsStPisConfinsDetalhamentoApuracao = ObterIcmsStPisConfinsDetalhamentoApuracao(tipo, query);
            }
            else
            {
                if(tipo == "Cancelada")
                {
                    agrupamentoDetalhamentoApuracao.NotaFiscalCanceladaDetalhamentoApuracao = ObterNotaFiscalCanceladaDetalhamentoApuracao(ids,query);
                }
                else
                {
                    agrupamentoDetalhamentoApuracao.DevolucaoTransferenciaDetalhamentoApuracao = ObterDevolucaoTransferenciaDetalhamentoApuracao(tipo,query);
                }
            }

            return agrupamentoDetalhamentoApuracao;

        }

        private List<DevolucaoTransferenciaDetalhamentoApuracao> ObterDevolucaoTransferenciaDetalhamentoApuracao(string tipo, IEnumerable<NfeProd> query)
        {
            List<NFE> nfes;
            var devolucaoTransferenciaDetalhamentoApuracoes = new List<DevolucaoTransferenciaDetalhamentoApuracao>();
            if (tipo == "Devolucao")           
                nfes = query.Where(c => c.Nfe.ModeloTipo.Equals("DevolucaoSaida")).Select(c=> c.Nfe).ToList();            
            else          
                nfes = query.Where(c => c.Nfe.TranMercadoria).Select(c => c.Nfe).ToList();

            nfes.ForEach(c => {
                devolucaoTransferenciaDetalhamentoApuracoes.Add(new DevolucaoTransferenciaDetalhamentoApuracao { 
                  Danfe = c.Danfe,
                  DataEmi = c.DhEmi,
                  ModeloTipo = c.ModeloTipo,
                  NfeRef = c.CodBarra,
                  NumeroNfe = c.Nnfe,
                  ValorTotalNfe = c.VtTotalNfe,
                  ValorTotalProd = c.VlTotalPro
                });
            });

            return devolucaoTransferenciaDetalhamentoApuracoes;
        }

        private List<NotaFiscalCanceladaDetalhamentoApuracao> ObterNotaFiscalCanceladaDetalhamentoApuracao(List<Guid> ids, IEnumerable<NfeProd> query)
        {
            var notaFiscalCanceladaDetalhamentoApuracoes = new List<NotaFiscalCanceladaDetalhamentoApuracao>();
           var canceladas =  _dbContextNfCanceladas.GetColection.AsQueryable().Where(c=> ids.Contains(c.Id.Value) && c.ModeloNota.Equals("Saida")).ToList();
            canceladas.ForEach(c =>
            {
                var nfe = query.Where(x => x.Nfe.CodBarra == c.RefNfe).Select(a => a.Nfe).FirstOrDefault();
                notaFiscalCanceladaDetalhamentoApuracoes.Add(new NotaFiscalCanceladaDetalhamentoApuracao
                {
                    Danfe = nfe.Danfe,
                    DataEmi = nfe.DhEmi,
                    DescEventoCanceladas = c.DescEvento,
                    ModeloTipo = c.ModeloTipo,
                    NfeRef = c.RefNfe,
                    NumeroNfe = nfe.Nnfe,
                    ValorTotalNfe = nfe.VtTotalNfe,
                    ValorTotalProd = nfe.VlTotalPro                    
                }); ;
            });
            return notaFiscalCanceladaDetalhamentoApuracoes;
        }

        private List<IcmsStPisConfinsDetalhamentoApuracao> ObterIcmsStPisConfinsDetalhamentoApuracao(string tipo, IEnumerable<NfeProd> query)
        {
            List<Produtos> produtos;
            switch (tipo)
            {
                case "IcmsSt":
                    produtos = query.Where(c => c.Produtos.IcmsSt).Select(c => c.Produtos).ToList();
                    break;
                case "Beneficios":
                    produtos = query.Where(c => c.Produtos.Beneficios).Select(c => c.Produtos).ToList();
                    break;
                case "Isento":
                    produtos = query.Where(c => c.Produtos.Isento).Select(c => c.Produtos).ToList();
                    break;
                case "Imune":
                    produtos = query.Where(c => c.Produtos.Imune).Select(c => c.Produtos).ToList();
                    break;
                case "ExigSuspensa" :
                    produtos = query.Where(c => c.Produtos.ExigSuspensa).Select(c => c.Produtos).ToList();
                    break;
                case "AntEncTributacao":
                    produtos = query.Where(c => c.Produtos.AntEncTributacao).Select(c => c.Produtos).ToList();
                    break;
                case "LancamentoOficio":
                    produtos = query.Where(c => c.Produtos.LancamentoOficio).Select(c => c.Produtos).ToList();
                    break;
                case "IsencaoReducao":
                    produtos = query.Where(c => c.Produtos.IsencaoReducao).Select(c => c.Produtos).ToList();
                    break;
                case "IsencaoReducaoCestaBasica":
                    produtos = query.Where(c => c.Produtos.IsencaoReducaoCestaBasica).Select(c => c.Produtos).ToList();
                    break;
                case "NcmMono":
                    produtos = query.Where(c => c.Produtos.NcmMono).Select(c => c.Produtos).ToList();
                    break;
                case "ExigSuspensaMono":
                    produtos = query.Where(c => c.Produtos.ExigSuspensaMono).Select(c => c.Produtos).ToList();
                    break;
                case "LancamentoOficioMono":
                    produtos = query.Where(c => c.Produtos.LancamentoOficioMono).Select(c => c.Produtos).ToList();
                    break;
                case "SubTributariaMono":
                    produtos = query.Where(c => c.Produtos.SubTributariaMono).Select(c => c.Produtos).ToList();
                    break;
                default:
                    throw new Exception("Tipo do produto não encontrado.");                   

            }
            var IcmsStPisConfinsDetalhamentoApuracoes = new List<IcmsStPisConfinsDetalhamentoApuracao>();
            produtos.ForEach(c => {
                var nfe = query.Where(a => a.Nfe.Id == c.NfeId).Select(x => x.Nfe).FirstOrDefault();
                IcmsStPisConfinsDetalhamentoApuracoes.Add( new IcmsStPisConfinsDetalhamentoApuracao { 
                     CFOP = c.Cfop,
                     CodigoProduto = c.CodProduto,
                     Lei = c.Lei,
                     NCM = c.NcmProd,
                     NumeroNfe = nfe.Nnfe,
                     ValorNfe = nfe.VtTotalNfe,
                     ValorProd = c.VlProduto,
                     Danfe = nfe.Danfe,
                     NomeProduto = c.DescProduto
                });
            });
            return IcmsStPisConfinsDetalhamentoApuracoes;
        }
    }
}
