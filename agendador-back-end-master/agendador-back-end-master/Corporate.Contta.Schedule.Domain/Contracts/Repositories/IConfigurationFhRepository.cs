using Corporate.Contta.Schedule.Domain.Entities.ConfigurationFhAgg;
using Corporate.Contta.Schedule.Domain.Entities.NotificationAgg;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Domain.Contracts.Repositories
{
    public interface IConfigurationFhRepository
    {
        Task Insert(ConfigurationFh configurationFh);

        Task<ConfigurationFh> Get(Guid? empresaId);

        Task<ConfigurationFh> Update(ConfigurationFh configurationFh);

        //Task<Notification> Get(Guid userId);
    }
}
