using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Agency.Common.Controller;
using Agency.Common.Extentions;
using Agency.Common.Filters;
using Agency.DataLayer.Context;
using Agency.DomainClasses.Entities.Person;
using Agency.DomainClasses.Entities.TourHotel;
using Agency.DomainClasses.Entities.TourOption;
using Agency.DomainClasses.Entities.TourVehicle;
using Agency.DomainClasses.Entities.Vehicle;
using Agency.ServiceLayer.Contracts.Hotel;
using Agency.ServiceLayer.Contracts.StateCity;
using Agency.ServiceLayer.Contracts.Tour;
using Agency.ServiceLayer.Contracts.Users;
using Agency.ViewModel.Common;
using Agency.ViewModel.Hotel;
using Agency.ViewModel.Option;
using Agency.ViewModel.Reserve;
using Agency.ViewModel.Tour;
using Agency.ViewModel.Vehicle;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityFramework.Extensions;
using Microsoft.Owin.Security;
//using Microsoft.Web.Administration;

namespace Agency.ServiceLayer.EFService.Tour
{
    public  class TourService : ITourService
    {
        #region Fields

        private readonly IStateCityService _stateCityService;
        private readonly IMapper _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IHotelService _hotelService;
        private readonly IDbSet<DomainClasses.Entities.tour.Tour> _tours;
        private readonly IDbSet<DomainClasses.Entities.Reserve.Reserve> _reserves;
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
        private readonly IDbSet<DomainClasses.Entities.Cost.Cost> _costs;
        private readonly HttpContextBase _httpContextBase;
        private readonly MapperConfiguration _configuration;
        #endregion

        #region Ctor

        public TourService(HttpContextBase httpContextBase, IUnitOfWork unitOfWork, IApplicationUserManager userManager,
            IMapper mappingEngine, MapperConfiguration configuration, IStateCityService stateCityService,
            IHotelService hotelService)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _hotelService = hotelService;
            _tours = _unitOfWork.Set<DomainClasses.Entities.tour.Tour>();
            _reserves = _unitOfWork.Set<DomainClasses.Entities.Reserve.Reserve>();
            _hotels = _unitOfWork.Set<DomainClasses.Entities.Hotel.Hotel>();
            _tourHotels = _unitOfWork.Set<DomainClasses.Entities.TourHotel.TourHotel>();
            _tourVehicles = _unitOfWork.Set<DomainClasses.Entities.TourVehicle.TourVehicle>();
            _tourOptions = _unitOfWork.Set<DomainClasses.Entities.TourOption.TourOption>();
            _vehicles= _unitOfWork.Set<DomainClasses.Entities.Vehicle.Vehicle>();
            _cities = _unitOfWork.Set<DomainClasses.Entities.City.City>();
            _states = _unitOfWork.Set<DomainClasses.Entities.State.State>();
            _persons = _unitOfWork.Set<DomainClasses.Entities.Person.Person>();
            _seats = _unitOfWork.Set<DomainClasses.Entities.Vehicle.Seat>();
            _seatFormats = _unitOfWork.Set<DomainClasses.Entities.Vehicle.SeatFormat>();
            _costs = _unitOfWork.Set<DomainClasses.Entities.Cost.Cost>();
            _mappingEngine = mappingEngine;
            _configuration = configuration;
            _httpContextBase = httpContextBase;
            _stateCityService = stateCityService;

        }
        #endregion

