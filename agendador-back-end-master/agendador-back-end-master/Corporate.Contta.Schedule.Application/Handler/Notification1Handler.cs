using AutoMapper;
using Corporate.Contta.Schedule.Application.Mapping;
using Corporate.Contta.Schedule.Application.Mapping.Dto.Notification;
using Corporate.Contta.Schedule.Application.Mapping.Request;
using Corporate.Contta.Schedule.Application.Mapping.Response;
using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities.NotificationAgg;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Application.Handler
{
    public class NotificationHandler : IRequestHandler<NewNotificationRequest, Response>,
         IRequestHandler<GetAllNotificationRequest, Response>,
         IRequestHandler<UpdateNotificationRequest, Response>
    {
        private IMapper _mapper;
        private INotificationUserRepository _notificationRepository;

        public NotificationHandler(IMapper mapper, INotificationUserRepository _notification)
        {
            _mapper = mapper;
            _notificationRepository = _notification;
        }

        public async Task<Response> Handle(NewNotificationRequest request, CancellationToken cancellationToken)
        {
            await _notificationRepository.Insert(_mapper.Map<Notification>(request));
            return new Response(Message.OperacaoRealizadaComSucesso);
        }

        public async Task<Response> Handle(GetAllNotificationRequest request, CancellationToken cancellationToken)
        {
            var result = await _notificationRepository.GetAll(request.EmpresaId);
            return new Response(_mapper.Map<List<NotificationDto>>(result));
        }

        public async Task<Response> Handle(UpdateNotificationRequest request, CancellationToken cancellationToken)
        {
            var result = await _notificationRepository.Update(_mapper.Map<UpdateNotificationRequest, Notification>(request));
            if (!result)
                return new Response(Message.ErroAoRealizaOperacao);
            return new Response(Message.OperacaoRealizadaComSucesso);
        }
    }
}
