using AutoMapper;
using Corporate.Contta.Schedule.Application.Mapping;
using Corporate.Contta.Schedule.Application.Mapping.Request;
using Corporate.Contta.Schedule.Application.Mapping.Response;
using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities.Criticas;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Application.Handler
{
    public class CriticasNovasHandler : IRequestHandler<NewCriticasNovasRequest, Response>,
        IRequestHandler<GetCriticasNovasRequest, Response>
    {
        private IMapper _mapper;
        private ICriticasRepository _criticasRepository;

        public CriticasNovasHandler(IMapper mapper, ICriticasRepository criticasRepository)
        {
            _mapper = mapper;
            _criticasRepository = criticasRepository;
        }

        public async Task<Response> Handle(NewCriticasNovasRequest request, CancellationToken cancellationToken)
        {

            await _criticasRepository.Insert(_mapper.Map<CriticasNovas>(request));
            return new Response(Message.OperacaoRealizadaComSucesso);
        }

        public async Task<Response> Handle(GetCriticasNovasRequest request, CancellationToken cancellationToken)
        {
            var result = await _criticasRepository.Get(request.CompanyId);
            return new Response(_mapper.Map<CriticasNovas>(result));
        }
    }
}
