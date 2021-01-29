using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using EntityFramework.Caching;
using Agency.Common.Extentions;
using Agency.IocConfig;
using Agency.ServiceLayer.Contracts.Common;
using StructureMap.Web.Pipeline;
using Agency.Web;
using ElmahEFLogger.CustomElmahLogger;

namespace Agency.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        #region Application_Start
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            try
            {
                RouteConfig.RegisterRoutes(RouteTable.Routes);
                FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
                ApplicationStart.Config();
                BundleConfig.RegisterBundles(BundleTable.Bundles);

            }
            catch
            {
                HttpRuntime.UnloadAppDomain(); // سبب ری استارت برنامه و آغاز مجدد آن با درخواست بعدی می‌شود
                throw;
            }

        }
        #endregion

        #region Application_EndRequest
        protected void Application_EndRequest(object sender, EventArgs e)
        {
            try
            {
                foreach (var task in ProjectObjectFactory.Container.GetAllInstances<IRunAfterEachRequest>())
                {
                    task.Execute();
                }
            }
            catch (Exception)
            {
                HttpContextLifecycle.DisposeAndClearAll();
            }
        }
        #endregion

        #region Application_BeginRequest
        private void Application_BeginRequest(object sender, EventArgs e)
        {
            foreach (var task in ProjectObjectFactory.Container.GetAllInstances<IRunOnEachRequest>())
            {
                task.Execute();
            }
        }
        #endregion

        #region ShouldIgnoreRequest
        private bool ShouldIgnoreRequest()
        {
            string[] reservedPath =
              {
                  "/__browserLink",
                  "/Scripts",
                  "/Content"
              };

            var rawUrl = Context.Request.RawUrl;
            if (reservedPath.Any(path => rawUrl.StartsWith(path, StringComparison.OrdinalIgnoreCase)))
            {
                return true;
            }

            return BundleTable.Bundles.Select(bundle => bundle.Path.TrimStart('~'))
                      .Any(bundlePath => rawUrl.StartsWith(bundlePath, StringComparison.OrdinalIgnoreCase));
        }
        #endregion

        #region Private
        private void SetPermissions(IEnumerable<string> permissions)
        {
            Context.User =
                new GenericPrincipal(Context.User.Identity, permissions.ToArray());
        }
        #endregion

        #region Application_Error
        protected void Application_Error()
        {
            foreach (var task in ProjectObjectFactory.Container.GetAllInstances<IRunOnError>())
            {
                task.Execute();
            }
        }
        #endregion
    }
}
