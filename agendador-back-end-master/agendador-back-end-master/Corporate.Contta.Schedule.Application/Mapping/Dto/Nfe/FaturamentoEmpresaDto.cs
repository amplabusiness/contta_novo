using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using System;

namespace Corporate.Contta.Schedule.Application.Mapping.Dto
{
    public class FaturamentoEmpresaDto
    {
        public Guid CompanyInformation { get; set; }

        public Faturamento Faturamento { get; set; }
    }
}
