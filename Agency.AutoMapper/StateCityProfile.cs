using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.DomainClasses.Entities.City;
using Agency.DomainClasses.Entities.Hotel;
using Agency.DomainClasses.Entities.State;
using Agency.ViewModel.Hotel;
using Agency.ViewModel.StateCity;
using AutoMapper;

namespace Agency.AutoMapper.Extensions
{
    public class StateCityProfile : Profile
    {
        protected override void Configure()
        {

            CreateMap<State, CreateStateViewModel>();
            CreateMap<CreateStateViewModel, State>();

            CreateMap<City, CreateCityViewModel>();
            CreateMap<CreateCityViewModel, City>();
            CreateMap<City, ShowCityViewModel>();
            CreateMap<ShowCityViewModel, City>();
            CreateMap<State, ShowStateViewModel>();
            CreateMap<ShowStateViewModel, State >();
            CreateMap<State, EditStateViewModel>();
            CreateMap<EditStateViewModel, State>();
            CreateMap<City, EditCityViewModel>();
            CreateMap<EditCityViewModel, City>();

        }
    }
}