        #region Create
        public void Create(CreateTourViewModel viewModel)
        {
            var tour = _mappingEngine.Map<DomainClasses.Entities.tour.Tour>(viewModel);
            tour.UserId = _userManager.GetCurrentUserId();
            _tours.Add(tour);
            foreach (var item in viewModel.HotelIds)
            {
                var x = new TourHotel() {HotelId = item, TourId = tour.Id};
                _tourHotels.Add(x);

            }
           if(viewModel.VehicleList.Count>0)
            foreach (var item in viewModel.VehicleList)
            {
                for (var i = 1; i <= item.Count; i++)
                {
                    var x = new TourVehicle() { VehicleId = item.VehicleId, TourId = tour.Id,Index = i};
                    _tourVehicles.Add(x);
                }
              
            }
            var counter = 0;
           
            foreach (var item in viewModel.OptionList)
            {
                if (counter <= 0) { counter++; continue;}
                var x = new TourOption() { TourId = tour.Id, Title = item.Title};
                _tourOptions.Add(x);
                counter++;
            }

            _unitOfWork.SaveAllChanges();
        }
        #endregion

        #region GetCreateViewModel

        public async Task<CreateTourViewModel> GetCreateViewModelAsync()
        {
            var viewModel = new CreateTourViewModel()
            {
                States = await _stateCityService.GetStatesAsync(),
                SourceCities =new List<SelectListItem>(),
                DesCities =new List<SelectListItem>(),
                HotelsSelectList = new List<SelectListItem>(),
                OptionList = new List<OptionViewModel>() { new OptionViewModel() {Title = "a"} },
                VehicleList = new List<VehicleItemViewModel>() { new VehicleItemViewModel() { VehicleListItems =
                await _vehicles.AsNoTracking().Select(p => new SelectListItem { Text = p.Name, Value = p.Id.ToString() }).ToListAsync() , Count = 1} }
            };
            return viewModel;
        }

        #endregion

        #region GetCreateViewModel

        public async Task<CreateTourViewModel> GetCreateViewModelAsync2(CreateTourViewModel viewmodel)
        {
            var viewModel = new CreateTourViewModel()
            {
                States = await _stateCityService.GetStatesAsync(),
                Cities = new List<SelectListItem>(),
                SourceCities = _stateCityService.GetCities(viewmodel.SourceStateId),
                DesCities = _stateCityService.GetCities(viewmodel.DestinationStateId),
                HotelsSelectList = _hotelService.GetHotelsOfCity(viewmodel.DestinationCityId),
            };
            foreach (var item in viewmodel.VehicleList)
            {
                item.VehicleListItems =
                    await
                        _vehicles.AsNoTracking()
                            .Select(p => new SelectListItem {Text = p.Name, Value = p.Id.ToString()})
                            .ToListAsync();
            }
            return viewModel;
        }

        #endregion

        #region Edit

