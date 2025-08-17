using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ConttaComsumidor.Models.CompanyInformationAgg;
using EmaloteContta.Infra.Base;
using EmaloteContta.Models.Respositories;
using EmaloteContta.Models.Speed;
using MongoDB.Driver;
namespace EmaloteContta.Infra.Repositorios
{
    public class EmpresaEmitenteRepositorio : BaseRepository<ConttaComsumidor.Models.CompanyInformationAgg.CompanyInformation>
    {
        private static MongoDBContext<ConttaComsumidor.Models.CompanyInformationAgg.CompanyInformation> _dbContext = new MongoDBContext<ConttaComsumidor.Models.CompanyInformationAgg.CompanyInformation>();

        public EmpresaEmitenteRepositorio() : base(_dbContext) { }


        public async Task<bool> EmpresaJaExiste(string cnpj)
        {
            var result = await _dbContext.GetColection.FindAsync(c => c.Cnpj.Equals(cnpj));
            return result.Any();
        }

        public async Task<ConttaComsumidor.Models.CompanyInformationAgg.CompanyInformation> ObterPorCnpj(string cnpj)
        {
            var result = _dbContext.GetColection.Find(c => c.Cnpj.Equals(cnpj)).FirstOrDefault();
            return result;
        }

        public async Task<List<ConttaComsumidor.Models.CompanyInformationAgg.CompanyInformation>> ObterTodasEmpresa()
        {
            var result = _dbContext.GetColection.Find(c => c.Cnpj != null).ToList();
            return result;
        }
    }
}
