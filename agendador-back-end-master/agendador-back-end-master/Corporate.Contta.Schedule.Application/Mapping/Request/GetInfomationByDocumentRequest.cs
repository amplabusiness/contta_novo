using Corporate.Contta.Schedule.Application.Mapping.Result.GetInfomationByDocument;
using MediatR;
using System;
using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class GetInfomationByDocumentRequest : IRequest<GetInfomationByDocumentResponse>
    {
        public string Document { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }

        public Guid Id { get; set; }

        public string   Diretorio { get; set; }

        public bool Manual { get; set; }
        public bool ConfirmarCadastro { get; set; }
        public Guid? UserIdTerceiro { get; set; }
    }
}