        public async Task<ShowTourViewModel> Edit(EditTourViewModel viewModel)
        {
            viewModel.OptionList.RemoveAt(0);
            var tour = _tours.Find(viewModel.Id);
            _mappingEngine.Map(viewModel, tour);
            tour.DestinationCityId = viewModel.DestinationCityId;
            tour.DestinationStateId = viewModel.DestinationStateId;
            tour.SourceCityId = viewModel.SourceCityId;
            tour.SourceStateId = viewModel.SourceStateId;
            await _unitOfWork.SaveAllChangesAsync();
            var o = await _tours.AsNoTracking().ProjectTo<ShowTourViewModel>(_configuration)
                .FirstOrDefaultAsync(p => p.Id == viewModel.Id);
            o.vehicleList = viewModel.VehicleList;
            o.OptionsList = viewModel.OptionList;           

            #region TourHotel
            var tourhotel = await _tourHotels.Where(p => p.TourId == viewModel.Id).ToListAsync();

            var temp2 = tourhotel;

            for (int i = 0; i < temp2.Count; i++)
            {
                foreach (var item1 in viewModel.HotelIds)
                {
                    if (i >= 0 && temp2[i].HotelId == item1)
                    {
                        temp2.Remove(temp2[i]);
                        i--;
                        break;
                    }
                }
                if (i < 0)
                {
                    break;
                }
            }
            foreach (var item in temp2)
            {
                await _tourHotels.Where(p => p.HotelId == item.HotelId).DeleteAsync();
            }

            var temp3 = viewModel.HotelIds;
            var tourhotel1 = await _tourHotels.Where(p => p.TourId == viewModel.Id).ToListAsync();
            for (int i = 0; i < temp3.Count; i++)
            {
                foreach (var item1 in tourhotel1)
                {
                    if (i >= 0 && temp3[i] == item1.HotelId)
                    {
                        temp3.Remove(temp3[i]);
                        i--;
                    }
                }
                if (i < 0)
                {
                    break;
                }
            }
            foreach (var item in temp3)
            {
                _tourHotels.Add(new TourHotel() { HotelId = item, TourId = tour.Id });
            }
            #endregion

            #region TourOption
            var touroption = await _tourOptions.Where(p => p.TourId == viewModel.Id).ToListAsync();

            var TempOption = touroption;

            for (int i = 0; i < TempOption.Count; i++)
            {
                foreach (var item1 in viewModel.OptionList)
                {
                    if (i >= 0 && TempOption[i].Id == item1.Id)
                    {
                        TempOption.Remove(TempOption[i]);
                        i--;
                        break;
                    }
                }
                if (i < 0)
                {
                    break;
                }
            }
            foreach (var item in TempOption)
            {
                await _tourOptions.Where(p => p.Id == item.Id).DeleteAsync();

            }
            var Tempoption1 = viewModel.OptionList;
            var touroption1 = await _tourOptions.Where(p => p.TourId == viewModel.Id).ToListAsync();
            for (int i = 0; i < Tempoption1.Count; i++)
            {
                foreach (var item1 in touroption1)
                {
                    if (i >= 0 && Tempoption1[i].Id == item1.Id)
                    {
                        Tempoption1.Remove(Tempoption1[i]);
                        i--;
                    }

                }

                if (i < 0)
                {
                    break;
                }

            }
            foreach (var item in Tempoption1)
            {
                _tourOptions.Add(new TourOption() { Title = item.Title, TourId = tour.Id });
            }
            #endregion

            #region TourVehicle
            viewModel.VehicleList.RemoveAt(0);
            var tourvehicle = await _tourVehicles.Where(p => p.TourId == viewModel.Id).ToListAsync();
            var temp22 = tourvehicle;

            for (var i = 0; i < temp22.Count; i++)
            {
                foreach (var item1 in viewModel.VehicleList.Where(item => item.Index > 0))
                {
                    if (i >= 0 && temp22[i].Index == item1.Index && temp22[i].VehicleId==item1.VehicleId)
                    {
                        temp22.Remove(temp22[i]);
                        i--;
                        break;
                    }
                }
            }
            foreach (var item in temp22)
            {
                await _tourVehicles.Where(p => p.VehicleId == item.VehicleId && p.TourId==tour.Id && p.Index == item.Index).DeleteAsync();
            }

            var newvehicles = viewModel.VehicleList.Where(p => p.Index == 0);
            foreach (var item in newvehicles)
            {
                var indexlist = _tourVehicles.Local.Where(p => p.VehicleId == item.VehicleId 
                    && p.TourId==viewModel.Id).ToList();
                var index = !indexlist.Any() ? 0 : indexlist.Max(p => p.Index);
                for (var i = 0; i < item.Count; i++)
                {
                    _tourVehicles.Add(new TourVehicle() {Index = ++index, TourId = viewModel.Id, VehicleId = item.VehicleId});
                }
            }
            #endregion
            _unitOfWork.SaveAllChanges();
            return o;
        }

        #endregion

        #region GetEditViewAsync

