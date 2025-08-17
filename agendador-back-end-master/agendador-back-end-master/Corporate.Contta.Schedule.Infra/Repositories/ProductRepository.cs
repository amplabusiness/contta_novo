using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities;
using Corporate.Contta.Schedule.Domain.Entities.EstoqueAgg;
using Corporate.Contta.Schedule.Domain.Entities.Imporsto;
using Corporate.Contta.Schedule.Domain.Entities.ImpostoAgg;
using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using Corporate.Contta.Schedule.Domain.Entities.Product;
using Corporate.Contta.Schedule.Infra.Repositories.Base;
using MongoDB.Driver;
using Sanatana.MongoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Infra.Repositories
{
    public class ProductRepository : BaseRepository<Produtos>, IProductRepository
    {
        private static MongoDBContext<Produtos> _dbContext = new MongoDBContext<Produtos>();
        private static MongoDBContext<Estoque> _dbContextEstoque = new MongoDBContext<Estoque>();
        private static MongoDBContext<ProdutosFornecedor> _dbContextFornec = new MongoDBContext<ProdutosFornecedor>();
        private static MongoDBContext<CompanyInformation> _dbContextCompany = new MongoDBContext<CompanyInformation>();
        private static MongoDBContext<NFE> _dbContextNFE = new MongoDBContext<NFE>();
        private static MongoDBContext<ProdutosFornecedor> _dbContextProdForncedor = new MongoDBContext<ProdutosFornecedor>();
        private static MongoDBContext<TbCFOP> _dbContextCfop = new MongoDBContext<TbCFOP>();

        private readonly IMongoDatabase _database = MongoClient();     
      
        private MongoDbConnectionSettings _settings;


        public static IMongoDatabase MongoClient()
        {

            IMongoClient mongoClient = new MongoClient("mongodb://contta:contta123456@192.46.218.34:27017/?authSource=admin&readPreference=primary&ssl=false");
            IMongoDatabase database = mongoClient.GetDatabase("conttadb");

            return database;
        }


        public ProductRepository() : base(_dbContext)
        {
        }

        public async Task<List<TbCFOP>> GetAll()
        {
            var result = _dbContextCfop.GetColection.Find(c => c.CFOP > 0).ToList();

            return result;
        }

        public async Task<IEnumerable<Produtos>> GetAll(Guid companyId)
        {
            var result = await _dbContext.GetColection.FindAsync(c => c.CompanyInformation.Equals(companyId) && !c.Modificado).ConfigureAwait(false);

            var protudos = result.ToList();
            return protudos;
        }

        #region INCM/ST Ncm Mono
        public async Task<IEnumerable<Produtos>> GetAllIcmsSt(Guid companyId, DateTime dhEmi)
        {
            var inicioMes = new DateTime(dhEmi.Year, dhEmi.Month, 1);
            var fimMes = inicioMes.AddMonths(1).AddDays(-1);

            var result = await _dbContext.GetColection.FindAsync(c => c.CompanyInformation.Equals(companyId) && c.Modificado == false && c.DhEmt >= inicioMes && c.DhEmt <= fimMes && c.ProdutoBaySimples).ConfigureAwait(false);
            var protudos = result.ToList().Take(100);
            return protudos;
        }

        public async Task<IEnumerable<Produtos>> GetAllIcmsStAlterado(Guid companyId, DateTime dhEmi)
        {
            var inicioMes = new DateTime(dhEmi.Year, dhEmi.Month, 1);
            var fimMes = inicioMes.AddMonths(1).AddDays(-1);
            var result = await _dbContext.GetColection.FindAsync(c => c.CompanyInformation.Equals(companyId) && c.Modificado == true && c.DhEmt >= inicioMes && c.DhEmt <= fimMes && c.ProdutoBaySimples).ConfigureAwait(false);
            var protudos = result.ToList();
            return protudos;
        }

        public async Task<IEnumerable<Produtos>> GetAllNcmMono(Guid companyId, DateTime dhEmi)
        {
            var inicioMes = new DateTime(dhEmi.Year, dhEmi.Month, 1);
            var fimMes = inicioMes.AddMonths(1).AddDays(-1);
            var result = await _dbContext.GetColection.FindAsync(c => c.CompanyInformation.Equals(companyId) && c.Modificado == false && c.DhEmt >= inicioMes && c.DhEmt <= fimMes && c.ProdutoBaySimples).ConfigureAwait(false);
            var protudos = result.ToList();
            return protudos;

        }

        public async Task<IEnumerable<Produtos>> GetAllNcmMonoAlterado(Guid companyId, DateTime dhEmi)
        {
            var inicioMes = new DateTime(dhEmi.Year, dhEmi.Month, 1);
            var fimMes = inicioMes.AddMonths(1).AddDays(-1);
            var result = await _dbContext.GetColection.FindAsync(c => c.CompanyInformation.Equals(companyId) && c.Modificado == true && c.DhEmt >= inicioMes && c.DhEmt <= fimMes && c.ProdutoBaySimples).ConfigureAwait(false);
            var protudos = result.ToList();
            return protudos;

        }
        #endregion

        public async Task<IEnumerable<Produtos>> GetAllAntecipaTri(Guid companyId)
        {
            var result = await _dbContext.GetColection.FindAsync(c => c.CompanyInformation.Equals(companyId) && c.NcmMono && c.Modificado == false).ConfigureAwait(false);
            var protudos = result.ToList();
            return protudos;

        }

        public async Task<IEnumerable<Produtos>> GetAllImpostosProd(Guid? companyId, DateTime dhEmissao)
        {
            var inicioMes = new DateTime(dhEmissao.Year, dhEmissao.Month, 1);
            var fimMes = inicioMes.AddMonths(1).AddDays(-1);

            var result = await _dbContext.GetColection.FindAsync(c => c.CompanyInformation.Equals(companyId) && c.Modificado && c.DhEmt >= inicioMes && c.DhEmt <= fimMes &&
                                                                      c.NcmMono || c.IcmsSt || c.Beneficios || c.Isento || c.Imune || c.ExigSuspensa || c.LancamentoOficio || c.IsencaoReducao &&
                                                                      c.IsencaoReducaoCestaBasica &&
                                                                      c.ExigSuspensaMono || c.AntEncTributacao || c.LancamentoOficioMono || c.SubTributariaMono
                                                                      ).ConfigureAwait(false);
            var protudos = result.ToList();
            return protudos;

        }
        public async Task<IEnumerable<Produtos>> GetAllImpostosProdIcms(Guid? companyId, DateTime dhEmissao)
        {
            var inicioMes = new DateTime(dhEmissao.Year, dhEmissao.Month, 1);
            var fimMes = inicioMes.AddMonths(1).AddDays(-1);

            var result = await _dbContext.GetColection.FindAsync(c => c.CompanyInformation.Equals(companyId) && c.Modificado && c.DhEmt >= inicioMes && c.DhEmt <= fimMes &&
                                                                       c.IcmsSt).ConfigureAwait(false);
            var protudos = result.ToList();
            return protudos;

        }
        public async Task<IEnumerable<Produtos>> GetAllImpostosProdPisConfins(Guid? companyId, DateTime dhEmissao)
        {
            var inicioMes = new DateTime(dhEmissao.Year, dhEmissao.Month, 1);
            var fimMes = inicioMes.AddMonths(1).AddDays(-1);

            var result = await _dbContext.GetColection.FindAsync(c => c.CompanyInformation.Equals(companyId) && c.Modificado && c.DhEmt >= inicioMes && c.DhEmt <= fimMes &&
                                                                       c.NcmMono).ConfigureAwait(false);
            var protudos = result.ToList();
            return protudos;

        }

        public async Task<List<Estoque>> GetProd(Guid companyId, string codPrd, string desCod)
        {
            var estqoue = new List<Estoque>();
            if (codPrd != null && codPrd != "")
            {
                var resultado = _dbContext.GetColection.Find(c => c.CompanyInformation.Equals(companyId) && c.CodProduto.Equals(codPrd)).ToList();

                if(resultado.Count > 0)
                {
                    var result = resultado.Where(c => c.CodProduto == codPrd).FirstOrDefault();

                    estqoue.Add(new Estoque()
                    {
                        Id = result.Id,
                        Descricao = result.DescProduto,
                        CodProd = result.CodProduto
                    });


                    return estqoue;
                }
                else
                {
                    return estqoue;
                }
             
            }
            else if (desCod != null && desCod != "")
            {
                var resultado = _dbContext.GetColection.Find(c => c.CompanyInformation.Equals(companyId) && c.DescProduto.Contains(desCod)).ToList();

                if(resultado.Count >0)
                {
                    foreach (var item in resultado)
                    {
                        var dados = estqoue.Select(c => c.Id.Equals(item.Id)).FirstOrDefault();
                        if (!dados)
                        {
                            estqoue.Add(new Estoque()
                            {
                                Id = item.Id,
                                Descricao = item.DescProduto,
                                CodProd = item.CodProduto
                            });
                        }

                    }

                    var result = estqoue;

                    return result;
                }
                else
                {
                    return estqoue;
                }                
            }

            return estqoue;
        }

        public async Task<IEnumerable<Produtos>> GetAllCfep(Guid companyId)
        {
            var result = await _dbContext.GetColection.FindAsync(c => c.EmpresaEmitId.Equals(companyId)).ConfigureAwait(false);

            var protudos = result.ToList();

            GetCfop cfop = new GetCfop();

            var listCfop = cfop.GetListaCfopVenda();

            var listaCfopDiferentes = protudos.Where(i => !listCfop.Contains(i.Cfop)).ToList();

            var produtosVenda = (from w1 in protudos where listCfop.Any(w2 => w1.Cfop != w2) select w1).ToList();

            return listaCfopDiferentes;
        }

        public async Task<List<Produtos>> GetAllByNfe(Guid nfeId)
        {
            try
            {
                var result = await _dbContext.GetColection.FindAsync(c => c.NfeId.Equals(nfeId)).ConfigureAwait(false);
                return result.ToList();
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<List<ProdutosFornecedor>> GetAllByNfeFornecedor(Guid nfeId)
        {
            var result = await _dbContextProdForncedor.GetColection.FindAsync(c => c.NfeId.Equals(nfeId)).ConfigureAwait(false);
            return result.ToList();
        }

        public async Task<Produtos> GetById(Guid? id)
        {
            var result = await _dbContext.GetColection.FindAsync(c => c.Id.Equals(id)).ConfigureAwait(false);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<bool> Update(List<Produtos> ListProdutos, Guid empresaId)
        {
            foreach (var produtos in ListProdutos)
            {
                var listaProdutosCod = _dbContext.GetColection.Find(c => c.CodProduto.Equals(produtos.CodProduto) && c.Modificado == false).ToList();

                var listaIdProduct = listaProdutosCod.Select(c => c.Id).ToList();

                produtos.Modificado = true;
                var update = Builders<Produtos>.Update.Set(c => c.DescProduto, produtos.DescProduto)
                                                     .Set(c => c.Lei, produtos.Lei)
                                                     .Set(c => c.Auditado, produtos.Auditado)
                                                     .Set(c => c.CodProduto, produtos.CodProduto)
                                                     .Set(c => c.NcmProd, produtos.NcmProd)
                                                     .Set(c => c.UnidMedida, produtos.UnidMedida)
                                                     .Set(c => c.Quantidade, produtos.Quantidade)
                                                     .Set(c => c.VlUnitario, produtos.VlUnitario)
                                                     .Set(c => c.UniMedTributado, produtos.UniMedTributado)
                                                     .Set(c => c.QtdTributaria, produtos.QtdTributaria)
                                                     .Set(c => c.VlUnitTributado, produtos.VlUnitTributado)
                                                     .Set(c => c.VlProduto, produtos.VlProduto)
                                                     .Set(c => c.VlTlFrete, produtos.VlTlFrete)
                                                     .Set(c => c.VlTlSeguro, produtos.VlTlSeguro)
                                                     .Set(c => c.VlTlDesconto, produtos.VlTlDesconto)
                                                     .Set(c => c.OutrasDespesas, produtos.OutrasDespesas)
                                                     .Set(c => c.Cfop, produtos.Cfop)
                                                     .Set(c => c.Ean, produtos.Ean)
                                                     .Set(c => c.PedCompra, produtos.PedCompra)
                                                     .Set(c => c.NItemPedido, produtos.NItemPedido)
                                                     .Set(c => c.Origem, produtos.Origem)
                                                     .Set(c => c.Tributos, produtos.Tributos)
                                                     .Set(c => c.VlAproxTributos, produtos.VlAproxTributos)
                                                     .Set(c => c.NfeId, produtos.NfeId)
                                                     .Set(c => c.EmpresaEmitId, produtos.EmpresaEmitId)
                                                     .Set(c => c.Csons, produtos.Csons)
                                                     .Set(c => c.CompanyInformation, produtos.CompanyInformation)
                                                     .Set(c => c.IntegradaEmpresa, produtos.IntegradaEmpresa)
                                                     .Set(c => c.NcmMono, produtos.NcmMono)
                                                     .Set(c => c.IcmsSt, produtos.IcmsSt)
                                                     .Set(c => c.Beneficios, produtos.Beneficios)
                                                     .Set(c => c.Modificado, produtos.Modificado)
                                                     .Set(c => c.Validado, produtos.Validado)
                                                     .Set(c => c.RoboMode, produtos.RoboMode);

                foreach (var item in listaIdProduct)
                {
                    var updateResult = await _dbContext.GetColection.UpdateOneAsync(c => c.Id.Equals(item), update);

                    if (updateResult.ModifiedCount > 0)
                    {
                        //Criar Log

                    };
                }
            }

            return true;
        }

        public async Task<bool> UpdateFornec(List<ProdutosFornecedor> ListProdutos, Guid empresaId)
        {
            foreach (var produtos in ListProdutos)
            {
                var listaProdutosCod = _dbContextFornec.GetColection.Find(c => c.CodProduto.Equals(produtos.CodProduto) && c.Modificado == false).ToList();

                var listaIdProduct = listaProdutosCod.Select(c => c.Id).ToList();

                produtos.Modificado = true;
                var update = Builders<ProdutosFornecedor>.Update.Set(c => c.DescProduto, produtos.DescProduto)
                                                     .Set(c => c.NcmProd, produtos.NcmProd)
                                                     .Set(c => c.Cfop, produtos.Cfop);
                                                     

                foreach (var item in listaIdProduct)
                {
                    var updateResult = await _dbContextFornec.GetColection.UpdateOneAsync(c => c.Id.Equals(item), update);

                    if (updateResult.ModifiedCount > 0)
                    {
                        //Criar Log

                    };
                }
            }

            return true;
        }

        public async Task<Reclassificacao> GetById(Guid empresaId, Guid nfeId)
        {
            var company = _dbContextCompany.GetColection.Find(e => e.Id.Equals(empresaId)).FirstOrDefault();
            var result = _dbContext.GetColection.Find(c => c.CompanyInformation.Equals(empresaId) && c.NfeId.Equals(nfeId)).ToList();

            //ToDo: Modificar modelo Nfe.
            var reclassificacaoDto = new Reclassificacao
            {
                RazaoSocial = company.Name,
                Fantasia = company.Alias,
                Cnpj = company.Cnpj,
                ModeloNfe = "55",
                ListaProdutos = result
            };

            return reclassificacaoDto;
        }

        public async Task<IEnumerable<ProdutosFornecedor>> GetAllProdForn(Guid companyId)
        {
            try
            {
                var result = _dbContextProdForncedor.GetColection.Find(c => c.CompanyInformation.Equals(companyId) && c.Ean == "SEM GTIN").ToList();
                var protudos = result.ToList();
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<bool> UpdateDepara(string codProFornecedor, string codProCliente, Guid empresaId, string marca)
        {
            var produtos = _dbContext.GetColection.Find(c => c.CodProduto == codProCliente && c.CompanyInformation == empresaId).FirstOrDefault();
            var produtosFornc = _dbContextFornec.GetColection.Find(c => c.CodProduto == codProFornecedor && c.CompanyInformation == empresaId).FirstOrDefault();

            if (produtos != null)
            {
                produtos.Ean = codProFornecedor;

                var update = Builders<Produtos>.Update.Set(c => c.DescProduto, produtos.DescProduto)
                                                     .Set(c => c.Lei, produtos.Lei)
                                                     .Set(c => c.Auditado, produtos.Auditado)
                                                     .Set(c => c.CodProduto, produtos.CodProduto)
                                                     .Set(c => c.NcmProd, produtos.NcmProd)
                                                     .Set(c => c.UnidMedida, produtos.UnidMedida)
                                                     .Set(c => c.Quantidade, produtos.Quantidade)
                                                     .Set(c => c.VlUnitario, produtos.VlUnitario)
                                                     .Set(c => c.UniMedTributado, produtos.UniMedTributado)
                                                     .Set(c => c.QtdTributaria, produtos.QtdTributaria)
                                                     .Set(c => c.VlUnitTributado, produtos.VlUnitTributado)
                                                     .Set(c => c.VlProduto, produtos.VlProduto)
                                                     .Set(c => c.VlTlFrete, produtos.VlTlFrete)
                                                     .Set(c => c.VlTlSeguro, produtos.VlTlSeguro)
                                                     .Set(c => c.VlTlDesconto, produtos.VlTlDesconto)
                                                     .Set(c => c.OutrasDespesas, produtos.OutrasDespesas)
                                                     .Set(c => c.Cfop, produtos.Cfop)
                                                     .Set(c => c.Ean, produtos.Ean)
                                                     .Set(c => c.PedCompra, produtos.PedCompra)
                                                     .Set(c => c.NItemPedido, produtos.NItemPedido)
                                                     .Set(c => c.Origem, produtos.Origem)
                                                     .Set(c => c.Tributos, produtos.Tributos)
                                                     .Set(c => c.VlAproxTributos, produtos.VlAproxTributos)
                                                     .Set(c => c.NfeId, produtos.NfeId)
                                                     .Set(c => c.EmpresaEmitId, produtos.EmpresaEmitId)
                                                     .Set(c => c.Csons, produtos.Csons)
                                                     .Set(c => c.CompanyInformation, produtos.CompanyInformation)
                                                     .Set(c => c.IntegradaEmpresa, produtos.IntegradaEmpresa)
                                                     .Set(c => c.NcmMono, produtos.NcmMono)
                                                     .Set(c => c.IcmsSt, produtos.IcmsSt)
                                                     .Set(c => c.Beneficios, produtos.Beneficios)
                                                     .Set(c => c.Modificado, produtos.Modificado)
                                                     .Set(c => c.Validado, produtos.Validado)
                                                     .Set(c => c.RoboMode, produtos.RoboMode);


                var updateResult = await _dbContext.GetColection.UpdateOneAsync(c => c.Id.Equals(produtos.Id), update);

                if (updateResult.ModifiedCount > 0)
                {
                    await UpdateProdFornc(codProCliente, produtos, produtosFornc);
                    return true;

                };

                await UpdateProdFornc(codProCliente, produtos, produtosFornc);
                return true;
            }

            return false;
        }

        private static async Task UpdateProdFornc(string codProCliente, Produtos produtos, ProdutosFornecedor produtosFornc)
        {
            if (produtosFornc != null)
            {

                produtosFornc.Ean = codProCliente;
                var updateFornec = Builders<ProdutosFornecedor>.Update.Set(c => c.DescProduto, produtosFornc.DescProduto)
                                                     .Set(c => c.Auditado, produtosFornc.Auditado)
                                                     .Set(c => c.CodProduto, produtosFornc.CodProduto)
                                                     .Set(c => c.NcmProd, produtosFornc.NcmProd)
                                                     .Set(c => c.UnidMedida, produtosFornc.UnidMedida)
                                                     .Set(c => c.Quantidade, produtosFornc.Quantidade)
                                                     .Set(c => c.VlUnitario, produtosFornc.VlUnitario)
                                                     .Set(c => c.UniMedTributado, produtosFornc.UniMedTributado)
                                                     .Set(c => c.QtdTributaria, produtosFornc.QtdTributaria)
                                                     .Set(c => c.VlUnitTributado, produtosFornc.VlUnitTributado)
                                                     .Set(c => c.VlProduto, produtosFornc.VlProduto)
                                                     .Set(c => c.VlTlFrete, produtosFornc.VlTlFrete)
                                                     .Set(c => c.VlTlSeguro, produtosFornc.VlTlSeguro)
                                                     .Set(c => c.VlTlDesconto, produtosFornc.VlTlDesconto)
                                                     .Set(c => c.OutrasDespesas, produtosFornc.OutrasDespesas)
                                                     .Set(c => c.Cfop, produtosFornc.Cfop)
                                                     .Set(c => c.Ean, produtosFornc.Ean)
                                                     .Set(c => c.PedCompra, produtosFornc.PedCompra)
                                                     .Set(c => c.NItemPedido, produtosFornc.NItemPedido)
                                                     .Set(c => c.Origem, produtosFornc.Origem)
                                                     .Set(c => c.Tributos, produtosFornc.Tributos)
                                                     .Set(c => c.VlAproxTributos, produtosFornc.VlAproxTributos)
                                                     .Set(c => c.NfeId, produtosFornc.NfeId)
                                                     .Set(c => c.EmpresaEmitId, produtosFornc.EmpresaEmitId)
                                                     .Set(c => c.Csons, produtosFornc.Csons)
                                                     .Set(c => c.CompanyInformation, produtosFornc.CompanyInformation)
                                                     .Set(c => c.IntegradaEmpresa, produtosFornc.IntegradaEmpresa)
                                                     .Set(c => c.Modificado, produtosFornc.Modificado)
                                                     .Set(c => c.Validado, produtosFornc.Validado)
                                                     .Set(c => c.RoboMode, produtosFornc.RoboMode);

                var updateResult1 = await _dbContextFornec.GetColection.UpdateOneAsync(c => c.Id.Equals(produtosFornc.Id), updateFornec);


                if (updateResult1.ModifiedCount > 0)
                {

                }
            }
        }

        public async Task<IEnumerable<Produtos>> GetAllBeneficio(Guid companyId)
        {
            var result = await _dbContext.GetColection.FindAsync(c => c.CompanyInformation.Equals(companyId) && c.Beneficios && c.Modificado == false).ConfigureAwait(false);
            var protudos = result.ToList();
            return protudos;
        }


        public async Task<IEnumerable<Produtos>> GetAllImune(Guid companyId)
        {
            var result = await _dbContext.GetColection.FindAsync(c => c.CompanyInformation.Equals(companyId) && c.Imune && c.Modificado == false).ConfigureAwait(false);
            var protudos = result.ToList();
            return protudos;
        }

        public async Task<IEnumerable<Produtos>> GetAllInsento(Guid companyId)
        {
            var result = await _dbContext.GetColection.FindAsync(c => c.CompanyInformation.Equals(companyId) && c.Isento && c.Modificado == false).ConfigureAwait(false);
            var protudos = result.ToList();
            return protudos;
        }

        public async Task<IEnumerable<Produtos>> GetAllInsencaoCesta(Guid companyId)
        {
            var result = await _dbContext.GetColection.FindAsync(c => c.CompanyInformation.Equals(companyId) && c.IsencaoReducaoCestaBasica && c.Modificado == false).ConfigureAwait(false);
            var protudos = result.ToList();
            return protudos;
        }

        public async Task<IEnumerable<Produtos>> GetAllInsencao(Guid companyId)
        {
            var result = await _dbContext.GetColection.FindAsync(c => c.CompanyInformation.Equals(companyId) && c.IsencaoReducao && c.Modificado == false).ConfigureAwait(false);
            var protudos = result.ToList();
            return protudos;
        }
        public async Task<List<FileProduct>> GetFileProduct(Guid companyInformation)
        {
            var result = await _dbContext.GetColection.FindAsync(c => c.CompanyInformation == companyInformation).ConfigureAwait(false);
            return result.ToList().Select(c => new FileProduct
            {
                CodProduto = c.CodProduto,
                CodigoBarra = c.Ean,
                DescProduto = c.DescProduto,
                NCM = c.NcmProd,
                Qtd = 0,
                VlUnitario = c.VlUnitario,
                Unidade = c.UnidMedida,


            }).ToList();
        }
        public async Task<bool> UpdateImpostos(List<ProdutosImpsotos> listProdutos)
        {
            IMongoCollection<Produtos> _context = _database.GetCollection<Produtos>("Produtos");
            IndexKeysDefinition<Produtos> subscriberIndex = Builders<Produtos>.IndexKeys             
               .Ascending(p => p.CodProduto);
            CreateIndexOptions subscriberOptions = new CreateIndexOptions()
            {
                Unique = false
            };
            var model = new CreateIndexModel<Produtos>(subscriberIndex, subscriberOptions);
            IMongoCollection<Produtos> collection = _context;
            string subscriberName = collection.Indexes.CreateOne(model);
            var icmsSt = false;
            var ncmMono = false;

            var company = listProdutos.Select(c => c.EmpresaId).FirstOrDefault();           

            foreach (var produtos in listProdutos)
            {
                var produtoDto = _dbContext.GetColection.Find(c => c.Id == produtos.ProdutoId).FirstOrDefault();

                if (produtos.IcmsSt)
                {
                    icmsSt = true;
                }
                if (produtos.NcmMono)
                {
                    ncmMono = true;
                }

                var update = Builders<Produtos>.Update.Set(c => c.NcmMono, ncmMono)
                                                     .Set(c => c.IcmsSt, icmsSt)
                                                     .Set(c => c.Modificado, produtos.Modificado);


                var updateResult = await _dbContext.GetColection.UpdateOneAsync(c => c.Id.Equals(produtos.ProdutoId), update);

                if (produtoDto != null)
                {

                    var lisProd = _dbContext.GetColection.Find(c => c.CodProduto == produtoDto.CodProduto).ToList();


                    foreach (var item in lisProd)
                    {
                        var updateResultInt = await _dbContext.GetColection.UpdateOneAsync(c => c.Id.Equals(item.Id), update);
                    }
                }

                if (updateResult.ModifiedCount > 0)
                {
                    //Criar Log

                };

            }

            return true;
        }

        public async Task<Reclassificacaofornec> GetByIdFornec(Guid empresaId, Guid nfeId)
        {
            var company = _dbContextCompany.GetColection.Find(e => e.Id.Equals(empresaId)).FirstOrDefault();
            var result = _dbContextFornec.GetColection.Find(c => c.CompanyInformation.Equals(empresaId) && c.NfeId.Equals(nfeId)).ToList();

            //ToDo: Modificar modelo Nfe.
            var reclassificacaoDto = new Reclassificacaofornec
            {
                RazaoSocial = company.Name,
                Fantasia = company.Alias,
                Cnpj = company.Cnpj,
                ModeloNfe = "55",
                ListaProdutos = result
            };

            return reclassificacaoDto;
        }

        public async Task<bool> UpdateProdutoFornect(List<ProdutosFornecedor> listaProd)
        {
            try
            {
                if (listaProd.Count > 0)
                {
                    foreach (var item in listaProd)
                    {
                        var updateFornec = Builders<ProdutosFornecedor>.Update.Set(c => c.NcmProd, item.NcmProd)
                                                        .Set(c => c.Cfop, item.Cfop);


                        var updateResult1 = await _dbContextFornec.GetColection.UpdateOneAsync(c => c.Id.Equals(item.Id), updateFornec);


                        if (updateResult1.ModifiedCount > 0)
                        {

                        }

                    }

                    return true;
                }

            }
            catch (Exception ex)
            {
                throw;
            }           

            return false;
        }

        public async Task<bool> UpdateProd(List<ImpostosProd> listProdutos, Guid empresaId, DateTime dhEm)
        {
            var inicioMes = new DateTime(dhEm.Year, dhEm.Month, 1);
            var fimMes = inicioMes.AddMonths(1).AddDays(-1);

            var listaProdutosCod = await _dbContext.GetColection.FindAsync(c => c.CompanyInformation.Equals(empresaId) && c.Modificado == false && c.DhEmt >= inicioMes && c.DhEmt <= fimMes).ConfigureAwait(false);

            var listaProdDto = listaProdutosCod.ToList();

            foreach (var produtos in listProdutos)
            {
                var listaIdProduct = listaProdDto.Where(c => c.CodProduto.Equals(produtos.CodProduto)).ToList();

                produtos.Modificado = true;

                var update = Builders<Produtos>.Update.Set(c => c.IsencaoReducao, produtos.IsencaoReducao)
                                                     .Set(c => c.Beneficios, produtos.Beneficios)
                                                     .Set(c => c.Auditado, produtos.Imune)
                                                     .Set(c => c.Isento, produtos.Isento)
                                                     .Set(c => c.NcmMono, produtos.NcmMono)
                                                     .Set(c => c.IcmsSt, produtos.IcmsSt)
                                                     .Set(c => c.Beneficios, produtos.Beneficios)
                                                     .Set(c => c.Modificado, produtos.Modificado);
                                                     

                foreach (var item in listaIdProduct)
                {
                    var updateResult = await _dbContext.GetColection.UpdateOneAsync(c => c.Id.Equals(item.Id), update);

                    if (updateResult.ModifiedCount > 0)
                    {                    

                    };
                }
            }

            return true;
        }
    }
}
