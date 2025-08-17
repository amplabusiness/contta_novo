using AutoMapper;
using Corporate.Contta.Schedule.Api.EmaloteXml;
using Corporate.Contta.Schedule.Api.Extension;
using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities.NotificationAgg;
using Corporate.Contta.Schedule.Domain.Enum;
using Corporate.Contta.Schedule.Infra.Repositories.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Corporate.Contta.Schedule.Domain.Entities.ModeloXml.NotaFiscalEletronicaMod55;
using Corporate.Contta.Schedule.Infra.Tools;
using System.Threading;

namespace Corporate.Contta.Schedule.Api.Controllers
{
    [Route("api/upload")]
    public class UploadController : Controller
    {
        //private readonly IExternalTableRepository _tableRepository;
        readonly ILogger<UploadController> _log;
        readonly IDiagnosticContext _diagnosticContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ICompanyGateway _companyGateway;
        private readonly INotificationUserRepository _notificationRepository;
        private readonly IConfigurationUserRepository _configurationUserRepository;
        private readonly IEstoqueRepository _estoqueRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly INfeRepository _nfeRepository;

        public static IWebHostEnvironment _environment;

        public UploadController(/*IExternalTableRepository tableRepository,*/
                              ILogger<UploadController> log,
                              IDiagnosticContext diagnosticContext,
                              INfeRepository nfeRepository,
                              IMapper mapper,
                              IMediator mediator,
                              ICompanyGateway companyGateway,
                              IEstoqueRepository estoqueRepository,
                              IConfigurationUserRepository configurationUserRepository,
                              INotificationUserRepository notificationUserRepository,
                              ICompanyRepository companyRepository,
                              IWebHostEnvironment environment)

        {
            //_tableRepository = tableRepository;
            _log = log;
            _diagnosticContext = diagnosticContext ?? throw new ArgumentNullException(nameof(diagnosticContext));
            _mapper = mapper;
            _mediator = mediator;
            _environment = environment;
            _nfeRepository = nfeRepository;
            _estoqueRepository = estoqueRepository;
            _configurationUserRepository = configurationUserRepository;
            _notificationRepository = notificationUserRepository;
            _companyRepository = companyRepository;
            _companyGateway = companyGateway;
        }

