
using EmaloteContta.Data;
using EmaloteContta.Main;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmaloteContta
{
    public class WorkerSecundario : BackgroundService
    {
        private readonly ILogger<WorkerSecundario> _logger;
        private readonly int time = 2000;
        private string _path;      

        public WorkerSecundario(ILogger<WorkerSecundario> logger)
        {
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {          
            var qtd = 0;            

            while (!stoppingToken.IsCancellationRequested)
            {
                //await InitialXml(@"C:\Users\kennedy.silva\Desktop\Contta\xml", stoppingToken);
                DiretorioViewModel diretorioViewModel = new DiretorioViewModel();
                var listDiretorio = diretorioViewModel.ListaDiretorios.ToList();
                var qtdDi = listDiretorio.Count();

                foreach (var item in listDiretorio)
                {
                    qtd = qtd + 1;
                    if (qtdDi > 6)
                    {
                        if (qtd >= 7)
                        {
                            await InitialXml(item.Caminho, stoppingToken);
                        }
                    }
                }
                qtd = 0;
            }
        }

        private static async Task InitialXml(string item, CancellationToken stoppingToken)
        {
            var path = item;
            var xmlToClass = new XmlToClass(path);
            xmlToClass.OutputFolder = @"C:\XmlIntegrado";

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

                Console.WriteLine($"Notas Integradas Com Sucesso WorkerSecundaria={item}");
            }
        }
    }
}
