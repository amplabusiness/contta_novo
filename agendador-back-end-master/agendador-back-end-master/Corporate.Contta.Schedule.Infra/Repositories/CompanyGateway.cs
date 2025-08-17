using AutoMapper;
using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities;
using Corporate.Contta.Schedule.Domain.Entities.ConfigurationAdmin;
using Corporate.Contta.Schedule.Domain.Entities.ConfigurationFhAgg;
using Corporate.Contta.Schedule.Domain.Entities.Criticas;
using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using Corporate.Contta.Schedule.Infra.Models.CompanyInformation;
using Corporate.Contta.Schedule.Infra.Repositories.Base;
using Corporate.Contta.Schedule.Infra.Repositories.Interfaces;
using Corporate.Contta.Schedule.Infra.Tools;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Corporate.Contta.Schedule.Domain.Entities.EstoqueAgg;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Corporate.Contta.Schedule.Domain.Entities.UserAgg;

namespace Corporate.Contta.Schedule.Infra.Repositories
{
    public class CompanyGateway : BaseHttp<HttpParam, CompanyInformationModel>, ICompanyGateway
    {
        private readonly IConfiguration _configuration;
        private readonly ISociosRepository _sociosRepository;
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICriticasRepository _criticasRepository;
        private IUserRepository _configurationUserRepository;
        private readonly IConfigurationFhRepository _configurationFhRepository;
        readonly CrawlerConsultaAnexo webs = new CrawlerConsultaAnexo();
        private List<Membership> listMembership = new List<Membership>();

        public CompanyGateway(IConfigurationFhRepository configurationFhRepository,
            ICriticasRepository criticasRepository, IConfiguration configuration,
            IMapper mapper, ICompanyRepository companyRepository,
            IUserRepository configurationUserRepository, ISociosRepository sociosRepository)
        {
            _configuration = configuration;
            _mapper = mapper;
            _companyRepository = companyRepository;
            _criticasRepository = criticasRepository;
            _sociosRepository = sociosRepository;
            _configurationFhRepository = configurationFhRepository;
            _configurationUserRepository = configurationUserRepository;
        }

