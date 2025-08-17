
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Xml.Serialization;
using EmaloteContta.Models.Devolucao;
using EmaloteContta.Data;

namespace EmaloteContta.Servico
{
    public class RabbitmqXml
    {
        public bool RabbitmqFiasMod55(List<EmaloteContta.Models.NotaFiscalEletronicaMod55.NfeProc> NotasFicaisEletronicas)
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

                int cont = 0;
                foreach (var item in NotasFicaisEletronicas)
                {
                    var json = JsonConvert.SerializeObject(item);
                    string message = json;
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "Modelo55",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" [x] Sent {0}", message);
                } 
            }
           
            return true;
        }



        public bool RabbitmqFiasMod57(List<EmaloteContta.Models.CTeMod57.CteProc> NotasFicaisEletronicas)
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
                channel.QueueDeclare(queue: "Modelo57",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                int cont = 0;
                foreach (var item in NotasFicaisEletronicas)
                {
                    var json = JsonConvert.SerializeObject(item);
                    string message = json;
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "Modelo57",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }

            return true;
        }

        public bool RabbitmqFiasMod55Devolucao(List<EmaloteContta.Models.Devolucao.NotaFiscalDevolucaoMod55.NfeProc> NotasFicaisEletronicasDev)
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
                channel.QueueDeclare(queue: "Devolucao",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                int cont = 0;
                foreach (var item in NotasFicaisEletronicasDev)
                {
                    var json = JsonConvert.SerializeObject(item);
                    string message = json;
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "Devolucao",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }

            return true;
        }

        public bool RabbitmqFiasMod55Canceladas(List<Models.ProcEventoNFe> listNotaFiscalCanceladas)
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
                channel.QueueDeclare(queue: "Canceladas",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                int cont = 0;
                foreach (var item in listNotaFiscalCanceladas)
                {
                    var json = JsonConvert.SerializeObject(item);
                    string message = json;
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "Canceladas",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }

            return true;
        }

        public bool RabbitmqFiasMod65(List<EmaloteContta.Models.NotaFiscalConsumidorFinalMod65.NfeProc> listNotaFiscalMod65)
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
                channel.QueueDeclare(queue: "Modelo65",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                int cont = 0;
                foreach (var item in listNotaFiscalMod65)
                {
                    var json = JsonConvert.SerializeObject(item);
                    string message = json;
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "Modelo65",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }

            return true;
        }

        public bool RabbitmqFiasNodServico(List<Models.NotaFiscalDeServico.GerarNfseResposta> NotasFicaisEletronicasServico)
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
                channel.QueueDeclare(queue: "Servico",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                int cont = 0;
                foreach (var item in NotasFicaisEletronicasServico)
                {
                    var json = JsonConvert.SerializeObject(item);
                    string message = json;
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "Servico",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }

            return true;
        }


        public bool RabbitmqCertificado(List<Certificado> listCertificados)
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
                channel.QueueDeclare(queue: "Certificado",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
              
                foreach (var item in listCertificados)
                {
                    var json = JsonConvert.SerializeObject(item);
                    string message = json;
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "Certificado",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }

            return true;
        }



        public bool RabbitmqFiasMod55Geral(List<EmaloteContta.Models.NotaFiscalEletronicaMod55.NfeProc> listXml)
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

                int cont = 0;

                foreach (var item in listXml)
                {
                    var json = JsonConvert.SerializeObject(item);
                    string message = json;
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "Modelo55",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine($"Quantidade_Enviada = {cont = cont + 1}" );
                }               
            }                       

            return true;
        }

    }
}
