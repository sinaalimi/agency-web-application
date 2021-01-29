using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Agency.DataLayer.Context;
using Agency.ServiceLayer.Contracts.Hotel;
using Agency.ServiceLayer.Contracts.StateCity;
using Agency.ServiceLayer.Contracts.Users;
using Agency.ServiceLayer.Contracts.Website;
using Agency.ViewModel.Home;
using Agency.ViewModel.Hotel;
using Agency.ViewModel.Tour;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Agency.ServiceLayer.EFService.Website
{
    public class SiteService:ISiteService
    {
        #region Fields

        private readonly IStateCityService _stateCityService;
        private readonly IMapper _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IHotelService _hotelService;
        private readonly IDbSet<DomainClasses.Entities.tour.Tour> _tours;
        private readonly IDbSet<DomainClasses.Entities.Hotel.Hotel> _hotels;
        private readonly IDbSet<DomainClasses.Entities.TourHotel.TourHotel> _tourHotels;
        private readonly IDbSet<DomainClasses.Entities.TourVehicle.TourVehicle> _tourVehicles;
        private readonly IDbSet<DomainClasses.Entities.TourOption.TourOption> _tourOptions;
        private readonly IDbSet<DomainClasses.Entities.Vehicle.Vehicle> _vehicles;
        private readonly IDbSet<DomainClasses.Entities.City.City> _cities;
        private readonly IDbSet<DomainClasses.Entities.Vehicle.Seat> _seats;
        private readonly IDbSet<DomainClasses.Entities.Person.Person> _persons;
        private readonly IDbSet<DomainClasses.Entities.State.State> _states;
        private readonly IDbSet<DomainClasses.Entities.Vehicle.SeatFormat> _seatFormats;
        private readonly HttpContextBase _httpContextBase;
        private readonly MapperConfiguration _configuration;
        #endregion

        #region Ctor

        public SiteService(HttpContextBase httpContextBase, IUnitOfWork unitOfWork, IApplicationUserManager userManager,
            IMapper mappingEngine, MapperConfiguration configuration, IStateCityService stateCityService,
            IHotelService hotelService)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _hotelService = hotelService;
            _tours = _unitOfWork.Set<DomainClasses.Entities.tour.Tour>();
            _hotels = _unitOfWork.Set<DomainClasses.Entities.Hotel.Hotel>();
            _tourHotels = _unitOfWork.Set<DomainClasses.Entities.TourHotel.TourHotel>();
            _tourVehicles = _unitOfWork.Set<DomainClasses.Entities.TourVehicle.TourVehicle>();
            _tourOptions = _unitOfWork.Set<DomainClasses.Entities.TourOption.TourOption>();
            _vehicles = _unitOfWork.Set<DomainClasses.Entities.Vehicle.Vehicle>();
            _cities = _unitOfWork.Set<DomainClasses.Entities.City.City>();
            _states = _unitOfWork.Set<DomainClasses.Entities.State.State>();
            _persons = _unitOfWork.Set<DomainClasses.Entities.Person.Person>();
            _seats = _unitOfWork.Set<DomainClasses.Entities.Vehicle.Seat>();
            _seatFormats = _unitOfWork.Set<DomainClasses.Entities.Vehicle.SeatFormat>();
            _mappingEngine = mappingEngine;
            _configuration = configuration;
            _httpContextBase = httpContextBase;
            _stateCityService = stateCityService;

        }
        #endregion

        public ListTourSummeryViewModel GetTours()
        {
            var tours = _tours.OrderByDescending(p => p.StartTime).Take(6).
                ProjectTo<TourSummeryViewModel>(_configuration).ToList();
            var cities = _cities.AsNoTracking().ToList();
            var states = _states.AsNoTracking().ToList();
            foreach (var item in tours)
            {
                item.SourceCity = cities.Single(p => p.Id == item.SourceCityId).Name;
                item.SourceState = states.Single(p => p.Id == item.SourceStateId).Name;
                item.DesCity = cities.Single(p => p.Id == item.DestinationCityId).Name;
                item.DesState = states.Single(p => p.Id == item.DestinationStateId).Name;
            }
            return new ListTourSummeryViewModel() {Tours = tours};
        }

        public CounterViewModel GetCounter()
        {
            var customers = _persons.Count();
            var hotels = _hotels.Count();
            var dests = _tours.AsNoTracking().Select(p => p.DestinationCityId).Distinct().Count();
            var tours = _tours.Count();
            return new CounterViewModel()
            {
                Customers = customers,
                Destinations = dests,
                DoneTours = tours,
                Hotels = hotels
            };
        }

        public TourSummeryViewModel GetTour(Guid id)
        {
            var cities = _cities.AsNoTracking().ToList();
            var states = _states.AsNoTracking().ToList();
            var tour =
                _tours.Where(p => p.Id == id).AsNoTracking().ProjectTo<TourSummeryViewModel>(_configuration).Single();
            tour.SourceCity = cities.Single(p => p.Id == tour.SourceCityId).Name;
            tour.SourceState = states.Single(p => p.Id == tour.SourceStateId).Name;
            tour.DesCity = cities.Single(p => p.Id == tour.DestinationCityId).Name;
            tour.DesState = states.Single(p => p.Id == tour.DestinationStateId).Name;
            return tour;
        }

        public ListTourSummeryViewModel GetAllTours()
        {
            var tours = _tours.OrderByDescending(p => p.StartTime).ProjectTo<TourSummeryViewModel>(_configuration).ToList();
            var cities = _cities.AsNoTracking().ToList();
            var states = _states.AsNoTracking().ToList();
            foreach (var item in tours)
            {
                item.SourceCity = cities.Single(p => p.Id == item.SourceCityId).Name;
                item.SourceState = states.Single(p => p.Id == item.SourceStateId).Name;
                item.DesCity = cities.Single(p => p.Id == item.DestinationCityId).Name;
                item.DesState = states.Single(p => p.Id == item.DestinationStateId).Name;
            }
            return new ListTourSummeryViewModel() { Tours = tours };
        }

        public ListTourSummeryViewModel GetExpiredTours()
        {
            var tours = _tours.Where(p=>p.EndTime.Year<=DateTime.Now.Year && p.EndTime.Month<=DateTime.Now.Month
                && p.EndTime.Day<=DateTime.Now.Day).ProjectTo<TourSummeryViewModel>(_configuration).ToList();
            var cities = _cities.AsNoTracking().ToList();
            var states = _states.AsNoTracking().ToList();
            foreach (var item in tours)
            {
                item.SourceCity = cities.Single(p => p.Id == item.SourceCityId).Name;
                item.SourceState = states.Single(p => p.Id == item.SourceStateId).Name;
                item.DesCity = cities.Single(p => p.Id == item.DestinationCityId).Name;
                item.DesState = states.Single(p => p.Id == item.DestinationStateId).Name;
            }
            return new ListTourSummeryViewModel() { Tours = tours };
        }

        public GalleryViewModel GetGallery()
        {
            var hotels = _hotels.Where(p=>p.ImageSource!=null)
                .AsNoTracking().ProjectTo<ShowHotelViewModel>(_configuration).ToList();
            var tours = _tours.Where(p => p.ImageSrc != null)
                .AsNoTracking()
                .ProjectTo<TourSummeryViewModel>(_configuration).ToList();
            return new GalleryViewModel() { Hotels = hotels,Tours = tours};
        } 
    }
}
