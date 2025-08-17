using Corporate.Contta.Schedule.Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Domain.Entities.NfeAgg
{
    public class TbDashboardClientes : Entity
    {
        public TbDashboardClientes()
        {
            FaturamentoMensaisEntrada = new FaturamentoMesnalEntrada();
            FaturamentoMensaisSaida = new FaturamentoMesnalSaida();
            DataEmissaoMensais = new DataEmissao();
            ValorContabil = new ValorContabil();
            SimplesNacional = new SimplesNacionalDash();
            ListaNfe = new List<NFEEntity>();
            CompanyInformation = new Guid();
        }

        public DataEmissao DataEmissaoMensais { get; set; }
        public ValorContabil ValorContabil { get; set; }
        public SimplesNacionalDash SimplesNacional { get; set; }
        public FaturamentoMesnalSaida FaturamentoMensaisSaida { get; set; }
        public FaturamentoMesnalEntrada FaturamentoMensaisEntrada { get; set; }
        public List<NFEEntity> ListaNfe { get; set; }
        public Guid? CompanyInformation { get; set; }

    }

    public class DataEmissao
    {
        public int Ano { get; set; }
        public int Mes { get; set; }
        public DateTime DhEmi { get; set; }
    }

    public class ValorContabil
    {
        public double ValorEntradaMercadoria { get; set; }
        public double ValorSaidaMercadoria { get; set; }

        public double NotaDevolucaoEntrada { get; set; }
        public double NotaDevolucaoSaida { get; set; }

        public double NotaServicoPrestador { get; set; }
        public double NotaServicoTomador { get; set; }
        public double NotaDevolucaoPrestacao { get; set; }

        public double ValorFreteIntramunicipal { get; set; }
        public double ValorFreteIntermunicipal { get; set; }
        public double ValorFreteInterestadual { get; set; }
        public decimal BaseCalculo { get; set; }

    }
    public class SimplesNacionalDash
    {
        public double FaturamentoAnual { get; set; }
        public double BaseDeCalculo { get; set; }
        public double ImpostosAPagar { get; set; }
        public DateTime DateFounded { get; set; }
    }

    public class FaturamentoMesnalSaida
    {
        public int Ano { get; set; }
        public int Mes { get; set; }
        public double Valor { get; set; }
    }

    public class FaturamentoMesnalEntrada
    {
        public int Ano { get; set; }
        public int Mes { get; set; }
        public double Valor { get; set; }
    }

    public class NFEEntity
    {
        public string CodBarra { get; set; }

    }
}
