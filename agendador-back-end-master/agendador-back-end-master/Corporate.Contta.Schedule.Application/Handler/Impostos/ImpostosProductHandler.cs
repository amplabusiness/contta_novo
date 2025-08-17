using AutoMapper;
using Corporate.Contta.Schedule.Application.Mapping.Dto.ImpostosProduct;
using Corporate.Contta.Schedule.Application.Mapping.Request;
using Corporate.Contta.Schedule.Application.Mapping.Request.Impostos;
using Corporate.Contta.Schedule.Application.Mapping.Response;
using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Application.Handler.Impostos
{
    public class ImpostosProductHandler : IRequestHandler<NewImpostoAntecipacaoRequest, Response>,
        IRequestHandler<NewImpostoInsentoRequest, Response>,
        IRequestHandler<NewImpostoRedCestabasicaRequest, Response>,
        IRequestHandler<NewImpostoExigibilidadeRequest, Response>,
        IRequestHandler<GetAllProdutosImpostosRequest, Response>,
        IRequestHandler<NewImpostoReducaoRequest, Response>,
        IRequestHandler<NewImpostoImuneRequest, Response>,
        IRequestHandler<UpdateImpIncerramentoRequest, Response>,
        IRequestHandler<UpdateExigibilidadeRequest, Response>
    {
        private IImpostosProdutosRepository _impostosProdRepository;
        private IMapper _mapper;
        public ImpostosProductHandler(IMapper mapper, IImpostosProdutosRepository impostosProdutosRepository)
        {
            _mapper = mapper;
            _impostosProdRepository = impostosProdutosRepository;
        }

        public async Task<Response> Handle(NewImpostoAntecipacaoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _impostosProdRepository.InsertAnt(request.ListImpostoAntecipacao);
                return new Response(Message.OperacaoRealizadaComSucesso); 
            }
            catch (Exception)
            {
                return new Response(Message.ErroAoRealizaOperacao);
            }  
        }

        public async Task<Response> Handle(NewImpostoInsentoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _impostosProdRepository.InsertInsent(request.ListImpostoInsento);
                return new Response(Message.OperacaoRealizadaComSucesso);
            }
            catch (Exception)
            {
                return new Response(Message.ErroAoRealizaOperacao);
            }
        }

        public async Task<Response> Handle(NewImpostoImuneRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _impostosProdRepository.InsertImun(request.ListImpostoImune);
                return new Response(Message.OperacaoRealizadaComSucesso);
            }
            catch (Exception)
            {
                return new Response(Message.ErroAoRealizaOperacao);
            }
        }

        public async Task<Response> Handle(NewImpostoRedCestabasicaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _impostosProdRepository.InsertRedCestaBasica(request.ListImpostoRedCestBasica);
                return new Response(Message.OperacaoRealizadaComSucesso);
            }
            catch (Exception)
            {
                return new Response(Message.ErroAoRealizaOperacao);
            }
        }

        public async Task<Response> Handle(NewImpostoExigibilidadeRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _impostosProdRepository.InsertExigibilidadeSus(request);
                return new Response(Message.OperacaoRealizadaComSucesso);
            }
            catch (Exception)
            {
                return new Response(Message.ErroAoRealizaOperacao);
            }
        }

        public async Task<Response> Handle(NewImpostoReducaoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _impostosProdRepository.InsertReducao(request);
                return new Response(Message.OperacaoRealizadaComSucesso);
            }
            catch (Exception)
            {
                return new Response(Message.ErroAoRealizaOperacao);
            }
        }

        public async Task<Response> Handle(GetAllProdutosImpostosRequest request, CancellationToken cancellationToken)
        {
            if (request.AntecipacapTri)
            {
                var result = _impostosProdRepository.GetAllImpostosAntecipacao(request.EmpresaId);
                if (result.Result.Count > 0)
                    return new Response(_mapper.Map<List<AntecipacaoDto>>(result.Result));
                return new Response(Message.ErroTrazerTabelaImpposto);
            }
            else if (request.CestaBasica)
            {
                var result = _impostosProdRepository.GetAllImpostoCestaBasica(request.EmpresaId);
                if (result.Result.Count > 0)
                    return new Response(_mapper.Map<List<CestaBasicaDto>>(result.Result));
                return new Response(Message.ErroTrazerTabelaImpposto);

            }
            else if (request.Exigibilidade)
            {
                var result = _impostosProdRepository.GetAllImpostosExi(request.EmpresaId);
                if (result.Result.Count > 0)
                    return new Response(_mapper.Map<List<ExigibilidadeDto>>(result.Result));
                return new Response(Message.ErroTrazerTabelaImpposto);
            }
            else if (request.Imune)
            {
                var result = _impostosProdRepository.GetAllImpostosimune(request.EmpresaId);
                if (result.Result.Count > 0)
                    return new Response(_mapper.Map<List<ImpostoImuneDto>>(result.Result));
                return new Response(Message.ErroTrazerTabelaImpposto);
            }
            else if (request.Insento)
            {
                var result = _impostosProdRepository.GetAllImpostosInsento(request.EmpresaId);
                if (result.Result.Count > 0)
                    return new Response(_mapper.Map<List<ImpostoInsentoDto>>(result.Result));
                return new Response(Message.ErroTrazerTabelaImpposto);
            }
            else if (request.Reducao)
            {
                var result = _impostosProdRepository.GetAllImpostosReducao(request.EmpresaId);
                if (result.Result.Count > 0)
                    return new Response(_mapper.Map<List<ImpostoReducaoDto>>(result.Result));
                return new Response(Message.ErroTrazerTabelaImpposto);
            }
            else
            {
                return new Response(Message.ErroTrazerTabelaImpposto);
            }
        }

        public async Task<Response> Handle(UpdateImpIncerramentoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                _ = _impostosProdRepository.UpdateEncerramento(request.Id, request.Status);
                return new Response(Message.OperacaoRealizadaComSucesso);
            }
            catch (Exception)
            {
                return new Response(Message.ErroAoRealizaOperacao);
                throw;
            }
            
        }

        public async Task<Response> Handle(UpdateExigibilidadeRequest request, CancellationToken cancellationToken)
        {
            try
            {
                _ = _impostosProdRepository.UpdateExigibilidadeSus(request.Id, request.Status);
                return new Response(Message.OperacaoRealizadaComSucesso);
            }
            catch (Exception)
            {
                return new Response(Message.ErroAoRealizaOperacao);
                throw;
            }
        }
    }
}
