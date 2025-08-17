using AutoMapper;
using Corporate.Contta.Schedule.Application.Mapping.Request;
using Corporate.Contta.Schedule.Application.Mapping.Response;
using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities;
using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using Corporate.Contta.Schedule.Infra.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Application.Handler
{
    public class CompanyHandler : IRequestHandler<NewCompanyRequest, Response>,
        IRequestHandler<UpdateCompanyRequest, Response>,
        IRequestHandler<DeleteCompanyRequest, Response>,
        IRequestHandler<GetCompanyRequest, Response>,
        IRequestHandler<GetAllCompanyRequest, Response>,
        IRequestHandler<GetAllCompanyDisabledRequest, Response>,
        IRequestHandler<NewFaturamento12Request, Response>,
        IRequestHandler<NewCompanySociosRequest, Response>
    {
        private IMapper _mapper;
        private ICompanyRepository _companyRepository;
        private FaturamentoEmpresaRepository _faturamentoEmpresaRepository;

        public CompanyHandler(IMapper mapper, ICompanyRepository companyRepository)
        {
            _mapper = mapper;
            _companyRepository = companyRepository;
        }

        public async Task<Response> Handle(NewCompanyRequest request, CancellationToken cancellationToken)
        {
            if (await _companyRepository.ExistsCompany(request.Cnpj))
                return new Response().AddError(Message.EmpresaJaCadastrada);

            var result = await _companyRepository.Insert(_mapper.Map<CompanyInformation>(request));
            if (result.EmpresaCadastrada == false)
                return new Response(Message.EmpresaJaCadastradaUser);
            return new Response(Message.OperacaoRealizadaComSucesso);
        }

        public async Task<Response> Handle(UpdateCompanyRequest request, CancellationToken cancellationToken)
        {
            var result = await _companyRepository.Update(_mapper.Map<CompanyInformation>(request), "");
            if (!result)
                return new Response(Message.ErroAoRealizaOperacao);
            return new Response(Message.OperacaoRealizadaComSucesso);
        }

        public async Task<Response> Handle(DeleteCompanyRequest request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetById(request.Id.Value);
            if (company == null)
                return new Response(Message.EmpresaNaoEncontrada);
            var result = await _companyRepository.Delete(company,request.UserId);
            if (!result)
                return new Response(Message.ErroAoRealizaOperacao);

            return new Response(Message.OperacaoRealizadaComSucesso);
        }

        public async Task<Response> Handle(GetCompanyRequest request, CancellationToken cancellationToken)
        {
            var result = await _companyRepository.GetCompanyInformationByCnpj(request.Document);
            return new Response(_mapper.Map<CompanyInformationDto>(result));
        }

        public async Task<Response> Handle(GetAllCompanyRequest request, CancellationToken cancellationToken)
        {
            var result = await _companyRepository.GetAll(request.UserId);
            return new Response(_mapper.Map<List<CompanyInformationDto>>(result));
        }

        public async Task<Response> Handle(GetAllCompanyDisabledRequest request, CancellationToken cancellationToken)
        {
            var result = await _companyRepository.GetAllDisabled();
            return new Response(_mapper.Map<List<CompanyInformationDto>>(result));
        }

        public async Task<Response> Handle(NewFaturamento12Request request, CancellationToken cancellationToken)
        {
            if (request.FechamentoEmp == false)
            {
                await FaturamentoEmpresaRepository.Insert(_mapper.Map<FaturamentoEmpresa>(request));
                return new Response(Message.OperacaoRealizadaComSucesso);
            }
            else
            {
              var result =  FaturamentoEmpresaRepository.InsertCurt(_mapper.Map<FaturamentoEmpresa>(request));
                return new Response(result);
            }
        }

        public async Task<Response> Handle(NewCompanySociosRequest request, CancellationToken cancellationToken)
        {
            var result = await _companyRepository.InsertCompanySocios(request.Document, request.UserId);
            if (result.EmpresaCadastrada == false)
                return new Response(_mapper.Map<CompanyInformationSocios>(result));
            return new Response(Message.OperacaoRealizadaComSucesso);
        }

        private FaturamentoEmpresaRepository FaturamentoEmpresaRepository
        {
            get
            {
                if (_faturamentoEmpresaRepository == null)
                {
                    _faturamentoEmpresaRepository = new FaturamentoEmpresaRepository();
                }

                return _faturamentoEmpresaRepository;
            }
        }
    }
}
