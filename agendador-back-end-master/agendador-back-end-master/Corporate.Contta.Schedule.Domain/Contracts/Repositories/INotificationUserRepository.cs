using Corporate.Contta.Schedule.Domain.Entities.NotificationAgg;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Domain.Contracts.Repositories
{
    public interface INotificationUserRepository
    {
        Task Insert(Notification notification);

        Task<List<Notification>> GetAll(Guid? empresaId);

        Task<bool> Update(Notification notification);

        //Task<Notification> Get(Guid userId);
    }
}
