using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmaloteContta.Infra.Base;
using EmaloteContta.Models.Respositories;
using EmaloteContta.Models.Speed;
using MongoDB.Driver;

namespace EmaloteContta.Infra.Repositorios
{
    public class EmpresaDestinatariaRepositorio : BaseRepository<EmpresaDest>, IEmpresaDestinatariaRepositorio
    {
        private static MongoDBContext<EmpresaDest> _dbContext = new MongoDBContext<EmpresaDest>();

        public EmpresaDestinatariaRepositorio() : base(_dbContext) { }

        public override Task<EmpresaDest> Add(EmpresaDest entity)
        {
            if (!entity.Id.HasValue)
                entity.Id = Guid.NewGuid();

            return base.Add(entity);
        }

        public async Task<bool> EmpresaJaExiste(string cnpj)
        {
            var result = await _dbContext.GetColection.FindAsync(c => c.Cnpj.Equals(cnpj));
            return result.Any();
        }

        public async Task<EmpresaDest> ObterPorCnpj(string cnpj)
        {
            var result = await _dbContext.GetColection.FindAsync(c => c.Cnpj.Equals(cnpj) || c.CPF.Equals(cnpj));
            return await result.FirstOrDefaultAsync();
        }
    }
}
