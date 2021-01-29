using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Agency.DataLayer.Context;
using Agency.DomainClasses.Entities.MainHotelOption;
using Agency.DomainClasses.Entities.RoomMainHotel;
using Agency.DomainClasses.Entities.TourHotel;
using Agency.DomainClasses.Entities.TourOption;
using Agency.DomainClasses.Entities.TourVehicle;
using Agency.ServiceLayer.Contracts.Hotel;
using Agency.ServiceLayer.Contracts.MainHotel;
using Agency.ServiceLayer.Contracts.StateCity;
using Agency.ServiceLayer.Contracts.Tour;
using Agency.ServiceLayer.Contracts.Users;
using Agency.Utilities;
using Agency.ViewModel.Common;
using Agency.ViewModel.Hotel;
using Agency.ViewModel.MainHotel;
using Agency.ViewModel.Option;
using Agency.ViewModel.Room;
using Agency.ViewModel.RoomMainHotel;
using Agency.ViewModel.Tour;
using Agency.ViewModel.Vehicle;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityFramework.Extensions;

namespace Agency.ServiceLayer.EFService.MainHotel
{
    public class MainHotelService : IMainHotelService
    {
        #region Fields
        private readonly IStateCityService _stateCityService;
        private readonly IMapper _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        //private readonly IMainHotelService _mainHotelService;
        private readonly IDbSet<DomainClasses.Entities.MainHotel.MainHotel>_mainHotels;
        private readonly IDbSet<DomainClasses.Entities.MainHotelOption.MainHotelOption> _mainHotelOptions;
        private readonly IDbSet<DomainClasses.Entities.RoomMainHotel.RoomMainHotel> _roomMainHotels;
        private readonly IDbSet<DomainClasses.Entities.MainHotel.Room> _rooms;
        private readonly IDbSet<DomainClasses.Entities.City.City> _cities;
        private readonly IDbSet<DomainClasses.Entities.State.State> _states;
        private readonly HttpContextBase _httpContextBase;
        private readonly MapperConfiguration _configuration;
        #endregion

        #region Ctor

        public MainHotelService(HttpContextBase httpContextBase, IUnitOfWork unitOfWork, IApplicationUserManager userManager,
            IMapper mappingEngine, MapperConfiguration configuration, IStateCityService stateCityService)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            //_mainHotelService = mainHotelService;
            _rooms = _unitOfWork.Set<DomainClasses.Entities.MainHotel.Room>();
            _mainHotels = _unitOfWork.Set<DomainClasses.Entities.MainHotel.MainHotel>();
            _mainHotelOptions = _unitOfWork.Set<DomainClasses.Entities.MainHotelOption.MainHotelOption>();
            _cities = _unitOfWork.Set<DomainClasses.Entities.City.City>();
            _states = _unitOfWork.Set<DomainClasses.Entities.State.State>();
            _roomMainHotels = _unitOfWork.Set<DomainClasses.Entities.RoomMainHotel.RoomMainHotel>();
            _mappingEngine = mappingEngine;
            _configuration = configuration;
            _httpContextBase = httpContextBase;
            _stateCityService = stateCityService;

        }
        #endregion

        #region Create
        public void Create(CreateMainHotelViewModel viewModel)
        {

            var mainhotel = _mappingEngine.Map<DomainClasses.Entities.MainHotel.MainHotel>(viewModel);
            mainhotel.UserId = _userManager.GetCurrentUserId();
            _mainHotels.Add(mainhotel);

            foreach (var item in viewModel.RoomMainHotelList)
            {
                var x = new RoomMainHotel() { RoomId = item.RoomId, HotelId = mainhotel.Id,Price = item.Price,RemainingCount = item.Count, Count = item.Count,FirstDate = item.FirstDate,LasTime = item.LasTime,Description = item.Description};
                _roomMainHotels.Add(x);
            }
            var counter = 0;

            //foreach (var item in viewModel.OptionsList)
            //{
            //    if (counter <= 0) { counter++; continue; }
            //    var x = new TourOption() { Cost = item.Cost, TourId = tour.Id, Title = item.Title };
            //    _tourOptions.Add(x);
            //    counter++;
            //}
            foreach (var item in viewModel.OptionList)
            {
                if (counter <= 0) { counter++; continue; }
                var x = new MainHotelOption() { Price = item.Price, HotelId = mainhotel.Id, Name = item.Name };
                _mainHotelOptions.Add(x);
                counter++;
            }

            _unitOfWork.SaveAllChanges();
        }
        #endregion

