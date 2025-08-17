using AutoMapper;
using Corporate.Contta.Schedule.Application.Mapping.Dto.CompanyInformation;
using Corporate.Contta.Schedule.Application.Mapping.Dto.User;
using Corporate.Contta.Schedule.Application.Mapping.Param;
using Corporate.Contta.Schedule.Application.Mapping.Request;
using Corporate.Contta.Schedule.Application.Mapping.Response;
using Corporate.Contta.Schedule.Application.Mapping.Response.GetInformationByMasterId;
using Corporate.Contta.Schedule.Application.Mapping.Result.GetUser;
using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities;
using Corporate.Contta.Schedule.Domain.Entities.Configuration;
using Corporate.Contta.Schedule.Domain.Entities.UserAgg;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Application.Handler
{
    public class UserHandler :
        IRequestHandler<NewUserRequest, Response>,
        IRequestHandler<UpdateUserRequest, Response>,
        IRequestHandler<DeleteUserRequest, Response>,
        IRequestHandler<RedefinePasswordRequest, Response>,
        IRequestHandler<ChangeLinkWithTheCompanyRequest, Response>,
        IRequestHandler<GetInformationUserRequest, Response> ,
        IRequestHandler<GetCompanySummaryInformationRequest, Response>,
        IRequestHandler<RedefinePasswordByEmailRequest, Response>,
        IRequestHandler<GetUserRequest, Response>
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;
        private ICompanyRepository _companyRepository;
        private IConfigurationUserRepository _configurationUser;
        public UserHandler( IUserRepository userRepository, IMapper mapper, IConfigurationUserRepository configurationUser, ICompanyRepository companyRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _companyRepository = companyRepository;
            _configurationUser = configurationUser;
        }
        public async Task<Response> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            await _userRepository.DeleteUserById(request.Id.Value);
            return new Response(Message.OperacaoRealizadaComSucesso);
        }

        public async Task<Response> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.UpdateUser(_mapper.Map<UpdateUserRequest, User>(request));
            if (!result)
                return new Response(Message.ErroAoRealizaOperacao);
            return new Response(Message.OperacaoRealizadaComSucesso);
        }

        public async Task<Response> Handle(NewUserRequest request, CancellationToken cancellationToken)
        {   
            _userRepository.Add(_mapper.Map<User>(request));
            return new Response(Message.OperacaoRealizadaComSucesso);
        }

        public async Task<Response> Handle(RedefinePasswordRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.Id.Value);            
            if (!user.ValidatePassword(request.OldPassword))
                return new Response().AddError(Message.OperacaoRealizadaComSucesso);            

            user.UpdatePassword(request.NewPassword);
            var result  = await _userRepository.RedefinePassword(request.Id.Value, user.Password);
            if (!result)
                return new Response(Message.ErroAoRealizaOperacao);
            return new Response(Message.OperacaoRealizadaComSucesso);

        }

        public async Task<Response> Handle(ChangeLinkWithTheCompanyRequest request, CancellationToken cancellationToken)
        {
           var result = await _userRepository.ChangeLinkWithTheCompany(request.Id.Value, request.UserCompanies);
           return result ? new Response(Message.OperacaoRealizadaComSucesso) : new Response(Message.ErroAoRealizaOperacao);
        }

        public async Task<Response> Handle(GetInformationUserRequest request, CancellationToken cancellationToken)
        {
            if (request.UserComum)
            {
                var userCmum = await _userRepository.GetUserById(request.Id);
                return new Response(_mapper.Map<User>(userCmum));
            }
           
            var user = await _userRepository.GetUsersByMasterId(request.Id);
            if (user.Count > 0)
                return new Response(_mapper.Map<List<User>>(user));

            var company = await _companyRepository.GetById(request.Id);
            if(company != null)
            {
                return new Response(_mapper.Map<List<User>>(company));
            }           

            if (user == null && company == null)
                return new Response(Message.UsurioNãoEncontrado);

            return new Response(Message.UsurioNãoEncontrado);

        }

        public async Task<Response> Handle(GetCompanySummaryInformationRequest request, CancellationToken cancellationToken)
        {
            var listCompanies = await _companyRepository.GetCampanySummaryInformationByMasterId(request.Id.Value);
            return new Response(_mapper.Map<List<CompanySummaryInformationDto>>(listCompanies));
            
        }

        public async Task<Response> Handle(RedefinePasswordByEmailRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.UserId);
            if (user == null)
                throw new Exception(Message.UsuarioNaoEncontrado);

            user.UpdatePassword(request.NewPassword);
          var resutl = await _userRepository.RedefinePassword(user.Id.Value, user.Password);
            if (resutl)
                return new Response(Message.OperacaoRealizadaComSucesso);
            else
                return new Response(Message.ErroAoRealizaOperacao);
        }

        public async Task<Response> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(request.Id);
            if (user == null)
                throw new Exception(Message.UsuarioNaoEncontrado);
              return new Response(_mapper.Map<User>(user)); ;
        }
    }
}
