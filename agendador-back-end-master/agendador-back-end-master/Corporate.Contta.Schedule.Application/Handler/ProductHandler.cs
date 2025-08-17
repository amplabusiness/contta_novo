using AutoMapper;
using Corporate.Contta.Schedule.Application.Mapping.Dto.ImpostosProduct;
using Corporate.Contta.Schedule.Application.Mapping.Dto.Nfe;
using Corporate.Contta.Schedule.Application.Mapping.Request;
using Corporate.Contta.Schedule.Application.Mapping.Request.Impostos;
using Corporate.Contta.Schedule.Application.Mapping.Response;
using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities.EstoqueAgg;
using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using Corporate.Contta.Schedule.Domain.Entities.Product;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Application.Handler
{
    public class ProductHandler : IRequestHandler<GetAllByProductIdCompanyRequest, Response>,
        IRequestHandler<GetByIdNfeProductRequest, Response>,
        IRequestHandler<GetByIdProductRequest, Response>,
        IRequestHandler<UpdateProdutoRequest, Response>,   
        IRequestHandler<GetCodProductRequest, Response>,
        IRequestHandler<UpdateProdIcmsMonoRequest, Response>
    {

        private IProductRepository _repository;
        private IMapper _mapper;
        public ProductHandler(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _repository = productRepository;
        }

        public async Task<Response> Handle(GetAllByProductIdCompanyRequest request, CancellationToken cancellationToken)
        {
            if (request.DeparaProd)
            {
                var result = await _repository.GetAllProdForn(request.EmpresaId);

                return new Response(_mapper.Map<List<ProdutosFornecedor>>(result));
            }
            else if (request.Monofasico)
            {
                if (request.Alterado)
                {
                    var result = await _repository.GetAllNcmMonoAlterado(request.EmpresaId, request.DateEmiss);
                    return new Response(_mapper.Map<List<ProductDto>>(result));
                }
                else
                {
                    var result = await _repository.GetAllNcmMono(request.EmpresaId,request.DateEmiss);
                    return new Response(_mapper.Map<List<ProductDto>>(result));
                }    
              
            }
            else if (request.IcmsSt)
            {
                if (request.Alterado)
                {
                    var result = await _repository.GetAllIcmsStAlterado(request.EmpresaId,request.DateEmiss);

                    return new Response(_mapper.Map<List<ProductDto>>(result));

                }
                else
                {
                    var result = await _repository.GetAllIcmsSt(request.EmpresaId,request.DateEmiss);

                    return new Response(_mapper.Map<List<ProductDto>>(result));
                }
               
            }
            else if (request.Beneficio)
            {
                var result = await _repository.GetAllBeneficio(request.EmpresaId);

                return new Response(_mapper.Map<List<ProductDto>>(result));
            }
            else if (request.Insento)
            {
                var result = await _repository.GetAllInsento(request.EmpresaId);

                return new Response(_mapper.Map<List<ProductDto>>(result));
            }
            else if (request.Imune)
            {
                var result = await _repository.GetAllImune(request.EmpresaId);

                return new Response(_mapper.Map<List<ProductDto>>(result));
            }
            else if (request.Insencao)
            {
                var result = await _repository.GetAllInsencao(request.EmpresaId);

                return new Response(_mapper.Map<List<ProductDto>>(result));
            }
            else if (request.InsencaoCesta)
            {
                var result = await _repository.GetAllInsencaoCesta(request.EmpresaId);

                return new Response(_mapper.Map<List<ProductDto>>(result));
            }
            else
            {
                var result = await _repository.GetAll(request.EmpresaId);

                return new Response(_mapper.Map<List<ProductDto>>(result));
            }
        }

        public async Task<Response> Handle(GetByIdNfeProductRequest request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllByNfe(request.NfeId);
            return new Response(_mapper.Map<List<ProductDto>>(result));
        }

        public async Task<Response> Handle(GetByIdProductRequest request, CancellationToken cancellationToken)
        {
            if(request.Modelo == "Venda")
            {
                var result = await _repository.GetById(request.EmpresaId, request.NfeId);
                return new Response(_mapper.Map<ReclassificacaoDto>(result));
            }
            else
            {
                var result = await _repository.GetByIdFornec(request.EmpresaId, request.NfeId);
                return new Response(_mapper.Map<ReclassificacaoFornecDto>(result));
            }            
        }

        public async Task<Response> Handle(GetCodProductRequest request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetProd(request.Document, request.CodProd,request.DescProd);
            return new Response(_mapper.Map<List<Estoque>>(result));
        }

        public async Task<Response> Handle(UpdateProdutoRequest request, CancellationToken cancellationToken)
        {
            if (request.Depara)
            {
                var result = await _repository.UpdateDepara(request.CodProFornecedor,request.CodProCliente, request.EmpresaId, request.Marca);
                if (!result)
                    return new Response(Message.ErroAoRealizaOperacao);
                return new Response(Message.OperacaoRealizadaComSucesso);
            }
            else if (request.Entrada)
            {
                var result = await _repository.UpdateFornec(request.ListProdutosFornect, request.EmpresaId);
                if (!result)
                    return new Response(Message.ErroAoRealizaOperacao);
                return new Response(Message.OperacaoRealizadaComSucesso);
            }
            else
            {
                var result = await _repository.Update(request.ListProdutos, request.EmpresaId);
                if (!result)
                    return new Response(Message.ErroAoRealizaOperacao);
                return new Response(Message.OperacaoRealizadaComSucesso);
            }

        }     

        public async Task<Response> Handle(UpdateProdIcmsMonoRequest request, CancellationToken cancellationToken)
        {
            var result = await _repository.UpdateImpostos(request);
            if (!result)
                return new Response(Message.ErroAoRealizaOperacao);
            return new Response(Message.OperacaoRealizadaComSucesso);
        }
    }
}
