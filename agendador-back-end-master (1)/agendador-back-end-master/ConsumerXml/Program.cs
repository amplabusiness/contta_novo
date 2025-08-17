using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace ConsumerXml
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                HostName = "contta.com.br",
                Password = "contta123",
                Port = 5672,
                VirtualHost = "/"
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "Modelo55",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);               
                consumer.Received += (model, ea) =>
                {
                    NfeProc result = Messagem(ea);

                    Console.WriteLine(" [x] Received {0}", result);

                };
                channel.BasicConsume(queue: "Modelo55",
                                     autoAck: true,
                                     consumer: consumer);               

                Console.ReadLine();
            }
        }

        private static NfeProc Messagem(BasicDeliverEventArgs ea)
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var result = JsonConvert.DeserializeObject<NfeProc>(message);
            return result;
        }
    }
}
