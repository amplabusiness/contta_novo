using MediatR;
using System;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class NewConfigurationUserRequest : IRequest<Response.Response>
    {
        public Guid Id { get; set; }
        public bool DashboardTutorial { get; set; }
        public bool SubstituicaoTutorial { get; set; }
        public bool PisConfinsTutorial { get; set; }
        public bool ClickedDownLoadButton { get; set; }
        public bool ClickedChangeCompanyButton { get; set; }
        public bool IcmSInsento { get; set; }
        public bool IcmSImune { get; set; }
        public bool PISCofinsIsento { get; set; }
        public bool PISCofinsImune { get; set; }
        public bool ProductTb { get; set; }
        public Guid UserId { get; set; }
        public string Token { get; set; }
    }
}
