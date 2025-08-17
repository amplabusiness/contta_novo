namespace ConsumerXml
{
    public class RabbitOptions
    {
        public string Host { get; set; } = "contta.com.br";
        public int Port { get; set; } = 5672;
        public string VirtualHost { get; set; } = "/";
        public string User { get; set; } = "guest";
        public string Password { get; set; } = "guest";
        public string Queue { get; set; } = "Modelo55";
        public ushort Prefetch { get; set; } = 20;
        public bool Durable { get; set; } = false;
        public bool Exclusive { get; set; } = false;
        public bool AutoDelete { get; set; } = false;
        public string? DeadLetterExchange { get; set; }
        public string? DeadLetterRoutingKey { get; set; }
    }
}
