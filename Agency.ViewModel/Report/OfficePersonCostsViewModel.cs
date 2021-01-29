using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.ViewModel.Report
{
    public class OfficePersonCostsViewModel
    {
        [DisplayName("نام دفتر")]
        public string UserName { get; set; }
        public string TourName { get; set; }
        public string Relation { get; set; }

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

        [DisplayName("هزینه کل")]

        public int BaseCost { get; set; }
        public int COupleBedCost { get; set; }
        public int Insurance { get; set; }
        public int OptionCOst { get; set; }
        public int TotalCost { get; set; }

        public int SumCost { get; set;  }

        public int TotalAgencySHare { get; set;  }
        
        public int agencyshare { get; set;  }

        public Guid TourId { get; set; }




    }
}
