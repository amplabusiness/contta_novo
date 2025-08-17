using Corporate.Contta.Schedule.Api.Validators;
using Corporate.Contta.Schedule.Application.Mapping.Request;
using Corporate.Contta.Schedule.Application.Mapping.Result.GetInfomationByDocument;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Collections.Generic;
using Corporate.Contta.Schedule.Domain.Entities;
using Corporate.Contta.Schedule.Infra.Tools;

namespace Corporate.Contta.Schedule.Api.Controllers
{
    [Route("api/information")]
    public class InformationController : Controller
    {
        private readonly ILogger<InformationController> _logger;
        private readonly IMediator _mediator;

        public InformationController(ILogger<InformationController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }        
        
        [Route("company/{cnpj}/{tokenUser}")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetInfomationByDocument(string cnpj,string tokenUser,bool confirmarCadastro,Guid? userIdTerceiro)
        {
            try
            {
                var getInfomationByDocumentRequest = new GetInfomationByDocumentRequest { Document = cnpj, UserId = tokenUser, ConfirmarCadastro = confirmarCadastro,UserIdTerceiro = userIdTerceiro };

                var validator = await new GetInfomationByDocumentValidator().ValidateAsync(getInfomationByDocumentRequest);

                getInfomationByDocumentRequest.UserId = tokenUser;

                if (!validator.IsValid)
                {
                    var error = "Documento inválido!";

                    var responseError = new GetInfomationByDocumentResponse(error);

                    _logger.LogWarning($"{error} -- {getInfomationByDocumentRequest.Document}");

                    return BadRequest(responseError);
                }

                var result = await _mediator.Send(getInfomationByDocumentRequest);     
                
                if(result.CompanyInformation.EmpresaCadastrada)
                {
                   return BadRequest();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                var erroMessage = "Empresa já esta cadastrada para outro Usuário Admin";
                if (ex.Message.Contains("Empresa já esta cadastrada para outro Usuário Admin"))
                {                   
                    return BadRequest(erroMessage);
                }     

                var response = new GetInfomationByDocumentResponse($"{erroMessage}+{ex}");

                return BadRequest(response);
            }
        }

        [Route("companyDest")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetInfomationByDocumentDest(string cnpj, string tokenUser, bool Manual)
        {
            try
            {
                var getInfomationByDocumentRequest = new GetInfomationByDocumentManualRequest { Document = cnpj, UserId = tokenUser, Manual = Manual };

                getInfomationByDocumentRequest.UserId = tokenUser;

                var result = await _mediator.Send(getInfomationByDocumentRequest);

                return Ok(result);
            }
            catch (Exception ex)
            {
                var erroMessage = "Deu um erro!";
                _logger.LogError(ex, erroMessage);

                var response = new GetInfomationByDocumentResponse(erroMessage);

                return BadRequest(response);
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> NewCompany([FromBody] List<string> listCnpj)
        {
            try
            {
              
                var result = await _mediator.Send(listCnpj);
                          

                return Ok(result);
            }
            catch (Exception ex)
            {
                var erroMessage = "Deu um erro!";
                _logger.LogError(ex, erroMessage);

                var response = new GetInfomationByDocumentResponse(erroMessage);

                return BadRequest(response);
            }
        }
                
    }
}
