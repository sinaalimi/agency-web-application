using System.Web.Mvc;
using System.Web.Routing;

namespace Agency.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            #region IgnoreRoutes
            routes.IgnoreRoute("Content/{*pathInfo}");
            routes.IgnoreRoute("Scripts/{*pathInfo}");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("{resource}.ico");
            routes.IgnoreRoute("{resource}.png");
            routes.IgnoreRoute("{resource}.jpg");
            routes.IgnoreRoute("{resource}.gif");
            routes.IgnoreRoute("{resource}.txt");
            #endregion

            routes.LowercaseUrls = true;
            routes.MapMvcAttributeRoutes();


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "account", action = "login", id = UrlParameter.Optional }
            );



        }
    }
}
