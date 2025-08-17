using System;
using System.Collections.Generic;
using System.Text;

namespace EmaloteContta.Models.Speed.Base
{
    public class Entity: ValueObject
    {
        public Guid? Id { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
