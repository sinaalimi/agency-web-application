using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Agency.DataLayer.Context;
using Agency.DomainClasses.Entities.Person;
using Agency.DomainClasses.Entities.TourOption;
using Agency.DomainClasses.Entities.Vehicle;
using Agency.ServiceLayer.Contracts.Hotel;
using Agency.ServiceLayer.Contracts.Report;
using Agency.ServiceLayer.Contracts.StateCity;
using Agency.ServiceLayer.Contracts.Tour;
using Agency.ServiceLayer.Contracts.Users;
using Agency.ViewModel.Hotel;
using Agency.ViewModel.Option;
using Agency.ViewModel.Report;
using Agency.ViewModel.Tour;
using Agency.ViewModel.Vehicle;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ListTourViewModel = Agency.ViewModel.Report.ListTourViewModel;

namespace Agency.ServiceLayer.EFService.Report
{
    public class ReportService : IReportService
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
        private readonly HttpContextBase _httpContextBase;
        private readonly MapperConfiguration _configuration;
        #endregion

        #region Ctor

        public ReportService(HttpContextBase httpContextBase, IUnitOfWork unitOfWork, IApplicationUserManager userManager,
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
            _mappingEngine = mappingEngine;
            _configuration = configuration;
            _httpContextBase = httpContextBase;
            _stateCityService = stateCityService;

        }
        #endregion

