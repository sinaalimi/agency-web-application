using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.ViewModel.ReserveHotel
{
    public class RoomFeaturesViewModel
    {
        public Guid RoomId { get; set; }

        public Guid RoomHotelId { get; set; }

        public int Capacity { get; set; }

        public string Type { get; set; }

        public string Options { get; set; }

        public int TotalPrice { get; set; }

    }
}
