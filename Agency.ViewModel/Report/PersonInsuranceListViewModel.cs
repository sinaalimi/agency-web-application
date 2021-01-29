using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.ViewModel.Report
{
    public class PersonInsuranceListViewModel
    {
        [DisplayName("نام دفتر")]
        public string UserName { get; set; }

        [DisplayName("نام تور")]
        public string TourName { get; set; }

        [DisplayName("نام")]
        public string Name { get; set; }

        [DisplayName("نام خانوادگی")]
        public string LastName { get; set; }

        [DisplayName("کد ملی")]
        public string NationalCode { get; set; }

        [DisplayName("شماره شناسنامه")]

        public string IdCode { get; set; }

        [DisplayName("هزینه بیمه")]

        public int InsuranceCost { get; set; }

        [DisplayName("موبایل")]
        public string Mobile { get; set; }
        public string ParentName { get; set; }
        public string Relation { get; set; }

        public int TotalInsurance { get; set;  }

        public Guid TourId { get; set; }

    }
}
