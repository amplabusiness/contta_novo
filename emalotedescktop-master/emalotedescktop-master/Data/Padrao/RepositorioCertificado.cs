using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;

namespace EmaloteContta.Data.Padrao
{
    public class RepositorioCertificado : RepositorioPadrao<Certificado>
    {
        protected override string LoadConnectionString()
        {
            return string.Format(@"Data Source=C:\e-Malote\Bancos\bd_certificados.db3;Version=3;");
        }       
    }
}
