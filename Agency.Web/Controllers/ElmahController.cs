using System.Web.Mvc;
using Agency.Common.Controller;

namespace Agency.Web.Controllers
{
    //[RoutePrefix("Admin")]
    //[Mvc5Authorize(AssignableToRolePermissions.CanAccessToSystemMaintenance)]
    //[Authorize(Roles = "Administrators")]
    [Authorize]
    public class ElmahController : BaseController
    {
        [Route("Elmah/{type?}")]
        public virtual ActionResult Index(string type)
        {
            return new ElmahResult(type);
        }
    }
}