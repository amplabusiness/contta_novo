using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;
using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading;

namespace ConsumerXml
{
    class Program
    {
        static void Main(string[] args)
        {
            // Configure logging (Serilog console)
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            // Load configuration from appsettings.json + environment variables
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var rabbit = new RabbitOptions();
            config.GetSection("RabbitMQ").Bind(rabbit);
            // Env vars override
            rabbit.Host = GetEnv("RABBITMQ_HOST", rabbit.Host);
            rabbit.Port = GetEnvInt("RABBITMQ_PORT", rabbit.Port);
            rabbit.VirtualHost = GetEnv("RABBITMQ_VHOST", rabbit.VirtualHost);
            rabbit.User = GetEnv("RABBITMQ_USER", rabbit.User);
            rabbit.Password = GetEnv("RABBITMQ_PASSWORD", rabbit.Password);
            rabbit.Queue = GetEnv("RABBITMQ_QUEUE", rabbit.Queue);
            rabbit.Prefetch = (ushort)Math.Max(1, GetEnvInt("RABBITMQ_PREFETCH", rabbit.Prefetch));

            // Support CloudAMQP single URL via RABBITMQ_URL
            // Example: amqps://user:pass@host/vhost
            var rabbitUrl = Environment.GetEnvironmentVariable("RABBITMQ_URL");
            ConnectionFactory factory;
            if (!string.IsNullOrWhiteSpace(rabbitUrl))
            {
                factory = new ConnectionFactory
                {
                    Uri = new Uri(rabbitUrl),
                    AutomaticRecoveryEnabled = true,
                };
                Log.Information("Using RABBITMQ_URL for connection (host: {Host}, vhost: {VHost})", factory.HostName, factory.VirtualHost);
            }
            else
            {
                factory = new ConnectionFactory
                {
                    HostName = rabbit.Host,
                    Port = rabbit.Port,
                    VirtualHost = rabbit.VirtualHost,
                    UserName = rabbit.User,
                    Password = rabbit.Password,
                    AutomaticRecoveryEnabled = true,
                };
            }
            using (var connection = factory.CreateConnection())
            {
                IModel channel = connection.CreateModel();
                try
                {
                // Queue declaration strategy:
                // 1) Try passive declare – if the queue exists, don't redefine properties/args.
                // 2) If not found (channel gets closed with 404), recreate channel and declare with desired props and DLQ args.
                bool queueExists = false;
                try
                {
                    channel.QueueDeclarePassive(rabbit.Queue);
                    queueExists = true;
                    Log.Information("Queue '{Queue}' exists; using passive declare.", rabbit.Queue);
                }
                catch (RabbitMQ.Client.Exceptions.OperationInterruptedException ex) when (ex.ShutdownReason != null && ex.ShutdownReason.ReplyCode == 404)
                {
                    Log.Warning("Queue '{Queue}' not found. Will declare it with configured properties.", rabbit.Queue);
                }

                if (!queueExists)
                {
                    // The channel is closed after a failed passive declare; open a new one
                    try { channel.Close(); } catch { }
                    try { channel.Dispose(); } catch { }
                    channel = connection.CreateModel();

                    var queueArgs = new Dictionary<string, object>();
                    if (!string.IsNullOrWhiteSpace(rabbit.DeadLetterExchange))
                        queueArgs["x-dead-letter-exchange"] = rabbit.DeadLetterExchange;
                    if (!string.IsNullOrWhiteSpace(rabbit.DeadLetterRoutingKey))
                        queueArgs["x-dead-letter-routing-key"] = rabbit.DeadLetterRoutingKey;

                    channel.QueueDeclare(queue: rabbit.Queue,
                                         durable: rabbit.Durable,
                                         exclusive: rabbit.Exclusive,
                                         autoDelete: rabbit.AutoDelete,
                                         arguments: queueArgs.Count > 0 ? queueArgs : null);
                }

                // Apply QoS to avoid many unacked messages in-flight
                channel.BasicQos(0, rabbit.Prefetch, false);

        var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    try
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
            var result = MessageParser.ParseNfeProc(message);

                        if (result == null)
                            throw new Exception("Failed to deserialize message to NfeProc (unsupported format or schema mismatch).");

                        // Prepare a compact summary for logs
                        var summary = new
                        {
                            chNFe = result?.ProtNFe?.InfProt?.ChNFe ?? result?.NFe?.InfNFe?.Id,
                            emitCNPJ = result?.NFe?.InfNFe?.Emit?.CNPJ,
                            destCNPJ = result?.NFe?.InfNFe?.Dest?.CNPJ,
                            destCPF = result?.NFe?.InfNFe?.Dest?.CPF,
                            valorNF = result?.NFe?.InfNFe?.Total?.ICMSTot?.VNF,
                            itens = result?.NFe?.InfNFe?.Det?.Count ?? 0,
                            emissao = result?.NFe?.InfNFe?.Ide?.DhEmi,
                        };

                        Log.Information("Modelo55 consumed: {Summary}", summary);

                        // Ack after successful processing
                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    }
                    catch (Exception ex)
                    {
            Log.Error(ex, "Failed to process message");
                        // Nack without requeue to avoid poison message loops
                        channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: false);
                    }

                };
                channel.BasicConsume(queue: rabbit.Queue,
                                     autoAck: false,
                                     consumer: consumer);

                // Graceful shutdown support
                using var cts = new CancellationTokenSource();
                Console.CancelKeyPress += (s, e) => { e.Cancel = true; cts.Cancel(); };
                Log.Information("Consumer started. Press Ctrl+C to stop.");
                while (!cts.IsCancellationRequested)
                {
                    Thread.Sleep(200);
                }
                Log.Information("Shutting down consumer...");
                }
                finally
                {
                    try { channel?.Close(); } catch { }
                    channel?.Dispose();
                }
            }
        }

    private static string GetEnv(string key, string defaultValue = null)
        {
            var v = Environment.GetEnvironmentVariable(key);
            return string.IsNullOrEmpty(v) ? defaultValue : v;
        }

    private static int GetEnvInt(string key, int defaultValue)
        {
            var v = Environment.GetEnvironmentVariable(key);
            return int.TryParse(v, out var i) ? i : defaultValue;
        }
    }
}
