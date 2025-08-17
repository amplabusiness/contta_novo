using Corporate.Contta.Schedule.Domain.Entities.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Domain.Contracts.Repositories
{
    public interface IConfigurationUserRepository
    {
        Task Insert(ConfigurationUser configurationUser);

        Task<ConfigurationUser> Get(Guid userId);     

        Task<bool> Update(ConfigurationUser configurationUser);

        Task<bool> UpdateConf(Guid userId);

     
    }
}
