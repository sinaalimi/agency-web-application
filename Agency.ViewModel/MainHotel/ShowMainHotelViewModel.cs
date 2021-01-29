using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Agency.ViewModel.Option;
using Agency.ViewModel.Room;
using Agency.ViewModel.RoomMainHotel;

namespace Agency.ViewModel.MainHotel
{
    public class ShowMainHotelViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int level { get; set; }

        public string Adress { get; set; }

        public string Tel { get; set; }

        public HttpPostedFileBase PicSrFile { get; set; }

        public string Image { get; set; }

        public Guid StateId { get; set; }

        public Guid CityId { get; set; }

        public string CityName { get; set; }

        public string StateName { get; set; }

        public List<HotelOptionViewModel> OptionList { get; set; }

        public List<RoomItemViewModel> RoomMainHotelList { get; set; }

        public List<SelectListItem> RoomTypeList { get; set; }

          public List<SelectListItem> Cities { get; set; }

        public List<SelectListItem> States { get; set; }

        public List<SmallRoomListViewModel> Rooms { get; set; }
    }
}
