using AutoMapper;
using Corporate.Contta.Schedule.Application.Mapping.Dto.Dashboard;
using Corporate.Contta.Schedule.Application.Mapping.Request;
using Corporate.Contta.Schedule.Application.Mapping.Response;
using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities.DashboardAgg;
using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using Corporate.Contta.Schedule.Infra.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Application.Handler
{
    class DashboardHandler 
    {
        public IDashboardRepository _repository;  
        public IMapper _mapper;
        public DashboardHandler(IMapper mapper, IDashboardRepository dashboardRepository, IEmpresaEmitRepository empresaEmitRepository, IEmpresaDestRepository empresaDestRepository)
        {
            _mapper = mapper;
            _repository = dashboardRepository;          
        }
       
    }
}
