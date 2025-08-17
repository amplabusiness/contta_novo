using Corporate.Contta.Schedule.Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Domain.Entities.ConfigurationAdmin
{
    public class AutorizationAdmin : Entity
    {
        public Guid? UserId { get; set; }
        public Guid? EmpresaId { get; set; }
        public string NomeUsuario { get; set; }
        public DateTime DataRegistro { get; set; }
        public bool Ativo { get; set; }
        public string UserIdAutorizate { get; set; }
        public List<string> ListUserId { get; set; }
        public string Cnpj { get; set; }
        public string NameClient { get; set; }
    }
}
