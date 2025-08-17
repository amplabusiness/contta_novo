using Corporate.Contta.Schedule.Domain.Entities.CompanyInformationAgg;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Domain.Contracts.Repositories
{
    public interface ISociosRepository
    {
        Task InsertSocios(string cpf, string nome);     
        Task<List<Socios<ObjectId>>> GetAllSociosContta(string cpf, string nome);     

    }
}
