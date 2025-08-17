using MongoDB.Bson;

namespace Corporate.Contta.Schedule.Domain.Entities.NfeAgg
{
    public class TbAjuste
    {
        public ObjectId Id { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
