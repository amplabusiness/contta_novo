namespace EmaloteContta.Data.Padrao
{
    public class RepositorioXmlCliente : RepositorioPadrao<XmlCliente>
    {
        protected override string LoadConnectionString()
        {
            return string.Format(@"Data Source=C:\e-Malote\Bancos\bd_xmlClientes.db3;Version=3;");
        }
    }
}
