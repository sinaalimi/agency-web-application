using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.DomainClasses.Entities.Hotel;
using Agency.DomainClasses.Entities.tour;
using Agency.DomainClasses.Entities.TourOption;
using Agency.ViewModel.Hotel;
using Agency.ViewModel.Option;
using Agency.ViewModel.Tour;
using Agency.ViewModel.Vehicle;
using AutoMapper;

namespace Agency.AutoMapper.Extensions
{
    public class TourProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Tour, CreateTourViewModel>();
            CreateMap<CreateTourViewModel, Tour>();

            CreateMap<Tour, EditTourViewModel>();
            CreateMap<EditTourViewModel, Tour>();

            CreateMap<Tour, ShowTourViewModel>();
            CreateMap<ShowTourViewModel, Tour>();
            CreateMap<Tour, SmallVehicleListViewModel>();
            CreateMap<SmallVehicleListViewModel, Tour>();

            CreateMap<TourOption, OptionViewModel>();

            CreateMap<Tour, TourDetailViewModel>();
        }
        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }
}
