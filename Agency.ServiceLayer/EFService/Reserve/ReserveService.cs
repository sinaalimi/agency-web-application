using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Agency.Common.Extentions;
using Agency.DataLayer.Context;
using Agency.DomainClasses.Entities.Person;
using Agency.DomainClasses.Entities.Vehicle;
using Agency.ServiceLayer.Contracts.Reserve;
using Agency.ServiceLayer.Contracts.Users;
using Agency.Utilities;
using Agency.ViewModel.Common;
using Agency.ViewModel.Jarime;
using Agency.ViewModel.Person;
using Agency.ViewModel.Reserve;
using Agency.ViewModel.Vehicle;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityFramework.Extensions;
using Spire.Doc;

namespace Agency.ServiceLayer.EFService.Reserve
{
    public class ReserveService : IReserveService
    {
        #region Fields

        private readonly IMapper _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IDbSet<DomainClasses.Entities.tour.Tour> _tours;
        private readonly IDbSet<DomainClasses.Entities.Hotel.Hotel> _hotels;
        private readonly IDbSet<DomainClasses.Entities.TourHotel.TourHotel> _tourHotels;
        private readonly IDbSet<DomainClasses.Entities.TourVehicle.TourVehicle> _tourvehicles;
        private readonly IDbSet<DomainClasses.Entities.Vehicle.Vehicle> _vehicles;
        private readonly IDbSet<DomainClasses.Entities.Person.Person> _persons;
        private readonly IDbSet<DomainClasses.Entities.Reserve.Reserve> _reserves;
        private readonly IDbSet<DomainClasses.Entities.Vehicle.SeatFormat> _seatFormats;
        private readonly IDbSet<DomainClasses.Entities.Vehicle.Seat> _seats;
        private readonly IDbSet<DomainClasses.Entities.City.City> _city;
        private readonly IDbSet<DomainClasses.Entities.Cost.Cost> _costs;
        private readonly HttpContextBase _httpContextBase;
        private readonly MapperConfiguration _configuration;
        #endregion

        #region Ctor

        public ReserveService(HttpContextBase httpContextBase, IUnitOfWork unitOfWork, IApplicationUserManager userManager,
            IMapper mappingEngine, MapperConfiguration configuration)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _tours = _unitOfWork.Set<DomainClasses.Entities.tour.Tour>();
            _hotels = _unitOfWork.Set<DomainClasses.Entities.Hotel.Hotel>();
            _tourHotels = _unitOfWork.Set<DomainClasses.Entities.TourHotel.TourHotel>();
            _tourvehicles = _unitOfWork.Set<DomainClasses.Entities.TourVehicle.TourVehicle>();
            _vehicles = _unitOfWork.Set<DomainClasses.Entities.Vehicle.Vehicle>();
            _persons = _unitOfWork.Set<DomainClasses.Entities.Person.Person>();
            _reserves = _unitOfWork.Set<DomainClasses.Entities.Reserve.Reserve>();
            _seatFormats = _unitOfWork.Set<DomainClasses.Entities.Vehicle.SeatFormat>();
            _seats = _unitOfWork.Set<DomainClasses.Entities.Vehicle.Seat>();
            _city = _unitOfWork.Set<DomainClasses.Entities.City.City>();
            _costs = _unitOfWork.Set<DomainClasses.Entities.Cost.Cost>();
            _mappingEngine = mappingEngine;
            _configuration = configuration;
            _httpContextBase = httpContextBase;

        }
        #endregion

        #region GetHotels
        public async Task<List<SelectListItem>> GetHotels(Guid id)
        {
            var hotels =
                await
                    _tourHotels.Include(p => p.Hotel)
                        .Where(p => p.TourId == id).Select(p => new SelectListItem()
                        {
                            Text = p.Hotel.Name,
                            Value = p.HotelId.ToString()
                        }).ToListAsync();
            return hotels;
        }
        #endregion

