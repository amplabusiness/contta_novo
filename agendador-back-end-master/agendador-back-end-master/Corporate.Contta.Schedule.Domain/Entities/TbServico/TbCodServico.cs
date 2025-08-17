using Corporate.Contta.Schedule.Domain.Entities.Base;
using MongoDB.Bson;

namespace Corporate.Contta.Schedule.Domain.Entities.TbServico
{
    public class TbCodServico 
    {
        public ObjectId _id { get; set; }
        public int codservico { get; set; }
        public string descservico { get; set; }
    }
}
