using AutoMapper;
using Corporate.Contta.Schedule.Application.Mapping.Dto.Nfe;
using Corporate.Contta.Schedule.Application.Mapping.Request;
using Corporate.Contta.Schedule.Application.Mapping.Response;
using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Application.Handler
{
    public class ApuracaoHandler : IRequestHandler<ApuracaoRequest, Response>
    {
        private IMapper _mapper;
        private IApuracaoRepository _apuracaoRepository;
        public ApuracaoHandler(IMapper mapper, IApuracaoRepository apuracaoRepository)
        {
            _mapper = mapper;
            _apuracaoRepository = apuracaoRepository;
        }
        public async Task<Response> Handle(ApuracaoRequest request, CancellationToken cancellationToken)
        {
            if (request.Mod55) 
            {
                var result = await _apuracaoRepository.GetAllApuracao(request.Token, request.DataEmissao, request.CompanyId);
                return new Response(_mapper.Map<List<ApuracaoDto>>(result));
            }
            else if (request.Mod57)
            {
                var result = await _apuracaoRepository.GetAllApuracaoCte(request.Token, request.DataEmissao, request.CompanyId);
                return new Response(_mapper.Map<ApuracaoCteDto>(result));
            }
            else if(request.Service)
            {
                var result = await _apuracaoRepository.GetAllApuracaoServico(request.Token, request.DataEmissao, request.CompanyId);
                return new Response(_mapper.Map<ApuracaoServicoDto>(result));
            }
            else
                return new Response();

        }
    }
}
