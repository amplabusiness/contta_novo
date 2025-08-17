using Corporate.Contta.Schedule.Application.Contracts.Implements;
using Corporate.Contta.Schedule.Application.Contracts.Repositories;
using Corporate.Contta.Schedule.Application.Email;
using Corporate.Contta.Schedule.Application.Handler;
using Corporate.Contta.Schedule.Application.Mapping.Request;
using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Infra.Repositories;
using Corporate.Contta.Schedule.Infra.Repositories.Interfaces;
using Corporate.Contta.Schedule.Infra.Services.Contracts;
using Corporate.Contta.Schedule.Infra.Services.Implements;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Corporate.Contta.Schedule.Api
{
    public static class NativeInjectorConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {           
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserApplication, UserApplication>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICompanyGateway, CompanyGateway>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IUserContextAcessorRepository, UserContextAcessorRepository>();
            services.AddScoped<INfeRepository, NfeRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();        
            services.AddScoped<IApuracaoRepository, ApuracaoRepository>();
            services.AddScoped<IEstoqueRepository, EstoqueRepository>();
            services.AddScoped<IEmpresaEmitRepository, EmpresaEmitRepository>();
            services.AddScoped<IEmpresaDestRepository, EmpresaDestRepository>();            
            services.AddScoped<INotificationUserRepository, NotificationRepository>();            
            services.AddScoped<IConfigurationFhRepository, ConfigurationFhRepository>();            
            services.AddScoped<IConfigurationUserRepository, ConfigurationUserRepository>();            
            services.AddScoped<IDashboardHomeRepository, DashboardHomeRepository>();            
            services.AddScoped<ICriticasRepository, CriticasNovasRepository>();
            services.AddScoped<IImpostosRepository, ImpostosRepository>();
            services.AddScoped<IDetalhamentoApuracaoRepository, DetalhamentoApuracaoRepository>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IImpostosProdutosRepository, ImpostoProductRepository>();
            services.AddScoped<ISociosRepository, SociosRepository>();
            services.AddScoped<IAgBlocoERepository, AgBlocoERepository>();
            //Handlers
            services.AddMediatR(typeof(GetInfomationByDocumentHandler).Assembly);
          

            // ASP.NET httpContext dependencyu
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
