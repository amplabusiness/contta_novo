using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities.ImpostoAgg;
using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using Corporate.Contta.Schedule.Infra.Repositories.Base;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Corporate.Contta.Schedule.Infra.Repositories
{
    public class ImpostoProductRepository : BaseRepository<ImpostoAntecipacao>, IImpostosProdutosRepository
    {
        private static MongoDBContext<ImpostoAntecipacao> _dbContextAntecipacao = new MongoDBContext<ImpostoAntecipacao>();
        private static MongoDBContext<ImpostoInsento> _dbContextInsento = new MongoDBContext<ImpostoInsento>();
        private static MongoDBContext<ImpostoImune> _dbContextInume = new MongoDBContext<ImpostoImune>();
        private static MongoDBContext<ImpostoRedCestaBasica> _dbContextRedBasic = new MongoDBContext<ImpostoRedCestaBasica>();
        private static MongoDBContext<ImpostoExigibilidadeSus> _dbContextExigibilidade = new MongoDBContext<ImpostoExigibilidadeSus>();
        private static MongoDBContext<ImpostoReducao> _dbContexReducao = new MongoDBContext<ImpostoReducao>();       
        private static MongoDBContext<TbCfopGeral> _dbContexCfopGeral = new MongoDBContext<TbCfopGeral>();
        private static MongoDBContext<TbCFOP> _dbContexCfop = new MongoDBContext<TbCFOP>();
        private static MongoDBContext<NFE> _dbContexNfe = new MongoDBContext<NFE>();

        public ImpostoProductRepository() : base(_dbContextAntecipacao) { }


        public async Task<List<TbCfopGeral>> GetAllCfopGeral()
        {
            try
            {
                IMongoClient mongoClient = new MongoClient("mongodb://contta:contta123456@192.46.218.34:27017/?authSource=admin&readPreference=primary&ssl=false");
                IMongoDatabase database = mongoClient.GetDatabase("conttadb");             
                IMongoCollection<TbCfopGeral> _collectionTbCompanyFatura = database.GetCollection<TbCfopGeral>("TbCfopGeral");

                var result = _collectionTbCompanyFatura.Find(C => C.Descricao != "").ToList();
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<List<ImpostoRedCestaBasica>> GetAllImpostoCestaBasica(Guid empresaId)
        {
            var result = await _dbContextRedBasic.GetColection.FindAsync(c => c.CompanyInformation.Equals(empresaId)).ConfigureAwait(false);
            return result.ToList();
        }

        public async Task<List<ImpostoAntecipacao>> GetAllImpostosAntecipacao(Guid empresaId)
        {
            var result = await _dbContextAntecipacao.GetColection.FindAsync(c => c.CompanyInformation.Equals(empresaId)).ConfigureAwait(false);
            return result.ToList();
        }

        public async Task<List<ImpostoExigibilidadeSus>> GetAllImpostosExi(Guid empresaId)
        {
            var result = await _dbContextExigibilidade.GetColection.FindAsync(c => c.CompanyInformation.Equals(empresaId)).ConfigureAwait(false);
            return result.ToList();
        }

        public async Task<List<ImpostoImune>> GetAllImpostosimune(Guid empresaId)
        {
            var result = await _dbContextInume.GetColection.FindAsync(c => c.CompanyInformation.Equals(empresaId)).ConfigureAwait(false);
            return result.ToList();
        }

        public async Task<List<ImpostoInsento>> GetAllImpostosInsento(Guid empresaId)
        {
            var result = await _dbContextInsento.GetColection.FindAsync(c => c.CompanyInformation.Equals(empresaId)).ConfigureAwait(false);
            return result.ToList();
        }

        public async Task<List<ImpostoReducao>> GetAllImpostosReducao(Guid empresaId)
        {
            var result = await _dbContexReducao.GetColection.FindAsync(c => c.CompanyInformation.Equals(empresaId)).ConfigureAwait(false);
            return result.ToList();
        }

        public async Task InsertAnt(List<ImpostoAntecipacao> listEntity)
        {
            try
            {
                if (listEntity.Count > 0)
                {
                    foreach (var item in listEntity)
                    {
                        item.Id = Guid.NewGuid();
                        item.NCM = item.NCM.Replace(".", "");
                        item.Status = "Ativo";
                        _dbContextAntecipacao.GetColection.InsertOne(item);
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task InsertExigibilidadeSus(ImpostoExigibilidadeSus entity)
        {
            try
            {
                entity.Id = Guid.NewGuid();
                entity.Status = "Ativo";
                _dbContextExigibilidade.GetColection.InsertOne(entity);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task InsertImun(List<ImpostoImune> listEntity)
        {
            try
            {
                if (listEntity.Count > 0)
                {
                    foreach (var item in listEntity)
                    {
                        item.Id = Guid.NewGuid();
                        item.NCM = item.NCM.Replace(".", "");
                        item.Status = "Ativo";
                        _dbContextInume.GetColection.InsertOne(item);
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task InsertInsent(List<ImpostoInsento> listEntity)
        {
            try
            {
                if (listEntity.Count > 0)
                {
                    foreach (var item in listEntity)
                    {
                        item.Id = Guid.NewGuid();
                        item.NCM = item.NCM.Replace(".", "");
                        item.Status = "Ativo";
                        _dbContextInsento.GetColection.InsertOne(item);
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task InsertRedCestaBasica(List<ImpostoRedCestaBasica> listEntity)
        {
            try
            {
                if (listEntity.Count > 0)
                {
                    foreach (var item in listEntity)
                    {
                        item.Id = Guid.NewGuid();
                        item.NCM = item.NCM.Replace(".", "");
                        item.Status = "Ativo";
                        _dbContextRedBasic.GetColection.InsertOne(item);
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task InsertReducao(ImpostoReducao entity)
        {
            try
            {
                entity.Id = Guid.NewGuid();
                entity.Status = "Ativo";
                _dbContexReducao.GetColection.InsertOne(entity);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<TbCFOP>> NewCfop(TbCFOP tbCfop)
        {

            var cfop = _dbContexCfop.GetColection.Find(c => c.CFOP == tbCfop.CFOP).FirstOrDefault();

            if(cfop == null)
            {
                tbCfop.Id = Guid.NewGuid();
                _dbContexCfop.GetColection.InsertOne(tbCfop);
                return _dbContexCfop.GetColection.Find(c => c.CFOP > 0).ToList();
            }
            return null;
        }

        public async Task<bool> DeleteCfop(Guid tbCfop)
        {
            try
            {
                var cfop = _dbContexCfop.GetColection.Find(c => c.Id.Equals(tbCfop)).FirstOrDefault();
                var result = _dbContexCfop.GetColection.DeleteOne(c => c.Id.Equals(tbCfop));
                if (result.DeletedCount > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<TbCFOP>> GetAllCfop()
        {
            var result = _dbContexCfop.GetColection.Find(c => c.CFOP > 0).ToList();

            return result;
        }

        public async Task UpdateExigibilidadeSus(Guid id, bool status)
        {
            try
            {
                var statusDto = "";
                if (status)
                    statusDto = "Ativo";
                else
                    statusDto = "Bloqueado";

                var exigibilidadeSus = _dbContextExigibilidade.GetColection.Find(c => c.Id.Equals(id)).FirstOrDefault();
                if (exigibilidadeSus != null)
                {
                    var update = Builders<ImpostoExigibilidadeSus>.Update.Set(c => c.Status , statusDto);
                                                 

                                                    var updateResult = await _dbContextExigibilidade.GetColection.UpdateOneAsync(c => c.Id.Equals(id), update);

                    if (updateResult.ModifiedCount > 0)
                    {
                        //Criar Log

                    };
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateEncerramento(Guid id, bool status)
        {
            try
            {
                var statusDto = "";
                if (status)
                    statusDto = "Ativo";
                else
                    statusDto = "Bloqueado";

                var exigibilidadeSus = _dbContextAntecipacao.GetColection.Find(c => c.Id.Equals(id)).FirstOrDefault();
                if (exigibilidadeSus != null)
                {
                    var update = Builders<ImpostoAntecipacao>.Update.Set(c => c.Status, statusDto);


                    var updateResult = await _dbContextAntecipacao.GetColection.UpdateOneAsync(c => c.Id.Equals(id), update);

                    if (updateResult.ModifiedCount > 0)
                    {
                        //Criar Log

                    };
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<NFE>> GetNfeDifal(Guid company, DateTime periudo)
        {
            var inicioMes = new DateTime(periudo.Year, periudo.Month, 1);
            var fimMes = inicioMes.AddMonths(1).AddDays(-1);          

            return _dbContexNfe.GetColection.Find(c => c.CompanyInformation.Equals(company) && c.Ativo && c.Difal && c.ModeloTipo == "Entrada" && c.DhEmi.Value >= inicioMes && c.DhEmi.Value <= fimMes).ToList();

        }
    }
}