        public async Task<EditTourViewModel> GetEditViewAsync(Guid id)
        {
            var model = await _tours.AsNoTracking()
                        .ProjectTo<EditTourViewModel>(_configuration)
                        .FirstOrDefaultAsync(p => p.Id == id);

            model.SourceStatesListItem = await _stateCityService.GetStatesAsync();
            model.SourceCitiesLitsItem = new List<SelectListItem>();
            model.DesStatesListItem = await _stateCityService.GetStatesAsync();
            model.DesCitiesListItem = new List<SelectListItem>();
            model.Hotels = new List<SelectListItem>();
            model.HotelIds=new List<Guid>();

            model.SourceCitiesLitsItem =  _stateCityService.GetCities(model.SourceStateId);
            model.DesCitiesListItem = _stateCityService.GetCities(model.DestinationStateId);
            model.SourceState = _states.Find(model.SourceStateId);
            model.SouCity = _cities.Find(model.SourceCityId);
            model.DesState = _states.Find(model.DestinationStateId);
            model.DesCity = _cities.Find(model.DestinationCityId);
            foreach (var item in model.SourceStatesListItem)
            {
                if (item.Text == model.SourceState.Name)
                    item.Selected = true;
            }
            foreach (var item in model.SourceCitiesLitsItem)
            {
                if (item.Text == model.SouCity.Name)
                    item.Selected = true;

            }

            foreach (var item in model.DesStatesListItem)
            {
                if (item.Text == model.DesState.Name)
                    item.Selected = true;
            }
            foreach (var item in model.DesCitiesListItem)
            {
                if (item.Text == model.DesCity.Name)
                    item.Selected = true;

            }

            var mashin = await _tourVehicles.Where(p => p.TourId == model.Id).Include(p=>p.Vehicle).ToListAsync();

            model.VehicleList=new List<VehicleItemViewModel>();
            foreach (var item in mashin)
            {
                var temp = new VehicleItemViewModel
                {
                    VehicleId = item.VehicleId,
                    VehicleName=item.Vehicle.Name +" ("+(item.Index)+")",
                    Index =item.Index,
                    IsEdit = true
                };
                var list =
                    await _vehicles.AsNoTracking()
                        .Select(p => new SelectListItem {Text = p.Name, Value = p.Id.ToString()})
                        .ToListAsync();
                foreach (var item2 in list.Where(item2 => item2.Value == temp.VehicleId.ToString()))
                {
                    item2.Selected = true;
                }
                temp.VehicleListItems = list;
                model.VehicleList.Add(temp);
            }
            model.VehicleList.Insert(0, new VehicleItemViewModel() {Count = 0,Index = 0,IsEdit = false,VehicleId = new Guid(),
                VehicleListItems = model.VehicleList[0].VehicleListItems,VehicleName = ""});
            #region options
            var options = await _tourOptions.Where(p => p.TourId == model.Id).ToListAsync();

            model.OptionList = new List<OptionViewModel>() {new OptionViewModel() {Title = "a"} };
            foreach (var item in options)
            {
                var temp = new OptionViewModel()
                {
                    Title = item.Title,
                    Id = item.Id,
                    TourId = item.TourId
                };
                model.OptionList.Add(temp);
            }
            #endregion

            var yechizi = await _tourHotels.Where(p => p.TourId == model.Id).Include(p => p.Hotel).ToListAsync();
       
            model.Hotels =await _hotels.AsNoTracking().Where(p => p.CityId == model.DestinationCityId).Select(p => new SelectListItem {Text = p.Name + " " + p.City.Name, Value = p.Id.ToString()}).ToListAsync();
            foreach (var item in model.Hotels)
            {
                if (yechizi.SingleOrDefault(p => p.HotelId.ToString() == item.Value) != null)
                {
                    model.HotelIds.Add(new Guid(item.Value));
                    item.Selected = true;
                }
            }
            model.VehicleList = model.VehicleList.OrderBy(p => p.VehicleId).ThenBy(p => p.Index).ToList();
            return model;
        }
        #endregion

