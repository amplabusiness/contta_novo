using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
   public class NewCompanySociosRequest : IRequest<Response.Response>
    {
        public string Document { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public Guid Id { get; set; }
    }
}
