using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmaloteContta.Infra.Base;
using EmaloteContta.Models.Respositories;
using EmaloteContta.Models.Speed;
using MongoDB.Driver;

namespace EmaloteContta.Infra.Repositorios
{
    public class ProdutosRepositorio : BaseRepository<Produtos>, IProdutosRepositorio
    {
        private static MongoDBContext<Produtos> _dbContext = new MongoDBContext<Produtos>();

        public ProdutosRepositorio() : base(_dbContext) { }

        public override Task<Produtos> Add(Produtos entity)
        {
            if (!entity.Id.HasValue)
                entity.Id = Guid.NewGuid();
            return base.Add(entity);
        }

        public async Task<List<Produtos>> ObterTodosOsProdutoDeEmpresa(Guid empresaId)
        {
            var result = await _dbContext.GetColection.FindAsync(c => c.EmpresaEmitId == empresaId);
            return result.ToList();
        }

        public async Task<List<Produtos>> ObterTodosOsProdutoDeUmaNota(Guid nfeId)
        {
            var result = await _dbContext.GetColection.FindAsync(c => c.NfeId == nfeId);
            return result.ToList();
        }

        public async Task<Produtos> ObterTodosOsProdutoDeUmaNotaPeloCodigo(Guid nfeId, string codProduto)
        {
            var result = await _dbContext.GetColection.FindAsync(c => c.CodProduto == codProduto && c.NfeId == nfeId);
            return result.FirstOrDefault();
        }
    }
}
