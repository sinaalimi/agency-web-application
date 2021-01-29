using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Agency.Common.Controller;
using Agency.Common.Extentions;
using Agency.Common.Filters;
using Agency.Common.Json;
using Agency.DataLayer.Context;
using Agency.DomainClasses.Entities.Person;
using Agency.ServiceLayer.Contracts.Cost;
using Agency.ServiceLayer.Contracts.Hotel;
using Agency.ServiceLayer.Contracts.Reserve;
using Agency.ServiceLayer.Contracts.StateCity;
using Agency.ServiceLayer.Contracts.Tour;
using Agency.ViewModel.Jarime;
using Agency.ViewModel.Person;
using Agency.ViewModel.Reserve;
using Agency.ViewModel.Vehicle;
using MvcSiteMapProvider.Collections.Specialized;
using MvcSiteMapProvider.Reflection;

namespace Agency.Web.Controllers
{
    [Authorize]
    public class ReserveController : BaseController
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHotelService _hotelService;
        private readonly IReserveService _reserveService;
        private readonly ICostService _costService;
        private readonly ITourService _tourService;
        //private readonly IApplicationUserManager _userManager;
        #endregion

        #region Ctor

        public ReserveController(IUnitOfWork unitOfWork,
            IHotelService hotelService, IReserveService reserveService, ICostService costService
            , ITourService tourService)
        {
            _unitOfWork = unitOfWork;
            //_userManager = userManager;
            _hotelService = hotelService;
            _reserveService = reserveService;
            _costService = costService;
            _tourService = tourService;
        }
        #endregion

