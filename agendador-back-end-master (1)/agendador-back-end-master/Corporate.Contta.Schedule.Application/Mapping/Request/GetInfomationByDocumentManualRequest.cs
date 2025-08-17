using Corporate.Contta.Schedule.Application.Mapping.Result.GetInfomationByDocument;
using MediatR;
using System;
using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class GetInfomationByDocumentManualRequest : IRequest<Response.Response>
    {
        public string Document { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }

        public Guid Id { get; set; }

        public List<string> ListCnpj { get; set; } = new List<string>();

        public bool Manual { get; set; }
    }
}
