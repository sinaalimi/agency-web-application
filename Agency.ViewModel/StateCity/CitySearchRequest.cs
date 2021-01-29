using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Agency.ViewModel.Common;

namespace Agency.ViewModel.StateCity
{
    public class CitySearchRequest:BaseSearchRequest
    {
        public CitySearchRequest()
        {
            CurrentSort = "Name";
        }

        [DisplayName("نام شهر")]
        public string Name { get; set; }

        [DisplayName("استان")]
        public string State { get; set; }

        public Guid StateId { get; set; }
        public List<SelectListItem> States { get; set; }

        public static class ApplicantSortBy
        {
            public const string Name = nameof(Name);

        }
    }
}
