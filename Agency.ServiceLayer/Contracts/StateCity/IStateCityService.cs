using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Agency.ViewModel.Common;
using Agency.ViewModel.StateCity;

namespace Agency.ServiceLayer.Contracts.StateCity
{
    public interface IStateCityService
    {
        Task<List<SelectListItem>> GetStatesAsync();
        List<SelectListItem> GetCities(Guid id);
        void Create(CreateStateViewModel viewModel);
        void CreateCity(CreateCityViewModel viewModel);
        Task<ListCityViewModel> GetPagedListCityAsync(CitySearchRequest request);
        Task<ListStateViewModel> GetPagedListAsync(StateSearchRequest request);
        Task<EditStateViewModel> GetEditViewAsync(Guid id);
        Task<ShowStateViewModel> Edit(EditStateViewModel viewModel);
        Task<EditCityViewModel> GetEditCityViewAsync(Guid id);
        Task<ShowCityViewModel> EditCity(EditCityViewModel viewModel);
        Task<DeleteMessageViewModel> DeleteAsync(Guid id);
        Task<DeleteMessageViewModel> DeleteCityAsync(Guid id);

        void SeedDatabase();
    }
}
