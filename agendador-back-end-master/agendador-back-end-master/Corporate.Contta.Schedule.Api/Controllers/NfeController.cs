using AutoMapper;
using Corporate.Contta.Schedule.Api.Validators;
using Corporate.Contta.Schedule.Application.Mapping.Dto.Nfe;
using Corporate.Contta.Schedule.Application.Mapping.Request;
using Corporate.Contta.Schedule.Application.Mapping.Response;
using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using Corporate.Contta.Schedule.Infra.Tools;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using WebGerarPDF;

namespace Corporate.Contta.Schedule.Api.Controllers
{
    //[Authorize("Bearer")]
    [Route("api/nfe")]
    public class NfeController : Controller
    {
        private readonly INfeRepository _nfeRepository;
        readonly ILogger<CompanyController> _log;
        readonly IDiagnosticContext _diagnosticContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IUserContextAcessorRepository _userContextAcessorRepository;

        static int _callCount;

        public NfeController(INfeRepository nfeRepository,
                              ILogger<CompanyController> log,
                              IDiagnosticContext diagnosticContext,
                              IMapper mapper,
                              IMediator mediator,
                              IUserContextAcessorRepository userContextAcessorRepository)
        {
            _nfeRepository = nfeRepository;
            _log = log;
            _diagnosticContext = diagnosticContext ?? throw new ArgumentNullException(nameof(diagnosticContext));
            _mapper = mapper;
            _mediator = mediator;
            _userContextAcessorRepository = userContextAcessorRepository;
        }

        //[Authorize]
        //[Route("Id")]
        //[HttpGet]
        //[Consumes(MediaTypeNames.Application.Json)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> GetInfomation(GetnfeRequest getnfeRequest)
        //{
        //    try
        //    {
        //        //var validator = await new GetInformationUserValidator().ValidateAsync(getnfeRequest);

        //        //if (!validator.IsValid)
        //        //{
        //        //    _log.LogError("Erro durante a validação dos dados {@getInformationUserRequest}", getnfeRequest);

        //        //    Response responseErro = new Response();
        //        //    validator.Errors.ToList().ForEach(c => { responseErro.AddError(c.ErrorMessage); });
        //        //    return BadRequest(new { message = responseErro.Errors });
        //        //}

        //        var result = await _mediator.Send(getnfeRequest);
        //        if (result.Errors.Any())
        //            return BadRequest(new { message = result.Errors });

        //        return Ok(result.Result);
        //    }
        //    catch (Exception ex)
        //    {
        //        var response = new Response().AddError(ex.Message);
        //        return BadRequest(new { message = response.Errors });
        //    }

        //}

        //[Authorize]