        #region List
        public async Task<ListTourViewModel> GetPagedListAsync(TourSearchRequest request)
        {
            var tour = _tours.AsNoTracking().AsQueryable().OrderBy($"{request.CurrentSort} {request.SortDirection}");
            tour = request.IsCanceled ? tour.Where(p => p.IsCanceled).AsQueryable() : tour.Where(p => !p.IsCanceled);
            if (request.StartTime.HasValue && request.StartTime.ToString()!= "1/1/1900 12:00:00 AM")
            {
                tour = tour.Where(p => p.StartTime == request.StartTime.Value).AsQueryable();
            }
            if (request.Title.HasValue())
            {
                tour = tour.Where(p => p.Title.Contains(request.Title)).AsQueryable();
            }
            tour =  tour.Skip((request.PageIndex - 1)*10).Take(10).AsQueryable().AsNoTracking();

            var tourList = await tour.ProjectTo<ShowTourViewModel>(_configuration).ToListAsync();
            
            foreach (var item in tourList)
            {
                var tourvehicle = _tourVehicles.Include(p=>p.Vehicle).Where(p => p.TourId == item.Id);
                item.Vehicles = new List<SmallVehicleListViewModel>();
                foreach (var item2 in tourvehicle)
                {
                    item.Vehicles.Add(new SmallVehicleListViewModel() {Id = item2.VehicleId,Name = item2.Vehicle.Name+" ("+item2.Index+")"});
                }

                item.Hotels = new List<SmallHotelListViewModel>();
                item.OptionsList = new List<OptionViewModel>();
                var HotelList = _tourHotels.Where(p => p.TourId == item.Id);
                var OptionList =
                 await _tourOptions.Where(p => p.TourId == item.Id)
                      .ProjectTo<OptionViewModel>(_configuration)
                      .ToListAsync();

                foreach (var r in HotelList)
                {
                    var Hotel = await _hotels.Where(p => p.Id == r.HotelId)
                                .ProjectTo<SmallHotelListViewModel>(_configuration)
                                .ToListAsync();

                    item.Hotels.Add(Hotel[0]);
                }

                item.OptionsList = OptionList;

                item.SourceCity = _cities.Find(item.SourceCityId).Name;
                item.DesCity = _cities.Find(item.DestinationCityId).Name;
            }

            return new ListTourViewModel()
            {
                SearchRequest = request,
                Tours = tourList
            };
        }
        #endregion

        #region Delete
        public async Task<DeleteMessageViewModel> DeleteAsync(Guid id)
        {
            if (!await _tours.AnyAsync(p => p.Id == id))
                return new DeleteMessageViewModel { Success = false, Message = "رکورد مورد نظر یافت نشد" };
            await _tours.Where(a => a.Id == id).DeleteAsync();
            await _tourVehicles.Where(p => p.TourId == id).DeleteAsync();
            await _tourHotels.Where(p => p.TourId == id).DeleteAsync();

            return new DeleteMessageViewModel { Success = true };

        }
        #endregion

        #region Cancel
        public async Task<int> CancelAsync(Guid id)
        {
            _tours.Find(id).IsCanceled = true;
            return await _unitOfWork.SaveAllChangesAsync();
        }
        #endregion

        #region GetTour

        public async Task<ShowTourViewModel> GetTour(Guid tourId)
        {
            var tour = await _tours.SingleAsync(p => p.Id == tourId);
            var model=_mappingEngine.Map<ShowTourViewModel>(tour);
            return model;
        }
        

        #endregion

