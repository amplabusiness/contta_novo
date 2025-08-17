using Corporate.Contta.Schedule.Domain.Entities.EstoqueAgg;
using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using Corporate.Contta.Schedule.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Domain.Contracts.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Produtos>> GetAll(Guid companyId);
        Task<bool> Update(List<Produtos> listProdutos, Guid empresaId);
        Task<bool> UpdateProd(List<ImpostosProd> listProdutos, Guid empresaId, DateTime dhEm);
        Task<bool> UpdateFornec(List<ProdutosFornecedor> listProdutos, Guid empresaId);
        Task<bool> UpdateImpostos(List<ProdutosImpsotos> listProdutos);
        Task<bool> UpdateDepara(string codProFornecedor, string codProCliente, Guid empresaId, string marca);
        Task<IEnumerable<Produtos>> GetAllIcmsSt(Guid companyId, DateTime dhEmiss);
        Task<IEnumerable<Produtos>> GetAllIcmsStAlterado(Guid companyId, DateTime dhEmiss);

        Task<List<Estoque>> GetProd(Guid companyId, string codPrd, string desCod);

        Task<IEnumerable<Produtos>> GetAllNcmMono(Guid companyId, DateTime dhEmiss);
        Task<IEnumerable<Produtos>> GetAllNcmMonoAlterado(Guid companyId, DateTime dhEmiss);
        Task<IEnumerable<Produtos>> GetAllBeneficio(Guid companyId);
        Task<IEnumerable<Produtos>> GetAllImune(Guid companyId);
        Task<IEnumerable<Produtos>> GetAllInsento(Guid companyId);
        Task<IEnumerable<Produtos>> GetAllInsencaoCesta(Guid companyId);
        Task<IEnumerable<Produtos>> GetAllInsencao(Guid companyId);
        Task<IEnumerable<ProdutosFornecedor>> GetAllProdForn(Guid companyId);
        Task<List<Produtos>> GetAllByNfe(Guid nfeId);
        Task<Reclassificacao> GetById(Guid empresaId, Guid nfeId);
        Task<Reclassificacaofornec> GetByIdFornec(Guid empresaId, Guid nfeId);
        Task<List<FileProduct>> GetFileProduct(Guid companyInformation);

        Task<bool> UpdateProdutoFornect(List<ProdutosFornecedor> listaProd);
    }
}