        [Route("newextable")]
        [HttpPost]
        public async Task<ActionResult> IntertExTable([FromForm] IFormFile file, string token)
        {
            var newMotification = new Notification();
            ConvertTokenId convertTokenId = new ConvertTokenId();
            var userId = convertTokenId.GetTokenUserMaster(token);

            if (file != null && file.Length > 0)
            {
                try
                {
                    if (!Directory.Exists(_environment.ContentRootPath + $"\\imagens{userId + DateTime.Now}\\"))
                    {
                        Directory.CreateDirectory(_environment.ContentRootPath + $"\\imagens{userId + DateTime.Now}\\");
                    }
                    using (FileStream filestream = System.IO.File.Create(_environment.ContentRootPath + $"\\imagens{userId + DateTime.Now}\\" + file.FileName))
                    {
                        await file.CopyToAsync(filestream);
                        filestream.Flush();
                        filestream.Close();
                        //var result = exelCnpj.GetListaCnpj(filestream.Name);

                        await _companyGateway.NewComanyLote(filestream.Name, token);

                        //var recorteDiretorio = filestream.Name.Substring(0, 85);
                        //var recorteDiretorio = filestream.Name.Substring(1, 12);
                        //System.IO.Directory.Delete(recorteDiretorio, true);                      

                        if (userId != null && userId != string.Empty)
                        {
                            newMotification.Active = true;
                            newMotification.Description = NotificationType.Lista_de_empresas_cadastradas_com_sucesso.ToString();
                            newMotification.RegisterDate = DateTime.Now;
                            //Passar por empresa
                            //newMotification.EmpresaId = userId;

                            var resultNotification = _notificationRepository.Insert(newMotification);
                            if (resultNotification != null)
                                return Ok("Lista de Cnpj Adicionada com sucesso!");
                        }
                        newMotification.Description = NotificationType.Erro_Gravacao_Lista_Empresa.ToString();
                        return BadRequest($"Não foi possivel efeturar o cadastro da tabela");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest($"Não foi possivel efeturar o cadastro da tabela={ ex}");
                }
            }
            else
                return BadRequest("Esta chegando null o arquivo");
        }

        [Route("newextableEstoque")]
        [HttpPost]
        public async Task<string> IntertExTableEstoque([FromForm] IFormFile file, Guid empresaId, string nomeCliente)
        {
            var notification = new Notification();

            if (file != null && file.Length > 0)
            {
                try
                {
                    if (!Directory.Exists(_environment.ContentRootPath + $"\\{nomeCliente + DateTime.Now.Day}\\"))
                    {
                        Directory.CreateDirectory(_environment.ContentRootPath + $"\\{nomeCliente + DateTime.Now.Day}\\");
                    }
                    else
                    {
                        return "Empresa já contem tabela de estoque cadastrada.";
                    }
                    using (FileStream filestream = System.IO.File.Create(_environment.ContentRootPath + $"\\{nomeCliente + DateTime.Now.Day}\\" + file.FileName))
                    {
                        await file.CopyToAsync(filestream);
                        filestream.Flush();
                        filestream.Close();

                        _estoqueRepository.AddEstoque(filestream.Name, empresaId);                       
                        //var recorteDiretorio = filestream.Name.Substring(0, 13);
                        //System.IO.Directory.Delete(recorteDiretorio, true);
                      
                        return "Tabela cadastrada com sucesso!";
                    }
                }
                catch (Exception ex)
                {

                    notification.Active = true;
                    notification.CodNotification = NotificationType.Erro_Gravacao_Lista_Estoque;
                    notification.Description = "Erro ao Efeturar o Cadastro da Tabela";
                    notification.EmpresaId = empresaId;
                    notification.RegisterDate = DateTime.Now;
                    notification.Result = "Error";
                    await _notificationRepository.Insert(notification);
                    return "Erro ao Efeturar o Cadastro da Tabela";
                }
            }

            notification.Active = true;
            notification.CodNotification = NotificationType.Erro_Arquivo_Enviado_Invalido;
            notification.Description = "Arquivo Enviado Esta Invalido";
            notification.EmpresaId = empresaId;
            notification.RegisterDate = DateTime.Now;
            notification.Result = "Error";
            await _notificationRepository.Insert(notification);
            return "Arquivo Enviado Esta Invalido";
        }

        [Route("xml")]
        [HttpPost]
        public async Task<IActionResult> XmlNfe([FromForm] List<IFormFile> files)
        {
            List<NfeProc> listaNfeMod55 = new List<NfeProc>();
            List<FileStream> diretorioXml = new List<FileStream>();
            List<string> listaNfeCanceladas = new List<string>();
            List<string> listaNfeDevolucao = new List<string>();
            List<string> listaNfeServico = new List<string>();
            List<string> listaNfeCte = new List<string>();
            List<string> listaNaoIntentificada = new List<string>();
            List<string> listaNfeConsumidorFinal = new List<string>();
            List<string> listaCartaCorrecao = new List<string>();
            List<string> listaCienciaOperacao = new List<string>();
            List<string> listaEvendoCte = new List<string>();
            List<string> listaConfirmacaoOperaca = new List<string>();
            List<string> listaDesconhecimentoOperacao = new List<string>();
            GerenciamentoXml gerenciamentoXml = new GerenciamentoXml();
            try
            {
                foreach (var item in files)
                {
                    if (!Directory.Exists(_environment.ContentRootPath + "\\xml\\"))
                    {
                        Directory.CreateDirectory(_environment.ContentRootPath + "\\xml\\");
                    }
                    using (FileStream filestream = System.IO.File.Create(_environment.ContentRootPath + "\\xml\\" + item.FileName))
                    {

                        await item.CopyToAsync(filestream);
                        filestream.Flush();
                        filestream.Close();

                        var restul = VerificarTipoDoXml.RetornarTipoXml(filestream.Name);

                        if (restul == ETipoNota.NotaFiscalEletronica)
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(NfeProc));
                            StreamReader stringReader = new StreamReader(filestream.Name);
                            var nfes = (NfeProc)serializer.Deserialize(stringReader);

                            await _nfeRepository.CreateXmlNfe(nfes);

                        }
                    }
                }

                if (diretorioXml.Count > 0)
                {

                    if (Directory.Exists(_environment.ContentRootPath + "\\xml\\"))
                    {
                        System.IO.DirectoryInfo di = new DirectoryInfo(_environment.ContentRootPath + "\\xml\\");
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();

            }
        }
    }
}

