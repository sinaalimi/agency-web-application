using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Agency.Common.Extentions;
using Agency.Common.Filters;
using Agency.DataLayer.Context;
using Agency.ServiceLayer.Contracts.Hotel;
using Agency.ServiceLayer.Contracts.StateCity;
using Agency.ViewModel.Hotel;
using Agency.ViewModel.StateCity;
using Agency.ViewModel.Vehicle;

namespace Agency.Web.Controllers
{
    [Authorize]
    public class StateCityController : Controller
    {
        // GET: StateCity

        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStateCityService _stateCityService;
        //private readonly IApplicationUserManager _userManager;
        #endregion

        #region Ctor

        public StateCityController(IUnitOfWork unitOfWork,
            IHotelService statecityService, IStateCityService stateCityService)
        {
            _unitOfWork = unitOfWork;
            //_userManager = userManager;
            _stateCityService = stateCityService;
            _stateCityService = stateCityService;
        }
        #endregion
        public ActionResult Index()
        {
            return View();
        }


        #region CreateState
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(CreateStateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                
                _stateCityService.Create(viewModel);
                this.NotySuccess(" استان با موفقیت ثبت شد");
                return RedirectToAction("Create", "StateCity");
            }
            else
            {
                return View(viewModel);
            }
        }
        #endregion

        #region CreateCity
        [HttpGet]
        public async Task<ActionResult> CreateCity()
        {
            var viewmodel = await _stateCityService.GetStatesAsync();
            var cityviewmodel = new CreateCityViewModel();
            cityviewmodel.States = viewmodel;
            return View(cityviewmodel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> CreateCity(CreateCityViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

                 _stateCityService.CreateCity(viewModel);
                this.NotySuccess(" شهر با موفقیت ثبت شد");
                return RedirectToAction("CreateCity", "StateCity");
            }
            else
            {
                return View(viewModel);
            }
        }
        #endregion

        #region ListState
        [HttpGet]
        public async Task<ActionResult> List()
        {
            var viewModel = await _stateCityService.GetPagedListAsync(new StateSearchRequest() );
            //var viewModel = await _vehicleService.GetPagedListAsync();
            return View(viewModel);
        }

        [AjaxOnly]
        [HttpPost]
        //[Route("Customer-List")]
        //[Mvc5Authorize(Auth.CanViewApplicantList)]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> ListAjax(StateSearchRequest request)
        {
            var viewModel = await _stateCityService.GetPagedListAsync(request);
            if (viewModel.states == null || !viewModel.states.Any()) return Content("no-more-info");
            return PartialView("_ListAjax", viewModel);
        }
        #endregion
        
        #region ListCity
        [HttpGet]
        public async Task<ActionResult> ListCity()
        {
            var viewModel = await _stateCityService.GetPagedListCityAsync(new CitySearchRequest() { States = await _stateCityService.GetStatesAsync() });
            //var viewModel = await _vehicleService.GetPagedListAsync();
            return View(viewModel);
        }

        [AjaxOnly]
        [HttpPost]
        //[Route("Customer-List")]
        //[Mvc5Authorize(Auth.CanViewApplicantList)]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> ListCityAjax(CitySearchRequest request)
        {
            var viewModel = await _stateCityService.GetPagedListCityAsync(request);
            if (viewModel.cities == null || !viewModel.cities.Any()) return Content("no-more-info");
            return PartialView("_ListCityAjax", viewModel);
        }
        #endregion


        #region EditState
        //   [Route("Edit-CustomerGroup-{id}")]
        //  [Route("virayesh/slider-{id}/user-{index}")]
        [HttpGet]
        //  [Mvc5Authorize(Auth.CanEditApplicant)]
        public virtual async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var viewModel = await _stateCityService.GetEditViewAsync(id.Value);
            if (viewModel == null) return HttpNotFound();

            return View("Edit", viewModel);
        }

        //[Route("Edit-CustomerGroup-{id}")]
        [HttpPost]
        //[CheckReferrer]
        [ValidateAntiForgeryToken]
        // [Mvc5Authorize(Auth.CanEditApplicant)]
        [ValidateInput(false)]
        public virtual async Task<ActionResult> Edit(EditStateViewModel viewModel)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(viewModel);

            //}

            //await _hotelService.Edit(viewModel);

            //this.NotyInformation("هتل با موفقیت ویرایش شد.");
            //return RedirectToAction("List", "Hotel");
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            else
            {
               
                var newItem = await _stateCityService.Edit(viewModel);
                this.NotyInformation("اطلاعات استان با موفقیت ویرایش شد");
                return RedirectToAction("List", "StateCity");

            }



        }
        #endregion

        #region EditCity
        //   [Route("Edit-CustomerGroup-{id}")]
        //  [Route("virayesh/slider-{id}/user-{index}")]
        [HttpGet]
        //  [Mvc5Authorize(Auth.CanEditApplicant)]
        public virtual async Task<ActionResult> EditCity(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var viewModel = await _stateCityService.GetEditCityViewAsync(id.Value);
            if (viewModel == null) return HttpNotFound();

            return View("EditCity", viewModel);
        }

        //[Route("Edit-CustomerGroup-{id}")]
        [HttpPost]
        //[CheckReferrer]
        [ValidateAntiForgeryToken]
        // [Mvc5Authorize(Auth.CanEditApplicant)]
        [ValidateInput(false)]
        public virtual async Task<ActionResult> EditCity(EditCityViewModel viewModel)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(viewModel);

            //}

            //await _hotelService.Edit(viewModel);

            //this.NotyInformation("هتل با موفقیت ویرایش شد.");
            //return RedirectToAction("List", "Hotel");
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            else
            {

                var newItem = await _stateCityService.EditCity(viewModel);
                this.NotyInformation("اطلاعات استان با موفقیت ویرایش شد");
                return RedirectToAction("ListCity", "StateCity");

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
            var ans = await _stateCityService.DeleteAsync(id);
            if (ans.Success)
                return Content("ok");
            return Json(new { success = false, msg = ans.Message });
        }
        #endregion
        #region DeleteCity
        [HttpPost]
        //[CheckReferrer]
        [ValidateAntiForgeryToken]
        //[Mvc5Authorize(Auth.CanDeleteApplicant)]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> DeleteCity(Guid id)
        {
            var ans = await _stateCityService.DeleteCityAsync(id);
            //if (ans.Success)
                return Content("ok");
            return Json(new { success = false, msg = ans.Message });
        }
        #endregion





    }
}