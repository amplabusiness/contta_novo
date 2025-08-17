using Corporate.Contta.Schedule.Domain.Enum;
using MediatR;
using System;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class NewNotificationRequest : IRequest<Response.Response>
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
