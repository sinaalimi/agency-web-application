using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Agency.DataLayer.Context;
using Agency.DomainClasses.Entities.PersonRoomHotel;
using Agency.ServiceLayer.Contracts.Hotel;
using Agency.ServiceLayer.Contracts.Reserve;
using Agency.ServiceLayer.Contracts.ReserveHotel;
using Agency.ServiceLayer.Contracts.StateCity;
using Agency.ServiceLayer.Contracts.Tour;
using Agency.ServiceLayer.Contracts.Users;
using Agency.ViewModel.Report;
using Agency.ViewModel.ReserveHotel;
using AutoMapper;

namespace Agency.ServiceLayer.EFService.ReserveHotel
{
    public class ReserveHotelService : IReserveHotelService
    {
        #region Fields

        private readonly ITourService _tourService;
        private readonly IStateCityService _stateCityService;
        private readonly IMapper _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IHotelService _hotelService;
        private readonly IDbSet<DomainClasses.Entities.tour.Tour> _tours;
        private readonly IDbSet<DomainClasses.Entities.Hotel.Hotel> _hotels;
        private readonly IDbSet<DomainClasses.Entities.TourHotel.TourHotel> _tourHotels;
        private readonly IDbSet<DomainClasses.Entities.Reserve.Reserve> _reserves;
        private readonly IDbSet<DomainClasses.Entities.TourVehicle.TourVehicle> _tourVehicles;
        private readonly IDbSet<DomainClasses.Entities.User.User> _users;
        private readonly IDbSet<DomainClasses.Entities.Vehicle.Vehicle> _vehicles;
        private readonly IDbSet<DomainClasses.Entities.Person.Person> _persons;
        private readonly IDbSet<DomainClasses.Entities.Vehicle.SeatFormat> _seatFormats;
        private readonly IDbSet<DomainClasses.Entities.Vehicle.Seat> _seats;
        private readonly IDbSet<DomainClasses.Entities.TourOption.TourOption> _tourOptions;
        private readonly IDbSet<DomainClasses.Entities.Cost.Cost> _costs;
        private readonly IDbSet<DomainClasses.Entities.City.City> _cities;
        private readonly IDbSet<DomainClasses.Entities.State.State> _states;
        private readonly IDbSet<DomainClasses.Entities.MainHotel.MainHotel> _mainhotels;
        private readonly IDbSet<DomainClasses.Entities.MainHotel.Room> _mainroom;
        private readonly IDbSet<DomainClasses.Entities.NewPerson.NewPerson> _newPersons;
        private readonly IDbSet<DomainClasses.Entities.PersonRoomHotel.PersonRoomHotel> _personRoomHotels;
        private readonly IDbSet<DomainClasses.Entities.RoomMainHotel.RoomMainHotel> _roommainhotel;
        private readonly IDbSet<DomainClasses.Entities.MainHotelOption.MainHotelOption> _mainhoteloption;
        private readonly HttpContextBase _httpContextBase;
        private readonly MapperConfiguration _configuration;
        #endregion

        #region Ctor

        public ReserveHotelService(HttpContextBase httpContextBase, IUnitOfWork unitOfWork, IApplicationUserManager userManager,
            IMapper mappingEngine, MapperConfiguration configuration,
            ITourService tourService, IStateCityService stateCityService, IHotelService hotelService)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _tourService = tourService;
            _stateCityService = stateCityService;
            _tours = _unitOfWork.Set<DomainClasses.Entities.tour.Tour>();
            _hotels = _unitOfWork.Set<DomainClasses.Entities.Hotel.Hotel>();
            _tourHotels = _unitOfWork.Set<DomainClasses.Entities.TourHotel.TourHotel>();
            _tourVehicles = _unitOfWork.Set<DomainClasses.Entities.TourVehicle.TourVehicle>();
            _tourOptions = _unitOfWork.Set<DomainClasses.Entities.TourOption.TourOption>();
            _vehicles = _unitOfWork.Set<DomainClasses.Entities.Vehicle.Vehicle>();
            _reserves = _unitOfWork.Set<DomainClasses.Entities.Reserve.Reserve>();
            _persons = _unitOfWork.Set<DomainClasses.Entities.Person.Person>();
            _users = _unitOfWork.Set<DomainClasses.Entities.User.User>();
            _seatFormats = _unitOfWork.Set<DomainClasses.Entities.Vehicle.SeatFormat>();
            _costs = _unitOfWork.Set<DomainClasses.Entities.Cost.Cost>();
            _seats = _unitOfWork.Set<DomainClasses.Entities.Vehicle.Seat>();
            _cities = _unitOfWork.Set<DomainClasses.Entities.City.City>();
            _states = _unitOfWork.Set<DomainClasses.Entities.State.State>();
            _mainhotels = _unitOfWork.Set<DomainClasses.Entities.MainHotel.MainHotel>();
            _mainroom = _unitOfWork.Set<DomainClasses.Entities.MainHotel.Room>();
            _newPersons = _unitOfWork.Set<DomainClasses.Entities.NewPerson.NewPerson>();
            _roommainhotel = _unitOfWork.Set<DomainClasses.Entities.RoomMainHotel.RoomMainHotel>();
            _personRoomHotels = _unitOfWork.Set<DomainClasses.Entities.PersonRoomHotel.PersonRoomHotel>();
            _mainhoteloption = _unitOfWork.Set<DomainClasses.Entities.MainHotelOption.MainHotelOption>();
            _mappingEngine = mappingEngine;
            _configuration = configuration;
            _httpContextBase = httpContextBase;
            _stateCityService = stateCityService;
            _hotelService = hotelService;
        }
        #endregion


