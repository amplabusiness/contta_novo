using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Api.Extension
{
    public class FilaRabbit
    {
        public bool RabbitmqFiasMod55Geral(List<IFormFile> listXml)
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = "173.255.209.202",
                    Password = "contta123",
                    Port = 5672,
                    VirtualHost = "/"
                };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "XmlManual",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    int cont = 0;

                    foreach (var item in listXml)
                    {
                        var json = JsonConvert.SerializeObject(item);
                        string message = json;
                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(exchange: "",
                                             routingKey: "XmlManual",
                                             basicProperties: null,
                                             body: body);
                        Console.WriteLine($"Quantidade_Enviada = {cont = cont + 1}");
                    }
                }

                return true;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}

