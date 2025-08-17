using AutoMapper;
using Corporate.Contta.Schedule.Application.Mapping;
using Corporate.Contta.Schedule.Application.Mapping.Dto;
using Corporate.Contta.Schedule.Application.Mapping.Dto.CompanyInformation;
using Corporate.Contta.Schedule.Application.Mapping.Dto.Configuration;
using Corporate.Contta.Schedule.Application.Mapping.Dto.Dashboard;
using Corporate.Contta.Schedule.Application.Mapping.Dto.Dashboard.Apuracao;
using Corporate.Contta.Schedule.Application.Mapping.Dto.ImpostosProduct;
using Corporate.Contta.Schedule.Application.Mapping.Dto.Nfe;
using Corporate.Contta.Schedule.Application.Mapping.Dto.Notification;
using Corporate.Contta.Schedule.Application.Mapping.Dto.User;
using Corporate.Contta.Schedule.Application.Mapping.Param;
using Corporate.Contta.Schedule.Application.Mapping.Request;
using Corporate.Contta.Schedule.Application.Mapping.Request.Impostos;
using Corporate.Contta.Schedule.Application.Mapping.Response;
using Corporate.Contta.Schedule.Domain.Entities;
using Corporate.Contta.Schedule.Domain.Entities.BlocoE;
using Corporate.Contta.Schedule.Domain.Entities.CompanyInformationAgg;
using Corporate.Contta.Schedule.Domain.Entities.Configuration;
using Corporate.Contta.Schedule.Domain.Entities.ConfigurationFhAgg;
using Corporate.Contta.Schedule.Domain.Entities.Criticas;
using Corporate.Contta.Schedule.Domain.Entities.DashboardAgg;
using Corporate.Contta.Schedule.Domain.Entities.DashboardAgg.Apuracoes;
using Corporate.Contta.Schedule.Domain.Entities.ExternalTable;
using Corporate.Contta.Schedule.Domain.Entities.FullNfeAgg;
using Corporate.Contta.Schedule.Domain.Entities.ImpostoAgg;
using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using Corporate.Contta.Schedule.Domain.Entities.NotificationAgg;
using Corporate.Contta.Schedule.Domain.Entities.Product;
using Corporate.Contta.Schedule.Domain.Entities.TbServico;
using Corporate.Contta.Schedule.Domain.Entities.UserAgg;
using Corporate.Contta.Schedule.Infra.Models;
using Corporate.Contta.Schedule.Infra.Models.CompanyInformation;
using MongoDB.Bson;

