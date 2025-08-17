using Corporate.Contta.Schedule.Application.Mapping.Request;
using Corporate.Contta.Schedule.Application.Mapping.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Api.Controllers
{
    [Route("api/agblocoe")]
    public class AgBlocoEController : Controller
    {
        private readonly IMediator _mediator;
        public AgBlocoEController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([FromBody] AgBlocoERequest request)
        {
            Response responseError;
            try
            {               
                if (request.CompanyInformationId == Guid.Empty)
                {
                    responseError = new Response();
                    responseError.AddError("Operação inválida.");
                    return BadRequest(new { message = responseError.Errors });
                }

                var result = await _mediator.Send(request);

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
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAgBlocoE(AgBlocoEGetRequest getAgBlocoGet)
        {
            try
            { 
                var result = await _mediator.Send(getAgBlocoGet);
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
