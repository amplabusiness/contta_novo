using Corporate.Contta.Schedule.Application.Mapping.Dto.CompanyInformation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Response.GetInformationByMasterId
{
    public class GetInformationByMasterIdResponse
    {
        public GetInformationByMasterIdResponse(List<CompanySummaryInformationDto> companySummaryInformationDtos)
        {
            CompanySummaryInformation = companySummaryInformationDtos;
        }
        public List<CompanySummaryInformationDto> CompanySummaryInformation { get; private set; }
    }
}
