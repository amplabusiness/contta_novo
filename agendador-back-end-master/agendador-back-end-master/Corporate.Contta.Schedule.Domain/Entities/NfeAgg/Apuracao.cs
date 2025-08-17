using System;
using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Domain.Entities.NfeAgg
{
    public class Apuracao
    {
        public Apuracao()
        {
            ApuracaoSaida = new List<ApuracaoSaida>();

            ApuracaoEntrada = new List<ApuracaoEntrada>();            
        }

        public List<ApuracaoSaida> ApuracaoSaida { get; set; }
        public List<ApuracaoEntrada> ApuracaoEntrada { get; set; }      
    }

    public class ApuracaoSaida
    {
        public int QtdNfe { get; set; }
        public double TotalNfe { get; set; }
        public double TotalProdutos { get; set; }
        public double TotalDesconto { get; set; }
        public double TotalSeguro { get; set; }
        public double TotalFrete { get; set; }
        public double TotalIpi { get; set; }
        public double Outros { get; set; }
        public double TotalIcmsSt { get; set; }   
        public double TotalIcmsDesc { get; set; }
        public double Cfop { get; set; }
        public string DescricaoCfop { get; set; }
        public List<Guid?> LintNfeId { get; set; }
        public bool CalculoSimples { get; set; }

    }

    public class ApuracaoEntrada
    {
        public int QtdNfe { get; set; }
        public double TotalNfe { get; set; }
        public double TotalProdutos { get; set; }
        public double TotalDesconto { get; set; }
        public double TotalSeguro { get; set; }
        public double TotalFrete { get; set; }
        public double TotalIpi { get; set; }
        public double Outros { get; set; }
        public double TotalIcmsSt { get; set; }
        public double TotalIcmsDesc { get; set; }
        public double Cfop { get; set; }
        public string DescricaoCfop { get; set; }
        public List<Guid?> LintNfeId { get; set; }
    } 

}

