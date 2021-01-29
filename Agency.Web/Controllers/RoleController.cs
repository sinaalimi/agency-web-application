using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using MvcSiteMapProvider;
using Agency.Common.Controller;
using Agency.Common.Extentions;
using Agency.Common.Filters;
using Agency.Common.Json;
using Agency.DataLayer.Context;
using Agency.ServiceLayer.Contracts.Users;
using Agency.ViewModel.Role;

namespace Agency.Web.Controllers
{
    //[RoutePrefix("UserManagement/Role")]
    //[Route("{action}")]
    [Authorize]
    public class RoleController : BaseController
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationRoleManager _roleManager;

        #endregion

        #region Const

        public RoleController(IUnitOfWork unitOfWork, IApplicationRoleManager roleManager)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
        }

        #endregion

        #region ListAjax , List
        [Route("لیست-گروه-های-کاربری")]
        [HttpGet]
        public virtual async Task<ActionResult> ListRole()
        {
            var roles = await _roleManager.GetPageList(new RoleSearchRequest());
            return View("List",roles);
        }

        [Route("لیست-گروه-های-کاربری")]
        [AjaxOnly]
        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public virtual async Task<ActionResult> ListAjax(RoleSearchRequest request)
        {
            var viewModel = await _roleManager.GetPageList(request);
            if (viewModel.Roles == null || !viewModel.Roles.Any()) return Content("no-more-info");
            return PartialView("_ListAjax", viewModel);
        }
        #endregion

        #region Create
        [HttpGet]
        [AjaxOnly]
        [Route("Create-Role")]
        public virtual ActionResult Create()
        {
            return PartialView("_Create");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        [Route("Create-Role")]
        public virtual async Task<ActionResult> Create(AddRoleViewModel viewModel)
        {
            if (_roleManager.CheckForExisByName(viewModel.Name, null))
                this.AddErrors("Name", "این گروه  قبلا در سیستم ثبت شده است");

            if (!ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data =
                        new
                        {
                            success = false,
                            View = this.RenderPartialViewToString("_Create", viewModel)
                        }
                };
            }
            var newRole = await _roleManager.AddRole(viewModel);
            return new JsonNetResult
            {
                Data =
                    new
                    {
                        success = true,
                        View = this.RenderPartialViewToString("_RoleItem", newRole)
                    }
            };
        }

        #endregion

        #region Edit
        [HttpGet]
        [AjaxOnly]
        [Route("Edit-Role-{id}")]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var viewModel = await _roleManager.GetRoleByPermissionsAsync(id.Value);
            if (viewModel == null)
                return HttpNotFound();
            if (viewModel.IsSystemRole)
                return Content("system");
            _roleManager.FillForEdit(viewModel);
            return PartialView("_Edit", viewModel);
        }

        [HttpPost]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        [Route("Edit-Role-{id}")]
        public virtual async Task<ActionResult> Edit(EditRoleViewModel viewModel)
        {
            if (await _roleManager.CheckRoleIsSystemRoleAsync(viewModel.Id))
                return Content("system");

            if (_roleManager.CheckForExisByName(viewModel.Name, viewModel.Id))
                this.AddErrors("Name", "این گروه  قبلا در سیستم ثبت شده است");

            if (!ModelState.IsValid)
            {
                _roleManager.FillForEdit(viewModel);
                return new JsonNetResult
                {
                    Data =
                        new
                        {
                            success = false,
                            View = this.RenderPartialViewToString("_Edit", viewModel)
                        }
                };
            }

            if (!await _roleManager.IsInDb(viewModel.Id))
                return HttpNotFound();

            if (!await _roleManager.EditRole(viewModel))
            {
                this.AddErrors("Name", "برای گروه کاربری مورد نظر ، دسترسی تعیین کنید");
                _roleManager.FillForEdit(viewModel);
                return new JsonNetResult
                {
                    Data =
                        new
                        {
                            success = false,
                            View = this.RenderPartialViewToString("_Edit", viewModel)
                        }
                };
            }
            await _unitOfWork.SaveAllChangesAsync();

            var editedRole = await _roleManager.GetRole(viewModel.Id);
            return new JsonNetResult
            {
                Data =
                    new
                    {
                        success = true,
                        View = this.RenderPartialViewToString("_RoleItem", editedRole)
                    }
            };

        }

        #endregion

        #region CancelEdit
        [HttpPost]
        [AjaxOnly]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> CancelEdit(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var viewModel = await _roleManager.GetRole(id.Value);
            if (viewModel == null) return HttpNotFound();
            return PartialView("_RoleItem", viewModel);
        }
        #endregion

        #region Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (await _roleManager.CheckRoleIsSystemRoleAsync(id.Value))
            {
                return Content("system");
            }
            await _roleManager.RemoveById(id.Value);
            return Content("ok");
        }

        #endregion

        #region RemoteValidation

        [HttpPost]
        [AjaxOnly]
        [OutputCache(NoStore = true, Duration = 0)]
        public virtual JsonResult RoleNameExist(string name, Guid? id)
        {
            return _roleManager.CheckForExisByName(name, id) ? Json(false) : Json(true);
        }

        #endregion
    }
}