using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Corporate.Contta.Schedule.Application.Mapping.Dto.Dashboard;
using Corporate.Contta.Schedule.Application.Mapping.Dto.Dashboard.Apuracao;
using Corporate.Contta.Schedule.Application.Mapping.Request;
using Corporate.Contta.Schedule.Application.Mapping.Response;
using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Infra.Repositories;
using Corporate.Contta.Schedule.Infra.Tools;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Corporate.Contta.Schedule.Api.Controllers
{
    [Route("api/dashboard")]
    public class DashboardController : Controller
    {
        private IMediator _mediator;
        public IMapper _mapper;
        private IApuracaoRepository _apuracaoRepository;
        public DashboardController(IMapper mapper, IMediator mediator, IApuracaoRepository apuracaoRepository)
        {
            _mediator = mediator;
            _mapper = mapper;
            _apuracaoRepository = apuracaoRepository;
        }

        [Route("getbycompany")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByCompany(DashboardRequest request)
        {
            try
            {
                DashboardRepository _dashboardRepository = new DashboardRepository();
                var resultDto = _dashboardRepository.GetByCompany(request.EmpresaId, request.RequestDate);

                var result = new Response(_mapper.Map<DashboardDto>(resultDto));
            

                return Ok(result.Result);
            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }
        }

        [Route("getAllHome")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllHome(HomeRequest request)
        {
            try
            {
                ConvertTokenId convertTokenId = new ConvertTokenId();
                if (string.IsNullOrEmpty(request.Token))
                    return BadRequest(new { message = "Dados de pesquisa inválidos." });
                request.UserId = new Guid(convertTokenId.GetTokenUserMaster(request.Token));

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

        [Route("detalhamentoApuracaoicmspisnfe")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDetalhamentoApuracaoIcmsPisNfe(DetalhamentoApuracaoIcmsPisNfeRequest request)
        {
            try
            {
                if (request.Ids == null || string.IsNullOrEmpty(request.Tipo) || string.IsNullOrEmpty(request.Grupo))
                    return BadRequest(new { message = "Dados de pesquisa inválidos." });

                var result = await _mediator.Send(request);

                if (result.Errors.Any())
                    return BadRequest(new { message = result.Errors });
                var apuracao = _mapper.Map<AgrupamentoDetalhamentoApuracaoDto>(result.Result);
                if (request.Grupo == "IcmsStPisConfins")
                    return Ok(apuracao.IcmsStPisConfinsDetalhamentoApuracao);
                else if (request.Tipo == "Cancelada")
                    return Ok(apuracao.NotaFiscalCanceladaDetalhamentoApuracao);
                else
                return Ok(apuracao.DevolucaoTransferenciaDetalhamentoApuracao);
            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }
        }
        [Route("detalhamentoApuracao")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDetalhamentoApuracaa(DetalhamentoApuracaoRequest request)
        {
            try
            {
                if (Guid.Empty == request.CompanyId)
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
        [Route("impdashboard")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDetalhamentoImposto(DetalhamentoImpostoRequest request)
        {
            try
            {
                if (Guid.Empty == request.CompanyId)
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

        [Route("newHome")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> NewHome(double aliquota, Guid empresaId, double fat12Messes, DateTime dataEmissao)
        {
            try
            {
               var result  = _apuracaoRepository.NewHome(aliquota, fat12Messes, empresaId, dataEmissao);

                return Ok(result.Result);
            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }
        }

        [Route("gethome")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetHome(string token, DateTime dataOperacao)
        {
            try
            {
                ConvertTokenId convertTokenId = new ConvertTokenId();
                var userId = convertTokenId.GetTokenUserMaster(token);

                var result = _apuracaoRepository.GetHome(userId, dataOperacao);


                return Ok(result.Result);
            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }
        }
    }
}
