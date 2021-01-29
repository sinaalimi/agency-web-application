using System.Web.Mvc;
using Agency.Common.Filters;
//using ElmahHandledErrorLoggerFilter = Acuity.Common.Filters.ElmahHandledErrorLoggerFilter;

namespace Agency.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {

            // logg action errors
            //filters.Add(new ElmahHandledErrorLoggerFilter());
            //  logg xss attacks
            filters.Add(new ElmahRequestValidationErrorFilter());

            filters.Add(new RemoveServerHeaderFilterAttribute());

            //filters.Add(new ForceWww("http://localhost:25890/"));

        }
    }
}