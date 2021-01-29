using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.ViewModel.Common;
using Agency.ViewModel.Vehicle;

namespace Agency.ServiceLayer.Contracts.Vehicle
{
    public interface IVehicleService
    {
        Task<ShowVehicleViewModel> Create(CreateVehicleViewModel viewModel);

        Task<VehicleListViewModel> GetPagedListAsync(VehicleSearchRequest request);

        Task<VehicleEditViewModel> GetEditViewAsync(Guid id);

        Task<ShowVehicleViewModel> Edit(VehicleEditViewModel viewModel);

        Task<DeleteMessageViewModel> DeleteAsync(Guid id);

        VehicleFormatViewModel CreateVehicleFormatView(Guid id);

        Task<int> AddFormat(VehicleFormatViewModel viewModel);
    }
}
