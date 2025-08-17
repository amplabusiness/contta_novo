using Corporate.Contta.Schedule.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Domain.Entities.DashboardAgg
{
    public class Dashboard : Entity
    {
        public Dashboard()
        {
            FaturamentoMensaisEntrada = new FaturamentoMesnalEntrada();
            FaturamentoMensaisSaida = new FaturamentoMesnalSaida();
            DataEmissaoMensais = new DataEmissao();
            ValorContabil = new ValorContabil();
            SimplesNacional = new SimplesNacionalDash();          
            ListaNfeCdBarra = new List<string>();
            ListRefNfe = new List<string>();
            ListaNfe = new List<NFEEntity>();
            ListFaturamentoMensaisSaida = new List<FaturamentoMesnalSaida>();
            ListFaturamentoMensaisEntrada = new List<FaturamentoMesnalEntrada>();
            CompanyInformation = new Guid();
            TotalImpostosProd = 0.0;

        }

        public DataEmissao DataEmissaoMensais { get; set; }
        public ValorContabil ValorContabil { get; set; }
        public SimplesNacionalDash SimplesNacional { get; set; }
        public FaturamentoMesnalSaida FaturamentoMensaisSaida { get; set; }
        public FaturamentoMesnalEntrada FaturamentoMensaisEntrada { get; set; }

        public List<FaturamentoMesnalSaida> ListFaturamentoMensaisSaida { get; set; }
        public List<FaturamentoMesnalEntrada> ListFaturamentoMensaisEntrada { get; set; }

        public List<string> ListaNfeCdBarra { get; set; }       
        public List<string> ListRefNfe { get; set; }
        public List<NFEEntity> ListaNfe { get; set; }

        public Guid? CompanyInformation { get; set; }
        public double TotalImpostosProd { get; set; }
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

        public double BaseCalculo { get; set; }
        public double BaseIcms { get; set; }  
        public double BaseConfins { get; set; }

    }
    public class SimplesNacionalDash
    {
        public double FaturamentoAnual { get; set; }
        public double BaseDeCalculo { get; set; }
        public double ImpostosAPagar { get; set; }
        public DateTime? DateFounded { get; set; }
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

    public class CompanyInfo
    {
        public string Cnpj { get; set; }

        public Guid CompanyInformation { get; set; }
       

        public string TipoModeloNfe { get; set; }

        public double ValorOriginalNfe { get; set; }
    }



    public class NotaFiscaisMes
    {
        public double QuantidadeEntrada { get; set; }
        public double QuantidadeSaida { get; set; }
    }

    public class NFEEntity
    {
        public string CodBarra { get; set; }

    }

}
