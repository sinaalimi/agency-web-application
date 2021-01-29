using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Agency.ViewModel.Common;

namespace Agency.ViewModel.Report
{
    public class ReportSearchRequest : BaseSearchRequest
    {
        public ReportSearchRequest()
        {
            CurrentSort = "User";
        }

        [DisplayName("دفتر")]
        public List<SelectListItem> UserItem { get; set; }

        public Guid UserId { get; set; }

        [DisplayName("شروع")]
        [UIHint("PersianDatePicker")]
        public DateTime? StartDate { get; set; }

        [DisplayName("پایان")]
        [UIHint("PersianDatePicker")]
        public DateTime? EndDate { get; set; }

        public Guid TourId { get; set; }

        public static class ApplicantSortBy
        {
            public const string UserItem = nameof(User);
            public const string Start = nameof(StartDate);
            public const string End = nameof(EndDate);

        }
    }
}
