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
    [Route("api/notification")]
    public class NotificationController : Controller
    {
        private readonly ILogger<NotificationController> _logger;
        private readonly IMediator _mediator;
        private readonly INotificationUserRepository _notificationRepository;

        public NotificationController(ILogger<NotificationController> logger, IMediator mediator, INotificationUserRepository notificationRepository)
        {
            _logger = logger;
            _mediator = mediator;
            _notificationRepository = notificationRepository;
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> NewNotification([FromBody] NewNotificationRequest notificatin )
        {
            Response responseError;
            try
            {
                if (notificatin == null)
                {
                    responseError = new Response();
                    responseError.AddError("Operação inválida.");
                    return BadRequest(new { message = responseError.Errors });
                }

                var result = await _mediator.Send(notificatin);

                return Ok(result);
            }
            catch (Exception ex)
            {  
                responseError = new Response();
                responseError.AddError("Operação inválida.");
                return BadRequest(new { message = responseError.Errors,ex });
            }
        }

        // [Authorize]        
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllInfomation(GetAllNotificationRequest getAllNotification)
        {
            try
            {
               
                var result = await _mediator.Send(getAllNotification);
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
        public async Task<IActionResult> UpdateNotification([FromBody] UpdateNotificationRequest updateNotification)
        {
            Response responseErro;
            try
            {
                if (updateNotification == null)
                {
                    responseErro = new Response();
                    responseErro.AddError("Operação inválida.");
                    return BadRequest(new { message = responseErro.Errors });
                }
               // _log.LogInformation("Alterando Empresa: {@updateCompany}", updateNotification);

                var result = await _mediator.Send(updateNotification);

                if (result.Result.Equals("Erro ao realizar operação."))
                {
                    responseErro = new Response();
                    responseErro.AddError("Operação inválida.");
                    return BadRequest(new { message = responseErro.Errors });
                }

                return Ok(result);
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
