using System;
using System.Linq;
using AutoMapper;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using Agency.AutoMapper;

namespace Agency.IocConfig
{
    public class AutoMapperRegistry : Registry
    {
        public AutoMapperRegistry()
        {
            var profiles =
                from t in typeof(UserProfile).Assembly.GetTypes()
                where typeof(Profile).IsAssignableFrom(t)
                select (Profile)Activator.CreateInstance(t);

            var config = new MapperConfiguration(cfg =>
            {

                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            });
            Scan(scan => {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
                scan.AddAllTypesOf<Profile>().NameBy(item => item.FullName);

            });
            For<MapperConfiguration>().Use(config);
            For<IMapper>().Use(ctx => ctx.GetInstance<MapperConfiguration>().CreateMapper(ctx.GetInstance));
        }
    }
}
