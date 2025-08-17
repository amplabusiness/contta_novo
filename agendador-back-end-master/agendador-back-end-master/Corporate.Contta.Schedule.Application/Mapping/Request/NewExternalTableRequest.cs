using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class NewExternalTableRequest : IRequest<Response.Response>
    {
        public string Item { get; set; }
        public string Subitem { get; set; }
        public string Descricao { get; set; }
        public string CEST { get; set; }
        public string NCM { get; set; }
        public int MVA1 { get; set; }
        public int MVA2 { get; set; }
        public int MVA3 { get; set; }
        public int MVA4 { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
    }
}
