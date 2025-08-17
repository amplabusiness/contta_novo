using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;

namespace EmaloteContta
{
    public class Program
    {
        public static void Main(string[] args)
        {

            new Nucleo().Start();
            CreateHostBuilder(args).Build().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    //services.AddHostedService<WorkerCertificado>();
                    services.AddHostedService<WorkerPrimario>();
                    //services.AddHostedService<WorkerSecundario>();
                });


        public class Division
        {
            public void DivisionRandom()
            {
                try
                {
                    var number1 = new Random().Next(0, 10);
                    var number2 = new Random().Next(0, 10);

                    var result = number1 / number2;
                }
                catch (DivideByZeroException ex)
                {
                    throw new Exception($"Divisão por zero {ex.Message}");
                }
            }
        }
    }
}
