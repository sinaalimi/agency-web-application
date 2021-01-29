using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Agency.ViewModel.Common;

namespace Agency.ViewModel.ReserveHotel
{
    public class ReserveHotelSearchRequest : BaseSearchRequest
    {
        public ReserveHotelSearchRequest()
        {
            CurrentSort = "User";
        }

        [DisplayName("استان")]
        public Guid StateId { get; set; }

        [DisplayName("شهر")]
        public Guid CityId { get; set; }

        public List<SelectListItem> States { get; set; }

        public List<SelectListItem> Cities { get; set; }

        [DisplayName("از تاریخ")]
        [UIHint("PersianDatePicker")]
        public DateTime? StartDate { get; set; }

        [DisplayName("به مدت")]
        public int? Night { get; set; }

        public static class ApplicantSortBy
        {
            public const string UserItem = nameof(User);
            public const string Start = nameof(StartDate);
            public const string Night = nameof(Night);
            public const string StateItem = nameof(StateItem);
            public const string CityItem = nameof(StateItem);

        }
    }
}
