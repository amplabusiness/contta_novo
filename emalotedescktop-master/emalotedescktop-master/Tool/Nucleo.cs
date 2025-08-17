using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Text;
using EmaloteContta.IOC;

namespace EmaloteContta
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
