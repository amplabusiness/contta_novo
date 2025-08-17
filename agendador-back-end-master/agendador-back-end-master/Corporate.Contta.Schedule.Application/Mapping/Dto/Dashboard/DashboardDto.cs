using AngleSharp.Dom;
using Corporate.Contta.Schedule.Domain.Entities;
using Corporate.Contta.Schedule.Domain.Entities.DashboardAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Dto.Dashboard
{
    public class DashboardDto
    {
        public DataEmissao DataEmissaoMensais { get; set; }
        public ValorContabil ValorContabil { get; set; }
        public SimplesNacionalDash SimplesNacional { get; set; }    

        public List<FaturamentoMesnalSaida> ListFaturamentoMensaisSaida { get; set; }
        public List<FaturamentoMesnalEntrada> ListFaturamentoMensaisEntrada { get; set; }

        public Guid? CompanyInformation { get; set; }
        public double TotalImpostosProd { get; set; }

    }   
}

