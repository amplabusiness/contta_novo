using Corporate.Contta.Schedule.Domain.Entities.DashboardAgg;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Domain.Contracts.Repositories
{
    public interface IDashboardRepository
    {

        Task<Dashboard> GetByCompany(Guid companyId, DateTime dhEmi);
    }
}
