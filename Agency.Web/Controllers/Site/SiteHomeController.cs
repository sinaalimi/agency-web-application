using System;
using System.Web.Mvc;
using Agency.Common.Controller;
using Agency.DataLayer.Context;
using Agency.ServiceLayer.Contracts.Slider;
using Agency.ServiceLayer.Contracts.Users;
using Agency.ServiceLayer.Contracts.Website;
using Agency.ViewModel.Slider;

//using GemBox.Document;

namespace Agency.Web.Controllers.Site
{
    public class SiteHomeController : BaseController
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISliderService _sliderService;
        private readonly ISiteService _siteService;
        private readonly IApplicationUserManager _userManager;
        #endregion

        #region Ctor

        public SiteHomeController(IUnitOfWork unitOfWork, IApplicationUserManager userManager,
            ISliderService sliderService,ISiteService siteService)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _sliderService = sliderService;
            _siteService = siteService;
        }
        #endregion

        // GET: SiteHome
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Slider()
        {
            var slides = _sliderService.GetPagedList(new SliderSearchRequest());
            return PartialView("_Slider", slides);
        }

        public ActionResult GetTours()
        {
            var tours = _siteService.GetTours();
            return PartialView("_Tours", tours);
        }

        public ActionResult GetCounter()
        {
            var counter = _siteService.GetCounter();
            return PartialView("_Counter", counter);
        }

        public ActionResult GetTestimonials()
        {
            return PartialView("_Testimonials");
        }

        public ActionResult AboutUs()
        {
            return PartialView("_AboutUs");
        }

        public PartialViewResult Gallery()
        {
            var model = _siteService.GetGallery();
            return PartialView("_Gallery", model);
        }

        public ActionResult Discounts()
        {
            var model = _siteService.GetExpiredTours();
            return PartialView("_Discounts",model);
        }

        public ActionResult GetTour(Guid id)
        {
            var tour = _siteService.GetTour(id);
            return View(tour);
        }

        public ActionResult GetAllTours()
        {
            var tours = _siteService.GetAllTours();
            return View(tours);
        }
    }
}