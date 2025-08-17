using Corporate.Contta.Schedule.Domain.Entities.BlocoE;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Domain.Contracts.Repositories
{
    public interface IAgBlocoERepository
    {
        Task Create(AgBlocoE agBlocoE);
        Task<AgBlocoE> GetByCompanyInformation(Guid companyInformation, DateTime date);
    }
}
