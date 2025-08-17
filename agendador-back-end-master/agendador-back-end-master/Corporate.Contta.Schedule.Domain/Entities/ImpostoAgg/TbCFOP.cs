using Corporate.Contta.Schedule.Domain.Entities.Base;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Domain.Entities.ImpostoAgg
{
    public class TbCFOP
    {
        public Guid Id { get; set; }
        public double CFOP { get; set; }

        public string Descricao { get; set; }
    }
}
