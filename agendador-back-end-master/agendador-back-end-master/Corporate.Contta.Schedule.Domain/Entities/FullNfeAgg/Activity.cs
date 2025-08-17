using Corporate.Contta.Schedule.Domain.Entities.Base;
using System;

namespace Corporate.Contta.Schedule.Domain.Entities.FullNfeAgg
{
    public class Activity : Entity
    {
        public Activity()
        {
            this.Id = Guid.NewGuid();
        }
        public string Code { get; set; }

        public string Description { get; set; }
    }
}
