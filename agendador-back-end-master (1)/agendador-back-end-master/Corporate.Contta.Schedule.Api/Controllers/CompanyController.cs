using AutoMapper;
//using Corporate.Contta.Schedule.Api.Extension;
using Corporate.Contta.Schedule.Api.Validators;
using Corporate.Contta.Schedule.Application.Mapping.Request;
using Corporate.Contta.Schedule.Application.Mapping.Response;
using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities.CompanyInformationAgg;
using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using Corporate.Contta.Schedule.Infra.Repositories;
using Corporate.Contta.Schedule.Infra.Tools;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Api.Controllers
{
    //[Authorize("Bearer")]
    [Route("api/company")]
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _companyRepository;       
        readonly ILogger<CompanyController> _log;
        readonly IDiagnosticContext _diagnosticContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IUserContextAcessorRepository _userContextAcessorRepository;
      

        static int _callCount;

        public static IWebHostEnvironment _environment;

        public CompanyController(ICompanyRepository companyRepository,
                              ILogger<CompanyController> log,
                              IDiagnosticContext diagnosticContext,
                              IMapper mapper,
                              IMediator mediator,
                              IUserContextAcessorRepository userContextAcessorRepository,                             
                              IWebHostEnvironment environment)
        {
            _companyRepository = companyRepository;
           
            _log = log;
            _diagnosticContext = diagnosticContext ?? throw new ArgumentNullException(nameof(diagnosticContext));
            _mapper = mapper;
            _mediator = mediator;     
            _userContextAcessorRepository = userContextAcessorRepository;
            _environment = environment;
        }

        // [Authorize]
        [Route("getinfomation")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetInfomation(GetCompanyRequest company)
        {
            try
            {
                var result = await _mediator.Send(company);
                if (result.Errors.Any())
                    return BadRequest(new { message = result.Errors });

                return Ok(result.Result);
            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }

        }

        // [Authorize]
        [Route("getcompanydest")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetInfomationDest(GetCompanyRequest company)
        {
            try
            {
                var result = await _mediator.Send(company);
                if (result.Errors.Any())
                    return BadRequest(new { message = result.Errors });

                return Ok(result.Result);
            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }

        }

        // [Authorize]
        [Route("getanexo/{primaryActivity}")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetIAnexo(string primaryActivity)
        {
            try
            {
                CrawlerConsultaAnexo webs = new CrawlerConsultaAnexo();

                var cnae = await webs.GetAnexo(primaryActivity.Replace(".", ""));

                if (cnae == null)
                {
                    _log.LogError("Erro durante a pesquisa do anexo da empresa", primaryActivity);

                    Response responseErro = new Response();
                    return BadRequest(new { message = responseErro.Errors });
                }

                return Ok(cnae);
            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }

        }

        //[Authorize]
        [Route("getall/{token}")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll(string token)
        {
            try
            {
                ConvertTokenId convertTokenId = new ConvertTokenId();
                var userId = convertTokenId.GetTokenUserMaster(token);
                var getCompanyAll = new GetAllCompanyRequest();
                getCompanyAll.UserId = userId;
                var result = await _mediator.Send(getCompanyAll);
                if (result.Errors.Any())
                    return BadRequest(new { message = result.Errors });

                return Ok(result.Result);
            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }

        }

        //[Authorize]
        [Route("faturamento")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllFaturamento(Guid empresaId, DateTime dataAtual)
        {
            try
            {

                var resultFat = _companyRepository.GetAllFaturamentoEmpFechadas(empresaId, dataAtual);

                if(resultFat.Result != null && resultFat.Result.Faturamentos.Count > 0)
                {
                    if(resultFat.Result.Faturamentos.Count < 12)
                    {
                        resultFat.Result.FaturamentoFechado = false;

                      var resultFaturamento =  _companyRepository.GetAllFaturamentoEmpCompente(resultFat.Result, dataAtual);
                       return Ok(resultFaturamento.Result);
                    }

                    return Ok(resultFat.Result);
                  
                }
                else
                {
                    var result = _companyRepository.GetAllFaturamentoEmp(empresaId, dataAtual);

                    return Ok(result.Result);
                }
              
            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }
        }

        //[Authorize]
        [Route("getalldisabled")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllDisabled()
        {
            try
            {
                var getCompanyAllDisabled = new GetAllCompanyDisabledRequest();
                var result = await _mediator.Send(getCompanyAllDisabled);
                if (result.Errors.Any())
                    return BadRequest(new { message = result.Errors });

                return Ok(result.Result);
            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }
        }

        [Route("getinfomationbydocument/{document}")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetInfomationByDocument(string document)
        {
            try
            {
                var getCompanyRequest = new GetCompanyRequest { Document = document };

                //TODO: CRIA VALIDATOR DO MEDIATOR
                var validator = await new GetCompanyValidator().ValidateAsync(getCompanyRequest);

                if (!validator.IsValid)
                {
                    var error = "Documento inválido!";
                    _log.LogWarning($"{error} -- {getCompanyRequest.Document}");
                    Response responseError = new Response();
                    validator.Errors.ToList().ForEach(c => { responseError.AddError(c.ErrorMessage); });
                    return BadRequest(new { message = responseError.Errors });
                }

                var result = await _mediator.Send(getCompanyRequest);

                return Ok(result);
            }
            catch (Exception ex)
            {
                var erroMessage = "Deu um erro!";
                _log.LogError(ex, erroMessage);
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }

        }

        //[Authorize]
        [Route("newcompany")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> NewCompany([FromBody] NewCompanyRequest newCompany)
        {
            Response responseError;
            CrawlerConsultaAnexo webs = new CrawlerConsultaAnexo();

            try
            {
                if (newCompany == null)
                {
                    responseError = new Response();
                    responseError.AddError("Operação inválida.");
                    return BadRequest(new { message = responseError.Errors });
                }

                _log.LogInformation("Criando nova empresa: {@newCompany}", newCompany);

                var validator = await new CompanyValidator().ValidateAsync(newCompany);

                if (!validator.IsValid)
                {
                    _log.LogError("Erro durante a validação dos dados {@newCompany}", newCompany);

                    responseError = new Response();
                    validator.Errors.ToList().ForEach(c => { responseError.AddError(c.ErrorMessage); });
                    return BadRequest(new { message = responseError.Errors });
                }

                var result = await _mediator.Send(newCompany);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Erro ao tentar criar um nova empresa: {@newCompany}", newCompany);
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }
        }

        //[Authorize]
        [Route("updatecompany")]
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCompany([FromBody] UpdateCompanyRequest updateCompanyFaturamento)
        {
            Response responseErro;
            try
            {
                if (updateCompanyFaturamento == null)
                {
                    responseErro = new Response();
                    responseErro.AddError("Operação inválida.");
                    return BadRequest(new { message = responseErro.Errors });
                }
                _log.LogInformation("Alterando Empresa: {@updateCompany}", updateCompanyFaturamento);               

                var result = await _mediator.Send(updateCompanyFaturamento);
             

                return Ok(result);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Erro ao tentar alterar empresa: {@updateCompany}", updateCompanyFaturamento);
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }
        }

        //[Authorize]
        [Route("datacompany")]
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCompanyDataCompany(Guid empresaId, DateTime novaData)
        {
            IMongoTbSimples mongoTbSimples = new IMongoTbSimples();
            Response responseErro;
            try
            {
                var result = mongoTbSimples.UpdateData(empresaId, novaData);

                var auterarData = _companyRepository.UpdateDataCompany(empresaId, novaData);

                return Ok(result);
            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }
        }

        //[Authorize]
        [Route("faturamento")]
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCompany([FromBody] FaturamentoEmpresa updateCompanyFaturamento, bool fechado)
        {
            Response responseErro;
            try
            {
                if (updateCompanyFaturamento == null)
                {
                    responseErro = new Response();
                    responseErro.AddError("Operação inválida.");
                    return BadRequest(new { message = responseErro.Errors });
                }
                _log.LogInformation("Alterando Empresa: {@updateCompany}", updateCompanyFaturamento);

                var result = _companyRepository.UpdateFaturamentoEmpre(updateCompanyFaturamento, fechado);


                return Ok(result.Result);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Erro ao tentar alterar empresa: {@updateCompany}", updateCompanyFaturamento);
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }
        }

        //[Authorize]
        [Route("anexo")]
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCompanyAnexo([FromBody] List<Anexo> anexo, Guid empresaId)
        {
            Response responseErro;
            try
            {
                if (anexo.Count == 0)
                {
                    responseErro = new Response();
                    responseErro.AddError("Operação inválida.");
                    return BadRequest(new { message = responseErro.Errors });
                }

              var result = await _companyRepository.UpdateAnexo(anexo, empresaId);                

                return Ok(result);
            }
            catch (Exception ex)
            {                
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }
        }

        //[Authorize]
        //[Authorize(Roles = "Administrator")]
        [Route("deletecompany")]
        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] 
        public async Task<IActionResult> DeleteUser(DeleteCompanyRequest deleteCompanyRequest)
        {
            try
            {
                var validator = await new DeleteCompanyValidator().ValidateAsync(deleteCompanyRequest);
                _log.LogInformation("Deletando empresa código: {@deleteCompanyRequest.Id}", deleteCompanyRequest.Id);
                if (!validator.IsValid)
                {
                    Response responseErro = new Response();
                    validator.Errors.ToList().ForEach(c => { responseErro.AddError(c.ErrorMessage); });
                    _log.LogError("Erro durante a validação dos dados {@deleteCompanyRequest.Id}", deleteCompanyRequest);
                    return BadRequest(new { message = responseErro.Errors });
                }
                var result = await _mediator.Send(deleteCompanyRequest);
                if (result.Errors.Any())
                    return BadRequest(new { message = result.Errors });

                return Ok(result);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Erro ao tentar deletar empresa: {@deleteCompanyRequest.Id}", deleteCompanyRequest.Id);
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }
        }

        [Route("newCompanyList")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string> IntertCompany([FromForm] IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                try
                {
                    if (!Directory.Exists(_environment.ContentRootPath + "\\imagens\\"))
                    {
                        Directory.CreateDirectory(_environment.ContentRootPath + "\\imagens\\");
                    }
                    using (FileStream filestream = System.IO.File.Create(_environment.ContentRootPath + "\\imagens\\" + file.FileName))
                    {
                        await file.CopyToAsync(filestream);
                        filestream.Flush();
                        return "\\imagens\\" + file.FileName;
                    }
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
            else
                return "Ocorreu uma falha no envio do arquivo...";

            //ExelPackageConttaContta exelPackage = new ExelPackageConttaContta();

            //var listaCnpj = exelPackage.GetListaCnpj(file);

            //foreach (var cnpj in listaCnpj)
            //{
            //    try
            //    {
            //        var getInfomationByDocumentRequest = new GetInfomationByDocumentRequest { Document = "" };

            //        //TODO: CRIA VALIDATOR DO MEDIATOR
            //        var validator = await new GetInfomationByDocumentValidator().ValidateAsync(getInfomationByDocumentRequest);

            //        if (!validator.IsValid)
            //        {
            //            var error = "Documento inválido!";

            //            var responseError = new GetInfomationByDocumentResponse(error);

            //            _log.LogWarning($"{error} -- {getInfomationByDocumentRequest.Document}");

            //            //return BadRequest(responseError);
            //        }

            //        var result = await _mediator.Send(getInfomationByDocumentRequest);


            //    }
            //    catch (Exception ex)
            //    {
            //        var erroMessage = "Deu um erro!";
            //        _log.LogError(ex, erroMessage);

            //        var response = new GetInfomationByDocumentResponse(erroMessage);

            //        //return BadRequest(response);
            //    }
            //}

            //return "";
        }

        //[Authorize]
        [Route("newfaturamento12")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> NewFaturamento12([FromBody] NewFaturamento12Request newFaturamento12)
        {
            try
            {
                var faturamento12 = new FaturamentoEmpresaFechada { CompanyInformation = newFaturamento12.CompanyInformation, FaturamentoFechado = true, Faturamentos = newFaturamento12.Faturamentos};

                var result =_companyRepository.NewFaturamentoEmp(faturamento12);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Erro ao salvar faturamento de empresa: {@newFaturamento12}", newFaturamento12);
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }
        }

        [Route("putfaturamento12")]
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutFaturamento12([FromBody] FaturamentoEmpresa putFaturamento12)
        {
            try
            {
                var result = await _companyRepository.UpdateFaturamentoCorreto(putFaturamento12);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


        //[Authorize]
        [Route("getConfig")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetConfig(Guid UserAdminId)
        {

            try
            {
                var result = await _companyRepository.GetConfigurationAdmin(UserAdminId);

                return Ok(result);
            }
            catch (Exception ex)
            {
               
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }
        }

        //[Authorize]
        [Route("updateConfig")]
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateConfig(Guid UserId, Guid autorizationAdminId, bool desativar)
        {

            try
            {
                var result = await _companyRepository.UpdateConfigurationAdmin(UserId, autorizationAdminId, desativar);

                return Ok(result);
            }
            catch (Exception ex)
            {

                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }
        }
    }
}

