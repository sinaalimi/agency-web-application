using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.ViewModel.Report
{
    public class ToursReportViewModel
    {
        public Guid TourId { get; set;  }

        [DisplayName("نام دفتر")]
        public string UserName { get; set; }

        [DisplayName("اتاق دونفره")]

        public bool CoupleBed { get; set; }

        [DisplayName("نام سرپرست")]
        public string ParentName { get; set; }


        [DisplayName("نام")]

        public string Name { get; set; }

        [DisplayName("نام خانوادگی")]

        public string LastName { get; set; }

        [DisplayName("کد ملی")]

        public string NationalCode { get; set; }

        [DisplayName("شماره شناسنامه")]

        public string IdCode { get; set; }

        [DisplayName("محل تولد")]

        public string BirthPlace { get; set; }

        [DisplayName("تاریخ تولد")]

        public string BirthDate { get; set; }

        [DisplayName("جنسیت")]

        public string Gender { get; set; }

        [DisplayName("تلفن")]

        public string Tel { get; set; }

        [DisplayName("موبایل")]
        public string Mobile { get; set; }

        [DisplayName("آدرس")]

        public string Address { get; set; }

        [DisplayName("شماره صندلی")]
        public int SeatNo { get; set; }

        [DisplayName("نام تور")]
        public string TourName { get; set; }

        [DisplayName("هزینه کل")]

        public int BaseCost { get; set; }  

        [DisplayName("هزینه اتاق دونفره")]

        public int CoupleBedCost { get; set; }

        [DisplayName("نام وسیله نقلیه")]
        public string VehicleName { get; set; }

        [DisplayName("نام هتل")]

        public string HotelName { get; set; }

        public  string Relation { get; set; }

        public  string Adress { get; set; }

        public int insurance { get; set; }

        public int OptionCost { get; set; }

        public int agencyshare { get; set; }

        public int OptBasecost { get; set;  }

        public int TotalCost { get; set; }

        public int TotalAgencyShare { get; set; }
        public int PersonId { get; set; }

        public int Row { get; set; }
        public int Col { get; set; }
        public int VehickeCounter { get; set; }
        public int VehicleIndex { get; set; }

        public Guid VehicleId { get; set;  }





    }
}
