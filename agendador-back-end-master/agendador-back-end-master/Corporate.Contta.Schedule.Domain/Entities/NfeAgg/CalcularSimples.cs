using Corporate.Contta.Schedule.Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Domain.Entities.NfeAgg
{
    public class CalcularSimples : Entity
    {


    }

    public class ImportosCsll
    {
        public int Faixa { get; set; }
        public double Porcentagem { get; set; }     
    }

    public class ImportosIrpj
    {

        public Guid Id { get; set; }
        public int Faixa { get; set; }
        public decimal Porcentagem { get; set; }
    }
    public class ImportosCofins
    {

        public Guid Id { get; set; }
        public int Faixa { get; set; }
        public decimal Porcentagem { get; set; }
    }
    public class ImportosPisPasep
    {

        public Guid Id { get; set; }
        public int Faixa { get; set; }
        public decimal Porcentagem { get; set; }
    }
    public class ImportosCpp
    {

        public Guid Id { get; set; }
        public int Faixa { get; set; }
        public decimal Porcentagem { get; set; }
    }
    public class ImportosIcms
    {
        public Guid Id { get; set; }
        public int Faixa { get; set; }
        public decimal Porcentagem { get; set; }
    }  
}
