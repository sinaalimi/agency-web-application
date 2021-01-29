using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Agency.Common.Controller;
using Agency.Common.Extentions;
using Agency.DataLayer.Context;
using Agency.DataLayer.Migrations;
using Agency.ServiceLayer.Contracts.Hotel;
using Agency.ServiceLayer.Contracts.StateCity;
using Agency.ServiceLayer.Contracts.Users;
using Agency.ViewModel.Common;
using Agency.ViewModel.Hotel;
using Agency.ViewModel.Option;
using Agency.ViewModel.Tour;
using Agency.ViewModel.Vehicle;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityFramework.Extensions;

namespace Agency.ServiceLayer.EFService.Hotel
{
     public class HotelService:IHotelService
    {
        #region Fields

        private readonly IStateCityService _stateCityService;
        private readonly IMapper _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IDbSet<DomainClasses.Entities.Hotel.Hotel> _hotels;
        private readonly IDbSet<DomainClasses.Entities.City.City> _cities;
        private readonly IDbSet<DomainClasses.Entities.State.State> _states;
        private readonly HttpContextBase _httpContextBase;
        private readonly MapperConfiguration _configuration;
        #endregion

        #region Ctor

        public HotelService(HttpContextBase httpContextBase, IUnitOfWork unitOfWork, IApplicationUserManager userManager,
            IMapper mappingEngine, MapperConfiguration configuration,IStateCityService stateCityService)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _hotels = _unitOfWork.Set<DomainClasses.Entities.Hotel.Hotel>();
            _cities = _unitOfWork.Set<DomainClasses.Entities.City.City>();
            _states = _unitOfWork.Set<DomainClasses.Entities.State.State>();
            _mappingEngine = mappingEngine;
            _configuration = configuration;
            _httpContextBase = httpContextBase;
            _stateCityService = stateCityService;
        }
        #endregion


        #region Create
        public void Create(CreateHotelViewModel viewModel)
        {
            var hotel = _mappingEngine.Map<DomainClasses.Entities.Hotel.Hotel>(viewModel);
            hotel.UserId = _userManager.GetCurrentUserId();
            _hotels.Add(hotel);
            _unitOfWork.SaveAllChanges();
        }
        #endregion

        #region List
        public async Task<HotelListViewModel> GetPagedListAsync(HotelSearchRequest request)
        {
            var userId = _userManager.GetCurrentUserId();
            var hotel = _hotels.AsNoTracking().Where(p=>p.UserId==userId)
                .Include(p => p.City).AsQueryable();

            if (request.StateId != new Guid())
            {
                hotel = hotel.Where(p => p.StateId == request.StateId).AsQueryable();
            }
            if (request.Degree.HasValue)
            {
                hotel = hotel.Where(p => p.Degree == request.Degree.Value).AsQueryable();
            }

            var x= await hotel.ProjectTo<ShowHotelViewModel>(_configuration).ToListAsync();

            foreach (var item in x)
            {
                var cities = _cities.Where(p => p.Id == item.CityId);
                foreach (var itemm in cities)
                {
                    var states = _states.Find(itemm.StateId);
                    item.State = states;
                }

            }

            return new HotelListViewModel()
            {
               SearchRequest = request,
                Hotels =x
            };
        }
        #endregion

        #region GetEditViewAsync

        public async Task<EditHotelViewModel> GetEditViewAsync(Guid id)
        {
            var model = await _hotels.AsNoTracking()
                        .ProjectTo<EditHotelViewModel>(_configuration)
                        .FirstOrDefaultAsync(p => p.Id == id);

            var states = _states.Find(model.City.StateId);
            model.State = states;
           

            model.States = await _stateCityService.GetStatesAsync();
            model.Cities=_stateCityService.GetCities(model.State.Id);
            foreach (var item in model.States)
            {
                if (item.Text == model.State.Name)
                    item.Selected = true;
            }
            foreach (var item in model.Cities)
            {
                if (item.Text == model.City.Name)
                    item.Selected = true;

            }
          
            return model;
        }
        #endregion

        #region Edit

        public async Task<ShowHotelViewModel> Edit(EditHotelViewModel viewModel)
        {

            var hotel = _hotels.Find(viewModel.Id);
            var image = hotel.ImageSource;
            hotel.Address = viewModel.Address;
            hotel.StateId = viewModel.StateId;
            hotel.CityId = viewModel.CityId;
            hotel.Degree = viewModel.Degree;
            if (viewModel.Description.HasValue())
                hotel.Description = viewModel.Description;
            if (viewModel.ImageSource.HasValue())
                hotel.ImageSource = viewModel.ImageSource;
            hotel.ManagerName = viewModel.ManagerName;
            hotel.UserId = _userManager.GetCurrentUserId();
            hotel.PhoneNumber = viewModel.PhoneNumber;
            hotel.Name = viewModel.Name;
            await _unitOfWork.SaveAllChangesAsync();
            if (image.HasValue())
            {
                FileManager.Delete("~/Content/HotelPhotoes/" + image);
            }
            
            var x= await _hotels.AsNoTracking().ProjectTo<ShowHotelViewModel>(_configuration)
                   .FirstOrDefaultAsync(p => p.Id == viewModel.Id);
            return x;
        }

        #endregion

        #region GetCreateViewModel

         public async Task<CreateHotelViewModel> GetCreateViewModelAsync()
         {
             var viewModel = new CreateHotelViewModel()
             {
                 States = await _stateCityService.GetStatesAsync(),
                 Cities  = new List<SelectListItem>()
             };
             return viewModel;
         }

        #endregion

        #region GetHotelDetails
        public async Task<HotelDetailViewModel> GetHotelDetails(Guid id)
        {
            var hotels = await _hotels.Where(p => p.Id == id).ProjectTo<HotelDetailViewModel>(_configuration).FirstAsync();
            hotels.CityName = _cities.Find(hotels.CityId).Name;
            hotels.StateName = _states.Find(hotels.StateId).Name;                
            return hotels;
        }
        #endregion

        #region Delete
        public async Task<DeleteMessageViewModel> DeleteAsync(Guid id)
        {
            if (!await _hotels.AnyAsync(p => p.Id == id))
                return new DeleteMessageViewModel { Success = false, Message = "رکورد مورد نظر یافت نشد" };
            var hotel = await _hotels.SingleAsync(p => p.Id == id);
            await _hotels.Where(a => a.Id == id).DeleteAsync();
            if(hotel.ImageSource.HasValue())
                FileManager.Delete("~/Content/HotelPhotoes/" + hotel.ImageSource);
            return new DeleteMessageViewModel { Success = true };

        }
        #endregion

         public List<SelectListItem> GetHotelsOfCity(Guid id)
         {
            var temp = _hotels.Where(p => p.CityId == id).ToList();
            return temp.Select(variable => new SelectListItem() { Text = variable.Name, Value = variable.Id.ToString() }).ToList();
        } 
    }
}
