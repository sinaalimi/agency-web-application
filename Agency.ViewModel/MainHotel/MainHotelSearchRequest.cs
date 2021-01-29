using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.ViewModel.Common;

namespace Agency.ViewModel.MainHotel
{
    public class MainHotelSearchRequest : BaseSearchRequest
    {
        public MainHotelSearchRequest()
        {
            CurrentSort = "Name";
        }

        [DisplayName("نام")]
        public string Name { get; set; }


        //public Guid StateId { get; set; }
        //public List<SelectListItem> States { get; set; }

        //public short? Degree { get; set; }


        public static class ApplicantSortBy
        {
            public const string Name = nameof(Name);
            //public const string Degree = nameof(Degree);
        }
    }
}
