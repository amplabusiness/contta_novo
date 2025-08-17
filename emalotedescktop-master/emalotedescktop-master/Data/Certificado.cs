using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmaloteContta.Data
{
    public class Certificado :BaseDTO
    {
        public int Id { get; set; }

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

        [Computed]
        public string Validade
        {
            get
            {
                return NonAfter.ToString();
            }
        }

        [Computed]
        public bool Vencido
        {
            get
            {
                return NonAfter.CompareTo(DateTime.Now) <= 0;
            }
        }

        [Computed]
        public string CorColuna
        {
            get
            {
                return Vencido ? "Red" : "Black";
            }
        }
    }
}
