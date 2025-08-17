using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Domain.Entities.DashboardAgg.Apuracoes
{
    public class DetalhamentoImposto
    {
        public DetalhamentoIcmsSt DetalhamentoIcmsSt { get; set; }
        public DetalhamentoPisConfins DetalhamentoPisConfins { get; set; }
    }
    public class DetalhamentoApuracao
    {
        public DetalhamentoIcmsSt DetalhamentoIcmsSt { get; set; }
        public DetalhamentoPisConfins DetalhamentoPisConfins { get; set; }
        public DetalhamentoNfeGeral DetalhamentoNfeGeral { get; set; }
    }
    public class DetalhamentoIcmsSt
    {
        public DetalhamentoIcmsSt()
        {
            this.IcmsSt = new IcmsSt();
            this.Beneficios = new Beneficios();
            this.Isento = new Isento();
            this.Imune = new Imune();
            this.ExigSuspensa = new ExigSuspensa();
            this.AntEncTributacao = new AntEncTributacao();
            this.LancamentoOficio = new LancamentoOficio();
            this.IsencaoReducao = new IsencaoReducao();
            this.IsencaoReducaoCestaBasica = new IsencaoReducaoCestaBasica();

        }
        public IcmsSt IcmsSt { get; set; }
        public Beneficios Beneficios { get; set; }
        public Isento Isento { get; set; }
        public Imune Imune { get; set; }
        public ExigSuspensa ExigSuspensa { get; set; }
        public LancamentoOficio LancamentoOficio { get; set; }
        public IsencaoReducao IsencaoReducao { get; set; }
        public IsencaoReducaoCestaBasica IsencaoReducaoCestaBasica { get; set; }
        public AntEncTributacao AntEncTributacao { get; set; }
    }
    public class DetalhamentoPisConfins
    {
        public DetalhamentoPisConfins()
        {
            this.NcmMono = new NcmMono();
            this.ExigSuspensaMono = new ExigSuspensaMono();
            this.LancamentoOficioMono = new LancamentoOficioMono();
            this.SubTributariaMono = new SubTributariaMono();
        }
        public NcmMono NcmMono { get; set; }
        public ExigSuspensaMono ExigSuspensaMono { get; set; }
        public SubTributariaMono SubTributariaMono { get; set; }
        public LancamentoOficioMono LancamentoOficioMono { get; set; }
    }
    public class DetalhamentoNfeGeral
    {
        public DetalhamentoNfeGeral()
        {
            this.Canceladas = new Canceladas();
            this.Devolucao = new Devolucao();
            this.TransferenciaMercadoria = new TransferenciaMercadoria();
        }
        public Canceladas Canceladas { get; set; }
        public Devolucao Devolucao { get; set; }
        public TransferenciaMercadoria TransferenciaMercadoria { get; set; }
    }
    #region DetalhamentoImcsSt
    public class IcmsSt
    {
        public double Total { get; set; }
        public List<Guid?> ListIdNfe { get; set; }
        public List<Guid?> ListIdProduto { get; set; }
    }
    public class Beneficios
    {
        public double Total { get; set; }
        public List<Guid?> ListIdNfe { get; set; }
        public List<Guid?> ListIdProduto { get; set; }
    }
    public class Isento
    {
        public double Total { get; set; }
        public List<Guid?> ListIdNfe { get; set; }
        public List<Guid?> ListIdProduto { get; set; }
    }
    public class Imune
    {
        public double Total { get; set; }
        public List<Guid?> ListIdNfe { get; set; }
        public List<Guid?> ListIdProduto { get; set; }
    }
    public class ExigSuspensa
    {
        public double Total { get; set; }
        public List<Guid?> ListIdNfe { get; set; }
        public List<Guid?> ListIdProduto { get; set; }
    }
    public class LancamentoOficio
    {
        public double Total { get; set; }
        public List<Guid?> ListIdNfe { get; set; }
        public List<Guid?> ListIdProduto { get; set; }
    }
    public class IsencaoReducao
    {
        public double Total { get; set; }
        public List<Guid?> ListIdNfe { get; set; }
        public List<Guid?> ListIdProduto { get; set; }
    }
    public class IsencaoReducaoCestaBasica
    {
        public double Total { get; set; }
        public List<Guid?> ListIdNfe { get; set; }
        public List<Guid?> ListIdProduto { get; set; }
    }
    public class AntEncTributacao
    {
        public double Total { get; set; }
        public List<Guid?> ListIdNfe { get; set; }
        public List<Guid?> ListIdProduto { get; set; }
    }
    #endregion

    #region DetalhamentoPisConfins
    public class NcmMono
    {
        public double Total { get; set; }
        public List<Guid?> ListIdNfe { get; set; }
        public List<Guid?> ListIdProduto { get; set; }
    }
    public class ExigSuspensaMono
    {
        public double Total { get; set; }
        public List<Guid?> ListIdNfe { get; set; }
        public List<Guid?> ListIdProduto { get; set; }
    }
    public class SubTributariaMono
    {
        public double Total { get; set; }
        public List<Guid?> ListIdNfe { get; set; }
        public List<Guid?> ListIdProduto { get; set; }
    }
    public class LancamentoOficioMono
    {
        public double Total { get; set; }
        public List<Guid?> ListIdNfe { get; set; }
        public List<Guid?> ListIdProduto { get; set; }
    }
    #endregion
    #region DetalhamentoNfeGeral
    public class Canceladas
    {
        public Canceladas()
        {
            this.ListIdNfe = new List<Guid?>();
            this.ListIdProduto = new List<Guid?>();
        }
        public double Total { get; set; }
        public List<Guid?> ListIdNfe { get; set; }
        public List<Guid?> ListIdProduto { get; set; }
    }
    public class Devolucao
    {
        public Devolucao()
        {
            this.ListIdNfe = new List<Guid?>();
        }
        public double Total { get; set; }
        public List<Guid?> ListIdNfe { get; set; }
        public List<Guid?> ListIdProduto { get; set; }
    }
    public class TransferenciaMercadoria
    {
        public TransferenciaMercadoria()
        {
            this.ListIdNfe = new List<Guid?>();
        }
        public double Total { get; set; }
        public List<Guid?> ListIdNfe { get; set; }
        public List<Guid?> ListIdProduto { get; set; }
    }
    #endregion
}
