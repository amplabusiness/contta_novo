using Corporate.Contta.Schedule.Domain.Entities.UserAgg;
using Corporate.Contta.Schedule.Domain.Enum;
using MediatR;
using System;
using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Application.Mapping.Param
{
    public class NewUserRequest : IRequest<Response.Response>
    {
        public NewUserRequest()
        {
            Companies = new List<UserCompany>();
            IsActive = true;
        }

        public UserGroup Group { get; set; }
        public string Role { get; set; }
        public string UserMasterId { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Picture { get; set; }
        public bool IsActive { get; set; }
        public List<Guid?> CompanyId { get; set; }
        public int Authorization { get; set; }

        //ToDo: Esse campo e para ser modificado depois que tiver as autorização nas requisição
        public string TokenAcesso { get; set; }
        public string Token { get; set; }

        //TODO: essas propriedades devem virar value objects
        public List<UserCompany> Companies { get; set; }
        public DateTime InsertDate { get; private set; }

        public bool UserComum { get; set; }

    }
}
