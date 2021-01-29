using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.ViewModel.Report
{
    public class OfficePersonSeatViewModel
    {
        public Guid TourId { get; set; }

        [DisplayName("نام دفتر")]
        public string UserName { get; set; }

        [DisplayName("نام تور")]
        public string TourName { get; set; }

        [DisplayName("نام")]
        public string Name { get; set; }

        [DisplayName("نام خانوادگی")]
        public string LastName { get; set; }

        [DisplayName("شماره صندلی")]
        public int SeatNo { get; set; }

        [DisplayName("نام وسیله نقلیه")]
        public string VehicleName { get; set; }

        [DisplayName("نام سرپرست")]
        public string ParentName { get; set; }

        [DisplayName("رابطه")]
        public string Relation { get; set; }



    }
}
