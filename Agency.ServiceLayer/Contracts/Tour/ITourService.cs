using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.ViewModel.Common;
using Agency.ViewModel.Hotel;
using Agency.ViewModel.Reserve;
using Agency.ViewModel.Tour;

namespace Agency.ServiceLayer.Contracts.Tour
{
    public interface ITourService
    {
        void Create(CreateTourViewModel viewModel);

        Task<ListTourViewModel> GetPagedListAsync(TourSearchRequest request);

        Task<CreateTourViewModel> GetCreateViewModelAsync();

        Task<DeleteMessageViewModel> DeleteAsync(Guid id);

        Task<ShowTourViewModel> Edit(EditTourViewModel viewModel);

        Task<EditTourViewModel> GetEditViewAsync(Guid id);

        Task<TourDetailViewModel> GetTourDetails(Guid id);

        Task<CreateTourViewModel> GetCreateViewModelAsync2(CreateTourViewModel viewmodel);

        Task<int> CancelAsync(Guid id);

        Task<ReserveSeatViewModel> GetSeatManageSchema(SelectVehicleViewModel viewmodel);

        Task<ReserveSeatViewModel> ChangeSeats(ReserveSeatViewModel model);

        Task<bool> DeactiveSeat(Guid tourvehicleid, int seatnumber);
        Task<bool> activeSeat(Guid tourvehicleid, int seatnumber);

        int ActiveToursCount();

        int TotalToursCount();

        int PersonCount();

        Task<int> RemainCount(Guid id);

        Task<ShowTourViewModel> GetTour(Guid tourId);
    }
} 
