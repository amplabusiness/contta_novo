using Corporate.Contta.Schedule.Domain.Entities;
using Corporate.Contta.Schedule.Domain.Entities.FullNfeAgg;
using Corporate.Contta.Schedule.Domain.Entities.ImpostoAgg;
using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using Corporate.Contta.Schedule.Domain.Entities.ServicoEntityAgg;
using Corporate.Contta.Schedule.Infra.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Api.Extension
{
    public class IntegrationTbSimplesNfManual
    {
        public async Task CreateTbNfeSaidaManual(NFE nfe, List<Produtos> produtos)
        {
            try
            {
                ProductRepository produtosCfop = new ProductRepository();

                TbDashboardClientes tbDashboardClientes = new TbDashboardClientes();
                var inicioMes = new DateTime(nfe.DhEmi.Value.Year, nfe.DhEmi.Value.Month, 1);
                var fimMes = inicioMes.AddMonths(1).AddDays(-1);
                var companyDto = new CompanyInformation();
                decimal baseCalculo = 0;

                IMongoClient mongoClient = new MongoClient("mongodb://contta:contta123456@192.46.218.34:27017/?authSource=admin&readPreference=primary&ssl=false");
                IMongoDatabase database = mongoClient.GetDatabase("conttadb");

                IMongoCollection<TbDashboardClientes> _collectionTbSimples = database.GetCollection<TbDashboardClientes>("TbSimples");
                IMongoCollection<CompanyInformation> _collectionCompany = database.GetCollection<CompanyInformation>("CompanyInformation");
                IMongoCollection<FaturamentoEmpresa> _collectionTbFaturamento = database.GetCollection<FaturamentoEmpresa>("FaturamentoEmpresa");
                IMongoCollection<ServicoEntity> _collectionTbServico = database.GetCollection<ServicoEntity>("ServicoEntity");
                IMongoCollection<NfeCanceldas> _collectionTbNfeCancelas = database.GetCollection<NfeCanceldas>("NfeCanceldas");
                IMongoCollection<EmpresaDest> _collectionCompanyDest = database.GetCollection<EmpresaDest>("EmpresaDest");
                IMongoCollection<NFE> _collectionNfe = database.GetCollection<NFE>("NFE");
                IMongoCollection<TbCFOP> _collectioCfop = database.GetCollection<TbCFOP>("tbCFOP");


                var listaCfop = produtosCfop.GetAll().Result;

                var listaCfopProdutos = produtos.Select(c => c.Cfop).ToList();

                foreach (var item in produtos)
                {
                    foreach (var cfop in listaCfop)
                    {
                        if (item.Cfop == cfop.CFOP)
                        {
                            baseCalculo = baseCalculo + Convert.ToDecimal(item.VlProduto) - Convert.ToDecimal(item.VlTlDesconto);
                        }
                    }
                }

                var dataCompleta = new FaturamentoMesnalSaida()
                {
                    Ano = nfe.DhEmi.Value.Year,
                    Mes = nfe.DhEmi.Value.Month,
                    Valor = nfe.VtTotalNfe,
                };
                var valorContabel = new ValorContabil()
                {
                    NotaDevolucaoEntrada = 0,
                    NotaDevolucaoSaida = 0,
                    NotaServicoPrestador = 0,
                    ValorEntradaMercadoria = 0,
                    ValorSaidaMercadoria = dataCompleta.Valor,
                    BaseCalculo = baseCalculo
                };

                companyDto = _collectionCompany.Find(c => c.Id == nfe.CompanyInformation).FirstOrDefault();

                var resultInitial = Task.Run(async () =>
                {
                    CriateDataEmissaoNfe(nfe, tbDashboardClientes);

                    CriateFUndationCompany(tbDashboardClientes, companyDto);

                }); resultInitial.Wait();

                var saidaDto = new FaturamentoMesnalSaida();
                saidaDto.Ano = dataCompleta.Ano;
                saidaDto.Mes = dataCompleta.Mes;
                saidaDto.Valor = dataCompleta.Valor;

                var resultTbListaSimplesDto = _collectionTbSimples.Find(c => c.DataEmissaoMensais.Ano == dataCompleta.Ano && c.DataEmissaoMensais.Mes == dataCompleta.Mes).ToList();
                var tbSimplesFeramentoCliDto = resultTbListaSimplesDto.FirstOrDefault(c => c.CompanyInformation == companyDto.Id);
                if (tbSimplesFeramentoCliDto == null)
                {
                    tbDashboardClientes.Id = Guid.NewGuid();
                    tbDashboardClientes.FaturamentoMensaisSaida = saidaDto;
                    tbDashboardClientes.ListaNfe.Add(new NFEEntity() { CodBarra = nfe.CodBarra });
                    tbDashboardClientes.ValorContabil = valorContabel;
                    _collectionTbSimples.InsertOne(tbDashboardClientes);
                }

                if (tbSimplesFeramentoCliDto != null)
                {
                    tbDashboardClientes.FaturamentoMensaisSaida = saidaDto;
                    tbDashboardClientes.ListaNfe.Add(new NFEEntity() { CodBarra = nfe.CodBarra });
                    tbDashboardClientes.ValorContabil = valorContabel;

                    var createTbSimplesSaida = Task.Run(async () =>
                    {
                        await ValidacaoEntardaSaida(tbSimplesFeramentoCliDto.Id, tbDashboardClientes, _collectionNfe, inicioMes, fimMes, companyDto, dataCompleta, _collectionTbSimples, new NFEEntity() { CodBarra = nfe.CodBarra }, new FaturamentoMesnalEntrada()).ConfigureAwait(false);

                    }); createTbSimplesSaida.Wait();
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> CreateTbNfeServicoPrestador(ServicoEntity nfe, Taker taker, double serviceValue)
        {

            TbDashboardClientes tbDashboardClientes = new TbDashboardClientes();
            var inicioMes = new DateTime(nfe.DataEmissao.Year, nfe.DataEmissao.Month, 1);
            var fimMes = inicioMes.AddMonths(1).AddDays(-1);
            var companyDto = new CompanyInformation();

            try
            {
                IMongoClient mongoClient = new MongoClient("mongodb://contta:contta123456@192.46.218.34:27017/?authSource=admin&readPreference=primary&ssl=false");
                IMongoDatabase database = mongoClient.GetDatabase("conttadb");

                IMongoCollection<TbDashboardClientes> _collectionTbSimples = database.GetCollection<TbDashboardClientes>("TbSimples");
                IMongoCollection<CompanyInformation> _collectionTbCompany = database.GetCollection<CompanyInformation>("CompanyInformation");
                IMongoCollection<FaturamentoEmpresa> _collectionTbFaturamento = database.GetCollection<FaturamentoEmpresa>("FaturamentoEmpresa");
                IMongoCollection<ServicoEntity> _collectionTbServico = database.GetCollection<ServicoEntity>("ServicoEntity");
                IMongoCollection<NfeCanceldas> _collectionTbNfeCancelas = database.GetCollection<NfeCanceldas>("NfeCanceldas");
                IMongoCollection<EmpresaDest> _collectionCompanyDest = database.GetCollection<EmpresaDest>("EmpresaDest");
                IMongoCollection<NFE> _collectionNfe = database.GetCollection<NFE>("NFE");

                companyDto = _collectionTbCompany.Find(c => c.Id.Equals(nfe.CompanyInformation)).FirstOrDefault();

                var valorContabel = new ValorContabil();

                var dataCompleta = new FaturamentoMesnalSaida()
                {
                    Ano = nfe.DataEmissao.Year,
                    Mes = nfe.DataEmissao.Month,
                    Valor = serviceValue,
                };

                if (taker != null)
                {
                    var valorTotalFreteIntramunicipal = 0.0;
                    var valorTotalFreteIntermunicipal = 0.0;
                    var valorTotalFrete = 0.0;

                    if (taker.State == companyDto.Address.State && taker.City == companyDto.Address.City)
                    {
                        valorTotalFreteIntramunicipal = dataCompleta.Valor;
                    }

                    if (taker.State == companyDto.Address.State && taker.City != companyDto.Address.City)
                    {
                        valorTotalFreteIntermunicipal = dataCompleta.Valor;
                    }
                    if (taker.State != companyDto.Address.State && taker.City != companyDto.Address.City)
                    {
                        valorTotalFrete = dataCompleta.Valor;
                    }

                    valorContabel = new ValorContabil()
                    {
                        NotaDevolucaoEntrada = 0,
                        NotaDevolucaoSaida = 0,
                        NotaServicoPrestador = dataCompleta.Valor,
                        ValorEntradaMercadoria = 0,
                        ValorSaidaMercadoria = 0,
                        ValorFreteIntermunicipal = valorTotalFreteIntramunicipal,
                        ValorFreteIntramunicipal = valorTotalFreteIntermunicipal,
                        ValorFreteInterestadual = valorTotalFrete

                    };
                }
                else
                {

                    valorContabel = new ValorContabil()
                    {
                        NotaDevolucaoEntrada = 0,
                        NotaDevolucaoSaida = 0,
                        NotaServicoPrestador = dataCompleta.Valor,
                        ValorEntradaMercadoria = 0,
                        ValorSaidaMercadoria = 0,
                        NotaDevolucaoPrestacao = 0,
                        NotaServicoTomador = 0,
                        ValorFreteInterestadual = 0,
                        ValorFreteIntermunicipal = 0,
                        ValorFreteIntramunicipal = 0
                    };

                }


                var resultInitial = Task.Run(async () =>
                {
                    tbDashboardClientes.DataEmissaoMensais.Ano = nfe.DataEmissao.Year;
                    tbDashboardClientes.DataEmissaoMensais.Mes = nfe.DataEmissao.Month;
                    tbDashboardClientes.DataEmissaoMensais.DhEmi = nfe.DataEmissao;
                    CriateFaturamento(tbDashboardClientes, companyDto, _collectionTbFaturamento);
                    CriateFUndationCompany(tbDashboardClientes, companyDto);

                }); resultInitial.Wait();


                var saidaDto = new FaturamentoMesnalSaida();
                saidaDto.Ano = dataCompleta.Ano;
                saidaDto.Mes = dataCompleta.Mes;
                saidaDto.Valor = dataCompleta.Valor;
                var resultTbListaSimplesDto = _collectionTbSimples.Find(c => c.DataEmissaoMensais.Ano == dataCompleta.Ano && c.DataEmissaoMensais.Mes == dataCompleta.Mes).ToList();
                var tbSimplesFeramentoCliDto = resultTbListaSimplesDto.FirstOrDefault(c => c.CompanyInformation == companyDto.Id);
                if (tbSimplesFeramentoCliDto == null)
                {
                    tbDashboardClientes.Id = Guid.NewGuid();
                    tbDashboardClientes.FaturamentoMensaisSaida = saidaDto;
                    tbDashboardClientes.ListaNfe.Add(new NFEEntity() { CodBarra = nfe.Numero.ToString() });
                    tbDashboardClientes.ValorContabil = valorContabel;
                    _collectionTbSimples.InsertOne(tbDashboardClientes);

                }

                if (tbSimplesFeramentoCliDto != null)
                    await ValidacaoTbSimplesServico(tbSimplesFeramentoCliDto.Id, tbDashboardClientes, _collectionNfe, inicioMes, fimMes, companyDto, dataCompleta, _collectionTbSimples, new NFEEntity() { CodBarra = nfe.Numero.ToString() }, new FaturamentoMesnalEntrada()).ConfigureAwait(false);

                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        private static void CriateDataEmissaoNfe(NFE nfe, TbDashboardClientes tbDashboardClientes)
        {
            if (nfe.DhEmi.Value.Year > 0 && nfe.DhEmi.Value.Month > 0)
            {
                tbDashboardClientes.DataEmissaoMensais.Ano = nfe.DhEmi.Value.Year;
                tbDashboardClientes.DataEmissaoMensais.Mes = nfe.DhEmi.Value.Month;
                tbDashboardClientes.DataEmissaoMensais.DhEmi = nfe.DhEmi.Value;
            }
        }

        private static void CriateFaturamento(TbDashboardClientes tbDashboardClientes, CompanyInformation companyDto, IMongoCollection<FaturamentoEmpresa> _collectionTbFaturamento)
        {
            //FaturamentoCliente
            var faturamento12 = _collectionTbFaturamento.Find(c => c.CompanyInformation == companyDto.Id).FirstOrDefault();
            if (faturamento12 != null)
            {
                tbDashboardClientes.SimplesNacional.FaturamentoAnual = faturamento12.Faturamentos.Sum(c => c.ValorFaturamento);
            }
        }

        private static void CriateFUndationCompany(TbDashboardClientes tbDashboardClientes, CompanyInformation companyDto)
        {
            tbDashboardClientes.CompanyInformation = companyDto.Id;
            if (companyDto != null)
                tbDashboardClientes.SimplesNacional.DateFounded = Convert.ToDateTime(companyDto.Founded);
        }

        private static async Task ValidacaoTbSimples(TbDashboardClientes tbDashboardClientes, IMongoCollection<NFE> _collectionNFe, DateTime inicioMes, DateTime fimMes, CompanyInformation companyDto, FaturamentoMesnalSaida faturamentoMesalSaida, IMongoCollection<TbDashboardClientes> _collectionTbSimples, NFEEntity nfeCodBarroDto, FaturamentoMesnalEntrada faturamentoMesnalEntrada)
        {
            try
            {
                List<NFEEntity> listCodBarra = new List<NFEEntity>();
                var lisaEntrada = new FaturamentoMesnalEntrada();
                var lisaSaida = new FaturamentoMesnalSaida();
                double valorEntrada = 0;
                double valorSaida = 0;

                var notasFiscalDto = _collectionTbSimples.Find(c => c.Id == tbDashboardClientes.Id).FirstOrDefault();

                var codBarrasNfe = notasFiscalDto.ListaNfe.Select(c => c.CodBarra).ToList();
                if (!codBarrasNfe.Contains(nfeCodBarroDto.CodBarra) && notasFiscalDto != null)
                {
                    if (nfeCodBarroDto != null)
                    {
                        notasFiscalDto.ListaNfe.Add(nfeCodBarroDto);
                    }

                    if (faturamentoMesalSaida.Valor > 0)
                    {
                        valorSaida = faturamentoMesalSaida.Valor;
                    }
                    else if (faturamentoMesnalEntrada.Valor > 0)
                    {
                        valorEntrada = faturamentoMesnalEntrada.Valor;
                    }

                    var valorContabel = new ValorContabil()
                    {
                        NotaDevolucaoEntrada = 0,
                        NotaDevolucaoSaida = 0,
                        NotaServicoPrestador = 0,
                        ValorEntradaMercadoria = 0,
                        ValorSaidaMercadoria = notasFiscalDto.ValorContabil.ValorSaidaMercadoria + valorSaida,
                        NotaDevolucaoPrestacao = 0,
                        NotaServicoTomador = 0,
                        ValorFreteInterestadual = 0,
                        ValorFreteIntermunicipal = 0,
                        ValorFreteIntramunicipal = 0


                    };

                    if (faturamentoMesnalEntrada.Valor > 0)
                    {
                        var valorOriginal = notasFiscalDto.FaturamentoMensaisEntrada.Valor + faturamentoMesnalEntrada.Valor;

                        lisaEntrada.Ano = notasFiscalDto.DataEmissaoMensais.Ano;
                        lisaEntrada.Mes = notasFiscalDto.DataEmissaoMensais.Mes;
                        lisaEntrada.Valor = valorOriginal;
                    }
                    else if (faturamentoMesalSaida.Valor > 0)
                    {
                        var valorOriginal = notasFiscalDto.FaturamentoMensaisSaida.Valor + faturamentoMesalSaida.Valor;

                        lisaSaida.Ano = notasFiscalDto.DataEmissaoMensais.Ano;
                        lisaSaida.Mes = notasFiscalDto.DataEmissaoMensais.Mes;
                        lisaSaida.Valor = valorOriginal;
                    }

                    if (notasFiscalDto != null)
                    {
                        _ = Task.Run(async () =>
                        {
                            var update = Builders<TbDashboardClientes>.Update.Set(c => c.DataEmissaoMensais.Ano, notasFiscalDto.DataEmissaoMensais.Ano)
                                                                   .Set(c => c.DataEmissaoMensais.Mes, notasFiscalDto.DataEmissaoMensais.Mes)
                                                                   .Set(c => c.DataEmissaoMensais.DhEmi, notasFiscalDto.DataEmissaoMensais.DhEmi)
                                                                   .Set(c => c.CompanyInformation, notasFiscalDto.CompanyInformation)
                                                                   .Set(c => c.ValorContabil, valorContabel)
                                                                   .Set(c => c.SimplesNacional.FaturamentoAnual, tbDashboardClientes.SimplesNacional.FaturamentoAnual)
                                                                   .Set(c => c.SimplesNacional.DateFounded, tbDashboardClientes.SimplesNacional.DateFounded)
                                                                   .Set(c => c.FaturamentoMensaisEntrada, lisaEntrada)
                                                                   .Set(c => c.FaturamentoMensaisSaida, lisaSaida)
                                                                   .Set(c => c.ListaNfe, notasFiscalDto.ListaNfe);

                            var updateResult = await _collectionTbSimples.UpdateOneAsync(c => c.Id == notasFiscalDto.Id, update);

                            if (updateResult.ModifiedCount > 0)
                            {

                            }

                        });
                    }
                }
                else
                {
                    await UpdateNFE(_collectionNFe, nfeCodBarroDto.CodBarra);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static async Task UpdateNFE(IMongoCollection<NFE> _collectionNFE, string codBarra)
        {
            var nfe = _collectionNFE.Find(c => c.CodBarra == codBarra).FirstOrDefault();

            if (nfe != null)
            {
                var update = Builders<NFE>.Update.Set(c => c.Integrada, true);
                var updateResult = await _collectionNFE.UpdateOneAsync(c => c.Id == nfe.Id, update);

                if (updateResult.ModifiedCount > 0)
                {

                }
            }
        }

        private static async Task ValidacaoTbSimplesServico(Guid? tbDashbordId, TbDashboardClientes tbDashboardClientes, IMongoCollection<NFE> _collectionNFe, DateTime inicioMes, DateTime fimMes, CompanyInformation companyDto, FaturamentoMesnalSaida faturamentoMesalSaida, IMongoCollection<TbDashboardClientes> _collectionTbSimples, NFEEntity nfeCodBarroDto, FaturamentoMesnalEntrada faturamentoMesnalEntrada)
        {
            try
            {
                if (tbDashbordId != null)
                {

                    List<NFEEntity> listCodBarra = new List<NFEEntity>();
                    var lisaEntrada = new FaturamentoMesnalEntrada();
                    var lisaSaida = new FaturamentoMesnalSaida();
                    var notasFiscalDto = new TbDashboardClientes();
                    double valorEntrada = 0;
                    double valorSaida = 0;

                    if (tbDashbordId.HasValue)
                    {
                        notasFiscalDto = _collectionTbSimples.Find(c => c.Id.Equals(tbDashbordId)).FirstOrDefault();
                    }
                    else
                    {
                        notasFiscalDto = _collectionTbSimples.Find(c => c.CompanyInformation == companyDto.Id &&
                              (c.DataEmissaoMensais.DhEmi >= inicioMes && c.DataEmissaoMensais.DhEmi <= fimMes)).FirstOrDefault();
                    }

                    var codBarrasNfe = notasFiscalDto.ListaNfe.Select(c => c.CodBarra).ToList();
                    if (!codBarrasNfe.Contains(nfeCodBarroDto.CodBarra) && notasFiscalDto != null)
                    {
                        if (nfeCodBarroDto != null)
                        {
                            notasFiscalDto.ListaNfe.Add(nfeCodBarroDto);
                        }

                        if (faturamentoMesalSaida.Valor > 0)
                        {
                            valorSaida = faturamentoMesalSaida.Valor;
                        }
                        else if (faturamentoMesnalEntrada.Valor > 0)
                        {
                            valorEntrada = faturamentoMesnalEntrada.Valor;
                        }

                        var valorContabel = new ValorContabil()
                        {
                            NotaServicoPrestador = notasFiscalDto.ValorContabil.NotaServicoPrestador + valorSaida,
                            NotaServicoTomador = notasFiscalDto.ValorContabil.NotaServicoTomador + valorEntrada,
                            NotaDevolucaoPrestacao = notasFiscalDto.ValorContabil.NotaDevolucaoPrestacao,

                            BaseCalculo = notasFiscalDto.ValorContabil.BaseCalculo + Convert.ToDecimal(valorSaida),
                            NotaDevolucaoEntrada = notasFiscalDto.ValorContabil.NotaDevolucaoEntrada,
                            NotaDevolucaoSaida = notasFiscalDto.ValorContabil.NotaDevolucaoSaida,
                            ValorEntradaMercadoria = notasFiscalDto.ValorContabil.ValorEntradaMercadoria,
                            ValorFreteInterestadual = notasFiscalDto.ValorContabil.ValorFreteInterestadual,
                            ValorFreteIntermunicipal = notasFiscalDto.ValorContabil.ValorFreteIntermunicipal,
                            ValorFreteIntramunicipal = notasFiscalDto.ValorContabil.ValorFreteIntramunicipal,
                            ValorSaidaMercadoria = notasFiscalDto.ValorContabil.ValorSaidaMercadoria          

                        };

                        if (faturamentoMesnalEntrada.Valor > 0)
                        {
                            var valorOriginal = notasFiscalDto.FaturamentoMensaisEntrada.Valor + faturamentoMesnalEntrada.Valor;

                            lisaEntrada.Ano = notasFiscalDto.DataEmissaoMensais.Ano;
                            lisaEntrada.Mes = notasFiscalDto.DataEmissaoMensais.Mes;
                            lisaEntrada.Valor = valorOriginal;
                        }
                        else if (faturamentoMesalSaida.Valor > 0)
                        {
                            var valorOriginal = notasFiscalDto.FaturamentoMensaisSaida.Valor + faturamentoMesalSaida.Valor;

                            lisaSaida.Ano = notasFiscalDto.DataEmissaoMensais.Ano;
                            lisaSaida.Mes = notasFiscalDto.DataEmissaoMensais.Mes;
                            lisaSaida.Valor = valorOriginal;
                        }

                        if (notasFiscalDto != null)
                        {
                            _ = Task.Run(async () =>
                            {
                                var update = Builders<TbDashboardClientes>.Update.Set(c => c.DataEmissaoMensais.Ano, notasFiscalDto.DataEmissaoMensais.Ano)
                                                                       .Set(c => c.DataEmissaoMensais.Mes, notasFiscalDto.DataEmissaoMensais.Mes)
                                                                       .Set(c => c.DataEmissaoMensais.DhEmi, notasFiscalDto.DataEmissaoMensais.DhEmi)
                                                                       .Set(c => c.CompanyInformation, notasFiscalDto.CompanyInformation)
                                                                       .Set(c => c.ValorContabil, valorContabel)
                                                                       .Set(c => c.SimplesNacional.FaturamentoAnual, tbDashboardClientes.SimplesNacional.FaturamentoAnual)
                                                                       .Set(c => c.SimplesNacional.DateFounded, tbDashboardClientes.SimplesNacional.DateFounded)
                                                                       .Set(c => c.FaturamentoMensaisEntrada, lisaEntrada)
                                                                       .Set(c => c.FaturamentoMensaisSaida, lisaSaida)
                                                                       .Set(c => c.ListaNfe, notasFiscalDto.ListaNfe);

                                var updateResult = await _collectionTbSimples.UpdateOneAsync(c => c.Id == notasFiscalDto.Id, update);

                                if (updateResult.ModifiedCount > 0)
                                {

                                }
                                else
                                {

                                }
                            });
                        }
                    }

                }
                else
                {
                    await UpdateNFE(_collectionNFe, nfeCodBarroDto.CodBarra);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private static async Task ValidacaoEntardaSaida(Guid? tbDashboardId, TbDashboardClientes tbDashboardClientes, IMongoCollection<NFE> _collectionNFe, DateTime inicioMes, DateTime fimMes, CompanyInformation companyDto, FaturamentoMesnalSaida faturamentoMesalSaida, IMongoCollection<TbDashboardClientes> _collectionTbSimples, NFEEntity nfeCodBarroDto, FaturamentoMesnalEntrada faturamentoMesnalEntrada)
        {
            try
            {
                if (tbDashboardId.Value != null)
                {

                    List<NFEEntity> listCodBarra = new List<NFEEntity>();
                    var lisaEntrada = new FaturamentoMesnalEntrada();
                    var lisaSaida = new FaturamentoMesnalSaida();
                    var notasFiscalDto = new TbDashboardClientes();
                    double valorEntrada = 0;
                    double valorSaida = 0;
                    double ValorFreteInterestadual = 0;
                    double ValorFreteIntramunicipal = 0;
                    double ValorFreteIntermunicipal = 0;
                    decimal baseCalculoSimples = 0;

                    if (tbDashboardId.HasValue)
                    {
                        notasFiscalDto = _collectionTbSimples.Find(c => c.Id == tbDashboardId.Value).FirstOrDefault();
                    }
                    else
                    {
                        notasFiscalDto = _collectionTbSimples.Find(c => c.CompanyInformation == companyDto.Id &&
                              (c.DataEmissaoMensais.DhEmi >= inicioMes && c.DataEmissaoMensais.DhEmi <= fimMes)).FirstOrDefault();
                    }


                    var codBarrasNfe = notasFiscalDto.ListaNfe.Select(c => c.CodBarra).ToList();
                    if (!codBarrasNfe.Contains(nfeCodBarroDto.CodBarra) && notasFiscalDto != null)
                    {
                        if (nfeCodBarroDto != null)
                        {
                            notasFiscalDto.ListaNfe.Add(nfeCodBarroDto);
                        }

                        if (faturamentoMesalSaida.Valor > 0)
                        {
                            valorSaida = faturamentoMesalSaida.Valor;
                            baseCalculoSimples = tbDashboardClientes.ValorContabil.BaseCalculo;
                        }
                        else if (faturamentoMesnalEntrada.Valor > 0)
                        {
                            valorEntrada = faturamentoMesnalEntrada.Valor;
                        }

                        if (tbDashboardClientes.ValorContabil.ValorFreteInterestadual > 0 || tbDashboardClientes.ValorContabil.ValorFreteIntramunicipal > 0 || tbDashboardClientes.ValorContabil.ValorFreteIntermunicipal > 0)
                        {
                            ValorFreteInterestadual = notasFiscalDto.ValorContabil.ValorFreteInterestadual + tbDashboardClientes.ValorContabil.ValorFreteInterestadual;
                            ValorFreteIntramunicipal = notasFiscalDto.ValorContabil.ValorFreteIntramunicipal + tbDashboardClientes.ValorContabil.ValorFreteIntramunicipal;
                            ValorFreteIntermunicipal = notasFiscalDto.ValorContabil.ValorFreteIntermunicipal + tbDashboardClientes.ValorContabil.ValorFreteIntermunicipal;

                        }
                        else
                        {
                            ValorFreteInterestadual = 0;
                            ValorFreteIntramunicipal = 0;
                            ValorFreteIntermunicipal = 0;
                        }

                        var valorContabel = new ValorContabil()
                        {
                            ValorFreteInterestadual = notasFiscalDto.ValorContabil.ValorFreteInterestadual + ValorFreteInterestadual,
                            ValorFreteIntermunicipal = notasFiscalDto.ValorContabil.ValorFreteIntermunicipal + ValorFreteIntramunicipal,
                            ValorFreteIntramunicipal = notasFiscalDto.ValorContabil.ValorFreteIntramunicipal + ValorFreteIntermunicipal,                            
                            ValorSaidaMercadoria = notasFiscalDto.ValorContabil.ValorSaidaMercadoria + valorSaida,
                            BaseCalculo = notasFiscalDto.ValorContabil.BaseCalculo + baseCalculoSimples,
                            NotaServicoPrestador = notasFiscalDto.ValorContabil.NotaServicoPrestador,
                            NotaServicoTomador = notasFiscalDto.ValorContabil.NotaServicoTomador,
                            NotaDevolucaoPrestacao = notasFiscalDto.ValorContabil.NotaDevolucaoPrestacao,                          
                            NotaDevolucaoEntrada = notasFiscalDto.ValorContabil.NotaDevolucaoEntrada,
                            NotaDevolucaoSaida = notasFiscalDto.ValorContabil.NotaDevolucaoSaida,
                            ValorEntradaMercadoria = notasFiscalDto.ValorContabil.ValorEntradaMercadoria  

                        };

                        if (faturamentoMesnalEntrada.Valor > 0)
                        {
                            var valorOriginal = notasFiscalDto.FaturamentoMensaisEntrada.Valor + faturamentoMesnalEntrada.Valor;

                            lisaEntrada.Ano = notasFiscalDto.DataEmissaoMensais.Ano;
                            lisaEntrada.Mes = notasFiscalDto.DataEmissaoMensais.Mes;
                            lisaEntrada.Valor = valorOriginal;
                        }
                        else if (faturamentoMesalSaida.Valor > 0)
                        {
                            var valorOriginal = notasFiscalDto.FaturamentoMensaisSaida.Valor + faturamentoMesalSaida.Valor;

                            lisaSaida.Ano = notasFiscalDto.DataEmissaoMensais.Ano;
                            lisaSaida.Mes = notasFiscalDto.DataEmissaoMensais.Mes;
                            lisaSaida.Valor = valorOriginal;
                        }


                        if (notasFiscalDto != null)
                        {
                            _ = Task.Run(async () =>
                            {
                                var update = Builders<TbDashboardClientes>.Update.Set(c => c.DataEmissaoMensais.Ano, notasFiscalDto.DataEmissaoMensais.Ano)
                                                                       .Set(c => c.DataEmissaoMensais.Mes, notasFiscalDto.DataEmissaoMensais.Mes)
                                                                       .Set(c => c.DataEmissaoMensais.DhEmi, notasFiscalDto.DataEmissaoMensais.DhEmi)
                                                                       .Set(c => c.CompanyInformation, notasFiscalDto.CompanyInformation)
                                                                       .Set(c => c.ValorContabil, valorContabel)
                                                                       .Set(c => c.SimplesNacional.FaturamentoAnual, tbDashboardClientes.SimplesNacional.FaturamentoAnual)
                                                                       .Set(c => c.SimplesNacional.DateFounded, tbDashboardClientes.SimplesNacional.DateFounded)
                                                                       .Set(c => c.FaturamentoMensaisEntrada, lisaEntrada)
                                                                       .Set(c => c.FaturamentoMensaisSaida, lisaSaida)
                                                                       .Set(c => c.ListaNfe, notasFiscalDto.ListaNfe);

                                var updateResult = await _collectionTbSimples.UpdateOneAsync(c => c.Id == notasFiscalDto.Id, update);

                                if (updateResult.ModifiedCount > 0)
                                {

                                }
                                else
                                {

                                }
                            });
                        }
                    }

                }
                else
                {
                    await UpdateNFE(_collectionNFe, nfeCodBarroDto.CodBarra);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
