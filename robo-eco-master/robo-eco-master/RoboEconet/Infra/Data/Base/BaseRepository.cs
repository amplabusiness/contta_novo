using MongoDB.Driver;
using RoboEconet.Infra.Data.Interface;
using RoboEconet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XmlCteNfeNfsToClass.Infra.Base
{
    public class BaseRepository 
    {
        private MongoDBContext<PisConfinsDto> _dbContext;

        public BaseRepository(MongoDBContext<PisConfinsDto> dbContext)
        {
            _dbContext = dbContext;
        }

        public  async Task<bool> Add(List<PisConfinsDto> listEntity)
        {
            try
            {
                var lisncmCad = _dbContext.GetColection.Find(c => c.NCM != null).ToList();
                bool result = listEntity.All(o => lisncmCad.Any(w => w.NCM == o.NCM));

                await _dbContext.GetColection.InsertManyAsync(listEntity);
                return true;

            }
            catch (Exception ex)
            {

                throw;
            }
            
            return false;
        }
    }
}
