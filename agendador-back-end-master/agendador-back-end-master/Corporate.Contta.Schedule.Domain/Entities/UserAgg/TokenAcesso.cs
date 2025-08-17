using MongoDB.Bson;
using System;

namespace Corporate.Contta.Schedule.Domain.Entities.UserAgg
{
    public class TokenAcesso
    {
        public ObjectId _id { get; set; }
        public string TokenAcess { get; set; }
        public Guid? UserId { get; set; }
    }
}