        #region GetCreateViewModel

        public async Task<CreateMainHotelViewModel> GetCreateViewModelAsync()
        {
            var viewModel = new CreateMainHotelViewModel()
            {
                States = await _stateCityService.GetStatesAsync(),
                Cities = new List<SelectListItem>(),
                OptionList = new List<HotelOptionViewModel>() { new HotelOptionViewModel() { Name = "a" } },
                RoomTypeList = await _rooms.AsNoTracking()
                           .Select(p => new SelectListItem { Text = p.Type, Value = p.Id.ToString() })
                           .ToListAsync() 
            };
            return viewModel;
        }



        #endregion

        #region GetCreateViewModel

        public async Task<CreateMainHotelViewModel> GetCreateViewModelAsync2(CreateMainHotelViewModel viewmodel)
        {
            var viewModel = new CreateMainHotelViewModel()
            {
                States = await _stateCityService.GetStatesAsync(),
                Cities = new List<SelectListItem>(),
                RoomTypeList = await _rooms.AsNoTracking()
                           .Select(p => new SelectListItem { Text = p.Type, Value = p.Id.ToString() })
                           .ToListAsync()

            };
            return viewModel;
        }

        #endregion

        #region Edit

        public async Task<ShowMainHotelViewModel> Edit(EditMainHotelViewModel viewModel)
        {
            viewModel.OptionList.RemoveAt(0);
            var mainhotel = _mainHotels.Find(viewModel.Id);
            mainhotel.UserId = _userManager.GetCurrentUserId();
         //   _mappingEngine.Map(viewModel, mainhotel);
            mainhotel.StateId = viewModel.StateId;
            mainhotel.Id = viewModel.Id;
            mainhotel.Adress = viewModel.Adress;
            mainhotel.CityId = viewModel.CityId;
            mainhotel.Name = viewModel.Name;
            mainhotel.Tel = viewModel.Tel;
            mainhotel.level = viewModel.level;
            await _unitOfWork.SaveAllChangesAsync();
            
       //     await _unitOfWork.SaveAllChangesAsync();
            var o = await _mainHotels.AsNoTracking().ProjectTo<ShowMainHotelViewModel>(_configuration)
                .FirstOrDefaultAsync(p => p.Id == viewModel.Id);
            o.OptionList = viewModel.OptionList;
            o.RoomMainHotelList = viewModel.RoomMainHotelList;
            // var vehicle= _tourVehicles.Where(p => p.TourId == viewModel.Id);
            //#region RoomMainHotel
            //var roomhotel = await _roomMainHotels.Where(p => p.HotelId == viewModel.Id).ToListAsync();

            //var temp2 = roomhotel;

            //for (int i = 0; i < temp2.Count; i++)
            //{
            //    foreach (var item1 in viewModel.RoomIds)
            //    {
            //        if (i >= 0 && temp2[i].RoomId == item1)
            //        {
            //            temp2.Remove(temp2[i]);
            //            i--;
            //            break;
            //        }
            //    }
            //    if (i < 0)
            //    {
            //        break;
            //    }
            //}
            //foreach (var item in temp2)
            //{
            //    await _roomMainHotels.Where(p => p.RoomId == item.RoomId).DeleteAsync();
            //}

            //var temp3 = viewModel.RoomIds;
            //var roomhotel1 = await _roomMainHotels.Where(p => p.HotelId == viewModel.Id).ToListAsync();
            //for (int i = 0; i < temp3.Count; i++)
            //{
            //    foreach (var item1 in roomhotel1)
            //    {
            //        if (i >= 0 && temp3[i] == item1.RoomId)
            //        {
            //            temp3.Remove(temp3[i]);
            //            i--;
            //        }
            //    }
            //    if (i < 0)
            //    {
            //        break;
            //    }
            //}
            //foreach (var item in temp3)
            //{
            //    _roomMainHotels.Add(new RoomMainHotel() { RoomId = item, HotelId = mainhotel.Id });
            //}
            //#endregion
            #region RoomHotel
            var roomhotel = await _roomMainHotels.Where(p => p.HotelId == viewModel.Id).ToListAsync();

            var temp = roomhotel;

            for (int i = 0; i < temp.Count; i++)
            {
                foreach (var item1 in viewModel.RoomMainHotelList)
                {
                    if (i >= 0 && temp[i].RoomId == item1.RoomId)
                    {

                        temp.Remove(temp[i]);
                        i--;
                        break;

                    }


                }
                if (i < 0)
                {
                    break;
                }

            }
            //var x = new Guid();
            //foreach (var item in temp)
            //{
            //    await _roomMainHotels.Where(p => p.RoomId == item.RoomId).DeleteAsync();

            //}
            //foreach (var item in viewModel.RoomMainHotelList)
            //{
            //    if (item.HotelId == Guid.NewGuid())
            //    {
            //        item.HotelId = mainhotel.Id; 
            //    }
            //    if (item.Id == Guid.NewGuid())
            //    {
            //        item.Id = SequentialGuidGenerator.NewSequentialGuid();
            //    }
            //}
            var temp1 = viewModel.RoomMainHotelList;
            var roomhotel1 = await _roomMainHotels.Where(p => p.HotelId == viewModel.Id).ToListAsync();
            for (int i = 0; i < temp1.Count; i++)
            {
                foreach (var item1 in roomhotel1)
                {
                    if (i >= 0 && temp1[i].RoomId == item1.RoomId)
                    {
                        temp1.Remove(temp1[i]);
                        i--;
                    }

                }

                //if (i < 0)
                //{
                //    i = 0;
                   
                //}

            }

            foreach (var item in temp1)
            {
                item.HotelId = mainhotel.Id;
                item.Id = SequentialGuidGenerator.NewSequentialGuid();
                _roomMainHotels.Add(new RoomMainHotel() { Count = item.Count, RoomId = item.RoomId, HotelId = item.HotelId , FirstDate = item.FirstDate , LasTime = item.LasTime , Description = item.Description , Price = item.Price , RemainingCount = item.Count , Id = item.Id});

            }
            #endregion
            #region MainHotelOptions
            var hoteloption = await _mainHotelOptions.Where(p => p.HotelId == viewModel.Id).ToListAsync();

            var TempOption = hoteloption;

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
                await _mainHotelOptions.Where(p => p.Id == item.Id).DeleteAsync();

            }
            var Tempoption1 = viewModel.OptionList;
            var hoteloption1 = await _mainHotelOptions.Where(p => p.HotelId == viewModel.Id).ToListAsync();
            for (int i = 0; i < Tempoption1.Count; i++)
            {
                foreach (var item1 in hoteloption1)
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
                _mainHotelOptions.Add(new MainHotelOption() {  Name = item.Name, Price = item.Price, HotelId = mainhotel.Id });

            }
            #endregion
            _unitOfWork.SaveAllChanges();
            //foreach (var item in tourvehicle)
            //{
            //    if()
            //}

