using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.ViewModel.Common;

namespace Agency.ViewModel.Reserve
{
   public  class ReserveSearchRequest : BaseSearchRequest
    {

        public ReserveSearchRequest()
        {
            //CurrentSort = "Tour.Title";
            CurrentSort = "ReserveTime";
        }

        [DisplayName("نام تور")]
        public string Name { get; set; }


        [DisplayName("تاریخ ثبت")]
        [UIHint("PersianDatePickerInputSm")]
        [DataType(DataType.Date)]
        public DateTime? ReserveTime { get; set; }


        public string CodeRahgiri { get; set; }

        public bool IsCanceled { get; set; }


        public static class ApplicantSortBy
        {
            public const string Name = "Tour.Title";
            public const string CodeRahgiri = "CodeRahgiri";
            public const string ReserveTime = "ReserveTime";
        }
    }
}
