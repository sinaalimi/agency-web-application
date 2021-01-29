using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Agency.DomainClasses.Entities.tour;
using Agency.DomainClasses.Entities.User;
using Agency.DomainClasses.Entities.Vehicle;
using Agency.ViewModel.Tour;
using Agency.ViewModel.User;
using Agency.ViewModel.Vehicle;
using AutoMapper;

namespace Agency.AutoMapper
{
    public class VehicleProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Vehicle, CreateVehicleViewModel>();
            CreateMap<CreateVehicleViewModel, Vehicle>();

            CreateMap<Vehicle, ShowVehicleViewModel>();
            CreateMap<ShowVehicleViewModel, Vehicle>();

            CreateMap<Vehicle, VehicleEditViewModel>();
            CreateMap<VehicleEditViewModel, Vehicle>();

            CreateMap<Vehicle, SmallVehicleListViewModel>();
            CreateMap<SmallVehicleListViewModel, Vehicle>();

            CreateMap<SeatFormat, SeatViewModel>();
            CreateMap<SeatViewModel, SeatFormat>();

            CreateMap<SeatFormat, CreateSaetViewModel>();
            CreateMap<CreateSaetViewModel, SeatFormat>();

        }
        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }
}
