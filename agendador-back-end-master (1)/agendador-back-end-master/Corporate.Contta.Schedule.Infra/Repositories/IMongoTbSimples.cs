using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities;
using Corporate.Contta.Schedule.Domain.Entities.DashboardAgg;
using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Infra.Repositories
{
    public class IMongoTbSimples
    {
        public (int Param1, double Param2) ObterParametros()
        {

            return (0, 0.0);
        }

        public async Task<(Dashboard, double faturamento12)> GetDashboard(Guid? empresaId, DateTime dhEmi)
        {
            var faturamento12messes = 0.0;
            Dashboard tbSimplesFeramentoCli = new Dashboard();
            try
            {
                var inicioMes = new DateTime(dhEmi.Year, dhEmi.Month, 1);
                var fimMes = inicioMes.AddMonths(1).AddDays(-1);

                IMongoClient mongoClient = new MongoClient("mongodb://contta:contta123456@192.46.218.34:27017/?authSource=admin&readPreference=primary&ssl=false");
                IMongoDatabase database = mongoClient.GetDatabase("conttadb");
                IMongoCollection<CompanyInformation> _collectionTbCompany = database.GetCollection<CompanyInformation>("CompanyInformation");
                IMongoCollection<FaturamentoEmpresa> _collectionTbCompanyFatura = database.GetCollection<FaturamentoEmpresa>("FaturamentoEmpresa");

                var listCompany = _collectionTbCompany.Find(c => c.Cnpj != null).ToList();
                var companyDto = listCompany.Where(c => c.Id.Equals(empresaId)).FirstOrDefault();

                if (companyDto != null)
                {
                    IMongoCollection<Dashboard> _collectionTbSimples = database.GetCollection<Dashboard>("TbSimples");

                    var tbSimples = _collectionTbSimples.Find(c => c.DataEmissaoMensais.Ano == dhEmi.Year && c.DataEmissaoMensais.Mes == dhEmi.Month).ToList();
                    if (tbSimples.Count > 0)
                        tbSimplesFeramentoCli = tbSimples.FirstOrDefault(c => c.CompanyInformation == companyDto.Id && c.ListaNfe.Count > 0);

                    if(tbSimplesFeramentoCli != null)
                    {
                        var tbSimplesSaidaDto = _collectionTbSimples.Find(c => c.FaturamentoMensaisSaida.Valor > 0).ToList();
                        var tbSimplesEntradaDto = _collectionTbSimples.Find(c => c.FaturamentoMensaisEntrada.Valor > 0).ToList();


                        if (tbSimplesEntradaDto.Count > 0)
                            tbSimplesFeramentoCli.ListFaturamentoMensaisEntrada = tbSimplesEntradaDto.Select(c => c.FaturamentoMensaisEntrada).Where(x => x.Valor > 0).ToList();
                        if (tbSimplesSaidaDto.Count > 0)
                            tbSimplesFeramentoCli.ListFaturamentoMensaisSaida = tbSimplesSaidaDto.Select(c => c.FaturamentoMensaisSaida).Where(x => x.Valor > 0).ToList();

                        var faturamentosDto = _collectionTbCompanyFatura.Find(_ => _.CompanyInformation != null).ToList();
                        var faturamentoMesses = faturamentosDto.FirstOrDefault(c => c.CompanyInformation == companyDto.Id);

                        if (tbSimples.Count == 0)
                        {
                            var tbSimplesGeralSimples = _collectionTbSimples.Find(c => c.SimplesNacional.DateFounded != null).FirstOrDefault();
                            if (tbSimplesGeralSimples != null)
                                tbSimplesFeramentoCli.SimplesNacional.DateFounded = tbSimplesGeralSimples.SimplesNacional.DateFounded;
                        }

                        tbSimplesFeramentoCli.SimplesNacional.DateFounded = Convert.ToDateTime(companyDto.Founded);

                        return (tbSimplesFeramentoCli, faturamento12messes);

                    }                   

                    return (tbSimplesFeramentoCli, faturamento12messes);

                }

            }
            catch (Exception ex)
            {
                throw;
            }

            return (tbSimplesFeramentoCli, faturamento12messes);
        }

        public async Task<bool> UpdateData(Guid? empresaId, DateTime dhEmi)
        {

            try
            {
                var inicioMes = new DateTime(dhEmi.Year, dhEmi.Month, 1);
                var fimMes = inicioMes.AddMonths(1).AddDays(-1);

                IMongoClient mongoClient = new MongoClient("mongodb://contta:contta123456@192.46.218.34:27017/?authSource=admin&readPreference=primary&ssl=false");
                IMongoDatabase database = mongoClient.GetDatabase("conttadb");
                IMongoCollection<CompanyInformation> _collectionTbCompany = database.GetCollection<CompanyInformation>("CompanyInformation");
                IMongoCollection<FaturamentoEmpresa> _collectionTbCompanyFatura = database.GetCollection<FaturamentoEmpresa>("FaturamentoEmpresa");

                var listCompany = _collectionTbCompany.Find(c => c.Cnpj != null).ToList();
                var companyDto = listCompany.Where(c => c.Id.Equals(empresaId)).FirstOrDefault();

                if (companyDto != null)
                {
                    IMongoCollection<Dashboard> _collectionTbSimples = database.GetCollection<Dashboard>("TbSimples");

                    var listatbSimples = _collectionTbSimples.Find(c => c.CompanyInformation.Equals(empresaId)).ToList();

                    foreach (var item in listatbSimples)
                    {
                        item.SimplesNacional.DateFounded = dhEmi;
                        var result = _collectionTbSimples.ReplaceOneAsync(b => b.Id == item.Id, item);
                    }

                    return true;

                }

                return false;

            }
            catch (Exception ex)
            {

                throw;
            }


        }

    }
}
