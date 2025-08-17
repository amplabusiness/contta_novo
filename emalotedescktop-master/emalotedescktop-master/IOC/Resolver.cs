
using Microsoft.Practices.Unity;
using EmaloteContta.Infra.Repositorios;
using EmaloteContta.Models.Respositories;
using EmaloteContta.Models.Service;
using EmaloteContta.Servico;

namespace EmaloteContta.IOC
{
    public class Resolver
    {
        public static void Resolve(UnityContainer container)
        {
            RegisterRepositories(container); 
        }

        private static void RegisterRepositories(UnityContainer container)
        {            
            container.RegisterType<IEmpresaDestinatariaRepositorio, EmpresaDestinatariaRepositorio>(new HierarchicalLifetimeManager());
            //container.RegisterType<IEmpresaEmitenteRepositorio,     EmpresaEmitenteRepositorio>(new HierarchicalLifetimeManager());
            container.RegisterType<IImpostosRepositorio,            ImpostosRepositorio>(new HierarchicalLifetimeManager());
            container.RegisterType<IProdutosRepositorio,            ProdutosRepositorio>(new HierarchicalLifetimeManager());
            container.RegisterType<INotaFiscalRepositorio,          NotaFiscalLocalRepositorio>(new HierarchicalLifetimeManager());
            container.RegisterType<INotaFiscalServices,             NotaFiscalServices>(new HierarchicalLifetimeManager());
            
        }

    }
}
