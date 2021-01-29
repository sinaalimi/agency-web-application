using System.Threading.Tasks;
using System.Web.Mvc;
using CaptchaMvc.Attributes;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Agency.Common.Controller;
using Agency.Common.Extentions;
using Agency.Common.Filters;
using Agency.DataLayer.Context;
using Agency.ServiceLayer.Contracts.Users;
using Agency.ViewModel.Account;

namespace Agency.Web.Controllers
{
    public class AccountController : BaseController
    {

        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IApplicationSignInManager _signInManager;
        private readonly IApplicationUserManager _userManager;

        #endregion


        #region Constructor

        public AccountController(IApplicationUserManager userManager, IUnitOfWork unitOfWork,
            IApplicationSignInManager signInManager,
            IAuthenticationManager authenticationManager
           )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationManager = authenticationManager;
            _unitOfWork = unitOfWork;
        }

        #endregion


        #region Login,LogOff
        [AllowAnonymous]
        [HttpGet]
        public virtual ActionResult Login(string returnUrl)
        {
            if (_signInManager.AuthenticationManager.User.Identity.IsAuthenticated)
                return RedirectToAction("List", "User");
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (_userManager.CheckIsUserBanned(model.UserName))
            {
                this.AddErrors("UserName", "حساب کاربری شما مسدود شده است");
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync
                (model.UserName, model.Password, model.RememberMe, shouldLockout: true);

            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("List", "User");
                case SignInStatus.LockedOut:
                    this.AddErrors("UserName",
                        $"دقیقه دوباره امتحان کنید {_userManager.DefaultAccountLockoutTimeSpan} حساب شما قفل شد ! لطفا بعد از ");
                    return View(model);
                case SignInStatus.Failure:
                    this.AddErrors("UserName", "نام کاربری یا کلمه عبور  صحیح نمی باشد");
                    this.AddErrors("Password", "نام کاربری یا کلمه عبور  صحیح نمی باشد");
                    return View(model);
                default:
                    this.AddErrors("UserName",
                        "در این لحظه امکان ورود به  سابت وجود ندارد . مراتب را با مسئولان سایت در میان بگذارید"
                       );
                    return View(model);
            }
        }

        [HttpGet]
        [Mvc5Authorize]
        public virtual ActionResult LogOff()
        {
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login","Account");
        }

        #endregion

        #region Private
        [NonAction]
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index","Home");
        }

        #endregion
    }
}