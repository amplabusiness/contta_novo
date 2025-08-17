using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities.NotificationAgg;
using Corporate.Contta.Schedule.Infra.Repositories.Base;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Infra.Repositories
{
    public class NotificationRepository : BaseRepository<Notification>, INotificationUserRepository
    {
        private static MongoDBContext<Notification> _dbContext = new MongoDBContext<Notification>();
               
        public NotificationRepository() : base(_dbContext) { }

        public async Task<List<Notification>> GetAll(Guid? emrpesaId)
        {
            var builder = Builders<Notification>.Filter;
            var filter = builder.Eq(nt => nt.EmpresaId, emrpesaId);
            var result =  _dbContext.GetColection.Find(filter).ToList();
           
            return result.FindAll(c => c.Active);
        }

        public async Task Insert(Notification notification)
        {
            notification.Id = Guid.NewGuid();
            await _dbContext.GetColection.InsertOneAsync(notification).ConfigureAwait(false);
        }

        public async Task<bool> Update(Notification notification)
        {
            try
            {
                var notificationDto = _dbContext.GetColection.Find(c => c.Id == notification.Id).FirstOrDefault();

                if (notificationDto != null)
                {
                    var update = Builders<Notification>.Update.Set(c => c.Description, notification.Description)
                                               .Set(c => c.RegisterDate, notification.RegisterDate)
                                               .Set(c => c.Active, notification.Active)
                                               .Set(c => c.Result, notification.Result)
                                               .Set(c => c.EmpresaId, notification.EmpresaId);

                    var updateResult = await _dbContext.GetColection.UpdateOneAsync(c => c.Id == notification.Id, update);
                    return updateResult.ModifiedCount > 0;

                }
                else
                {
                    return false;
                }


            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
