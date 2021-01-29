using Agency.ViewModel.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.ViewModel.Common;

namespace Agency.ServiceLayer.Contracts.Room
{
    public interface IRoomService
    {
        void Create(CreateRoomViewModel viewModel);
        Task<RoomListViewModel> GetPagedListAsync(RoomSearchRequest request);
        Task<RoomEditViewModel> GetEditViewAsync(Guid id);
        Task<ShowRoomViewModel> Edit(RoomEditViewModel viewModel);
        Task<DeleteMessageViewModel> DeleteAsync(Guid id);
    }
}
