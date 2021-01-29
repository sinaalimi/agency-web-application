using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.DomainClasses.Entities.Reserve;
using Agency.DomainClasses.Entities.Vehicle;
using Agency.ViewModel.Reserve;
using Agency.ViewModel.Vehicle;
using AutoMapper;

namespace Agency.AutoMapper
{
    public class ReserveProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Reserve, CreateReserveViewModel>();
            CreateMap<CreateReserveViewModel, Reserve>();
            CreateMap<Reserve, ShowReserveViewModel>();
            CreateMap<ShowReserveViewModel, Reserve>();
            CreateMap<Reserve, EditReserveViewModel>();
            CreateMap<EditReserveViewModel, Reserve>();

        }
        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }
}
