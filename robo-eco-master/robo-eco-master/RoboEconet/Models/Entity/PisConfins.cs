using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace RoboEconet.Models
{
    public class PisConfins : RoboEconet.Models.Entity.Entity
    {
      
        public string NCM { get; set; }
        public string Descricao { get; set; }
        public string NcmPai { get; set; }
        public List<NCM> NCMS { get; set; } = new List<NCM>();
        public List<AliquotaPisConfins> Aliquotas { get; set; } = new List<AliquotaPisConfins>();
        public List<Observacao> Observacoes { get; set; } = new List<Observacao>();
        public List<CST> CSTS { get; set; } = new List<CST>();
        public List<EFD> EFDS { get; set; } = new List<EFD>();
        public bool Monofasico { get; set; }
    }
}
