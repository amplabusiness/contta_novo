using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Corporate.Contta.Schedule.Domain.Entities.Base
{
    public class Entity : ValueObject
    {
        [Key]
        public Guid? Id { get; set; }         
        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
