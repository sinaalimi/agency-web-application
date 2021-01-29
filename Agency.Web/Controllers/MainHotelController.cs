using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Agency.Common.Controller;
using Agency.Common.Extentions;
using Agency.Common.Filters;
using Agency.DataLayer.Context;
using Agency.ServiceLayer.Contracts.MainHotel;
using Agency.ServiceLayer.Contracts.Tour;
using Agency.ViewModel.MainHotel;
using Agency.ViewModel.Tour;

namespace Agency.Web.Controllers
{
    public class MainHotelController : BaseController
    {
        // GET: MainHotel
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMainHotelService _mainHotelService;
        //private readonly IApplicationUserManager _userManager;
        #endregion

        #region Ctor

        public MainHotelController(IUnitOfWork unitOfWork,
            IMainHotelService mainHotelService)
        {
            _mainHotelService = mainHotelService;
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
            var viewmodel = await _mainHotelService.GetCreateViewModelAsync();

            return View(viewmodel);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateMainHotelViewModel viewModel)
        {
            int ii = 0; 
            foreach (var item in viewModel.RoomMainHotelList)
            {
                
                if (item.FirstDate > item.LasTime)
                {
                    this.AddErrors("RoomMainHotelList["+ ii + "].FirstDate", "تاریخ معتبر نیست");
                 //   break;
                }
                if (item.FirstDate < DateTime.Now)
                {
                    this.AddErrors("RoomMainHotelList[" + ii + "].FirstDate", "تاریخ معتبر نیست");
                  //  break;
                }
                ii++;
            }

            for (int i = 0; i < viewModel.RoomMainHotelList.Count - 1; i++)
            {
                for (int j = i + 1; j < viewModel.RoomMainHotelList.Count; j++)
                {
                    if (viewModel.RoomMainHotelList[j].RoomId == viewModel.RoomMainHotelList[i].RoomId)
                    {
                        this.AddErrors("RoomMainHotelList[" + j + "].RoomId", "این اتاق قبلا انتخاب شده است");
                    }
                }

            }

            if (ModelState.IsValid)
            {
                if (viewModel.Image != null)
                {
                    viewModel.Image = this.Upload(viewModel.PicSrFile, "/Content/MainHotelPhotoes/");
                }
                _mainHotelService.Create(viewModel);

                return RedirectToAction("Create", "MainHotel");
            }
            else
            {
                var temp = await _mainHotelService.GetCreateViewModelAsync2(viewModel);
                viewModel.States = temp.States;
                viewModel.Cities = temp.Cities;
                //viewModel.RoomMainHotelList = temp.RoomMainHotelList;
                viewModel.RoomTypeList = temp.RoomTypeList;
                //viewModel.OptionList = temp.OptionList;
                return View(viewModel);

            }
        }
        #endregion

        #region List
        [HttpGet]
        // [Route("Customer-List")]
        //[Activity(Description = "مشاهده لیست اساتید")]
        //[Mvc5Authorize(Auth.CanViewApplicantList)]
        public virtual async Task<ActionResult> List()
        {
            // var viewModel = await _tourService.GetPagedListAsync();
            var viewModel = await _mainHotelService.GetPagedListAsync(new MainHotelSearchRequest());
            return View(viewModel);
        }

        [AjaxOnly]
        [HttpPost]
        // [Route("Customer-List")]
        //[Mvc5Authorize(Auth.CanViewApplicantList)]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> ListAjax(MainHotelSearchRequest request)
        {
            var viewModel = await _mainHotelService.GetPagedListAsync(request);
            if (viewModel.MainHotels == null || !viewModel.MainHotels.Any()) return Content("no-more-info");
            return PartialView("_ListAjax", viewModel);
        }
        #endregion

        #region Edit
        [HttpGet]

        public virtual async Task<ActionResult> Edit(Guid? id)
        {

            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var viewModel = await _mainHotelService.GetEditViewAsync(id.Value);
            if (viewModel == null) return HttpNotFound();

            return View("Edit", viewModel);
        }

        [HttpPost]

        public virtual async Task<ActionResult> Edit(EditMainHotelViewModel viewModel)
        {
            // viewModel.ImageSource = this.Upload(viewModel.PicSrFile, "/Content/HotelPhotoes/");
            if (!ModelState.IsValid)
            {
                var model = await _mainHotelService.GetEditViewAsync(viewModel.Id);
                viewModel.Cities = model.Cities;
                viewModel.States = model.States;
                return View(viewModel);
            }
            else
            {
                var newItem = await _mainHotelService.Edit(viewModel);
                this.NotyInformation("اطلاعات تور با موفقیت ویرایش شد");
                return RedirectToAction("List", "MainHotel");
            }
        }

        #endregion

        #region Delete
        [HttpPost]
        //[CheckReferrer]
        [ValidateAntiForgeryToken]
        //[Mvc5Authorize(Auth.CanDeleteApplicant)]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> Delete(Guid id)
        {
            var ans = await _mainHotelService.DeleteAsync(id);
            if (ans.Success)
                return Content("ok");
            return Json(new { success = false, msg = ans.Message });
        }
        #endregion
    }
}