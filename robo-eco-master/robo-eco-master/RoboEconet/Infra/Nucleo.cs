using Microsoft.Practices.Unity;
using RoboEconet.Infra.IOC;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoboEconet.Infra
{
    public class Nucleo
    {
        public static UnityContainer Container { get; set; }

        public Nucleo()
        {

        }

        public void Start()
        {
            Container = new UnityContainer();

            Resolver.Resolve(Container);
        }
    }
}