        #region Create
        public void Create(CreatePersonViewModel viewModel)
        {
            var person = _mappingEngine.Map<DomainClasses.Entities.NewPerson.NewPerson>(viewModel);
            person.UserId = _userManager.GetCurrentUserId();
            _personRoomHotels.Add(new PersonRoomHotel()
            {
                RoomHotelId = viewModel.RoomHotelId,
                NewPersonId = viewModel.Id
            });
            _roommainhotel.Find(viewModel.RoomHotelId).RemainingCount--;
            _newPersons.Add(person);
            _unitOfWork.SaveAllChanges();
        }
        #endregion
        public List<SelectListItem> StateSelectListIetm()
        {
            //return _users.Include(p=>p.Reserves).Where(p=>)
            var selectlist = new List<SelectListItem>();
            var query1 = (from hotel in _mainhotels
                          join state in _states on hotel.StateId equals state.Id
                          select new
                          {
                              stateid = state.Id,
                              statename = state.Name
                          }
                ).ToList();
            var query = query1.GroupBy(x => x.stateid).Select(y => y.First());
            foreach (var VARIABLE in query)
            {
                selectlist.Add(new SelectListItem() { Text = VARIABLE.statename, Value = VARIABLE.stateid.ToString() });
            }

            return selectlist;
        }
        public List<SelectListItem> CitySelectListIetm(Guid stateid)
        {
            var selectlist = new List<SelectListItem>();
            selectlist = _stateCityService.GetCities(stateid);
            return selectlist;
        }
        public ListReserveHotelViewmodel GetPagedListHotelReserve(ReserveHotelSearchRequest request)
        {
            var mainhotels = new ListReserveHotelViewmodel();
            mainhotels.MainHotels = new List<MainHotelListforReserveViewModel>();
            var query = (from roomhotel in _roommainhotel
                          join hotel in _mainhotels on roomhotel.HotelId equals hotel.Id
                          join room in _mainroom on roomhotel.RoomId equals room.Id
                          select new
                          {
                              hotelid= roomhotel.HotelId,
                              roomid=roomhotel.RoomId,
                              hotelname=hotel.Name,
                              hoteladdress=hotel.Adress,
                              firstdate=roomhotel.FirstDate,
                              finish=roomhotel.LasTime,
                              remain=roomhotel.RemainingCount,
                              
                          }
                ).ToList();

            foreach (var item in query)
            {
                var time = request.StartDate?.AddDays(Convert.ToDouble(request.Night));
                if (item.firstdate <= request.StartDate && item.finish >= time&& item.remain>=0)
                {

                    var viewmodel = new MainHotelListforReserveViewModel();
                    viewmodel.HotelId = item.hotelid;
                    viewmodel.RoomId = item.roomid;
                    viewmodel.HotelName = item.hotelname;
                    viewmodel.HotelAddress = item.hoteladdress;
                    viewmodel.ListItemStartDate = request.StartDate.Value.ToString();
                    viewmodel.Night = request.Night.Value;
                    mainhotels.MainHotels.Add(viewmodel);
                }
            }
            mainhotels.SearchRequest=new ReserveHotelSearchRequest();
            mainhotels.SearchRequest.Cities=new List<SelectListItem>();
            return mainhotels;

        }

        public ListReserveHotelViewmodel GetPagedListRooms(Guid hotelid,string start,int night)
        {
            var mainrooms = new ListReserveHotelViewmodel();
            mainrooms.MainRooms=new List<RoomFeaturesViewModel>();

            var query = (from roomhotel in _roommainhotel
                join hotel in _mainhotels on roomhotel.HotelId equals hotel.Id
                join room in _mainroom on roomhotel.RoomId equals room.Id

                         select new
                         {
                             capacity= room.Capacity,
                             type=room.Type,
                             hotelid=roomhotel.HotelId,
                             price=roomhotel.Price,
                             firstdate=roomhotel.FirstDate,
                             finish=roomhotel.LasTime,
                             roomid=roomhotel.RoomId,
                             remain=roomhotel.RemainingCount,
                             roomhotelid=roomhotel.Id,
                         }
                ).ToList();
            foreach (var item in query)
            {
                //var time1 = request.StartDate.Value.AddDays(Convert.ToDouble(request.Night));
                DateTime t= DateTime.Parse(start);
                var time = t.AddDays(Convert.ToDouble(night));
                // var time = item.firstdate.AddDays(request.Night);
                if (item.firstdate <= t && item.finish >= time&& item.remain>=0)
                {
                    var options = _mainhoteloption.Where(p => p.HotelId == item.hotelid);
                    var viewmodel = new RoomFeaturesViewModel();
                    string optionnames = "";
                    bool b = false;
                    foreach (var item2 in options)
                    {
                        if (b == false)
                        {
                            optionnames += item2.Name;
                            b = true;
                        }
                        optionnames += "," + item2.Name;
                    }
                    viewmodel.Capacity = item.capacity;
                    viewmodel.Type = item.type;
                    viewmodel.Options = optionnames;
                    viewmodel.RoomId = item.roomid;
                    viewmodel.RoomHotelId = item.roomhotelid;
                    mainrooms.MainRooms.Add(viewmodel);


                }
            }
          
           

            return mainrooms;
        }
    }
}