        #region List
        public async Task<ViewModel.Tour.ListTourViewModel> GetPagedListAsync(TourSearchRequest request)
        {
            var tour = _tours.AsNoTracking().Include(p => p.TourVehicles);
            var TourList = await tour.ProjectTo<ShowTourViewModel>(_configuration).ToListAsync();

            foreach (var item in TourList)
            {
                item.Vehicles = new List<SmallVehicleListViewModel>();
                item.Hotels = new List<SmallHotelListViewModel>();
                item.OptionsList = new List<OptionViewModel>();
                var VehicleList = _tourVehicles.Where(p => p.TourId == item.Id);
                var HotelList = _tourHotels.Where(p => p.TourId == item.Id);
                var OptionList =
                 await _tourOptions.Where(p => p.TourId == item.Id)
                      .ProjectTo<OptionViewModel>(_configuration)
                      .ToListAsync();

                foreach (var t in VehicleList)
                {
                    var Vehicle = await _vehicles.Where(p => p.Id == t.VehicleId)
                            .ProjectTo<SmallVehicleListViewModel>(_configuration)
                            .ToListAsync();
                    item.Vehicles.Add(Vehicle[0]);
                }


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

            return new ViewModel.Tour.ListTourViewModel()
            {
                SearchRequest = request,
                Tours = TourList



            };
        }
        #endregion




        public List<ToursReportViewModel> ReportTours(Guid id)
        {
            ToursListViewModel listViewModel = new ToursListViewModel();
            listViewModel.ToursReportList = new List<ToursReportViewModel>();
            var query3 = (from reserve in _reserves where reserve.TourId==id
                          join person in _persons on reserve.ParentId equals person.ParentId
                          join person1 in _persons on reserve.ParentId equals person1.Id
                          join tour in _tours on reserve.TourId equals tour.Id
                          join tourvehicle in _tourVehicles on reserve.TourVehicleId equals tourvehicle.Id
                          join vehicle in _vehicles on tourvehicle.VehicleId equals vehicle.Id
                          join hotel in _hotels on reserve.HotelId equals hotel.Id
                          join cost in _costs on person.Id equals cost.Id
                          join user in _users on reserve.UserId equals user.Id
                          select new
                          {

                              UserName = user.AgencyName,
                              TourName = tour.Title,
                              CoupleBed = reserve.CoupleBed,
                              Name = person.Name,
                              LastName = person.LastName,
                              BirthDate = person.BirthDay,
                              BirthPlace = person.BirthPlace,
                              mobile = person.Mobile,
                              tel = person.PhoneNumber,
                              NationalCode = person.NationalCode,
                              Idcode = person.IdentityCode,
                              Relation = person.Relation,
                              ParentName = person1.Name,
                              VehicleName = vehicle.Name,
                              HotelName = hotel.Name,
                              adult = tour.AdultPrice,
                              chilld = tour.ChildPrice,
                              range = person.AgeRange,
                              insurance = tour.IsurancePrice,
                              isparent = person.IsParent,
                              share = tour.AgencyShare,
                              adress = person.Address,
                              id = tour.Id,
                              totcost = cost.PersonCost,
                              personId = person.Id,
                              couplebed = tour.CoupleBedPrice,
                              vehicleid = tourvehicle.Id,
                              index = tourvehicle.Index,
                              iscancel = cost.IsCanceled,
                              
                          }

                ).ToList();



            var query = query3.GroupBy(x => x.personId).Select(y => y.First());


            var query2 = (from p in _persons
                          join seat in _seats on p.Id equals seat.PersonId
                          join seatformat in _seatFormats on seat.SeatId equals seatformat.Id

                          select new
                          {
                              SeatNumber = seatformat.SeatNumber,
                              row = seatformat.Row,
                              col = seatformat.Col,

                          }
                ).ToList();

            int i = 0;
            foreach (var item in query)
            {
                ToursReportViewModel viewmodel = new ToursReportViewModel();

                if (item.CoupleBed == true && item.isparent == true)
                    viewmodel.CoupleBedCost = item.couplebed;
                else viewmodel.CoupleBedCost = 0;
                viewmodel.HotelName = item.HotelName;
                viewmodel.VehicleName = item.VehicleName;
                viewmodel.Name = item.Name;
                viewmodel.LastName = item.LastName;
                viewmodel.Address = item.adress;
                if (item.range == AgeRange.Adult)
                    viewmodel.BaseCost = item.adult;
                else if (item.range == AgeRange.Child)
                    viewmodel.BaseCost = item.chilld;
                else
                {
                    viewmodel.BaseCost = 0;
                }

                viewmodel.NationalCode = item.NationalCode;
                viewmodel.BirthDate = item.BirthDate.ToString();
                viewmodel.UserName = item.UserName;
                viewmodel.Mobile = item.mobile;
                viewmodel.TourName = item.TourName;
                viewmodel.ParentName = item.ParentName;
                viewmodel.IdCode = item.Idcode;
                if (item.range == AgeRange.Adult)
                {
                    viewmodel.agencyshare = item.share;
                }
                else viewmodel.agencyshare = 0;
                viewmodel.BirthPlace = item.BirthPlace;
                viewmodel.Tel = item.tel;
                viewmodel.Relation = item.Relation;
                viewmodel.insurance = item.insurance;
                if (item.range != AgeRange.Baby && item.iscancel==false)
                {
                    viewmodel.SeatNo = query2[i].SeatNumber;
                    viewmodel.Row = query2[i].row;
                    viewmodel.Col = query2[i].col;
                    i++;
                }
                
                viewmodel.VehicleIndex = item.index;
                viewmodel.TourId = item.id;
                viewmodel.VehicleId = item.vehicleid;
                if (item.iscancel != true)
                {
                    listViewModel.ToursReportList.Add(viewmodel);
                }
            }
            int totcost = 0;
            int totagency = 0;
            foreach (var item in listViewModel.ToursReportList)
            {
                totcost += item.BaseCost + item.insurance + item.CoupleBedCost; //item.OptionCost;
                totagency += item.agencyshare;
            }
            if (listViewModel.ToursReportList.Count != 0)
            {
                listViewModel.ToursReportList[listViewModel.ToursReportList.Count - 1].TotalCost = totcost;
                listViewModel.ToursReportList[listViewModel.ToursReportList.Count - 1].TotalAgencyShare = totagency;
            }


            return listViewModel.ToursReportList;
        }

        public List<CancelledToursReportViewModel> CanceledReserves(Guid id)
        {
            CancelledToursListViewModel listViewModel = new CancelledToursListViewModel();
            listViewModel.CancelList = new List<CancelledToursReportViewModel>();
            var query3 = (from reserve in _reserves where reserve.TourId==id
                          join person in _persons on reserve.ParentId equals person.ParentId
                          join person1 in _persons on reserve.ParentId equals person1.Id
                          join tour in _tours on reserve.TourId equals tour.Id
                          join tourvehicle in _tourVehicles on reserve.TourVehicleId equals tourvehicle.Id
                          join vehicle in _vehicles on tourvehicle.VehicleId equals vehicle.Id
                          join hotel in _hotels on reserve.HotelId equals hotel.Id
                          join cost in _costs on person.Id equals cost.Id
                          join user in _users on reserve.UserId equals user.Id
                          select new
                          {

                              UserName = user.AgencyName,
                              TourName = tour.Title,
                              CoupleBed = reserve.CoupleBed,
                              Name = person.Name,
                              LastName = person.LastName,
                              BirthDate = person.BirthDay,
                              BirthPlace = person.BirthPlace,
                              mobile = person.Mobile,
                              tel = person.PhoneNumber,
                              NationalCode = person.NationalCode,
                              Idcode = person.IdentityCode,
                              Relation = person.Relation,
                              ParentName = person1.Name,
                              VehicleName = vehicle.Name,
                              HotelName = hotel.Name,
                              adult = tour.AdultPrice,
                              chilld = tour.ChildPrice,
                              range = person.AgeRange,
                              insurance = tour.IsurancePrice,
                              isparent = person.IsParent,
                              share = tour.AgencyShare,
                              adress = person.Address,
                              id = tour.Id,
                              totcost = cost.PersonCost,
                              personId = person.Id,
                              couplebed = tour.CoupleBedPrice,
                              vehicleid = tourvehicle.Id,
                              index = tourvehicle.Index,
                              reservecancel = reserve.IsCanseled,
                              costcancel = cost.IsCanceled,
                              penaltycost = cost.PersonPenaltyCost

                          }

                ).ToList();



            var query = query3.GroupBy(x => x.personId).Select(y => y.First());

            int i = 0;
            foreach (var item in query)
            {
                CancelledToursReportViewModel viewmodel = new CancelledToursReportViewModel();
                if ( item.costcancel == true)
                {
                    viewmodel.Name = item.Name;
                    viewmodel.LastName = item.LastName;
                    viewmodel.Address = item.adress;
                    viewmodel.NationalCode = item.NationalCode;
                    viewmodel.BirthDate = item.BirthDate.ToLongTimeString();
                    viewmodel.UserName = item.UserName;
                    viewmodel.Mobile = item.mobile;
                    viewmodel.TourName = item.TourName;
                    viewmodel.ParentName = item.ParentName;
                    viewmodel.IdCode = item.Idcode;
                    viewmodel.BirthPlace = item.BirthPlace;
                    viewmodel.Tel = item.tel;
                    viewmodel.Relation = item.Relation;
                    viewmodel.TourId = item.id;
                    viewmodel.PenaltyCost = item.penaltycost;
                    
                    listViewModel.CancelList.Add(viewmodel);
                }           

            }
            int totcost = 0;
            int totagency = 0;
            foreach (var item in listViewModel.CancelList)
            {
                totcost += item.PenaltyCost;

            }
            if (listViewModel.CancelList.Count != 0)
            {
                listViewModel.CancelList[listViewModel.CancelList.Count - 1].TotalCost = totcost;
                
            }

            return listViewModel.CancelList;
        }

        public List<PersonInsuranceListViewModel> Insurance(Guid id)
        {
            InsuranceListViewModel listViewModel = new InsuranceListViewModel();
            listViewModel.PersonInsuranceList = new List<PersonInsuranceListViewModel>();
            var query3 = (from reserve in _reserves where reserve.TourId==id
                         join person in _persons on reserve.ParentId equals person.ParentId
                         join person1 in _persons on reserve.ParentId equals person1.Id
                         join tour in _tours on reserve.TourId equals tour.Id
                         join user in _users on reserve.UserId equals user.Id
                          join cost in _costs on person.Id equals cost.Id

                          select new
                         {

                             UserName = user.AgencyName,
                             TourName = tour.Title,
                             CoupleBed = reserve.CoupleBed,
                             Name = person.Name,
                             LastName = person.LastName,
                             BirthDate = person.BirthDay,
                             BirthPlace = person.BirthPlace,
                             mobile = person.Mobile,
                             tel = person.PhoneNumber,
                             NationalCode = person.NationalCode,
                             Idcode = person.IdentityCode,
                             Relation = person.Relation,
                             ParentName = person1.Name,
                             isparent = person.IsParent,
                             adult = tour.AdultPrice,
                             chilld = tour.ChildPrice,
                             range = person.AgeRange,
                             insurance = tour.IsurancePrice,
                             id = tour.Id,
                             personId = person.Id,
                             iscancel = cost.IsCanceled,


                         }

                ).ToList();
            var query = query3.GroupBy(x => x.personId).Select(y => y.First());
            
            foreach (var item in query)
            {
                PersonInsuranceListViewModel viewmodel = new PersonInsuranceListViewModel();

                viewmodel.Name = item.Name;
                viewmodel.TourId = item.id;
                viewmodel.LastName = item.LastName;
                viewmodel.NationalCode = item.NationalCode;
                viewmodel.UserName = item.UserName;
                viewmodel.Mobile = item.mobile;
                viewmodel.TourName = item.TourName;
                viewmodel.ParentName = item.ParentName;
                viewmodel.IdCode = item.Idcode;
                viewmodel.Relation = item.Relation;
                viewmodel.InsuranceCost = item.insurance;
                if (item.iscancel != true)
                {
                    listViewModel.PersonInsuranceList.Add(viewmodel);
                }

            }

           
            return listViewModel.PersonInsuranceList;

        }

        public List<OfficePersonCostsViewModel> Costs(Guid id)
        {
            OfficePersonListViewModel listViewModel = new OfficePersonListViewModel();
            listViewModel.OfficePersonList = new List<OfficePersonCostsViewModel>();
            var query2 = (from reserve in _reserves where reserve.TourId==id
                          join person in _persons on reserve.ParentId equals person.ParentId
                          join person1 in _persons on reserve.ParentId equals person1.Id
                          join tour in _tours on reserve.TourId equals tour.Id
                          join user in _users on reserve.UserId equals user.Id
                          join cost in _costs on person.Id equals cost.Id
                          select new
                          {

                              UserName = user.AgencyName,
                              TourName = tour.Title,
                              CoupleBed = reserve.CoupleBed,
                              Name = person.Name,
                              LastName = person.LastName,
                              BirthDate = person.BirthDay,
                              BirthPlace = person.BirthPlace,
                              mobile = person.Mobile,
                              tel = person.PhoneNumber,
                              NationalCode = person.NationalCode,
                              Idcode = person.IdentityCode,
                              Relation = person.Relation,
                              ParentName = person1.Name,
                              isparent = person.IsParent,
                              adult = tour.AdultPrice,
                              chilld = tour.ChildPrice,
                              range = person.AgeRange,
                              insurance = tour.IsurancePrice,
                              agency = tour.AgencyShare,
                              id = tour.Id,
                              optioncost = cost.PersonCost,
                              personid = person.Id,
                              couplebed = tour.CoupleBedPrice,
                              iscancel = cost.IsCanceled,
                          }

                ).ToList();
            var query = query2.GroupBy(x => x.personid).Select(y => y.First());
            foreach (var item in query)
            {
                OfficePersonCostsViewModel viewmodel = new OfficePersonCostsViewModel();

                if (item.CoupleBed == true && item.isparent == true)
                    viewmodel.COupleBedCost = item.couplebed;
                else viewmodel.COupleBedCost = 0;
                viewmodel.Name = item.Name;
                viewmodel.LastName = item.LastName;
                if (item.range == AgeRange.Adult)
                    viewmodel.BaseCost = item.adult;
                else if (item.range == AgeRange.Child)
                    viewmodel.BaseCost = item.chilld;
                else
                {
                    viewmodel.BaseCost = 0;
                }

                viewmodel.NationalCode = item.NationalCode;
                viewmodel.BirthDate = item.BirthDate.ToString();
                viewmodel.UserName = item.UserName;
                viewmodel.Mobile = item.mobile;
                viewmodel.TourName = item.TourName;
                viewmodel.ParentName = item.ParentName;
                viewmodel.IdCode = item.Idcode;
                viewmodel.BirthPlace = item.BirthPlace;
                viewmodel.Tel = item.tel;
                if (item.range == AgeRange.Adult)
                {
                    viewmodel.agencyshare = item.agency;
                }
                else viewmodel.agencyshare = 0; viewmodel.Relation = item.Relation;
                viewmodel.Insurance = item.insurance;
                viewmodel.TourId = item.id;
                if (item.range == AgeRange.Adult)
                {
                    if (item.CoupleBed == true && item.isparent == true)
                        viewmodel.TotalCost = item.adult + item.insurance + item.couplebed; //+ item.optioncost;
                    else
                        viewmodel.TotalCost = item.adult + item.insurance;// + viewmodel.OptionCOst;
                }
                else if (item.range == AgeRange.Child)
                    viewmodel.TotalCost = item.chilld + item.insurance;// viewmodel.OptionCOst;
                else
                {
                    viewmodel.TotalCost = item.insurance;
                }
                if (item.iscancel != true)
                {
                    listViewModel.OfficePersonList.Add(viewmodel);
                }
            }
            int totcost = 0;
            int totagency = 0;
            foreach (var item in listViewModel.OfficePersonList)
            {
                totcost += item.BaseCost + item.Insurance + item.COupleBedCost + item.OptionCOst;
                totagency += item.agencyshare;
            }
            if (listViewModel.OfficePersonList.Count != 0)
            {
                listViewModel.OfficePersonList[listViewModel.OfficePersonList.Count - 1].SumCost = totcost;
                listViewModel.OfficePersonList[listViewModel.OfficePersonList.Count - 1].TotalAgencySHare = totagency;
            }
            return listViewModel.OfficePersonList;
        }

        public List<OfficePersonSeatViewModel> SeatsReport(Guid id)
        {
            SeatListViewModel listViewModel = new SeatListViewModel();
            listViewModel.SeatsList = new List<OfficePersonSeatViewModel>();
            var query3 = (from reserve in _reserves where reserve.TourId==id
                         join person in _persons on reserve.ParentId equals person.ParentId
                         join person1 in _persons on reserve.ParentId equals person1.Id
                         join tour in _tours on reserve.TourId equals tour.Id
                         join tourvehicle in _tourVehicles on reserve.TourVehicleId equals tourvehicle.Id
                         join vehicle in _vehicles on tourvehicle.VehicleId equals vehicle.Id
                          join cost in _costs on person.Id equals cost.Id
                          join user in _users on reserve.UserId equals user.Id
                         select new
                         {

                             UserName = user.AgencyName,
                             TourName = tour.Title,
                             Name = person.Name,
                             LastName = person.LastName,
                             Relation = person.Relation,
                             ParentName = person1.Name,
                             VehicleName = vehicle.Name,
                             id = tour.Id,
                             range = person.AgeRange,
                             personId = person.Id,
                             iscancel = cost.IsCanceled,
                             
                         }

                ).ToList();
            var query = query3.GroupBy(x => x.personId).Select(y => y.First());

            var query2 = (from p in _persons
                          join seat in _seats on p.Id equals seat.PersonId
                          join seatformat in _seatFormats on seat.SeatId equals seatformat.Id

                          select new
                          {
                              SeatNumber = seatformat.SeatNumber
                          }
                ).ToList();

            int i = 0;
            foreach (var item in query)
            {
                OfficePersonSeatViewModel viewmodel = new OfficePersonSeatViewModel();



                viewmodel.VehicleName = item.VehicleName;
                viewmodel.Name = item.Name;
                viewmodel.TourId = item.id;
                viewmodel.LastName = item.LastName;
                viewmodel.UserName = item.UserName;
                viewmodel.TourName = item.TourName;
                viewmodel.ParentName = item.ParentName;
                viewmodel.Relation = item.Relation;
                if (item.range != AgeRange.Baby && item.iscancel == false)
                {
                    viewmodel.SeatNo = query2[i].SeatNumber;
                    i++;
                }
                if (item.iscancel != true)
                {
                    listViewModel.SeatsList.Add(viewmodel);
                }
            }
            return listViewModel.SeatsList;
        }

        ///
        public ListTourViewModel GetPagedList(ReportSearchRequest request)
        {
            var tours = new ListTourViewModel();
            tours.Tours = new List<TourListforReportViewModel>();
            var query1 = (from tour in _tours
                          join reserve in _reserves on tour.Id equals reserve.TourId
                          join user in _users on reserve.UserId equals user.Id
                          select new
                          {
                              id = tour.Id,
                              tourname = tour.Title,
                              sourceid = tour.SourceCityId,
                              destinationid = tour.DestinationCityId,
                              startdate = tour.StartTime,
                              reservetime = reserve.ReserveTime,
                              userid = user.Id,
                          }
                ).ToList();

            var query = query1.GroupBy(x => x.id).Select(y => y.First());
            foreach (var item in query)
            {
                if (item.userid == request.UserId && item.startdate <= request.EndDate && item.startdate >= request.StartDate)
                {
                    var viewmodel = new TourListforReportViewModel();
                    viewmodel.Id = item.id;
                    viewmodel.TourName = item.tourname;
                    viewmodel.SourceCity = _cities.Find(item.sourceid).Name;
                    viewmodel.DestinationCity = _cities.Find(item.destinationid).Name;
                    viewmodel.SatrtDate = item.startdate;
                    tours.Tours.Add(viewmodel);
                }
            }
            foreach (var item in tours.Tours)
            {
                item.ReportItemStartDate = request.StartDate.Value;
                item.ReportItemEndDate = request.EndDate.Value;
            }
            return tours;

        }
        ////////////
        public ListTourViewModel GetPagedListAsync(Guid tourid, Guid userid)
        {

            var reserves = new ListTourViewModel();
            reserves.Reserves = new List<ReserveListforReportViewModel>();

            var query1 = (from reserve in _reserves
                          join person in _persons on reserve.ParentId equals person.ParentId
                          join person1 in _persons on reserve.ParentId equals person1.Id
                          join tour in _tours on reserve.TourId equals tour.Id
                          join user in _users on reserve.UserId equals user.Id
                          join cost in _costs on person.Id equals cost.Id

                          select new
                          {
                              userid = reserve.UserId,
                              UserName = user.AgencyName,
                              TourName = tour.Title,
                              tourid = tour.Id,
                              CoupleBed = reserve.CoupleBed,
                              Name = person.Name,
                              LastName = person.LastName,
                              BirthDate = person.BirthDay,
                              BirthPlace = person.BirthPlace,
                              mobile = person.Mobile,
                              tel = person.PhoneNumber,
                              NationalCode = person.NationalCode,
                              Idcode = person.IdentityCode,
                              Relation = person.Relation,
                              ParentName = person1.Name,
                              isparent = person.IsParent,
                              adult = tour.AdultPrice,
                              chilld = tour.ChildPrice,
                              range = person.AgeRange,
                              insurance = tour.IsurancePrice,
                              agency = tour.AgencyShare,
                              id = tour.Id,
                              reservetime = reserve.ReserveTime,
                              reserveid = reserve.Id,
                              personid = person.Id,
                              OfficeShare = tour.OfficeCost,
                              bedcost = tour.CoupleBedPrice,
                              totalcostfinal = cost.PersonCost,
                          }

               ).ToList();

            var query = query1.GroupBy(x => x.personid).Select(y => y.First());

            foreach (var item in query)
            {
                if (item.userid == userid && item.tourid == tourid)
                {
                    var viewmodel = new ReserveListforReportViewModel();
                    if (item.CoupleBed == true && item.isparent == true)
                        viewmodel.COupleBedCost = item.bedcost;
                    else viewmodel.COupleBedCost = 0;
                    viewmodel.Name = item.Name;
                    viewmodel.LastName = item.LastName;
                    if (item.range == AgeRange.Adult)
                        viewmodel.BaseCost = item.adult;
                    else if (item.range == AgeRange.Child)
                        viewmodel.BaseCost = item.chilld;
                    else
                    {
                        viewmodel.BaseCost = 0;
                    }
                    viewmodel.TourId = item.tourid;
                    viewmodel.UserId = item.userid;
                    viewmodel.NationalCode = item.NationalCode;
                    viewmodel.BirthDate = item.BirthDate.ToString();
                    viewmodel.UserName = item.UserName;
                    viewmodel.Mobile = item.mobile;
                    viewmodel.TourName = item.TourName;
                    viewmodel.OfficeShare = item.OfficeShare;
                    viewmodel.ParentName = item.ParentName;
                    viewmodel.IdCode = item.Idcode;
                    viewmodel.BirthPlace = item.BirthPlace;
                    viewmodel.Tel = item.tel;
                    viewmodel.agencyshare = item.agency;
                    viewmodel.Relation = item.Relation;
                    viewmodel.Insurance = item.insurance;
                    if (item.range == AgeRange.Adult)
                    {
                        if (item.CoupleBed == true && item.isparent == true)
                            viewmodel.TotalCost = item.adult + item.insurance + 60000;
                        else
                            viewmodel.TotalCost = item.adult + item.insurance;
                    }
                    else if (item.range == AgeRange.Child)
                        viewmodel.TotalCost = item.chilld + item.insurance;
                    else
                    {
                        viewmodel.TotalCost = item.insurance;
                    }
                    viewmodel.TourId = item.id;
                    if (item.range != AgeRange.Baby)
                        viewmodel.OptionCOst = item.totalcostfinal - viewmodel.BaseCost - viewmodel.Insurance - viewmodel.COupleBedCost;
                    else
                    {
                        viewmodel.OptionCOst = 0;
                    }
                    reserves.Reserves.Add(viewmodel);

                }
            }
            int totcost = 0;
            int totagency = 0;
            int totoffice = 0;
            foreach (var item in reserves.Reserves)
            {
                totcost += item.BaseCost + item.Insurance + item.COupleBedCost + item.OptionCOst;
                totagency += item.agencyshare;
                totagency += item.OfficeShare;
            }
            if (reserves.Reserves.Count != 0)
            {
                reserves.Reserves[reserves.Reserves.Count - 1].SumCost = totcost;
                reserves.Reserves[reserves.Reserves.Count - 1].TotalAgencySHare = totagency;
                reserves.Reserves[reserves.Reserves.Count - 1].TotalOfficeShare = totoffice;
            }


            var query2 = reserves.Reserves.AsQueryable();

            return reserves;
        }
    }
}
