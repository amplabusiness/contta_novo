using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using Corporate.Contta.Schedule.Infra.Repositories.Base;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Infra.Repositories
{
    public class FaturamentoEmpresaRepository : BaseRepository<FaturamentoEmpresa>, IFaturamentoEmpresaRepository
    {
        private static MongoDBContext<FaturamentoEmpresa> _dbContext = new MongoDBContext<FaturamentoEmpresa>();

        public FaturamentoEmpresaRepository() : base(_dbContext) { }

        #region

        public async Task Insert(FaturamentoEmpresa faturamentoEmpresa)
        {
            var listFaturamento = new List<Faturamento>();
            var listFaturamentoExistDto = new List<Faturamento>();
            var listFaturamentoExist = new List<Faturamento>();
            var faturamentoEmpresaDto = new FaturamentoEmpresa();
            var valorTotal = 0.0;

            var faturamentoAtual = _dbContext.GetColection.Find(c => c.CompanyInformation == faturamentoEmpresa.CompanyInformation && c.FaturamentoFechado == false).FirstOrDefault();

            if (faturamentoAtual == null)
            {
                foreach (var item in faturamentoEmpresa.Faturamentos)
                {
                    item.Id = Guid.NewGuid();
                    listFaturamento.Add(item);
                    valorTotal = item.ValorFaturamento + valorTotal;
                }
                faturamentoEmpresaDto = new FaturamentoEmpresa
                {
                    CompanyInformation = faturamentoEmpresa.CompanyInformation,
                    FaturamentoFechado = faturamentoEmpresa.FaturamentoFechado,
                    TotalAnual = valorTotal,
                    Faturamentos = listFaturamento,
                    Id = Guid.NewGuid()
                };

                await _dbContext.GetColection.InsertOneAsync(faturamentoEmpresaDto).ConfigureAwait(false);
            }
            else
            {
                foreach (var faturamento in faturamentoEmpresa.Faturamentos)
                {
                    var faturamentoDto = faturamentoAtual.Faturamentos.Where(c => c.DataFaturamento.Year == faturamento.DataFaturamento.Year && c.DataFaturamento.Month == faturamento.DataFaturamento.Month).FirstOrDefault();

                    if (faturamentoDto == null)
                    {
                        faturamento.Id = Guid.NewGuid();
                        listFaturamento.Add(faturamento);
                    }
                    else
                    {
                        faturamentoDto.Id = Guid.NewGuid();
                        faturamentoDto.ValorFaturamento = faturamento.ValorFaturamento;
                        listFaturamentoExistDto.Add(faturamentoDto);
                    }
                }

                foreach (var item in faturamentoAtual.Faturamentos)
                {
                    var faturamentoExi = listFaturamento.FirstOrDefault(c => c.DataFaturamento == item.DataFaturamento && c.ValorFaturamento == item.ValorFaturamento);

                    if (faturamentoExi == null)
                        listFaturamentoExist.Add(item);
                }

                listFaturamento.AddRange(listFaturamentoExist);

                faturamentoEmpresaDto = new FaturamentoEmpresa
                {
                    CompanyInformation = faturamentoAtual.CompanyInformation,
                    FaturamentoFechado = faturamentoAtual.FaturamentoFechado,
                    TotalAnual = faturamentoAtual.TotalAnual,
                    Faturamentos = listFaturamento,
                    Id = faturamentoAtual.Id

                };

                Update(faturamentoEmpresaDto, faturamentoEmpresa.FaturamentoFechado);
            }
        }

        public bool ExistsFaturamentoToCompany(Guid companyInformation)
        {
            var result = _dbContext.GetColection.Find(c => c.CompanyInformation.Equals(companyInformation));
            return result.Any();
        }

        public async Task<bool> Delete(Guid idFaturamentoEmpresa)
        {
            var updateResult = await _dbContext.GetColection.DeleteOneAsync(c => c.Id == idFaturamentoEmpresa);
            return updateResult.DeletedCount > 0;
        }

        public async Task<FaturamentoEmpresa> GetByIdEmpresa(Guid idEmpresa)
        {
            var result = await _dbContext.GetColection.FindAsync(c => c.CompanyInformation == idEmpresa).ConfigureAwait(false);
            return result.FirstOrDefault();
        }

        public async Task<bool> Update(FaturamentoEmpresa faturamentoEmpresa, bool fecharFaturamento)
        {
            if (faturamentoEmpresa != null)
            {

                var result = _dbContext.GetColection.DeleteOne(c => c.Id.Equals(faturamentoEmpresa.Id));

                if (result.DeletedCount > 0)
                {
                    if (fecharFaturamento)
                        faturamentoEmpresa.FaturamentoFechado = true;
                    await _dbContext.GetColection.InsertOneAsync(faturamentoEmpresa).ConfigureAwait(false);

                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;

        }

        public async Task<bool> UpdateFaturas(FaturamentoEmpresa faturamentoEmpresa)
        {
            var FaturamentoEmpresa = new FaturamentoEmpresa();

            var faturamento = _dbContext.GetColection.Find(c => c.CompanyInformation == faturamentoEmpresa.CompanyInformation).FirstOrDefault();

            foreach (var item in faturamento.Faturamentos)
            {
                foreach (var fat in faturamentoEmpresa.Faturamentos)
                {
                    if (item.Id != fat.Id)
                    {
                        FaturamentoEmpresa.Faturamentos.Add(item);
                    }
                }
            }


            var update = Builders<FaturamentoEmpresa>.Update.Set(c => c.CompanyInformation, faturamentoEmpresa.CompanyInformation)
                                                            .Set(c => c.FaturamentoFechado, faturamentoEmpresa.FaturamentoFechado);

            var updateResult = await _dbContext.GetColection.UpdateOneAsync(c => c.Id == faturamento.Id, update);

            if (updateResult.ModifiedCount > 0)
            {
                return true;
            }

            return false;
        }

        public double InsertCurt(FaturamentoEmpresa faturamentoEmpresa)
        {
            try
            {
                var faturamentoAtual = _dbContext.GetColection.Find(c => c.CompanyInformation == faturamentoEmpresa.CompanyInformation).FirstOrDefault();

                if (faturamentoAtual == null)
                {
                    var valorFechamento = faturamentoEmpresa.Faturamentos.Select(c => c.ValorFaturamento).Sum();
                    var dataFechamento = faturamentoEmpresa.Faturamentos.Select(c => c.DataFaturamento).FirstOrDefault();
                    var fechamentoSimples = valorFechamento * 12;
                    faturamentoEmpresa.Faturamentos.Clear();

                    var fechamento = new Faturamento
                    {
                        Ano = dataFechamento.Year,
                        DataFaturamento = dataFechamento,
                        Id = Guid.NewGuid(),
                        ValorFaturamento = fechamentoSimples

                    };

                    faturamentoEmpresa.Faturamentos.Add(fechamento);
                    faturamentoEmpresa.FaturamentoFechado = true;
                    faturamentoEmpresa.Id = Guid.NewGuid();
                    faturamentoEmpresa.TotalAnual = fechamentoSimples;

                    _dbContext.GetColection.InsertOne(faturamentoEmpresa);

                    return fechamentoSimples;
                }

                return faturamentoAtual.TotalAnual;

            }
            catch (Exception)
            {

                throw;
            }

        }

        #endregion
              


    }
}
