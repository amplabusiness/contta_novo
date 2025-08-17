using AutoMapper;
using Corporate.Contta.Schedule.Application.Mapping.Request;
using Corporate.Contta.Schedule.Application.Mapping.Response;
using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities.BlocoE;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Application.Handler
{
    public class AgBlocoEHandler : IRequestHandler<AgBlocoERequest, Response>, IRequestHandler<AgBlocoEGetRequest, Response>
    {
        private IMapper _mapper;
        private IAgBlocoERepository _agBlocoERepository;

        public AgBlocoEHandler(IMapper mapper, IAgBlocoERepository agBlocoERepository)
        {
            _mapper = mapper;
            _agBlocoERepository = agBlocoERepository;
        }
        public async Task<Response> Handle(AgBlocoERequest request, CancellationToken cancellationToken)
        {
            await _agBlocoERepository.Create(_mapper.Map<AgBlocoE>(request));
            return new Response(Message.OperacaoRealizadaComSucesso);
        }

        public async Task<Response> Handle(AgBlocoEGetRequest request, CancellationToken cancellationToken)
        {
            var result = await _agBlocoERepository.GetByCompanyInformation(request.CompanyInformationId, request.DateInition);
            return new Response(_mapper.Map<AgBlocoERequest>(result));
        }
    }
}
