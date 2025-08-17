using Corporate.Contta.Schedule.Application.Mapping;
using Corporate.Contta.Schedule.Application.Mapping.Request;
using Corporate.Contta.Schedule.Application.Mapping.Response;
using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Infra.Tools;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Api.Controllers
{
    [Route("api/criticas")]
    public class CriticasController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ICriticasRepository _criticasRepository;

        public CriticasController(IMediator mediator, ICriticasRepository criticasRepository)
        {
            _mediator = mediator;
            _criticasRepository = criticasRepository;
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> NewCriticas([FromBody] NewCriticasNovasRequest newCriticasNovas)
        {
            Response responseError;
            try
            {

                if (newCriticasNovas == null)
                {
                    responseError = new Response();
                    responseError.AddError("Operação inválida.");
                    return BadRequest(new { message = responseError.Errors });
                }
                newCriticasNovas.DataCadastro = DateTime.Now;
                var result = await _mediator.Send(newCriticasNovas);

                return Ok(result);
            }
            catch (Exception ex)
            {
                responseError = new Response();
                responseError.AddError("Operação inválida.");
                return BadRequest(new { message = responseError.Errors, ex });
            }
        }


        // [Authorize]  
        [Route("criticasnovas")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllCriticasNovas(GetCriticasNovasRequest getCriticasNovas)
        {
            try
            {
                var result = await _mediator.Send(getCriticasNovas);
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

    }
}
