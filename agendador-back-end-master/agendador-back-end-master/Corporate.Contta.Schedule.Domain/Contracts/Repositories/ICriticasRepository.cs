using Corporate.Contta.Schedule.Domain.Entities.Criticas;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Domain.Contracts.Repositories
{
    public interface ICriticasRepository
    {
        Task Insert(CriticasNovas criticasNovas);
        Task Insert(CriticasAntigas criticasAntigas);

        Task<CriticasNovas> Get(Guid companyId);
    }
}
