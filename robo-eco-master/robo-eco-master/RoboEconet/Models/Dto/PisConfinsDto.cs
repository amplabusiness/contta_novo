using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace RoboEconet.Models
{
    public class PisConfinsDto
    {
        public  Guid Id { get; set; }
        public string NCM { get; set; }
        public string Descricao { get; set; }
        public string NCMPai { get; set; }

        public List<NCMDto> NCMS { get; set; } = new List<NCMDto>();
        public List<AliquotaPisConfinsDto> Aliquotas { get; set; } = new List<AliquotaPisConfinsDto>();
        public List<ObservacaoDto> Observacoes { get; set; } = new List<ObservacaoDto>();
        public List<CSTDto> CSTS { get; set; } = new List<CSTDto>();
        public List<EFDDto> EFDS { get; set; } = new List<EFDDto>();
        public bool Monofasico { get; set; }
    }
}
