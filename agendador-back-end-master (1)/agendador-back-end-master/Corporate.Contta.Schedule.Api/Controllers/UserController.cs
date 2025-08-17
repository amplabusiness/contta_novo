using AutoMapper;
using Corporate.Contta.Schedule.Api.Validators;
using Corporate.Contta.Schedule.Application.Contracts.Repositories;
using Corporate.Contta.Schedule.Application.Mapping.Param;
using Corporate.Contta.Schedule.Application.Mapping.Request;
using Corporate.Contta.Schedule.Application.Mapping.Response;
using Corporate.Contta.Schedule.Application.Mapping.Response.UserResponse;
using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities.UserAgg;
using Corporate.Contta.Schedule.Infra.Tools;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Linq;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Api.Controllers
{
    //[Authorize("Bearer")]
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        readonly ILogger<UserController> _log;
        readonly IDiagnosticContext _diagnosticContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IUserContextAcessorRepository _userContextAcessorRepository;
        private readonly IConfigurationUserRepository _configurationUserRepository;
        private readonly IUserApplication _userApplication;

        static int _callCount;

        public UserController(IUserRepository userRepository,
                              ILogger<UserController> log,
                              IDiagnosticContext diagnosticContext,
                              IMapper mapper,
                              IMediator mediator,
                              IUserContextAcessorRepository userContextAcessorRepository,
                              IConfigurationUserRepository configurationUserRepository,
                              IUserApplication userApplication)
        {
            _userRepository = userRepository;
            _log = log;
            _diagnosticContext = diagnosticContext ?? throw new ArgumentNullException(nameof(diagnosticContext));
            _mapper = mapper;
            _mediator = mediator;
            _userContextAcessorRepository = userContextAcessorRepository;
            _configurationUserRepository = configurationUserRepository;
            _userApplication = userApplication;
        }

        [Route("getUser")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetInfomation(GetUserRequest getInformationUserRequest)
        {
            try
            {

                if (getInformationUserRequest.Id != null)
                {
                    var result = await _mediator.Send(getInformationUserRequest);
                    if (result.Errors.Any())
                        return BadRequest(new { message = result.Errors });

                    return Ok(result.Result);
                }
                else
                {
                    return BadRequest(new { message = "Não foi possivel localizar o MasterId,Tente Novamente" });
                }
            }
            catch (Exception ex)
            {
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="getInformationUserRequest"></param>
        /// <returns></returns>

        //[Authorize]
        [Route("getinfomation")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetInfomation(GetInformationUserRequest getInformationUserRequest)
        {
            try
            {
                ConvertTokenId convertTokenId = new ConvertTokenId();
                var userId = convertTokenId.GetTokenUserMaster(getInformationUserRequest.Token);

                if(userId == "" || userId == null)
                {
                    return BadRequest(new { message = "Token informado invalido"});
                }

                getInformationUserRequest.Id = new Guid(userId);

                //var validator = await new GetInformationUserValidator().ValidateAsync(getInformationUserRequest);

                //if (!validator.IsValid)
                //{
                //    _log.LogError("Erro durante a validação dos dados {@getInformationUserRequest}", getInformationUserRequest);

                //    Response responseErro = new Response();
                //    validator.Errors.ToList().ForEach(c => { responseErro.AddError(c.ErrorMessage); });
                //    return BadRequest(new { message = responseErro.Errors });
                //}

                var result = await _mediator.Send(getInformationUserRequest);
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
        [Route("getinfomationcompany")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetInfomationCompany(GetCompanySummaryInformationRequest getCompanySummaryInformationRequest)
        {
            try
            {
                var validator = await new GetCompanySummaryInformationValidator().ValidateAsync(getCompanySummaryInformationRequest);

                if (!validator.IsValid)
                {
                    _log.LogError("Erro durante a validação dos dados {@getCompanySummaryInformationRequest}", getCompanySummaryInformationRequest);

                    Response responseErro = new Response();
                    validator.Errors.ToList().ForEach(c => { responseErro.AddError(c.ErrorMessage); });
                    return BadRequest(new { message = responseErro.Errors });
                }

                var result = await _mediator.Send(getCompanySummaryInformationRequest);
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
        [Route("newuser")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> NewUser([FromBody] NewUserRequest newUser)
        {
            try
            {
                if (newUser.TokenAcesso != "")
                {
                    var tokenAcesso = new TokenAcesso()
                    {
                        TokenAcess = newUser.TokenAcesso,
                        UserId = null
                    };

                    var validadorToken = _userRepository.GetToken(tokenAcesso);

                    if (validadorToken.Result == null)
                    {
                        return BadRequest("Token Informado não é valido");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Token Informado não é valido{ex}.");
            }

            if (Domain.Enum.UserGroup.User == newUser.Group)
            {
                var t = _userContextAcessorRepository.Id;
                var configurationUser = new NewConfigurationUserRequest();

                ConvertTokenId convertTokenId = new ConvertTokenId();
                var userId = convertTokenId.GetTokenUserMaster(newUser.Token);
                newUser.UserMasterId = userId;
            }

            try
            {
                _log.LogInformation("Criando novo usuário: {@newUser}", newUser);

                var validator = await new NewUserValidator().ValidateAsync(newUser);

                if (!validator.IsValid)
                {
                    _log.LogError("Erro durante a validação dos dados {@newUser}", newUser);

                    Response responseError = new Response();
                    validator.Errors.ToList().ForEach(c => { responseError.AddError(c.ErrorMessage); });
                    return BadRequest(new { message = responseError.Errors });
                }

                var result = await _mediator.Send(newUser);

                if (Domain.Enum.UserGroup.Administrator == newUser.Group && newUser.UserComum == false)
                {
                    var userDto = _mapper.Map<User>(newUser);
                    await _userRepository.UpdateTokenAcess(userDto);
                }

                if (result.Errors.Any())
                    return BadRequest(new { message = result.Errors });

                return Ok(result);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Erro ao tentar criar um novo usuário: {@newUser}", newUser);
                var response = new Response().AddError(ex.Message);
                return BadRequest($"Usuário Já Cadastrado na Nossa Base={ex}");
            };
        }

        //[Authorize]
        [Route("updateuser")]
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest updateUser)
        {
            try
            {
                _log.LogInformation("Alterando usuário: {@updateUser}", updateUser);

                var validator = await new UpdateUserValidator().ValidateAsync(updateUser);

                if (!validator.IsValid)
                {
                    Response responseErro = new Response();
                    validator.Errors.ToList().ForEach(c => { responseErro.AddError(c.ErrorMessage); });
                    _log.LogError("Erro durante a validação dos dados {@updateUser}", updateUser);
                    return BadRequest(new { message = responseErro.Errors });
                }

                var result = await _mediator.Send(updateUser);
                if (result.Errors.Any())
                    return BadRequest(new { message = result.Errors });

                return Ok(result);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Erro ao tentar alterar o usuário: {@updateUser}", updateUser);
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }
        }

        [Authorize]
        //[Authorize(Roles = "Administrator")]
        [Route("deleteuser")]
        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteUserRequest deleteUserRequest)
        {
            try
            {
                var validator = await new DeleteUserValidator().ValidateAsync(deleteUserRequest);
                _log.LogInformation("Deletando usuário código: {@deleteUserRequest.Id}", deleteUserRequest.Id);
                if (!validator.IsValid)
                {
                    Response responseErro = new Response();
                    validator.Errors.ToList().ForEach(c => { responseErro.AddError(c.ErrorMessage); });
                    _log.LogError("Erro durante a validação dos dados {@deleteUserRequest.Id}", deleteUserRequest);
                    return BadRequest(new { message = responseErro.Errors });
                }
                var result = await _mediator.Send(deleteUserRequest);
                if (result.Errors.Any())
                    return BadRequest(new { message = result.Errors });

                return Ok(result);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Erro ao tentar deletar usuário: {@deleteUserRequest.Id}", deleteUserRequest.Id);
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }
        }

        [Authorize]
        //[Authorize(Roles = "Administrator")]
        [Route("redefinepassword")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RedefinePassword([FromBody] RedefinePasswordRequest redefinePassword)
        {
            try
            {
                _log.LogInformation("Redefinir senha do usuário: {@redefinePassword}", redefinePassword);

                var validator = await new RedefinePasswordValidadtor().ValidateAsync(redefinePassword);

                if (!validator.IsValid)
                {
                    _log.LogError("Erro durante a validação dos dados {@newUser}", redefinePassword);

                    Response responseErro = new Response();
                    validator.Errors.ToList().ForEach(c => { responseErro.AddError(c.ErrorMessage); });
                    return BadRequest(new { message = responseErro.Errors });
                }

                var result = await _mediator.Send(redefinePassword);

                if (result.Errors.Any())
                    return BadRequest(new { message = result.Errors });

                return Ok(result);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Erro ao redefinir senha do usuário: {@redefinePassword}", redefinePassword);
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { response = response.Errors });
            }
        }

        [Authorize]
        [Route("updateusercompanies")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUserCompanies([FromBody] ChangeLinkWithTheCompanyRequest changeLinkWithTheCompanyRequest)
        {
            try
            {
                _log.LogInformation("Redefinir lista de empresa do usuário: {@changeLinkWithTheCompanyRequest}", changeLinkWithTheCompanyRequest);

                var validator = await new ChangeLinkWithTheCompanyValidator().ValidateAsync(changeLinkWithTheCompanyRequest);

                if (!validator.IsValid)
                {
                    _log.LogError("Erro durante a validação dos dados {@changeLinkWithTheCompanyRequest}", changeLinkWithTheCompanyRequest);

                    Response responseErro = new Response();
                    validator.Errors.ToList().ForEach(c => { responseErro.AddError(c.ErrorMessage); });
                    return BadRequest(new { message = responseErro.Errors });
                }

                var result = await _mediator.Send(changeLinkWithTheCompanyRequest);
                if (result.Errors.Any())
                    return BadRequest(new { message = result.Errors });

                return Ok(result);

            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Erro ao redefinir lista de empresa usuário: {@changeLinkWithTheCompanyRequest}", changeLinkWithTheCompanyRequest);
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { message = response.Errors });
            }
        }


        [Route("passwordchangerequest")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public  IActionResult PasswordChangeRequest(string email)
        {
            try
            {
                _log.LogInformation("solicitação de alteração senha do usuário: {@email}", email);

                _userApplication.PasswordChangeRequest(email);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Erro na solicitação de alteração de senha do usuário: {@email}", email);
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { response = response.Errors });
            }
        }

        [Route("redefinePasswordbyemail")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RedefinePasswordByEmailRequest([FromQuery]RedefinePasswordByEmailRequest redefinePasswordByEmailRequest)
        {
            try
            {
                if(string.IsNullOrEmpty(redefinePasswordByEmailRequest.Token) || string.IsNullOrEmpty(redefinePasswordByEmailRequest.NewPassword))
                    return BadRequest(new { message = "Dados inválidos." });
                
                ConvertTokenId convertTokenId = new ConvertTokenId();
                if(!convertTokenId.TokenExperied(redefinePasswordByEmailRequest.Token))
                    return BadRequest(new { message = "Token expirado." });

                redefinePasswordByEmailRequest.UserId = new Guid(convertTokenId.GetTokenUserMaster(redefinePasswordByEmailRequest.Token));
                _log.LogInformation("alterando senha do usuário id: {@redefinePasswordByEmailRequest.UserId}", redefinePasswordByEmailRequest.UserId);

                var result  = await _mediator.Send(redefinePasswordByEmailRequest);
                return Ok(result.Result);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Erro ao alterar senha do usuário id: {@redefinePasswordByEmailRequest.UserId}", redefinePasswordByEmailRequest);
                var response = new Response().AddError(ex.Message);
                return BadRequest(new { response = response.Errors });
            }
        }
        
    }
}
