using Corporate.Contta.Schedule.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Response.UserResponse
{
    public class UserResponse:BaseResult
    {
        public UserResponse()
        {

        }
        public UserResponse(string error)
            :base(error)
        {
            
        }

        public UserResponse(List<string> errors)
            :base(errors)
        {
           
        }

        public void AddUserMessageError(string error)
        {
            AddMessageError(error);
        }
    }
}
