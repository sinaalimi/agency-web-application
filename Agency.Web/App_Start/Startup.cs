using System;
using Agency.IocConfig;
using Agency.ServiceLayer.Contracts.StateCity;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using StructureMap.Web;
using Agency.ServiceLayer.Contracts.Users;
using Agency.ServiceLayer.EFService.Users;

namespace Agency.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }

        private static void ConfigureAuth(IAppBuilder appBuilder)
        {
            const int twoWeeks = 14;

            ProjectObjectFactory.Container.Configure(config => config.For<IDataProtectionProvider>()
                .HybridHttpOrThreadLocalScoped()
                .Use(() => appBuilder.GetDataProtectionProvider()));

            appBuilder.CreatePerOwinContext(
                () => ProjectObjectFactory.Container.GetInstance<ApplicationUserManager>());

            appBuilder.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                ExpireTimeSpan = TimeSpan.FromHours(2),
                SlidingExpiration = true,
                CookieName = "Agency",
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity =
                            ProjectObjectFactory.Container.GetInstance<IApplicationUserManager>().OnValidateIdentity()
                }
            });

            ProjectObjectFactory.Container.GetInstance<IApplicationRoleManager>()
           .SeedDatabase();

            ProjectObjectFactory.Container.GetInstance<IApplicationUserManager>()
               .SeedDatabase();

            ProjectObjectFactory.Container.GetInstance<IStateCityService>()
               .SeedDatabase();

        }
    }
}