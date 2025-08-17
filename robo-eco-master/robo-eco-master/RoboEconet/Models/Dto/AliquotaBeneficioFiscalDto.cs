namespace RoboEconet.Models
{
    public class AliquotaBeneficioFiscalDto
    {
        public string UF { get; set; }
        public decimal? Aliquota { get; set; }
        public decimal? Protege { get; set; }
        public decimal? AliquotaEfetiva { get; set; }
        public string NCM { get; set; }
        public string Descricao { get; set; }
    }
}
