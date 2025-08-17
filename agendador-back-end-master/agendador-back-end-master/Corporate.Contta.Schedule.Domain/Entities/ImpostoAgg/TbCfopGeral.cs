using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Domain.Entities.ImpostoAgg
{
    public class TbCfopGeral
    {
        public ObjectId Id { get; set; }
        public int Codigo { get; set; }
        public string Descricao { get; set; }
    }   
}
