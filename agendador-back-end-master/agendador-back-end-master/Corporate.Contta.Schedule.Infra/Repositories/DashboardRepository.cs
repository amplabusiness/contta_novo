using Corporate.Contta.Schedule.Domain.Entities.DashboardAgg;
using Corporate.Contta.Schedule.Infra.Repositories.Base;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Infra.Repositories
{
    public class DashboardRepository 
    {
        private static DashboardHomeRepository _dashboardHomeRepository = new DashboardHomeRepository();
        private static ProductRepository _produtos = new ProductRepository();
      
        public  Dashboard GetByCompany(Guid? empresaId , DateTime dhEmi)
        {
            try
            {
                IMongoTbSimples mongoTbSimples = new IMongoTbSimples();
                var dashboadDto = mongoTbSimples.GetDashboard(empresaId, dhEmi);

                Dashboard dasboard = new Dashboard();                              

                if (dashboadDto.Result.Item1 != null)
                {
                    dasboard = dashboadDto.Result.Item1;
                    dasboard.SimplesNacional.DateFounded = dasboard.SimplesNacional.DateFounded;
                }

                if (dashboadDto.Result.faturamento12 > 0.0)
                    dasboard.SimplesNacional.FaturamentoAnual = dashboadDto.Result.faturamento12;

                if(dasboard.ValorContabil.ValorSaidaMercadoria > 0)
                {
                    var listImpostos = _produtos.GetAllImpostosProd(empresaId, dhEmi);
                    var listaImpostoIcms = _produtos.GetAllImpostosProdIcms(empresaId, dhEmi);
                    var listaImportoPis = _produtos.GetAllImpostosProdPisConfins(empresaId, dhEmi);

                    if(listImpostos.Result != null)
                    {
                        dasboard.TotalImpostosProd = listImpostos.Result.Select(c => c.VlProduto).Sum();
                    }

                    var baseCalculoRemontada = dasboard.ValorContabil.BaseCalculo - dasboard.ValorContabil.NotaDevolucaoSaida;
                    dasboard.ValorContabil.BaseCalculo = baseCalculoRemontada;
                    dasboard.ValorContabil.BaseIcms = baseCalculoRemontada - listaImpostoIcms.Result.Select(c => c.VlProduto).Sum();                    
                    dasboard.ValorContabil.BaseConfins = baseCalculoRemontada - listaImportoPis.Result.Select(c => c.VlProduto).Sum();
                }             
              
                return dasboard;

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
