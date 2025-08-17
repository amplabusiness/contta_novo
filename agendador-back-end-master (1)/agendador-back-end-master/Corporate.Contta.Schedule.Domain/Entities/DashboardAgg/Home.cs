using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Domain.Entities.DashboardAgg
{
    public class Home
    {
        public Guid? Id { get; set; }
        public Guid? CompanyId { get; set; }
        public string RazaoSocial { get; set; }
        public bool Status { get; set; }
        public bool Difal { get; set; }
        public bool Declaracao { get; set; }
        public bool Das { get; set; }
        public bool Extrato { get; set; }
        public double Faturamento { get; set; }
        public double Aliquota { get; set; }
        public DateTime DataFechamento { get; set; }
        public string ValidadeCertificado { get; set; }

    }
}
