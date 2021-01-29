using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.ViewModel.ReserveHotel
{
    public class MainHotelListforReserveViewModel
    {
        public Guid HotelId { get; set; }

        public Guid RoomId { get; set; }

        public string HotelName { get; set; }

        public int? MinimumPrice { get; set; }

        public string HotelAddress { get; set; }

        public string Description { get; set; }

        public string ListItemStartDate { get; set; }

        public int Night { get; set; }
    }
}
