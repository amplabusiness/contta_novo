using Microsoft.Practices.Unity;
using RoboEconet.Infra.Data.Interface;
using XmlCteNfeNfsToClass.Infra.Repositorios;

namespace RoboEconet.Infra.IOC
{
    public class Resolver
    {
        public static void Resolve(UnityContainer container)
        {
            RegisterRepositories(container);
        }

        private static void RegisterRepositories(UnityContainer container)
        {
            container.RegisterType<IPisConfinsRepository, PisConfinsRepository>(new HierarchicalLifetimeManager());
            

        }
    }
}
