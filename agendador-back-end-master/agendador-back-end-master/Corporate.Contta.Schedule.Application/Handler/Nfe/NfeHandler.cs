using AutoMapper;
using Corporate.Contta.Schedule.Application.Mapping.Dto.Nfe;
using Corporate.Contta.Schedule.Application.Mapping.Request;
using Corporate.Contta.Schedule.Application.Mapping.Response;
using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities.FullNfeAgg;
using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Application.Handler
{
    public class NfeHandler : IRequestHandler<NewNfeRequest, Response>,
        IRequestHandler<UpdateNfeRequest, Response>,
        IRequestHandler<GetnfeRequest, Response>,
        IRequestHandler<GetAllNfeRequest, Response>,
        IRequestHandler<GetAllFullNfeRequest, Response>,
        IRequestHandler<GetAllNfeTRequest, Response>,
        IRequestHandler<GetByIdNfeRequest, Response>,
        IRequestHandler<GetAllByIdCompanyRequest, Response>,
        IRequestHandler<GetBasCalculoRequest, Response>,
        IRequestHandler<DesativarNota, Response>,
        IRequestHandler<NewNfVendaManualRequest, Response>,
        IRequestHandler<NewNfServicoManualRequest, Response>,
        IRequestHandler<NewAjusteNfeRequest, Response>,
        IRequestHandler<FilterNfeRequest, Response>,
        IRequestHandler<GetAllNfeServicoRequest, Response>,
        IRequestHandler<GetAllNfeMod57Request, Response>,
        IRequestHandler<GetRegistrosBlERequest, Response>,
        IRequestHandler<UpdateNfeAjusteRequest, Response>,
        IRequestHandler<FilterNfeCanceladaRequest, Response>,
        IRequestHandler<GetTotalizadorNfeSaidaRequest, Response>       
    {
        private IMapper _mapper;
        private INfeRepository _nfeRepository;
        private ICompanyRepository _companyRepository;
        public NfeHandler(IMapper mapper, INfeRepository nfeRepository, ICompanyRepository companyRepository)
        {
            _mapper = mapper;
            _nfeRepository = nfeRepository;
            _companyRepository = companyRepository;
        }
        public async Task<Response> Handle(GetAllNfeServicoRequest request, CancellationToken cancellationToken)
        {
            var result = await _nfeRepository.GetAllNfeServico(request.Documento, request.Operation, request.Data, request.Pagina, request.QtdPorPagina);
            return new Response(result);
        }
        public async Task<Response> Handle(GetAllNfeMod57Request request, CancellationToken cancellationToken)
        {
            var result = await _nfeRepository.GetAllNfeMod57(request.Documento, request.Operation, request.Data);
            return new Response(result);
        }


        public Task<Response> Handle(UpdateNfeRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Response> Handle(GetnfeRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Response> Handle(GetAllNfeRequest request, CancellationToken cancellationToken)
        {
            var result = await _nfeRepository.GetAll(request.Documento, request.Operation);
            return new Response(_mapper.Map<List<NfeDto>>(result));
        }


        public async Task<Response> Handle(GetAllFullNfeRequest request, CancellationToken cancellationToken)
        {
            var result = await _nfeRepository.GetAllFull(request.Documento, request.Operation, request.Data);
            return new Response(result);
        }

        public async Task<Response> Handle(GetAllNfeTRequest request, CancellationToken cancellationToken)
        {            
            var result = await _nfeRepository.GetAllNfeT(request.Documento, request.Operation, request.Data, request.Pagina, request.QtdPorPagina, request.ListNfe, request.Apuracao);
            return new Response(result);
        }

        public async Task<Response> Handle(GetRegistrosBlERequest request, CancellationToken cancellationToken)
        {
            var result = await _nfeRepository.GetRegistrosBlE(request.EmpresaId, request.Data);
            return new Response(result);
        }

        public Task<Response> Handle(NewNfeRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Response> Handle(GetByIdNfeRequest request, CancellationToken cancellationToken)
        {
            var result = await _nfeRepository.GetById(request.NfeId);
            return new Response(_mapper.Map<NfeDto>(result));
        }

        public async Task<Response> Handle(GetAllByIdCompanyRequest request, CancellationToken cancellationToken)
        {
            //ToDo:Ver as informação que esta sendo buscada nesse método
            var result = await _nfeRepository.GetAll(request.Id, 1);
            return new Response(_mapper.Map<List<NfeDto>>(result));
        }

        public async Task<Response> Handle(DesativarNota request, CancellationToken cancellationToken)
        {
            var result = await _nfeRepository.DesativarNota(request.Id);
            if (!result)
                return new Response(Message.ErroAoRealizaOperacao);
            return new Response(Message.OperacaoRealizadaComSucesso);
        }

        public async Task<Response> Handle(GetBasCalculoRequest request, CancellationToken cancellationToken)
        {
            var result = await _nfeRepository.GetBaseCalculo(request.Id, request.DateClose);

            return new Response(_mapper.Map<TotalNfe>(result));
        }

        public async Task<Response> Handle(NewNfVendaManualRequest request, CancellationToken cancellationToken)
        {
            if (!await _companyRepository.ExistsCompany(request.CompanyInformation))
                return new Response(Message.EmpresaNaoEncontrada);
            await _nfeRepository.Insert(_mapper.Map<NfVendaManual>(request));
            return new Response(Message.OperacaoRealizadaComSucesso);
        }

        public async Task<Response> Handle(NewNfServicoManualRequest request, CancellationToken cancellationToken)
        {
            if (!await _companyRepository.ExistsCompany(request.CompanyInformation))
                return new Response(Message.EmpresaNaoEncontrada);            
            await _nfeRepository.Insert(_mapper.Map<NfServicoManual>(request), request.NotaServeTransporte);
            return new Response(Message.OperacaoRealizadaComSucesso);
        }

        public async Task<Response> Handle(FilterNfeRequest request, CancellationToken cancellationToken)
        {
            var nfes = await _nfeRepository.GetByFilter(request.CompanyId, request.TipoNfe, request.DescProduto, request.Cnpj, request.DhEmiss, request.Uf, request.NomeCli);
            return new Response(_mapper.Map<List<NfeDto>>(nfes));
        }

        public async Task<Response> Handle(FilterNfeCanceladaRequest request, CancellationToken cancellationToken)
        {
            var nfesCanceladas = await _nfeRepository.GetByFilterCanceladas(request.CompanyId, request.TipoNfe, request.Cnpj, request.DhEmiss);
            return new Response(_mapper.Map<List<RetornoNfeCanceladasDto>>(nfesCanceladas));
        }

        public async Task<Response> Handle(NewAjusteNfeRequest request, CancellationToken cancellationToken)
        {
            var nfeTotal = await _nfeRepository.InsertAjusteNfe(request);
            return new Response(nfeTotal);
        }

        public async Task<Response> Handle(UpdateNfeAjusteRequest request, CancellationToken cancellationToken)
        {
            var resultado = await _nfeRepository.UpdateAluste(request.ajusteId, request.TotalNfe, request.Aliquota, request.TotalCalculo);

            if (!resultado)
                return new Response(Message.ErroAoRealizaOperacao);
            else
                return new Response(Message.OperacaoRealizadaComSucesso);
        }

        public async Task<Response> Handle(GetTotalizadorNfeSaidaRequest request, CancellationToken cancellationToken)
        {
            var totalizadorNfeSaida = await _nfeRepository.GetTotalizadorNfeSaida(request.EmpresaId, request.Data);
            return new Response(_mapper.Map<TotalizadorNfeSaidaDto>(totalizadorNfeSaida));
        }
    }
}