        [HttpGet]
        public async Task<ActionResult> Create(Guid id)
        {
            var tour = await _tourService.GetTour(id);
            if (tour.FinishRegister < DateTime.Now.Date)
            {
                this.NotyAlert("مهلت ثبت نام برای این تور تمام شده است", true);
                return RedirectToAction("List", "Tour");
            }
            var remaincount = await _tourService.RemainCount(id);
            var model = new PersonCountViewModel() { TourId = id, RemainTourCapacity = remaincount };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> CreateReserve(PersonCountViewModel model)
        {
            if (model.AdultCount + model.ChildCount > model.RemainTourCapacity)
            {
                this.AddErrors("AdultCount", "تعداد افراد از ظرفیت تور بیشتر است");
                return View("Create", model);
            }
            CreateReserveViewModel viewmodel = new CreateReserveViewModel();
            var v = await _reserveService.GetHotels(model.TourId);
            var vehicle = await _reserveService.GetVehicles(model.TourId);

            viewmodel.ParentViewModel = new CreateParentViewModel();
            viewmodel.ListPersonViewModel = new List<CreatePersonViewModel>();
            viewmodel.TourId = model.TourId;
            viewmodel.HotelList = v;
            viewmodel.VehicleList = vehicle;

            int count = (model.AdultCount + model.ChildCount + model.BabyCount) - 1;
            for (int i = 0; i < count; i++)
            {
                viewmodel.ListPersonViewModel.Add(new CreatePersonViewModel());
            }
            if (model.AdultCount == 2)
                viewmodel.CoupleBed = true;
            return View("CreateReserve", viewmodel);
        }

        [HttpPost]
        public async Task<ActionResult> CreateReserve2(CreateReserveViewModel model)
        {
            model.ParentViewModel.AgeRange = AgeRange.Adult;
            var reserveid = await _reserveService.CreateReservePerson(model);
            return RedirectToAction("CreateReserve3", new { reserveid = reserveid });
        }

        [HttpGet]
        public async Task<ActionResult> CreateReserve3(Guid reserveid)
        {
            var persons = await _reserveService.GetPerson(reserveid);
            return View("CreateReserve3", persons);
        }

        class ReserveJson
        {
            public bool Success;
            public bool Gender;
            public bool IsAuthenticate;
            public int result;
        }
        [HttpGet]
        public async Task<ActionResult> ReserveSeat(Guid personid, Guid seatid, Guid reserveid)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                ReserveJson rr = new ReserveJson() { IsAuthenticate = false };
                return Json(rr, JsonRequestBehavior.AllowGet);
            }
            var result = await _reserveService.SeatReserve(personid, seatid, reserveid);
            ReserveJson r = new ReserveJson();
            r.Success = true;
            r.Gender = false;
            r.IsAuthenticate = true;
            r.result = result;
            return Json(r, JsonRequestBehavior.AllowGet);

        }

        #region List
        [HttpGet]
        public async Task<ActionResult> List()
        {
            var viewModel = await _reserveService.GetPagedListAsync(new ReserveSearchRequest());
            return View(viewModel);
        }

        [AjaxOnly]
        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> ListAjax(ReserveSearchRequest request)
        {
            var viewModel = await _reserveService.GetPagedListAsync(request);
            if (viewModel.Reserves == null || !viewModel.Reserves.Any()) return Content("no-more-info");
            return PartialView("_ListAjax", viewModel);
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var viewModel = await _reserveService.GetEdit(id.Value);
            if (viewModel == null) return HttpNotFound();

            return View("Edit", viewModel);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(EditReserveViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var model = await _reserveService.GetEdit(viewModel.Id);
                viewModel.HotelList = model.HotelList;
                viewModel.VehicleList = model.VehicleList;
                return View(viewModel);
            }
            else
            {
                var newItem = await _reserveService.Edit(viewModel);
                this.NotyInformation("اطلاعات تور با موفقیت ویرایش شد");
                return RedirectToAction("List", "Reserve");
            }
        }
        #endregion

        #region Delete
        [HttpGet]
        [ValidateAntiForgeryToken]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public async Task<ActionResult> Delete(Guid personid, Guid reserveid)
        {
            var ans = await _reserveService.DeleteAsync(personid);
            if (ans.Success)
                return RedirectToAction("List", "Reserve");
            return Json(new { success = false, msg = ans.Message });
        }
        #endregion

        #region Show Persons for cancel
        [HttpGet]
        public virtual async Task<ActionResult> Cancel(Guid id)
        {
            var model = await _reserveService.GetPersonsOfReserve(id);
            if (model == null) return HttpNotFound();
            return View("CancelReserve", model);
        }
        #endregion

        #region CancelAjax


        [HttpGet]
        public ActionResult CancelAllNoAjax(Guid reserveId)
        {
            var model = _reserveService.TimeCalculate2(reserveId);
            return View("CancelAllNoAjax", model);
        }

        [HttpPost]
        public async Task<ActionResult> CancelAllReserve(JarimeViewModel model)
        {
            var fileName = await _reserveService.CancelAllAsync(model);
            return RedirectToAction("DownloadCancelFile", "Reserve", new { filename = fileName });
        }

        [HttpGet]
        public ActionResult CancelAjax(Guid reserveId, Guid personId)
        {
            var model = _reserveService.TimeCalculate(reserveId, personId);
            return PartialView("_CancelReserveInfo", model);
        }

        [HttpPost]
        public async Task<ActionResult> CancelReserve(JarimeViewModel model)
        {
            var fileName = await _reserveService.CancelAsync(model.ReserveId, model.PersonId, model.PenaltyPrice);
            var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/Files/Canceled/"), fileName);
            return Json(model.ReserveId.ToString(), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region DownloadCancelFile
        public ActionResult DownloadCancelFile(string filename)
        {
            if (filename[0] == '\"')
            {
                filename = filename.Substring(1, filename.Length - 2);
            }
            var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/Files/Canceled/"), filename);
            return File(path, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
        }
        #endregion

        #region DownloadContractFile
        public ActionResult DownloadContractFile(string filename)
        {
            if (filename[0] == '\"')
            {
                filename = filename.Substring(1, filename.Length - 2);
            }
            var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/Files/FinalReserves/"), filename);
            return File(path, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
        }
        #endregion

        #region DeletePersonOfSeat

        [HttpGet]
        [ValidateAntiForgeryToken]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public async Task<ActionResult> DeletePersonOfSeat(Guid personid, Guid reserveid)
        {
            var ans = await _reserveService.DeletePersonOfSeat(personid);
            if (ans.Success)
                return RedirectToAction("List", "Reserve");
            return Json(new { success = false, msg = ans.Message });
        }

        #endregion


        public async Task<ActionResult> SetTemporary(Guid? reserveid)
        {
            var f = await _reserveService.SetTemporary(reserveid.Value);
            if (f.HasValue())
            {
                this.NotyAlert("ثبت نهایی با موفقیت انجام شد");
                //return View("List");
                return RedirectToAction("ShowCost", "Cost", new { reserveid = reserveid });
            }
            else
            {
                this.NotySuccess("ثبت نهایی با مشکل مواجه شد");
                var showcost = _costService.GetEditView(reserveid.Value);
                showcost.ReserveId = reserveid.Value;
                return RedirectToAction("ShowCost", "Cost", showcost);
            }
        }
    }
}