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
using Agency.ServiceLayer.Contracts.Slider;
using Agency.ServiceLayer.Contracts.Users;
using Agency.ViewModel.Slider;

namespace Agency.Web.Controllers
{
    [RoutePrefix("Slider")]
    [Route("{action}")]
    [Authorize]
    public class SliderController : BaseController
    {

        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISliderService _sliderService;
        private readonly IApplicationUserManager _userManager;
        #endregion

        #region Ctor

        public SliderController(IUnitOfWork unitOfWork, IApplicationUserManager userManager, ISliderService sliderService)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _sliderService = sliderService;
        }
        #endregion
        // GET: Slider
        public ActionResult Index()
        {
            return View("Index");
        }

        #region Create

        [HttpGet]
        public ActionResult CreateSlide()
        {
            AddSlideViewModel viewmodel = new AddSlideViewModel();
            viewmodel.Index = _sliderService.LastIndex();
            return View(viewmodel);
        }

        [HttpPost]
        // [CheckReferrer]
        [ValidateAntiForgeryToken]
        //[Mvc5Authorize(Auth.CanCreateApplicant)]
        [AllowUploadSpecialFilesOnly(".png,.jpg,.jpeg,.gif", justImage: true)]
        public virtual ActionResult CreateSlide(AddSlideViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateSlide", viewModel);

            }

            if (!_sliderService.IsIndexExist(viewModel.Index))
                this.AddErrors("Index", "این اندیس قبلا ثبت شده است");

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            viewModel.PicSrc = this.Upload(viewModel.PicSrFile, "/Content/SliderPhotoes/");
            _sliderService.Create(viewModel);
            this.NotyInformation("اسلاید با موفقیت ثبت شد.");
            return RedirectToAction("CreateSlide");
        }
        #endregion

        #region List
        [HttpGet]
        //[Activity(Description = "مشاهده لیست اساتید")]
        //[Mvc5Authorize(Auth.CanViewApplicantList)]
        public virtual async Task<ActionResult> List()
        {
            var viewModel = await _sliderService.GetPagedListAsync(new SliderSearchRequest());
            return View(viewModel);
        }

        [AjaxOnly]
        [HttpPost]
        //[Mvc5Authorize(Auth.CanViewApplicantList)]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> ListAjax(SliderSearchRequest request)
        {
            var viewModel = await _sliderService.GetPagedListAsync(request);
            if (viewModel.Slides == null || !viewModel.Slides.Any()) return Content("no-more-info");
            return PartialView("_ListAjax", viewModel);
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
            await _sliderService.DeleteAsync(id);
            return Content("ok");
        }
        #endregion


        #region Edit
        [Route("virayesh/slider-{Id}")]
        //[Route("virayesh/slider-{Id}/user-{index}")]
        [HttpGet]
        //[Mvc5Authorize(Auth.CanEditApplicant)]
        public virtual async Task<ActionResult> Edit(Guid? id,int? index)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var viewModel = await _sliderService.GetForEdit(id.Value);
            if (viewModel == null) return HttpNotFound();
            return View("_Edit",viewModel);
        }

        [Route("virayesh/slider-{Id}")]
        [HttpPost]
        // [CheckReferrer]
        [ValidateAntiForgeryToken]
        [AllowUploadSpecialFilesOnly(".png,.jpg,.jpeg,.gif", justImage: true)]
        //[Mvc5Authorize(Auth.CanEditApplicant)]
        public virtual ActionResult Edit(EditSlideViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("_Edit", viewModel);

            }

            if (!_sliderService.IsIndexValid(viewModel.Index))
                this.AddErrors("Index", "اندیس معتبر نیست");

            if (!ModelState.IsValid)
            {
                return View("_Edit", viewModel);
            }

            _sliderService.Edit(viewModel);

            //if(viewModel.PicSrFile!=null)
            //     viewModel.PicSrc = this.Upload(viewModel.PicSrFile, "/Content/SliderPhotoes/");

            this.NotyInformation("اسلاید با موفقیت ویرایش شد.");
            return RedirectToAction("List", "Slider");
        }
        #endregion
    }
}