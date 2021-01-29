using Agency.Common.Controller;
using Agency.Common.Extentions;
using Agency.DataLayer.Context;
using Agency.ServiceLayer.Contracts.Room;
using Agency.ViewModel.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Agency.Common.Filters;
using System.Net;
using Agency.Common.Json;

namespace Agency.Web.Controllers
{
    public class RoomController : BaseController
    {
        // GET: Rom
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoomService _roomService;
        //private readonly IApplicationUserManager _userManager;
        #endregion

        #region Ctor

        public RoomController(IUnitOfWork unitOfWork,
            IRoomService roomService)
        {
            _roomService = roomService;
            //_userManager = userManager;
            _unitOfWork = unitOfWork;


        }
        #endregion


        public ActionResult Index()
        {
            var x = "as.ks";
            return View();
        }

        #region Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateRoomViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {

                _roomService.Create(viewmodel);
                this.NotySuccess("اتاق با موفقیت ثبت شد");
                return RedirectToAction("Create", "Room");
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
            var viewModel = await _roomService.GetPagedListAsync(new RoomSearchRequest());
            //var viewModel = await _vehicleService.GetPagedListAsync();
            return View(viewModel);
        }

        [AjaxOnly]
        [HttpPost]
        //[Route("Customer-List")]
        //[Mvc5Authorize(Auth.CanViewApplicantList)]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> ListAjax(RoomSearchRequest request)
        {
            var viewModel = await _roomService.GetPagedListAsync(request);
            if (viewModel.Rooms == null || !viewModel.Rooms.Any()) return Content("no-more-info");
            return PartialView("_ListAjax", viewModel);
        }
        #endregion

        #region Edit
        //[Route("Edit-CustomerGroup-{id}")]
        //[Route("virayesh/slider-{id}/user-{index}")]
        [HttpGet]
        //[Mvc5Authorize(Auth.CanEditApplicant)]
        public virtual async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var viewModel = await _roomService.GetEditViewAsync(id.Value);
            if (viewModel == null) return HttpNotFound();

            return PartialView("_Edit", viewModel);
        }

        //[Route("Edit-CustomerGroup-{id}")]
        [HttpPost]
        // [CheckReferrer]
        [ValidateAntiForgeryToken]
        //[Mvc5Authorize(Auth.CanEditApplicant)]
        public virtual async Task<ActionResult> Edit(RoomEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data = new { success = false, View = this.RenderPartialViewToString("_Edit", viewModel) }
                };
            }

            var newItem = await _roomService.Edit(viewModel);
            return new JsonNetResult
            {
                Data = new
                {
                    success = true,
                    View = this.RenderPartialViewToString("_roomItem", newItem)
                }
            };

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
            var ans = await _roomService.DeleteAsync(id);
            if (ans.Success)
                return Content("ok");
            return Json(new { success = false, msg = ans.Message });
        }
        #endregion
    }
}