using AutoMapper;
using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities;
using Corporate.Contta.Schedule.Domain.Entities.CompanyInformationAgg;
using Corporate.Contta.Schedule.Domain.Entities.ConfigurationAdmin;
using Corporate.Contta.Schedule.Domain.Entities.DashboardAgg;
using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using Corporate.Contta.Schedule.Domain.Entities.UserAgg;
using Corporate.Contta.Schedule.Infra.Models.CompanyInformation;
using Corporate.Contta.Schedule.Infra.Repositories.Base;
using Corporate.Contta.Schedule.Infra.Tools;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Infra.Repositories
{
    public class CompanyRepository : BaseHttp<HttpParam, CompanyInformationModel>, ICompanyRepository
    {
        private static MongoDBContext<CompanyInformationSocios> _dbContextSocios = new MongoDBContext<CompanyInformationSocios>();
        private static MongoDBContext<CompanyInformation> _dbContext = new MongoDBContext<CompanyInformation>();
        private static MongoDBContext<EmpresaDest> _dbContextDest = new MongoDBContext<EmpresaDest>();
        private static MongoDBContext<FaturamentoEmpresa> _dbContextFaturamento = new MongoDBContext<FaturamentoEmpresa>();
        private static MongoDBContext<AutorizationAdmin> _dbContextCOnfigAdmin = new MongoDBContext<AutorizationAdmin>();
        private static MongoDBContext<FaturamentoEmpresaFechada> _dbContextFaturamentoEmpresaFechada = new MongoDBContext<FaturamentoEmpresaFechada>();


        private static MongoDBContext<User> _dbContextUser = new MongoDBContext<User>();

        private IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CompanyRepository(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> Delete(CompanyInformation companyInformation, Guid? userId = null)
        {
            //Admin
            var users = companyInformation.ListUserId;
            bool status = false;
            if (userId != null)
            {
                var user = _dbContextUser.GetColection.Find(c => c.Id == userId).FirstOrDefault();
                if (user != null)
                {
                    if (user.Group != Domain.Enum.UserGroup.Administrator)
                    {
                        status = true;
                        users.Remove(user.Id.ToString());
                    }
                }
            }
            var update = Builders<CompanyInformation>.Update.Set(c => c.Active, status)
                                                            .Set(c => c.ListUserId, users);
            var updateResult = await _dbContext.GetColection.UpdateOneAsync(c => c.Id == companyInformation.Id, update);

            var userDto = _dbContextUser.GetColection.Find(c => c.Id.Equals(userId)).FirstOrDefault();
            if (userDto != null)
            {
                if (userDto.CompanyId.Count > 0)
                {
                    userDto.CompanyId.Remove(userId);

                    var updateUser = Builders<User>.Update.Set(c => c.CompanyId, userDto.CompanyId);
                    var updateResultTeste = await _dbContextUser.GetColection.UpdateOneAsync(c => c.Id == userDto.Id, updateUser);
                }
            }

            return updateResult.ModifiedCount > 0;
        }

        public async Task<bool> ExistsCompany(string cnpj)
        {
            var result = await _dbContext.GetColection.FindAsync(c => c.Cnpj.Equals(cnpj)).ConfigureAwait(false); ;
            return result.Any();
        }

        public async Task<List<CompanyInformation>> GetAllDisabled()
        {
            var result = await _dbContext.GetColection.FindAsync(c => c.Active == false).ConfigureAwait(false);
            return result.ToList();
        }

        public async Task<CompanyInformation> GetById(Guid id)
        {
            var result = await _dbContext.GetColection.FindAsync(c => c.Id == id).ConfigureAwait(false);

            return await result.FirstOrDefaultAsync();
        }

        public async Task<List<CompanyInformation>> GetCampanySummaryInformationByMasterId(Guid masterId)
        {
            var user = await _userRepository.GetUserById(masterId);
            var companies = user.Companies;

            var lisUser = _userRepository.GetUsersByMasterId(masterId);

            var listUsuario = _dbContextUser.GetColection.FindAsync(c => c.UserMasterId == masterId).Result.ToList();

            var listCompanies = new List<CompanyInformation>();
            companies.ForEach(x =>
            {
                //var result = await _dbContext.GetColection.Find({}, {"_id":0, "Cnpj":1, "Name":1})
                var result = _dbContext.GetColection.Find(c => c.Cnpj == x.Document).FirstOrDefault();
                listCompanies.Add(result);
            });

            return listCompanies;
        }

        public async Task<CompanyInformation> GetCompanyInformationByCnpj(string cnpj)
        {

            var result = await _dbContext.GetColection.FindAsync(c => c.Cnpj.Equals(cnpj)).ConfigureAwait(false);

            return await result.FirstOrDefaultAsync();

        }

        public async Task<CompanyInformation> Insert(CompanyInformation companyInformation)
        {
            if (!companyInformation.Id.HasValue)
            {
                companyInformation.Id = Guid.NewGuid();
            }

            await _dbContext.GetColection.InsertOneAsync(companyInformation).ConfigureAwait(false);
            return companyInformation;

        }

        public async Task<bool> Update(CompanyInformation companyInformation, string userId)
        {
            var companyDto = _dbContext.GetColection.Find(c => c.Id.Equals(companyInformation.Id)).FirstOrDefault();


            if (companyDto != null)
            {
                if (userId != null)
                    companyDto.ListUserId.Add(userId);

                if (companyInformation.Tipo == 1)
                    companyDto.SimplesNacional = companyInformation.SimplesNacional;
                if (companyInformation.Tipo == 2)
                {
                    companyDto.RegimeBox = companyInformation.RegimeBox;
                    companyDto.RegimeCompetence = companyInformation.RegimeCompetence;
                }
                if (companyInformation.Active == false)
                    companyDto.Active = true;
                if (companyInformation.IntegradoEstoque == "pending")
                {
                    companyDto.IntegradoEstoque = "pending";
                }
                if (companyInformation.IntegradoEstoque == "success")
                {
                    companyDto.IntegradoEstoque = "success";
                }
                var update = Builders<CompanyInformation>.Update.Set(c => c.Name, companyDto.Name)
                                                  .Set(c => c.NameFantasy, companyDto.NameFantasy)
                                                  .Set(c => c.Active, companyDto.Active)
                                                  .Set(c => c.StateRegistration, companyDto.StateRegistration)
                                                  .Set(c => c.Alias, companyDto.Alias)
                                                  .Set(c => c.Cnpj, companyDto.Cnpj)
                                                  .Set(c => c.Type, companyDto.Type)
                                                  .Set(c => c.Founded, companyDto.Founded)
                                                  .Set(c => c.Regime, companyDto.Regime)
                                                  .Set(c => c.Size, companyDto.Size)
                                                  .Set(c => c.Capital, companyDto.Capital)
                                                  .Set(c => c.Email, companyDto.Email)
                                                  .Set(c => c.Anexo, companyDto.Anexo)
                                                  .Set(c => c.Phone, companyDto.Phone)
                                                  .Set(c => c.FederalEntity, companyDto.FederalEntity)
                                                  .Set(c => c.Registration, companyDto.Registration)
                                                  .Set(c => c.Address, companyDto.Address)
                                                  .Set(c => c.LegalNature, companyDto.LegalNature)
                                                  .Set(c => c.PrimaryActivity, companyDto.PrimaryActivity)
                                                  .Set(c => c.SecondaryActivities, companyDto.SecondaryActivities)
                                                  .Set(c => c.Membership, companyDto.Membership)
                                                  .Set(c => c.Files, companyDto.Files)
                                                  .Set(c => c.Sintegra, companyDto.Sintegra)
                                                  .Set(c => c.SimplesNacional, companyDto.SimplesNacional)
                                                  .Set(c => c.ListUserId, companyDto.ListUserId)
                                                  .Set(c => c.RegimeBox, companyDto.RegimeBox)
                                                  .Set(c => c.RegimeCompetence, companyDto.RegimeCompetence)
                                                  .Set(c => c.IntegradoEstoque, companyDto.IntegradoEstoque)
                                                  .Set(c => c.MunicipalRegistration, companyDto.MunicipalRegistration);

                var updateResult = await _dbContext.GetColection.UpdateOneAsync(c => c.Id == companyDto.Id, update);

                if (updateResult.ModifiedCount > 0)
                {
                    return true;
                }

                return false;
            }

            return false;
        }

        public async Task<List<CompanyInformation>> GetAll(string userId)
        {
            try
            {
                var listCompany = new List<CompanyInformation>();

                var listaCOmpnayDto = _dbContext.GetColection.Find(_ => _.Active).ToList();

                var user = _dbContextUser.GetColection.Find(c => c.Id == new Guid(userId)).FirstOrDefault();
                var result = listaCOmpnayDto.Where(_ => _.ListUserId.Contains(userId) && _.Active).ToList();
                if (result.Count != 0)
                {
                    listCompany.AddRange(result);
                }
                if (user.Group == Domain.Enum.UserGroup.User)
                {
                    foreach (var item in user.CompanyId)
                    {
                        var empresaDto = listaCOmpnayDto.Where(c => c.Id.Equals(item.Value)).FirstOrDefault();
                        if (empresaDto != null)
                            listCompany.Add(empresaDto);

                    }

                }

                return listCompany;

                //if (user.Group == Domain.Enum.UserGroup.User && user.CompanyId.Count > 0)
                //{
                //    foreach (var item in user.CompanyId)
                //    {
                //        var resultUser = _dbContext.GetColection.Find(_ => true && _.Id.Equals(item.Value) && _.Active).FirstOrDefault();

                //        var empresaUser = _dbContext.GetColection.Find(c => c.ListUserId.Contains(item.Value))

                //        listCompany.Add(resultUser);
                //    };

                //    var empresaCadUser = _dbContext.GetColection.Find(_ => true && _.UserComunId.Equals(userId) && _.Active).ToList();
                //    if (empresaCadUser.Count > 0)
                //        listCompany.AddRange(empresaCadUser);
                //    return listCompany;
                //}
                //else
                //{
                //    var result = _dbContext.GetColection.Find(_ => true && _.ListUserId.Contains(userId) && _.Active).ToList();
                //    return result;
                //}
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<bool> ExistsCompany(Guid id)
        {
            try
            {
                var result = _dbContext.GetColection.Find(c => c.Id.Equals(id)).FirstOrDefault();
                if (result != null)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task InsertDest(EmpresaDest companyInformation)
        {
            if (!companyInformation.Id.HasValue)
            {
                companyInformation.Id = Guid.NewGuid();
            }

            await _dbContextDest.GetColection.InsertOneAsync(companyInformation).ConfigureAwait(false);
        }

        public async Task<EmpresaDest> ExistsCompanyDest(string cnpj)
        {
            var result = await _dbContextDest.GetColection.FindAsync(c => c.Cnpj.Equals(cnpj)).ConfigureAwait(false); ;
            return result.FirstOrDefault();
        }

        public async Task<CompanyInformation> UpdateAnexo(List<Anexo> anexo, Guid empresaId)
        {

            var companyDto = _dbContext.GetColection.Find(c => c.Id.Equals(empresaId)).FirstOrDefault();

            if (anexo.Count > 0)
            {
                anexo.ForEach(c => companyDto.Anexo.Add(c));
            }

            var update = Builders<CompanyInformation>.Update.Set(c => c.Anexo, companyDto.Anexo);

            var updateResult = await _dbContext.GetColection.UpdateOneAsync(c => c.Id == companyDto.Id, update);

            if (updateResult.ModifiedCount > 0)
            {
                return companyDto;
            }
            else
            {
                return null;
            }
        }

        public async Task<FaturamentoEmpresa> GetAllFaturamento(Guid empresaId, DateTime dataPeriudo)
        {
            List<string> Lista = new List<string>();

            var faturamentoEmpresa = new FaturamentoEmpresa();
            var listFaturamentoDto = new List<Faturamento>();

            var faturamento = _dbContextFaturamento.GetColection.Find(c => c.CompanyInformation.Equals(empresaId)).FirstOrDefault();


            if (faturamento != null)
            {
                if (faturamento.FaturamentoFechado)
                {
                    return faturamento;
                }
            }
            else
            {
                var valorFaturamento12 = 0.0;

                for (int i = 0; i <= 12; i++)
                {
                    if (i != 0)
                    {
                        Lista.Add(dataPeriudo.AddMonths(-i).ToString("MM/yyyy"));
                    }
                }

                foreach (var data in Lista)
                {
                    var dataDto = Convert.ToDateTime(data);

                    if (faturamento == null)
                    {
                        listFaturamentoDto.Add(new Faturamento()
                        {
                            DataFaturamento = dataDto,
                            ValorFaturamento = 0,
                            Ano = dataDto.Year
                        });
                    }
                    else
                    {
                        var faturamentoAtual = faturamento.Faturamentos.Where(c => c.DataFaturamento.Year == dataDto.Year && c.DataFaturamento.Month == dataDto.Month).FirstOrDefault();

                        if (faturamentoAtual != null)
                        {
                            listFaturamentoDto.Add(new Faturamento()
                            {
                                DataFaturamento = dataDto,
                                ValorFaturamento = faturamentoAtual.ValorFaturamento,
                                Ano = dataDto.Year,
                                Id = faturamentoAtual.Id

                            });
                        }
                        else
                        {
                            listFaturamentoDto.Add(new Faturamento()
                            {
                                DataFaturamento = dataDto,
                                ValorFaturamento = 0,
                                Ano = dataDto.Year,
                                Id = faturamento.Id,

                            });
                        }
                    }
                }

                if (faturamento == null)
                {
                    var faturamentoEmpresaDtoNull = new FaturamentoEmpresa()
                    {
                        FaturamentoFechado = false,
                        Id = Guid.Empty,
                        TotalAnual = 0,
                        CompanyInformation = empresaId,
                        Faturamentos = listFaturamentoDto,
                    };

                    return faturamentoEmpresaDtoNull;
                }

                var faturamentoEmpresaDto = new FaturamentoEmpresa()
                {
                    FaturamentoFechado = faturamento.FaturamentoFechado,
                    Id = faturamento.Id,
                    TotalAnual = faturamento.TotalAnual,
                    CompanyInformation = empresaId,
                    Faturamentos = listFaturamentoDto,
                };

                if (listFaturamentoDto.Count > 0)
                {
                    if (faturamento == null)
                    {
                        faturamentoEmpresaDto.FaturamentoFechado = false;
                        faturamentoEmpresaDto.TotalAnual = 0;
                        faturamentoEmpresaDto.CompanyInformation = empresaId;
                    }
                    var faturamentoAnual = faturamentoEmpresaDto.Faturamentos.Any(c => c.ValorFaturamento == 0);

                    if (faturamentoAnual)
                    {
                        faturamentoEmpresaDto.FaturamentoFechado = false;
                        faturamentoEmpresaDto.TotalAnual = 0;
                    }
                    else
                    {
                        faturamentoEmpresaDto.FaturamentoFechado = true;
                        faturamentoEmpresaDto.TotalAnual = faturamentoEmpresaDto.Faturamentos.Sum(c => c.ValorFaturamento);
                    }
                }
            }

            return faturamentoEmpresa;
        }

        public async Task<FaturamentoEmpresaFechada> UpdateFaturamentoEmpre(FaturamentoEmpresa faturamentoEmpresas, bool faturamentoFechado)
        {
            var faturamentosFechado = new List<Faturamento>();
            var faturamentosEmpresa = new FaturamentoEmpresaFechada();
            var faturamentos = new List<Faturamento>();

            try
            {
                if (faturamentoFechado)
                {
                    var listaFaturamento = _dbContextFaturamentoEmpresaFechada.GetColection.Find(c => c.FaturamentoFechado && c.CompanyInformation == faturamentoEmpresas.CompanyInformation).ToList();

                    var listFaturamentoDto = listaFaturamento.Select(c => c.Faturamentos).ToList();

                    foreach (var item in faturamentoEmpresas.Faturamentos)
                    {
                        foreach (var fat in listFaturamentoDto)
                        {
                            var teste = fat.ToList();

                            foreach (var feature in teste)
                            {
                                if (item.Id == feature.Id)
                                {
                                    var result = faturamentosFechado.Where(c => c.DataFaturamento.Year == item.DataFaturamento.Year && c.DataFaturamento.Month == item.DataFaturamento.Month).FirstOrDefault();
                                    if (result == null)
                                        faturamentosFechado.Add(item);
                                }
                            }
                        }
                    }

                    var faturamentoId = listaFaturamento.Select(c => c.Id).ToList();

                    foreach (var item in faturamentoId)
                    {

                        faturamentoEmpresas.TotalAnual = faturamentosFechado.Sum(c => c.ValorFaturamento);
                        var updateFechamento = Builders<FaturamentoEmpresaFechada>.Update.Set(c => c.Faturamentos, faturamentosFechado)
                                                                                  .Set(c => c.TotalAnual, faturamentoEmpresas.TotalAnual);

                        var updateResultFechamento = await _dbContextFaturamentoEmpresaFechada.GetColection.UpdateOneAsync(c => c.Id == item, updateFechamento);

                        if (updateResultFechamento.ModifiedCount > 0)
                        {

                        }
                    }

                    faturamentosEmpresa = new FaturamentoEmpresaFechada
                    {
                        Id = faturamentoEmpresas.Id,
                        CompanyInformation = faturamentoEmpresas.CompanyInformation,
                        FaturamentoFechado = true,
                        TotalAnual = faturamentoEmpresas.TotalAnual,
                        Faturamentos = faturamentosFechado
                    };

                    return faturamentosEmpresa;

                }
                else
                {

                    var listFechamento = _dbContextFaturamento.GetColection.Find(c => c.CompanyInformation == faturamentoEmpresas.CompanyInformation).ToList();

                    var listFaturamentos = listFechamento.Select(c => c.Faturamentos).ToList();

                    foreach (var item in faturamentoEmpresas.Faturamentos)
                    {
                        foreach (var fat in listFaturamentos)
                        {
                            var teste = fat.ToList();

                            foreach (var feature in teste)
                            {
                                if (item.Id == feature.Id)
                                {
                                    faturamentos.Add(item);
                                }
                                else
                                {
                                    faturamentos.Add(feature);
                                }
                            }
                        }
                    }

                    var totalAnual = faturamentos.Sum(c => c.ValorFaturamento);

                    var update = Builders<FaturamentoEmpresa>.Update.Set(c => c.CompanyInformation, faturamentoEmpresas.CompanyInformation)
                                                                    .Set(c => c.TotalAnual, totalAnual)
                                                                    .Set(c => c.Faturamentos, faturamentos);

                    var updateResult = await _dbContextFaturamento.GetColection.UpdateOneAsync(c => c.Id == faturamentoEmpresas.Id, update);

                    if (updateResult.ModifiedCount > 0)
                    {
                        faturamentosEmpresa = new FaturamentoEmpresaFechada
                        {
                            CompanyInformation = faturamentoEmpresas.CompanyInformation,
                            FaturamentoFechado = true,
                            TotalAnual = faturamentos.Sum(c => c.ValorFaturamento),
                            Faturamentos = faturamentos
                        };

                        return faturamentosEmpresa;
                    }
                }

                return faturamentosEmpresa;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<bool> UpdateFaturamentoCorreto(FaturamentoEmpresa faturamentoEmpresa)
        {
            var Faturamento = new List<Faturamento>();

            var faturamento = _dbContextFaturamento.GetColection.Find(c => c.CompanyInformation == faturamentoEmpresa.CompanyInformation).FirstOrDefault();


            var listaFaturamentoDto = faturamento.Faturamentos.ToList();

            var listaFaturamento = faturamentoEmpresa.Faturamentos.ToList();


            foreach (var item in listaFaturamento)
            {
                var teste = listaFaturamentoDto.RemoveAll(c => c.Id == item.Id);
            }

            foreach (var item in listaFaturamentoDto)
            {
                Faturamento.Add(item);
            }

            foreach (var item in listaFaturamento)
            {
                Faturamento.Add(item);
            }



            var update = Builders<FaturamentoEmpresa>.Update.Set(c => c.Faturamentos, Faturamento)
                                                             .Set(c => c.FaturamentoFechado, faturamentoEmpresa.FaturamentoFechado);

            var updateResult = await _dbContextFaturamento.GetColection.UpdateOneAsync(c => c.Id == faturamento.Id, update);

            if (updateResult.ModifiedCount > 0)
            {
                return true;
            }

            return false;

        }

        public async Task InsertConfigAdmin(AutorizationAdmin autorizationAdmin)
        {
            var restul = _dbContextCOnfigAdmin.GetColection.Find(c => c.Cnpj == autorizationAdmin.Cnpj).FirstOrDefault();
            if (restul == null)
            {
                autorizationAdmin.Id = Guid.NewGuid();
                autorizationAdmin.Ativo = false;
                _dbContextCOnfigAdmin.GetColection.InsertOne(autorizationAdmin);
            }
        }

        public async Task<bool> UpdateConfigurationAdmin(Guid UserId, Guid autorizationAdminId, bool ativo)
        {
            var user = _userRepository.GetUsersByMaster(UserId);

            var status = false;

            if (user.Result != null)
            {
                var config = _dbContextCOnfigAdmin.GetColection.Find(c => c.UserId == UserId).FirstOrDefault();

                if (ativo == true)
                {
                    status = true;
                }

                var update = Builders<AutorizationAdmin>.Update.Set(c => c.Ativo, status)
                                                          .Set(c => c.UserIdAutorizate, UserId.ToString());

                var updateResult = await _dbContextCOnfigAdmin.GetColection.UpdateOneAsync(c => c.Id == config.Id, update);

                if (updateResult.ModifiedCount > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<AutorizationAdmin> GetConfigurationAdmin(Guid UserId)
        {
            //var user = _userRepository.GetUsersByMaster(UserId);
            
            var restul = _dbContextCOnfigAdmin.GetColection.Find(c => c.ListUserId.Contains(UserId.ToString()) && c.Ativo == true).FirstOrDefault();           
            return restul;
        }

        public async Task<bool> UpdateDataCompany(Guid EmpresaId, DateTime dataNova)
        {
            try
            {
                var company = _dbContext.GetColection.Find(c => c.Id.Equals(EmpresaId)).FirstOrDefault();

                if (company != null)
                {
                    var update = Builders<CompanyInformation>.Update.Set(c => c.Founded, dataNova);

                    var updateResult = await _dbContext.GetColection.UpdateOneAsync(c => c.Id == company.Id, update);

                    if (updateResult.ModifiedCount > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw;
            }


            return false;
        }

        public async Task<CompanyInformationSocios> InsertCompanySocios(string cnpj, string userId)
        {
            try
            {
                List<string> lis = new List<string>();

                var company = await GetCompanyInformationByCnpjSocios(cnpj);

                var user = _userRepository.GetUserById(new Guid(userId));
                string UserComunId = "";

                if (user.Result.Group == Domain.Enum.UserGroup.User)
                {
                    userId = user.Result.UserMasterId.ToString();
                    UserComunId = user.Result.Id.ToString();
                }
                else
                {
                    UserComunId = user.Id.ToString();
                }

                if (company == null)
                {
                    company = await GetCompanyInformationOnExternalService(cnpj);

                    if (company != null)
                    {
                        foreach (var item in company.ListUserId)
                        {
                            lis.Add(item);
                        }

                        lis.Add(user.Result.Id.ToString());

                        company.ListUserId.AddRange(lis);
                        company.UserComunId = userId;
                        company.IntegradoEstoque = "notStarted";

                        company.Id = Guid.NewGuid();

                        _dbContextSocios.GetColection.InsertOne(company);

                        return company;
                    }
                }

                return company;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private async Task<CompanyInformationSocios> GetCompanyInformationOnExternalService(string cnpj)
        {
            var apiUrl = $"https://api.cnpja.com.br/companies/{cnpj}";
            var cnpjServiceToken = _configuration["AppSettings:CnpjServiceToken"];

            var param = new HttpParam(apiUrl, cnpjServiceToken);

            var result = await GetWithAuthorization(param);

            var companyInformation = _mapper.Map<CompanyInformationSocios>(result);

            return companyInformation;
        }

        public async Task<CompanyInformationSocios> GetCompanyInformationByCnpjSocios(string cnpj)
        {
            var result = await _dbContextSocios.GetColection.FindAsync(c => c.Cnpj.Equals(cnpj)).ConfigureAwait(false);

            return result.FirstOrDefault();
        }

        public async Task<CompanyInformationSocios> GetAllCompanySocios(string userId, string cnpj)
        {

            var listaEmpresaSocios = new CompanyInformationSocios();
            try
            {
                return listaEmpresaSocios = _dbContextSocios.GetColection.Find(c => c.UserComunId.Equals(userId) && c.Cnpj == cnpj).FirstOrDefault();

            }
            catch (Exception)
            {

                throw;
            }

        }

        #region

        public async Task InserFaturamentoEmp(Guid? empresaId)
        {
            List<string> Lista = new List<string>();
            List<int> Ano = new List<int>();
            var faturamentoEmpresaDto = new FaturamentoEmpresa();
            var listFaturamentoDto = new List<Faturamento>();

            var dataPeriudo = DateTime.Now;

            var qtdAnos = dataPeriudo.Year - 5;

            try
            {
                var dataMod = dataPeriudo.AddMonths(+5);
                var qtd = 0;
                for (int i = 0; i <= 4; i++)
                {
                    var ultimosAnos = dataPeriudo.Year - i;
                    Ano.Add(ultimosAnos);
                }


                for (int i = 0; i <= 12; i++)
                {
                    if (i != 0)
                    {
                        Lista.Add(dataMod.AddMonths(-i).ToString("MM/yyyy"));
                    }
                }

                foreach (var itemAno in Ano)
                {
                    foreach (var item in Lista)
                    {
                        var dataDto = Convert.ToDateTime(item);

                        listFaturamentoDto.Add(new Faturamento()
                        {
                            Id = Guid.NewGuid(),
                            DataFaturamento = dataDto,
                            ValorFaturamento = 0,
                            Ano = itemAno
                        });
                    }

                    faturamentoEmpresaDto = new FaturamentoEmpresa
                    {
                        CompanyInformation = empresaId,
                        FaturamentoFechado = false,
                        TotalAnual = 0,
                        Faturamentos = listFaturamentoDto,
                        Id = Guid.NewGuid()
                    };

                    await _dbContextFaturamento.GetColection.InsertOneAsync(faturamentoEmpresaDto).ConfigureAwait(false);

                    qtd = qtd + 1;
                    if (qtd == 1)
                        Lista = Lista.Select(c => c.Replace("2021", "2020")).ToList();
                    if (qtd == 2)
                        Lista = Lista.Select(c => c.Replace("2020", "2019")).ToList();
                    if (qtd == 3)
                        Lista = Lista.Select(c => c.Replace("2019", "2018")).ToList();
                    if (qtd == 4)
                        Lista = Lista.Select(c => c.Replace("2018", "2017")).ToList();

                    listFaturamentoDto.Clear();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<FaturamentoEmpresa> GetAllFaturamentoEmp(Guid empresaId, DateTime dataEmissao)
        {
            List<string> lista = new List<string>();
            var listFaturamentoDto = new List<Faturamento>();
            var faturamentoEmpresaDto = new FaturamentoEmpresa();

            var inicioMes = new DateTime(dataEmissao.Year, dataEmissao.Month, 1);
            var fimMes = inicioMes.AddMonths(1).AddDays(-1);

            try
            {
                var listFaturamentoEmp = _dbContextFaturamento.GetColection.Find(c => c.CompanyInformation.Equals(empresaId)).ToList();

                for (int i = 0; i <= 12; i++)
                {
                    if (i != 0)
                    {
                        lista.Add(dataEmissao.AddMonths(-i).ToString("MM/yyyy"));
                    }

                }


                var listaFat = listFaturamentoEmp.Select(c => c.Faturamentos).ToList();

                foreach (var fatura in listaFat)
                {
                    var ListFaut = fatura.ToList();

                    foreach (var item in lista)
                    {
                        var data = Convert.ToDateTime(item);

                        foreach (var ft in ListFaut)
                        {
                            if (ft.DataFaturamento.Year == data.Year && ft.DataFaturamento.Month == data.Month)
                            {
                                var listaExist = listFaturamentoDto.Where(c => c.Id == ft.Id).FirstOrDefault();

                                if (listaExist != null)
                                {
                                    break;
                                }
                                else
                                {
                                    listFaturamentoDto.Add(ft);
                                    break;

                                }


                            }
                        }
                    }
                }

                faturamentoEmpresaDto = new FaturamentoEmpresa
                {
                    CompanyInformation = empresaId,
                    FaturamentoFechado = false,
                    TotalAnual = listFaturamentoDto.Sum(c => c.ValorFaturamento),
                    Faturamentos = listFaturamentoDto
                };


                return faturamentoEmpresaDto;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<FaturamentoEmpresaFechada> GetAllFaturamentoEmpFechadas(Guid empresaId, DateTime dataEmissao)
        {
            List<string> lista = new List<string>();
            var listFaturamentoDto = new List<Faturamento>();
            var faturamentoEmpresaDto = new FaturamentoEmpresaFechada();

            var inicioMes = new DateTime(dataEmissao.Year, dataEmissao.Month, 1);
            var fimMes = inicioMes.AddMonths(1).AddDays(-1);

            try
            {
                var listFaturamentoEmp = _dbContextFaturamentoEmpresaFechada.GetColection.Find(c => c.CompanyInformation.Equals(empresaId)).ToList();

                for (int i = 0; i <= 12; i++)
                {
                    if (i != 0)
                    {
                        lista.Add(dataEmissao.AddMonths(-i).ToString("MM/yyyy"));
                    }
                }

                var listaFat = listFaturamentoEmp.Select(c => c.Faturamentos).ToList();

                if (listaFat.Count > 0)
                {


                    foreach (var fatura in listaFat)
                    {
                        var ListFaut = fatura.ToList();

                        foreach (var item in lista)
                        {
                            var data = Convert.ToDateTime(item);

                            foreach (var ft in ListFaut)
                            {
                                var teste = ListFaut.Where(c => c.Ano == data.Year && c.DataFaturamento.Month == data.Month).FirstOrDefault();

                                if (teste != null)
                                {
                                    if (ft.DataFaturamento.Year == data.Year && ft.DataFaturamento.Month == data.Month)
                                    {
                                        var listaExist = listFaturamentoDto.Where(c => c.DataFaturamento.Year == ft.DataFaturamento.Year && c.DataFaturamento.Month == ft.DataFaturamento.Month).FirstOrDefault();

                                        if (listaExist != null)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            listFaturamentoDto.Add(ft);
                                            break;

                                        }
                                    }

                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }

                    faturamentoEmpresaDto = new FaturamentoEmpresaFechada
                    {
                        CompanyInformation = empresaId,
                        FaturamentoFechado = true,
                        TotalAnual = listFaturamentoDto.Sum(c => c.ValorFaturamento),
                        Faturamentos = listFaturamentoDto
                    };
                }
                else
                {
                    return null;
                }


                return faturamentoEmpresaDto;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<FaturamentoEmpresaFechada> NewFaturamentoEmp(FaturamentoEmpresaFechada faturamentoEmpresa)
        {
            try
            {
                List<string> lista = new List<string>();
                var listFaturamentoDto = new List<Faturamento>();
                var faturamentoEmpresaDto = new FaturamentoEmpresaFechada();

                faturamentoEmpresa.FaturamentoFechado = true;
                faturamentoEmpresa.Id = Guid.NewGuid();
                faturamentoEmpresa.TotalAnual = faturamentoEmpresa.Faturamentos.Sum(C => C.ValorFaturamento);
                faturamentoEmpresa.Faturamentos.ForEach(c => c.Id = Guid.NewGuid());

                _dbContextFaturamentoEmpresaFechada.GetColection.InsertOne(faturamentoEmpresa);

                return faturamentoEmpresa;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Task<bool> UpdateFaturamento(FaturamentoEmpresa faturamentoEmpresas)
        {
            throw new NotImplementedException();
        }

        public async Task<FaturamentoEmpresaFechada> GetAllFaturamentoEmpCompente(FaturamentoEmpresaFechada faturamentoEmpresas, DateTime dataEmissao)
        {
            List<string> lista = new List<string>();
            var listFaturamentoDto = new List<Faturamento>();
            var faturamentoEmpresaDto = new FaturamentoEmpresaFechada();

            var fechamentoList = new FaturamentoEmpresa()
            {
                CompanyInformation = faturamentoEmpresas.CompanyInformation,
                FaturamentoFechado = false,
                TotalAnual = 0,
                Faturamentos = listFaturamentoDto
            };

            var listFaturamentoEmp = _dbContextFaturamento.GetColection.Find(c => c.CompanyInformation.Equals(faturamentoEmpresas.CompanyInformation)).ToList();

            for (int i = 0; i <= 12; i++)
            {
                if (i != 0)
                {
                    lista.Add(dataEmissao.AddMonths(-i).ToString("MM/yyyy"));
                }

            }

            var listaFat = faturamentoEmpresas.Faturamentos.ToList();

            foreach (var fatura in listaFat)
            {
                var ListFaut = fatura;

                foreach (var item in lista)
                {
                    var data = Convert.ToDateTime(item);

                    if (fatura.DataFaturamento.Year == data.Year && fatura.DataFaturamento.Month == data.Month)
                    {
                        var listaExist = listFaturamentoDto.Where(c => c.Id == fatura.Id).FirstOrDefault();

                        if (listaExist != null)
                        {
                            break;
                        }
                        else
                        {
                            listFaturamentoDto.Add(fatura);
                            break;
                        }
                    }

                }
            }

            var listaFatTerceiro = listFaturamentoEmp.Select(c => c.Faturamentos).ToList();

            foreach (var fatura in listaFatTerceiro)
            {
                var ListFaut = fatura.ToList();

                foreach (var item in lista)
                {
                    var data = Convert.ToDateTime(item);

                    foreach (var ft in ListFaut)
                    {
                        if (ft.DataFaturamento.Year == data.Year && ft.DataFaturamento.Month == data.Month)
                        {
                            var listaExist = listFaturamentoDto.Where(c => c.DataFaturamento.Year == ft.DataFaturamento.Year &&
                            c.DataFaturamento.Month == ft.DataFaturamento.Month).FirstOrDefault();

                            if (listaExist != null)
                            {
                                break;
                            }
                            else
                            {
                                listFaturamentoDto.Add(ft);
                                break;

                            }
                        }
                    }
                }
            }

            faturamentoEmpresaDto = new FaturamentoEmpresaFechada
            {
                CompanyInformation = faturamentoEmpresas.CompanyInformation,
                FaturamentoFechado = false,
                TotalAnual = listFaturamentoDto.Sum(c => c.ValorFaturamento),
                Faturamentos = listFaturamentoDto.OrderByDescending(c => c.DataFaturamento).ToList()
            };

            return faturamentoEmpresaDto;
        }

        #endregion
    }
}
