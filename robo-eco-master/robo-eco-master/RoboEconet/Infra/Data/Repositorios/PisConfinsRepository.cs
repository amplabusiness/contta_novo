using System;
using System.Threading.Tasks;
using XmlCteNfeNfsToClass.Infra.Base;
using RoboEconet.Models;
using RoboEconet.Infra.Data.Interface;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Linq;

namespace XmlCteNfeNfsToClass.Infra.Repositorios
{
    public class PisConfinsRepository : BaseRepository, IPisConfinsRepository
    {
        private static MongoDBContext<PisConfinsDto> _dbContext = new MongoDBContext<PisConfinsDto>();

        private BaseRepository _baseRepository;

        public PisConfinsRepository() : base(_dbContext)
        {
        }

        public async Task<bool> Create(List<PisConfinsDto> listEntity)
        {
            try
            {
                List<PisConfinsDto> listaPisConfinsDto = new List<PisConfinsDto>();

                var listaNcmDtoConvert = listEntity.ToList();
                var listaNcm = _dbContext.GetColection.Find(c => c.NCMPai != null);
                var listaConvert = listaNcm.ToList();

                foreach (var item in listEntity)
                {
                    var ncmAtual = listaConvert.Find(c => c.NCMPai == item.NCMPai);

                    if (ncmAtual == null)
                    {
                        _dbContext.GetColection.InsertMany(listEntity);
                        return true;
                        
                    }
                    else
                    {
                        return false;
                    }
                }

                return true;

            }
            catch (Exception ex)
            {

                return false;
            }
        }


        //public async Task<List<Impostos>> ObterTodosImpostoDaNota(Guid nfeId)
        //{
        //    var result = await _dbContext.GetColection.FindAsync(c => c.NfeId == nfeId);
        //    return result.ToList();
        //}

        //public async Task<Impostos> ObterTodosImpostoProduto(Guid produtoId)
        //{
        //    var result = await _dbContext.GetColection.FindAsync(c => c.ProdutoId == produtoId);
        //    return result.FirstOrDefault();
        //}
    }
}
