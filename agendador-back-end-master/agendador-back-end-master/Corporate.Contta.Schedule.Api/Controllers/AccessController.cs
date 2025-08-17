using Corporate.Contta.Schedule.Api.Validators;
using Corporate.Contta.Schedule.Application.Contracts.Repositories;
using Corporate.Contta.Schedule.Application.Mapping.Param;
using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Api.Controllers
{
    [Route("api/access")]
    //[ApiController]
    public class AccessController : Controller
    {
        readonly ILogger<AccessController> _log;
        private readonly IUserApplication _userApplication;

        public AccessController(ILogger<AccessController> log,
                                IConfiguration config,
                                IUserApplication userApplication)
        {
            _log = log;
            _userApplication = userApplication;
        }

        [Route("getaccesstoken")]
        [HttpPost()]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody]LoginUserRequest loginUser)
        {
            try
            {
                _log.LogInformation("Autenticando usuário: {@loginUser}", loginUser);

                var validator = await new LoginUserValidator().ValidateAsync(loginUser);
                if (!validator.IsValid)
                {
                    _log.LogError("Erro durante a validação dos dados {@newUser}", loginUser);

                    return BadRequest(validator.Errors);
                }

                var validateLoginResult = await _userApplication.GenerateAccessToken(new GenerateAccessTokenFilter(loginUser.Email, loginUser.Password));

                if (!validateLoginResult.Authorized)
                {
                    _log.LogWarning("Usuário não autorizado: {@loginUser}", loginUser);
                    return Unauthorized(401);
                }

                //TODO: Criar user result, pq está retornando o objeto de dominio
                var user = validateLoginResult.User;

                return Ok(new
                {
                    token = validateLoginResult.Token,
                    user
                });
            }
            catch (Exception ex)
            {                
                _log.LogError(ex, "Falha ao autenticar usuário: {@loginUser}", loginUser);
                return BadRequest(new { message = ex.Message ,loginUser});
            }            
        }


        [Route("getaccesstoken")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLogin(LoginUserRequest loginUser)
        {
            try
            {
                _log.LogInformation("Autenticando usuário: {@loginUser}", loginUser);

                var validator = await new LoginUserValidator().ValidateAsync(loginUser);
                if (!validator.IsValid)
                {
                    _log.LogError("Erro durante a validação dos dados {@newUser}", loginUser);

                    return BadRequest(validator.Errors);
                }

                var validateLoginResult = await _userApplication.GenerateAccessToken(new GenerateAccessTokenFilter(loginUser.Email, loginUser.Password));

                if (!validateLoginResult.Authorized)
                {
                    _log.LogWarning("Usuário não autorizado: {@loginUser}", loginUser);
                    return Unauthorized(401);
                }

                //TODO: Criar user result, pq está retornando o objeto de dominio
                var user = validateLoginResult.User;

                return Ok(new
                {
                    token = validateLoginResult.Token,
                    user
                });
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Falha ao autenticar usuário: {@loginUser}", loginUser);

                return BadRequest(new { message = ex.Message, loginUser });
            }
        }
    }
}