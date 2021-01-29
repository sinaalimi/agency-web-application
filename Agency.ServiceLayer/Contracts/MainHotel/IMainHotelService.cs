using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.ViewModel.Common;
using Agency.ViewModel.MainHotel;
using Agency.ViewModel.Tour;

namespace Agency.ServiceLayer.Contracts.MainHotel
{
    public interface IMainHotelService
    {
        void Create(CreateMainHotelViewModel viewModel);

        Task<CreateMainHotelViewModel> GetCreateViewModelAsync();

        Task<CreateMainHotelViewModel> GetCreateViewModelAsync2(CreateMainHotelViewModel viewmodel);

        Task<ShowMainHotelViewModel> Edit(EditMainHotelViewModel viewModel);

        Task<EditMainHotelViewModel> GetEditViewAsync(Guid id);

        Task<DeleteMessageViewModel> DeleteAsync(Guid id);

        Task<ListMainHotelViewModel> GetPagedListAsync(MainHotelSearchRequest request);







    }
}
