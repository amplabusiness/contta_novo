using AutoMapper;
using Corporate.Contta.Schedule.Application.Mapping.Dto.CompanyInformation;
using Corporate.Contta.Schedule.Application.Mapping.Request;
using Corporate.Contta.Schedule.Application.Mapping.Response;
using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Api.Controllers
{
    [Route("api/socio")]
    public class SociosController : Controller
    {
        private readonly ISociosRepository _sociosRepository;
        private readonly ICompanyRepository _companyRepository;
        readonly ILogger<SociosController> _log;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;    

        public SociosController(
                              ILogger<SociosController> log,
                              ISociosRepository sociosRepository,
                              IMapper mapper,
                              ICompanyRepository companyRepository,
                              IMediator mediator)
        {

            _log = log;
            _sociosRepository = sociosRepository;
            _mapper = mapper;
            _mediator = mediator;
            _companyRepository = companyRepository;
        }


        [Route("getSocios")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetInfomation(string nome, string cpf)
        {
            try
            {

                if (nome != null)
                {
                    var result = await _sociosRepository.GetAllSociosContta(cpf, nome);

                    var listSocios = _mapper.Map<List<SociosDto>>(result);

                    if(listSocios.Count == 0)
                    {
                        return BadRequest($"Não contem empresa vinculada a esse cpf={cpf}");
                    }

                    return Ok(listSocios);
                }
                else
                {
                    return BadRequest(new { message = "Dados Informado estão invalidos" });
                }
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
        public async Task<IActionResult> GetInfomationByDocument(string tokenUser, string cnpj)
        {
            try
            {
               
                if (tokenUser != null)
                {
                    var jwt = tokenUser;
                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadJwtToken(jwt).Payload.Values;
                    var userId = "";
                    foreach (var item in token)
                    {
                        userId = item.ToString();
                        break;
                    }
                    var request = new NewCompanySociosRequest{ Document = cnpj,UserId = userId };

                    var listEmpresa = _companyRepository.GetAllCompanySocios(userId, cnpj);

                    if(listEmpresa.Result != null)
                    {
                        return Ok(listEmpresa.Result);
                    }
                    else
                    {
                        var result = await _mediator.Send(request);
                        return Ok(result.Result);
                    }

                }
                else
                {
                    return BadRequest("Token Invalido");

                }
              
            }
            catch (Exception ex)
            {
                var erroMessage = "Erro ao preecadsatro empresa Socios";

                return BadRequest(erroMessage);
            }
        }

        [Route("newsocios")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetInfomationByDocument([FromBody] NewCompanySociosRequest request)
        {
            try
            {
                var jwt = request.Token;
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(jwt).Payload.Values;
                var userId = "";
                foreach (var item in token)
                {
                    userId = item.ToString();
                    break;
                }
                request.UserId = userId;

                var result = await _mediator.Send(request);  
                return Ok(result.Result);
            }
            catch (Exception ex)
            {
                var erroMessage = "Erro ao preecadsatro empresa Socios";        

                return BadRequest(erroMessage);
            }
        }
    }
}
