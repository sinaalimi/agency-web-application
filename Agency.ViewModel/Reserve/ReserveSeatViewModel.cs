using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Agency.ViewModel.Vehicle;

namespace Agency.ViewModel.Reserve
{
   public class ReserveSeatViewModel
    {
        public List<SeatViewModel> SeatList { get; set; }
         
        public List<SelectListItem> PersonList { get; set; } 

        public string VehicleName { get; set; }

        [DisplayName("شخص")]
        public Guid PersonId { get; set; }

        public Guid ReserveId { get; set; }

        [DisplayName("وسیله نقلیه")]
        public Guid TourVehicleId { get; set; }

        public int? FromSeat { get; set; }
        public int? ToSeat { get; set; }

        /// <summary>
        /// در صورتی که از صندل پر به خالی انتقال داده شود مقدار ترو میشود
        /// </summary>
        public bool Notify { get; set; }

        /// <summary>
        /// در صورتی که به/از صندل غیر فعال انتقال داده شود ترو میشود
        /// </summary>
        public bool MoveToDeactive { get; set; }

        /// <summary>
        /// دو صندل دارای یک شماره باشند
        /// </summary>
        public bool SameSeatNumbers { get; set; }

        public DateTime Reservetime { get; set; }
    }
}
