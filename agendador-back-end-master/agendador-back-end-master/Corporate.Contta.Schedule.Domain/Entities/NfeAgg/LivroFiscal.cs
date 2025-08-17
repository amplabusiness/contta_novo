using Corporate.Contta.Schedule.Domain.Entities.Base;
using Corporate.Contta.Schedule.Domain.Entities.NfeTAgg;
using System;
using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Domain.Entities.NfeAgg
{
    public class LivroFiscal : Entity
    {
        public int  Dia { get; set; }
        public string RazaoSocial { get; set; }
        public string Especie { get; set; }     
        public string Cnpj   { get; set; }
        public string Inscricao   { get; set; }
        public string EstadoEmissao { get; set; }
        public DateTime DataInicial   { get; set; }
        public DateTime? DataEmissao   { get; set; }
        public DateTime DataFinal   { get; set; }
        public string Operacao { get; set; }
        public int N_NotaFiscal { get; set; }
        public string Serie { get; set; }
        public double TotalNfeTributado { get; set; }     
        public double TotalNfeNaoTributado { get; set; }
        public double CFOP { get; set; }
        public double BASECALCULO { get; set; }
        public double ALIQ { get; set; }
        public double IMPDEBITADO { get; set; }
        public double OUTRAS { get; set; }
        public List<Items> ListProdutos { get; set; }
    }
}
