using AutoMapper;
using Corporate.Contta.Schedule.Application.Mapping;
using Corporate.Contta.Schedule.Application.Mapping.Dto.Notification;
using Corporate.Contta.Schedule.Application.Mapping.Request;
using Corporate.Contta.Schedule.Application.Mapping.Response;
using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities.ConfigurationFhAgg;
using Corporate.Contta.Schedule.Domain.Entities.NotificationAgg;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Application.Handler
{
    public class ConfigurationFhHandler : IRequestHandler<NewConfigurationFhRequest, Response>,
         IRequestHandler<GetConfigurationFhUserRequest, Response>,
         IRequestHandler<UpdateConfigurationFhRequest, Response>
    {
        private IMapper _mapper;
        private IConfigurationFhRepository _configurationFhRepositor;

        public ConfigurationFhHandler(IMapper mapper, IConfigurationFhRepository configurationFhRepository)
        {
            _mapper = mapper;
            _configurationFhRepositor = configurationFhRepository;
        }

        public async Task<Response> Handle(NewConfigurationFhRequest request, CancellationToken cancellationToken)
        {            
            await _configurationFhRepositor.Insert(_mapper.Map<ConfigurationFh>(request));
            return new Response(Message.OperacaoRealizadaComSucesso);
        }

        public async Task<Response> Handle(GetConfigurationFhUserRequest request, CancellationToken cancellationToken)
        {
            var result = await _configurationFhRepositor.Get(request.Id);
            return new Response(_mapper.Map<ConfigurationFhDto>(result));
        }

        public async Task<Response> Handle(UpdateConfigurationFhRequest request, CancellationToken cancellationToken)
        {
            var result = await _configurationFhRepositor.Update(_mapper.Map<UpdateConfigurationFhRequest, ConfigurationFh>(request));
            if (result == null)
                return new Response(Message.ErroAoRealizaOperacao);
            return new Response(Message.OperacaoRealizadaComSucesso);
        }
    }
}
