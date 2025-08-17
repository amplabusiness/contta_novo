using AutoMapper;
using Corporate.Contta.Schedule.Application.Mapping.Dto.Dashboard;
using Corporate.Contta.Schedule.Application.Mapping.Dto.Dashboard.Apuracao;
using Corporate.Contta.Schedule.Application.Mapping.Request;
using Corporate.Contta.Schedule.Application.Mapping.Response;
using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities.DashboardAgg.Apuracoes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Application.Handler
{
    public class DetalhamentoApuracaoHandle : IRequestHandler<DetalhamentoApuracaoRequest, Response>, 
                                              IRequestHandler<DetalhamentoImpostoRequest, Response>,
                                              IRequestHandler<DetalhamentoApuracaoIcmsPisNfeRequest, Response>
    {
        private IMapper _mapper;
        private IDetalhamentoApuracaoRepository _repository;
       
        public DetalhamentoApuracaoHandle(IMapper mapper, IDetalhamentoApuracaoRepository detalhamentoApuracaoRepository)
        {
            _mapper = mapper;
            _repository = detalhamentoApuracaoRepository;
        }
        public async Task<Response> Handle(DetalhamentoApuracaoRequest request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetDetalhamentoApuracao(request.CompanyId, request.DhEmiss);
            return new Response(_mapper.Map<DetalhamentoApuracaoDto>(result));
        }

        public async Task<Response> Handle(DetalhamentoImpostoRequest request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetDetalhamentoImposto(request.CompanyId, request.DhEmiss);
            return new Response(_mapper.Map<DetalhamentoImpostoDto>(result));
        }

        public async Task<Response> Handle(DetalhamentoApuracaoIcmsPisNfeRequest request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAgrupamentoDetalhamentoApuracao(request.Ids,request.Tipo,request.Grupo);
            return new Response(_mapper.Map<AgrupamentoDetalhamentoApuracaoDto>(result));
        }
    }
}
