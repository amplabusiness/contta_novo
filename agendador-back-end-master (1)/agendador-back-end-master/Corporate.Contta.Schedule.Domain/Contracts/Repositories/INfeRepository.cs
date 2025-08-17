using Corporate.Contta.Schedule.Domain.Entities.BlocoE;
using Corporate.Contta.Schedule.Domain.Entities.FullNfeAgg;
using Corporate.Contta.Schedule.Domain.Entities.ModeloXml.NotaFiscalEletronicaMod55;
using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using Corporate.Contta.Schedule.Domain.Entities.NfeModAgg;
using Corporate.Contta.Schedule.Domain.Entities.NfeTAgg;
using Corporate.Contta.Schedule.Domain.Entities.Product;
using Corporate.Contta.Schedule.Domain.Entities.RegistroBlEAgg;
using Corporate.Contta.Schedule.Domain.Entities.TbServico;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Domain.Contracts.Repositories
{
    public interface INfeRepository
    {
        Task<NFE> GetProductInformationBy(string barCode);
        Task<List<NFE>> GetProductInformationByMasterId(Guid masterId);
        Task<NFE> Insert(NFE Nfe);
        Task Insert(NfVendaManual nfVenda);
        Task Insert(NfServicoManual nfServico, bool trasportadora);
        Task<bool> Update(List<Produtos> produtos);
        Task<bool> UpdateAluste(Guid idAjuste, double totalNfe, double aliquota, double totalCalculo);
        Task<bool> Delete(NFE product);
        Task<NFE> GetById(Guid id);
        Task<List<NFE>> GetAll(Guid companyId, int operation);
        Task<List<TbCodServico>> GetAllCodServico();
        Task<List<FullNFE>> GetAllFull(Guid companyId, string operation, DateTime data);
        Task<RetornoNfeT> GetAllNfeT(Guid companyId, string operation, DateTime data, int pagina, int qtdPorPagina, List<Guid> listNfe, bool apuracao);
        Task<RegistroBlE> GetRegistrosBlE(Guid companyId, DateTime data);
        Task<List<NFE>> GetByFilter(Guid companyId, string tipoNfe, string descProduto, string cnpj, DateTime? dhEmiss, string uf, string nomeCli);
        Task<List<RetornoNfeCancelada>> GetByFilterCanceladas(Guid companyId, string tipoNfe, string cnpj, DateTime? dhEmiss);
        Task<bool> DesativarNota(Guid id);
        Task<TotalNfe> GetBaseCalculo(Guid id, DateTime dateClose);
        Task<List<NFE>> GetAllDisabled();
        Task<List<RetornoNfeMod57>> GetAllNfeMod57(Guid companyId, string operation, DateTime data);
        Task<List<RetornoNfeServico>> GetAllNfeServico(Guid companyId, string operation, DateTime data, int pagina, int qtdPorPagina);

        Task<AjusteNfe> InsertAjusteNfe(AjusteNfe ajusteNfe);

        Task<bool> NotaJaFoiGravada(string chave);
        Task InsertProdutos(Produtos produtos);

        Task InsertFornec(ProdutosFornecedor produtos);

        Task<bool> CreateXmlNfe(NfeProc nota);

        (List<LivroFiscal>, List<LigroFiscalRodape>) GetAllNfe(Guid empresaId, DateTime dataOperacao, string operacao);

        Task<List<AjusteNfe>> GetAllBlocoE(Guid empresaId, string operacao);

        Task<bool> DeleteNfeManualVenda(Guid idNfe);
        Task<List<NfVendaManual>> GetAllNfeManualVenda(Guid empresaId);
        Task<bool> DeleteNfeManualServi(Guid idNfe);
        Task<List<NfServicoManual>> GetAllNfeManualServi(Guid empresaId);
        Task<TotalizadorNfeSaida> GetTotalizadorNfeSaida(Guid empresaId, DateTime data);

        Task<RegistroE110> InsertTbAjuste(List<TbDeducao> tbDeducao, double valorSaldoDevedor); 
    }
}
