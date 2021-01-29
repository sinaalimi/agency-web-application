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
using Agency.Common.Noty;
using Agency.DataLayer.Context;
using Agency.ServiceLayer.Contracts.Users;
using Agency.Utilities;
using Agency.ViewModel.User;

namespace Agency.Web.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        #region Fields

        private readonly IPermissionService _permissionService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IApplicationRoleManager _roleManager;

        #endregion

        #region Constructor

        public UserController(IUnitOfWork unitOfWork, IPermissionService permissionService, IApplicationRoleManager roleManager,
            IApplicationUserManager userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
            _permissionService = permissionService;
        }

        #endregion

        #region Create
        #region Ajax
        [HttpGet]
        [AjaxOnly]
        [Route("ایجاد-کاربر")]
        public virtual ActionResult Createajax()
        {
            return PartialView("_Createajax");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ایجاد-کاربر")]
        [AjaxOnly]
        public virtual async Task<ActionResult> Createajax(AddUserViewModel viewModel)
        {
            #region Validation
            if (_userManager.CheckUserNameExist(viewModel.UserName, null))
                this.AddErrors("UserName", "این نام کاربری قبلا در سیستم ثبت شده است");
            if (!viewModel.Password.IsSafePasword())
                this.AddErrors("Password", "این کلمه عبور به راحتی قابل تشخیص است");

            #endregion

            if (!ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data =
                        new
                        {
                            success = false,
                            View = this.RenderPartialViewToString("_Createajax", viewModel)
                        }
                };
            }
            var newUser =
            await _userManager.AddUser(viewModel);
            return new JsonNetResult
            {
                Data =
                    new
                    {
                        success = true,
                        View = this.RenderPartialViewToString("_UserItem", newUser)
                    }
            };
        }
        #endregion




        #endregion

        #region List
        [Route("مدیریت-کاربران")]
        public async Task<ActionResult> List()
        {
            var viewModel = await _userManager.GetPageList(
                new UserSearchRequest());
            viewModel.Roles = await _roleManager.GetAllAsSelectList();
            return View(viewModel);
        }

        [AjaxOnly]
        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        [Route("مدیریت-کاربران")]
        public async Task<ActionResult> ListAjax(UserSearchRequest search) 
        {
            var viewModel = await _userManager.GetPageList(search);
            if (viewModel.Users == null || !viewModel.Users.Any()) return Content("no-more-info");
            return PartialView("_ListAjax", viewModel);
        }

        #endregion

        #region Edit
        [HttpGet]
        [AjaxOnly]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var viewModel = await _userManager.GetForEditAsync(id.Value);
            if (viewModel == null) return HttpNotFound();

            return PartialView("_Edit",viewModel);
        }

        [HttpPost]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Edit(EditUserViewModel viewModel)
        {
            #region Validation
            if (_userManager.CheckUserNameExist(viewModel.UserName, viewModel.Id))
                this.AddErrors("UserName", "این نام کاربری قبلا در سیستم ثبت شده است");
            if (viewModel.Password.IsNotEmpty() && !viewModel.Password.IsSafePasword())
                this.AddErrors("Password", "این کلمه عبور به راحتی قابل تشخیص است");

            #endregion

            if (!ModelState.IsValid)
            {
                await _userManager.FillForEdit(viewModel);
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
            var dbUser = await _userManager.FindByIdAsync(viewModel.Id);
            if (dbUser == null) return HttpNotFound();
            var userViewModel = await _userManager.EditUser(viewModel);
            return new JsonNetResult
            {
                Data =
                    new
                    {
                        success = true,
                       View = this.RenderPartialViewToString("_UserItem", userViewModel)
                    }
            };
        }

        #endregion

        #region CancelEdit
        [HttpPost]
        [AjaxOnly]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public virtual async Task<ActionResult> CancelEdit(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var viewModel = await _userManager.GetUserViewModel(id.Value);
            if (viewModel == null) return HttpNotFound();
            return PartialView("_UserItem", viewModel);
        }
        #endregion

        #region Ban

        [HttpPost]
        [AjaxOnly]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public virtual async Task<ActionResult> BanUser(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (await _userManager.IsSystemUser(id.Value))
            {
                return Content("system");
            }
            var userViewModel = await _userManager.Ban(id.Value, true);
            return PartialView("_UserItem", userViewModel);

        }

        [HttpPost]
        [AjaxOnly]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public virtual async Task<ActionResult> EnableUser(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var userViewModel = await _userManager.Ban(id.Value, false);
            return PartialView("_UserItem", userViewModel);

        }
        #endregion
    }
}