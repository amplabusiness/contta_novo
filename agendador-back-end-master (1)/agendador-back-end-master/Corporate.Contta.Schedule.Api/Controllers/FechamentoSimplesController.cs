using AutoMapper;
using Corporate.Contta.Schedule.Application.Mapping;
using Corporate.Contta.Schedule.Application.Mapping.Request;
using Corporate.Contta.Schedule.Application.Mapping.Response;
using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities.ConfigurationFhAgg;
using Corporate.Contta.Schedule.Infra.Tools;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Api.Controllers
{
    [Route("api/configurationfh")]
    public class FechamentoSimplesController : Controller
    {
        private readonly ILogger<FechamentoSimplesController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IConfigurationFhRepository _configurationFhRepository;

        public FechamentoSimplesController(ILogger<FechamentoSimplesController> logger, IMapper mapper, IMediator mediator, IConfigurationFhRepository configurationFhRepository)
        {
            _logger = logger;
            _mediator = mediator;
            _configurationFhRepository = configurationFhRepository;
            _mapper = mapper;
        }

        [HttpPatch]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> NewFechamentoSimplesFh([FromBody] NewConfigurationFhRequest configurationFh)
        {
            Response responseError;
            try
            {
                if (configurationFh == null)
                {
                    responseError = new Response();
                    responseError.AddError("Operação inválida.");
                    return BadRequest(new { message = responseError.Errors });
                }

                var configurationDto = _configurationFhRepository.Get(configurationFh.CompanyInformation);
                if (configurationDto.Result == null)
                {
                    var result = await _mediator.Send(configurationFh);

                    return Ok(result);
                }
                else
                {
                    var configurationFhDto = _mapper.Map<ConfigurationFh>(configurationFh);
                    var result = _configurationFhRepository.Update(configurationFhDto);

                    if (result.Result != null)
                        return Ok(result.Result);
                    else
                        return BadRequest("Não foi possivel efetuar alteração,Tente novamente");

                }
            }
            catch (Exception ex)
            {
                responseError = new Response();
                responseError.AddError("Operação inválida.");
                return BadRequest(new { message = responseError.Errors, ex });
            }
        }

        //[Route("empresaId/{empresaId}")]    
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetConfigurationF(string empresaId)
        {
            try
            {
                var getConfigurationFh = new GetConfigurationFhUserRequest() { Id = new Guid(empresaId) };

                var result = await _mediator.Send(getConfigurationFh);
                if (result.Errors == null)
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
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateConfiguratio([FromBody] UpdateConfigurationFhRequest updateConfiguration)
        {
            Response responseErro;
            try
            {
                if (updateConfiguration == null)
                {
                    responseErro = new Response();
                    responseErro.AddError("Operação inválida.");
                    return BadRequest(new { message = responseErro.Errors });
                }
                // _log.LogInformation("Alterando Empresa: {@updateCompany}", updateNotification);

                //var result = await _mediator.Send(updateConfiguration);

                var configurationFhDto = _mapper.Map<ConfigurationFh>(updateConfiguration);

                var result = _configurationFhRepository.Update((configurationFhDto));

                if (result.Result.Equals("Erro ao realizar operação."))
                {
                    responseErro = new Response();
                    responseErro.AddError("Operação inválida.");
                    return BadRequest(new { message = responseErro.Errors });
                }

                if (result.Result != null)
                    return Ok(result.Result);
                else
                   return BadRequest("Não foi Possivel Efetuar A requisição!");
            }
            catch (Exception ex)
            {
                // _log.LogError(ex, "Erro ao tentar alterar empresa: {@updateCompany}", updateCompany);
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }
        }
    }
}
