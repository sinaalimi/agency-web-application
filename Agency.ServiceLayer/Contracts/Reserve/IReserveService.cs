using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Agency.DomainClasses.Entities.Person;
using Agency.ViewModel.Common;
using Agency.ViewModel.Jarime;
using Agency.ViewModel.Reserve;
using Agency.ViewModel.Vehicle;

namespace Agency.ServiceLayer.Contracts.Reserve
{
    public interface IReserveService
    {
        Task<List<SelectListItem>> GetHotels(Guid id);

        Task<Guid> CreateReservePerson(CreateReserveViewModel model);

        Task<List<SelectListItem>> GetVehicles(Guid id);

        Task<ReserveSeatViewModel> GetPerson(Guid reserveId);

        Task<int> SeatReserve(Guid personid, Guid seatid, Guid reservid);

        Task<ReserveListViewModel> GetPagedListAsync(ReserveSearchRequest request);

        Task<EditReserveViewModel> GetEdit(Guid id);

        Task<ShowReserveViewModel> Edit(EditReserveViewModel viewModel);

        Task<DeleteMessageViewModel> DeleteAsync(Guid id);

        Task<DeleteMessageViewModel> DeletePersonOfSeat(Guid id);

        JarimeViewModel TimeCalculate(Guid reserveId, Guid personId);
        Task<string> CancelAsync(Guid reserveId, Guid personId, int penalty);

        Task<CancelReserveViewModel> GetPersonsOfReserve(Guid reserveId);


        JarimeViewModel TimeCalculate2(Guid reserveId);

        Task<string> CancelAllAsync(JarimeViewModel model);

        Task<int> Schedule();

        Task<string> SetTemporary(Guid reserveId);
    }
}