        public async Task<CompanyInformation> GetCompanyInformationByCnpj(string cnpj, string tokenUser, Guid id, bool ConfirmarCadastro, Guid? useriIdTerceiro)
        {
            try
            {
                List<string> lis = new List<string>();
                List<SecondaryActivities> lisAnexo = new List<SecondaryActivities>();

                var company = await _companyRepository.GetCompanyInformationByCnpj(cnpj);
                string userId = GetUserId(tokenUser);
                string UserComunId = "";

                var user = _configurationUserRepository.GetUserById(new Guid(userId));

                if (user.Result.Group == Domain.Enum.UserGroup.User)
                {
                    userId = user.Result.UserMasterId.ToString();
                    UserComunId = user.Result.Id.ToString();
                }

                if (company == null)
                {
                    company = await GetCompanyInformationOnExternalService(cnpj);

                    listMembership = company.Membership;

                    Thread tSocios = new Thread(InsertSocios);
                    tSocios.Start();

                    if (company != null)
                    {
                        var criticasNovas = new CriticasNovas();
                        var configurationFh = new ConfigurationFh()
                        {
                            FechamentoLivroCaixa = new FechamentoLivroCaixa { CodUltimoEnviou = "0", DataFechamento = DateTime.Now },
                            FechamentoLivroEntrada = new FechamentoLivroEntrada { CodUltimoEnviou = "0", DataFechamento = DateTime.Now },
                            FechamentoLivroSaida = new FechamentoLivroSaida { CodUltimoEnviou = "0", DataFechamento = DateTime.Now },
                            FechamentoSimples = new FechamentoSimples { DataFechamento = DateTime.Now }
                        };

                        var cnae = await webs.GetAnexo(company.PrimaryActivity.Code);
                        company.Anexo.Add(new Domain.Entities.CompanyInformationAgg.Anexo { Descricao = cnae.Replace("(*)", "").Trim() });

                        var qtdSecundaria = company.SecondaryActivities.Count;

                        foreach (var item in company.SecondaryActivities)
                        {
                            var resultAnexo = await webs.GetAnexo(item.Code);
                            lisAnexo.Add(new SecondaryActivities()
                            {
                                Code = item.Code,
                                DescriptionAnexo = resultAnexo,
                                Description = item.Description
                            });
                        }

                        if (company.SecondaryActivities.Count > 0)
                        {
                            company.SecondaryActivities.Clear();
                            company.SecondaryActivities.AddRange(lisAnexo);

                            if (company.SecondaryActivities.Count != qtdSecundaria)
                            {
                                throw new Exception("Erro ao processar cadastro da empresa, tente novamente mais tarde");
                            }
                        }

                        foreach (var item in company.ListUserId)
                        {
                            lis.Add(item);
                        }

                        lis.Add(user.Result.Id.ToString());

                        company.ListUserId.AddRange(lis);
                        company.PrimaryActivity.Anexo = cnae.Replace("(*)", "").Trim();
                        company.UserComunId = UserComunId;
                        company.IntegradoEstoque = "notStarted";
                        if (company.PrimaryActivity.Code != null)
                        {
                            var digAnexo = company.PrimaryActivity.Code.Substring(0, 2);

                            if (company.PrimaryActivity.Description.Contains("Transporte") || digAnexo == "49" || digAnexo == "50" || digAnexo == "51" || digAnexo == "52" || digAnexo == "53")
                            {
                                company.Transportadora = true;
                            }
                            if (!company.PrimaryActivity.Description.Contains("Transporte") && cnae.Replace("(*)", "").Trim() == "Anexo III" || cnae.Replace("(*)", "").Trim() == "Anexo IV" || cnae.Replace("(*)", "").Trim() == "Anexo V")
                            {
                                company.Servico = true;
                            }
                        }

                        var result = await _companyRepository.Insert(company);

                        if (user.Result.Group == Domain.Enum.UserGroup.User)
                            await _configurationUserRepository.UpdateUser(user.Result, result.Id);
                        //Criar por padrão tabelas de criticas e Data Fechamento simples.
                        criticasNovas.CompanyInformation = result.Id;
                        await _criticasRepository.Insert(criticasNovas);

                        configurationFh.CompanyInformation = result.Id;
                        await _configurationFhRepository.Insert(configurationFh);

                        _companyRepository.InserFaturamentoEmp(company.Id);
                        return company;
                    }
                }
                if (company.Active == false)
                {
                    await _companyRepository.Update(company, "");
                }
                else if (company != null && ConfirmarCadastro)
                {
                    var userCOnfig = user.Result.Id;
                    var novaEmpresa = company.ListUserId.Contains(user.Result.Id.ToString());
                    if (novaEmpresa == false)
                        await _companyRepository.Update(company, user.Result.Id.ToString());
                    company.EmpresaCadastrada = false;
                    if (ConfirmarCadastro)
                    {
                        if (user.Result.Group != Domain.Enum.UserGroup.Administrator)
                        {
                            userCOnfig = user.Result.UserMasterId;
                        }
                        await _companyRepository.Update(company, useriIdTerceiro.ToString());
                        var result = await _companyRepository.UpdateConfigurationAdmin(useriIdTerceiro.Value, userCOnfig.Value, false);
                        return company;
                    }

                }
                if (company != null && user.Result.Group == Domain.Enum.UserGroup.Administrator)
                {
                    var listUser = company.ListUserId;
                    var config = new AutorizationAdmin();
                    config.DataRegistro = DateTime.Now;
                    config.EmpresaId = company.Id;
                    config.NomeUsuario = user.Result.Name;
                    config.UserId = user.Result.Id;
                    config.ListUserId = listUser.Where(c => c.Trim() != "").ToList();
                    config.NameClient = company.Name;
                    config.Cnpj = company.Cnpj;
                    await _companyRepository.InsertConfigAdmin(config);

                    throw new Exception("Empresa já esta cadastrada para outro Usuário Admin");
                }

                return company;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void InsertSocios()
        {
            if (listMembership.Count > 0)
            {
                foreach (var item in listMembership)
                {
                    _sociosRepository.InsertSocios(item.CpfSocio, item.Name);
                }
            }
        }

        private static string GetUserId(string tokenUser)
        {
            var jwt = tokenUser;
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt).Payload.Values;
            var userId = "";

            foreach (var item in token)
            {
                userId = item.ToString();
                break;
            }

            return userId;
        }

        public async Task<EmpresaDest> GetCompanyInformationByCnpjDest(string cnpj, string userId, Guid id)
        {
            var company = await _companyRepository.ExistsCompanyDest(cnpj);

            var escricao = "";

            if (company == null)
            {
                var companyDto = await GetCompanyInformationOnExternalService(cnpj);

                var sitegaRegister = companyDto.Sintegra.Registrations;

                if (sitegaRegister.Count > 0)
                {
                    escricao = sitegaRegister.Find(c => c.Number != null).Number;
                }

                var empresaDestDto = new EmpresaDest()
                {
                    Cnpj = companyDto.Cnpj,
                    RazaoSocial = companyDto.Name,
                    Cep = companyDto.Address.Zip,
                    Endereco = companyDto.Address.Details,
                    Bairro = companyDto.Address.Neighborhood,
                    Cidade = companyDto.Address.City,
                    Uf = companyDto.Address.State,
                    IncrEstadual = escricao

                };

                if (companyDto != null)
                {
                    companyDto.ListUserId.Add(userId);
                    await _companyRepository.InsertDest(empresaDestDto);
                }

                return empresaDestDto;
            }

            return company;
        }

        public async Task NewComanyLote(string diretorio, string tokenUser)
        {
            try
            {               

                var listCnpj = GetListaCnpj(diretorio);

                foreach (var item in listCnpj)
                {
                    await CreateCompanyListCnpj(tokenUser, item);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task CreateCompanyListCnpj(string tokenUser, string item)
        {
            List<string> lis = new List<string>();
            List<SecondaryActivities> lisAnexo = new List<SecondaryActivities>();
            CompanyInformation company = new CompanyInformation();
            User user = new User();
            var qtdSecundaria = 0;
            var resultAnexo = "";
            SecondaryActivities secudar = new SecondaryActivities();

            company = await  _companyRepository.GetCompanyInformationByCnpj(item);
            string userId = GetUserId(tokenUser);
            string UserComunId = "";

            user = await _configurationUserRepository.GetUserById(new Guid(userId));

            if (user.Group == Domain.Enum.UserGroup.User)
            {
                userId = user.UserMasterId.ToString();
                UserComunId = user.Id.ToString();
            }

            if (company == null)
            {
                company = await GetCompanyInformationOnExternalService(item);

                listMembership = company.Membership;
                Thread tSocios = new Thread(InsertSocios);
                tSocios.Start();

                if (company != null)
                {
                    var cnae = await webs.GetAnexo(company.PrimaryActivity.Code);
                    company.Anexo.Add(new Domain.Entities.CompanyInformationAgg.Anexo { Descricao = cnae.Replace("(*)", "").Trim() });

                    qtdSecundaria = company.SecondaryActivities.Count;

                    foreach (var itemSeco in company.SecondaryActivities)
                    {
                        resultAnexo = await webs.GetAnexo(itemSeco.Code);

                        secudar = lisAnexo.Where(c => c.Code.Equals(itemSeco.Code)).FirstOrDefault();

                        if (secudar == null)
                        {
                            lisAnexo.Add(new SecondaryActivities()
                            {
                                Code = itemSeco.Code,
                                DescriptionAnexo = resultAnexo,
                                Description = itemSeco.Description
                            });
                        }
                    }

                    if (company.SecondaryActivities.Count > 0)
                    {
                        company.SecondaryActivities.Clear();
                        company.SecondaryActivities.AddRange(lisAnexo);

                        if (company.SecondaryActivities.Count != qtdSecundaria)
                        {
                          
                        }
                    }

                    foreach (var itemLis in company.ListUserId)
                    {
                        lis.Add(itemLis);
                    }

                    lis.Add(user.Id.ToString());

                    company.ListUserId.AddRange(lis);
                    company.PrimaryActivity.Anexo = cnae.Replace("(*)", "").Trim();
                    company.UserComunId = UserComunId;
                    company.IntegradoEstoque = "notStarted";
                    if (company.PrimaryActivity.Code != null && company.PrimaryActivity.Description.Contains("Transporte"))
                    {
                        var digAnexo = company.PrimaryActivity.Code.Substring(0, 2);

                        if (company.PrimaryActivity.Description.Contains("Transporte") || digAnexo == "49" || digAnexo == "50" || digAnexo == "51" || digAnexo == "52" || digAnexo == "53")
                        {
                            company.Transportadora = true;
                        }
                        if (!company.PrimaryActivity.Description.Contains("Transporte") && cnae.Replace("(*)", "").Trim() == "Anexo III" || cnae.Replace("(*)", "").Trim() == "Anexo IV" || cnae.Replace("(*)", "").Trim() == "Anexo V")
                        {
                            company.Servico = true;
                        }
                    }

                    var result = await _companyRepository.Insert(company);
                    _companyRepository.InserFaturamentoEmp(company.Id);

                    var criticasNovas = new CriticasNovas();
                    var configurationFh = new ConfigurationFh()
                    {
                        FechamentoLivroCaixa = new FechamentoLivroCaixa { CodUltimoEnviou = "0", DataFechamento = DateTime.Now },
                        FechamentoLivroEntrada = new FechamentoLivroEntrada { CodUltimoEnviou = "0", DataFechamento = DateTime.Now },
                        FechamentoLivroSaida = new FechamentoLivroSaida { CodUltimoEnviou = "0", DataFechamento = DateTime.Now },
                        FechamentoSimples = new FechamentoSimples { DataFechamento = DateTime.Now }
                    };

                    if (user.Group == Domain.Enum.UserGroup.User)
                        await _configurationUserRepository.UpdateUser(user, result.Id);
                    //Criar por padrão tabelas de criticas e Data Fechamento simples.
                    criticasNovas.CompanyInformation = result.Id;
                    await _criticasRepository.Insert(criticasNovas);

                    configurationFh.CompanyInformation = result.Id;
                    await _configurationFhRepository.Insert(configurationFh);
                }
            }
        }

        private async Task<CompanyInformation> GetCompanyInformationOnExternalService(string cnpj)
        {
            var apiUrl = $"https://api.cnpja.com.br/companies/{cnpj}?company_max_age=1&sintegra_max_age=1&simples_max_age=1";
            var cnpjServiceToken = _configuration["AppSettings:CnpjServiceToken"];

            var param = new HttpParam(apiUrl, cnpjServiceToken);

            var result = await GetWithAuthorization(param);

            var companyInformation = _mapper.Map<CompanyInformation>(result);

            return companyInformation;
        }


        public List<string> GetListaCnpj(string diretory)
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            List<string> listCnpj = new List<string>();

            try
            {
                var bytes = File.ReadAllBytes(diretory);
                var stream = new MemoryStream(bytes);
                var package = new ExcelPackage(stream);
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;
                var colCount = worksheet.Dimension.Columns;
                var header = ExcelUtils.CellsToArray(worksheet, 1, colCount);

                for (var row = 2; row <= rowCount; row++)
                {
                    var data = ExcelUtils.CellsToArray(worksheet, row, colCount);

                    foreach (var item in data.ToList())
                    {
                        if (item != "-" && item != null)
                        {
                            if (item.Contains("/"))
                            {
                                var cnpj = item.Trim();
                                cnpj = item.Replace(".", "").Replace("/", "").Replace("-", "");

                                if (cnpj.Length == 14)
                                {
                                    listCnpj.Add(cnpj);
                                }
                                else
                                {

                                }

                            }
                        }
                    }
                }

                return listCnpj;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<Estoque> GetListaEstoque(string diretory)
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            List<Estoque> listEstoque = new List<Estoque>();

            try
            {
                var bytes = File.ReadAllBytes(diretory);
                var stream = new MemoryStream(bytes);
                var package = new ExcelPackage(stream);
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;
                var colCount = worksheet.Dimension.Columns;
                var header = ExcelUtils.CellsToArray(worksheet, 1, colCount);

                for (var row = 2; row <= rowCount; row++)
                {
                    var data = ExcelUtils.CellsToArray(worksheet, row, colCount);
                    var qtd = 0;
                    var EstoqueDto = new Estoque();

                    foreach (var item in data.ToList())
                    {
                        if (item != null)
                        {
                            if (qtd == 0)
                                EstoqueDto.CodProd = item;
                            else if (qtd == 1)
                                EstoqueDto.Descricao = item;
                            else if (qtd == 2)
                                EstoqueDto.Marca = item;
                            else if (qtd == 3)
                                EstoqueDto.VlUnitario = item.Trim();
                            else if (qtd == 4)
                                EstoqueDto.UniMedida = item;
                            else if (qtd == 5)
                            {
                                bool ehValido = item.All(char.IsDigit);
                                if (ehValido)
                                    EstoqueDto.Quantidade = (decimal.Parse(item));
                                else
                                    EstoqueDto.Quantidade = 0;
                            }


                            else if (qtd == 6)
                                EstoqueDto.CodBarra = item;

                            qtd = qtd + 1;
                        }
                    }

                    EstoqueDto.Id = Guid.NewGuid();
                    listEstoque.Add(EstoqueDto);
                }

                return listEstoque;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static class ExcelUtils
        {
            public static string[] CellsToArray(ExcelWorksheet worksheet, int row, int colCount)
            {
                var array = new string[colCount];
                for (int col = 0; col < colCount; col++) array[col] = worksheet.Cells[row, (col + 1)].Value?.ToString();
                return array;
            }

            public static string GetData(string prop, string[] header, string[] data) => header
                .Zip(data, (h, d) => new KeyValuePair<string, string>(h, d))
                .FirstOrDefault(d => d.Key.Equals(prop))
                .Value;
        }


    }
}
