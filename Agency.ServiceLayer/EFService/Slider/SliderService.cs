using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using System.Web;
using Agency.Common.Controller;
using Agency.Common.Extentions;
using Agency.DataLayer.Context;
using Agency.ServiceLayer.Contracts.Slider;
using Agency.ServiceLayer.Contracts.Users;
using Agency.ViewModel.Slider;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityFramework.Extensions;

namespace Agency.ServiceLayer.EFService.Slider
{
    public class SliderService : ISliderService
    {
        #region Fields

        private readonly IMapper _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IDbSet<DomainClasses.Entities.Slider.Slider> _sliders;
        private readonly HttpContextBase _httpContextBase;
        private readonly MapperConfiguration _configuration;
        #endregion

        #region Ctor

        public SliderService(HttpContextBase httpContextBase, IUnitOfWork unitOfWork, IApplicationUserManager userManager,
            IMapper mappingEngine, MapperConfiguration configuration)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _sliders = _unitOfWork.Set<DomainClasses.Entities.Slider.Slider>();
            _mappingEngine = mappingEngine;
            _configuration = configuration;
            _httpContextBase = httpContextBase;
        }
        #endregion

        #region Create
        public void Create(AddSlideViewModel viewModel)
        {
            var slide = _mappingEngine.Map<DomainClasses.Entities.Slider.Slider>(viewModel);
            if (slide.Link!=null && !slide.Link.ToLower().Contains("http://"))
            {
                slide.Link = slide.Link.Insert(0, "http://");
            }
            slide.UserId = _userManager.GetCurrentUserId();
            //FileManager.Upload(viewModel.PicSrFile, "/Content/SliderPhotoes/");
            _sliders.Add(slide);
            _unitOfWork.SaveAllChanges();

        }
        #endregion

        #region Edit

        public void Edit(EditSlideViewModel viewModel)
        {
            var slide = _sliders.Find(viewModel.Id);
            
            if (viewModel.Index != slide.Index)
            {
                var currentslideIndex = slide.Index;
                slide.Index = 0;
                _unitOfWork.SaveAllChanges();
                var temp = _sliders.SingleOrDefault(p => p.Index == viewModel.Index);
                if (temp != null)
                {
                    temp.Index = currentslideIndex;
                    _sliders.Attach(temp);
                    _unitOfWork.MarkAsChanged(temp);
                    _unitOfWork.SaveAllChanges();
                }
            }
            if (viewModel.Link != null && !viewModel.Link.ToLower().Contains("http://"))
            {
                viewModel.Link = viewModel.Link.Insert(0, "http://");
            }
            //_mappingEngine.Map(viewModel, slide);
            slide.Title = viewModel.Title;
            slide.Describ = viewModel.Describ;
            slide.Link = viewModel.Link;
            slide.ButtonTitle = viewModel.ButtonTitle;
            if (viewModel.PicSrFile!=null)
            {
                FileManager.Delete("~/Content/SliderPhotoes/" + slide.PicSrc);
                slide.PicSrc = FileManager.Upload(viewModel.PicSrFile, "/Content/SliderPhotoes/");
            }
            slide.Index = viewModel.Index;
            _unitOfWork.SaveAllChanges();
        }

        public bool IsIndexValid(int index)
        {
            var max = _sliders.Max(p => p.Index);
            if (index <= max && index > 0)
                return true;
            return false;
        }

        #endregion
        public bool IsIndexExist(int index)
        {
            bool x = _sliders.Any(p => p.Index == index);
            if (x == true)
            {
                return false;
            }
            return true;
        }

        public int LastIndex()
        {
            int last = 0;
            if(_sliders.Any())
                last = _sliders.Max(p => p.Index);
            if (last == 0)
                return 1;
            return last + 1;
        }

        public async Task<SliderListViewModel> GetPagedListAsync(SliderSearchRequest request)
        {
            var slides = _sliders.AsNoTracking().AsQueryable();
            if (request.Title.HasValue())
            {
                slides = slides.Where(p => p.Title.Contains(request.Title)).AsQueryable();
            }

            if (request.Index > 0)
            {
                slides = slides.Where(p => p.Index==request.Index).AsQueryable();
            }


            slides = slides.OrderBy($"{request.CurrentSort} {request.SortDirection}");

            var query = await slides
                    .Skip((request.PageIndex - 1) * 10).Take(10).ProjectTo<SliderViewModel>(_configuration)
                    .ToListAsync();

            return new SliderListViewModel()
            {
                SearchRequest = request,
                Slides = query
            };
        }

        public SliderListViewModel GetPagedList(SliderSearchRequest request)
        {
            request.SortDirection = "Asc";
            var slides = _sliders.AsNoTracking().AsQueryable();
            if (request.Title.HasValue())
            {
                slides = slides.Where(p => p.Title.Contains(request.Title)).AsQueryable();
            }

            if (request.Index > 0)
            {
                slides = slides.Where(p => p.Index == request.Index).AsQueryable();
            }


            slides = slides.OrderBy($"{request.CurrentSort} {request.SortDirection}");

            var query = slides
                    .Skip((request.PageIndex - 1) * 10).Take(10).ProjectTo<SliderViewModel>(_configuration)
                    .ToList();

            return new SliderListViewModel()
            {
                SearchRequest = request,
                Slides = query
            };
        }


        #region Delete
        public async Task<int> DeleteAsync(Guid id)
        {
            var x=_sliders.SingleOrDefault(p=>p.Id==id);
            int previndex;
            if (x != null)
            {
                
                previndex = x.Index;
                var allslides = _sliders.Where(p => p.Index > previndex).ToList();
                var removeint=await _sliders.Where(a => a.Id == id).DeleteAsync();
                foreach (var item in allslides)
                {
                   var a= _sliders.Find(item.Id);
                    a.Index = item.Index - 1;
                }
                await _unitOfWork.SaveAllChangesAsync();
                FileManager.Delete("~/Content/SliderPhotoes/" + x.PicSrc);
                //File.Delete("/Content/SliderPhotoes/" + x.PicSrc);
                return removeint;
            }
            return -1;
        }
        #endregion

        #region GetForEdit
        public async Task<EditSlideViewModel> GetForEdit(Guid id)
        {
            var userWithRoles = await _sliders.AsNoTracking()
         .FirstOrDefaultAsync(a => a.Id == id);

            if (userWithRoles == null) return null;

            var slide = _mappingEngine.Map<EditSlideViewModel>(userWithRoles);

            return slide;

        }
        #endregion
    }
}
