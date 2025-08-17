using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities.Configuration;
using Corporate.Contta.Schedule.Domain.Entities.UserAgg;
using Corporate.Contta.Schedule.Infra.Repositories.Base;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Infra.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private static MongoDBContext<User> _dbContext = new MongoDBContext<User>();
        private static MongoDBContext<TokenAcesso> _dbContextTokenAcesso = new MongoDBContext<TokenAcesso>();

        private IConfigurationUserRepository _configurationUserRepository;

        public UserRepository(IConfigurationUserRepository configuration) : base(_dbContext) { _configurationUserRepository = configuration; }

        public override void Add(User entity)
        {
            try
            {
                var user = _dbContext.GetColection.Find(c => c.Email == entity.Email).FirstOrDefault();

                if (user != null)
                    throw new Exception("Usuario já cadastrado em nossa base!");
                if (!entity.Id.HasValue)
                    entity.Id = Guid.NewGuid();
                entity.SetPasswordHash();
                base.Add(entity);

                //Método criado para gerar configuration padrão do usúario cadastrado.
                var configurationDto = new ConfigurationUser();
                configurationDto.UserId = entity.Id;
                _configurationUserRepository.Insert(configurationDto);
            }
            catch (Exception)
            {

                throw;
            }


        }

        public async Task<bool> ChangeLinkWithTheCompany(Guid id, List<UserCompany> userCompanies)
        {
            var user = await GetUserById(id);
            if (user == null)
                return false;
            var update = Builders<User>.Update.Set(c => c.Companies, userCompanies);
            var resultUpdate = await _dbContext.GetColection.UpdateOneAsync(c => c.Id == user.Id, update);
            return resultUpdate.ModifiedCount > 0;
        }

        public async Task DeleteUserById(Guid userId)
        {
            await _dbContext.GetColection.DeleteOneAsync(c => c.Id == userId);
        }

        public async Task<bool> ExistsUser(string userName)
        {
            var builder = Builders<User>.Filter;
            var filter = builder.Eq(user => user.Name, userName);

            var result = await _dbContext.GetColection.FindAsync(filter);

            return result.Any();
        }

        public async Task<List<UserCompany>> GetAllUserCompany(Guid id)
        {
            var companies = new List<UserCompany>();
            var result = await _dbContext.GetColection.FindAsync(c => c.Id == id).ConfigureAwait(false);

            companies = result?.FirstOrDefault().Companies;
            return companies;
        }

        public async Task<TokenAcesso> GetToken(TokenAcesso tokenAcesso)
        {
            try
            {
                var result = _dbContextTokenAcesso.GetColection.Find(c => c.TokenAcess.Equals(tokenAcesso.TokenAcess) && c.UserId == null).FirstOrDefault();

                if (result != null)
                    return result;
                else
                {
                    throw new Exception("Token De Acesso INvalido!");
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Token De Acesso INvalido!");
            }
        }

        public async Task<bool> UpdateTokenAcess(User user)
        {
            try
            {
                var userDto = _dbContext.GetColection.Find(c => c.TokenAcesso == user.TokenAcesso).FirstOrDefault();
                var userMaster = _dbContext.GetColection.Find(c => c.Id == user.Id).FirstOrDefault();
                var token = _dbContextTokenAcesso.GetColection.Find(c => c.TokenAcess == user.TokenAcesso || c.TokenAcess == user.TokenAcesso).FirstOrDefault();

                var update = Builders<TokenAcesso>.Update.Set(c => c.TokenAcess, token.TokenAcess)
                                                   .Set(c => c.UserId, userDto.Id);

                var updateResult = await _dbContextTokenAcesso.GetColection.UpdateOneAsync(c => c._id == token._id, update);

                return updateResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<User> GetUserById(Guid Id)
        {
            var result = await _dbContext.GetColection.FindAsync(c => c.Id == Id).ConfigureAwait(false);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetUsersByMasterId(Guid userMasterId)
        {
            var users = await _dbContext.GetColection.FindAsync(c => c.UserMasterId == userMasterId);

            return users.ToList();
        }

        public async Task<User> Login(string email)
        {
            var builder = Builders<User>.Filter;
            var filter = builder.Eq(user => user.Email, email);

            var result = await _dbContext.GetColection.FindAsync(filter).ConfigureAwait(false);

            return await result.FirstOrDefaultAsync();
        }

        public async Task<bool> RedefinePassword(Guid id, string passwordNew)
        {
            var update = Builders<User>.Update.Set(c => c.Password, passwordNew);
            var resultUpdate = await _dbContext.GetColection.UpdateOneAsync(c => c.Id == id, update);
            return resultUpdate.ModifiedCount > 0;
        }

        public async Task<bool> UpdateUser(User user)
        {
           
            var userDto = _dbContext.GetColection.Find(c => c.Id == user.Id).FirstOrDefault();
            string passoword = "";
            if (user.Password != "")
            {
                user.SetPasswordHash();
            }
            else
            {
                passoword = userDto.Password;
            }
            if (userDto != null)
            {
                if (user.Password != "")
                    user.SetPasswordHash();
                var update = Builders<User>.Update.Set(c => c.Group, user.Group)
                                     .Set(c => c.Role, user.Role)
                                     .Set(c => c.UserMasterId, user.UserMasterId)
                                     .Set(c => c.Name, user.Name)
                                     .Set(c => c.Document, user.Document)
                                     .Set(c => c.Email, user.Email)
                                     .Set(c => c.Password, passoword)
                                     .Set(c => c.Picture, user.Picture)
                                     .Set(c => c.TokenAcesso, user.TokenAcesso)
                                     .Set(c => c.IsActive, user.IsActive)
                                     .Set(c => c.CompanyId, user.CompanyId)
                                     .Set(c => c.Companies, user.Companies);

                var updateResult = await _dbContext.GetColection.UpdateOneAsync(c => c.Id == user.Id, update);

                return updateResult.ModifiedCount > 0;
            }

            return false;
        }

        public async Task<bool> UpdateUser(User user, Guid? id)
        {
            var userDto = _dbContext.GetColection.Find(c => c.Id == user.Id).FirstOrDefault();
            if (userDto != null)
            {               
                var update = Builders<User>.Update.Set(c => c.Group, user.Group)
                                     .Set(c => c.Role, user.Role)
                                     .Set(c => c.UserMasterId, user.UserMasterId)
                                     .Set(c => c.Name, user.Name)
                                     .Set(c => c.Document, user.Document)
                                     .Set(c => c.Email, user.Email)
                                     .Set(c => c.Password, user.Password)
                                     .Set(c => c.Picture, user.Picture)
                                     .Set(c => c.TokenAcesso, user.TokenAcesso)
                                     .Set(c => c.IsActive, user.IsActive)
                                     .Set(c => c.CompanyId, user.CompanyId)
                                     .Set(c => c.Companies, user.Companies);

                var updateResult = await _dbContext.GetColection.UpdateOneAsync(c => c.Id == user.Id, update);
                return updateResult.ModifiedCount > 0;
            }

            return false;
        }

        public async Task<User> GetUser(Guid Id)
        {
            var result = await _dbContext.GetColection.FindAsync(c => c.Id == Id).ConfigureAwait(false);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<User> GetUsersByMaster(Guid userMasterId)
        {
            var users =  _dbContext.GetColection.Find(c => c.Id == userMasterId).FirstOrDefault();

            return users;
        }
    }
}
