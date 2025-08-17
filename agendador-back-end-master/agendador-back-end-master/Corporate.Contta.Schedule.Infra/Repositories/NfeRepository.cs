using AutoMapper;
using Corporate.Contta.Schedule.Api.Extension;
using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities;
using Corporate.Contta.Schedule.Domain.Entities.BlocoE;
using Corporate.Contta.Schedule.Domain.Entities.DashboardAgg.Apuracoes;
using Corporate.Contta.Schedule.Domain.Entities.FullNfeAgg;
using Corporate.Contta.Schedule.Domain.Entities.Imporsto;
using Corporate.Contta.Schedule.Domain.Entities.ImpostoAgg;
using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using Corporate.Contta.Schedule.Domain.Entities.NfeModAgg;
using Corporate.Contta.Schedule.Domain.Entities.NfeTAgg;
using Corporate.Contta.Schedule.Domain.Entities.Product;
using Corporate.Contta.Schedule.Domain.Entities.RegistroBlEAgg;
using Corporate.Contta.Schedule.Domain.Entities.ServicoEntityAgg;
using Corporate.Contta.Schedule.Domain.Entities.TbServico;
using Corporate.Contta.Schedule.Infra.Models.Adapter;
using Corporate.Contta.Schedule.Infra.Models.ModeloXml.NotaFiscalEletronicaMod55;
using Corporate.Contta.Schedule.Infra.Repositories.Base;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Infra.Repositories
{
    public class NfeRepository : BaseRepository<NFE>, INfeRepository
    {
        private static MongoDBContext<CompanyInformation> _dbContextComapany = new MongoDBContext<CompanyInformation>();
        private static MongoDBContext<TbAjuste> _dbContextAjusteDb = new MongoDBContext<TbAjuste>();
        private static MongoDBContext<NFE> _dbContext = new MongoDBContext<NFE>();
        private static MongoDBContext<EmpresaDest> _dbContextEmpresaDest = new MongoDBContext<EmpresaDest>();
        private static MongoDBContext<TbCodServico> _dbContextCodServico = new MongoDBContext<TbCodServico>();
        private static MongoDBContext<ServicoEntity> _dbContextServico = new MongoDBContext<ServicoEntity>();
        private static MongoDBContext<Produtos> _dbContextProduto = new MongoDBContext<Produtos>();
        private static MongoDBContext<NfVendaManual> _dbContextNfVendaManual = new MongoDBContext<NfVendaManual>();
        private static MongoDBContext<NfServicoManual> _dbContextNfServicoManual = new MongoDBContext<NfServicoManual>();
        private static MongoDBContext<NfeCanceldas> _dbContextNfCanceladas = new MongoDBContext<NfeCanceldas>();
        private static MongoDBContext<ProdutosFornecedor> _dbContextProdutosFornecedores = new MongoDBContext<ProdutosFornecedor>();
        private static MongoDBContext<AjusteNfe> _dbContextAjuste = new MongoDBContext<AjusteNfe>();
        private readonly ICompanyRepository _companyRepository;
        private readonly IEmpresaEmitRepository _empresaEmitRepository;
        private readonly IEmpresaDestRepository __empresaDestRepository;
        private EmpresaDestRepository _empresaDestRepository;
        private ServicoEntityRepository _servicoEntityRepository;
        private ProductRepository _productRepository;
        private ImpostosRepository _impostoRepository;

        private readonly IImpostosRepository _impostos;

        private IMapper _mapper;

        GetCfop getCfop = new GetCfop();

        public NfeRepository(IMapper mapper, IEmpresaDestRepository empresaDestRepository, ICompanyRepository companyRepository, IImpostosRepository impostosRepository, IEmpresaEmitRepository empresaEmitRepository) : base(_dbContext)
        {
            _companyRepository = companyRepository;
            _empresaEmitRepository = empresaEmitRepository;
            _impostos = impostosRepository;
            __empresaDestRepository = empresaDestRepository;
            _mapper = mapper;
        }

        public Task<bool> Delete(NFE product)
        {
            throw new NotImplementedException();
        }

        public Task<List<NFE>> GetAllDisabled()
        {
            throw new NotImplementedException();
        }

        public async Task<NFE> GetById(Guid id)
        {
            var result = await _dbContext.GetColection.FindAsync(c => c.Id.Equals(id)).ConfigureAwait(false);
            return await result.FirstOrDefaultAsync();
        }

        public Task<NFE> GetProductInformationBy(string barCode)
        {
            throw new NotImplementedException();
        }

        public Task<List<NFE>> GetProductInformationByMasterId(Guid masterId)
        {
            throw new NotImplementedException();
        }

        public async Task<NFE> Insert(NFE nfe)
        {
            if (nfe != null)
                nfe.Id = Guid.NewGuid();
            _dbContext.GetColection.InsertOne(nfe);

            return nfe;
        }

        public async Task<bool> Update(List<Produtos> listProduct)
        {
            foreach (var item in listProduct)
            {
                var produtoDto = _productRepository.GetById(item.Id).Result;

                if (item.NfeId == produtoDto.NfeId)
                {
                    try
                    {
                        item.Auditado = true;
                        var update = Builders<Produtos>.Update.Set(c => c.CodProduto, item.CodProduto)
                                                    .Set(c => c.DescProduto, item.DescProduto)
                                                    .Set(c => c.NcmProd, item.NcmProd)
                                                    .Set(c => c.UnidMedida, item.UnidMedida)
                                                    .Set(c => c.Quantidade, item.Quantidade)
                                                    .Set(c => c.VlUnitario, item.VlUnitario)
                                                    .Set(c => c.UniMedTributado, item.UniMedTributado)
                                                    .Set(c => c.QtdTributaria, item.QtdTributaria)
                                                    .Set(c => c.VlUnitTributado, item.VlUnitTributado)
                                                    .Set(c => c.VlProduto, item.VlProduto)
                                                    .Set(c => c.VlTlFrete, item.VlTlFrete)
                                                    .Set(c => c.VlTlSeguro, item.VlTlSeguro)
                                                    .Set(c => c.VlTlDesconto, item.VlTlDesconto)
                                                    .Set(c => c.OutrasDespesas, item.OutrasDespesas)
                                                    .Set(c => c.Cfop, item.Cfop)
                                                    .Set(c => c.Ean, item.Ean)
                                                    .Set(c => c.PedCompra, item.PedCompra)
                                                    .Set(c => c.NItemPedido, item.NItemPedido)
                                                    .Set(c => c.Origem, item.Origem)
                                                    .Set(c => c.Tributos, item.Tributos)
                                                    .Set(c => c.VlAproxTributos, item.VlAproxTributos)
                                                    .Set(c => c.EmpresaEmitId, item.EmpresaEmitId)
                                                    .Set(c => c.Csons, item.Csons)
                                                    .Set(c => c.NcmMono, item.NcmMono)
                                                    .Set(c => c.Beneficios, item.Beneficios)
                                                    .Set(c => c.IcmsSt, item.IcmsSt)
                                                    .Set(c => c.Modificado, item.Modificado)
                                                    .Set(c => c.NfeId, item.NfeId);

                        var updateResult = await _dbContextProduto.GetColection.UpdateOneAsync(c => c.Id == item.Id, update);
                        if (updateResult.ModifiedCount > 0)
                        {
                            //Criar errro
                        }

                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                }


            }

            return true;
        }

        public async Task<List<NFE>> GetAll(Guid companyId, int operation)
        {
            var result = await _dbContext.GetColection.FindAsync(c => c.CompanyInformation == companyId).ConfigureAwait(false);

            var listaNfe = result.ToList();

            var listaCfopSaida = getCfop.GetListaCfopVenda();
            var listaCfopEntrada = getCfop.GetListaCfopEntradas();
            var listaCfopDevolucao = getCfop.GetListaCfopDevolucaoVendas();
            var listaCfopCanceladas = new List<double>();

            return listaNfe.ToList();

        }

        public async Task<List<FullNFE>> GetAllFull(Guid companyId, string operation, DateTime data)
        {
            var inicioMes = new DateTime(data.Year, data.Month, 1);
            var fimMes = inicioMes.AddMonths(1).AddDays(-1);
            var tipoOperacao = false;

            var result = await _dbContext.GetColection
                                .FindAsync(c => c.CompanyInformation == companyId && c.ModeloTipo == operation && c.FinNFe == 1 &&
                                (c.DhEmi.HasValue && c.DhEmi.Value >= inicioMes && c.DhEmi.Value <= fimMes))
                                .ConfigureAwait(false);

            if (operation != "Venda")
            {
                tipoOperacao = true;
            }

            return await ObtenhaListaFullNFE(result.ToList(), tipoOperacao);
        }

        #region Initial tela tripa
        public async Task<RetornoNfeT> GetAllNfeT(Guid companyId, string operation, DateTime data, int pagina, int qtdPorPagina, List<Guid> listNfe, bool apuracao)
        {
            var inicioMes = new DateTime(data.Year, data.Month, 1);
            var fimMes = inicioMes.AddMonths(1).AddDays(-1);
            var listNfeCodBarra = new List<NFE>();
            var productFornect = false;

            if (operation != "Venda")
            {
                productFornect = true;
            }

            if (apuracao)
            {
                foreach (var item in listNfe)
                {
                    listNfeCodBarra.Add(_dbContext.GetColection.Find(c => c.Id == item).FirstOrDefault());
                }

                var retornoNfeT = new RetornoNfeT();
                retornoNfeT.TotalDePaginas = (int)Math.Ceiling((double)retornoNfeT.TotalDeItens / qtdPorPagina);
                retornoNfeT.PaginaAtual = pagina;
                retornoNfeT.Itens = await ObtenhaListaNfeT(listNfeCodBarra, false);

                return retornoNfeT;
            }
            else
            {
                var query = _dbContext.GetColection.Find(c => c.CompanyInformation == companyId && c.ModeloTipo == operation && (c.DhEmi.HasValue && c.DhEmi.Value >= inicioMes && c.DhEmi.Value <= fimMes && c.Modelo == "55")).SortByDescending(x => x.Nnfe);

                var totalTask = query.CountDocumentsAsync();
                var itemsTask = query.Skip((pagina - 1) * qtdPorPagina).Limit(qtdPorPagina).ToListAsync();
                await Task.WhenAll(totalTask, itemsTask);

                var retornoNfeT = new RetornoNfeT();
                retornoNfeT.TotalDeItens = totalTask.Result;
                retornoNfeT.TotalDePaginas = (int)Math.Ceiling((double)retornoNfeT.TotalDeItens / qtdPorPagina);
                retornoNfeT.PaginaAtual = pagina;
                retornoNfeT.Itens = await ObtenhaListaNfeT(itemsTask.Result, productFornect);

                return retornoNfeT;
            }

        }

        public async Task<List<RetornoNfeMod57>> GetAllNfeMod57(Guid companyId, string operation, DateTime data)
        {
            var operacaoTransporte = "";
            var retornoNfeMod57 = new List<RetornoNfeMod57>();
            var inicioMes = new DateTime(data.Year, data.Month, 1);
            var fimMes = inicioMes.AddMonths(1).AddDays(-1);

            var query = _dbContext.GetColection.Find(c => c.CompanyInformation == companyId && c.DhEmi.Value >= inicioMes && c.DhEmi.Value <= fimMes && c.Modelo == "57").ToList();

            var empresaEmit = _dbContextComapany.GetColection.Find(c => c.Id.Equals(companyId)).FirstOrDefault();

            foreach (var item in query)
            {
                if (item.UFIni == empresaEmit.Address.State && item.CMunFim == empresaEmit.Address.CityIbge)
                {
                    operacaoTransporte = "Transporte Intramunicipal";
                }
                if (item.UFIni == empresaEmit.Address.State && item.CMunFim != empresaEmit.Address.CityIbge)
                {
                    operacaoTransporte = "Transporte Intermunicipal";
                }
                if (item.UFIni != empresaEmit.Address.State && item.CMunFim != empresaEmit.Address.CityIbge)
                {
                    operacaoTransporte = "Transporte Interestadual";
                }

                if (item.ModeloTipo == "Saida")
                {
                    retornoNfeMod57.Add(new RetornoNfeMod57
                    {
                        Id = item.Id,
                        Cnpj = empresaEmit.Cnpj,
                        Municipio = empresaEmit.Address.City,
                        NumeroNotaFiscal = item.Nnfe.ToString(),
                        TipoCte = item.TipNfe.ToString(),
                        Uf = item.UFEnv,
                        ValorDesconto = item.VlTotalDesc,
                        ValorIcms = item.VtIcms,
                        ValorTotalServico = item.VtTotalNfe,
                        ValorTotal = item.VtTotalNfe,
                        TipoFrete = operacaoTransporte

                    });
                }
                else
                {
                    retornoNfeMod57.Add(new RetornoNfeMod57
                    {
                        Cnpj = empresaEmit.Cnpj,
                        Municipio = empresaEmit.Address.City,
                        NumeroNotaFiscal = item.Nnfe.ToString(),
                        TipoCte = item.TipNfe.ToString(),
                        Uf = item.UFEnv,
                        ValorDesconto = item.VlTotalDesc,
                        ValorIcms = item.VtIcms,
                        ValorTotalServico = item.VtTotalNfe,
                        ValorTotal = item.VtTotalNfe,
                        TipoFrete = operacaoTransporte

                    });

                }
            }

            return retornoNfeMod57;
        }

        public async Task<List<RetornoNfeServico>> GetAllNfeServico(Guid companyId, string operation, DateTime data, int pagina, int qtdPorPagina)
        {
            var listNfeServico = new List<RetornoNfeServico>();
            var inicioMes = new DateTime(data.Year, data.Month, 1);
            var fimMes = inicioMes.AddMonths(1).AddDays(-1);
            var query = _dbContextServico.GetColection.Find(c => c.CompanyInformation == companyId && c.ModeloNota == operation && c.DataEmissao >= inicioMes && c.DataEmissao <= fimMes).ToList();


            foreach (var item in query)
            {
                var empresaDest = __empresaDestRepository.GetById(item.EmpresaDesId).Result;

                listNfeServico.Add(new RetornoNfeServico()
                {
                    Id = item.Id,
                    CnpjCpf = empresaDest.Cnpj,
                    Aliquota = item.Aliquota,
                    Cofins = item.ValorCofins,
                    ValorCsll = item.ValorCsll,
                    ValorIr = item.ValorIr,
                    ValorCofins = item.ValorCofins,
                    Csll = item.ValorCsll,
                    Desconto = item.DescontoIncondicionado,
                    Inss = item.ValorInss,
                    Ir = item.ValorIr,
                    ValorDeducao = item.ValorDeducoes,
                    RazaoSocial = empresaDest.RazaoSocial,
                    ValorIss = item.ValorIss,
                    ValorPis = item.ValorPis,
                    TotalNFe = item.ValorServicos


                });
            }

            return listNfeServico;
        }

        public async Task<RegistroBlE> GetRegistrosBlE(Guid companyId, DateTime data)
        {
            var inicioMes = new DateTime(data.Year, data.Month, 1);
            var fimMes = inicioMes.AddMonths(1).AddDays(-1);

            var result = await _dbContext.GetColection
                                .FindAsync(c => c.CompanyInformation == companyId && c.ModeloTipo == "Venda" &&
                                (c.DhEmi.HasValue && c.DhEmi.Value >= inicioMes && c.DhEmi.Value <= fimMes));

            return ObtenhaRegistroBlE(result.ToList(), inicioMes, fimMes);
        }

        #endregion

        public async Task<TotalNfe> GetBaseCalculo(Guid companyId, DateTime dateClose)
        {
            var result = await _dbContext.GetColection.FindAsync(c => c.CompanyInformation == companyId  /* && c.DhEmi.Value.Date.Month == dateClose.Month*/).ConfigureAwait(false);

            var totalBaseCalculo = result.ToList();

            // returne de todos os dados
            var totalbase = totalBaseCalculo.Sum(c => c.BaseCAlIcms);
            var totalNfe = totalBaseCalculo.Sum(c => c.VtTotalNfe);
            var tributos = totalBaseCalculo.Sum(c => c.VlAproxTributos);

            var TotalNfe = new TotalNfe
            {
                BaseCAlIcms = totalbase,
                VtTotalNfe = totalNfe,
                VlAproxTributos = tributos
            };

            return TotalNfe;
        }

        public async Task<bool> DesativarNota(Guid id)
        {
            var update = Builders<NFE>.Update.Set(c => c.Ativo, false);
            var updateResult = await _dbContext.GetColection.UpdateOneAsync(c => c.Id == id, update);
            return updateResult.ModifiedCount > 0;
        }

        #region Criação informação tela tripa

        public async Task<List<NfeT>> ObtenhaListaNfeT(List<NFE> listaNfe, bool productFornect)
        {
            var listaNfeT = new List<NfeT>();

            foreach (var nfe in listaNfe)
            {
                var nfeT = await ObtenhaNfeT(nfe, productFornect);
                listaNfeT.Add(nfeT);
            }

            return listaNfeT;
        }


        private async Task<NfeT> ObtenhaNfeT(NFE nfe, bool productFornc)
        {
            var nfeT = new NfeT();

            nfeT.Id = nfe.Id;
            nfeT.Cfop = 0;
            nfeT.Issuer = nfe.EmpresaEmetId;
            nfeT.DocumentSituationCode = nfe.FinNFe;
            nfeT.Series = nfe.Serie;
            nfeT.DocumentNumber = nfe.Nnfe;
            nfeT.NfeKey = nfe.CodBarra;
            nfeT.EmissionDate = nfe.DhEmi;
            nfeT.InOrOutDate = nfe.DhSaida;
            nfeT.TotalDocumentValue = nfe.VtTotalNfe;
            nfeT.PaymentType = nfe.FormPag;
            nfeT.DiscountValue = nfe.VlTotalDesc;
            nfeT.InsuranceValue = nfe.VlTotalSeguro;
            nfeT.OtherExpendituresValue = 0;
            nfeT.IcmsCalculationBasis = nfe.BaseCAlIcms;
            nfeT.Cnpj = nfe.CNPJEmitente;
            nfeT.IdNfe = nfe.Id;
            nfeT.UrlDanfe = nfe.Danfe;
            nfeT.Carta = nfe.Carta;
            nfeT.DescricaoCarta = nfe.DescricaoCarta;


            var impostos = await ImpostosRepository.GetByCompanyAndNfe(nfe.CompanyInformation.Value, nfe.Id.Value);

            if (impostos != null)
            {
                nfeT.IcmsValue = impostos.VlIcms;
                nfeT.IcmsStCalculationBasis = impostos.VlBcIcmsSt;
                nfeT.RetainedIcms = impostos.VlIcms;
                nfeT.IpiValue = impostos.Ipi;
                nfeT.CofinsValue = impostos.VCOFINS;
                nfeT.RetainedPis = impostos.VPIS;
                nfeT.RetainedCofins = impostos.CstCofins;
            }

            nfeT.Items = await ObtenhaListaDeItens(nfe, productFornc);
            nfeT.Analytical = ObtenhaAnalytical(nfe, impostos);

            return nfeT;
        }

        private async Task<List<Items>> ObtenhaListaDeItens(NFE nfe, bool productFornec)
        {
            try
            {
                var listItens = new List<Items>();

                if (productFornec)
                {
                    var listaProdutosFornec = await ProductRepository.GetAllByNfeFornecedor(nfe.Id.Value);

                    foreach (var produto in listaProdutosFornec)
                    {
                        var item = await ObtenhaItemsFornec(produto, nfe);
                        listItens.Add(item);
                    }
                    return listItens;
                }
                else
                {

                    var listaProdutos = await ProductRepository.GetAllByNfe(nfe.Id.Value);


                    foreach (var produto in listaProdutos)
                    {
                        var item = await ObtenhaItems(produto, nfe);
                        listItens.Add(item);
                    }

                    return listItens;

                }
            }
            catch (Exception ex)
            {

                throw;
            }


        }

        private async Task<Items> ObtenhaItemsFornec(ProdutosFornecedor produto, NFE nfe)
        {
            var item = new Items();
            var impostos = await ImpostosRepository.GetByProductAndNfe(produto.Id.Value, nfe.Id.Value);

            var number = new string(nfe.NatOperacao.Where(char.IsDigit).ToArray());

            item.Id = produto.Id;
            item.ItemCode = produto.CodProduto;
            item.AdditionalDescription = produto.DescProduto;
            item.ItemQuantity = produto.Quantidade;
            item.MeasureCode = produto.UnidMedida;
            item.TotalItemValue = produto.VlProduto;
            item.DiscountValue = produto.VlTlDesconto;
            item.Cfop = produto.Cfop;
            item.OperationNatureCode = nfe.NatOperacao;
            item.IcmsCalculationBasis = nfe.BaseCAlIcms;

            if (impostos != null)
            {
                item.CstIcms = impostos.Cest;
                item.IcmsAliquot = impostos.AliqIcms;
                item.IcmsValue = impostos.VlIcms;
                item.IcmsStCalculationBasis = impostos.VlBcIcmsSt;
                item.IcmsStAliquot = impostos.AliqInter;
                item.IcmsStValue = impostos.VlIcmsSt;
                item.CstIpi = impostos.CstPis;
                item.IpiAliquot = impostos.AliquotaIpi;
                item.IpiValue = impostos.Ipi;
                item.CstPis = impostos.CstPis;
                item.CofinsValue = impostos.VCOFINS;
            }

            return item;
        }


        private async Task<Items> ObtenhaItems(Produtos produto, NFE nfe)
        {
            var item = new Items();
            var impostos = await ImpostosRepository.GetByProductAndNfe(produto.Id.Value, nfe.Id.Value);

            var naturezaOpercacao = "";

            if (nfe.NatOperacao != null && nfe.NatOperacao != "")
            {
                naturezaOpercacao = Regex.Replace(nfe.NatOperacao, @"[\d-]", string.Empty).Replace(".", "");
            }

            item.Id = produto.Id;
            item.ItemCode = produto.CodProduto;
            item.AdditionalDescription = produto.DescProduto;
            item.ItemQuantity = produto.Quantidade;
            item.MeasureCode = produto.UnidMedida;
            item.TotalItemValue = produto.VlProduto;
            item.DiscountValue = produto.VlTlDesconto;
            item.Cfop = produto.Cfop;
            item.OperationNatureCode = naturezaOpercacao;
            item.IcmsCalculationBasis = nfe.BaseCAlIcms;
            item.ProdutoBaySimples = produto.ProdutoBaySimples;

            if (impostos != null)
            {
                item.CstIcms = impostos.Cest;
                item.IcmsAliquot = impostos.AliqIcms;
                item.IcmsValue = impostos.VlIcms;
                item.IcmsStCalculationBasis = impostos.VlBcIcmsSt;
                item.IcmsStAliquot = impostos.AliqInter;
                item.IcmsStValue = impostos.VlIcmsSt;
                item.CstIpi = impostos.CstPis;
                item.IpiAliquot = impostos.AliquotaIpi;
                item.IpiValue = impostos.Ipi;
                item.CstPis = impostos.CstPis;
                item.CofinsValue = impostos.VCOFINS;
            }

            return item;
        }

        private Analytical ObtenhaAnalytical(NFE nfe, Impostos impostos)
        {
            var analytical = new Analytical();

            analytical.Cfop = 0;
            analytical.OperationValue = nfe.DesOperacao;
            analytical.IcmsCalculationBasis = nfe.BaseCAlIcms;
            analytical.IcmsValue = nfe.VtIcms;

            if (impostos != null)
            {
                analytical.CstIcms = impostos.Cest;
                analytical.IcmsAliquot = impostos.AliqIcms;
                analytical.IcmsStCalculationBasis = impostos.VlBcIcmsSt;
                analytical.IcmsStValue = impostos.VlIcmsSt;
                analytical.IpiValue = impostos.Ipi;
            }

            return analytical;
        }

        #endregion

        #region FULLNFE

        public async Task<List<FullNFE>> ObtenhaListaFullNFE(List<NFE> listaNfe, bool productFornecedor)
        {
            var listaFullNfe = new List<FullNFE>();

            foreach (var nfe in listaNfe)
            {
                var fullNfe = await ObtenhaFullNfe(nfe);
                listaFullNfe.Add(fullNfe);
            }

            return listaFullNfe;
        }

        private async Task<FullNFE> ObtenhaFullNfe(NFE nfe)
        {
            var fullNfe = new FullNFE();

            fullNfe.Id = nfe.Id;
            fullNfe.Taker = await ObtenhaTaker(nfe);
            fullNfe.Activity = ObtenhaActivity(nfe);
            fullNfe.TaxCalculation = ObtenhaTaxCalculation(nfe);
            fullNfe.Sale = await ObtenhaSale(nfe);

            var servicoEntity = await ServicoEntityRepository.GetById(new Guid());

            if (servicoEntity != null)
            {
                fullNfe.FederalRetentions = ObtenhaFederalRetentions(servicoEntity);
                fullNfe.Demonstrative = ObtenhaDemonstrative(servicoEntity);
            }

            return fullNfe;
        }

        private async Task<Taker> ObtenhaTaker(NFE nfe)
        {
            var taker = new Taker();
            var company = await _companyRepository.GetById(nfe.CompanyInformation.Value);

            if (company != null)
            {
                taker.Id = company.Id;
                taker.Cnpj_Cpf = company.Cnpj;
                taker.Name = company.Name;
                taker.CitySubscription = company.MunicipalRegistration;

                if (company.Address != null)
                {
                    var adress = company.Address;
                    taker.ZipCode = adress.Zip;
                    taker.Address = adress.Street;
                    taker.Neighborhood = adress.Neighborhood;
                    taker.City = adress.City;
                    taker.State = adress.State;
                }
            }

            return taker;
        }

        private Activity ObtenhaActivity(NFE nfe)
        {
            var activity = new Activity();
            activity.Code = nfe.CodBarra;
            activity.Description = nfe.DesOperacao;

            return activity;
        }

        private FederalRetentions ObtenhaFederalRetentions(ServicoEntity servicoEntity)
        {
            var federalRetentions = new FederalRetentions();
            federalRetentions.Pis = servicoEntity.ValorPis;
            federalRetentions.Confins = servicoEntity.ValorCofins;
            federalRetentions.Inss = servicoEntity.ValorInss;
            federalRetentions.Ir = servicoEntity.ValorIr;
            federalRetentions.Csll = servicoEntity.ValorCsll;

            return federalRetentions;
        }

        private Demonstrative ObtenhaDemonstrative(ServicoEntity servicoEntity)
        {
            var demonstrative = new Demonstrative();
            demonstrative.ProvidedServiceCity = servicoEntity.CodigoMunicipio;
            demonstrative.TaxedServiceCity = servicoEntity.CodigoTributacaoMunicipio;
            demonstrative.UnconditionalDiscount = servicoEntity.DescontoIncondicionado;
            demonstrative.ServicesValue = servicoEntity.ValorServicos;
            demonstrative.UnconditionalDiscount = servicoEntity.DescontoIncondicionado;
            demonstrative.RetainedIssqn = 0;
            demonstrative.LiquidValue = 0;
            demonstrative.FederalRetentions = 0;

            return demonstrative;
        }

        private TaxCalculation ObtenhaTaxCalculation(NFE nfe)
        {
            var taxCalculation = new TaxCalculation();
            taxCalculation.Uf = "";
            taxCalculation.City = "";
            taxCalculation.UnconditionalDiscount = "";
            taxCalculation.InvoiceValue = "";
            taxCalculation.Deductions = "";
            taxCalculation.CalculationBasis = "";
            taxCalculation.Aliquot = "";
            taxCalculation.TaxValues = "";

            return taxCalculation;
        }

        private async Task<Sale> ObtenhaSale(NFE nfe)
        {
            var sale = new Sale();
            sale.Taxes = ObtenhaTaxes(nfe);
            sale.Products = await ObtenhaListProducts(nfe);
            sale.Receiver = await ObtenhaReceiver(nfe);

            return sale;
        }

        private Taxes ObtenhaTaxes(NFE nfe)
        {
            var taxes = new Taxes();
            taxes.BcIcms = nfe.BaseCAlIcms;
            taxes.BcIcmsSt = nfe.BaseCalIcmsSt;
            taxes.Discount = nfe.VlTotalPro;
            taxes.IcmsStValue = nfe.VtIcmsSt;
            taxes.IcmsValue = nfe.VtIcms;
            taxes.IpiValue = nfe.VlIpi;
            taxes.Other = nfe.VlOutDes;
            taxes.TotalInvoiceValue = nfe.VtTotalNfe;
            taxes.TotalProductsValue = nfe.VlTotalPro;

            return taxes;
        }

        private async Task<Receiver> ObtenhaReceiver(NFE nfe)
        {
            var receiver = new Receiver();
            var empresa = await EmpresaDestRepository.GetById(nfe.EmpresaDesId);

            if (empresa != null)
            {
                receiver.Id = empresa.Id;
                receiver.Address = empresa.Logradouro;
                receiver.Cep = empresa.Cep;
                receiver.City = empresa.Cidade;
                receiver.CnpjCpf = !string.IsNullOrEmpty(empresa.Cnpj) ? empresa.Cnpj : empresa.CPF;
                receiver.Name = empresa.RazaoSocial;
                receiver.Neighborhood = empresa.Bairro;
                //receiver.PhoneFax = empresa. ////TODO: não tem telefone
                receiver.State = empresa.IncrEstadual;
                receiver.StateSubscription = empresa.InscEstSubTribu;
            }

            receiver.EmissionDate = nfe.DhEmi;

            if (nfe.DhSaida.HasValue)
            {
                receiver.InOutDate = nfe.DhSaida.Value.ToShortDateString();
            }

            return receiver;
        }

        private async Task<IList<Products>> ObtenhaListProducts(NFE nfe)
        {
            var listProducts = new List<Products>();


            var listaProdutos = await ProductRepository.GetAllByNfe(nfe.Id.Value);

            foreach (var produto in listaProdutos)
            {
                var products = await ObtenhaProducts(produto, nfe.Id.Value);
                listProducts.Add(products);
            }

            return listProducts;

        }

        private async Task<Products> ObtenhaProducts(Produtos produto, Guid idNfe)
        {
            var products = new Products();
            var impostos = await ImpostosRepository.GetByProductAndNfe(produto.Id.Value, idNfe);

            if (impostos != null)
            {
                products.BcIcms = impostos.BCIcms;
                products.Cst = impostos.CST;
                products.IcmsAliquot = impostos?.AliqIcms.ToString();
                products.IcmsValue = impostos.Icms;
                products.IpiAliquot = impostos?.AliquotaIpi.ToString();
                products.IpiValue = impostos.Ipi;
            }

            products.Id = produto.Id;
            products.Description = produto.DescProduto;
            products.Cfop = produto?.Cfop.ToString();
            products.Code = produto.CodProduto;
            products.NcmSh = produto.NcmProd;
            products.Quantity = produto.Quantidade;
            products.Unit = produto.UnidMedida;
            products.UnitValue = produto.VlUnitario;

            return products;
        }

        private async Task<Products> ObtenhaProductsFornecedor(ProdutosFornecedor produto, Guid idNfe)
        {
            var products = new Products();
            var impostos = await ImpostosRepository.GetByProductAndNfe(produto.Id.Value, idNfe);

            if (impostos != null)
            {
                products.BcIcms = impostos.BCIcms;
                products.Cst = impostos.CST;
                products.IcmsAliquot = impostos?.AliqIcms.ToString();
                products.IcmsValue = impostos.Icms;
                products.IpiAliquot = impostos?.AliquotaIpi.ToString();
                products.IpiValue = impostos.Ipi;
            }

            products.Id = produto.Id;
            products.Description = produto.DescProduto;
            products.Cfop = produto?.Cfop.ToString();
            products.Code = produto.CodProduto;
            products.NcmSh = produto.NcmProd;
            products.Quantity = produto.Quantidade;
            products.Unit = produto.UnidMedida;
            products.UnitValue = produto.VlUnitario;

            return products;
        }

        public async Task Insert(NfVendaManual nfVenda)
        {
            nfVenda.Id = Guid.NewGuid();
            await _dbContextNfVendaManual.GetColection.InsertOneAsync(nfVenda).ConfigureAwait(false);
            await InsertNfe(nfVenda);
        }

        public async Task InsertNfe(NfVendaManual nfVendaManual)
        {

            Random randNum = new Random();
            var nfeNumber = randNum.Next(5, 500000);

            IntegrationTbSimplesNfManual integrationNfManual = new IntegrationTbSimplesNfManual();
            var nfe = new NFE();
            var produtos = new Produtos();
            var listProdutos = new List<Produtos>();
            var simplesProd = false;

            try
            {
                var company = _dbContextComapany.GetColection.Find(c => c.Id == nfVendaManual.CompanyInformation).FirstOrDefault();
                var empresaDest = _dbContextEmpresaDest.GetColection.Find(c => c.Cnpj == nfVendaManual.Receiver.CnpjCpf).FirstOrDefault();

                foreach (var item in nfVendaManual.Products)
                {
                    var produtoExiste = _dbContextProduto.GetColection.Find(c => c.CodProduto.Equals(item.Code)).FirstOrDefault();
                    if (produtoExiste == null)
                        simplesProd = true;
                    produtos.Cfop = Convert.ToDouble(item.Cfop);
                    produtos.CodProduto = item.Code;
                    produtos.Csons = Convert.ToInt32(item.Cst);
                    produtos.DescProduto = item.Description;
                    produtos.NcmProd = item.NcmSh;
                    produtos.Quantidade = item.Quantity;
                    produtos.UnidMedida = item.Unit;
                    produtos.VlUnitario = item.UnitValue;
                    produtos.NfeId = nfVendaManual.Id.Value;
                    produtos.CompanyInformation = nfVendaManual.CompanyInformation;
                    produtos.DhEmt = DateTime.Now;
                    produtos.ProdutoBaySimples = simplesProd;
                    listProdutos.Add(produtos);
                }

                nfe.Id = nfVendaManual.Id;
                nfe.Modelo = "55";
                nfe.Serie = 1;
                nfe.Nnfe = nfeNumber;
                nfe.CompanyInformation = company.Id;
                nfe.BaseCAlIcms = nfVendaManual.Taxes.BcIcms;
                nfe.BaseCalIcmsSt = nfVendaManual.Taxes.BcIcmsSt;
                nfe.VlTotalDesc = nfVendaManual.Taxes.Discount;
                nfe.VtIcms = nfVendaManual.Taxes.IcmsValue;
                nfe.VtIcmsSt = nfVendaManual.Taxes.IcmsStValue;
                nfe.VlIpi = nfVendaManual.Taxes.IpiValue;
                nfe.VlOutDes = nfVendaManual.Taxes.Other;
                nfe.VtTotalNfe = nfVendaManual.Taxes.TotalInvoiceValue;
                nfe.VlTotalPro = nfVendaManual.Taxes.TotalProductsValue;
                nfe.DhEmi = nfVendaManual.Receiver.EmissionDate;
                nfe.ModeloTipo = "Venda";
                nfe.UFEnv = nfVendaManual.Receiver.State;
                nfe.Ativo = true;
                nfe.Status = "NfeManual";
                nfe.CNPJEmitente = company.Cnpj;
                nfe.CodBarra = Guid.NewGuid().ToString();
                nfe.Integrada = false;
                nfe.EmpresaDesId = empresaDest.Id;

                await _dbContext.GetColection.InsertOneAsync(nfe).ConfigureAwait(false);

                foreach (var item in listProdutos)
                {
                    item.VlProduto = (item.Quantidade * item.VlUnitario) - item.VlTlDesconto - item.OutrasDespesas;
                    item.NfeId = nfe.Id;
                    item.Id = Guid.NewGuid();
                    await _dbContextProduto.GetColection.InsertOneAsync(item).ConfigureAwait(false);
                }

                await integrationNfManual.CreateTbNfeSaidaManual(nfe, listProdutos);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task InsertNfeServico(NfServicoManual nfServico, bool transportadora)
        {

            try
            {
                nfServico.Id = Guid.NewGuid();
                IntegrationTbSimplesNfManual integrationNfManual = new IntegrationTbSimplesNfManual();
                var nfeServico = new ServicoEntity();

                var empresaEmite = _dbContextComapany.GetColection.Find(c => c.Id.Equals(nfServico.CompanyInformation)).FirstOrDefault();
                if (empresaEmite != null)
                {
                    nfeServico.CnpjEmitente = empresaEmite.Cnpj;

                    nfeServico.DataEmissao = DateTime.Now;
                    nfeServico.ValorServicos = nfServico.Demonstrative.ServicesValue - nfServico.Demonstrative.UnconditionalDiscount;
                    nfeServico.Numero = nfServico.Id.ToString();
                    nfeServico.CompanyInformation = nfServico.CompanyInformation;
                }

                var valorServico = nfServico.Demonstrative.LiquidValue;

                await integrationNfManual.CreateTbNfeServicoPrestador(nfeServico, nfServico.Taker, valorServico);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task Insert(NfServicoManual nfServico, bool trasportadora)
        {
            try
            {
                if (nfServico.Taker.Cnpj_Cpf.Length == 11)
                {
                    var empresaDest = new EmpresaDest();

                    empresaDest.Id = Guid.NewGuid();
                    empresaDest.Fantasia = nfServico.Taker.Name;
                    empresaDest.CPF = nfServico.Taker.Cnpj_Cpf;
                    empresaDest.Cidade = nfServico.Taker.City;
                    empresaDest.Bairro = nfServico.Taker.Neighborhood;
                    empresaDest.RazaoSocial = nfServico.Taker.Name;
                    empresaDest.Numero = nfServico.Taker.Name;
                    empresaDest.Endereco = nfServico.Taker.State;
                    _dbContextEmpresaDest.GetColection.InsertOne(empresaDest);

                    nfServico.EmpresaDestId = empresaDest.Id;

                }
                nfServico.DataEmissão = DateTime.Now;

                await _dbContextNfServicoManual.GetColection.InsertOneAsync(nfServico).ConfigureAwait(false);
                await AddNfeServico(nfServico);
                await InsertNfeServico(nfServico, trasportadora);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task AddNfeServico(NfServicoManual nfServico)
        {
            try
            {
                var empresaEmi = _dbContextComapany.GetColection.Find(c => c.Id.Equals(nfServico.CompanyInformation)).FirstOrDefault();
                var empresaDest = _dbContextEmpresaDest.GetColection.Find(c => c.Cnpj == nfServico.Taker.Cnpj_Cpf || c.CPF == nfServico.Taker.Cnpj_Cpf).FirstOrDefault();

                var nfe = new ServicoEntity()
                {
                    Aliquota = 0,
                    BaseCalculo = nfServico.Demonstrative.LiquidValue.ToString(),
                    CnpjEmitente = empresaEmi.Cnpj,
                    CompanyInformation = empresaEmi.Id.Value,
                    ModeloNota = "Prestador",
                    ValorServicos = nfServico.Demonstrative.LiquidValue,
                    CodigoMunicipio = nfServico.Taker.CitySubscription,
                    DataEmissao = DateTime.Now,
                    DescontoIncondicionado = nfServico.Demonstrative.UnconditionalDiscount,
                    ValorIss = nfServico.Demonstrative.RetainedIssqn,
                    CodigoTributacaoMunicipio = "0",
                    CodigoVerificacao = "0",
                    Serie = "1",
                    Discriminacao = "",
                    Id = nfServico.Id,
                    Numero = Guid.NewGuid().ToString(),
                    Status = "Ativo",
                    Tipo = "Manual",
                    EmpresaEmetId = empresaEmi.Id.Value,
                    ETipoNota = Domain.Enum.ETipoNota.NotaFiscalDeServico,
                    EmpresaDesId = empresaDest.Id

                };

                _dbContextServico.GetColection.InsertOne(nfe);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<NFE>> GetByFilter(Guid companyId, string tipoNfe, string descProduto, string cnpj, DateTime? dhEmiss, string uf, string nomeCli)
        {
            var filter = Builders<NFE>.Filter.Eq(c => c.CompanyInformation, companyId);
            filter &= Builders<NFE>.Filter.Eq(c => c.ModeloTipo, tipoNfe);

            if (dhEmiss.HasValue)
            {
                var inicioMes = new DateTime(dhEmiss.Value.Year, dhEmiss.Value.Month, 1);
                var fimMes = inicioMes.AddMonths(1).AddDays(-1);
                filter &= Builders<NFE>.Filter.Gte(x => x.DhEmi, inicioMes) &
                         Builders<NFE>.Filter.Lt(x => x.DhEmi, fimMes);
            }
            if (!string.IsNullOrEmpty(uf))
                filter &= Builders<NFE>.Filter.Eq(c => c.UFEnv, uf);
            if (tipoNfe.Equals("Entrada") || tipoNfe.Equals("DevolucaoCompra"))
            {
                var query = from nfe in _dbContext.GetColection.Find(filter).ToList()
                            join produtos in _dbContextProdutosFornecedores.GetColection.AsQueryable().ToList() on nfe.Id equals produtos.NfeId
                            join empresaDest in await EmpresaDestRepository.GetAll() on nfe.EmpresaDesId equals empresaDest.Id
                            select new { nfe, produtos, empresaDest };

                if (!string.IsNullOrEmpty(descProduto))
                    query = query.Where(c => c.produtos.DescProduto.ToUpper().Contains(descProduto.ToUpper()));
                if (!string.IsNullOrEmpty(cnpj) && tipoNfe.Equals("Entrada"))
                    query = query.Where(c => c.empresaDest.Cnpj == cnpj);
                if (!string.IsNullOrEmpty(nomeCli))
                    query = query.Where(c => c.empresaDest.RazaoSocial.ToUpper().Contains(nomeCli.ToUpper()));
                List<NFE> nfes = new List<NFE>();
                var nfesGroup = query.GroupBy(x => x.nfe.Id).Select(c => c.First()).Select(e => e.nfe);
                var result = await _dbContextComapany.GetColection.FindAsync(e => e.Id == companyId);
                var empresa = result.FirstOrDefault();
                foreach (var c in nfesGroup)
                {

                    if (!nfes.Any(x => x.Id == c.Id))
                        nfes.Add(new NFE
                        {
                            Id = c.Id,
                            Nnfe = c.Nnfe,
                            CNPJEmitente = c.CNPJEmitente,
                            TipNfe = c.TipNfe,
                            Modelo = c.Modelo,
                            ModeloTipo = c.ModeloTipo,
                            VtTotalNfe = c.VtTotalNfe,
                            DhEmi = c.DhEmi,
                            Danfe = c.Danfe,
                            UFEnv = c.UFEnv,
                            Cnpj = empresa != null ? empresa.Cnpj : null,
                            RazaoSocial = empresa != null ? empresa.Name : null
                        });
                }
                return nfes;


            }
            else
            {
                var query1 = from nfe in _dbContext.GetColection.Find(filter).ToList()
                             join produtos in _dbContextProduto.GetColection.AsQueryable().ToList() on nfe.Id equals produtos.NfeId
                             join empresaDest in await EmpresaDestRepository.GetAll() on nfe.EmpresaDesId equals empresaDest.Id
                             select new { nfe, produtos, empresaDest };

                if (!string.IsNullOrEmpty(descProduto))
                    query1 = query1.Where(c => c.produtos.DescProduto.ToUpper().Contains(descProduto.ToUpper()));
                if (!string.IsNullOrEmpty(cnpj) && tipoNfe.Equals("Entrada"))
                    query1 = query1.Where(c => c.empresaDest.Cnpj == cnpj);
                if (!string.IsNullOrEmpty(nomeCli))
                    query1 = query1.Where(c => c.empresaDest.RazaoSocial.ToUpper().Contains(nomeCli.ToUpper()));

                foreach (var item in query1)
                {
                    var result = await _dbContextComapany.GetColection.FindAsync(e => e.Id == item.nfe.CompanyInformation);
                    var empresa = result.FirstOrDefault();
                    item.nfe.Cnpj = empresa != null ? empresa.Cnpj : null;
                    item.nfe.RazaoSocial = empresa != null ? empresa.Name : null;
                }
                return query1.GroupBy(x => x.nfe.Id).Select(c => c.First()).Select(c => new NFE
                {
                    Id = c.nfe.Id,
                    Nnfe = c.nfe.Nnfe,
                    ModeloTipo = c.nfe.ModeloTipo,
                    VtTotalNfe = c.nfe.VtTotalNfe,
                    DhEmi = c.nfe.DhEmi,
                    Danfe = c.nfe.Danfe,
                    Modelo = c.nfe.Modelo,
                    Cnpj = c.nfe.Cnpj,
                    RazaoSocial = c.nfe.RazaoSocial,
                    UFEnv = c.nfe.UFEnv,
                }).ToList();
            }
        }

        public async Task<List<RetornoNfeCancelada>> GetByFilterCanceladas(Guid companyId, string tipoNfe, string cnpj, DateTime? dhEmiss)
        {
            var query1 = from nfeCanceladas in _dbContextNfCanceladas.GetColection.AsQueryable().Where(c => c.ModeloNota == tipoNfe).ToList()
                         join nfe in _dbContext.GetColection.AsQueryable().Where(c => c.CompanyInformation == companyId).ToList() on nfeCanceladas.RefNfe equals nfe.CodBarra
                         select new RetornoNfeCancelada { NFe = nfe, NfeCancelada = nfeCanceladas };

            if (!string.IsNullOrEmpty(cnpj))
                query1 = query1.Where(c => c.NfeCancelada.CnpjEmitente == cnpj);
            if (dhEmiss.HasValue)
            {
                var inicioMes = new DateTime(dhEmiss.Value.Year, dhEmiss.Value.Month, 1);
                var fimMes = inicioMes.AddMonths(1).AddDays(-1);
                query1 = query1.Where(c => c.NfeCancelada.DhEvento >= inicioMes && c.NfeCancelada.DhEvento <= fimMes);
            }
            if (tipoNfe.Equals("Entrada"))
            {
                foreach (var item in query1.Select(c => c.NFe))
                {
                    var empresaEmit = await _empresaEmitRepository.GetById(item.EmpresaEmetId);
                    item.Danfe = item.Danfe;
                }
            }
            else
            {
                foreach (var item in query1.Select(c => c.NFe))
                {
                    var empresaDest = await EmpresaDestRepository.GetById(item.EmpresaDesId);
                    item.Danfe = item.Danfe;
                }
            }

            return query1.ToList();
        }

        public async Task<bool> NotaJaFoiGravada(string chave)
        {
            try
            {
                var result = _dbContext.GetColection.Find(c => c.CodBarra.Equals(chave)).FirstOrDefault();
                if (result != null)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task InsertProdutos(Produtos produtos)
        {

            produtos.Id = Guid.NewGuid();
            _dbContextProduto.GetColection.InsertOne(produtos);



        }

        public async Task InsertFornec(ProdutosFornecedor produtos)
        {

            produtos.Id = Guid.NewGuid();
            _dbContextProdutosFornecedores.GetColection.InsertOne(produtos);


        }

        #endregion

        #region REGISTROS BL E

        private RegistroBlE ObtenhaRegistroBlE(List<NFE> listaNfe, DateTime dataInicio, DateTime dataFim)
        {
            var registroBlE = new RegistroBlE();

            if (listaNfe.Count > 0)
            {
                registroBlE.registroE100 = new RegistroE100 { dataInicial = dataInicio, dataFinal = dataFim };
                registroBlE.registroE100.Id = Guid.NewGuid();
                registroBlE.registroE110 = ObtenhaRegistroE110(listaNfe, dataFim);
                registroBlE.registroE111 = ObtenhaRegistroE111(listaNfe[0]);
                registroBlE.registroE113 = ObtenhaRegistroE113(listaNfe[0]);
                registroBlE.registroE115 = ObtenhaRegistroE115(listaNfe[0]);
                registroBlE.registroE116 = ObtenhaRegistroE116(listaNfe[0], dataFim);
            }

            return registroBlE;
        }

        private RegistroE110 ObtenhaRegistroE110(List<NFE> listaNfe, DateTime dataFim)
        {

            var registroE110 = new RegistroE110();

            registroE110.Id = Guid.NewGuid();
            registroE110.valorDebitoImpostos = 0;
            registroE110.valorAjustesDebitoDocFiscal = 0;
            registroE110.valorAjustesDebito = 0;
            registroE110.valorEstornosCreditos = 0;
            registroE110.valorCreditoImpostos = 0;
            registroE110.dataFinal = dataFim;
            registroE110.valorAjustesCreditoDocFiscal = 0;
            registroE110.valorAjustesCredito = 0;
            registroE110.valorEstornosDebitos = 0;
            registroE110.saldoCredorAnterior = 0;
            registroE110.valorSaldoDevedor = Convert.ToDecimal(listaNfe.Select(c => c.VtIcms).Sum());
            registroE110.valorDeducoes = 0;
            registroE110.valorIcmsRecolher = 0;
            registroE110.valorSaldoCredorIcms = 0;
            registroE110.extraApuracao = 0;


            return registroE110;
        }

        private RegistroE111 ObtenhaRegistroE111(NFE nfe)
        {
            var registroE111 = new RegistroE111();
            registroE111.Id = Guid.NewGuid();
            registroE111.codAjuste = "";
            registroE111.descComplementar = "";
            registroE111.valorAjuste = 0;

            return registroE111;
        }

        private RegistroE113 ObtenhaRegistroE113(NFE nfe)
        {
            var registroE113 = new RegistroE113();
            registroE113.Id = Guid.NewGuid();
            registroE113.codParticipante = "";
            registroE113.codModeloDocumento = nfe.Modelo;
            registroE113.serie = nfe.Serie.ToString();
            registroE113.subserie = "";
            registroE113.numeroDocumento = nfe.Nnfe;
            registroE113.dataEmissao = nfe.DhEmi.Value;
            registroE113.codItem = "";
            registroE113.valorAjusteItem = 0;
            registroE113.chave = nfe.Nnfe;

            return registroE113;
        }

        private RegistroE115 ObtenhaRegistroE115(NFE nfe)
        {
            var registroE115 = new RegistroE115();
            registroE115.Id = Guid.NewGuid();
            registroE115.codInformacao = "";
            registroE115.valorInformacao = 0;
            registroE115.descComplementar = "";

            return registroE115;
        }

        private RegistroE116 ObtenhaRegistroE116(NFE nfe, DateTime dataFim)
        {
            var registroE116 = new RegistroE116();
            registroE116.Id = Guid.NewGuid();
            registroE116.codIcms = "";
            registroE116.valorIcms = 0;
            registroE116.dataVencimentoIcms = "";
            registroE116.codReceita = "";
            registroE116.numeroProcesso = "";
            registroE116.origemProcesso = 0;
            registroE116.descProcesso = "";
            registroE116.descComplementar = "";
            registroE116.mesReferencia = dataFim.Month.ToString();

            return registroE116;
        }

        #endregion

        #region REPOSITÓRIOS

        private ProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new ProductRepository();
                }

                return _productRepository;
            }
        }

        private ImpostosRepository ImpostosRepository
        {
            get
            {
                if (_impostoRepository == null)
                {
                    _impostoRepository = new ImpostosRepository();
                }

                return _impostoRepository;
            }
        }

        private EmpresaDestRepository EmpresaDestRepository
        {
            get
            {
                if (_empresaDestRepository == null)
                {
                    _empresaDestRepository = new EmpresaDestRepository();
                }

                return _empresaDestRepository;
            }
        }

        private ServicoEntityRepository ServicoEntityRepository
        {
            get
            {
                if (_servicoEntityRepository == null)
                {
                    _servicoEntityRepository = new ServicoEntityRepository();
                }

                return _servicoEntityRepository;
            }
        }

        public async Task<bool> CreateXmlNfe(Domain.Entities.ModeloXml.NotaFiscalEletronicaMod55.NfeProc nota)
        {
            if (nota.NFe.InfNFe.Emit.CNPJ != null)
            {
                try
                {
                    var operacao = nota.NFe.InfNFe.Ide.NatOp;

                    var empresaDto = _companyRepository.ExistsCompany(nota.NFe.InfNFe.Emit.CNPJ);
                    if (empresaDto.Result)
                    {
                        nota.ModeloNota = "Saida";
                        nota.CnpjEmitente = nota.NFe.InfNFe.Emit.CNPJ;
                    }
                    else if (empresaDto == null)
                    {
                        nota.ModeloNota = "Entrada";
                        nota.CnpjEmitente = nota.NFe.InfNFe.Dest.CNPJ;
                    }

                    var nfe = EntidadeXmlToEntidadeMongodbMod55.CreateEntidadeMongoNotaFiscal(nota);
                    if (await NotaJaFoiGravada(nfe.CodBarra))
                        return false;
                    if (nfe == null)
                        return false;

                    if (string.IsNullOrEmpty(nota.NFe.InfNFe.Emit.CNPJ))
                        return false;

                    var emit = await _empresaEmitRepository.ObterPorCnpj(nota.NFe.InfNFe.Emit.CNPJ.Replace(".", "").Replace("/", "").Replace("-", ""));
                    var cpfCnpj = string.IsNullOrEmpty(nota.NFe.InfNFe.Dest.CNPJ) ? nota.NFe.InfNFe.Dest.CPF : nota.NFe.InfNFe.Dest.CNPJ;
                    if (string.IsNullOrEmpty(cpfCnpj))
                        return false;
                    var dest = await __empresaDestRepository.ObterPorCnpj(cpfCnpj.Replace(".", "").Replace("/", "").Replace("-", ""));

                    if (emit == null)
                    {
                        var emitMongo = await _empresaEmitRepository.Insert(EntidadeXmlToEntidadeMongodbMod55.CretaEntidadeMongoEmpresaEmitente(nota.NFe.InfNFe.Emit));
                        nfe.EmpresaEmetId = emitMongo.Id.Value;
                        emit = emitMongo;
                    }
                    else { nfe.EmpresaEmetId = emit.Id.Value; }
                    if (dest == null)
                    {
                        var destMongo = await __empresaDestRepository.Insert(EntidadeXmlToEntidadeMongodbMod55.CretaEntidadeMongoEmpresaDestinatario(nota.NFe.InfNFe.Dest));
                        nfe.EmpresaDesId = destMongo.Id.Value;
                    }
                    else { nfe.EmpresaDesId = dest.Id.Value; }

                    var nfeMongo = await Insert(nfe);

                    if (nfe.ModeloTipo == "Venda")
                    {
                        var listaProdDto = EntidadeXmlToEntidadeMongodbMod55.CriateProdutos(nota.NFe.InfNFe.Det, nfe.DhEmi);
                        listaProdDto.ForEach(c =>
                        {
                            c.NfeId = nfe.Id.Value;
                            c.CompanyInformation = nfe.CompanyInformation;
                            InsertProdutos(c);

                        });
                    }
                    else
                    {
                        var listaProdForncedorDto = EntidadeXmlToEntidadeMongodbMod55.CriateProdutosFornecedor(nota.NFe.InfNFe.Det);
                        listaProdForncedorDto.ForEach(c =>
                        {
                            c.NfeId = nfe.Id.Value;
                            c.CompanyInformation = nfe.CompanyInformation;
                            InsertFornec(c);

                        });
                    }

                    var listaImpostos = EntidadeXmlToEntidadeMongodbMod55.CriateImpostos(nota.NFe.InfNFe.Det);
                    listaImpostos.ForEach(c =>
                    {
                        _impostos.Insert(c);
                    });

                    return true;
                }
                catch (Exception ex)
                {

                    throw;
                }

            }

            return false;
        }

        public async Task<List<TbCodServico>> GetAllCodServico()
        {
            var result = _dbContextCodServico.GetColection.Find(c => c.codservico > 0).ToList();

            return result;
        }

        public async Task<AjusteNfe> InsertAjusteNfe(AjusteNfe ajusteNfe)
        {        

            try
            {
               
                var ajusteDto = _dbContextAjuste.GetColection.Find(c => c.CodAjuste == ajusteNfe.CodAjuste).FirstOrDefault();
                if (ajusteDto == null)
                {
                    ajusteNfe.Id = Guid.NewGuid();
                    await _dbContextAjuste.GetColection.InsertOneAsync(ajusteNfe);
                }
                 
                var inicioMes = new DateTime(ajusteNfe.DhEmiss.Year, ajusteNfe.DhEmiss.Month, 1);
                var fimMes = inicioMes.AddMonths(1).AddDays(-1);

                if (ajusteNfe.Cfops.Count > 0)
                {
                    var queryNfeVendas = from nfeVendas in _dbContext.GetColection.AsQueryable().Where(c => c.CompanyInformation == ajusteNfe.CompanyInformation && c.DhEmi.HasValue && c.DhEmi.Value >= inicioMes && c.DhEmi.Value <= fimMes && c.ModeloTipo == "Venda" && c.Modelo == "55").ToList()
                                         join produtos in _dbContextProduto.GetColection.AsQueryable().Where(c => c.CompanyInformation == ajusteNfe.CompanyInformation).ToList() on nfeVendas.Id equals produtos.NfeId
                                         where ajusteNfe.Cfops.Any(c => Convert.ToDouble(c) == produtos.Cfop) && nfeVendas.ModeloTipo == "Venda"
                                         select new NotasPorProdutos { Nfe = nfeVendas, Produtos = produtos };

                    ajusteNfe.VlTotalNfe = queryNfeVendas.Select(c => c.Produtos.VlProduto).Sum();

                    if (ajusteNfe.VlTotalNfe > 0)
                        return ajusteNfe;
                }
                if (ajusteNfe.Csts.Count > 0)
                {

                }
                if (ajusteNfe.Ncms.Count > 0)
                {
                    var queryNfeVendas = from nfeVendas in _dbContext.GetColection.AsQueryable().Where(c => c.CompanyInformation == ajusteNfe.CompanyInformation && c.DhEmi.HasValue && c.DhEmi.Value >= inicioMes && c.DhEmi.Value <= fimMes && c.ModeloTipo == "Venda" && c.Modelo == "55").ToList()
                                         join produtos in _dbContextProduto.GetColection.AsQueryable().Where(c => c.CompanyInformation == ajusteNfe.CompanyInformation).ToList() on nfeVendas.Id equals produtos.NfeId
                                         where ajusteNfe.Ncms.Any(c => Convert.ToDouble(c) == produtos.Cfop) && nfeVendas.ModeloTipo == "Venda"
                                         select new NotasPorProdutos { Nfe = nfeVendas, Produtos = produtos };

                    ajusteNfe.VlTotalNfe = queryNfeVendas.Select(c => c.Produtos.VlProduto).Sum();
                    if (ajusteNfe.VlTotalNfe > 0)
                        return ajusteNfe;
                }
                if (ajusteNfe.Tipo != null || ajusteNfe.TipoNota != null || ajusteNfe.Totalizador != null)
                {

                    if (ajusteNfe.Totalizador.Contains("Total por Produtos"))
                    {
                        var queryNfeVendas = from nfeVendas in _dbContext.GetColection.AsQueryable().Where(c => c.CompanyInformation == ajusteNfe.CompanyInformation && c.DhEmi.HasValue && c.DhEmi.Value >= inicioMes && c.DhEmi.Value <= fimMes && c.ModeloTipo == "Venda" && c.Modelo == "55").ToList()
                                             join produtos in _dbContextProduto.GetColection.AsQueryable().Where(c => c.CompanyInformation == ajusteNfe.CompanyInformation).ToList() on nfeVendas.Id equals produtos.NfeId
                                             where nfeVendas.ModeloTipo == "Venda"
                                             select new NotasPorProdutos { Nfe = nfeVendas, Produtos = produtos };


                        ajusteNfe.VlTotalNfe = queryNfeVendas.Select(c => c.Produtos.VlProduto).Sum();

                        return ajusteNfe;
                    }
                    else if (ajusteNfe.Totalizador.Contains("Total por Base"))
                    {
                        var nfeConsulta = _dbContext.GetColection.Find(x => x.ModeloTipo == ajusteNfe.TipoNota && x.TipAten == x.TipAten &&
                       x.CompanyInformation == ajusteNfe.CompanyInformation && x.BaseCAlIcms > 0).ToList();

                        ajusteNfe.VlTotalNfe = nfeConsulta.Select(c => c.VtTotalNfe).Sum();

                        return ajusteNfe;

                    }
                    else if (ajusteNfe.Totalizador.Contains("Total por nota Fiscal"))
                    {
                        var nfeConsulta = _dbContext.GetColection.Find(x => x.ModeloTipo == ajusteNfe.TipoNota && x.TipAten == x.TipAten &&
                       x.CompanyInformation == ajusteNfe.CompanyInformation).ToList();

                        ajusteNfe.VlTotalNfe = nfeConsulta.Select(c => c.VtTotalNfe).Sum();

                        return ajusteNfe;
                    }
                }
                var ajuste = new AjusteNfe();

                return ajuste;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> UpdateAluste(Guid idAjuste, double totalNfe, double aliquota, double totalCalculo)
        {

            var ajusteDto = _dbContextAjuste.GetColection.Find(c => c.Id.Equals(idAjuste)).FirstOrDefault();

            if (ajusteDto != null)
            {
                var update = Builders<AjusteNfe>.Update.Set(c => c.TotalNfe, totalNfe)
                                             .Set(c => c.Aliquota, aliquota)
                                             .Set(c => c.TotalNfe, totalCalculo);

                var updateResult = await _dbContextAjuste.GetColection.UpdateOneAsync(c => c.Id == ajusteDto.Id, update);

                if (updateResult.ModifiedCount > 0)
                {
                    return true;
                }

                return false;
            }


            return false;
        }

        public (List<LivroFiscal>, List<LigroFiscalRodape>) GetAllNfe(Guid empresaId, DateTime dataOperacao, string operacao)
        {
            var livroFiscalDto = new List<LivroFiscal>();
            var inicioMes = new DateTime(dataOperacao.Year, dataOperacao.Month, 1);
            var fimMes = inicioMes.AddMonths(1).AddDays(-1);
            var listNfeCodBarra = new List<NFE>();
            var totalTributado = 0.0;
            List<double> listCfop = new List<double>();
            var empresaDto = _dbContextComapany.GetColection.Find(c => c.Id == empresaId).FirstOrDefault();

            if (operacao == "Venda")
            {
                var queryVenda = _dbContext.GetColection.Find(c => c.CompanyInformation == empresaId && c.ModeloTipo == operacao && (c.DhEmi.HasValue && c.DhEmi.Value >= inicioMes && c.DhEmi.Value <= fimMes && c.Modelo == "55")).ToList();

                var queryDevolucao = _dbContext.GetColection.Find(c => c.CompanyInformation == empresaId && c.ModeloTipo == "DevolucaoSaida" && (c.DhEmi.HasValue && c.DhEmi.Value >= inicioMes && c.DhEmi.Value <= fimMes && c.Modelo == "55")).ToList();

                foreach (var item in queryVenda)
                {
                    var listItens = new List<Items>();
                    if (operacao == "Venda")
                    {
                        listItens = ObtenhaListaDeItens(item, false).Result;
                    }
                    else
                    {
                        listItens = ObtenhaListaDeItens(item, true).Result;
                    }

                    foreach (var prod in listItens)
                    {
                        if (prod.ProdutoBaySimples)
                        {
                            totalTributado = totalTributado + prod.TotalItemValue;

                            livroFiscalDto.Add(new LivroFiscal()
                            {
                                RazaoSocial = empresaDto.Name,
                                Cnpj = empresaDto.Cnpj,
                                Inscricao = empresaDto.Sintegra.Registrations.Select(c => c.Number).FirstOrDefault(),
                                DataFinal = fimMes,
                                DataInicial = inicioMes,
                                Serie = item.Serie.ToString(),
                                Especie = "ESPÉCIE",
                                N_NotaFiscal = item.Nnfe,
                                Dia = item.DhEmi.Value.Day,
                                EstadoEmissao = item.UFEnv,
                                TotalNfeTributado = prod.TotalItemValue,
                                CFOP = prod.Cfop,
                                Operacao = item.ModeloTipo,
                                ALIQ = 0.0,
                                BASECALCULO = 0.0,
                                IMPDEBITADO = 0.0,
                                OUTRAS = 0.0,
                                TotalNfeNaoTributado = prod.TotalItemValue,
                            });

                            listCfop.Add(prod.Cfop);
                        }

                    }
                }
                foreach (var item in queryDevolucao)
                {
                    var listItens = new List<Items>();
                    if (operacao == "Venda")
                    {
                        listItens = ObtenhaListaDeItens(item, false).Result;
                    }
                    else
                    {
                        listItens = ObtenhaListaDeItens(item, true).Result;
                    }

                    foreach (var prod in listItens)
                    {
                        if (prod.ProdutoBaySimples)
                        {
                            totalTributado = totalTributado + prod.TotalItemValue;

                            livroFiscalDto.Add(new LivroFiscal()
                            {
                                RazaoSocial = empresaDto.Name,
                                Cnpj = empresaDto.Cnpj,
                                Inscricao = empresaDto.Sintegra.Registrations.Select(c => c.Number).FirstOrDefault(),
                                DataFinal = fimMes,
                                DataInicial = inicioMes,
                                Serie = item.Serie.ToString(),
                                Especie = "ESPÉCIE",
                                N_NotaFiscal = item.Nnfe,
                                Dia = item.DhEmi.Value.Day,
                                EstadoEmissao = item.UFEnv,
                                TotalNfeTributado = prod.TotalItemValue,
                                CFOP = prod.Cfop,
                                Operacao = item.ModeloTipo,
                                ALIQ = 0.0,
                                BASECALCULO = 0.0,
                                IMPDEBITADO = 0.0,
                                OUTRAS = 0.0,
                                TotalNfeNaoTributado = prod.TotalItemValue,
                            });

                            listCfop.Add(prod.Cfop);
                        }
                    }
                }

                var listRodaPe = new List<LigroFiscalRodape>();

                foreach (var cfopDto in listCfop.Distinct().ToList())
                {
                    var livroFiscalCfop = livroFiscalDto.Where(C => C.CFOP == cfopDto).ToList();

                    listRodaPe.Add(new LigroFiscalRodape()
                    {
                        Cfop = cfopDto,
                        Outros = 0.0,
                        TotalBase = 0.0,
                        TotalCfopNaoTrib = livroFiscalCfop.Sum(c => c.TotalNfeTributado),
                        TotalCfopTrib = livroFiscalCfop.Sum(c => c.TotalNfeTributado),
                        TotalLig = 0.0
                    });
                }

                return (livroFiscalDto, listRodaPe);

            }
            else
            {
                var query = _dbContext.GetColection.Find(c => c.CompanyInformation == empresaId && c.ModeloTipo == operacao && (c.DhEmi.HasValue && c.DhEmi.Value >= inicioMes && c.DhEmi.Value <= fimMes && c.Modelo == "55")).ToList();

                foreach (var item in query)
                {
                    var listItens = new List<Items>();
                    if (operacao == "Entrada")
                    {
                        listItens = ObtenhaListaDeItens(item, false).Result;
                    }
                    else
                    {
                        listItens = ObtenhaListaDeItens(item, true).Result;
                    }

                    foreach (var prod in listItens)
                    {

                        totalTributado = totalTributado + prod.TotalItemValue;

                        livroFiscalDto.Add(new LivroFiscal()
                        {
                            RazaoSocial = empresaDto.Name,
                            Cnpj = empresaDto.Cnpj,
                            Inscricao = empresaDto.Sintegra.Registrations.Select(c => c.Number).FirstOrDefault(),
                            DataFinal = fimMes,
                            DataInicial = inicioMes,
                            DataEmissao = item.DhEmi.Value.Date,
                            Serie = item.Serie.ToString(),
                            Especie = "ESPÉCIE",
                            N_NotaFiscal = item.Nnfe,
                            Dia = item.DhEmi.Value.Day,
                            EstadoEmissao = item.UFEnv,
                            TotalNfeTributado = prod.TotalItemValue,
                            CFOP = prod.Cfop,
                            Operacao = item.ModeloTipo,
                            ALIQ = 0.0,
                            BASECALCULO = 0.0,
                            IMPDEBITADO = 0.0,
                            OUTRAS = 0.0,
                            TotalNfeNaoTributado = prod.TotalItemValue,
                        });

                        listCfop.Add(prod.Cfop);

                    }
                }

                var listRodaPe = new List<LigroFiscalRodape>();

                foreach (var cfopDto in listCfop.Distinct().ToList())
                {
                    var livroFiscalCfop = livroFiscalDto.Where(C => C.CFOP == cfopDto).ToList();

                    listRodaPe.Add(new LigroFiscalRodape()
                    {
                        Cfop = cfopDto,
                        Outros = 0.0,
                        TotalBase = 0.0,
                        TotalCfopNaoTrib = livroFiscalCfop.Sum(c => c.TotalNfeTributado),
                        TotalCfopTrib = livroFiscalCfop.Sum(c => c.TotalNfeTributado),
                        TotalLig = 0.0
                    });
                }

                return (livroFiscalDto, listRodaPe);
            }
        }

        public async Task<List<NfVendaManual>> GetAllNfeManualVenda(Guid empresaId)
        {
            try
            {
                var result = _dbContextNfVendaManual.GetColection.Find(c => c.CompanyInformation == empresaId).ToList();
                return result;

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<List<NfServicoManual>> GetAllNfeManualServi(Guid empresaId)
        {
            return _dbContextNfServicoManual.GetColection.Find(c => c.CompanyInformation == empresaId).ToList();
        }

        public async Task<bool> DeleteNfeManualVenda(Guid idNfe)
        {
            var nfe = _dbContext.GetColection.Find(c => c.Id == idNfe).FirstOrDefault();
            var company = _dbContextComapany.GetColection.Find(c => c.Id == nfe.CompanyInformation).FirstOrDefault();
            DebitarValorSimples(nfe.VtTotalNfe, nfe.DhEmi.Value, company.Cnpj, nfe.Id.ToString(), "venda");
            var resut = _dbContextNfVendaManual.GetColection.DeleteOne(c => c.Id == idNfe);
            var resultNfe = _dbContext.GetColection.DeleteOne(c => c.Id == nfe.Id);
            if (resut.DeletedCount > 0 && resultNfe.DeletedCount > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteNfeManualServi(Guid idNfe)
        {
            var servicoEntity = _dbContextServico.GetColection.Find(c => c.Id == idNfe).FirstOrDefault();
            if (servicoEntity == null)
                return false;
            var company = _dbContextComapany.GetColection.Find(c => c.Id == servicoEntity.CompanyInformation).FirstOrDefault();
            if (company == null)
                return false;
            DebitarValorSimples(servicoEntity.ValorServicos, servicoEntity.DataEmissao, company.Cnpj, servicoEntity.Id.ToString(), "servico");
            var resut = _dbContextNfServicoManual.GetColection.DeleteOne(c => c.Id == idNfe);
            var resultEntity = _dbContextServico.GetColection.DeleteOne(c => c.Id == servicoEntity.Id);
            if (resut.DeletedCount > 0 && resultEntity.DeletedCount > 0)
            {
                return true;
            }
            return false;
        }

        private void DebitarValorSimples(double valorServicos, DateTime dataEmissao, string cnpj, string idNfe, string tipo)
        {
            IMongoClient mongoClient = new MongoClient("mongodb://contta:contta123456@192.46.218.34:27017/?authSource=admin&readPreference=primary&ssl=false");
            IMongoDatabase database = mongoClient.GetDatabase("conttadb");

            IMongoCollection<TbDashboardClientes> _collectionTbSimples = database.GetCollection<TbDashboardClientes>("TbSimples");
            var resultTbListaSimplesDto = _collectionTbSimples.Find(c => c.DataEmissaoMensais.Ano == dataEmissao.Year && c.DataEmissaoMensais.Mes == dataEmissao.Month).FirstOrDefault();
            if (resultTbListaSimplesDto == null)
                throw new Exception($"Tabela: TbSimples{cnpj} não encontrada.");
            //if(resultTbListaSimplesDto.ListaNfe.Any(c=> c.CodBarra == idNfe))
            //{
            UpdateDefinition<TbDashboardClientes> update;
            if (tipo.Equals("servico"))
                update = Builders<TbDashboardClientes>.Update.Set(c => c.ValorContabil.NotaServicoPrestador, resultTbListaSimplesDto.ValorContabil.NotaServicoPrestador - valorServicos);
            else
                update = Builders<TbDashboardClientes>.Update.Set(c => c.ValorContabil.ValorSaidaMercadoria, resultTbListaSimplesDto.ValorContabil.ValorSaidaMercadoria - valorServicos);
            var updateResult = _collectionTbSimples.UpdateOne(c => c.Id == resultTbListaSimplesDto.Id, update);
            //}
        }

        public async Task<List<AjusteNfe>> GetAllBlocoE(Guid empresaId, string operacao)
        {
            var result = _dbContextAjuste.GetColection.Find(c => c.CompanyInformation == empresaId && c.TipoNota == operacao).ToList();

            return result;
        }

        public async Task<TotalizadorNfeSaida> GetTotalizadorNfeSaida(Guid empresaId, DateTime data)
        {
            var inicioMes = new DateTime(data.Year, data.Month, 1);

            var nfeSaida = _dbContext.GetColection
                                .AsQueryable().Where(c => c.CompanyInformation == empresaId && c.ModeloTipo == "Venda" &&
                                (c.DhEmi.HasValue && c.DhEmi.Value >= inicioMes && c.DhEmi.Value <= data)).ToList();

            var nfeCanceladas = from canceladas in _dbContextNfCanceladas.GetColection.AsQueryable().Where(c => c.ModeloNota == "Venda").ToList()
                                join nfe in nfeSaida.ToList() on canceladas.RefNfe equals nfe.CodBarra
                                select new { nfe, canceladas };
            var nfeOrdenada = nfeSaida.ToList().OrderBy(c => c.Nnfe).ToList();

            return new TotalizadorNfeSaida
            (
               nfeOrdenada.FirstOrDefault()?.Nnfe,
               nfeOrdenada.LastOrDefault()?.Nnfe,
               nfeCanceladas.ToList().Select(c => c.nfe).Count(),
               0,
               nfeCanceladas.ToList().Select(c => c.nfe.Nnfe).ToList(),
               nfeOrdenada.ToList().Select(c => c.Nnfe).Distinct().ToList()
            );

        }

        public async Task<RegistroE110> InsertTbAjuste(List<TbDeducao> tbDeducao, double valorSaldoDevedor)
        {
            var result = _dbContextAjusteDb.GetColection.Find(c => c.Code != null).ToList();
            var valorCredito = 0.0;
            var valorDEbito = 0.0;

            foreach (var item in tbDeducao)
            {
                var tipo = result.FirstOrDefault(c => c.Code == item.CodAjust);

                if (tipo != null)
                {
                    if (tipo.Type == "Débito")
                    {
                        valorCredito = item.ValorAjuste + valorCredito;
                    }
                    else if (tipo.Type == "Crédito")
                    {
                        valorDEbito = item.ValorAjuste + valorDEbito;
                    }
                }
            }

            var registroE110 = new RegistroE110();

            registroE110.Id = Guid.NewGuid();
            registroE110.valorDebitoImpostos = 0;
            registroE110.valorAjustesDebitoDocFiscal = 0;
            registroE110.valorAjustesDebito = Convert.ToDecimal(valorDEbito);
            registroE110.valorEstornosCreditos = Convert.ToDecimal(valorCredito);
            registroE110.valorCreditoImpostos = 0;
            registroE110.valorAjustesCreditoDocFiscal = 0;
            registroE110.valorAjustesCredito = 0;
            registroE110.valorEstornosDebitos = 0;
            registroE110.saldoCredorAnterior = 0;
            registroE110.valorSaldoDevedor = Convert.ToDecimal(valorSaldoDevedor);
            registroE110.valorDeducoes = 0;
            registroE110.valorIcmsRecolher = registroE110.valorAjustesDebito + registroE110.valorSaldoDevedor - registroE110.valorEstornosCreditos;
            registroE110.valorSaldoCredorIcms = 0;
            registroE110.extraApuracao = 0;

            return registroE110;

        }

        #endregion
    }
}
