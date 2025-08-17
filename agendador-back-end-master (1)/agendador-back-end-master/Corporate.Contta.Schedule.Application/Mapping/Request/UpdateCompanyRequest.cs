using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class UpdateNfeRequest: NewNfeRequest
    {
        public Guid Id { get; set; }
    }
}
