using AutoMapper;
using Corporate.Contta.Schedule.Application.Mapping.Request;
using Corporate.Contta.Schedule.Application.Mapping.Response;
using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities.ExternalTable;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Application.Handler
{
    //public class ExternalTableHandler : IRequestHandler<NewExternalTableRequest, Response>       
    //{
    //    private IMapper _mapper;
    //    private IExternalTableRepository _tableRepository;

    //    public ExternalTableHandler(IMapper mapper, IExternalTableRepository tableRepository)
    //    {
    //        _mapper = mapper;
    //        _tableRepository = tableRepository;
    //    }

    //    public async Task<Response> Handle(NewExternalTableRequest request, CancellationToken cancellationToken)
    //    {
    //        await _tableRepository.Insert(_mapper.Map<IcmsSt>(request));
    //        return new Response(Message.OperacaoRealizadaComSucesso);
    //    }
    //}
}
