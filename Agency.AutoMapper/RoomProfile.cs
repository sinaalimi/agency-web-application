using Agency.DomainClasses.Entities.MainHotel;
using Agency.ViewModel.Room;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.ViewModel.Vehicle;

namespace Agency.AutoMapper
{
    public class RoomProfile : Profile
    {
        protected override void Configure()
        {

            CreateMap<Room, CreateRoomViewModel>();
            CreateMap<CreateRoomViewModel, Room>();

            CreateMap<Room, ShowRoomViewModel>();
            CreateMap<ShowRoomViewModel, Room>();

            CreateMap<Room, RoomEditViewModel>();
            CreateMap<RoomEditViewModel, Room>();

            CreateMap<Room, SmallRoomListViewModel>();
            CreateMap<SmallRoomListViewModel, Room>();

        }
    }
}
