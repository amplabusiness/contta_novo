using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Dto.Dashboard
{
    public class DetalhamentoImpostoDto
    {
        public DetalhamentoIcmsStDto DetalhamentoIcmsSt { get; set; }
        public DetalhamentoPisConfinsDto DetalhamentoPisConfins { get; set; }
    }
    public class DetalhamentoApuracaoDto
    {
        public DetalhamentoIcmsStDto DetalhamentoIcmsSt { get; set; }
        public DetalhamentoPisConfinsDto DetalhamentoPisConfins { get; set; }
        public DetalhamentoNfeGeralDto DetalhamentoNfeGeral { get; set; }
    }
    public class DetalhamentoIcmsStDto
    {
        public IcmsStDto IcmsSt { get; set; }
        public BeneficiosDto Beneficios { get; set; }
        public IsentoDto Isento { get; set; }
        public ImuneDto Imune { get; set; }
        public ExigSuspensaDto ExigSuspensa { get; set; }
        public LancamentoOficioDto LancamentoOficio { get; set; }
        public IsencaoReducaoDto IsencaoReducao { get; set; }
        public IsencaoReducaoCestaBasicaDto IsencaoReducaoCestaBasica { get; set; }
        public AntEncTributacaoDto AntEncTributacao { get; set; }
    }
    public class DetalhamentoPisConfinsDto
    {
        public NcmMonoDto NcmMono { get; set; }
        public ExigSuspensaMonoDto ExigSuspensaMono { get; set; }
        public SubTributariaMonoDto SubTributariaMono { get; set; }
        public LancamentoOficioMonoDto LancamentoOficioMono { get; set; }
    }
    public class DetalhamentoNfeGeralDto
    {
        public CanceladasDto Canceladas { get; set; }
        public DevolucaoDto Devolucao { get; set; }
        public TransferenciaMercadoriaDto TransferenciaMercadoria { get; set; }
    }
    #region DetalhamentoImcsSt
    public class IcmsStDto
    {
        public double Total { get; set; }
        public List<Guid> ListIdNfe { get; set; }
        public List<Guid> ListIdProduto { get; set; }
    }
    public class BeneficiosDto
    {
        public double Total { get; set; }
        public List<Guid> ListIdNfe { get; set; }
        public List<Guid> ListIdProduto { get; set; }
    }
    public class IsentoDto
    {
        public double Total { get; set; }
        public List<Guid> ListIdNfe { get; set; }
        public List<Guid> ListIdProduto { get; set; }
    }
    public class ImuneDto
    {
        public double Total { get; set; }
        public List<Guid> ListIdNfe { get; set; }
        public List<Guid> ListIdProduto { get; set; }
    }
    public class ExigSuspensaDto
    {
        public double Total { get; set; }
        public List<Guid> ListIdNfe { get; set; }
        public List<Guid> ListIdProduto { get; set; }
    }
    public class LancamentoOficioDto
    {
        public double Total { get; set; }
        public List<Guid> ListIdNfe { get; set; }
        public List<Guid> ListIdProduto { get; set; }
    }
    public class IsencaoReducaoDto
    {
        public double Total { get; set; }
        public List<Guid> ListIdNfe { get; set; }
        public List<Guid> ListIdProduto { get; set; }
    }
    public class IsencaoReducaoCestaBasicaDto
    {
        public double Total { get; set; }
        public List<Guid> ListIdNfe { get; set; }
        public List<Guid> ListIdProduto { get; set; }
    }
    public class AntEncTributacaoDto
    {
        public double Total { get; set; }
        public List<Guid> ListIdNfe { get; set; }
        public List<Guid> ListIdProduto { get; set; }
    }
    #endregion

    #region DetalhamentoPisConfins
    public class NcmMonoDto
    {
        public double Total { get; set; }
        public List<Guid> ListIdNfe { get; set; }
        public List<Guid> ListIdProduto { get; set; }
    }
    public class ExigSuspensaMonoDto
    {
        public double Total { get; set; }
        public List<Guid> ListIdNfe { get; set; }
        public List<Guid> ListIdProduto { get; set; }
    }
    public class SubTributariaMonoDto
    {
        public double Total { get; set; }
        public List<Guid> ListIdNfe { get; set; }
        public List<Guid> ListIdProduto { get; set; }
    }
    public class LancamentoOficioMonoDto
    {
        public double Total { get; set; }
        public List<Guid> ListIdNfe { get; set; }
        public List<Guid> ListIdProduto { get; set; }
    }
    #endregion
    #region DetalhamentoNfeGeral
    public class CanceladasDto
    {
        public double Total { get; set; }
        public List<Guid> ListIdNfe { get; set; }
        public List<Guid> ListIdProduto { get; set; }
    }
    public class DevolucaoDto
    {
        public double Total { get; set; }
        public List<Guid> ListIdNfe { get; set; }
        public List<Guid> ListIdProduto { get; set; }
    }
    public class TransferenciaMercadoriaDto
    {
        public double Total { get; set; }
        public List<Guid> ListIdNfe { get; set; }
        public List<Guid> ListIdProduto { get; set; }
    }
    #endregion
}
