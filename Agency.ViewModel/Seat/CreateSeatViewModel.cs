using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.ViewModel.Seat
{
    public class CreateSeatViewModel
    {
        #region Properties
        public bool SeatGender { get; set; }

        public bool Isempty { get; set; }

        public bool Isvalid { get; set; }

        public Guid SeatId { get; set; }

        public Guid UserId { get; set; }

        public Guid TourVehicleId { get; set; }
        #endregion
    }
}
