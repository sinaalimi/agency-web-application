using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.DomainClasses.Entities.Hotel;
using Agency.DomainClasses.Entities.tour;
using Agency.ViewModel.Hotel;
using Agency.ViewModel.Tour;
using AutoMapper;

namespace Agency.AutoMapper
{
    public class HotelProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Hotel, CreateHotelViewModel>();
            CreateMap<CreateHotelViewModel, Hotel>();

            CreateMap<Hotel, ShowHotelViewModel>();
            CreateMap<ShowHotelViewModel, Hotel>();

            CreateMap<Hotel, EditHotelViewModel>();
            CreateMap<EditHotelViewModel, Hotel>();

            CreateMap<Hotel, SmallHotelListViewModel>();
            CreateMap<SmallHotelListViewModel, Hotel>();

            CreateMap<Hotel, HotelDetailViewModel>();
        }
        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }
}
