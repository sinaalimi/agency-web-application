using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using Agency.DataLayer.Context;
using Agency.ServiceLayer.Contracts.Users;
using Agency.Utilities;
using Agency.ViewModel.Home;
using Agency.ServiceLayer.Contracts.Tour;

namespace Agency.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly ITourService _tourService;

        #endregion

        #region Ctor

        public HomeController(IUnitOfWork unitOfWork, IApplicationUserManager userManager,
            ITourService tourService)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _tourService = tourService;
        }
        #endregion

        [Route("پنل-مدیریتی")]
        public ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult GetBenckMarks()
        {
            var user = _userManager.GetCurrentUser().IsSystemAccount;

            var viewModel = new BenchmarkViewModel
            {
                UsersCount = _userManager.Count().GetPersianNumber(),
                ActiveToursCount = _tourService.ActiveToursCount().GetPersianNumber(),
                TotalToursCount = _tourService.TotalToursCount().GetPersianNumber(),
                CustomerCount = _tourService.PersonCount().GetPersianNumber(),
                IsSystemAccount=user
            };
            return PartialView("_BenckMarks",viewModel);
        }
    }
}