namespace Corporate.Contta.Schedule.Api.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CompanyInformationModel, CompanyInformation>();
            CreateMap<CompanyInformationModel, CompanyInformationSocios>();
            CreateMap<AddressModel, Address>();
            CreateMap<FilesModel, Files>();
            CreateMap<LegalNatureModel, LegalNature>();
            CreateMap<MembershipModel, Membership>();
            CreateMap<PrimaryActivityModel, PrimaryActivity>();
            CreateMap<RegistrationModel, Registration>();
            CreateMap<RegistrationsModel, Registrations>();
            CreateMap<RoleModel, Role>();
            CreateMap<SecondaryActivitiesModel, SecondaryActivities>();
            CreateMap<SimplesNacionalMode, SimplesNacional>();
            CreateMap<SintegraModel, Sintegra>();
            CreateMap<NotificationModel, Notification>();
            CreateMap<ConfigurationUserModel, ConfigurationUser>();
            CreateMap<Socios<ObjectId>, SociosDto>();


            CreateMap<ImpostoAntecipacao, AntecipacaoDto>();
            CreateMap<TbCodServico, TbServicoDto>();
            CreateMap<ImpostoExigibilidadeSus, ExigibilidadeDto>();
            CreateMap<ImpostoImune, ImpostoImuneDto>();
            CreateMap<ImpostoInsento, ImpostoInsentoDto>();
            CreateMap<ImpostoRedCestaBasica, CestaBasicaDto>();
            CreateMap<ImpostoReducao, ImpostoReducaoDto>();


            CreateMap<Notification, NotificationDto>();
            CreateMap<ConfigurationFh, ConfigurationFhDto>();
            CreateMap<Reclassificacao, ReclassificacaoDto>();
            CreateMap<Reclassificacaofornec, ReclassificacaoFornecDto>();
            CreateMap<CompanyInformation, CompanyInformationDto>();
            CreateMap<FaturamentoEmpresa, FaturamentoEmpresaDto>();
            CreateMap<Address, AddressDto>();
            CreateMap<Files, FilesDto>();
            CreateMap<LegalNature, LegalNatureDto>();
            CreateMap<Membership, MembershipDto>();
            CreateMap<PrimaryActivity, PrimaryActivityDto>();
            CreateMap<Registration, RegistrationDto>();
            CreateMap<Registrations, RegistrationsDto>();
            CreateMap<Role, RoleDto>();
            CreateMap<SecondaryActivities, SecondaryActivitiesDto>();
            CreateMap<SimplesNacional, SimplesNacionalDto>();
            CreateMap<Sintegra, SintegraDto>();
            CreateMap<ConfigurationUser, ConfigurationUserDto>();
            CreateMap<User, UserDTO>();
            CreateMap<CompanySummaryInformation, CompanySummaryInformationDto>();
            CreateMap<CompanyInformation, CompanySummaryInformationDto>();
            CreateMap<NFE, NfeDto>();
            CreateMap<Dashboard, DashboardDto>();
            CreateMap<Apuracao, ApuracaoDto>();
            CreateMap<ApuracaoCte, ApuracaoCteDto>();
            CreateMap<ApuracaoService, ApuracaoServicoDto>();

            CreateMap<Produtos, ProductDto>();


            //CreateMap<NewCompanySociosRequest, CompanyInformationSocios>();

            CreateMap<NewImpostoAntecipacaoRequest, ImpostoAntecipacao>();
            CreateMap<NewCriticasNovasRequest, CriticasNovas>();
            CreateMap<NewUserRequest, User>();
            CreateMap<NewCompanyRequest, CompanyInformation>();
            CreateMap<NewNotificationRequest, Notification>();
            CreateMap<NewConfigurationFhRequest, ConfigurationFh>();
            CreateMap<NewConfigurationUserRequest, ConfigurationUser>();
            CreateMap<NewExternalTableRequest, Domain.Entities.ExternalTable.IcmsSt>();
            CreateMap<NewFaturamento12Request, FaturamentoEmpresa>();


            CreateMap<NewNfVendaManualRequest, NfVendaManual>();
            CreateMap<TaxesDto, Taxes>();
            CreateMap<ProductsDto, Products>();
            CreateMap<ReceiverDto, Receiver>();
            CreateMap<NfeCanceladasDto, NfeCanceldas>();
            CreateMap<NfeCanceldas, NfeCanceladasDto>();

            CreateMap<RetornoNfeCanceladasDto, RetornoNfeCancelada>();
            CreateMap<RetornoNfeCancelada, RetornoNfeCanceladasDto>();


            CreateMap<NewNfServicoManualRequest, NfServicoManual>();
            CreateMap<TakerDto, Taker>();
            CreateMap<ActivityDto, Activity>();
            CreateMap<FederalRetentionsDto, FederalRetentions>();
            CreateMap<DemonstrativeDto, Demonstrative>();
            CreateMap<TaxCalculationDto, TaxCalculation>();

            CreateMap<UpdateCompanyRequest, CompanyInformation>();
            CreateMap<UpdateUserRequest, User>().ConstructUsing(c => new User(c.Id));
            CreateMap<UpdateNotificationRequest, Notification>();
            CreateMap<UpdateConfigurationFhRequest, ConfigurationFh>();
            CreateMap<UpdateProdutoRequest, Produtos>();
            CreateMap<UpdateConfigurationRequest, ConfigurationUser>();

            CreateMap<UpdateImpIncerramentoRequest, ImpostoRedCestaBasica>();
            CreateMap<UpdateExigibilidadeRequest, ImpostoExigibilidadeSus>();

            CreateMap<GetCriticasNovasRequest, CriticasNovas>();
            CreateMap<GetConfigurationFhUserRequest, ConfigurationFh>();

            CreateMap<DetalhamentoImposto, DetalhamentoImpostoDto>();
            CreateMap<DetalhamentoApuracao, DetalhamentoApuracaoDto>();
            CreateMap<DetalhamentoIcmsSt, DetalhamentoIcmsStDto>();
            CreateMap<DetalhamentoPisConfins, DetalhamentoPisConfinsDto>();
            CreateMap<DetalhamentoNfeGeral, DetalhamentoNfeGeralDto>();

            CreateMap<Domain.Entities.DashboardAgg.Apuracoes.IcmsSt, IcmsStDto>();
            CreateMap<Beneficios, BeneficiosDto>();
            CreateMap<Isento, IsentoDto>();
            CreateMap<Imune, ImuneDto>();
            CreateMap<ExigSuspensa, ExigSuspensaDto>();
            CreateMap<LancamentoOficio, LancamentoOficioDto>();
            CreateMap<IsencaoReducao, IsencaoReducaoDto>();
            CreateMap<IsencaoReducaoCestaBasica, IsencaoReducaoCestaBasicaDto>();
            CreateMap<DetalhamentoApuracao, DetalhamentoApuracaoDto>();
            CreateMap<AntEncTributacao, AntEncTributacaoDto>();


            CreateMap<NcmMono, NcmMonoDto>();
            CreateMap<ExigSuspensaMono, ExigSuspensaMonoDto>();
            CreateMap<SubTributariaMono, SubTributariaMonoDto>();
            CreateMap<LancamentoOficioMono, LancamentoOficioMonoDto>();

            CreateMap<Canceladas, CanceladasDto>();
            CreateMap<Devolucao, DevolucaoDto>();
            CreateMap<TransferenciaMercadoria, TransferenciaMercadoriaDto>();

            CreateMap<AgrupamentoDetalhamentoApuracao, AgrupamentoDetalhamentoApuracaoDto>();
            CreateMap<DevolucaoTransferenciaDetalhamentoApuracao, DevolucaoTransferenciaDetalhamentoApuracaoDto>();
            CreateMap<IcmsStPisConfinsDetalhamentoApuracao, IcmsStPisConfinsDetalhamentoApuracaoDto>();
            CreateMap<NotaFiscalCanceladaDetalhamentoApuracao, NotaFiscalCanceladaDetalhamentoApuracaoDto>();

            CreateMap<Response, AgrupamentoDetalhamentoApuracaoDto>();

            CreateMap<FileProduct, FileProductDto>();
            CreateMap<FileProductDto, FileProduct>();

            CreateMap<AgBlocoE, AgBlocoERequest>();
            CreateMap<AgBlocoERequest, AgBlocoE>();

            CreateMap<E100, E100Request>();
            CreateMap<E110, E110Request>();
            CreateMap<E111, E111Request>();
            CreateMap<E113, E113Request>();
            CreateMap<E115, E115Request>();
            CreateMap<E116, E116Request>();

            CreateMap<E100Request, E100>();
            CreateMap<E110Request, E110>();
            CreateMap<E111Request, E111>();
            CreateMap<E113Request, E113>();
            CreateMap<E115Request, E115>();
            CreateMap<E116Request, E116>();

            CreateMap<TotalizadorNfeSaida, TotalizadorNfeSaidaDto>();
        }
    }
}
