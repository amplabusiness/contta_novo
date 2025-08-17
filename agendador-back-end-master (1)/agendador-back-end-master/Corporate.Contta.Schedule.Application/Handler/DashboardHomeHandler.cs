using AutoMapper;
using Corporate.Contta.Schedule.Application.Mapping.Dto.Dashboard;
using Corporate.Contta.Schedule.Application.Mapping.Request;
using Corporate.Contta.Schedule.Application.Mapping.Response;
using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities.DashboardAgg;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Application.Handler
{
    class DashboardHomeHandler : IRequestHandler<HomeRequest, Response>
    {
        public IDashboardHomeRepository _repository;
        public IMapper _mapper;
        public DashboardHomeHandler(IMapper mapper, IDashboardHomeRepository dashboardHomeRepository)
        {
            _mapper = mapper;
            _repository = dashboardHomeRepository;
        }
        public async Task<Response> Handle(HomeRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result =  _repository.GetAllHome(request.UserId, request.dhEmi);
                return new Response(_mapper.Map<List<HomeCompany>>(result));
            }
            catch (Exception)
            {
                return new Response(Message.ErroAoRealizaOperacao);
                throw;
            }   
        }
    }
}
