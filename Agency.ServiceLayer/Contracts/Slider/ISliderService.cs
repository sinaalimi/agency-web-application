using System;
using System.Threading.Tasks;
using Agency.ViewModel.Slider;

namespace Agency.ServiceLayer.Contracts.Slider
{
    public interface ISliderService
    {
        void Create(AddSlideViewModel slide);

        bool IsIndexExist(int index);

        int LastIndex();

        Task<SliderListViewModel> GetPagedListAsync(SliderSearchRequest request);

        Task<int> DeleteAsync(Guid id);

        Task<EditSlideViewModel> GetForEdit(Guid id);

        void Edit(EditSlideViewModel viewModel);

        bool IsIndexValid(int index);

        SliderListViewModel GetPagedList(SliderSearchRequest request);
    }
}