        #region GetTourDetails
        public async Task<TourDetailViewModel> GetTourDetails(Guid id)
        {
            var viewModel =await _tours.Where(p => p.Id == id).ProjectTo<TourDetailViewModel>(_configuration).FirstAsync();
            var vehicles = await _tourVehicles.Where(p => p.TourId == id).Include(p=>p.Vehicle).ToListAsync();
            var hotels = await _tourHotels.Where(p => p.TourId == id).Include(p=>p.Hotel).ToListAsync();
            var OptionList =
                await _tourOptions.Where(p => p.TourId == id).ProjectTo<OptionViewModel>(_configuration).ToListAsync();
           
            viewModel.Hotels=new List<string>();
            viewModel.Vehicles=new List<string>();
            viewModel.HotelDesciption=new List<string>();
            foreach (var item in hotels)
            {
                viewModel.Hotels.Add(item.Hotel.Name);
                viewModel.HotelDesciption.Add(item.Hotel.Description);
            }
            foreach (var item in vehicles)
            {
                viewModel.Vehicles.Add(item.Vehicle.Name);
            }
            viewModel.DesCity = _cities.Find(viewModel.DestinationCityId).Name;
            viewModel.DesState = _states.Find(viewModel.DestinationStateId).Name;
            viewModel.SourceCity = _cities.Find(viewModel.SourceCityId).Name;
            viewModel.SourceSate = _states.Find(viewModel.SourceStateId).Name;
            viewModel.OptionsList = OptionList;
            return viewModel;
        }
        #endregion

        #region GetSeatManageSchema
        public async Task<ReserveSeatViewModel> GetSeatManageSchema(SelectVehicleViewModel viewmodel)
        {
            var currentUser = _userManager.GetCurrentUser();
            var tourvehicleid = viewmodel.Id;
            var tourvehicle = _tourVehicles.Find(tourvehicleid);
            var vehicleid = tourvehicle.VehicleId;
            var vehiclename = _vehicles.Find(vehicleid).Name + "(" + tourvehicle.Index + ")";
            var format = await _seatFormats.Where(p => p.VehicleId == vehicleid).ProjectTo<SeatViewModel>(_configuration).ToListAsync();

            var reserveSeats = await _seats.Where(p => p.TourVehicleId == tourvehicleid).Include(p => p.Person).ToListAsync();
            foreach (var item in reserveSeats)
            {
                var s = format.FirstOrDefault(p => p.Id == item.SeatId);
                if (s != null && item.PersonId != null)
                {
                    s.Isempty = false;
                    s.SeatGender = item.SeatGender;
                    s.Name = item.Person.Name + " " + item.Person.LastName;
                    s.IsDeactive = item.IsDeactive;
                    if (currentUser.IsSystemAccount || item.UserId == currentUser.Id)
                        s.IsDetailVisible = true;

                }
                else
                {
                    s.Isempty = false;
                    s.IsDeactive = item.IsDeactive;
                }
            }
            return new ReserveSeatViewModel()
            {
                SeatList = format.OrderBy(p=>p.Row).ThenBy(p=>p.Col).ToList() ,
                TourVehicleId = tourvehicleid,
                VehicleName = vehiclename
            };
        }
        #endregion

        #region ChangeSeats

