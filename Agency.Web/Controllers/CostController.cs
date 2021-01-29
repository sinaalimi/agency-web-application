using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Agency.Common.Controller;
using Agency.Common.Extentions;
using Agency.DataLayer.Context;
using Agency.ServiceLayer.Contracts.Cost;
using Agency.ServiceLayer.Contracts.Vehicle;

namespace Agency.Web.Controllers
{
    [Authorize]
    public class CostController : BaseController
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICostService _costService;
        //private readonly IApplicationUserManager _userManager;
        #endregion

        #region Ctor

        public CostController(IUnitOfWork unitOfWork,
            ICostService costService)
        {
            _unitOfWork = unitOfWork;
            //_userManager = userManager;
            _costService = costService;
        }
        #endregion

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowCost(Guid? reserveid)
        {
            var showcost = _costService.GetEditView(reserveid.Value);
            if (!showcost.IsAllSeatsReserved)
            {
                this.NotyAlert("لطفا برای همه افراد صندل رزرو کنید");
                return RedirectToAction("CreateReserve3", "Reserve", new { reserveid = reserveid.Value });
            }              
            showcost.ReserveId = reserveid.Value;
            return View("ShowCost",showcost);
        }
       
    }
}