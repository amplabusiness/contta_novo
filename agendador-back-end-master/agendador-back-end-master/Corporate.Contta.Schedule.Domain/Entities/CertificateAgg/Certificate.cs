using System;

namespace Corporate.Contta.Schedule.Domain.Entities.CertificateAgg
{
    public class Certificado
    {
        public Guid? IdCertificao { get; set; }
        public int Id { get; set; }

        public string CNPJ { get; set; }

        public string RazaoSocial { get; set; }
        public DateTime NonAfter { get; set; }

        public DateTime NonBefore { get; set; }

        public string SerialNumber { get; set; }

        public string PublicKey { get; set; }

        public string PrivateKey { get; set; }

        public byte[] RawData { get; set; }

        public string ContentType { get; set; }

        public string FriendlyName { get; set; }

        public bool Verify { get; set; }

        public string SimpleName { get; set; }

        public string SignatureAlgorithm { get; set; }

        public string Subject { get; set; }

        public bool Archived { get; set; }

        public int LengthRawData { get; set; }

        public string Thumbprint { get; set; }

        public int Version { get; set; }

        public string Token { get; set; }

        public DateTime DataCadastro { get; set; }

        public bool Integrado { get; set; }
    }
}
