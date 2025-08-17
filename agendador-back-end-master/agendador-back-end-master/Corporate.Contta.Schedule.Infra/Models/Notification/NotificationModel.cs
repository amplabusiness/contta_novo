using Corporate.Contta.Schedule.Infra.Repositories.Base;
using Newtonsoft.Json;
using System;

namespace Corporate.Contta.Schedule.Infra.Models
{
    public class NotificationModel : HttpBaseResult
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("registerDate")]
        public DateTime RegisterDate { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("UserId")]
        public Guid UserId { get; set; }
    }
}
