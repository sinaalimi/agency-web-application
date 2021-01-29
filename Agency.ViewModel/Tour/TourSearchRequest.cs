using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Agency.ViewModel.Common;

namespace Agency.ViewModel.Tour
{
    public class TourSearchRequest :BaseSearchRequest
    {
        public TourSearchRequest()
        {
            CurrentSort = "StartTime";
        }

        [DisplayName("عنوان")]
        public string Title { get; set; }


        public bool IsCanceled { get; set; }

        [DisplayName("تاریخ اعزام")]
        [UIHint("PersianDatePickerInputSm")]
        [DataType(DataType.Date)]
        public DateTime? StartTime { get; set; }

        //public Guid StateId { get; set; }
        //public List<SelectListItem> States { get; set; }

        //public short? Degree { get; set; }


        public static class ApplicantSortBy
        {
            public const string Title = nameof(Title);
            public const string IsCanceled = nameof(IsCanceled);
            public const string StartTime = nameof(StartTime);
        }
    }
}
