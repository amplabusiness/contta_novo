using AutoMapper;
using Corporate.Contta.Schedule.Application.Mapping;
using Corporate.Contta.Schedule.Application.Mapping.Request;
using Corporate.Contta.Schedule.Application.Mapping.Response;
using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities.Configuration;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Application.Handler
{
    public class ConfigurationUserHandler : IRequestHandler<NewConfigurationUserRequest, Response>,
        IRequestHandler<GetConfigurationUserRequest, Response>,
        IRequestHandler<UpdateConfigurationRequest, Response>
    {
        private IMapper _mapper;
        private IConfigurationUserRepository _configurationUserRepository;

        public ConfigurationUserHandler(IMapper mapper, IConfigurationUserRepository _configuration)
        {
            _mapper = mapper;
            _configurationUserRepository = _configuration;
        }

        public async Task<Response> Handle(NewConfigurationUserRequest request, CancellationToken cancellationToken)
        {
            await _configurationUserRepository.Insert(_mapper.Map<ConfigurationUser>(request));
            return new Response(Message.OperacaoRealizadaComSucesso);
        }

        public async Task<Response> Handle(GetConfigurationUserRequest request, CancellationToken cancellationToken)
        {
            var result = await _configurationUserRepository.Get(request.UserId);
            return new Response(_mapper.Map<ConfigurationUser>(result));
        }

        public async Task<Response> Handle(UpdateConfigurationRequest request, CancellationToken cancellationToken)
        {
            var result = await _configurationUserRepository.Update(_mapper.Map<UpdateConfigurationRequest, ConfigurationUser>(request));
            if (!result)
                return new Response(Message.ErroAoRealizaOperacao);
            return new Response(Message.OperacaoRealizadaComSucesso);
        }
    }
}
