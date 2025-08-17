using Corporate.Contta.Schedule.Application.Mapping.Dto.User;
using Corporate.Contta.Schedule.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Result.GetUser
{
    public sealed class GetUserResponse
    {
        public GetUserResponse(List<UserDTO> getUserResponses)
        {
            ListUserDTO = getUserResponses;
        }
        public List<UserDTO> ListUserDTO { get; private set; }

    }
}
