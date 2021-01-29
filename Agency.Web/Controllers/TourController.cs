using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Agency.Common.Controller;
using Agency.Common.Extentions;
using Agency.Common.Filters;
using Agency.Common.Json;
using Agency.DataLayer.Context;
using Agency.DomainClasses.Entities.tour;
using Agency.ServiceLayer.Contracts.Hotel;
using Agency.ServiceLayer.Contracts.Reserve;
using Agency.ServiceLayer.Contracts.Tour;
using Agency.ViewModel.Hotel;
using Agency.ViewModel.Reserve;
using Agency.ViewModel.Tour;

namespace Agency.Web.Controllers
{
    [Authorize]
    public class TourController : BaseController
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITourService _tourService;
        private readonly IReserveService _reserveService;
        //private readonly IApplicationUserManager _userManager;
        #endregion

        #region Ctor

        public TourController(IUnitOfWork unitOfWork,
            ITourService tourService,IReserveService reserveService)
        {
           _tourService = tourService;
           _reserveService = reserveService;

            //_userManager = userManager;
            _unitOfWork = unitOfWork;
        

        }
        #endregion
        public ActionResult Index()
        {
            return View();
        }

        #region create
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var viewmodel = await _tourService.GetCreateViewModelAsync();
            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowUploadSpecialFilesOnly(".png,.jpg,.jpeg,.gif", justImage: true)]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(CreateTourViewModel viewModel)
        {
            if (viewModel.StartTime >= viewModel.EndTime )
            {
                this.AddErrors("StartTime","تاریخ را تصحیح کنید");
            }
            if (viewModel.StartTime < DateTime.Now)
            {
                this.AddErrors("StartTime","تاریخ معتبر نیست");
            }
            if (viewModel.FinishRegister < DateTime.Now)
            {
                this.AddErrors("FinishRegister","تاریخ پایان ثبت نام نباید قبل از تاریخ امروز باشد");
                
            }
            if (viewModel.FinishRegister > viewModel.EndTime)
            {
                this.AddErrors("FinishRegister", "تاریخ پایان ثبت نام نباید بعد از تاریخ برگشت باشد ");
            }
            if (viewModel.FinishRegister > viewModel.StartTime)
            {
                this.AddErrors("FinishRegister", "تاریخ پایان ثبت نام نباید بعد از تاریخ اعزام باشد");
            }

            for (int i = 0; i < viewModel.VehicleList.Count-1; i++)
            {
                for (int j = i + 1; j < viewModel.VehicleList.Count; j++)
                {
                    if (viewModel.VehicleList[j].VehicleId == viewModel.VehicleList[i].VehicleId)
                    {
                        this.AddErrors("VehicleList["+j+"].VehicleId","این وسیله نقلیه قبلا انتخاب شده است");
                    }
                }
                
            }
            if (ModelState.IsValid)
            {
                _tourService.Create(viewModel);
                this.NotySuccess("تور با موفقیت ثبت شد");
                return RedirectToAction("List", "Tour");
            }
            else
            {
                var temp = await _tourService.GetCreateViewModelAsync2(viewModel);
                viewModel.States = temp.States;
                viewModel.HotelsSelectList = temp.HotelsSelectList; 
                viewModel.SourceCities = temp.SourceCities;
                viewModel.DesCities = temp.DesCities;
                return View(viewModel);

            }
        }
        #endregion

        #region List
        [HttpGet]
        public virtual async Task<ActionResult> List()
        {
            var viewModel = await _tourService.GetPagedListAsync(new TourSearchRequest());
            return View(viewModel);
        }

        [AjaxOnly]
        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> ListAjax(TourSearchRequest request)
        {
            var viewModel = await _tourService.GetPagedListAsync(request);
            if (viewModel.Tours == null || !viewModel.Tours.Any()) return Content("no-more-info");
            return PartialView("_ListAjax", viewModel);
        }
        #endregion

        #region Edit
        [HttpGet]
        public virtual async Task<ActionResult> Edit(Guid? id)
        {

            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var viewModel = await _tourService.GetEditViewAsync(id.Value);
            if (viewModel == null) return HttpNotFound();

            return View("Edit", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowUploadSpecialFilesOnly(".png,.jpg,.jpeg,.gif", justImage: true)]
        [ValidateInput(false)]
        public virtual async Task<ActionResult> Edit(EditTourViewModel viewModel)
        {
            if (viewModel.VehicleList.Count < 2)
            {
                this.AddErrors("error","this is error");
                this.NotyAlert("لطفا وسیله نقلیه انتخاب کنید");
            }
            if (!ModelState.IsValid)
            {
                var model = await _tourService.GetEditViewAsync(viewModel.Id);
                viewModel.DesCitiesListItem = model.DesCitiesListItem;
                viewModel.DesStatesListItem = model.DesStatesListItem;
                viewModel.Hotels = model.Hotels;
                viewModel.SourceCitiesLitsItem = model.SourceCitiesLitsItem;
                viewModel.SourceStatesListItem = model.SourceStatesListItem;
                viewModel.VehicleList = model.VehicleList;
                return View(viewModel);
            }
            else
            {
                var newItem = await _tourService.Edit(viewModel);
                this.NotyInformation("اطلاعات تور با موفقیت ویرایش شد");
                return RedirectToAction("List", "Tour");
            }
        }
        #endregion

        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> Delete(Guid id)
        {
            var ans = await _tourService.DeleteAsync(id);
            if (ans.Success)
                return Content("ok");
            return Json(new { success = false, msg = ans.Message });
        }
        #endregion

        #region Cancel
        [HttpGet]
        public async Task<ActionResult> Cancel(Guid id)
        {
            var ans = await _tourService.CancelAsync(id);
            if (ans < 1)
                this.NotyAlert("کنسل کردن تور با مشکل مواجه شد");
            else
                this.NotySuccess("تور با موفقیت کنسل شد");
            return RedirectToAction("List", "Tour");
        }
        #endregion

        #region Details
        [HttpGet]
        public async Task<ActionResult> Details(Guid id)
        {
            var viewModel = await _tourService.GetTourDetails(id);
            if (viewModel == null) return HttpNotFound();
            return View(viewModel);
        }
        #endregion
    }
}