        #region CreateReservePerson
        public async Task<Guid> CreateReservePerson(CreateReserveViewModel model)
        {
            var parent = _mappingEngine.Map<DomainClasses.Entities.Person.Person>(model.ParentViewModel);
            parent.IsParent = true;
            parent.UserId = _userManager.GetCurrentUserId();
            parent.ParentId = parent.Id;
            parent.AgeRange = AgeRange.Adult;
            parent.Gender = model.ParentViewModel.Gender;
            _persons.Add(parent);

            var reserve = _mappingEngine.Map<DomainClasses.Entities.Reserve.Reserve>(model);

            reserve.ParentId = parent.Id;
            reserve.UserId = _userManager.GetCurrentUserId();
            reserve.ReserveTime = DateTime.Now;
            var t = DateTime.Now;
            reserve.CodeRahgiri = t.Year.ToString() + t.Month + t.Day + t.Hour + t.Minute + t.Second;

            #region Tour

            var tour = _tours.Find(model.TourId);
            #endregion
            var totalcost = 0;
            var parentPrice = tour.AdultPrice + tour.IsurancePrice;
            if (model.CoupleBed) parentPrice += tour.CoupleBedPrice;
            _costs.Add(new DomainClasses.Entities.Cost.Cost(){
                PersonCost = parentPrice,Id = parent.Id,ReserveId = reserve.Id
            });
            totalcost += parentPrice;
            if (model.ListPersonViewModel != null)
            {
                foreach (var item in model.ListPersonViewModel)
                {
                    var child = _mappingEngine.Map<DomainClasses.Entities.Person.Person>(item);
                    child.ParentId = parent.Id;child.UserId = _userManager.GetCurrentUserId();
                    child.AgeRange = item.AgeRange;_persons.Add(child); var personPrice = 0;
                    if (child.AgeRange == AgeRange.Adult)
                        personPrice = tour.AdultPrice + tour.IsurancePrice;
                    else if (child.AgeRange == AgeRange.Child)
                        personPrice = tour.ChildPrice + tour.IsurancePrice;
                    else personPrice = tour.IsurancePrice;
                    totalcost += personPrice;
                    _costs.Add(new DomainClasses.Entities.Cost.Cost(){
                        PersonCost = personPrice,Id = child.Id,ReserveId = reserve.Id});
                }
                reserve.TotalCost = totalcost;
            }
            reserve.IsTemporary = true;
            _reserves.Add(reserve);
            await _unitOfWork.SaveAllChangesAsync();
            return reserve.Id;

        }
        #endregion

        #region GetVehicles
        public async Task<List<SelectListItem>> GetVehicles(Guid id)
        {
            var Vehicles = await
                _tourvehicles.Where(p => p.TourId == id).Include(p => p.Vehicle).OrderBy(p => p.VehicleId).ThenBy(p => p.Index).Select(p => new SelectListItem()
                {
                    Text = p.Vehicle.Name + " " + p.Index,
                    Value = p.Id.ToString()
                }).ToListAsync();
            return Vehicles;
        }
        #endregion