        public async Task<ReserveSeatViewModel> ChangeSeats(ReserveSeatViewModel model)
        {
            bool moveToDeactiveSeat = false;
            bool sameSeatNumber = false;
            var currentUser = _userManager.GetCurrentUser();
            var tourvehicle = await _tourVehicles.SingleAsync(p => p.Id == model.TourVehicleId);
            var format = await _seatFormats.Where(p => p.VehicleId == tourvehicle.VehicleId)
                .OrderBy(p=>p.SeatNumber).ProjectTo<SeatViewModel>(_configuration).ToListAsync();
            var reserveSeats = await _seats.Where(p => p.TourVehicleId == tourvehicle.Id).Include(p=>p.SeatFormat).Include(p => p.Person).ToListAsync();
            var seat1 = reserveSeats.SingleOrDefault(p => p.SeatFormat.SeatNumber == model.FromSeat);
            var seat2 = reserveSeats.SingleOrDefault(p => p.SeatFormat.SeatNumber == model.ToSeat);
            if (seat1 != null && seat2 != null)
            {
                if (seat1.IsDeactive || seat2.IsDeactive)
                {
                    moveToDeactiveSeat = true;
                }
                else if (seat1.Id == seat2.Id)
                {
                    sameSeatNumber = true;
                }
                else
                {
                    var t1 = _seats.Find(seat1.Id);
                    var t2 = _seats.Find(seat2.Id);
                    var temp = t1.SeatId;
                    t1.SeatId = t2.SeatId;
                    t2.SeatId = temp;
                    await _unitOfWork.SaveAllChangesAsync();
                }
            }
            else if(seat1!=null && seat2==null)
            {
                if (seat1.IsDeactive)
                {
                    moveToDeactiveSeat = true;
                }
                else
                {
                    var v = await _seatFormats.SingleOrDefaultAsync(p => p.VehicleId == tourvehicle.VehicleId &&
                                                                         p.SeatNumber == model.ToSeat);
                    seat1.SeatId = v.Id;
                    await _unitOfWork.SaveAllChangesAsync();
                }  
            }
            else
            {
                model.Notify = true;

            }


            foreach (var item in reserveSeats)
            {
                var s = format.FirstOrDefault(p => p.Id == item.SeatId);
                if (s != null && item.PersonId != null)
                {
                    s.Isempty = false;
                    s.SeatGender = item.SeatGender;
                    s.Name = item.Person.Name + " " + item.Person.LastName;
                    s.IsDeactive = item.IsDeactive;
                    if (currentUser.IsSystemAccount || item.UserId == currentUser.Id)
                        s.IsDetailVisible = true;
                }
                else
                {
                    s.Isempty = false;
                    s.IsDeactive = item.IsDeactive;
                }
            }


            return new ReserveSeatViewModel()
            {
                SeatList = format,
                TourVehicleId = tourvehicle.Id,
                Notify = model.Notify,
                MoveToDeactive = moveToDeactiveSeat,
                SameSeatNumbers = sameSeatNumber
            };
        }
        #endregion

        #region DeactiveSeat

        public async Task<bool> DeactiveSeat(Guid tourvehicleid,int seatnumber)
        {
          var vehicleid= _tourVehicles.Find(tourvehicleid).VehicleId;
            
          var seatid= _seatFormats.FirstOrDefault(p => p.VehicleId == vehicleid && p.SeatNumber==seatnumber).Id;
            var find = _seats.Any(p => p.IsDeactive == true && p.TourVehicleId == tourvehicleid && p.SeatId == seatid);
            if (find)
                return false;
            var seat=new Seat()
            {
                SeatId = seatid,
                UserId = _userManager.GetCurrentUserId(),
                TourVehicleId = tourvehicleid,
                IsDeactive = true
            };
            try
            {
                _seats.Add(seat);
                await _unitOfWork.SaveAllChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
           
            
        }
        #endregion

        #region activeSeat

        public async Task<bool> activeSeat(Guid tourvehicleid, int seatnumber)
        {
            var vehicleid = _tourVehicles.Find(tourvehicleid).VehicleId;

            var seatid = _seatFormats.FirstOrDefault(p => p.VehicleId == vehicleid && p.SeatNumber == seatnumber).Id;
            try
            {
                await _seats.DeleteAsync(p => p.IsDeactive && p.TourVehicleId == tourvehicleid && p.SeatId == seatid);
                await _unitOfWork.SaveAllChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
          

        }
        #endregion

        #region TourCount

        public int ActiveToursCount()
        {
            return _tours.Count(p => p.EndTime < DateTime.Now && !p.IsCanceled);
        }
        public int TotalToursCount()
        {
            return _tours.Count(p => !p.IsCanceled);
        }

        #endregion

        #region PersonCount

        public int PersonCount()
        {
            return _persons.Count();
        }
        #endregion


        #region TourRemainCount

        public async Task<int> RemainCount(Guid id)
        {
            var tour = await _tours.SingleAsync(p => p.Id == id);
            var capacity = tour.Capacity;
            var reservecount = _costs.Count(p => p.Reserve.TourId == id && !p.IsCanceled &&
                        p.Person.AgeRange!=AgeRange.Baby);
            return capacity - reservecount;
        }
        #endregion

    }
}
