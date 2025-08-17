using Corporate.Contta.Schedule.Domain.Enum;
using System;

namespace Corporate.Contta.Schedule.Domain.Entities.NotificationAgg
{
    public class Notification
    {
        public Guid Id { get; set; } 
        public string Description { get; set; }       
        public DateTime RegisterDate { get; set; }
        public bool Active { get; set; }
        public Guid? EmpresaId { get; set; }
        public NotificationType CodNotification { get; set; }

        public string Result { get; set; }
    }
}
