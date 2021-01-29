using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap;
using StructureMap.Configuration.DSL;
using Agency.ServiceLayer.EFService.Users;

namespace Agency.IocConfig
{
    public class ServiceLayerRegistery : Registry
    {
        public ServiceLayerRegistery()
        {
            //Policies.SetAllProperties(y =>
            //{
            //    y.OfType<IActivityLogService>();
            //});
            Scan(scanner =>
            {
                scanner.WithDefaultConventions();
                scanner.AssemblyContainingType<ApplicationUserManager>();

            });
        }
    }
}
