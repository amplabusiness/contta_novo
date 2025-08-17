using EmaloteContta.Data;
using EmaloteContta.Servico;
using EmaloteContta.Tool;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace EmaloteContta
{
    public class WorkerCertificado : BackgroundService
    {
        private readonly ILogger<WorkerCertificado> _logger;
        private readonly int time = 2000;
        private string _path;

        public WorkerCertificado(ILogger<WorkerCertificado> logger)
        {
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)

        {
            while (!stoppingToken.IsCancellationRequested)
            {
                List<Certificado> listaAnteriorJSON = new List<Certificado>();
                CertificadoViewModel certificadoViewModel = new CertificadoViewModel();
                var listCertificado = certificadoViewModel.ListaCertificados.ToList();

                if (listCertificado.Count > 0)
                {
                    var path = Path.Combine(Ferramenta.AppDataJsonPath, nameof(Certificado), $"{nameof(Certificado)}.json");

                    if (File.Exists(path))
                    {
                        var jsonRead = File.ReadAllText(path);
                        listaAnteriorJSON = JsonConvert.DeserializeObject<List<Certificado>>(jsonRead);
                    }

                    var adicionados = listCertificado.Except(listaAnteriorJSON, new DtoEqualityComparer<Certificado>()).ToList();

                    if (adicionados.Count > 0)
                    {
                        XmlDocument doc = new XmlDocument();
                        RabbitmqXml rabbitmqXml = new RabbitmqXml();

                        rabbitmqXml.RabbitmqCertificado(adicionados);

                    }

                    foreach (var item in adicionados)
                    {
                        listaAnteriorJSON.Add(item);
                        Ferramenta.Gravar(listaAnteriorJSON, path);
                    }
                }

            }


        }

        public class DtoEqualityComparer<T> : IEqualityComparer<T> where T : BaseDTO
        {
            public bool Equals(T dto1, T dto2)
            {
                if (dto2 == null && dto1 == null)
                {
                    return true;
                }
                else if (dto1 == null || dto2 == null)
                {
                    return false;
                }
                else if (dto1.CNPJ == dto2.CNPJ)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public int GetHashCode(T dto)
            {
                return 1;// dto.CodigoERP.GetHashCode();
            }
        }
    }
}
