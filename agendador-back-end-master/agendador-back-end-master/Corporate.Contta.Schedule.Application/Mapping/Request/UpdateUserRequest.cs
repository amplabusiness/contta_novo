using Corporate.Contta.Schedule.Application.Mapping.Param;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class UpdateUserRequest:NewUserRequest
    {
        public Guid Id { get; set; }
    }
}
