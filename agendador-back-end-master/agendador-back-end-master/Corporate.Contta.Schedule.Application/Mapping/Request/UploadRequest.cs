using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
   public  class UploadRequest : IRequest<Response.Response>
    {
        public IFormFile File { get; set; }
        public Guid EmpresaId { get; set; }
    }
}
