using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EmaloteContta.Main;
using EmaloteContta.Data;

namespace EmaloteContta
{
    public class WorkerPrimario : BackgroundService
    {
        private readonly ILogger<WorkerPrimario> _logger;
        private readonly int time = 2000;
        private string _path;   
        public WorkerPrimario(ILogger<WorkerPrimario> logger)
        {
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {          
            var qtd = 0;      
            

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    //await InitialXml(@"C:\Users\kennedy.silva\Desktop\Contta\xml", stoppingToken);
                    DiretorioViewModel diretorioViewModel = new DiretorioViewModel();
                    var listDiretorio = diretorioViewModel.ListaDiretorios.ToList();
                    var qtdDi = listDiretorio.Count();

                    foreach (var item in listDiretorio)
                    {
                        if (qtdDi > 6)
                        {
                            qtd = qtd + 1;
                            if (qtd >= 0 && qtd <= 6)
                            {
                                await InitialXml(item.Caminho, stoppingToken);
                            }
                        }
                        else
                        {
                            await InitialXml(item.Caminho, stoppingToken);
                        }

                    }
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
               
                qtd = 0;
            }
        }

        private static async Task InitialXml(string item, CancellationToken stoppingToken)
        {
            var path = item;
            var xmlToClass = new XmlToClass(path);
       

            if (Directory.Exists(@"C:\XmlIntegrado"))
            {
                xmlToClass.OutputFolder = @"C:\XmlIntegrado";
            }
            else
            {
                Directory.CreateDirectory(@"C:\XmlIntegrado");
                xmlToClass.OutputFolder = @"C:\XmlIntegrado";
            }

            if (Directory.Exists(path))
            {
                xmlToClass.Start();
                xmlToClass.GetNotasFicaisDeServico();
                xmlToClass.GetNotasFicaisEletronicas();
                xmlToClass.GetNotasFicaisEletronicasDev();
                xmlToClass.GetNotaFiscalModCanc();
                xmlToClass.GetNotaFiscalMod65();
                xmlToClass.GetNotaFiscalCteMod57();
                xmlToClass.GetCtes();
                var erros = xmlToClass.GetErros();
                if (erros.Count > 0)
                    erros.ForEach(c => { Console.WriteLine(c); });
                await Task.Delay(1000, stoppingToken);

                Console.WriteLine($"Notas Integradas Com Sucesso Worker_Primaria={item}");
            }
        }
    }
}
