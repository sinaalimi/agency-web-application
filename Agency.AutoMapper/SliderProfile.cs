using Agency.DomainClasses.Entities.Slider;
using Agency.ViewModel.Slider;
using AutoMapper;

namespace Agency.AutoMapper
{
    public class SliderProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<AddSlideViewModel, Slider>();
            CreateMap<Slider, AddSlideViewModel>();

            CreateMap<Slider, SliderViewModel>();
            CreateMap<SliderViewModel, Slider>();

            CreateMap<Slider, EditSlideViewModel>();
            CreateMap<EditSlideViewModel, Slider>();
        }

        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }
}