        #region GetPerson
        public async Task<ReserveSeatViewModel> GetPerson(Guid reserveId)
        {
            var currentUser = _userManager.GetCurrentUser();
            var reseve = _reserves.Find(reserveId);
            var personList = await _persons.Where(p => p.ParentId == reseve.ParentId && p.AgeRange!=AgeRange.Baby).Select(p => new SelectListItem()
            {
                Text = p.Name,
                Value = p.Id.ToString()
            }).ToListAsync();
            var tourvehicle = _tourvehicles.Find(reseve.TourVehicleId);
            var vehicle = await _vehicles.SingleAsync(p => p.Id == tourvehicle.VehicleId);
            var format = await _seatFormats.Where(p => p.VehicleId == tourvehicle.VehicleId).
                ProjectTo<SeatViewModel>(_configuration).OrderBy(p => p.SeatNumber).ToListAsync();

            var reserveSeats = await _seats.Where(p => p.TourVehicleId == reseve.TourVehicleId).Include(p => p.Person).ToListAsync();
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
                if (personList.Any(p => p.Value == item.PersonId.ToString()))
                {
                    s.IsHamGuruh = true;
                }
            }
            return new ReserveSeatViewModel()
            {
                PersonList = personList, SeatList = format.OrderBy(p => p.Row).ThenBy(p => p.Col).ToList(), ReserveId = reserveId,VehicleName = vehicle.Name + "("+tourvehicle.Index+")", Reservetime = reseve.ReserveTime
            };
        }
        #endregion

        #region SeatReserve
        public async Task<int> SeatReserve(Guid personid, Guid seatid, Guid reservid)
        {
            var person = _persons.Find(personid);
            var reserve = _reserves.Find(reservid);
            var tourvehicleid = _tourvehicles.Single(p => p.TourId == reserve.TourId && p.Id == reserve.TourVehicleId).Id;
            if (_seats.Any(p => p.SeatId == seatid && p.PersonId != null && p.TourVehicleId==tourvehicleid))
            {
                return 2;
            }
            if (_seats.Any(p => p.SeatId == seatid && p.PersonId == null && p.TourVehicleId==tourvehicleid))
            {
                return 3;
            }
            var person2 = await _seats.SingleOrDefaultAsync(p => p.PersonId == personid && p.TourVehicleId == tourvehicleid);
            if (person2 != null)
            {
                await _seats.Where(p => p.Id == person2.Id).DeleteAsync();
            }
            var seat = new Seat()
            {
                SeatGender = person.Gender,
                Isempty = false,
                TourVehicleId = tourvehicleid,
                SeatId = seatid,
                UserId = _userManager.GetCurrentUserId(),
                PersonId = personid,
                ReserveId = reservid
            };
            _seats.Add(seat);
            return await _unitOfWork.SaveAllChangesAsync();
        }
        #endregion

        #region List
        public async Task<ReserveListViewModel> GetPagedListAsync(ReserveSearchRequest request)
        {
            var currentUser = _userManager.GetCurrentUser();
            var Reserve = _reserves.AsNoTracking().Where(p => p.IsCanseled == request.IsCanceled && p.UserId==currentUser.Id).Include(p => p.Tour).Include(p => p.Hotel).Include(p => p.TourVehicle);

            if (request.Name.HasValue())
            {
                Reserve = Reserve.Where(p => p.Tour.Title.Contains(request.Name)).AsQueryable();
            }
            if (request.ReserveTime.HasValue && request.ReserveTime.ToString() != "1/1/1900 12:00:00 AM")
            {
                Reserve = Reserve.Where(p => p.ReserveTime.Year == request.ReserveTime.Value.Year &&
                                             p.ReserveTime.Month == request.ReserveTime.Value.Month &&
                                             p.ReserveTime.Day == request.ReserveTime.Value.Day).AsQueryable();
            }
            if (request.CodeRahgiri.HasValue())
            {

                Reserve = Reserve.Where(p => p.CodeRahgiri.Contains(request.CodeRahgiri)).AsQueryable();
            }

            Reserve = Reserve.OrderBy($"{request.CurrentSort} {request.SortDirection}");

            var query = await Reserve
                    .Skip((request.PageIndex - 1) * 10).Take(10).ProjectTo<ShowReserveViewModel>(_configuration)
                    .ToListAsync();

            foreach (var item in query)
            {
                var personList = _persons.Where(p => p.ParentId == item.ParentId);
                var personCount = _costs.Where(p => p.ReserveId == item.Id && p.IsCanceled != true);
                item.ParentName = _persons.Find(item.ParentId).Name;
                var Tour = _tours.Find(item.Tour.Id);
                item.PersonCount = personCount.Count();
                var Reserve1 = Reserve.FirstOrDefault(p => p.Id == item.Id);
                var f = Reserve1.TourVehicle.Vehicle;
                item.Vehicle = new ShowVehicleViewModel()
                {
                    Capacity = f.Capacity,
                    Name = f.Name + Reserve1.TourVehicle.Index,
                    Id = f.Id,
                };

                item.Source = _city.Find(Tour.SourceCityId).Name;
                item.Destination = _city.Find(Tour.DestinationCityId).Name;
            }

            return new ReserveListViewModel()
            {
                SearchRequest = request,
                Reserves = query
            };
        }
        #endregion

        #region Edit

        public async Task<EditReserveViewModel> GetEdit(Guid id)
        {
            var reserve = _reserves.Find(id);
            var personlist = _persons.Where(p => p.ParentId == reserve.ParentId).ProjectTo<EditPersonViewModel>(_configuration).ToList();
            var cancelperson = _costs.Where(p => p.ReserveId == id && p.IsCanceled == true);
            personlist = personlist.Where(p => p.ParentId != p.Id).ToList();
            var parent = _persons.Where(p => p.Id == reserve.ParentId).ProjectTo<EditParentViewModel>(_configuration).FirstOrDefault();
            parent.AgeRange = AgeRange.Adult;

            foreach (var item in cancelperson)
            {
                for (int i = 0; i < personlist.Count; i++)
                {
                    if (item.Id == personlist[i].Id)
                    {
                        personlist.Remove(personlist[i]);
                        break;
                    }
                }
            }
            var hotels =
            await
                _tourHotels.Include(p => p.Hotel)
                    .Where(p => p.TourId == reserve.TourId).Select(p => new SelectListItem()
                    {
                        Text = p.Hotel.Name,
                        Value = p.HotelId.ToString()
                    }).ToListAsync();
            if (hotels.Count > 0)
            {
                hotels.FirstOrDefault(p => p.Value == reserve.HotelId.ToString()).Selected = true;
            }

            var Vehicles = await
                _tourvehicles.Where(p => p.TourId == reserve.TourId).Include(p => p.Vehicle).Select(p => new SelectListItem()
                {
                    Text = p.Vehicle.Name + " " + p.Index,
                    Value = p.Id.ToString()
                }).ToListAsync();
            if (Vehicles.Count > 0)
            {
                Vehicles.FirstOrDefault(p => p.Value == reserve.TourVehicleId.ToString()).Selected = true;
            }

            personlist.Insert(0, new EditPersonViewModel()
            {
                AgeRange = AgeRange.Adult,
                BirthDay = DateTime.Now,
                BirthPlace = "as",
                Gender = true,
                IdentityCode = "53434",
                IsParent = true,
                LastName = "asdsdsad",
                Name = "Asdas",
                NationalCode = "1234567895",
                Relation = "asaslkd"
            });

            return new EditReserveViewModel()
            {
                CoupleBed = reserve.CoupleBed,
                TourId = reserve.TourId,
                TourVehicleId = reserve.TourVehicleId,
                HotelId = reserve.HotelId,
                ListEditPersonViewModel = personlist,
                ParentViewModel = parent,
                VehicleList = Vehicles,
                HotelList = hotels,
                Id = reserve.Id
            };
        }

        public async Task<ShowReserveViewModel> Edit(EditReserveViewModel viewModel)
        {

            viewModel.ListEditPersonViewModel.RemoveAt(0);
            var reserve = _reserves.Find(viewModel.Id);
            viewModel.UserId = _userManager.GetCurrentUserId();
            _mappingEngine.Map(viewModel, reserve);
            var parent = _persons.Find(reserve.ParentId);
            _mappingEngine.Map(viewModel.ParentViewModel, parent);
            parent.UserId = _userManager.GetCurrentUserId();
            var ListPerson = _persons.Where(p => p.ParentId == reserve.ParentId).ProjectTo<EditPersonViewModel>(_configuration).ToList();
            var cancelperson = _costs.Where(p => p.ReserveId == viewModel.Id && p.IsCanceled == true);
            var fff = ListPerson.Single(p => p.Id == reserve.ParentId);
            ListPerson.Remove(fff);
            foreach (var item in cancelperson)
            {
                for (int i = 0; i < ListPerson.Count; i++)
                {
                    if (item.Id == ListPerson[i].Id)
                    {
                        ListPerson.Remove(ListPerson[i]);
                        break;
                    }
                }
            }
            for (var i = 0; i < ListPerson.Count; i++)
            {
                foreach (var item1 in viewModel.ListEditPersonViewModel)
                {
                    if (i >= 0 && ListPerson[i].Id == item1.Id)
                    {
                        ListPerson.Remove(ListPerson[i]);
                        i--;
                        break;
                    }
                }
            }
            foreach (var item in ListPerson)
            {

                await _persons.Where(p => p.Id == item.Id).DeleteAsync();
            }
            if (viewModel.ListEditPersonViewModel != null && viewModel.ListEditPersonViewModel.Count > 0)
                foreach (var item in viewModel.ListEditPersonViewModel)
                {
                    var person = _persons.Find(item.Id);
                    item.UserId = _userManager.GetCurrentUserId();
                    person.ParentId = item.ParentId;
                    _mappingEngine.Map(item, person);
                }
            if (viewModel.ListPersonViewModel != null)
                foreach (var item in viewModel.ListPersonViewModel)
                {
                    item.ParentId = parent.Id;
                    item.UserId = _userManager.GetCurrentUserId();
                    var person = _mappingEngine.Map<DomainClasses.Entities.Person.Person>(item);
                    _persons.Add(person);
                }
            await _unitOfWork.SaveAllChangesAsync();
            return await _reserves.Where(p => p.Id == viewModel.Id).ProjectTo<ShowReserveViewModel>(_configuration).FirstAsync();
        }

        #endregion

        #region Delete
        public async Task<DeleteMessageViewModel> DeleteAsync(Guid id)
        {
            if (!await _persons.AnyAsync(p => p.Id == id))
                return new DeleteMessageViewModel { Success = false, Message = "رکورد مورد نظر یافت نشد" };
            await _persons.Where(a => a.Id == id).DeleteAsync();

            return new DeleteMessageViewModel { Success = true };

        }
        #endregion

        #region DeletePersonOfSeat
        public async Task<DeleteMessageViewModel> DeletePersonOfSeat(Guid id)
        {
            if (!await _persons.AnyAsync(p => p.Id == id))
                return new DeleteMessageViewModel { Success = false, Message = "رکورد مورد نظر یافت نشد" };
            await _persons.Where(a => a.Id == id).DeleteAsync();
            await _seats.Where(p => p.PersonId == id).DeleteAsync();

            return new DeleteMessageViewModel { Success = true };

        }
        #endregion

        #region TimeCalculateForOnePerson
        public JarimeViewModel TimeCalculate(Guid reserveId, Guid personId)
        {
            var g = new JarimeViewModel();
            var reserve = _reserves.Find(reserveId);
            var person = _persons.Find(personId);
            g.IsParent = person.Id == person.ParentId;
            var t = reserve.Tour.StartTime;
            var days = t - DateTime.Now;

            if (days.TotalDays >= 30)
            {
                g.Percent = 5;
            }
            else if (days.TotalDays <= 29 && days.TotalDays > 15)
            {
                g.Percent = 10;
            }
            else if (days.TotalDays <= 15 && days.TotalDays > 7)
            {
                g.Percent = 15;
            }
            else if (days.TotalDays <= 7 && days.TotalDays > 3)
            {
                g.Percent = 20;
            }
            else if (days.TotalDays <= 3 && days.TotalDays >= 1)
            {
                g.Percent = 30;
            }
            else if (days.TotalHours <= 24 && days.TotalHours > 12)
            {
                g.Percent = 40;
            }
            else if (days.TotalHours <= 12 && days.TotalHours > 0)
            {
                g.Percent = 50;
            }
            if (g.IsParent)
            {
                var parent = _persons.Find(reserve.ParentId);
                var cost = _costs.Single(p => p.Person.Id == personId);
                g.Price = cost.PersonCost;
                g.PenaltyPrice = (cost.PersonCost * g.Percent) / 100;
                g.FinalTotalPrice = cost.PersonCost - g.PenaltyPrice;
                g.PersonId = personId;
                g.ReserveId = reserveId;
                g.CodeRahgiri = reserve.CodeRahgiri;
                g.PersonName = parent.Name + " " + parent.LastName;
            }
            else
            {
                var cost = _costs.Single(p => p.Person.Id == personId);
                g.Price = cost.PersonCost;
                if (person.AgeRange == AgeRange.Baby)
                {
                    g.PenaltyPrice = cost.PersonCost;
                }
                else
                {
                    g.PenaltyPrice = (cost.PersonCost * g.Percent) / 100;
                }

                g.FinalTotalPrice = cost.PersonCost - g.PenaltyPrice;
                g.PersonId = personId;
                g.ReserveId = reserveId;
                g.CodeRahgiri = reserve.CodeRahgiri;
                g.PersonName = person.Name + " " + person.LastName;
            }
            return g;
        }
        #endregion

        #region CancelOnePerson
        public async Task<string> CancelAsync(Guid reserveId, Guid personId, int penalty)
        {
            var cost2 = _costs.Find(personId);
            if (cost2.IsCanceled) return cost2.CancelFilePath;
            var reserve = _reserves.Find(reserveId);
            var LastParent = _persons.Find(reserve.ParentId);
            var t = DateTime.Now;
            if (personId == _reserves.Find(reserveId).ParentId)
            {
                var persons = _costs.Where(p => p.ReserveId == reserveId && !p.IsCanceled).Include(p => p.Person);
                var maxDate = persons.Where(p => p.Id != personId).Min(p => p.Person.BirthDay);
                var maxperson = persons.FirstOrDefault(p => p.Person.Id!=LastParent.Id && p.Person.BirthDay == maxDate);
                foreach (var item in persons)
                {
                    if (item.Person.Id != personId)
                        item.Person.ParentId = maxperson.Id;
                }
                reserve.ParentId = maxperson.Id;
                var parentitem = persons.SingleOrDefault(p => p.Id == personId);
                parentitem.IsCanceled = true;
                parentitem.CancelDate = t;
                parentitem.PersonPenaltyCost = penalty;
                _reserves.Find(reserveId).TotalCost -= parentitem.PersonCost;
                parentitem.PersonCost = 0;
            }
            else
            {
                var cost = _costs.SingleAsync(p => p.ReserveId == reserveId && p.Id == personId).Result;
                cost.PersonPenaltyCost = penalty;
                cost.CancelDate = t;
                cost.IsCanceled = true;
                _reserves.Find(reserveId).TotalCost -= cost.PersonCost;
                cost.PersonCost = 0;

            }
            var person = _persons.Find(personId);
            Document document = new Document();
            document.LoadFromFile(Path.Combine(HttpContext.Current.Server.MapPath("/Content/Files"), "cancelreserve.docx"));
            document.Replace("name", LastParent.Name + " " + LastParent.LastName, false, true);
            document.Replace("code", reserve.CodeRahgiri.GetPersianNumber(), false, true);
            document.Replace("melli", LastParent.NationalCode.GetPersianNumber(), false, true);
            document.Replace("personname", person.Name + " " + person.LastName, false, true);
            document.Replace("personmelli", person.NationalCode.GetPersianNumber(), false, true);
            document.Replace("agency", reserve.User.AgencyName, false, true);
            document.Replace("price", penalty.GetPersianNumber(), false, true);
            var date = t.ToPersianString(PersianDateTimeFormat.Date).Split('/');
            var newdate = date[2] + '/' + date[1] + '/' + date[0];
            document.Replace("date", newdate, false, true);
            string fileName = "CancelPerson-" + person.NationalCode + "-" + DateTime.Now.Year.ToString() + DateTime.Now.Month + DateTime.Now.Day + "-" +
                              reserve.CodeRahgiri + ".docx";
            document.SaveToFile(Path.Combine(HttpContext.Current.Server.MapPath("/Content/Files/Canceled"), fileName), FileFormat.Docx);
            _costs.Find(personId).CancelFilePath = fileName;
            var r = await _unitOfWork.SaveAllChangesAsync();
            return fileName;
        }
        #endregion

        #region TimeCalculateForAllPersons
        public JarimeViewModel TimeCalculate2(Guid reserveId)
        {
            var g = new JarimeViewModel();
            var reserve = _reserves.Find(reserveId);
            var parent = _persons.Find(reserve.ParentId);
            var t = reserve.Tour.StartTime;
            var days = t - DateTime.Now;
            int y = 0;

            if (days.TotalDays >= 30)
            {
                g.Percent = 5;
            }
            else if (days.TotalDays <= 29 && days.TotalDays > 15)
            {
                g.Percent = 10;
            }
            else if (days.TotalDays <= 15 && days.TotalDays > 7)
            {
                g.Percent = 15;
            }
            else if (days.TotalDays <= 7 && days.TotalDays > 3)
            {
                g.Percent = 20;
            }
            else if (days.TotalDays <= 3 && days.TotalDays >= 1)
            {
                g.Percent = 30;
            }
            else if (days.TotalHours <= 24 && days.TotalHours > 12)
            {
                g.Percent = 40;
            }
            else if (days.TotalHours <= 12 && days.TotalHours > 0)
            {
                g.Percent = 50;
            }

            var persons = _persons.Where(p => p.ParentId == parent.Id);

            var cost = 0;
            foreach (var item in persons)
            {
                if (item.AgeRange != AgeRange.Baby)
                {
                    var c = _costs.Find(item.Id);
                    if (!c.IsCanceled)
                        cost += c.PersonCost;
                }
            }

            g.Price = cost;
            g.PenaltyPrice = (cost * g.Percent) / 100;
            g.FinalTotalPrice = cost - g.PenaltyPrice;
            g.PersonId = parent.Id;
            g.ReserveId = reserveId;
            g.CodeRahgiri = reserve.CodeRahgiri;
            g.PersonName = parent.Name + " " + parent.LastName;

            foreach (var item in persons)
            {
                if (item.AgeRange == AgeRange.Baby)
                {
                    g.PenaltyPrice += item.Cost.PersonCost;
                    g.Price += item.Cost.PersonCost;
                    g.FinalTotalPrice -= item.Cost.PersonCost;
                }
            }
            return g;
        }
        #endregion

        #region CancelAllReserve
        public async Task<string> CancelAllAsync(JarimeViewModel model)
        {
            var reserve = _reserves.Find(model.ReserveId);
            if (reserve.IsCanseled) return reserve.CancelFilePath;
            var g = DateTime.Now;
            reserve.CancelDate = g;
            reserve.IsCanseled = true;
            reserve.Penalty = model.PenaltyPrice;
            var parent = _persons.Find(reserve.ParentId);
            // _reserves.Find(reserveId).TotalCost -= penalty;//total cost hamun costiye k fard dar reserve dard agar cancel shavad jarimeyi bara tour mimand va baghie b mioshtari dade mishvd
            var costs = _costs.Where(p => p.ReserveId == model.ReserveId && !p.IsCanceled);
            foreach (var item in costs)
            {
                item.IsCanceled = true;
                if (item.Person.AgeRange == AgeRange.Baby)
                {
                    item.PersonPenaltyCost = item.PersonCost;
                    item.PersonCost = 0;
                }
                else
                {
                    item.PersonPenaltyCost = (item.PersonCost * model.Percent) / 100;
                    item.PersonCost = 0;
                }
                item.CancelDate = g;
            }
            Document document = new Document();
            document.LoadFromFile(Path.Combine(HttpContext.Current.Server.MapPath("/Content/Files"), "cancelallreserve.docx"));
            document.Replace("name", parent.Name + " " + parent.LastName, false, true);
            document.Replace("code", reserve.CodeRahgiri.GetPersianNumber(), false, true);
            document.Replace("melli", parent.NationalCode.GetPersianNumber(), false, true);
            document.Replace("agency", reserve.User.AgencyName, false, true);
            document.Replace("price", model.PenaltyPrice.GetPersianNumber(), false, true);
            var date = reserve.CancelDate.ToPersianString(PersianDateTimeFormat.Date).Split('/');
            var newdate = date[2] + '/' + date[1] + '/' + date[0];
            document.Replace("date", newdate, false, true);
            string fileName = "CancelAll-" + DateTime.Now.Year.ToString() + DateTime.Now.Month + DateTime.Now.Day + "-" +
                              model.CodeRahgiri + ".docx";
            document.SaveToFile(Path.Combine(HttpContext.Current.Server.MapPath("/Content/Files/Canceled"), fileName), FileFormat.Docx);

            reserve.CancelFilePath = fileName;
            await _seats.Where(p => p.ReserveId == reserve.Id).DeleteAsync();

            var r = await _unitOfWork.SaveAllChangesAsync();
            return fileName;
        }
        #endregion
        

        public async Task<CancelReserveViewModel> GetPersonsOfReserve(Guid reserveId)
        {
            var reserve =await _reserves.SingleAsync(p => p.Id == reserveId);
            var parentId = _reserves.Find(reserveId).ParentId;
            var persons = await _persons.Where(p => p.ParentId == parentId).
                ProjectTo<ShowPersonViewModel>(_configuration).ToListAsync();
            var costs = await _costs.Where(p => p.ReserveId == reserveId).ToListAsync();
            foreach (var item in persons)
            {
                item.ReserveId = reserveId;
                var cost = costs.Single(p => p.Person.Id == item.Id);
                item.IsCanceled = cost.IsCanceled;
                item.CancelFilePath = cost.CancelFilePath;
            }
            return new CancelReserveViewModel() {Persons = persons,CancelAllFilePath = reserve.CancelFilePath};
        }


        public async Task<int> Schedule()
        {
            var reserve = _reserves.Where(p => p.IsTemporary).AsNoTracking().ToList();
            var NowTime = DateTime.Now;
            foreach (var item in reserve)
            {
                var reservetime = item.ReserveTime;
                var i = NowTime - reservetime;
                if (i.TotalMinutes >= 15)
                {
                    await _costs.Where(p => p.ReserveId == item.Id).DeleteAsync();
                    var person = _persons.Where(p => p.ParentId == item.ParentId);
                    await _reserves.Where(p => p.Id == item.Id).DeleteAsync();
                    await _persons.Where(p => p.ParentId == item.ParentId).DeleteAsync();

                    foreach (var item1 in person)
                    {
                        await _seats.Where(p => p.PersonId == item1.Id).DeleteAsync();
                    }
                }
            }
            return await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task<string> SetTemporary(Guid reserveId)
        {
            var reserve = _reserves.Find(reserveId);
            var user = _userManager.FindUserById(reserve.UserId);
            reserve.IsTemporary = false;
            var tour = _tours.Find(reserve.TourId);
            var parent = _persons.Find(reserve.ParentId);
            var persons = await _persons.Where(p => p.ParentId == parent.Id && p.Id!=p.ParentId).ToListAsync();

            Document doc = new Document();
            doc.LoadFromFile(Path.Combine(HttpContext.Current.Server.MapPath("/Content/Files"), "garardad.docx"));
            doc.Replace("person", parent.Name + " " + parent.LastName, false, true);
            doc.Replace("coderahgiri", reserve.CodeRahgiri.GetPersianNumber(), false, true);
            doc.Replace("nationalcode", parent.NationalCode.GetPersianNumber(), false, true);
            doc.Replace("identitycode", parent.IdentityCode.GetPersianNumber(), false, true);
            doc.Replace("Parent", parent.ParentName, false, true);

            var birthdate = parent.BirthDay.ToPersianString(PersianDateTimeFormat.Date).Split('/');
            var newbirthdate = birthdate[2] + '/' + birthdate[1] + '/' + birthdate[0];
            doc.Replace("birthdate", newbirthdate, false, true);

            doc.Replace("mobile", parent.Mobile.GetPersianNumber(), false, true);
            doc.Replace("count", (persons.Count+1).ToString().GetPersianNumber(), false, true);
            doc.Replace("state", "آذربایجان غربی", false, true);

            var date2 = DateTime.Now.ToPersianString(PersianDateTimeFormat.Date).Split('/');
            var newdate2 = date2[2] + '/' + date2[1] + '/' + date2[0];
            doc.Replace("date2",newdate2 , false, true);

            var time = DateTime.Now.Hour.ToString().GetPersianNumber() + ":" +
                       DateTime.Now.Minute.ToString().GetPersianNumber();
            doc.Replace("time",time , false, true);

            doc.Replace("visitor", tour.SupervisorName, false, true);

            //// safe dovome file garardad
            doc.Replace("agencyname", user.AgencyName, false, true);
            doc.Replace("tourname", tour.Title, false, true);
            doc.Replace("source", _city.Single(p=>p.Id==tour.SourceCityId).Name, false, true);
            doc.Replace("destination", _city.Single(p=>p.Id==tour.DestinationCityId).Name, false, true);
            doc.Replace("duration", (tour.EndTime-tour.StartTime).Days.ToString(), false, true);

            var movedate = tour.StartTime.ToPersianString(PersianDateTimeFormat.Date).Split('/');
            var newmovedate = movedate[2] + '/' + movedate[1] + '/' + movedate[0];
            doc.Replace("movedate", newmovedate, false, true);
            var returndate = tour.EndTime.ToPersianString(PersianDateTimeFormat.Date).Split('/');
            var newreturndate = returndate[2] + '/' + returndate[1] + '/' + returndate[0];
            doc.Replace("returndate", newreturndate, false, true);

            doc.Replace("movehour", tour.StartHour.GetPersianNumber(), false, true);
            doc.Replace("couplebedprice", tour.CoupleBedPrice.ToString().GetPersianNumber(), false, true);


            Table table = doc.Sections[0].Tables[0] as Spire.Doc.Table;
            table.AddRow(true, 8);
            table[1, 0].AddParagraph().AppendText("1");
            table[1, 1].AddParagraph().AppendText(parent.Name + " " + parent.LastName);
            table[1, 2].AddParagraph().AppendText(parent.IdentityCode.GetPersianNumber());

            var date3 = parent.BirthDay.ToPersianString(PersianDateTimeFormat.Date).Split('/');
            var newdate3 = date3[2] + '/' + date3[1] + '/' + date3[0];
            table[1, 3].AddParagraph().AppendText(newdate3);

            table[1, 4].AddParagraph().AppendText(parent.BirthPlace);
            table[1, 5].AddParagraph().AppendText(parent.Job);
            table[1, 7].AddParagraph().AppendText(parent.NationalCode.GetPersianNumber());

            for (int i = 2; i < persons.Count+2; i++)
            {
                table.AddRow(true, 8);
                table[i, 0].AddParagraph().AppendText(i.ToString());
                table[i, 1].AddParagraph().AppendText(persons[i-2].Name + " " + persons[i-2].LastName);
                table[i, 2].AddParagraph().AppendText(persons[i-2].IdentityCode.GetPersianNumber());

                var date4 = persons[i - 2].BirthDay.ToPersianString(PersianDateTimeFormat.Date).Split('/');
                var newdate4 = date4[2] + '/' + date4[1] + '/' + date4[0];
                table[i, 3].AddParagraph().AppendText(newdate4);

                table[i, 4].AddParagraph().AppendText(persons[i-2].BirthPlace);
                table[i, 6].AddParagraph().AppendText(persons[i-2].Relation);
                table[i, 7].AddParagraph().AppendText(persons[i-2].NationalCode.GetPersianNumber());
            }

            //jadvale dovom
            doc.Replace("movedate", newmovedate, false, true);
            var desCity=_city.Single(p => p.Id == tour.DestinationCityId);
            doc.Replace("descity",desCity.Name , false, true);
            doc.Replace("goroad", tour.GoRoad.HasValue() ? tour.GoRoad : "", false, true);
            doc.Replace("comeroad", tour.ComeRoad.HasValue() ? tour.ComeRoad : "", false, true);
            string hotels = "",stars="";
            foreach (var item in tour.TourHotels)
            {
                hotels += item.Hotel.Name + "،";
                stars += item.Hotel.Degree + "،";
            }
            hotels = hotels.Substring(0, hotels.Length - 1);
            stars = stars.Substring(0, stars.Length - 1);
            doc.Replace("hotel", hotels, false, true);
           
            doc.Replace("star", stars, false, true);

            var filename = "reserve-" + reserve.Id + "-" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + ".docx";
            reserve.ContractFilePath = filename;
            doc.SaveToFile(Path.Combine(HttpContext.Current.Server.MapPath("/Content/Files/FinalReserves/"),filename), FileFormat.Docx);
            var result = await _unitOfWork.SaveAllChangesAsync();
            return filename;
        }
    }

}
