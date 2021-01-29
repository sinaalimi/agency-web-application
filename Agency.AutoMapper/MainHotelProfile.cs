using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.DomainClasses.Entities.MainHotel;
using Agency.DomainClasses.Entities.MainHotelOption;
using Agency.DomainClasses.Entities.tour;
using Agency.DomainClasses.Entities.TourOption;
using Agency.ViewModel.MainHotel;
using Agency.ViewModel.Option;
using Agency.ViewModel.Tour;
using Agency.ViewModel.Vehicle;
using AutoMapper;

namespace Agency.AutoMapper
{
    public class MainHotelProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<MainHotel, CreateMainHotelViewModel>();
            CreateMap<CreateMainHotelViewModel, MainHotel>();

            CreateMap<MainHotelOption,HotelOptionViewModel>();


         
            CreateMap<MainHotel, EditMainHotelViewModel>();
            CreateMap<EditMainHotelViewModel, MainHotel>();

            CreateMap<MainHotel, ShowMainHotelViewModel>();
            CreateMap<ShowMainHotelViewModel, MainHotel>();
          
            CreateMap<MainHotelOption, HotelOptionViewModel>();

           // CreateMap<MainHotel, TourDetailViewModel>();


        }
        public override string ProfileName
        {
            get { return GetType().Name; }
        }

    }
}
