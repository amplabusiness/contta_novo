namespace EmaloteContta.Data.Padrao
{
    public class RepositorioPdfCliente : RepositorioPadrao<PdfCliente>
    {
        protected override string LoadConnectionString()
        {
            return string.Format(@"Data Source=C:\e-Malote\Bancos\bd_pfClientes.db3;Version=3;");
        }
    }
}
