using Agency.DataLayer.Context;
using Agency.ServiceLayer.Contracts.Room;
using Agency.ServiceLayer.Contracts.Users;
using Agency.ViewModel.Room;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Agency.Common.Extentions;
using Agency.ViewModel.Vehicle;
using AutoMapper.QueryableExtensions;
using Agency.ViewModel.Common;
using EntityFramework.Extensions;

namespace Agency.ServiceLayer.EFService.Room
{
    public class RoomService : IRoomService
    {
        #region Fields

        private readonly IMapper _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IDbSet<DomainClasses.Entities.MainHotel.Room> _rooms;
        private readonly HttpContextBase _httpContextBase;
        private readonly MapperConfiguration _configuration;
        #endregion

        #region Ctor

        public RoomService(HttpContextBase httpContextBase, IUnitOfWork unitOfWork, IApplicationUserManager userManager,
            IMapper mappingEngine, MapperConfiguration configuration)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _rooms = _unitOfWork.Set<DomainClasses.Entities.MainHotel.Room>();
            _mappingEngine = mappingEngine;
            _configuration = configuration;
            _httpContextBase = httpContextBase;
        }
        #endregion

        #region Create
        public void Create(CreateRoomViewModel viewModel)
        {
            var room = _mappingEngine.Map<DomainClasses.Entities.MainHotel.Room>(viewModel);
            room.UserId = _userManager.GetCurrentUserId();
            _rooms.Add(room);
            _unitOfWork.SaveAllChanges();
        }
        #endregion

        #region List
        public async Task<RoomListViewModel> GetPagedListAsync(RoomSearchRequest request)
        {

            var room = _rooms.AsNoTracking();
            //if (request.Type.HasValue())
            //{
            //    vehicle = vehicle.Where(p => p.VehicleType.Contains(request.Type)).AsQueryable();
            //}
            if (request.Type.HasValue())
            {
                room = room.Where(p => p.Type.Contains(request.Type)).AsQueryable();
            }


            room = room.OrderBy($"{request.CurrentSort} {request.SortDirection}");
            //var x = await vehicle.ProjectTo<ShowVehicleViewModel>(_configuration).ToListAsync();

            var query = await room
                    .Skip((request.PageIndex - 1) * 10).Take(10).ProjectTo<ShowRoomViewModel>(_configuration)
                    .ToListAsync();

            return new RoomListViewModel()
            {
                SearchRequest = request,
                Rooms = query
            };
        }
        #endregion


        #region GetEditViewAsync

        public async Task<RoomEditViewModel> GetEditViewAsync(Guid id)
        {
            var model = await _rooms.AsNoTracking()
                        .ProjectTo<RoomEditViewModel>(_configuration)
                        .FirstOrDefaultAsync(p => p.Id == id);
            return model;
        }
        #endregion

        #region Edit

        public async Task<ShowRoomViewModel> Edit(RoomEditViewModel viewModel)
        {
            var room = _rooms.Find(viewModel.Id);

            _mappingEngine.Map(viewModel, room);
            await _unitOfWork.SaveAllChangesAsync();
            return await _rooms.AsNoTracking().ProjectTo<ShowRoomViewModel>(_configuration)
                   .FirstOrDefaultAsync(p => p.Id == viewModel.Id);
        }

        #endregion

        #region Delete
        public async Task<DeleteMessageViewModel> DeleteAsync(Guid id)
        {
            if (!await _rooms.AnyAsync(p => p.Id == id))
                return new DeleteMessageViewModel { Success = false, Message = "رکورد مورد نظر یافت نشد" };
            await _rooms.Where(a => a.Id == id).DeleteAsync();

            return new DeleteMessageViewModel { Success = true };

        }
        #endregion
    }
}
