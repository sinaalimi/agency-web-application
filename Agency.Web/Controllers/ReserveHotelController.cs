using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Agency.Common.Controller;
using Agency.Common.Extentions;
using Agency.Common.Filters;
using Agency.DataLayer.Context;
using Agency.ServiceLayer.Contracts.Report;
using Agency.ServiceLayer.Contracts.ReserveHotel;
using Agency.ServiceLayer.Contracts.Tour;
using Agency.ServiceLayer.Contracts.Users;
using Agency.ViewModel.Report;
using Agency.ViewModel.ReserveHotel;

namespace Agency.Web.Controllers
{
    public class ReserveHotelController : BaseController
    {


        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IReportService _reportService;
        private readonly ITourService _tourService;
        private readonly IReserveHotelService _reservehotelService;
        private readonly IApplicationUserManager _userManager;
        //private readonly IApplicationUserManager _userManager;

        #endregion

        #region Ctor

        public ReserveHotelController(IUnitOfWork unitOfWork,
            IReportService reportService, ITourService tourService, IApplicationUserManager userService, IReserveHotelService reservehotelService)
        {
            _reportService = reportService;
           // _userManager = userManager;
            _unitOfWork = unitOfWork;
            _tourService = tourService;
            _userManager = userService;
            _reservehotelService = reservehotelService;


        }

        #endregion
        // GET: ReserveHotel
        public ActionResult Index()
        {

            return View();
        }


        #region ListMainHotel
        [HttpGet]
        public virtual ActionResult ListMainHotels()
        {
            //  var viewModel = await _tourService.GetPagedList();
            var viewModel = _reservehotelService.GetPagedListHotelReserve(new ReserveHotelSearchRequest());
            viewModel.SearchRequest.States = _reservehotelService.StateSelectListIetm();
            return View(viewModel);
        }


        [HttpPost]
        [AjaxOnly]
        // [Route("Customer-List")]
        //[Mvc5Authorize(Auth.CanViewApplicantList)]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual ActionResult ListAjax(ReserveHotelSearchRequest request)
        {
            var viewModel = _reservehotelService.GetPagedListHotelReserve(request);
            if (viewModel == null || !viewModel.MainHotels.Any()) return Content("no-more-info");
            //foreach (var item in viewModel.MainHotels)
            //{
            //    item.UserId = request.UserId;
            //}
            return PartialView("_ListAjax", viewModel);
        }
        #endregion

        #region Listreserves
        public async Task<ActionResult> ShowRooms(Guid hotelid, string start,int night)
        {

            var viewmodel = _reservehotelService.GetPagedListRooms(hotelid,start,night);
            if (viewmodel == null || !viewmodel.MainRooms.Any()) return Content("no-more-info");
            return View(viewmodel);
        }
        #endregion

        [HttpGet]
        public async Task<ActionResult> CreatePerson(Guid roomhotelid)
        {
           // var viewmodel = await _mainHotelService.GetCreateViewModelAsync();

            return View(new CreatePersonViewModel() {RoomHotelId = roomhotelid});
        }

        [HttpPost]
        public ActionResult CreatePerson(CreatePersonViewModel viewmodel)
        {

            if (ModelState.IsValid)
            {

                _reservehotelService.Create(viewmodel);
                this.NotySuccess("اطلاعات با موفقیت ثبت شد");
                return RedirectToAction("CreatePerson", "ReserveHotel");
            }
            else
            {
                return View(viewmodel);
            }

        }

    }
}