        [Route("gettotalizadornfesaida")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll(GetTotalizadorNfeSaidaRequest request)
        {
            try
            { 
                var result = await _mediator.Send(request);
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

        [Route("getall")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll(GetAllNfeRequest request)
        {
            try
            {
                var getNfeAll = new GetAllNfeRequest();

                var result = await _mediator.Send(request);
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

        [Route("getallNfeLivro")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllNfe(Guid empresaId, DateTime dataOperacao, string tipoFiscal)
        {
            try
            {
                GerarLivroFiscal geradorPdf = new GerarLivroFiscal();

                var result = _nfeRepository.GetAllNfe(empresaId, dataOperacao, tipoFiscal);

                var livroFiscal = result.Item1;
                var livroFiscal1 = result.Item2;

                if (tipoFiscal == "Venda")
                {
                    var restukPdf = geradorPdf.GerarRelatorioSaida(livroFiscal, livroFiscal1);
                    return File(restukPdf.MemoryStream, restukPdf.ContantType, restukPdf.NamePdfFile);

                }
                if (tipoFiscal == "Entrada")
                {
                    var restukPdfEntarda = geradorPdf.GerarRelatorio_Entrada();
                    return File(restukPdfEntarda.MemoryStream, restukPdfEntarda.ContantType, restukPdfEntarda.NamePdfFile);

                }

                return BadRequest("Tipo Nota Fiscal Invalido");

            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }
        }

        [Route("getallTbE")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllTabelaE(Guid empresaId, string tipoFiscal)
        {
            try
            {
                GerarLivroFiscal geradorPdf = new GerarLivroFiscal();

                var result = _nfeRepository.GetAllBlocoE(empresaId, tipoFiscal);

                return Ok(result.Result);

            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }
        }

        //[Authorize]
        [Route("getallNfe")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllNfe(GetAllFullNfeRequest request)
        {
            try
            {
                var getNfeAll = new GetAllFullNfeRequest();

                var result = await _mediator.Send(request);
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
        [Route("getallNfeT")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllNfeT(GetAllNfeTRequest request)
        {
            try
            {
                var getNfeAll = new GetAllNfeTRequest();

                var result = await _mediator.Send(request);
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

        [Route("getallNfeMod57")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllNfeMod57(GetAllNfeMod57Request request)
        {
            try
            {
                var getNfeAll = new GetAllNfeMod57Request();

                var result = await _mediator.Send(request);
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

        [Route("getallNfeServico")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllNfeModServico(GetAllNfeServicoRequest request)
        {
            try
            {
                var getNfeAll = new GetAllNfeServicoRequest();

                var result = await _mediator.Send(request);
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

        [Route("getallNfeServmanual")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllNfeModServicoManual(Guid CompanyInformation)
        {
            try
            {
                var result = _nfeRepository.GetAllNfeManualServi(CompanyInformation);

                return Ok(result);
            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }

        }

        [Route("getallNfeVendamanual")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllNfeModVendaManual(Guid CompanyInformation)
        {
            try
            {
                var result = _nfeRepository.GetAllNfeManualVenda(CompanyInformation);

                return Ok(result.Result);
            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }

        }

        //[Authorize]
        [Route("GetRegistrosBlE")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRegistrosBlE(GetRegistrosBlERequest request)
        {
            try
            {
                var getNfeAll = new GetRegistrosBlERequest();

                var result = await _mediator.Send(request);
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

        [Route("GetRegistrosBlE")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> NewRegistrosBlETabela([FromBody] List<TbDeducao> tbDeducao, double valorSaldoDevedor)
        {
            try
            {
                var getNfeAll = new GetRegistrosBlERequest();

                var result = await _nfeRepository.InsertTbAjuste(tbDeducao, valorSaldoDevedor);              

                return Ok(result);
            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }

        }

        //RegistroE110


        //[Authorize]
        [Route("getallapuracao")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllApuracao(ApuracaoRequest request)
        {
            try
            {
                var getallApuracao = new ApuracaoRequest();

                var result = await _mediator.Send(request);
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
        [Route("getbasecalculo")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBaseCalculo(GetBasCalculoRequest request)
        {
            try
            {
                var getBaseCalculo = new GetBasCalculoRequest();

                var result = await _mediator.Send(request);
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
        [Route("tabelaAnexo")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTabelaExterna(string url)
        {
            CrawlerAliquota crawlerAliquota = new CrawlerAliquota();
            CrawlerConsultaNcm crawlerConsultaNcm = new CrawlerConsultaNcm();

            try
            {
                //var atualizar = await crawlerAliquota.GetAnexo(url);
                var atualizar = await crawlerConsultaNcm.GetAnexo();

                return Ok(atualizar);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Erro ao tentar criar um nova empresa: {@newCompany}");
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

        [Route("tbservico")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllNfeModServico()
        {
            try
            {
                var getNfeAll = new GetAllNfeServicoRequest();

                var result = _nfeRepository.GetAllCodServico();

                var tbServico = _mapper.Map<List<TbServicoDto>>(result.Result);

                return Ok(tbServico.ToList());
            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }

        }

        //[Authorize]
        [Route("updatenfeajuste")]
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCompanyAjuste([FromBody] UpdateNfeAjusteRequest updateNfeAjuste)
        {
            Response responseErro;
            try
            {
                if (updateNfeAjuste == null)
                {
                    responseErro = new Response();
                    responseErro.AddError("Operação inválida.");
                    return BadRequest(new { message = responseErro.Errors });
                }

                _log.LogInformation("Alterando Empresa: {@updateCompany}", updateNfeAjuste);

                var result = await _mediator.Send(updateNfeAjuste);
                if (result.Errors.Any())
                    return BadRequest(new { message = result.Errors });

                return Ok(result.Result);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Erro ao tentar alterar empresa: {@updateCompany}", updateNfeAjuste);
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }
        }
        //[Authorize]
        [Route("updatenfe")]
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCompany([FromBody] UpdateCompanyRequest updateCompany)
        {
            Response responseErro;
            try
            {
                if (updateCompany == null)
                {
                    responseErro = new Response();
                    responseErro.AddError("Operação inválida.");
                    return BadRequest(new { message = responseErro.Errors });
                }
                _log.LogInformation("Alterando Empresa: {@updateCompany}", updateCompany);

                //var validator = await new CompanyValidator().ValidateAsync(updateCompany);

                //if (!validator.IsValid)
                //{
                //    responseErro = new Response();
                //    validator.Errors.ToList().ForEach(c => { responseErro.AddError(c.ErrorMessage); });
                //    _log.LogError("Erro durante a validação dos dados {@updateCompany}", updateCompany);
                //    return BadRequest(new { message = responseErro.Errors });
                //}

                var result = await _mediator.Send(updateCompany);
                if (result.Errors.Any())
                    return BadRequest(new { message = result.Errors });

                return Ok(result);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Erro ao tentar alterar empresa: {@updateCompany}", updateCompany);
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }
        }

        //[Authorize]
        //[Authorize(Roles = "Administrator")]
        [Route("deletenfe")]
        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteCompanyRequest deleteCompanyRequest)
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

        [Route("getallbycompany")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll(GetAllByIdCompanyRequest request)
        {
            try
            {
                if (request.Id == Guid.Empty || request.Id == null)
                    return BadRequest(new { message = "Dados de pesquisa inválidos." });
                var result = await _mediator.Send(request);
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

        [Route("getbyid")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(GetByIdNfeRequest request)
        {
            try
            {
                if (request.NfeId == Guid.Empty || request.NfeId == null)
                    return BadRequest(new { message = "Dados de pesquisa inválidos." });
                var result = await _mediator.Send(request);
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

        [Route("consultanfe")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByFilter(FilterNfeRequest request)
        {
            try
            {
                if (request.CompanyId == Guid.Empty || string.IsNullOrEmpty(request.TipoNfe))
                    return BadRequest(new { message = "Dados de pesquisa inválidos." });
                var result = await _mediator.Send(request);
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

        [Route("canceladas")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByFilterCanceladas(FilterNfeCanceladaRequest request)
        {
            try
            {
                if (request.CompanyId == Guid.Empty || string.IsNullOrEmpty(request.TipoNfe))
                    return BadRequest(new { message = "Dados de pesquisa inválidos." });
                var result = await _mediator.Send(request);
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



        [Route("desativarnota")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DesativarNota(DesativarNota request)
        {
            try
            {
                if (request.Id == Guid.Empty || request.Id == null)
                    return BadRequest(new { message = "Dados de pesquisa inválidos." });
                var result = await _mediator.Send(request);
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

        [Route("ajusteBlocoE")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> NewAjusteNfe([FromBody] NewAjusteNfeRequest newAjusteNfe)
        {
            Response responseError;
            try
            {
                if (newAjusteNfe == null)
                {
                    responseError = new Response();
                    responseError.AddError("Operação inválida.");
                    return BadRequest(new { message = responseError.Errors });
                }
                var result = await _mediator.Send(newAjusteNfe);

                return Ok(result.Result);
            }
            catch (Exception ex)
            {
                responseError = new Response();
                responseError.AddError("Operação inválida.");
                return BadRequest(new { message = responseError.Errors, ex });
            }
        }

        [Route("vendamanual")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> NewNfVendaManual([FromBody] NewNfVendaManualRequest nfVenda)
        {
            Response responseError;
            try
            {
                if (nfVenda == null)
                {
                    responseError = new Response();
                    responseError.AddError("Operação inválida.");
                    return BadRequest(new { message = responseError.Errors });
                }
                var result = await _mediator.Send(nfVenda);

                return Ok(result);
            }
            catch (Exception ex)
            {
                responseError = new Response();
                responseError.AddError("Operação inválida.");
                return BadRequest(new { message = responseError.Errors, ex });
            }
        }

        [Route("servicomanual")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> NewNfServicoManual([FromBody] NewNfServicoManualRequest nfServico)
        {
            Response responseError;
            try
            {
                if (nfServico == null)
                {
                    responseError = new Response();
                    responseError.AddError("Operação inválida.");
                    return BadRequest(new { message = responseError.Errors });
                }
                var result = await _mediator.Send(nfServico);

                return Ok(result);
            }
            catch (Exception ex)
            {
                responseError = new Response();
                responseError.AddError("Operação inválida.");
                return BadRequest(new { message = responseError.Errors, ex });
            }
        }



        [Route("deleteNfeServmanual")]
        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteNfeModServicoManual(Guid id)
        {
            try
            {
                var result = _nfeRepository.DeleteNfeManualServi(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }

        }

        [Route("deleteNfeVendamanual")]
        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteNfeModVendaManual(Guid id)
        {
            try
            {
                var result = _nfeRepository.DeleteNfeManualVenda(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }
        }

        [Route("livroSaida")]
        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLivroSaida(Guid empresaId, DateTime dhEmissao, string tipoOperacao)
        {
            try
            {
                var result = _nfeRepository.GetAllNfe(empresaId, dhEmissao, tipoOperacao);

                var base64 = "";

                return Ok(base64);


            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }
        }
        [Route("livroEntrada")]
        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLivroEntarda(Guid empresaId, DateTime dhEmissao, string tipoOperacao)
        {
            try
            {
                var result = _nfeRepository.GetAllNfe(empresaId, dhEmissao, tipoOperacao);

                var base64 = "";

                return Ok(base64);


            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }
        }
    }
}

