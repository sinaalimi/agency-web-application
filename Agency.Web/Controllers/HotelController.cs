using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using Agency.Common.Controller;
using Agency.Common.Extentions;
using Agency.Common.Filters;
using Agency.DataLayer.Context;
using Agency.ServiceLayer.Contracts.Hotel;
using Agency.ServiceLayer.Contracts.StateCity;
using Agency.ViewModel.Hotel;

namespace Agency.Web.Controllers
{
    [Authorize]
    public class HotelController : BaseController
    {

        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHotelService _hotelService;
        private readonly IStateCityService _stateCityService;
        //private readonly IApplicationUserManager _userManager;
        #endregion

        #region Ctor

        public HotelController(IUnitOfWork unitOfWork,
            IHotelService hotelService,IStateCityService stateCityService)
        {
            _unitOfWork = unitOfWork;
            //_userManager = userManager;
            _hotelService = hotelService;
            _stateCityService = stateCityService;
        }
        #endregion

        #region create
        [HttpGet]
        [Route("هتل/ایجاد")]
        public async Task<ActionResult>  Create()
        {
            var viewmodel = await _hotelService.GetCreateViewModelAsync();
            return View(viewmodel);
        }

        [HttpPost]
        [Route("هتل/ایجاد")]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(CreateHotelViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.PicSrFile != null)
                {
                    viewModel.ImageSource = this.Upload(viewModel.PicSrFile, "/Content/HotelPhotoes/");
                }
                _hotelService.Create(viewModel);
                this.NotySuccess("هتل با موفقیت ثبت شد");
                return RedirectToAction("List", "Hotel");
            }
            else
            {
                var temp = await _hotelService.GetCreateViewModelAsync();
                viewModel.States = temp.States;
                viewModel.Cities=new List<SelectListItem>();
                return View(viewModel);
            }
        }
        #endregion

        [HttpPost]
        public ActionResult SelectCities(Guid id)
        {
            var model  = _stateCityService.GetCities(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SelectHotels(Guid id)
        {
            var hotels = _hotelService.GetHotelsOfCity(id);
            return Json(hotels, JsonRequestBehavior.AllowGet);
        }

        #region List
        [HttpGet]
        [Route("هتل/لیست")]
        public async Task<ActionResult> List()
        {
            var viewModel = await _hotelService.GetPagedListAsync(new HotelSearchRequest() {States = await _stateCityService.GetStatesAsync()});
            return View(viewModel);
        }

        [AjaxOnly]
        [HttpPost]
        [Route("هتل/لیست")]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> ListAjax(HotelSearchRequest request)
        {
            var viewModel = await _hotelService.GetPagedListAsync(request);
            if (viewModel.Hotels == null || !viewModel.Hotels.Any()) return Content("no-more-info");
            return PartialView("_ListAjax", viewModel);
        }
        #endregion

        #region Edit
        [Route("ویرایش-هتل-{id}")]
        [HttpGet]
        public virtual async Task<ActionResult> Edit(Guid? id)
        {
         
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var viewModel = await _hotelService.GetEditViewAsync(id.Value);
            if (viewModel == null) return HttpNotFound();

            return View("Edit", viewModel);
        }

        [Route("ویرایش-هتل-{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public virtual async Task<ActionResult> Edit(EditHotelViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            else
            {
                if (viewModel.PicSrFile != null)
                {
                   
                        viewModel.ImageSource = this.Upload(viewModel.PicSrFile, "/Content/HotelPhotoes/");
                }
                var newItem = await _hotelService.Edit(viewModel);
                this.NotyInformation("اطلاعات هتل با موفقیت ویرایش شد");
                return RedirectToAction("List", "Hotel");

            }
        }
        #endregion
        
        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> Delete(Guid id)
        {
            var ans = await _hotelService.DeleteAsync(id);
            if (ans.Success)
                return Content("ok");
            return Json(new { success = false, msg = ans.Message });
        }
        #endregion

        #region Details
        [HttpGet]
        [Route("هتل/اطلاعات-بیشتر-{id}")]
        public async Task<ActionResult> Details(Guid id)
        {
            var viewModel = await _hotelService.GetHotelDetails(id);
            if (viewModel == null) return HttpNotFound();
            return View(viewModel);
        }
        #endregion

    }
}