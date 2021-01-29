using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Agency.ViewModel.Common;

namespace Agency.ViewModel.Hotel
{
    public class HotelSearchRequest:BaseSearchRequest
    {
        public HotelSearchRequest()
        {
            CurrentSort = "State";
        }

        [DisplayName("استان")]
        public string State { get; set; }

        public Guid StateId { get; set; }
        public List<SelectListItem> States { get; set; }

        public short? Degree { get; set; }


        public static class ApplicantSortBy
        {
            public const string State = nameof(State);
            public const string Degree = nameof(Degree);
        }
    }
}
