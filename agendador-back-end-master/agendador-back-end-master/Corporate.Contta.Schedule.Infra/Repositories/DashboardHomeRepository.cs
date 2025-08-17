using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities;
using Corporate.Contta.Schedule.Domain.Entities.CertificateAgg;
using Corporate.Contta.Schedule.Domain.Entities.DashboardAgg;
using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using Corporate.Contta.Schedule.Domain.Entities.UserAgg;
using Corporate.Contta.Schedule.Infra.Repositories.Base;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Corporate.Contta.Schedule.Infra.Repositories
{
    public class DashboardHomeRepository : BaseRepository<HomeCompany>, IDashboardHomeRepository
    {
        private static MongoDBContext<HomeCompany> _dbContext = new MongoDBContext<HomeCompany>();
        private static MongoDBContext<CompanyInformation> _dbContextCompany = new MongoDBContext<CompanyInformation>();
        private static MongoDBContext<Certificado> _dbContextCertificado = new MongoDBContext<Certificado>();
        private static MongoDBContext<NFE> _dbContextNfe = new MongoDBContext<NFE>();
        private static MongoDBContext<User> _dbContextUser = new MongoDBContext<User>();

        public DashboardHomeRepository() : base(_dbContext) { }

        public List<HomeCompany> GetAllHome(Guid userId, DateTime dhEmi)
        {
            List<HomeCompany> listHomeCompanies = new List<HomeCompany>();

            var builder = Builders<CompanyInformation>.Filter;
            var builderCertficado = Builders<Certificado>.Filter;
            var builderNfe = Builders<NFE>.Filter;
            var listcompanyInformation = new List<CompanyInformation>();

            var listaCOmpnayDto = _dbContextCompany.GetColection.Find(_ => _.Active).ToList();
            var user = _dbContextUser.GetColection.Find(c => c.Id.Equals(userId)).FirstOrDefault();

            try
            {
                if (user.Group == Domain.Enum.UserGroup.Administrator)
                    listcompanyInformation = listaCOmpnayDto.Where(c => c.ListUserId.Contains(userId.ToString())).ToList();
                else
                {
                    foreach (var item in user.CompanyId)
                    {
                        var empresaDto = listaCOmpnayDto.Where(c => c.Id.Equals(item.Value)).FirstOrDefault();
                        if (empresaDto != null)
                            listcompanyInformation.Add(empresaDto);
                    }
                }

                foreach (var item in listcompanyInformation)
                {
                    var filterCert = builderCertficado.Eq(certificado => certificado.CNPJ, item.Cnpj);
                    var validCertificado = _dbContextCertificado.GetColection.Find(filterCert).FirstOrDefault();

                    var filterNfe = builderNfe.Eq(nfe => nfe.CompanyInformation, item.Id);

                    var nfesDoCorrenteAno = _dbContextNfe.GetColection.Find(filterNfe).ToList();

                    if (validCertificado != null)

                        listHomeCompanies.Add(new HomeCompany
                        {
                            RazaoSocial = item.Name,
                            ValiCertificado = validCertificado.NonBefore,
                            //FaturamentoMes = faturamentoDto.Sum(c => c.VtTotalNfe)

                        });
                }

                return listHomeCompanies;

            }
            catch (Exception ex)
            {

                throw;
            }            

        }

    }
}
