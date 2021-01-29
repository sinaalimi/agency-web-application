using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using Agency.Common.Controller;
using Agency.Common.Extentions;
using Agency.Common.Filters;
using Agency.DataLayer.Context;
using Agency.ServiceLayer.Contracts.Vehicle;
using Agency.ViewModel.Vehicle;

namespace Agency.Web.Controllers
{
    [Authorize]
    public class VehicleController : BaseController
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVehicleService _vehicleService;
        #endregion

        #region Ctor

        public VehicleController(IUnitOfWork unitOfWork,
            IVehicleService vehicleService)
        {
            _unitOfWork = unitOfWork;
            _vehicleService = vehicleService;
        }
        #endregion

        #region Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateVehicleViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                var vehicle =await _vehicleService.Create(viewmodel);
                this.NotySuccess("وسیله نقلیه با موفقیت ثبت شد");
                return RedirectToAction("Format", "Vehicle",new {id=vehicle.Id});
            }
            else
            {
                return View(viewmodel);
            }
            
        }

        #endregion

        #region List
        [HttpGet]
        public async Task<ActionResult> List()
        {
            var viewModel = await _vehicleService.GetPagedListAsync(new VehicleSearchRequest());
            return View(viewModel);
        }

        [AjaxOnly]
        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> ListAjax(VehicleSearchRequest request)
        {
            var viewModel = await _vehicleService.GetPagedListAsync(request);
            if (viewModel.Vehicles == null || !viewModel.Vehicles.Any()) return Content("no-more-info");
            return PartialView("_ListAjax", viewModel);
        }
        #endregion

        #region Edit
        [HttpGet]
        public virtual async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var viewModel = await _vehicleService.GetEditViewAsync(id.Value);
            if (viewModel == null) return HttpNotFound();

            return View("Edit",viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Edit(VehicleEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            if (viewModel.PicSrFile != null)
            {
                viewModel.ImageSource = this.Upload(viewModel.PicSrFile, "/Content/VehiclePhoto/");
            }
            var newItem = await _vehicleService.Edit(viewModel);

            return RedirectToAction("List");
        }
        #endregion

        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> Delete(Guid id)
        {
            var ans = await _vehicleService.DeleteAsync(id);
            if (ans.Success)
                return Content("ok");
            return Json(new { success = false, msg = ans.Message });
        }
        #endregion

        #region Format

        [HttpGet]
        public ActionResult Format(Guid id)
        {
           var model=_vehicleService.CreateVehicleFormatView(id);
            if (!model.IsRemovable)
            {
                this.NotyAlert("به دلیل استفاده از چیدمان در رزرو امکان تغییر آن وجود ندارد");
                return RedirectToAction("List", "Vehicle");
            }
            return View(model);
        }
        #endregion

        public async Task<ActionResult> Format2(VehicleFormatViewModel model)
        {
            try
            {
                await _vehicleService.AddFormat(model);
                this.NotySuccess("اطلاعات با موفقیت ثبت شد");
            }
            catch (Exception e)
            {
                this.NotyAlert("از چیدمان این وسیله قبلا استفاده شده و امکان تغییر آن وجود ندارد");
            }
            return RedirectToAction("List", "Vehicle");
        }
    }
}