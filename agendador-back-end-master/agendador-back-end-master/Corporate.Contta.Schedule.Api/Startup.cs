using AutoMapper;
using Corporate.Contta.Schedule.Api.Configuration;
using Corporate.Contta.Schedule.Application.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System;

namespace Corporate.Contta.Schedule.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        private AppSettings Settings => Configuration.Get<AppSettings>();
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(o => Configuration.Bind(o));
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
                        builder.WithOrigins("https://portal.173.255.209.202").AllowAnyHeader().AllowAnyMethod();
                        builder.WithOrigins("173.255.209.202:27017").AllowAnyHeader().AllowAnyMethod();
                        builder.WithOrigins("https://portal-simples-git-develop-conttaampla.vercel.app").AllowAnyHeader().AllowAnyMethod();

                        //builder.WithOrigins("portal-simples.vercel.app").AllowAnyHeader().AllowAnyMethod();

                    });
            });

            services.AddControllers();
            services.AddHealthChecks();

            IMapper mapper = AutoMapperConfig.GetMapperConfiguration().CreateMapper();
            services.AddSingleton(mapper);

            services.RegisterServices();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        //ValidateIssuerSigningKey = true,
                        //IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Contta Agendador API",
                    Description = "Consulta CNPJ API",
                    TermsOfService = new Uri("https://contta.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Josu Corra",
                        Email = "josuejtc@gmail.com",
                        Url = new Uri("https://contta.com/spboyer"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://contta.com/license"),
                    }
                });
            });            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

      
            //app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            //app.UseCors("ApiCorsPolicy");
           

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contta Schedule API V1");
            });

            app.UseSerilogRequestLogging();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapControllers();
            });
        }
    }
}
