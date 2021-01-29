using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Agency.ViewModel.Common;
using Agency.ViewModel.Hotel;
using Agency.ViewModel.Vehicle;

namespace Agency.ServiceLayer.Contracts.Hotel
{
    public interface IHotelService
    {
        void Create(CreateHotelViewModel viewModel);

        Task<HotelListViewModel> GetPagedListAsync(HotelSearchRequest request);

        Task<EditHotelViewModel> GetEditViewAsync(Guid id);

        Task<ShowHotelViewModel> Edit(EditHotelViewModel viewModel);

        Task<DeleteMessageViewModel> DeleteAsync(Guid id);

        Task<CreateHotelViewModel> GetCreateViewModelAsync();

        List<SelectListItem> GetHotelsOfCity(Guid id);

        Task<HotelDetailViewModel> GetHotelDetails(Guid id);
    }
}
