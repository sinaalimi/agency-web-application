using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using System.Web;
using Agency.Common.Extentions;
using Agency.DataLayer.Context;
using Agency.ServiceLayer.Contracts.Users;
using Agency.ServiceLayer.Contracts.Vehicle;
using Agency.ViewModel.Common;
using Agency.ViewModel.Vehicle;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityFramework.Extensions;

namespace Agency.ServiceLayer.EFService.Vehicle
{
    public class VehicleService : IVehicleService
    {
        #region Fields

        private readonly IMapper _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IDbSet<DomainClasses.Entities.Vehicle.Vehicle> _vehicles;
        private readonly IDbSet<DomainClasses.Entities.Vehicle.SeatFormat> _seatFormats;
        private readonly IDbSet<DomainClasses.Entities.Vehicle.Seat> _seats;
        private readonly IDbSet<DomainClasses.Entities.TourVehicle.TourVehicle> _tourVehicles;
        private readonly HttpContextBase _httpContextBase;
        private readonly MapperConfiguration _configuration;
        #endregion

        #region Ctor

        public VehicleService(HttpContextBase httpContextBase, IUnitOfWork unitOfWork, IApplicationUserManager userManager,
            IMapper mappingEngine, MapperConfiguration configuration)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _vehicles = _unitOfWork.Set<DomainClasses.Entities.Vehicle.Vehicle>();
            _seatFormats = _unitOfWork.Set<DomainClasses.Entities.Vehicle.SeatFormat>();
            _seats = _unitOfWork.Set<DomainClasses.Entities.Vehicle.Seat>();
            _tourVehicles = _unitOfWork.Set<DomainClasses.Entities.TourVehicle.TourVehicle>();
            _mappingEngine = mappingEngine;
            _configuration = configuration;
            _httpContextBase = httpContextBase;
        }
        #endregion

        #region Create
        public async Task<ShowVehicleViewModel> Create(CreateVehicleViewModel viewModel)
        {
            var vehicle = _mappingEngine.Map<DomainClasses.Entities.Vehicle.Vehicle>(viewModel);
            _vehicles.Add(vehicle);
            _unitOfWork.SaveAllChanges();
            var model =
                await _vehicles.Where(p => p.Id == vehicle.Id).ProjectTo<ShowVehicleViewModel>(_configuration).SingleAsync();
            return model;
        }
        #endregion

        #region List
        public async Task<VehicleListViewModel> GetPagedListAsync(VehicleSearchRequest request)
        {
            var userId = _userManager.GetCurrentUserId();
            var vehicle = _vehicles.AsNoTracking();

            if (request.Name.HasValue())
            {
                vehicle = vehicle.Where(p => p.Name.Contains(request.Name)).AsQueryable();
            }


            vehicle = vehicle.OrderBy($"{request.CurrentSort} {request.SortDirection}");

            var query = await vehicle
                    .Skip((request.PageIndex - 1) * 10).Take(10).ProjectTo<ShowVehicleViewModel>(_configuration)
                    .ToListAsync();

            return new VehicleListViewModel()
            {
                SearchRequest = request,
                Vehicles = query
            };
        }
        #endregion

        #region GetEditViewAsync

        public async Task<VehicleEditViewModel> GetEditViewAsync(Guid id)
        {
            var model =await _vehicles.AsNoTracking()
                        .ProjectTo<VehicleEditViewModel>(_configuration)
                        .FirstOrDefaultAsync(p => p.Id == id);
            return model;
        }
        #endregion

        #region Edit

        public async Task<ShowVehicleViewModel> Edit(VehicleEditViewModel viewModel)
        {
            var vehicle = _vehicles.Find(viewModel.Id);

            _mappingEngine.Map(viewModel, vehicle);
            await _unitOfWork.SaveAllChangesAsync();
            return await _vehicles.AsNoTracking().ProjectTo<ShowVehicleViewModel>(_configuration)
                   .FirstOrDefaultAsync(p => p.Id == viewModel.Id);
        }

        #endregion

        #region Delete
        public async Task<DeleteMessageViewModel> DeleteAsync(Guid id)
        {
            if (!await _vehicles.AnyAsync(p => p.Id == id))
                return new DeleteMessageViewModel { Success = false, Message = "رکورد مورد نظر یافت نشد" };
            var vehicle = await _vehicles.SingleAsync(p => p.Id == id);
            await _vehicles.Where(a => a.Id == id).DeleteAsync();
            return new DeleteMessageViewModel { Success = true };

        }
        #endregion

        #region CreateVehicleFormatViewAsync

        public VehicleFormatViewModel CreateVehicleFormatView(Guid id)
        {
            int capacity = _vehicles.Find(id).Capacity;
            VehicleFormatViewModel model = new VehicleFormatViewModel();
            model.Seats = new List<CreateSaetViewModel>();
            for (int i = 0; i < capacity; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    model.Seats.Add(new CreateSaetViewModel() {Row = i, Col = j});
                }
            }
            model.SeatCount = capacity;
            model.VehicleId = id;
            model.IsFormated = _seatFormats.Any(p => p.VehicleId == id);
            model.IsRemovable= _seats.Any(p => p.TourVehicle.Vehicle.Id == id);
            model.IsRemovable = !model.IsRemovable;
            return model;
        }

        #endregion

        #region AddFormat
        public async Task<int> AddFormat(VehicleFormatViewModel viewModel)
        {
            await _seatFormats.Where(p => p.VehicleId == viewModel.VehicleId).DeleteAsync();
            foreach (var item in viewModel.Seats.Where(item => item.SeatNumber.HasValue))
            {
                item.VehicleId = viewModel.VehicleId;
                var model = _mappingEngine.Map<DomainClasses.Entities.Vehicle.SeatFormat>(item);
                model.UserId = _userManager.GetCurrentUserId();
                _seatFormats.Add(model);
            }
            return await _unitOfWork.SaveAllChangesAsync();
        }

        #endregion




    }
}
