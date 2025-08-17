using AutoMapper;
using Corporate.Contta.Schedule.Application.Mapping.Request;
using Corporate.Contta.Schedule.Application.Mapping.Response;
using Corporate.Contta.Schedule.Application.Mapping.Result.GetInfomationByDocument;
using Corporate.Contta.Schedule.Domain.Entities;
using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using Corporate.Contta.Schedule.Infra.Repositories.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Application.Handler
{
    public class GetInfomationByDocumentHandler : IRequestHandler<GetInfomationByDocumentRequest, GetInfomationByDocumentResponse>,
         IRequestHandler<GetInfomationByDocumentManualRequest, Response>
    {
        private readonly ICompanyGateway _companyGateway;
        private readonly IMapper _mapper;

        public GetInfomationByDocumentHandler(ICompanyGateway companyGateway, IMapper mapper)
        {
            _companyGateway = companyGateway;
            _mapper = mapper;
        }

        public async Task<GetInfomationByDocumentResponse> Handle(GetInfomationByDocumentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Manual)
                {
                    var companyInformation = await _companyGateway.GetCompanyInformationByCnpjDest(request.Document, request.UserId, request.Id);
                    return new GetInfomationByDocumentResponse(_mapper.Map<CompanyInformationDto>(companyInformation));

                }
                if (request.Diretorio != null)
                {
                    await _companyGateway.NewComanyLote(request.Diretorio, request.Token);
                    return new GetInfomationByDocumentResponse(_mapper.Map<CompanyInformationDto>(""));
                }
                else
                {
                    var companyInformation = await _companyGateway.GetCompanyInformationByCnpj(request.Document, request.UserId, request.Id, request.ConfirmarCadastro, request.UserIdTerceiro);
                    return new GetInfomationByDocumentResponse(_mapper.Map<CompanyInformationDto>(companyInformation));
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception($"Empresa já esta cadastrada para outro Usuário Admin{ex}");
            }  
        }

        public async Task<Response> Handle(GetInfomationByDocumentManualRequest request, CancellationToken cancellationToken)
        {
            var companyInformation = await _companyGateway.GetCompanyInformationByCnpjDest(request.Document, request.UserId, request.Id);
            return new Response(companyInformation);
        }
    }
}
