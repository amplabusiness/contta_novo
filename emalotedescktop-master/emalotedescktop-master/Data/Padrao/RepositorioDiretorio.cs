using eMalote.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EmaloteContta.Data.Padrao
{
    public class RepositorioDiretorio : RepositorioPadrao<Diretorio>
    {
        protected override string LoadConnectionString()
        {
            return string.Format(@"Data Source=C:\e-Malote\Bancos\bd_diretorios.db3;Version=3;");
        }
    }
}
