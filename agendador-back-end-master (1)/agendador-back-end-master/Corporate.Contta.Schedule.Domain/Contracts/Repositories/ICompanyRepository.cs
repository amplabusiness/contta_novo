using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Corporate.Contta.Schedule.Domain.Entities;
using Corporate.Contta.Schedule.Domain.Entities.CompanyInformationAgg;
using Corporate.Contta.Schedule.Domain.Entities.ConfigurationAdmin;
using Corporate.Contta.Schedule.Domain.Entities.DashboardAgg;
using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;

namespace Corporate.Contta.Schedule.Domain.Contracts.Repositories
{
    public interface ICompanyRepository
    {
        Task<CompanyInformation> GetCompanyInformationByCnpj(string cnpj);
        Task<CompanyInformationSocios> GetCompanyInformationByCnpjSocios(string cnpj);
        Task<bool> ExistsCompany(Guid id);
        Task<bool> ExistsCompany(string cnpj);
        Task<EmpresaDest> ExistsCompanyDest(string cnpj);
        Task<List<CompanyInformation>> GetCampanySummaryInformationByMasterId(Guid masterId);
        Task<CompanyInformation> Insert(CompanyInformation companyInformation);
        Task InsertDest(EmpresaDest companyInformation);
        Task InsertConfigAdmin(AutorizationAdmin autorizationAdmin);
        Task<bool> Update(CompanyInformation companyInformation, string userId);
        Task<CompanyInformation> UpdateAnexo(List<Anexo> anexo, Guid empresaId);
        Task<bool> Delete(CompanyInformation companyInformation, Guid? userId = null);
        Task<CompanyInformation> GetById(Guid id);

        Task<List<CompanyInformation>> GetAll(string userId);
        Task<CompanyInformationSocios> GetAllCompanySocios(string userId, string cnpj);
        Task<FaturamentoEmpresa> GetAllFaturamento(Guid empresaId, DateTime dataPeriudo);
        Task<List<CompanyInformation>> GetAllDisabled();
           
        Task<bool> UpdateFaturamento(FaturamentoEmpresa faturamentoEmpresas);
        Task<bool> UpdateFaturamentoCorreto(FaturamentoEmpresa faturamentoEmpresas);

        Task<AutorizationAdmin> GetConfigurationAdmin(Guid UserId);
        Task<bool> UpdateConfigurationAdmin(Guid UserId, Guid autorizationAdminId, bool desativar);

        Task<bool> UpdateDataCompany(Guid EmpresaId, DateTime dataNova);

        Task<CompanyInformationSocios> InsertCompanySocios(string cnpj, string userId);

        Task InserFaturamentoEmp(Guid? EmpresaId);
        Task<FaturamentoEmpresa> GetAllFaturamentoEmp(Guid empresaId, DateTime dataEmissao);

        Task<FaturamentoEmpresaFechada> GetAllFaturamentoEmpFechadas(Guid empresaId, DateTime dataEmissao);
        Task<FaturamentoEmpresaFechada> NewFaturamentoEmp(FaturamentoEmpresaFechada faturamentoEmpresa);
        Task<FaturamentoEmpresaFechada> UpdateFaturamentoEmpre(FaturamentoEmpresa faturamentoEmpresas, bool faturamentoFechado);

        Task<FaturamentoEmpresaFechada> GetAllFaturamentoEmpCompente(FaturamentoEmpresaFechada faturamentoEmpresas, DateTime dataEmissao);
    }
}
