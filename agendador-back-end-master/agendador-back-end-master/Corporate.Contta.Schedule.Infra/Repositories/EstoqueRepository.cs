using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities;
using Corporate.Contta.Schedule.Domain.Entities.EstoqueAgg;
using Corporate.Contta.Schedule.Domain.Entities.NotificationAgg;
using Corporate.Contta.Schedule.Domain.Entities.UserAgg;
using Corporate.Contta.Schedule.Domain.Enum;
using Corporate.Contta.Schedule.Infra.Repositories.Base;
using Corporate.Contta.Schedule.Infra.Repositories.Interfaces;
using MongoDB.Driver; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Infra.Repositories
{
    public class EstoqueRepository : BaseRepository<Estoque>, IEstoqueRepository
    {
        private static MongoDBContext<Estoque> _dbContext = new MongoDBContext<Estoque>();
        private static MongoDBContext<User> _dbContextUser = new MongoDBContext<User>();
        private static MongoDBContext<Notification> _dbContextNotification = new MongoDBContext<Notification>();
        private static MongoDBContext<CompanyInformation> _dbContextCompany = new MongoDBContext<CompanyInformation>();
        private readonly ICompanyRepository _companyRepository;
        private readonly ICompanyGateway _companyGateway;

        public EstoqueRepository(ICompanyRepository company, ICompanyGateway companyGateway) : base(_dbContext) 
        { _companyRepository = company;

            _companyGateway = companyGateway;
        }

        public void AddEstoque(string diretorio, Guid companyId)
        {
            var notification = new Notification();
            var companyDto = _dbContextCompany.GetColection.Find(c => c.Id.Equals(companyId)).FirstOrDefault();
            var result = _dbContext.GetColection.Find(c => c.CompanyInformation.Equals(companyId)).FirstOrDefault();

            var entity = _companyGateway.GetListaEstoque(diretorio);

            if (companyDto != null)
            {
                if (entity.Count > 0)
                {
                    companyDto.IntegradoEstoque = "pending";
                     _companyRepository.Update(companyDto, "");

                   entity.ForEach(c => c.Id = Guid.NewGuid());
                    entity.ForEach(c => c.CompanyInformation = companyDto.Id);
                    _dbContext.GetColection.InsertMany(entity);

                    notification.Id = Guid.NewGuid();
                    notification.Active = true;
                    notification.CodNotification = NotificationType.Lista_de_estoque_cadastradas_com_sucesso;
                    notification.Description = "Tabela Estoque Cadastrada Com Sucesso!";
                    notification.EmpresaId = companyId;
                    notification.Result = "Success";
                    notification.RegisterDate = DateTime.Now;                    
                    _dbContextNotification.GetColection.InsertOne(notification);

                    companyDto.IntegradoEstoque = "success";
                    _companyRepository.Update(companyDto, "");

               
                }               
            }           
        }

        public async Task<List<Estoque>> GetAllEstoque(Guid companyId)
        {
            var result = _dbContext.GetColection.Find(c => c.CompanyInformation.Equals(companyId)).ToList();

            return result;
        }
    }
}
