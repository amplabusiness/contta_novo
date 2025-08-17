using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmaloteContta.Models.Respositories
{
    public interface IProdutosRepositorio : IRepositorio<Speed.Produtos>
    {
        Task<List<Speed.Produtos>> ObterTodosOsProdutoDeUmaNota(Guid nfeId);
        Task<List<Speed.Produtos>> ObterTodosOsProdutoDeEmpresa(Guid empresaId);
        Task<Speed.Produtos> ObterTodosOsProdutoDeUmaNotaPeloCodigo(Guid nfeId, string codProduto);
    }
}