            //VehicleItemViewModel x=new VehicleItemViewModel();
            //   x.Count = viewModel.VehicleList[0].Count;
            //x.VehicleId = viewModel.VehicleList[0].VehicleId;
            //_mappingEngine.Map(x, vehicle);





            return o;
        }

        #endregion

        #region List
        public async Task<ListMainHotelViewModel> GetPagedListAsync(MainHotelSearchRequest request)
        {
            var mainhotel = _mainHotels.AsNoTracking().Include(p => p.RoomMainHotels);
           // mainhotel = request.IsActive ? mainhotel.Where(p => !p.IsCanceled).AsQueryable() : mainhotel.Where(p => p.IsCanceled).AsQueryable();
            var MainHotelList = await mainhotel.ProjectTo<ShowMainHotelViewModel>(_configuration).ToListAsync();

            foreach (var item in MainHotelList)
            {
               // item.RoomTypeList = new List<SmallVehicleListViewModel>();
                item.Rooms = new List<SmallRoomListViewModel>();
                item.OptionList = new List<HotelOptionViewModel>();
                
                var RoomList = _roomMainHotels.Where(p => p.HotelId == item.Id);
                var OptionList =
                    await _mainHotelOptions.Where(p => p.HotelId == item.Id)
                        .ProjectTo < HotelOptionViewModel>(_configuration)
                      .ToListAsync();

                foreach (var t in RoomList)
                {
                    var Room = await _rooms.Where(p => p.Id == t.RoomId)
                            .ProjectTo<SmallRoomListViewModel>(_configuration)
                            .ToListAsync();
                    item.Rooms.Add(Room[0]);
                }



                item.OptionList = OptionList;


                item.CityName = _cities.Find(item.CityId).Name;
           //     item.StateName = _cities.Find(item.StateId).Name;

            }


            //var u = await mainhotel.ProjectTo<SmallRoomListViewModel>(_configuration).ToListAsync();


            //var hotel = _roomMainHotels.FirstOrDefault(p => p.HotelId ==)
            //if (request.City.HasValue())
            //{
            //    tour = tour.Where(p => p.City.Contains(request.City)).AsQueryable();
            //}
            //if (request.Name.HasValue())
            //{
            //    vehicle = vehicle.Where(p => p.Name.Contains(request.Name)).AsQueryable();
            //}


            //hotel = hotel.OrderBy($"{request.CurrentSort} {request.SortDirection}");
            //var x = await hotel.ProjectTo<ShowHotelViewModel>(_configuration).ToListAsync();

            //var query = await hotel
            //        .Skip((request.PageIndex - 1) * 10).Take(10).ProjectTo<ShowTourViewModel>(_configuration)
            //        .ToListAsync();


            return new ListMainHotelViewModel()
            {
                SearchRequest = request,
                MainHotels= MainHotelList



            };
        }
        #endregion

        #region GetEditViewAsync

        public async Task<EditMainHotelViewModel> GetEditViewAsync(Guid id)
        {
            var model = await _mainHotels.AsNoTracking()
                        .ProjectTo<EditMainHotelViewModel>(_configuration)
                        .FirstOrDefaultAsync(p => p.Id == id);

            model.States = await _stateCityService.GetStatesAsync();
            model.Cities = new List<SelectListItem>();
            model.RoomTypeList = new List<SelectListItem>();
            model.RoomIds = new List<Guid>();
            //   model.VehicleIds=new List<Guid>();
            //model.HotelsSelectList=new List<SelectListItem>();
            // model.Cities = await _stateCityService.GetCities(model.);



            model.Cities = _stateCityService.GetCities(model.StateId);
            //var states = _states.Find(model.City.StateId);
            model.State = _states.Find(model.StateId);
            model.City = _cities.Find(model.CityId);
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


            //model.vehicles =
            //    await
            //        _vehicles.AsNoTracking()
            //            .Select(p => new SelectListItem {Text = p.Name, Value = p.Id.ToString()})
            //            .ToListAsync();


            #region options
            var options = await _mainHotelOptions.Where(p => p.HotelId == model.Id).ToListAsync();

            model.OptionList = new List<HotelOptionViewModel>() { new HotelOptionViewModel() { Name = "a" } };
            foreach (var item in options)
            {
                var temp = new HotelOptionViewModel()
                {
                    Price = item.Price,
                    Name = item.Name,
                    Id = item.Id,
                    HotelId = item.HotelId
                };
                model.OptionList.Add(temp);
            }
            #endregion

            var otagh = await _roomMainHotels.Where(p => p.HotelId == model.Id).Include(p => p.Room).ToListAsync();

            model.RoomMainHotelList = new List<RoomItemViewModel>();
            foreach (var item in otagh)
            {
                var temp = new RoomItemViewModel()
                {
                    RoomId = item.RoomId,
                    Count = item.Count,
                    FirstDate = item.FirstDate,
                    LasTime = item.LasTime,
                    Price = item.Price,
                    Description = item.Description,
                    HotelId = item.HotelId,
                    Id = item.Id
                    
                };
                var list =
                    await _rooms.AsNoTracking()
                        .Select(p => new SelectListItem { Text = p.Type, Value = p.Id.ToString() })
                        .ToListAsync();
                foreach (var item2 in list.Where(item2 => item2.Value == temp.RoomId.ToString()))
                {
                    item2.Selected = true;
                }
                temp.RoomListItems = list;
                model.RoomMainHotelList.Add(temp);
            }


            //   model.SourceStateId = sourcestate.Id;


            //model.States = await _stateCityService.GetStatesAsync();
            //model.Cities = _stateCityService.GetCities(model.);
            //foreach (var item in model.States)
            //{
            //    if (item.Text == model.State.Name)
            //        item.Selected = true;
            //}
            //foreach (var item in model.Cities)
            //{
            //    if (item.Text == model.City.Name)
            //        item.Selected = true;

            //}

            return model;
        }
        #endregion

        #region Delete
        public async Task<DeleteMessageViewModel> DeleteAsync(Guid id)
        {
            if (!await _mainHotels.AnyAsync(p => p.Id == id))
                return new DeleteMessageViewModel { Success = false, Message = "رکورد مورد نظر یافت نشد" };
            await _mainHotels.Where(a => a.Id == id).DeleteAsync();
            await _roomMainHotels.Where(p => p.HotelId == id).DeleteAsync();

            return new DeleteMessageViewModel { Success = true };

        }
        #endregion
    }
}
